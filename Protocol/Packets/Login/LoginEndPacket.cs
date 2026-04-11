namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginEndPacket: IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x03;

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