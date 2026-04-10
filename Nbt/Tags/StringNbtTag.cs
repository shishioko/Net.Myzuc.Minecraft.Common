using System.Web;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record StringNbtTag : NbtTag
    {
        public string Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.String;
        public StringNbtTag()
        {
            Value = string.Empty;
        }
        public StringNbtTag(string value)
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
        public override StringNbtTag Copy()
        {
            return new(Value);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteT16AU16(Value);
        }
        public override string ToString()
        {
            return HttpUtility.JavaScriptStringEncode(Value, true);
        }
        internal static StringNbtTag DeserializeValue(Stream stream)
        {
            return stream.ReadT16AU16();
        }
        public static explicit operator ByteNbtTag(StringNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Length);
        }
        public static explicit operator ShortNbtTag(StringNbtTag nbt)
        {
            return new((short)nbt.Value.Length);
        }
        public static explicit operator IntNbtTag(StringNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator LongNbtTag(StringNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator FloatNbtTag(StringNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator DoubleNbtTag(StringNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator ByteArrayNbtTag(StringNbtTag nbt)
        {
            return new();
        }
        public static explicit operator ListNbtTag(StringNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(StringNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(StringNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(StringNbtTag nbt)
        {
            return new();
        }
        public static implicit operator string(StringNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator StringNbtTag(string value)
        {
            return new(value);
        }
    }
}