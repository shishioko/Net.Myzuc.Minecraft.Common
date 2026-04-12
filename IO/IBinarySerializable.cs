namespace Net.Myzuc.Minecraft.Common.IO
{
    public interface IBinarySerializable<TSelf>
    {
        internal abstract void Serialize(Stream stream);
        internal static abstract TSelf Deserialize(Stream stream);
    }
}