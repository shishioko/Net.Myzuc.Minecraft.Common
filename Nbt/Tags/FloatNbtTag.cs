using System.Globalization;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record FloatNbtTag : NbtTag
    {
        public float Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Float;
        public FloatNbtTag()
        {
            Value = 0;
        }
        public FloatNbtTag(float value)
        {
            Value = value;
        }
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(FloatNbtTag)) throw new InvalidDataException();
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
                var type when type == typeof(FloatNbtTag) => this,
                var type when type == typeof(DoubleNbtTag) => new DoubleNbtTag(Value),
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
        public override FloatNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteF32(Value);
        }
        public override string ToString()
        {
            return $"{Value}F";
        }
        internal static FloatNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadF32());
        }
        public static implicit operator float(FloatNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator FloatNbtTag(float value)
        {
            return new(value);
        }
    }
}