using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginStartPacket : IPacket
    {
        public static bool Serverbound => true;
        static int IPacket.PacketId => 0x00;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;

        public string Name { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.Empty;

        public LoginStartPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Name);
            stream.WriteGuid(Guid);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginStartPacket packet = new();
            packet.Name = stream.ReadT16AS32V();
            packet.Guid = stream.ReadGuid();
            return packet;
        }
    }
}