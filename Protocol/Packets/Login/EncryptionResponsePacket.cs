using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class EncryptionResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x01;

        public byte[] EncryptedSecret = [];
        public byte[] EncryptedSample = [];

        public override void Serialize(Stream stream)
        {
            stream.WriteS32V(EncryptedSecret.Length);
            stream.WriteU8A(EncryptedSecret);
            stream.WriteS32V(EncryptedSample.Length);
            stream.WriteU8A(EncryptedSample);
        }
        public override void Deserialize(Stream stream)
        {
            EncryptedSecret = stream.ReadU8A(stream.ReadS32V());
            EncryptedSample = stream.ReadU8A(stream.ReadS32V());
        }
    }
}