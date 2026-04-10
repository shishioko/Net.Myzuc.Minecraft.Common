using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public int Id { get; init; } = 0;
        public Identifier Channel { get; init; } = new();
        public ReadOnlyMemory<byte> Data { get; init; } = new();

        public LoginCustomRequestPacket()
        {
            
        }

        internal LoginCustomRequestPacket(Stream stream) : base(stream)
        {
            Id = stream.ReadS32V();
            Channel = new(stream);
            Data = stream.ReadU8A().ToArray().AsMemory();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            Channel.Serialize(stream);
            stream.WriteU8A(Data.Span);
        }
    }
}