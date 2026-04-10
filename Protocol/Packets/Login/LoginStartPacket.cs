using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginStartPacket : Packet
    {
        public override bool Serverbound => true;
        protected internal override int PacketId => 0x00;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;

        public string Name { get; set; } = string.Empty;
        public Guid Guid { get; set; } = Guid.Empty;

        public LoginStartPacket()
        {
            
        }

        internal LoginStartPacket(Stream stream) : base(stream)
        {
            Name = stream.ReadT16AS32V();
            Guid = stream.ReadGuid();
        }
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Name);
            stream.WriteGuid(Guid);
        }
    }
}