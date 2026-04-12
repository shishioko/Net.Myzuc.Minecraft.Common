using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Clientbound
{
    public sealed record LoginCustomRequestPacket: IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x04;

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
        void IPacket.Deserialize(Stream stream)
        {
            LoginCustomRequestPacket packet = new();
            Id = stream.ReadS32V();
            Channel = stream.Read<Identifier>();
            Data = stream.ReadU8A().ToArray().AsMemory();
        }
    }
}