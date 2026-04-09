using System.Runtime.Serialization;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
                _ => throw new SerializationException()
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
        public static explicit operator ByteNbtTag(LongNbtTag nbt)
        {
            return new((sbyte)nbt.Value);
        }
        public static explicit operator ShortNbtTag(LongNbtTag nbt)
        {
            return new((short)nbt.Value);
        }
        public static explicit operator IntNbtTag(LongNbtTag nbt)
        {
            return new((int)nbt.Value);
        }
        public static explicit operator FloatNbtTag(LongNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator DoubleNbtTag(LongNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(LongNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(LongNbtTag nbt)
        {
            return new(nbt.Value.ToString());
        }
        public static explicit operator ListNbtTag(LongNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(LongNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(LongNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(LongNbtTag nbt)
        {
            return new();
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