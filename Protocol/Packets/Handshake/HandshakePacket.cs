using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake
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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            HandshakePacket packet = new();
            packet.ProtocolVersion = stream.ReadS32V();
            packet.Address = stream.ReadT16AS32V();
            packet.Port = stream.ReadU16();
            packet.Intent = (HandshakeIntent)stream.ReadS32V();
            return packet;
        }
    }
}