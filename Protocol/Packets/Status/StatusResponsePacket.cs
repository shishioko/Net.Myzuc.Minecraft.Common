using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record StatusResponsePacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x00;

        public Data.Status Status { get; init; } = new();
        
        public StatusResponsePacket(Stream stream) : base(stream)
        {
            Status = Data.Status.Parse(stream.ReadT16AS32V());
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Status.ToString());
        }
    }
}