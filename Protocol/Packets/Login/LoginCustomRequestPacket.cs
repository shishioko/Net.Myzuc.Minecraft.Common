using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public int Id { get; init; } = 0;
        public string Channel { get; init; } = string.Empty;
        public byte[] Data { get; init; } = [];

        public LoginCustomRequestPacket(Stream stream) : base(stream)
        {
            Id = stream.ReadS32V();
            Channel = stream.ReadT16AS32V();
            Data = stream.ReadU8A();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteT16AS32V(Channel);
            stream.WriteU8A(Data);
        }
    }
}