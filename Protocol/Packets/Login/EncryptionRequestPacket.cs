using System.Runtime.Serialization;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login;

public sealed class EncryptionRequestPacket: Packet
{
    public override bool Serverbound => false;
    public override ProtocolStage ProtocolStage => ProtocolStage.Login;
    public override int Id => 0x01;

    public String server_id = "";
    
    public byte[] public_key = new byte[] {};
    public byte[] verify_token = new byte[] {};
    public bool should_auth = false;

    public override void Serialize(Stream stream)
    {
        stream.WriteMinecraftString(server_id);

        stream.WriteS32V(public_key.Length);
        stream.WriteU8A(public_key);

        stream.WriteS32V(verify_token.Length);
        stream.WriteU8A(verify_token);
        
        stream.WriteBool(should_auth);
    }

    public override void Deserialize(Stream stream)
    {
        server_id = stream.ReadMinecraftString();
        if (server_id.Length > 20) throw new SerializationException("server_id > 20");

        public_key = stream.ReadU8A(stream.ReadS32V());
        verify_token = stream.ReadU8A(stream.ReadS32V());

        should_auth = stream.ReadBool();
    }
}