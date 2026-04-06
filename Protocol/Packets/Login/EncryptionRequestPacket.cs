using System.Runtime.Serialization;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class EncryptionRequestPacket: Packet
    {
        public override bool Serverbound => false;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x01;

        public string ServerID = string.Empty;
    
        public byte[] PublicKey = [];
        public byte[] VerifyToken = [];
        public bool ShouldAuth = false;

        public override void Serialize(Stream stream)
        {
            stream.WriteMinecraftString(ServerID);

            stream.WriteS32V(PublicKey.Length);
            stream.WriteU8A(PublicKey);

            stream.WriteS32V(VerifyToken.Length);
            stream.WriteU8A(VerifyToken);
        
            stream.WriteBool(ShouldAuth);
        }

        public override void Deserialize(Stream stream)
        {
            ServerID = stream.ReadMinecraftString();
            if (ServerID.Length > 20) throw new SerializationException("server_id > 20");

            PublicKey = stream.ReadU8A(stream.ReadS32V());
            VerifyToken = stream.ReadU8A(stream.ReadS32V());

            ShouldAuth = stream.ReadBool();
        }
    }
}