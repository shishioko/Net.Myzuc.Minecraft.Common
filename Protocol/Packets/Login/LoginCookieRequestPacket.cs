using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCookieRequestPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x05;

        public Identifier Id { get; set; } = new();

        public LoginCookieRequestPacket()
        {
            
        }

        internal LoginCookieRequestPacket(Stream stream) : base(stream)
        {
            Id = new(stream);
        }
        
        internal override void Serialize(Stream stream)
        {
            Id.Serialize(stream);
        }
    }
}