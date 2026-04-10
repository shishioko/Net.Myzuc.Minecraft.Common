namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record StatusRequestPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x00;
        
        public StatusRequestPacket()
        {
            
        }

        internal StatusRequestPacket(Stream stream) : base(stream)
        {
            
        }
        
        internal override void Serialize(Stream stream)
        {
            
        }
    }
}