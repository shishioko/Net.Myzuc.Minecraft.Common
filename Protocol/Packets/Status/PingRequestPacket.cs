
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record PingRequestPacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Status;
        protected internal override int PacketId => 0x01;

        public long Data { get; init; } = 0;
        
        public PingRequestPacket()
        {
            
        }

        internal PingRequestPacket(Stream stream) : base(stream)
        {
            Data = stream.ReadS64();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS64(Data);
        }
    }
}