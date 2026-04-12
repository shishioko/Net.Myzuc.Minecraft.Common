using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound
{
    public sealed record ConfigurationCookieRequestPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x00;

        public Identifier Id { get; set; } = new();

        public ConfigurationCookieRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Id);
        }
        void IPacket.Deserialize(Stream stream)
        {
            ConfigurationCookieRequestPacket packet = new();
            Id = stream.Read<Identifier>();
        }
    }
}