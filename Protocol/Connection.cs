using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.Protocol.Packets;
using Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake;

namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public class Connection : IDisposable, IAsyncDisposable
    {
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
        public async Task<TPacket> ReadAsync<TPacket>() where TPacket : Packet
        {
            Packet packet = await ReadAsync();
            if (packet is not TPacket tpacket) throw new ProtocolViolationException($"Read unexpected Packet {SignatureToString(packet)}!");
            return tpacket;
        }
        public async Task<Packet> ReadAsync()
        {
            using MemoryStream ms = await readRawAsync();
            int id = ms.ReadS32V();
            Packet packet = PacketRegistry.Create(RemoteIsClient, ProtocolStage, id);
            packet.Deserialize(ms);
            switch (packet)
            {
                case HandshakePacket handshakePacket:
                {
                    ProtocolStage = handshakePacket.Intent switch
                    {
                        HandshakePacket.IntentEnum.Status => ProtocolStage.Status,
                        HandshakePacket.IntentEnum.Login => ProtocolStage.Login,
                        HandshakePacket.IntentEnum.Transfer => ProtocolStage.Login,
                        _ => throw new ProtocolViolationException("Unknown Intent!")
                    };
                    break;
                }
            }
            return packet;

            async Task<MemoryStream> readRawAsync()
            {
                byte[] data = await Stream.ReadU8AAsync(await Stream.ReadS32VAsync());
                if (CompressionThreshold < 0) return new(data);
                MemoryStream ms2 = new(data);
                int decompressedSize = ms2.ReadS32V();
                if (decompressedSize <= 0) return ms2;
                await using ZLibStream zlib = new(ms2, CompressionMode.Decompress, false);
                return new(zlib.ReadU8A(decompressedSize));
            }
        }
        public async Task WriteAsync(Packet packet)
        {
            if (packet.Serverbound == RemoteIsClient || packet.ProtocolStage != ProtocolStage) throw new ProtocolViolationException($"Tried writing unexpected packet: {SignatureToString(packet)}");
            using MemoryStream ms = new();
            ms.WriteS32V(packet.Id);
            packet.Serialize(ms);
            await writeRawAsync(ms.ToArray());
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
                        using MemoryStream msCompressed = new();
                        await using ZLibStream zlib = new(msCompressed, CompressionMode.Compress, true);
                        zlib.WriteU8A(data);
                        byte[] compressed = msCompressed.ToArray();
                        ms2.WriteS32V(compressed.Length);
                        ms2.WriteU8A(compressed);
                    }
                    data = ms2.ToArray();
                }
                Stream.WriteS32V(data.Length);
                Stream.WriteU8A(data);
            }
        }
        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Stream.Dispose();
            GC.SuppressFinalize(this);
        }
        public async ValueTask DisposeAsync()
        {
            if (Disposed) return;
            Disposed = true;
            await Stream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
        private static string SignatureToString(Packet packet)
        {
            return $"{(packet.Serverbound ? "Serverbound" : "Clientbound")}/{packet.ProtocolStage}/{packet.Id:X2}";
        }
    }
}