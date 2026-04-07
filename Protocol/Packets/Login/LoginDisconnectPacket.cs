using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginDisconnectPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x00;

        public ChatComponent Message = new TextChatComponent();

        internal override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Message.ToString());
        }
        internal override void Deserialize(Stream stream)
        {
            Message = ChatComponent.Parse(stream.ReadMinecraftString());
        }
    }
}