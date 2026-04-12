using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound
{
    public sealed class CookieResponsePacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x04;

        public Identifier Id { get; set; } = new();
        public Memory<byte>? Data { get; set; } = null;

        public CookieResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Id);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue)
            {
                stream.WriteU8AS32V(Data.Value.Span);
            }
        }
        void IPacket.Deserialize(Stream stream)
        {
            CookieResponsePacket packet = new();
            Id = stream.Read<Identifier>();
            if(stream.ReadBool())
            {
                Data = stream.ReadU8AS32V().ToArray().AsMemory();
            }
        }
    }
}