using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record EncryptionRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x01;

        public string ServerId { get; set; } = string.Empty;
        public Memory<byte> PublicKey { get; set; } = new();
        public Memory<byte> DecryptedSample { get; set; } = new();
        public bool Authenticate { get; set; } = false;

        public EncryptionRequestPacket()
        {
            
        }

        internal EncryptionRequestPacket(Stream stream) : base(stream)
        {
            ServerId = stream.ReadT16AS32V();
            PublicKey = stream.ReadU8AS32V().ToArray().AsMemory();
            DecryptedSample = stream.ReadU8AS32V().ToArray().AsMemory();
            Authenticate = stream.ReadBool();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(ServerId);
            stream.WriteU8AS32V(PublicKey.Span);
            stream.WriteU8AS32V(DecryptedSample.Span);
            stream.WriteBool(Authenticate);
        }
    }
}