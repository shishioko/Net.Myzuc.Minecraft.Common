using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
        public static explicit operator ByteNbtTag(IntNbtTag nbt)
        {
            return new((sbyte)nbt.Value);
        }
        public static explicit operator ShortNbtTag(IntNbtTag nbt)
        {
            return new((short)nbt.Value);
        }
        public static explicit operator LongNbtTag(IntNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator FloatNbtTag(IntNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator DoubleNbtTag(IntNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(IntNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(IntNbtTag nbt)
        {
            return new(nbt.Value.ToString());
        }
        public static explicit operator ListNbtTag(IntNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(IntNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(IntNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(IntNbtTag nbt)
        {
            return new();
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
    }
}