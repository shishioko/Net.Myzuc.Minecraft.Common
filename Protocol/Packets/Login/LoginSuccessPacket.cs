using Net.Myzuc.Minecraft.Common.Data;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginSuccessPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x02;

        public ResolvedProfile Profile { get; init; } = new();

        public LoginSuccessPacket(Stream stream) : base(stream)
        {
            Profile = ResolvedProfile.Deserialize(stream);
        }
        
        internal override void Serialize(Stream stream)
        {
            ResolvedProfile.Serialize(stream, Profile);
        }
    }
}