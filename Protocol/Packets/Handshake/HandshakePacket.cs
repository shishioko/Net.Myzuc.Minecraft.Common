using System.IO;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Handshake
{
    public sealed class HandshakePacket : Packet
    {
        public enum IntentEnum : int
        {
            Status = 1,
            Login = 2,
            Transfer = 3,
        }

        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Handshake;
        protected internal override int PacketId => 0x00;
        
        public int ProtocolVersion = 0;
        public string Address = "";
        public ushort Port = 0;
        public IntentEnum Intent = IntentEnum.Status;
        
        internal override void Serialize(Stream stream)
        {
            stream.WriteS32V(ProtocolVersion);
            stream.WriteMinecraftString(Address);
            stream.WriteU16(Port);
            stream.WriteS32V((int)Intent);
        }
        internal override void Deserialize(Stream stream)
        {
            ProtocolVersion = stream.ReadS32V();
            Address = stream.ReadMinecraftString();
            Port = stream.ReadU16();
            Intent = (IntentEnum)stream.ReadS32V();
        }
    }
}