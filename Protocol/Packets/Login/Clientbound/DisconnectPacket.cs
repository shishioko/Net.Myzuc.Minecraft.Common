using System.Text.Json;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects.ChatComponents;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound
{
    public sealed record DisconnectPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x00;

        public ChatComponent? Message { get; set; } = null;

        public DisconnectPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(JsonSerializer.Serialize(Message, Global.JsonSerializerOptions));
        }
        void IPacket.Deserialize(Stream stream)
        {
            DisconnectPacket packet = new();
            Message = JsonSerializer.Deserialize<ChatComponent>(stream.ReadT16AS32V(), Global.JsonSerializerOptions);
        }
    }
}