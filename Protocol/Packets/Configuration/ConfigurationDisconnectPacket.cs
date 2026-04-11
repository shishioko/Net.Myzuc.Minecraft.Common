using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationDisconnectPacket : IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        static int IPacket.PacketId => 0x02;

        public ChatComponent? Message { get; set; } = null;

        public ConfigurationDisconnectPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteNbt(Nbt.Nbt.ToNullableNbt(Message));
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            ConfigurationDisconnectPacket packet = new();
            packet.Message = Nbt.Nbt.FromNullableNbt<ChatComponent>(stream.ReadNbt());
            return packet;
        }
    }
}