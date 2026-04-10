
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomResponsePacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x02;

        public int Id { get; init; } = 0;
        public byte[]? Data { get; init; } = null;

        public LoginCustomResponsePacket(Stream stream) : base(stream)
        {
            Id = stream.ReadS32V();
            if (stream.ReadBool())
            {
                Data = stream.ReadU8A();
            }
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteBool(Data is not null);
            if (Data is not null) stream.WriteU8A(Data);
        }
    }
}