namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status.Serverbound
{
    public sealed class StatusRequestPacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Status;
        int IPacket.PacketId => 0x00;
        
        public StatusRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            
        }
        void IPacket.Deserialize(Stream stream)
        {
            
        }
    }
}