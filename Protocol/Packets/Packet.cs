namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public abstract class Packet
    {
        public abstract bool Serverbound { get; }
        public abstract ProtocolStage ProtocolStage { get; }
        protected internal abstract int PacketId { get; }
        
        internal abstract void Deserialize(Stream stream);
        internal abstract void Serialize(Stream stream);

    }
}