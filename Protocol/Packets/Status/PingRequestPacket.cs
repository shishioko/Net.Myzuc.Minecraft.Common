using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed class PingRequestPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x01;

        public long Data = 0;
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        internal override void Deserialize(Stream stream)
        {
            Data = stream.ReadS64();
        }
    }
}