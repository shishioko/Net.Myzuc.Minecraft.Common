using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using CommunityToolkit.HighPerformance;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Protocol.Packets;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;
using System.Threading;
using Net.Myzuc.Minecraft.Common.Enums;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Serverbound;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake.Serverbound;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound;

namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public sealed class Connection : IDisposable, IAsyncDisposable
    {
        private static readonly IReadOnlyDictionary<(bool serverbound, ProtocolStage stage, int id), Type> Packets;
        static Connection()
        {
            Dictionary<(bool serverbound, ProtocolStage stage, int id), Type> packets = [];
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsAssignableTo(typeof(IPacket)) && !type.IsAbstract);
            foreach (Type type in types)
            {
                IPacket? instance = Activator.CreateInstance(type) as IPacket;
                Contract.Assert(instance is not null);
                (bool serverbound, ProtocolStage stage, int id) signature = (instance.Serverbound, instance.ProtocolStage, instance.PacketId);
                packets.Add(signature, type);
            }
            Packets = packets;
        }
        private bool Disposed { get; set; }
        private Stream Stream { get; set; }
        private SemaphoreSlim ReadSync { get; } = new(1, 1);
        private SemaphoreSlim WriteSync { get; } = new(1, 1);
        private bool RemoteIsClient { get; }
        private int CompressionThreshold { get; set; } = -1;
        public ProtocolStage ProtocolStage { get; private set; } = ProtocolStage.Handshake;
        
        public Connection(Socket socket, bool remoteIsClient)
        {
            Stream = new NetworkStream(socket, true);
            RemoteIsClient = remoteIsClient;
        }
        public void Encrypt(byte[] secret)
        {
            Stream = new AesCfbStream(Stream, secret, secret, false);
        }
        public async Task<IPacket> ReadAsync(CancellationToken cancellationToken = default)
        {
            using Stream stream1 = await readRawAsync();
            int id = stream1.ReadS32V();
            Packets.TryGetValue((RemoteIsClient, ProtocolStage, id), out Type? type);
            IPacket? packet = type is not null ? Activator.CreateInstance(type) as IPacket : null;
            if (packet is null) throw new ProtocolViolationException($"Unknown packet {(RemoteIsClient ? "Serverbound" : "Clientbound")}/{ProtocolStage}/{id:X2}!");
            packet.Deserialize(stream1);
            Run(packet);
            return packet;

            async Task<Stream> readRawAsync()
            {
                Memory<byte> data;
                try
                {
                    await ReadSync.WaitAsync();
                    data = await Stream.ReadU8AS32VAsync(cancellationToken);
                }
                finally
                {
                    ReadSync.Release();
                }
                if (CompressionThreshold < 0) return data.AsStream();
                Stream stream2 = data.AsStream();
                int decompressedSize = stream2.ReadS32V();
                if (decompressedSize <= 0) return stream2;
                await using ZLibStream zlib = new(stream2, CompressionMode.Decompress, false);
                return zlib.ReadU8A(decompressedSize).ToArray().AsMemory().AsStream();
            }
        }
        public async Task WriteAsync(IPacket packet, CancellationToken cancellationToken = default)
        {
            if (packet.Serverbound == RemoteIsClient || packet.ProtocolStage != ProtocolStage) throw new ProtocolViolationException($"Tried writing unexpected packet: {SignatureToString(packet)}");
            using MemoryStream ms = new();
            ms.WriteS32V(packet.PacketId);
            packet.Serialize(ms);
            await writeRawAsync(ms.ToArray());
            Run(packet);
            return;
            
            async Task writeRawAsync(byte[] data)
            {
                if (CompressionThreshold >= 0)
                {
                    using MemoryStream ms2 = new();
                    if (data.Length < CompressionThreshold)
                    {
                        ms2.WriteS32V(0);
                        ms2.WriteU8A(data);
                    }
                    else
                    {
                        ms2.WriteS32V(data.Length);
                        await using ZLibStream zlib = new(ms2, CompressionMode.Compress, true);
                        zlib.WriteU8A(data);
                        zlib.Flush();
                    }
                    data = ms2.ToArray();
                }
                try
                {
                    await WriteSync.WaitAsync();
                    await Stream.WriteU8AS32VAsync(data.AsMemory(), cancellationToken);
                }
                finally
                {
                    WriteSync.Release();
                }
            }
        }
        private void Run(IPacket packet)
        {
            switch (packet)
            {
                case HandshakePacket handshakePacket:
                {
                    ProtocolStage = handshakePacket.Intent switch
                    {
                        HandshakeIntent.Status => ProtocolStage.Status,
                        HandshakeIntent.Login or HandshakeIntent.Transfer => ProtocolStage.Login,
                        _ => ProtocolStage.Disconnected
                    };
                    break;
                }
                case LoginDisconnectPacket:
                {
                    ProtocolStage = ProtocolStage.Disconnected;
                    break;
                }
                case LoginCompressionPacket loginCompressionPacket:
                {
                    CompressionThreshold = loginCompressionPacket.Threshold;
                    break;
                }
                case LoginEndPacket:
                {
                    ProtocolStage = ProtocolStage.Configuration;
                    break;
                }
                case ConfigurationDisconnectPacket:
                {
                    ProtocolStage = ProtocolStage.Disconnected;
                    break;
                }
                case ConfigurationEndPacket:
                {
                    ProtocolStage = ProtocolStage.Play;
                    break;
                }
            }
        }
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Stream.Dispose();
        }
        public async ValueTask DisposeAsync()
        {
            if (Disposed) return;
            Disposed = true;
            await Stream.DisposeAsync();
        }
        private static string SignatureToString(IPacket packet)
        {
            return $"{(packet.Serverbound ? "Serverbound" : "Clientbound")}/{packet.ProtocolStage}/{packet.PacketId:X2}";
        }
    }
}