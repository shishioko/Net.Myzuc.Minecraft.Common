
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record LongNbtTag : NbtTag
    {
        public long Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Long;
        public LongNbtTag()
        {
            Value = 0;
        }
        public LongNbtTag(long value)
        {
            Value = value;
        }
        public override TNbtTag As<TNbtTag>()
        {
            NbtTag nbt = typeof(TNbtTag) switch
            {
                var type when type == typeof(NbtTag) => this,
                var type when type == typeof(ByteNbtTag) => new ByteNbtTag((sbyte)Value),
                var type when type == typeof(ShortNbtTag) => new ShortNbtTag((short)Value),
                var type when type == typeof(IntNbtTag) => new IntNbtTag((int)Value),
                var type when type == typeof(LongNbtTag) => this,
                var type when type == typeof(FloatNbtTag) => new FloatNbtTag(Value),
                var type when type == typeof(DoubleNbtTag) => new DoubleNbtTag(Value),
                var type when type == typeof(ByteArrayNbtTag) => new ByteArrayNbtTag(),
                var type when type == typeof(StringNbtTag) => new StringNbtTag(Value.ToString()),
                var type when type == typeof(ListNbtTag) => new ListNbtTag(),
                var type when type == typeof(CompoundNbtTag) => new CompoundNbtTag(),
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
                _ => throw new ArgumentException()
            };
            return (TNbtTag)nbt;
        }
        public override LongNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteS64(Value);
        }
        public override string ToString()
        {
            return $"{Value}L";
        }
        internal static LongNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS64());
        }
        public static implicit operator long(LongNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator LongNbtTag(long value)
        {
            return new(value);
        }
        public static implicit operator ulong(LongNbtTag nbt)
        {
            return (ulong)nbt.Value;
        }
        public static implicit operator LongNbtTag(ulong value)
        {
            return new((long)value);
        }
        public static implicit operator nint(LongNbtTag nbt)
        {
            return (nint)nbt.Value;
        }
        public static implicit operator LongNbtTag(nint value)
        {
            return new((long)value);
        }
        public static implicit operator nuint(LongNbtTag nbt)
        {
            return (nuint)nbt.Value;
        }
        public static implicit operator LongNbtTag(nuint value)
        {
            return new((ulong)value);
        }
    }
}