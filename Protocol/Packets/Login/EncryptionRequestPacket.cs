using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class EncryptionRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x01;

        public string ServerId = string.Empty;
        public byte[] PublicKey = [];
        public byte[] DecryptedSample = [];
        public bool Authenticate = false;

        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(ServerId);
            stream.WriteU8AS32V(PublicKey);
            stream.WriteU8AS32V(DecryptedSample);
            stream.WriteBool(Authenticate);
        }
        internal override void Deserialize(Stream stream)
        {
            ServerId = stream.ReadT16AS32V();
            PublicKey = stream.ReadU8AS32V();
            DecryptedSample = stream.ReadU8AS32V();
            Authenticate = stream.ReadBool();
        }
    }
}