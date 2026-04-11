using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCustomClientboundPacket: IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        static int IPacket.PacketId => 0x01;

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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            ConfigurationCustomClientboundPacket packet = new();
            packet.Channel = stream.Read<Identifier>();
            packet.Data = stream.ReadU8A().ToArray().AsMemory();
            return packet;
        }
    }
}