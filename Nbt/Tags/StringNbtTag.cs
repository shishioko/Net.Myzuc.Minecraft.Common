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
                var type when type == typeof(ByteNbtTag) => new ByteNbtTag((sbyte)Value.Length),
                var type when type == typeof(ShortNbtTag) => new ShortNbtTag((short)Value.Length),
                var type when type == typeof(IntNbtTag) => new IntNbtTag(Value.Length),
                var type when type == typeof(LongNbtTag) => new LongNbtTag(Value.Length),
                var type when type == typeof(FloatNbtTag) => new FloatNbtTag(Value.Length),
                var type when type == typeof(DoubleNbtTag) => new DoubleNbtTag(Value.Length),
                var type when type == typeof(ByteArrayNbtTag) => new ByteArrayNbtTag(),
                var type when type == typeof(StringNbtTag) => this,
                var type when type == typeof(ListNbtTag) => new ListNbtTag(),
                var type when type == typeof(CompoundNbtTag) => new CompoundNbtTag(),
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
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