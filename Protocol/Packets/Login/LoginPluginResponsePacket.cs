using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginPluginResponsePacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x02;

        public int SequenceId = 0;
        public byte[] Data = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(SequenceId);
            stream.WriteU8A(Data);
        }
        public override void Deserialize(Stream stream)
        {
            SequenceId = stream.ReadS32V();
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            Data = ms.ToArray();
        }
    }
}