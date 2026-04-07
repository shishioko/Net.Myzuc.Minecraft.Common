namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginEndPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x03;

        public override void Serialize(Stream stream)
        {
            
        }
        public override void Deserialize(Stream stream)
        {
            
        }
    }
}