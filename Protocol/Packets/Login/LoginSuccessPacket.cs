using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Data.Structs;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginSuccessPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x02;

        public ResolvedProfile Profile { get; set; } = new();

        public LoginSuccessPacket()
        {
            
        }

        internal LoginSuccessPacket(Stream stream) : base(stream)
        {
            Profile = new(stream);
        }
        
        internal override void Serialize(Stream stream)
        {
            Profile.Serialize(stream);
        }
    }
}