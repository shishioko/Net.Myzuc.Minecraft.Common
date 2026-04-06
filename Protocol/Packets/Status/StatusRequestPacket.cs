namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed class StatusRequestPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x00;
        
        public override void Serialize(Stream stream)
        {
            
        }
        public override void Deserialize(Stream stream)
        {
            
        }
    }
}