
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record ShortNbtTag : NbtTag
    {
        public short Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Short;
        public ShortNbtTag()
        {
            Value = 0;
        }
        public ShortNbtTag(short value)
        {
            Value = value;
        }
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(ShortNbtTag)) throw new InvalidDataException();
            NbtTag nbt = this;
            return (TNbtTag)nbt;
        }
        public override TNbtTag As<TNbtTag>()
        {
            NbtTag nbt = typeof(TNbtTag) switch
            {
                var type when type == typeof(NbtTag) => this,
                var type when type == typeof(ByteNbtTag) => new ByteNbtTag((sbyte)Value),
                var type when type == typeof(ShortNbtTag) => this,
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
        public override ShortNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteS16(Value);
        }
        public override string ToString()
        {
            return $"{Value}S";
        }
        internal static ShortNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS16());
        }
        public static implicit operator short(ShortNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator ShortNbtTag(short value)
        {
            return new(value);
        }
        public static implicit operator ushort(ShortNbtTag nbt)
        {
            return (ushort)nbt.Value;
        }
        public static implicit operator ShortNbtTag(ushort value)
        {
            return new((short)value);
        }
        public static implicit operator char(ShortNbtTag nbt)
        {
            return (char)nbt.Value;
        }
        public static implicit operator ShortNbtTag(char value)
        {
            return new((short)value);
        }
    }
}