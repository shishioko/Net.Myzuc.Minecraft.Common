using System.Text.Json;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed class StatusResponsePacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x00;

        public Data.Status Status = new();
        internal override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Status.ToString());
        }
        internal override void Deserialize(Stream stream)
        {
            Status = Data.Status.Parse(stream.ReadMinecraftString());
        }
    }
}