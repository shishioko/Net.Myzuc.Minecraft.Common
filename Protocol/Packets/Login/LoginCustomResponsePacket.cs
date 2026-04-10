
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomResponsePacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x02;

        public int Id { get; set; } = 0;
        public Memory<byte>? Data { get; set; } = null;

        public LoginCustomResponsePacket()
        {
            
        }

        internal LoginCustomResponsePacket(Stream stream) : base(stream)
        {
            Id = stream.ReadS32V();
            if (stream.ReadBool())
            {
                Data = stream.ReadU8A().ToArray().AsMemory();
            }
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue) stream.WriteU8A(Data.Value.Span);
        }
    }
}