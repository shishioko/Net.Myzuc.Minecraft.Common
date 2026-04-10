namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationSuccessPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x03;

        public ConfigurationSuccessPacket()
        {
            
        }

        internal ConfigurationSuccessPacket(Stream stream) : base(stream)
        {
            
        }
        
        internal override void Serialize(Stream stream)
        {
            
        }
    }
}