using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomServerboundPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x02;

        public Identifier Channel { get; init; } = new();
        public byte[] Data { get; init; } = [];

        public ConfigurationCustomServerboundPacket()
        {
            
        }

        internal ConfigurationCustomServerboundPacket(Stream stream) : base(stream)
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