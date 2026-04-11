namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Status
{
    public sealed record StatusRequestPacket : IPacket
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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            return new StatusRequestPacket();
        }
    }
}