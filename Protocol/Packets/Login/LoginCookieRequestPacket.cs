using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginCookieRequestPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x05;

        public string Id = string.Empty;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Id);
        }
        public override void Deserialize(Stream stream)
        {
            Id = stream.ReadMinecraftString();
        }
    }
}