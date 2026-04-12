using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public sealed class UnknownPacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Handshake;
        int IPacket.PacketId => int.MinValue;

        public int Id { get; set; } = 0x00;
        public Memory<byte> Content { get; set; } = new();

        public UnknownPacket()
        {
            
        }
        public UnknownPacket(int id)
        {
            Id = id;
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteU8A(Content.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            UnknownPacket packet = new();
            Content = stream.ReadU8A().ToArray().AsMemory();
        }
    }
}