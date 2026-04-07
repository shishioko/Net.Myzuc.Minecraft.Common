using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Protocol.Packets;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public class Connection : IDisposable, IAsyncDisposable
    {
        private static readonly IReadOnlyDictionary<(bool serverbound, ProtocolStage stage, int id), Type> Packets;
        static Connection()
        {
            Dictionary<(bool serverbound, ProtocolStage stage, int id), Type> packets = [];
            IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Packet)) && !type.IsAbstract);
            foreach (Type type in types)
            {
                Packet? instance = Activator.CreateInstance(type) as Packet;
                Contract.Assert(instance is not null);
                (bool serverbound, ProtocolStage stage, int id) signature = (instance.Serverbound, instance.ProtocolStage, instance.PacketId);
                packets.Add(signature, type);
            }
            Packets = packets;
        }
        private bool Disposed { get; set; }
        private Stream Stream { get; set; }
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
        public async Task<Packet> ReadAsync(CancellationToken cancellationToken = default)
        {
            using MemoryStream ms = await readRawAsync();
            int id = ms.ReadS32V();
            Packets.TryGetValue((RemoteIsClient, ProtocolStage, id), out Type? type);
            Packet? packet = type is not null ? Activator.CreateInstance(type) as Packet : null;
            if (packet is null) throw new ProtocolViolationException($"Unknown packet {(RemoteIsClient ? "Serverbound" : "Clientbound")}/{ProtocolStage}/{id:X2}!");
            packet.Deserialize(ms);
            Run(packet);
            return packet;

            async Task<MemoryStream> readRawAsync()
            {
                byte[] data = await Stream.ReadU8AAsync(await Stream.ReadS32VAsync(cancellationToken), cancellationToken);
                if (CompressionThreshold < 0) return new(data);
                MemoryStream ms2 = new(data);
                int decompressedSize = ms2.ReadS32V();
                if (decompressedSize <= 0) return ms2;
                await using ZLibStream zlib = new(ms2, CompressionMode.Decompress, false);
                return new(zlib.ReadU8A(decompressedSize));
            }
        }
        public async Task WriteAsync(Packet packet, CancellationToken cancellationToken = default)
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
                await Stream.WriteS32VAsync(data.Length, cancellationToken);
                await Stream.WriteU8AAsync(data, cancellationToken);
            }
        }
        private void Run(Packet packet)
        {
            switch (packet)
            {
                case HandshakePacket handshakePacket:
                {
                    ProtocolStage = handshakePacket.Intent switch
                    {
                        HandshakePacket.IntentEnum.Status => ProtocolStage.Status,
                        HandshakePacket.IntentEnum.Login or HandshakePacket.IntentEnum.Transfer => ProtocolStage.Login,
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
            }
        }
        public virtual void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Stream.Dispose();
            GC.SuppressFinalize(this);
        }
        public virtual async ValueTask DisposeAsync()
        {
            if (Disposed) return;
            Disposed = true;
            await Stream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
        private static string SignatureToString(Packet packet)
        {
            return $"{(packet.Serverbound ? "Serverbound" : "Clientbound")}/{packet.ProtocolStage}/{packet.PacketId:X2}";
        }
    }
}