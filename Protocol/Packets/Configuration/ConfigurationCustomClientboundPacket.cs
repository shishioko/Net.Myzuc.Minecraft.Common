using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomClientboundPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x01;

        public Identifier Channel { get; init; } = new();
        public byte[] Data { get; init; } = [];

        public ConfigurationCustomClientboundPacket()
        {
            
        }

        internal ConfigurationCustomClientboundPacket(Stream stream) : base(stream)
        {
            Channel = new(stream);
            Data = stream.ReadU8A();
        }
        
        internal override void Serialize(Stream stream)
        {
            Channel.Serialize(stream);
            stream.WriteU8A(Data);
        }
    }
}