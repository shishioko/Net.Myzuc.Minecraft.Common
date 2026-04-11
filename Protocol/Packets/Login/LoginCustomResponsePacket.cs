
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomResponsePacket: IPacket
    {
        public static bool Serverbound => true;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;
        static int IPacket.PacketId => 0x02;

        public int Id { get; set; } = 0;
        public Memory<byte>? Data { get; set; } = null;

        public LoginCustomResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue) stream.WriteU8A(Data.Value.Span);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginCustomResponsePacket packet = new();
            packet.Id = stream.ReadS32V();
            if (stream.ReadBool())
            {
                packet.Data = stream.ReadU8A().ToArray().AsMemory();
            }
            return packet;
        }
    }
}