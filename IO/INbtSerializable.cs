using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal interface INbtSerializable<TSelf>
    {
        internal abstract NbtTag ToNbt();
        internal static abstract TSelf FromNbt(NbtTag nbt);
    }
}