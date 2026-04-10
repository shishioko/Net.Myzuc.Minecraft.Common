using System.Text.Json;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record StatusResponsePacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x00;

        public Data.Structs.Status Status { get; set; } = new();
        
        public StatusResponsePacket()
        {
            
        }

        internal StatusResponsePacket(Stream stream) : base(stream)
        {
            Status = JsonSerializer.Deserialize<Data.Structs.Status>(stream.ReadT16AS32V(), Global.JsonSerializerOptions);
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(JsonSerializer.Serialize(Status, Global.JsonSerializerOptions) ?? throw new InvalidDataException());
        }
    }
}