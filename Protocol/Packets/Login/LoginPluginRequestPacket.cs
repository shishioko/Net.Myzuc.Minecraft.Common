using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

public sealed class LoginPluginRequestPacket: Packet
{
    public override bool Serverbound => false;
    public override ProtocolStage ProtocolStage => ProtocolStage.Login;
    public override int Id => 0x04;

    public int MessageID;
    public String Channel;
    
    // Additional data, depending on the actual channel

    public override void Serialize(Stream stream)
    {
        stream.WriteS32V(MessageID);
        stream.WriteMinecraftString(Channel);
    }

    public override void Deserialize(Stream stream)
    {
        MessageID = stream.ReadS32V();
        Channel = stream.ReadMinecraftString();
    }
}