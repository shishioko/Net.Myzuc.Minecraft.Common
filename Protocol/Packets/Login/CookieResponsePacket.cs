using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

public sealed class CookieResponsePacket : Packet
{
    public override bool Serverbound => true;
    public override ProtocolStage ProtocolStage => ProtocolStage.Login;
    public override int Id => 0x04;

    public String id = "";
    public byte[]? payload;

    public override void Serialize(Stream stream)
    {
        stream.WriteMinecraftString(id);
        
        stream.WriteBool(payload != null);

        if (payload != null)
        {
            stream.WriteS32V(payload.Length);
            stream.WriteU8A(payload);
        }
    }

    public override void Deserialize(Stream stream)
    {
        id = stream.ReadMinecraftString();
        
        if(stream.ReadBool())
        {
            payload = stream.ReadU8A(stream.ReadS32V());
        }
    }
}