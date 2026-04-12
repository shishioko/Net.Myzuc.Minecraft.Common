using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomClientboundPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x01;

        public Identifier Channel { get; set; } = new();
        public Memory<byte> Data { get; set; } = new();

        public ConfigurationCustomClientboundPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Channel);
            stream.WriteU8A(Data.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            ConfigurationCustomClientboundPacket packet = new();
            Channel = stream.Read<Identifier>();
            Data = stream.ReadU8A().ToArray().AsMemory();
        }
    }
}