using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Protocol.Packets.Configuration.Clientbound
{
    public sealed record RegistryPacket : IPacket
    {
        public bool Serverbound => false;
        public ProtocolStage ProtocolStage => ProtocolStage.Configuration;
        int IPacket.PacketId => 0x07;

        public IRegistry Registry { get; set; } = new Registry<NbtTag>();

        public RegistryPacket()
        {
            
        }
        
        void IPacket.Serialize(Stream stream)
        {
            stream.Write(Registry.Encode());
        }
        void IPacket.Deserialize(Stream stream)
        {
            RegistryPacket packet = new();
            Registry = Registries.Registry.Decode(stream.Read<Registry<NbtTag>>());
        }
    }
}