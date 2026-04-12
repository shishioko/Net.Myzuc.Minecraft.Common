
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record PingRequestPacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Status;
        int IPacket.PacketId => 0x01;

        public long Data { get; set; } = 0;
        
        public PingRequestPacket()
        {
            
        }

        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        void IPacket.Deserialize(Stream stream)
        {
            Data = stream.ReadS64();
        }
    }
}