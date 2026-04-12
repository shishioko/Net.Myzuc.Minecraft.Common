using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound
{
    public sealed record LoginSuccessPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x02;

        public ResolvedProfile Profile { get; set; } = new();

        public LoginSuccessPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Profile);
        }
        void IPacket.Deserialize(Stream stream)
        {
            LoginSuccessPacket packet = new();
            Profile = stream.Read<ResolvedProfile>();
        }
    }
}