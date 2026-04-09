using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginCookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x04;

        public string Id = string.Empty;
        public byte[]? Data = null;

        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Id);
            stream.WriteBool(Data is not null);
            if (Data is not null)
            {
                stream.WriteS32V(Data.Length);
                stream.WriteU8A(Data);
            }
        }
        internal override void Deserialize(Stream stream)
        {
            Id = stream.ReadT16AS32V();
            if(stream.ReadBool())
            {
                Data = stream.ReadU8A(stream.ReadS32V());
            }
        }
    }
}