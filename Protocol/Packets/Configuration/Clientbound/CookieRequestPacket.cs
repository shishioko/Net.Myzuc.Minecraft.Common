using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound
{
    public sealed class CookieRequestPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x00;

        public Identifier Id { get; set; } = new();

        public CookieRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Id);
        }
        void IPacket.Deserialize(Stream stream)
        {
            CookieRequestPacket packet = new();
            Id = stream.Read<Identifier>();
        }
    }
}