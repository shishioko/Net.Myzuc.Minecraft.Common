using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed class ConfigurationDisconnectPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x02;

        public ChatComponent Message = new TextChatComponent();

        internal override void Serialize(Stream stream)
        {
            stream.WriteNbt(Message);
        }
        internal override void Deserialize(Stream stream)
        {
            Message = stream.ReadNbt<ChatComponent>();
        }
    }
}