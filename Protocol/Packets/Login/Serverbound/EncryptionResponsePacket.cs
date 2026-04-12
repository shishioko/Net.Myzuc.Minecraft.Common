
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound
{
    public sealed class EncryptionResponsePacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x01;

        public Memory<byte> EncryptedSecret { get; set; } = new();
        public Memory<byte> EncryptedSample { get; set; } = new();

        public EncryptionResponsePacket()
        {
            
        }

        internal EncryptionResponsePacket(Stream stream)
        {
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteU8AS32V(EncryptedSecret.Span);
            stream.WriteU8AS32V(EncryptedSample.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            EncryptionResponsePacket packet = new();
            EncryptedSecret = stream.ReadU8AS32V().ToArray().AsMemory();
            EncryptedSample = stream.ReadU8AS32V().ToArray().AsMemory();
        }
    }
}