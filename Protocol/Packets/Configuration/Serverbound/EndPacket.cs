namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Serverbound
{
    public sealed record EndPacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x03;

        public EndPacket()
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