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
            stream.WriteMinecraftString(Name);
            stream.WriteGuid(Guid);
        }
        internal override void Deserialize(Stream stream)
        {
            Name = stream.ReadMinecraftString();
            Guid = stream.ReadGuid();
        }
    }
}