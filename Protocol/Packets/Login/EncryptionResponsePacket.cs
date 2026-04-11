
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record EncryptionResponsePacket : IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;
        static int IPacket.PacketId => 0x01;

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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            EncryptionResponsePacket packet = new();
            packet.EncryptedSecret = stream.ReadU8AS32V().ToArray().AsMemory();
            packet.EncryptedSample = stream.ReadU8AS32V().ToArray().AsMemory();
            return packet;
        }
    }
}