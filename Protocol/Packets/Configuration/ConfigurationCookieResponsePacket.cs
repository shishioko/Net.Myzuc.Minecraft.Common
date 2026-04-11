using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed record ConfigurationCookieResponsePacket : IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        static int IPacket.PacketId => 0x01;

        public Identifier Id { get; set; } = new();
        public Memory<byte>? Data { get; set; } = null;

        public ConfigurationCookieResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Id);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue)
            {
                stream.WriteU8AS32V(Data.Value.Span);
            }
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            ConfigurationCookieResponsePacket packet = new();
            packet.Id = stream.Read<Identifier>();
            if(stream.ReadBool())
            {
                packet.Data = stream.ReadU8AS32V().ToArray().AsMemory();
            }
            return packet;
        }
    }
}