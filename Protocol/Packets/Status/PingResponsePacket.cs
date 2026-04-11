
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record PingResponsePacket : IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Status;
        static int IPacket.PacketId => 0x01;

        public long Data { get; set; } = 0;
        
        public PingResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            PingResponsePacket packet = new();
            packet.Data = stream.ReadS64();
            return packet;
        }
    }
}