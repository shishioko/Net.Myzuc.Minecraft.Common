using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Data.Structs;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginSuccessPacket packet = new();
            packet.Profile = stream.Read<ResolvedProfile>();
            return packet;
        }
    }
}