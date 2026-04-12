using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomServerboundPacket: IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x02;

        public Identifier Channel { get; set; } = new();
        public Memory<byte> Data { get; set; } = new();

        public ConfigurationCustomServerboundPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Channel);
            stream.WriteU8A(Data.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            ConfigurationCustomServerboundPacket packet = new();
            Channel = stream.Read<Identifier>();
            Data = stream.ReadU8A().ToArray().AsMemory();
        }
    }
}