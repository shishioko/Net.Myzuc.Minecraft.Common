using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound
{
    public sealed class StartPacket : IPacket
    {
        public bool Serverbound => true;
        int IPacket.PacketId => 0x00;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;

        public string Name { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.Empty;

        public StartPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Name);
            stream.WriteGuid(Guid);
        }
        void IPacket.Deserialize(Stream stream)
        {
            StartPacket packet = new();
            Name = stream.ReadT16AS32V();
            Guid = stream.ReadGuid();
        }
    }
}