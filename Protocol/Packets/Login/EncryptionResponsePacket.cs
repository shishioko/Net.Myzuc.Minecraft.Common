
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record EncryptionResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x01;

        public Memory<byte> EncryptedSecret { get; set; } = new();
        public Memory<byte> EncryptedSample { get; set; } = new();

        public EncryptionResponsePacket()
        {
            
        }

        internal EncryptionResponsePacket(Stream stream) : base(stream)
        {
            EncryptedSecret = stream.ReadU8AS32V().ToArray().AsMemory();
            EncryptedSample = stream.ReadU8AS32V().ToArray().AsMemory();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteU8AS32V(EncryptedSecret.Span);
            stream.WriteU8AS32V(EncryptedSample.Span);
        }
    }
}