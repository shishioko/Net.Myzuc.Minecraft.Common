namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound
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
        void IPacket.Deserialize(Stream stream)
        {
            
        }
    }
}