using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginStartPacket : Packet
    {
        public override bool Serverbound => true;
        public override int Id => 0x00;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;

        public String PlayerName = "";
        public Guid PlayerUUID;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(PlayerName);
            stream.WriteGuid(PlayerUUID);
        }

        public override void Deserialize(Stream stream)
        {
            PlayerName = stream.ReadMinecraftString();
            PlayerUUID = stream.ReadGuid();
        }
    }
}