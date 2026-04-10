namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginEndPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x03;

        public LoginEndPacket(Stream stream) : base(stream)
        {
            
        }
        
        internal override void Serialize(Stream stream)
        {
            
        }
    }
}