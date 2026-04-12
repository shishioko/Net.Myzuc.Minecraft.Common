using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound
{
    public sealed record LoginCookieRequestPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x05;

        public Identifier Id { get; set; } = new();

        public LoginCookieRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Id);
        }
        void IPacket.Deserialize(Stream stream)
        {
            LoginCookieRequestPacket packet = new();
            Id = stream.Read<Identifier>();
        }
    }
}