using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LogicSuccessPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x02;

        public GameProfile Profile = new();

        public override void Serialize(Stream stream)
        {
            GameProfile.Serialize(stream, Profile);
        }
        public override void Deserialize(Stream stream)
        {
            Profile = GameProfile.Deserialize(stream);
        }
    }
}