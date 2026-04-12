using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationDisconnectPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x02;

        public ChatComponent? Message { get; set; } = null;

        public ConfigurationDisconnectPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteNbt(Nbt.Nbt.ToNullableNbt(Message));
        }
        void IPacket.Deserialize(Stream stream)
        {
            ConfigurationDisconnectPacket packet = new();
            Message = Nbt.Nbt.FromNullableNbt<ChatComponent>(stream.ReadNbt());
        }
    }
}