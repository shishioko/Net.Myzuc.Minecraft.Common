using System.Runtime.Serialization;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public abstract record Packet
    {
        [IgnoreDataMember]
        public abstract bool Serverbound { get; }
        [IgnoreDataMember]
        public abstract ProtocolStage ProtocolStage { get; }
        [IgnoreDataMember]
        protected internal abstract int PacketId { get; }

        internal Packet()
        {
            
        }
        
        internal Packet(Stream stream)
        {
            
        }
        
        internal abstract void Serialize(Stream stream);
    }
}