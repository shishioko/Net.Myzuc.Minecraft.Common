using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomClientboundPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x01;

        public string Channel { get; init; } = string.Empty;
        public byte[] Data { get; init; } = [];

        public ConfigurationCustomClientboundPacket(Stream stream) : base(stream)
        {
            Channel = stream.ReadT16AS32V();
            Data = stream.ReadU8A();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Channel);
            stream.WriteU8A(Data);
        }
    }
}