using System.Globalization;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
        public static explicit operator ByteNbtTag(FloatNbtTag nbt)
        {
            return new((sbyte)nbt.Value);
        }
        public static explicit operator ShortNbtTag(FloatNbtTag nbt)
        {
            return new((short)nbt.Value);
        }
        public static explicit operator IntNbtTag(FloatNbtTag nbt)
        {
            return new((int)nbt.Value);
        }
        public static explicit operator LongNbtTag(FloatNbtTag nbt)
        {
            return new((long)nbt.Value);
        }
        public static explicit operator DoubleNbtTag(FloatNbtTag nbt)
        {
            return new(nbt.Value);
        }
        public static explicit operator ByteArrayNbtTag(FloatNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(FloatNbtTag nbt)
        {
            return new(nbt.Value.ToString(CultureInfo.InvariantCulture));
        }
        public static explicit operator ListNbtTag(FloatNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(FloatNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(FloatNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(FloatNbtTag nbt)
        {
            return new();
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