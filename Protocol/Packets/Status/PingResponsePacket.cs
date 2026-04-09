
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed class PingResponsePacket : Packet
    {
        public override bool Serverbound => false;
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