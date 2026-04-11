namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginEndPacket: IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;
        static int IPacket.PacketId => 0x03;

        public LoginEndPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            return new LoginEndPacket();
        }
    }
}