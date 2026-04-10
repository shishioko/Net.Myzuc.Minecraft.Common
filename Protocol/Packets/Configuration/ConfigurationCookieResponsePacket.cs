using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x01;

        public Identifier Id { get; init; } = new();
        public byte[]? Data { get; init; } = null;

        public ConfigurationCookieResponsePacket()
        {
            
        }

        internal ConfigurationCookieResponsePacket(Stream stream) : base(stream)
        {
            Id = new(stream);
            if(stream.ReadBool())
            {
                Data = stream.ReadU8AS32V();
            }
        }
        
        internal override void Serialize(Stream stream)
        {
            Id.Serialize(stream);
            stream.WriteBool(Data is not null);
            if (Data is not null)
            {
                stream.WriteU8AS32V(Data);
            }
        }
    }
}