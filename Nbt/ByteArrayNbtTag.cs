using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed record ByteArrayNbtTag : NbtTag, IList<sbyte>
    {
        public int Length => Value.Length;
        int ICollection<sbyte>.Count => Value.Length;
        bool ICollection<sbyte>.IsReadOnly => Value.IsReadOnly;
        public sbyte[] Value { get; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.ByteArray;
        public ByteArrayNbtTag()
        {
            Value = [];
        }
        public ByteArrayNbtTag(int size)
        {
            Value = new sbyte[size];
        }
        public ByteArrayNbtTag(sbyte[] value)
        {
            Value = value;
        }
        public sbyte this[int index]
        {
            get
            {
                return Value[index];
            }
            set
            {
                Value[index] = value;
            }
        }
        void ICollection<sbyte>.Add(sbyte item)
        {
            ((ICollection<sbyte>)Value).Add(item);
        }
        void IList<sbyte>.Insert(int index, sbyte item)
        {
            ((IList<sbyte>)Value).Insert(index, item);
        }
        bool ICollection<sbyte>.Remove(sbyte item)
        {
            return ((ICollection<sbyte>)Value).Remove(item);
        }
        void IList<sbyte>.RemoveAt(int index)
        {
            ((IList<sbyte>)Value).RemoveAt(index);
        }
        public void Clear()
        {
            ((ICollection<sbyte>)Value).Clear();
        }
        public void CopyTo(sbyte[] array, int arrayIndex)
        {
            Value.CopyTo(array, arrayIndex);
        }
        public bool Contains(sbyte item)
        {
            return Value.Contains(item);
        }
        public int IndexOf(sbyte item)
        {
            return Value.IndexOf(item);
        }
        public IEnumerator<sbyte> GetEnumerator()
        {
            return ((IEnumerable<sbyte>)Value).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
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
                _ => throw new SerializationException()
            };
            return (TNbtTag)nbt;
        }
        public override ByteArrayNbtTag Copy()
        {
            sbyte[] original = Value;
            sbyte[] copy = new sbyte[Value.Length];
            Buffer.BlockCopy(original, 0, copy, 0, copy.Length);
            return new(copy);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            sbyte[] data = Value;
            stream.WriteS32(data.Length);
            stream.WriteU8A(MemoryMarshal.Cast<sbyte,byte>(data).ToArray());
        }
        public override string ToString()
        {
            return $"[B;{string.Join(',', Value.Select(value => $"{value}B"))}]";
        }
        internal static ByteArrayNbtTag DeserializeValue(Stream stream)
        {
            byte[] buffer = stream.ReadU8A(stream.ReadS32());
            return new(MemoryMarshal.Cast<byte, sbyte>(buffer).ToArray());
        }
        public static explicit operator ByteNbtTag(ByteArrayNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Length);
        }
        public static explicit operator ShortNbtTag(ByteArrayNbtTag nbt)
        {
            return new((short)nbt.Value.Length);
        }
        public static explicit operator IntNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator LongNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator FloatNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator DoubleNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator StringNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Length.ToString());
        }
        public static explicit operator ListNbtTag(ByteArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(ByteArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (int)value).ToArray());
        }
        public static explicit operator LongArrayNbtTag(ByteArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (long)value).ToArray());
        }
        public static implicit operator sbyte[](ByteArrayNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator ByteArrayNbtTag(sbyte[] value)
        {
            return new(value);
        }
        public static implicit operator byte[](ByteArrayNbtTag nbt)
        {
            return MemoryMarshal.Cast<sbyte, byte>(nbt.Value).ToArray();
        }
        public static implicit operator ByteArrayNbtTag(byte[] value)
        {
            return new(MemoryMarshal.Cast<byte, sbyte>(value).ToArray());
        }
        public static implicit operator bool[](ByteArrayNbtTag nbt)
        {
            return MemoryMarshal.Cast<sbyte, bool>(nbt.Value).ToArray();
        }
        public static implicit operator ByteArrayNbtTag(bool[] value)
        {
            return new(MemoryMarshal.Cast<bool, sbyte>(value).ToArray());
        }
    }
}