using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginPluginRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x04;

        public int MessageID = 0;
        public string Channel = string.Empty;

        public byte[] Data = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(MessageID);
            stream.WriteMinecraftString(Channel);
            stream.WriteU8A(Data);
        }

        public override void Deserialize(Stream stream)
        {
            MessageID = stream.ReadS32V();
            Channel = stream.ReadMinecraftString();
            using MemoryStream ms = new();
            stream.CopyTo(ms);
            Data = ms.ToArray();
        }
    }
}