namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public interface IPacket
    {
        public abstract bool Serverbound { get; }
        public abstract ProtocolStage ProtocolStage { get; }
        internal abstract int PacketId { get; }

        internal abstract void Serialize(Stream stream);
        internal abstract void Deserialize(Stream stream);
    }
}