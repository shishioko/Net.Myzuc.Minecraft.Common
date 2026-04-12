using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Objects.GameEvents;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Play.Clientbound
{
    public class GameEventPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Play;
        int IPacket.PacketId => 0x26;
        
        public GameEvent GameEvent { get; set; } = new FailedRespawnGameEvent();

        public GameEventPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(GameEvent);
        }
        void IPacket.Deserialize(Stream stream)
        {
            GameEvent = stream.Read<GameEvent>();
        }
    }
}