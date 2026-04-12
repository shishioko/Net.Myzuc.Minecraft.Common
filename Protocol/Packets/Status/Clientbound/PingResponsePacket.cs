
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status.Clientbound
{
    public sealed class PingResponsePacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Status;
        int IPacket.PacketId => 0x01;

        public long Data { get; set; } = 0;
        
        public PingResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        void IPacket.Deserialize(Stream stream)
        {
            PingResponsePacket packet = new();
            Data = stream.ReadS64();
        }
    }
}