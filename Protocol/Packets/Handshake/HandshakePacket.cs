using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Data.Enums;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake
{
    public sealed record HandshakePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Handshake;
        protected internal override int PacketId => 0x00;
        
        public int ProtocolVersion { get; set; } = 0;
        public string Address { get; set; } = "";
        public ushort Port { get; set; } = 0;
        public HandshakeIntent Intent { get; set; } = HandshakeIntent.Status;

        public HandshakePacket()
        {
            
        }
        
        internal HandshakePacket(Stream stream) : base(stream)
        {
            ProtocolVersion = stream.ReadS32V();
            Address = stream.ReadT16AS32V();
            Port = stream.ReadU16();
            Intent = (HandshakeIntent)stream.ReadS32V();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(ProtocolVersion);
            stream.WriteT16AS32V(Address);
            stream.WriteU16(Port);
            stream.WriteS32V((int)Intent);
        }
    }
}