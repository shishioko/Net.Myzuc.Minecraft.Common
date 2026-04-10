using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCookieRequestPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x05;

        public string Id { get; init; } = string.Empty;

        public LoginCookieRequestPacket()
        {
            
        }

        internal LoginCookieRequestPacket(Stream stream) : base(stream)
        {
            Id = stream.ReadT16AS32V();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Id);
        }
    }
}