using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal interface INbtSerializable<TSelf>
    {
        internal static abstract NbtTag ToNbt(TSelf data);
        internal static abstract TSelf FromNbt(NbtTag nbt);
    }
}