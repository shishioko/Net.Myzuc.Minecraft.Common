
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
                var type when type == typeof(ByteNbtTag) => (ByteNbtTag)this,
                var type when type == typeof(ShortNbtTag) => (ShortNbtTag)this,
                var type when type == typeof(IntNbtTag) => (IntNbtTag)this,
                var type when type == typeof(LongNbtTag) => (LongNbtTag)this,
                var type when type == typeof(FloatNbtTag) => (FloatNbtTag)this,
                var type when type == typeof(DoubleNbtTag) => (DoubleNbtTag)this,
                var type when type == typeof(ByteArrayContent) => (LongNbtTag)this,
                var type when type == typeof(StringNbtTag) => (StringNbtTag)this,
                var type when type == typeof(ListNbtTag) => (ListNbtTag)this,
                var type when type == typeof(CompoundNbtTag) => (CompoundNbtTag)this,
                var type when type == typeof(IntArrayNbtTag) => (IntArrayNbtTag)this,
                var type when type == typeof(LongArrayNbtTag) => (LongArrayNbtTag)this,
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
        public static explicit operator ShortNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator IntNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator LongNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator FloatNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator DoubleNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(ByteNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(ByteNbtTag nbt)
        {
            return new(nbt.Value.ToString());
        }
        public static explicit operator ListNbtTag(ByteNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(ByteNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(ByteNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(ByteNbtTag nbt)
        {
            return new();
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