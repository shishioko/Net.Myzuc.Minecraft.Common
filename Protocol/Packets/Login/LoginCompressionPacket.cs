using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginCompressionPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x03;

        public int Threshold = 0;

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(Threshold);
        }
        public override void Deserialize(Stream stream)
        {
            Threshold = stream.ReadS32V();
        }
    }
}