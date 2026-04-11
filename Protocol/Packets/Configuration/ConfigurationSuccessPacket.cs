namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationSuccessPacket : IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        static int IPacket.PacketId => 0x03;

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