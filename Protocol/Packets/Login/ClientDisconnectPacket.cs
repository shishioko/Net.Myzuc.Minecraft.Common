using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class ClientDisconnectPacket : Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x00;

        public string Message = "{}";

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(Message); // Not sure if components are strings, I forgor
        }

        public override void Deserialize(Stream stream)
        {
            Message = stream.ReadMinecraftString();
        }
    }
}