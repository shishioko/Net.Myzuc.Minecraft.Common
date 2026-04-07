using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginCustomRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public int Id = 0;
        public string Channel = string.Empty;
        public byte[] Data = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteMinecraftString(Channel);
            stream.WriteU8A(Data);
        }
        public override void Deserialize(Stream stream)
        {
            Id = stream.ReadS32V();
            Channel = stream.ReadMinecraftString();
            Data = stream.ReadU8A();
        }
    }
}