using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginStartPacket : Packet
    {
        public override bool Serverbound => true;
        protected internal override int PacketId => 0x00;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;

        public ResolvedProfile Profile { get; init; } = new();

        public LoginStartPacket()
        {
            
        }

        internal LoginStartPacket(Stream stream) : base(stream)
        {
            Profile = new(stream);
        }
        
        internal override void Serialize(Stream stream)
        {
            Profile.Serialize(stream);
        }
    }
}