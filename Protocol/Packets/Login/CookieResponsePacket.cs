using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class CookieResponsePacket : Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x04;

        public String CookieID = "";
        public byte[]? Payload;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(CookieID);
        
            stream.WriteBool(Payload != null);

            if (Payload != null)
            {
                stream.WriteS32V(Payload.Length);
                stream.WriteU8A(Payload);
            }
        }

        public override void Deserialize(Stream stream)
        {
            CookieID = stream.ReadMinecraftString();
        
            if(stream.ReadBool())
            {
                Payload = stream.ReadU8A(stream.ReadS32V());
            }
        }
    }
}