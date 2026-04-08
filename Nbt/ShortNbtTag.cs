using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
        public static explicit operator ByteNbtTag(ShortNbtTag nbt)
        {
            return new((sbyte)nbt.Value);
        }
        public static explicit operator IntNbtTag(ShortNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator LongNbtTag(ShortNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator FloatNbtTag(ShortNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator DoubleNbtTag(ShortNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(ShortNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(ShortNbtTag nbt)
        {
            return new(nbt.Value.ToString());
        }
        public static explicit operator ListNbtTag(ShortNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(ShortNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(ShortNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(ShortNbtTag nbt)
        {
            return new();
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
    }
}