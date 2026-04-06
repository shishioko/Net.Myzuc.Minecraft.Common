using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

public sealed class EncryptionResponsePacket : Packet
{
    public override bool Serverbound => true;
    public override ProtocolStage ProtocolStage => ProtocolStage.Login;
    public override int Id => 0x01;

    public byte[] shared_secret = new byte[] { };
    public byte[] verify_token = new byte[] { };

    public override void Serialize(Stream stream)
    {
        stream.WriteS32V(shared_secret.Length);
        stream.WriteU8A(shared_secret);
        
        stream.WriteS32V(verify_token.Length);
        stream.WriteU8A(verify_token);
    }

    public override void Deserialize(Stream stream)
    {
        shared_secret = stream.ReadU8A(stream.ReadS32V());
        verify_token = stream.ReadU8A(stream.ReadS32V());
    }
}