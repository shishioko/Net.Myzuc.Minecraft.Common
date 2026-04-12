namespace Net.Myzuc.Minecraft.Common.IO
{
    internal interface IBinarySerializable<TSelf>
    {
        internal abstract void Serialize(Stream stream);
        internal static abstract TSelf Deserialize(Stream stream);
    }
}