
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCompressionPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x03;

        public int Threshold { get; set; } = 0;

        public LoginCompressionPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(Threshold);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginCompressionPacket packet = new();
            packet.Threshold = stream.ReadS32V();
            return packet;
        }
    }
}