using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public sealed class PingRequestPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        public override int Id => 0x01;

        public long Data = 0;
        
        public override void Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
        public override void Deserialize(Stream stream)
        {
            Data = stream.ReadS64();
        }
    }
}