namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound
{
    public sealed record SuccessPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x03;

        public SuccessPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            
        }
        void IPacket.Deserialize(Stream stream)
        {
            
        }
    }
}