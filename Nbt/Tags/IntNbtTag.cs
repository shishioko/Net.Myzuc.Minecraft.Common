using System.Drawing;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record IntNbtTag : NbtTag
    {
        public int Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Int;
        public IntNbtTag()
        {
            Value = 0;
        }
        public IntNbtTag(int value)
        {
            Value = value;
        }
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(IntNbtTag)) throw new InvalidDataException();
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
                var type when type == typeof(IntNbtTag) => this,
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
        public override IntNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteS32(Value);
        }
        public override string ToString()
        {
            return $"{Value}I";
        }
        internal static IntNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS32());
        }
        public static implicit operator int(IntNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator IntNbtTag(int value)
        {
            return new(value);
        }
        public static implicit operator uint(IntNbtTag nbt)
        {
            return (uint)nbt.Value;
        }
        public static implicit operator IntNbtTag(uint value)
        {
            return new((int)value);
        }
        public static implicit operator nint(IntNbtTag nbt)
        {
            return (int)nbt.Value;
        }
        public static implicit operator IntNbtTag(nint value)
        {
            return new((int)value);
        }
        public static implicit operator nuint(IntNbtTag nbt)
        {
            return (uint)nbt.Value;
        }
        public static implicit operator IntNbtTag(nuint value)
        {
            return new((uint)value);
        }
        public static implicit operator Color(IntNbtTag nbt)
        {
            return Color.FromArgb(nbt.Value);
        }
        public static implicit operator IntNbtTag(Color value)
        {
            return new(value.ToArgb());
        }
    }
}