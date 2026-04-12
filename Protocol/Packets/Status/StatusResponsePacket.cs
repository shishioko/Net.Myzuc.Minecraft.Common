using System.Runtime.Serialization;
using System.Text.Json;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record StatusResponsePacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Status;
        int IPacket.PacketId => 0x00;

        public Data.Structs.Status Status { get; set; } = new();
        
        public StatusResponsePacket()
        {
            
        }

        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(JsonSerializer.Serialize(Status, Global.JsonSerializerOptions) ?? throw new InvalidDataException());
        }
        void IPacket.Deserialize(Stream stream)
        {
            StatusResponsePacket packet = new();
            Status = JsonSerializer.Deserialize<Data.Structs.Status>(stream.ReadT16AS32V(), Global.JsonSerializerOptions) ?? throw new SerializationException();
        }
    }
}