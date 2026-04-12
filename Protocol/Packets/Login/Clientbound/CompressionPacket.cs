
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound
{
    public sealed class CompressionPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x03;

        public int Threshold { get; set; } = 0;

        public CompressionPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(Threshold);
        }
        void IPacket.Deserialize(Stream stream)
        {
            CompressionPacket packet = new();
            Threshold = stream.ReadS32V();
        }
    }
}