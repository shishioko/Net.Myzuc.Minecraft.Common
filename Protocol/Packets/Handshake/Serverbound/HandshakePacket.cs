using Net.Myzuc.Minecraft.Common.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake.Serverbound
{
    public sealed record HandshakePacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Handshake;
        int IPacket.PacketId => 0x00;
        
        public int ProtocolVersion { get; set; } = 0;
        public string Address { get; set; } = "";
        public ushort Port { get; set; } = 0;
        public HandshakeIntent Intent { get; set; } = HandshakeIntent.Status;

        public HandshakePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(ProtocolVersion);
            stream.WriteT16AS32V(Address);
            stream.WriteU16(Port);
            stream.WriteS32V((int)Intent);
        }
        void IPacket.Deserialize(Stream stream)
        {
            HandshakePacket packet = new();
            ProtocolVersion = stream.ReadS32V();
            Address = stream.ReadT16AS32V();
            Port = stream.ReadU16();
            Intent = (HandshakeIntent)stream.ReadS32V();
        }
    }
}