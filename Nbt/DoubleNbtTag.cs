using System.Globalization;
using System.Runtime.Serialization;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed record DoubleNbtTag : NbtTag
    {
        public double Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Double;
        public DoubleNbtTag()
        {
            Value = 0;
        }
        public DoubleNbtTag(double value)
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
        public override DoubleNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteF64(Value);
        }
        public override string ToString()
        {
            return $"{Value}D";
        }
        internal static DoubleNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadF64());
        }
        public static explicit operator ByteNbtTag(DoubleNbtTag nbt)
        {
            return new((sbyte)nbt.Value);
        }
        public static explicit operator ShortNbtTag(DoubleNbtTag nbt)
        {
            return new((short)nbt.Value);
        }
        public static explicit operator IntNbtTag(DoubleNbtTag nbt)
        {
            return new((int)nbt.Value);
        }
        public static explicit operator LongNbtTag(DoubleNbtTag nbt)
        {
            return new((long)nbt.Value);
        }
        public static explicit operator FloatNbtTag(DoubleNbtTag nbt)
        {
            return new((float)nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(DoubleNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(DoubleNbtTag nbt)
        {
            return new(nbt.Value.ToString(CultureInfo.InvariantCulture));
        }
        public static explicit operator ListNbtTag(DoubleNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(DoubleNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(DoubleNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(DoubleNbtTag nbt)
        {
            return new();
        }
        public static implicit operator double(DoubleNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator DoubleNbtTag(double value)
        {
            return new(value);
        }
        public static implicit operator decimal(DoubleNbtTag nbt)
        {
            return (decimal)nbt.Value;
        }
        public static implicit operator DoubleNbtTag(decimal value)
        {
            return new((double)value);
        }
    }
}