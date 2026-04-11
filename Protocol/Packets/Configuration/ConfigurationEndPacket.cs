namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationEndPacket : IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        static int IPacket.PacketId => 0x03;

        public ConfigurationEndPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            return new ConfigurationEndPacket();
        }
    }
}