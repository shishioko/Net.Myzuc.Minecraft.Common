using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginStartPacket : Packet
    {
        public override bool Serverbound => true;
        public override int Id => 0x00;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;

        public String player_name;
        public Guid player_uuid;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(player_name);
            stream.WriteGuid(player_uuid);
        }

        public override void Deserialize(Stream stream)
        {
            player_name = stream.ReadMinecraftString();
            player_uuid = stream.ReadGuid();
        }
    }
}