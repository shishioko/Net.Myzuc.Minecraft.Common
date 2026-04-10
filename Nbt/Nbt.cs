using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public static class Nbt
    {
        public static NbtTag? Deserialize(Stream stream)
        {
            NbtValueKind valueKind = (NbtValueKind)stream.ReadS8();
            return NbtTag.DeserializeValue(stream, valueKind);
        }
        public static NbtTag FromSNbt(string data)
        {
            throw new NotImplementedException();
        }
        public static string ToSNbt(NbtTag data)
        {
            return data.ToString();
        }
    }
}