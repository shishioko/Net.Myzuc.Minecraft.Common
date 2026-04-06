using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class CookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public string Id = string.Empty;
        public byte[]? Payload = null;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Id);
            stream.WriteBool(Payload is not null);
            if (Payload is not null)
            {
                stream.WriteS32V(Payload.Length);
                stream.WriteU8A(Payload);
            }
        }
        public override void Deserialize(Stream stream)
        {
            Id = stream.ReadMinecraftString();
            if(stream.ReadBool())
            {
                Payload = stream.ReadU8A(stream.ReadS32V());
            }
        }
    }
}