using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginStartPacket : Packet
    {
        public override bool Serverbound => true;
        protected internal override int PacketId => 0x00;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;

        public string Name = string.Empty;
        public Guid Guid = Guid.Empty;

        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Name);
            stream.WriteGuid(Guid);
        }
        internal override void Deserialize(Stream stream)
        {
            Name = stream.ReadT16AS32V();
            Guid = stream.ReadGuid();
        }
    }
}