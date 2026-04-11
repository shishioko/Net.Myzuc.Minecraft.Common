using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login
{
    public sealed record LoginCustomRequestPacket: IPacket
    {
        public static bool Serverbound => false;
        public static ProtocolStage ProtocolStage => ProtocolStage.Login;
        static int IPacket.PacketId => 0x04;

        public int Id { get; set; } = 0;
        public Identifier Channel { get; set; } = new();
        public Memory<byte> Data { get; set; } = new();

        public LoginCustomRequestPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.Write(Channel);
            stream.WriteU8A(Data.Span);
        }
        static IPacket IPacket.Deserialize(Stream stream)
        {
            LoginCustomRequestPacket packet = new();
            packet.Id = stream.ReadS32V();
            packet.Channel = stream.Read<Identifier>();
            packet.Data = stream.ReadU8A().ToArray().AsMemory();
            return packet;
        }
    }
}