using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class EncryptionResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x01;

        public byte[] SharedSecret = [];
        public byte[] VerifyToken = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(SharedSecret.Length);
            stream.WriteU8A(SharedSecret);
            stream.WriteS32V(VerifyToken.Length);
            stream.WriteU8A(VerifyToken);
        }
        public override void Deserialize(Stream stream)
        {
            SharedSecret = stream.ReadU8A(stream.ReadS32V());
            VerifyToken = stream.ReadU8A(stream.ReadS32V());
        }
    }
}