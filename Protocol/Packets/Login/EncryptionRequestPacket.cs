using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record EncryptionRequestPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x01;

        public string ServerId { get; set; } = string.Empty;
        public Memory<byte> PublicKey { get; set; } = new();
        public Memory<byte> DecryptedSample { get; set; } = new();
        public bool Authenticate { get; set; } = false;

        public EncryptionRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(ServerId);
            stream.WriteU8AS32V(PublicKey.Span);
            stream.WriteU8AS32V(DecryptedSample.Span);
            stream.WriteBool(Authenticate);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            EncryptionRequestPacket packet = new();
            packet.ServerId = stream.ReadT16AS32V();
            packet.PublicKey = stream.ReadU8AS32V().ToArray().AsMemory();
            packet.DecryptedSample = stream.ReadU8AS32V().ToArray().AsMemory();
            packet.Authenticate = stream.ReadBool();
            return packet;
        }
    }
}