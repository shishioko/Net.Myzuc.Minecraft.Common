using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration
{
    public sealed class ConfigurationCustomServerboundPacket: Packet
    {
        public override bool Serverbound => true;
        public override ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        protected internal override int PacketId => 0x02;

        public string Channel = string.Empty;
        public byte[] Data = [];

        internal override void Serialize(Stream stream)
        {
            stream.WriteT16AS32V(Channel);
            stream.WriteU8A(Data);
        }
        internal override void Deserialize(Stream stream)
        {
            Channel = stream.ReadT16AS32V();
            Data = stream.ReadU8A();
        }
    }
}