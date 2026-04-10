using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public Identifier Id { get; init; } = new();
        public ReadOnlyMemory<byte>? Data { get; init; } = null;

        public LoginCookieResponsePacket()
        {
            
        }

        internal LoginCookieResponsePacket(Stream stream) : base(stream)
        {
            Id = new(stream);
            if(stream.ReadBool())
            {
                Data = stream.ReadU8AS32V().ToArray().AsMemory();
            }
        }
        
        internal override void Serialize(Stream stream)
        {
            Id.Serialize(stream);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue)
            {
                stream.WriteU8AS32V(Data.Value.Span);
            }
        }
    }
}