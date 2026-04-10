
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record ByteNbtTag : NbtTag
    {
        public sbyte Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Byte;
        public ByteNbtTag()
        {
            Value = 0;
        }
        public ByteNbtTag(sbyte value)
        {
            Value = value;
        }
        public override TNbtTag As<TNbtTag>()
        {
            NbtTag nbt = typeof(TNbtTag) switch
            {
                var type when type == typeof(NbtTag) => this,
                var type when type == typeof(ByteNbtTag) => this,
                var type when type == typeof(ShortNbtTag) => new ShortNbtTag(Value),
                var type when type == typeof(IntNbtTag) => new IntNbtTag(Value),
                var type when type == typeof(LongNbtTag) => new LongNbtTag(Value),
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
        public override ByteNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteS8(Value);
        }
        public override string ToString()
        {
            return $"{Value}B";
        }
        internal static ByteNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS8());
        }
        public static implicit operator sbyte(ByteNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator ByteNbtTag(sbyte value)
        {
            return new(value);
        }
        public static implicit operator byte(ByteNbtTag nbt)
        {
            return (byte)nbt.Value;
        }
        public static implicit operator ByteNbtTag(byte value)
        {
            return new((sbyte)value);
        }
        public static implicit operator bool(ByteNbtTag nbt)
        {
            return nbt.Value != 0;
        }
        public static implicit operator ByteNbtTag(bool value)
        {
            return new(value ? (sbyte)1 : (sbyte)0);
        }
    }
}