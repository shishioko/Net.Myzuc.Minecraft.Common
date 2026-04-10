
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public abstract record NbtTag
    {
        protected internal abstract NbtValueKind ValueKind { get; }
        internal NbtTag()
        {
            
        }
        public abstract TNbtTag Get<TNbtTag>() where TNbtTag : NbtTag;
        public abstract TNbtTag As<TNbtTag>() where TNbtTag : NbtTag;
        public abstract NbtTag Copy();
        public abstract NbtTag Merge(NbtTag nbt);
        internal abstract void SerializeValue(Stream stream);
        public abstract override string ToString();
        internal static NbtTag? DeserializeValue(Stream stream, NbtValueKind valueKind)
        {
            return valueKind switch
            {
                NbtValueKind.End => throw new InvalidOperationException(),
                NbtValueKind.Byte => ByteNbtTag.DeserializeValue(stream),
                NbtValueKind.Short => ShortNbtTag.DeserializeValue(stream),
                NbtValueKind.Int => IntNbtTag.DeserializeValue(stream),
                NbtValueKind.Long => LongNbtTag.DeserializeValue(stream),
                NbtValueKind.Float => FloatNbtTag.DeserializeValue(stream),
                NbtValueKind.Double => DoubleNbtTag.DeserializeValue(stream),
                NbtValueKind.ByteArray => ByteArrayNbtTag.DeserializeValue(stream),
                NbtValueKind.String => StringNbtTag.DeserializeValue(stream),
                NbtValueKind.List => ListNbtTag.DeserializeValue(stream),
                NbtValueKind.Compound => CompoundNbtTag.DeserializeValue(stream),
                NbtValueKind.IntArray => IntArrayNbtTag.DeserializeValue(stream),
                NbtValueKind.LongArray => LongArrayNbtTag.DeserializeValue(stream),
                _ => null,
            };
        }
        public static NbtTag Parse(StringReader reader)
        {
            throw new NotImplementedException();
        }
    }
}