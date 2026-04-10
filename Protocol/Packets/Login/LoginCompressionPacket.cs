
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCompressionPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x03;

        public int Threshold { get; init; } = 0;

        public LoginCompressionPacket(Stream stream) : base(stream)
        {
            Threshold = stream.ReadS32V();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Threshold);
        }
    }
}