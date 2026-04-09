using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public abstract record NbtTag
    {
        protected internal abstract NbtValueKind ValueKind { get; }
        internal NbtTag()
        {
            
        }
        public NbtTag GetValue(NbtValueKind valueKind)
        {
            return valueKind switch
            {
                NbtValueKind.End => throw new InvalidOperationException(),
                NbtValueKind.Byte => (ByteNbtTag)this,
                NbtValueKind.Short => (ShortNbtTag)this,
                NbtValueKind.Int => (IntNbtTag)this,
                NbtValueKind.Long => (LongNbtTag)this,
                NbtValueKind.Float => (FloatNbtTag)this,
                NbtValueKind.Double => (DoubleNbtTag)this,
                NbtValueKind.ByteArray => (ByteArrayNbtTag)this,
                NbtValueKind.String => (StringNbtTag)this,
                NbtValueKind.List => (ListNbtTag)this,
                NbtValueKind.Compound => (CompoundNbtTag)this,
                NbtValueKind.IntArray => (IntArrayNbtTag)this,
                NbtValueKind.LongArray => (LongArrayNbtTag)this,
                _ => throw new ArgumentException(),
            };
        }
        public abstract TNbtTag As<TNbtTag>() where TNbtTag : NbtTag;
        public abstract NbtTag Copy();
        public abstract NbtTag Merge(NbtTag nbt);
        internal abstract void SerializeValue(Stream stream);
        public abstract override string ToString();
        public void Serialize(Stream stream)
        {
            stream.WriteS8((sbyte)ValueKind);
            SerializeValue(stream);
        }
        internal static NbtTag DeserializeValue(Stream stream, NbtValueKind valueKind)
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
                _ => throw new ArgumentException(),
            };
        }
        public static NbtTag Deserialize(Stream stream)
        {
            NbtValueKind valueKind = (NbtValueKind)stream.ReadS8();
            return DeserializeValue(stream, valueKind);
        }
        public static NbtTag Parse(StringReader reader)
        {
            throw new NotImplementedException();
        }
    }
}