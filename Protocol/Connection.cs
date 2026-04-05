using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.Protocol.Packets;

namespace Net.Myzuc.Minecraft.Common.Protocol
{
    public class Connection : IDisposable, IAsyncDisposable
    {
        private bool Disposed { get; set; }
        private Stream Stream { get; set; }
        private int CompressionThreshold { get; set; } = -1;
        public Packet.ProtocolStageEnum ProtocolStage { get; private set; } = Packet.ProtocolStageEnum.Handshake;
        
        public Connection(Socket socket)
        {
            Stream = new NetworkStream(socket, true);
        }
        public async Task<Packet> ReadAsync()
        {
            using MemoryStream ms = await readRawAsync();
            int id = ms.ReadS32V();
            Packet packet = Packet.Create(true, ProtocolStage, id) ?? throw new ProtocolViolationException($"Unknown Packet 0x{id:X2}!");
            packet.Deserialize(ms);
            switch (packet)
            {
                case HandshakePacket handshakePacket:
                {
                    ProtocolStage = handshakePacket.Intent switch
                    {
                        HandshakePacket.IntentEnum.Status => Packet.ProtocolStageEnum.Status,
                        HandshakePacket.IntentEnum.Login => Packet.ProtocolStageEnum.Login,
                        HandshakePacket.IntentEnum.Transfer => Packet.ProtocolStageEnum.Login,
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
    }
}