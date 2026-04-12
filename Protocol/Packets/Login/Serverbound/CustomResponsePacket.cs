
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Login.Serverbound
{
    public sealed class CustomResponsePacket: IPacket
    {
        public bool Serverbound => true;
        public ProtocolStage ProtocolStage => ProtocolStage.Login;
        int IPacket.PacketId => 0x02;

        public int Id { get; set; } = 0;
        public Memory<byte>? Data { get; set; } = null;

        public CustomResponsePacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.WriteS32V(Id);
            stream.WriteBool(Data.HasValue);
            if (Data.HasValue) stream.WriteU8A(Data.Value.Span);
        }
        void IPacket.Deserialize(Stream stream)
        {
            CustomResponsePacket packet = new();
            Id = stream.ReadS32V();
            if (stream.ReadBool())
            {
                Data = stream.ReadU8A().ToArray().AsMemory();
            }
        }
    }
}