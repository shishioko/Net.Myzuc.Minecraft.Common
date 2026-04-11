using System.Runtime.Serialization;
using System.Text.Json;
using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginDisconnectPacket : IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;
        static int IPacket.PacketId => 0x00;

        public ChatComponent? Message { get; set; } = null;

        public LoginDisconnectPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(JsonSerializer.Serialize(Message, Global.JsonSerializerOptions));
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginDisconnectPacket packet = new();
            packet.Message = JsonSerializer.Deserialize<ChatComponent>(stream.ReadT16AS32V(), Global.JsonSerializerOptions);
            return packet;
        }
    }
}