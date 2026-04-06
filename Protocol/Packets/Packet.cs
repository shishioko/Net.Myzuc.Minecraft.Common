using System.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets
{
    public abstract class Packet
    {
        public abstract bool Serverbound { get; }
        public abstract ProtocolStage ProtocolStage { get; }
        public abstract int Id { get; }
        
        public abstract void Deserialize(Stream stream);
        public abstract void Serialize(Stream stream);

    }
}