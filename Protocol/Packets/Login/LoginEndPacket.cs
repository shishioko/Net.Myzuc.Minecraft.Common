namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginEndPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        protected internal override int PacketId => 0x03;

        internal override void Serialize(Stream stream)
        {
            
        }
        internal override void Deserialize(Stream stream)
        {
            
        }
    }
}