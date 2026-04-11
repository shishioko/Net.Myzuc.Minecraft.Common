namespace Net.Myzuc.Minecraft.Common.IO
{
    internal interface IBinarySerializable<TSelf>
    {
        internal static abstract void Serialize(TSelf data, Stream stream);
        internal static abstract TSelf Deserialize(Stream stream);
    }
}