namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed class LoginAckPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Login;
        public override int Id => 0x03;

        public override void Serialize(Stream stream)
        {
            
        }
        public override void Deserialize(Stream stream)
        {
            
        }
    }
}