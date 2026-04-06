using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginPluginResponsePacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x02;

        public int Id = 0;
        public byte[] Data = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteU8A(Data);
        }
        public override void Deserialize(Stream stream)
        {
            Id = stream.ReadS32V();
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            Data = ms.ToArray();
        }
    }
}