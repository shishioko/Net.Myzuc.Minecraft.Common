using System.Globalization;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
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
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(DoubleNbtTag)) throw new InvalidDataException();
            NbtTag nbt = this;
            return (TNbtTag)nbt;
        }
        public override TNbtTag As<TNbtTag>()
        {
            NbtTag nbt = typeof(TNbtTag) switch
            {
                var type when type == typeof(NbtTag) => this,
                var type when type == typeof(ByteNbtTag) => new ByteNbtTag((sbyte)Value),
                var type when type == typeof(ShortNbtTag) => new ShortNbtTag((short)Value),
                var type when type == typeof(IntNbtTag) => new IntNbtTag((int)Value),
                var type when type == typeof(LongNbtTag) => new LongNbtTag((long)Value),
                var type when type == typeof(FloatNbtTag) => new FloatNbtTag((float)Value),
                var type when type == typeof(DoubleNbtTag) => this,
                var type when type == typeof(ByteArrayNbtTag) => new ByteArrayNbtTag(),
                var type when type == typeof(StringNbtTag) => new StringNbtTag(Value.ToString(CultureInfo.InvariantCulture)),
                var type when type == typeof(ListNbtTag) => new ListNbtTag(),
                var type when type == typeof(CompoundNbtTag) => new CompoundNbtTag(),
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
                _ => throw new ArgumentException()
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