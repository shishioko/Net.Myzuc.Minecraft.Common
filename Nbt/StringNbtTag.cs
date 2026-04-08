using System.Text;
using System.Web;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
            byte[] buffer = Encoding.UTF8.GetBytes(Value);
            if (buffer.Length > ushort.MaxValue) buffer = buffer[..ushort.MaxValue];
            stream.WriteU16((ushort)buffer.Length);
            stream.WriteU8A(buffer);
        }
        public override string ToString()
        {
            return HttpUtility.JavaScriptStringEncode(Value, true);
        }
        internal static StringNbtTag DeserializeValue(Stream stream)
        {
            byte[] buffer = stream.ReadU8A(stream.ReadU16());
            return Encoding.UTF8.GetString(buffer);
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