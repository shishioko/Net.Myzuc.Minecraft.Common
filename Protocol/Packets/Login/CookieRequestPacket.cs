using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

public sealed class CookieRequestPacket : Packet
{
    public override bool Serverbound => false;
    public override ProtocolStage ProtocolStage => ProtocolStage.Login;
    public override int Id => 0x05;

    public String Identifier;

    public override void Serialize(Stream stream)
    {
        stream.WriteMinecraftString(Identifier);
    }

    public override void Deserialize(Stream stream)
    {
        Identifier = stream.ReadMinecraftString();
    }
}