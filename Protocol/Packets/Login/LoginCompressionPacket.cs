
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginCompressionPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x03;

        public int Threshold = 0;

        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Threshold);
        }
        internal override void Deserialize(Stream stream)
        {
            Threshold = stream.ReadS32V();
        }
    }
}