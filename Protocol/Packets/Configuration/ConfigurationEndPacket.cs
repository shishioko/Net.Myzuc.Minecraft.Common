namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationEndPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x03;

        public ConfigurationEndPacket()
        {
            
        }

        internal ConfigurationEndPacket(Stream stream) : base(stream)
        {
            
        }
        
        internal override void Serialize(Stream stream)
        {
            
        }
    }
}