using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public static class Nbt
    {
        public static void Serialize(NbtTag? nbt, Stream stream)
        {
            stream.WriteS8((sbyte)(nbt?.ValueKind ?? NbtValueKind.End));
            nbt?.SerializeValue(stream);
        }
        public static NbtTag? Deserialize(Stream stream)
        {
            NbtValueKind valueKind = (NbtValueKind)stream.ReadS8();
            return NbtTag.DeserializeValue(stream, valueKind);
        }

        internal static NbtTag ToNbt<TNbtSerializable>(TNbtSerializable data) where TNbtSerializable : INbtSerializable<TNbtSerializable>
        {
            return data.ToNbt();
        }
        internal static NbtTag? ToNullableNbt<TNbtSerializable>(TNbtSerializable? data) where TNbtSerializable : INbtSerializable<TNbtSerializable>
        {
            if (data is null) return null;
            return data.ToNbt();
        }
        internal static TNbtSerializable FromNbt<TNbtSerializable>(NbtTag nbt) where TNbtSerializable : INbtSerializable<TNbtSerializable>
        {
            return TNbtSerializable.FromNbt(nbt);
        }
        internal static TNbtSerializable? FromNullableNbt<TNbtSerializable>(NbtTag? nbt) where TNbtSerializable : INbtSerializable<TNbtSerializable>
        {
            if (nbt is null) return default;
            return TNbtSerializable.FromNbt(nbt);
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