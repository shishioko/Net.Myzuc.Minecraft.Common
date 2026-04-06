using System.Runtime.Serialization;
using Me.Shiokawaii.IO;
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
        public byte[] VerifyToken = [];
        public bool Authenticate = false;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(ServerId);
            stream.WriteS32V(PublicKey.Length);
            stream.WriteU8A(PublicKey);
            stream.WriteS32V(VerifyToken.Length);
            stream.WriteU8A(VerifyToken);
            stream.WriteBool(Authenticate);
        }
        public override void Deserialize(Stream stream)
        {
            ServerId = stream.ReadMinecraftString();
            PublicKey = stream.ReadU8A(stream.ReadS32V());
            VerifyToken = stream.ReadU8A(stream.ReadS32V());
            Authenticate = stream.ReadBool();
        }
    }
}