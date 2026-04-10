using System.Runtime.Serialization;
using System.Text.Json;
using Net.Myzuc.Minecraft.Common.ChatComponents;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginDisconnectPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x00;

        public ChatComponent Message { get; set; } = new TextChatComponent();

        public LoginDisconnectPacket()
        {
            
        }

        internal LoginDisconnectPacket(Stream stream) : base(stream)
        {
            Message = JsonSerializer.Deserialize<ChatComponent>(stream.ReadT16AS32V(), Global.JsonSerializerOptions) ?? throw new SerializationException();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(JsonSerializer.Serialize(Message, Global.JsonSerializerOptions));
        }
    }
}