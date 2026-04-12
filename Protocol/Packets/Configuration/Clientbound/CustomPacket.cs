using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound
{
    public sealed class CustomPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x01;

        public Identifier Channel { get; set; } = new();
        public Memory<byte> Data { get; set; } = new();

        public CustomPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Channel);
            stream.WriteU8A(Data.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            CustomPacket packet = new();
            Channel = stream.Read<Identifier>();
            Data = stream.ReadU8A().ToArray().AsMemory();
        }
    }
}