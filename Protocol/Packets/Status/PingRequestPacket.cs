
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record PingRequestPacket : IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Status;
        static int IPacket.PacketId => 0x01;

        public long Data { get; set; } = 0;
        
        public PingRequestPacket()
        {
            
        }

        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            PingRequestPacket packet = new();
            packet.Data = stream.ReadS64();
            return packet;
        }
    }
}