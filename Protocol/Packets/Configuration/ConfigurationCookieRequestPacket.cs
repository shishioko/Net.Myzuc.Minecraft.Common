using Net.Myzuc.Minecraft.Common.Data.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCookieRequestPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x00;

        public Identifier Id { get; set; } = new();

        public ConfigurationCookieRequestPacket()
        {
            
        }

        internal ConfigurationCookieRequestPacket(Stream stream) : base(stream)
        {
            Id = new(stream);
        }
        
        internal override void Serialize(Stream stream)
        {
            Id.Serialize(stream);
        }
    }
}