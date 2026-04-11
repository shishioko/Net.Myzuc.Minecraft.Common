namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationSuccessPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x03;

        public ConfigurationSuccessPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            return new ConfigurationSuccessPacket();
        }
    }
}