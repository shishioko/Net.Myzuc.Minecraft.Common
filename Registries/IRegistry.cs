using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Registries
{
    public interface IRegistry
    {
        public abstract Identifier Id { get; }
        public static abstract IRegistry Decode(Registry<NbtTag> registry);
        public abstract Registry<NbtTag> Encode();
    }
}