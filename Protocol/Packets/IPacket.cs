namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public interface IPacket
    {
        public static abstract bool Serverbound { get; }
        public static abstract ProtocolStage ProtocolStage { get; }
        internal static abstract int PacketId { get; }

        internal abstract void Serialize(Stream stream);
        internal static abstract IPacket Deserialize(Stream stream);
    }
}