using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Registries
{
    public interface IRegistryEntry
    {
        internal static abstract Identifier RegistryId { get; }
    }
}