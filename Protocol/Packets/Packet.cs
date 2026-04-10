namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public abstract record Packet
    {
        public abstract bool Serverbound { get; }
        public abstract ProtocolStage ProtocolStage { get; }
        protected internal abstract int PacketId { get; }

        internal Packet(Stream stream)
        {
            
        }
        
        internal abstract void Serialize(Stream stream);
    }
}