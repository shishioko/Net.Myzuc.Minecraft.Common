using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCookieResponsePacket : IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x04;

        public Identifier Id { get; set; } = new();
        public Memory<byte>? Data { get; set; } = null;

        public LoginCookieResponsePacket()
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
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginCookieResponsePacket packet = new();
            packet.Id = stream.Read<Identifier>();
            if(stream.ReadBool())
            {
                packet.Data = stream.ReadU8AS32V().ToArray().AsMemory();
            }
            return packet;
        }
    }
}