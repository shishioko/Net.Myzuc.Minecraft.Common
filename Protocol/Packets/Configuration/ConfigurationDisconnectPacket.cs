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

        public NbtTag Message = new CompoundNbtTag();

        internal override void Serialize(Stream stream)
        {
            Message.Serialize(stream);
        }
        internal override void Deserialize(Stream stream)
        {
            Message = NbtTag.Deserialize(stream);
        }
    }
}