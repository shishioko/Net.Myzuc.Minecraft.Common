using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public string Id { get; init; } = string.Empty;
        public byte[]? Data { get; init; } = null;

        public LoginCookieResponsePacket()
        {
            
        }

        internal LoginCookieResponsePacket(Stream stream) : base(stream)
        {
            Id = stream.ReadT16AS32V();
            if(stream.ReadBool())
            {
                Data = stream.ReadU8AS32V();
            }
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Id);
            stream.WriteBool(Data is not null);
            if (Data is not null)
            {
                stream.WriteU8AS32V(Data);
            }
        }
    }
}