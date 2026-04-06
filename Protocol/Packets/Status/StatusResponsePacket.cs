using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed class StatusResponsePacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        public override int Id => 0x00;

        public Objects.Status Status = new();
        
        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftJson(Status);
        }
        public override void Deserialize(Stream stream)
        {
            Status = stream.ReadMinecraftJson<Objects.Status>();
        }
    }
}