using System.Collections;
using System.Runtime.InteropServices;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record LongArrayNbtTag : NbtTag, IList<long>
    {
        public int Length => Value.Length;
        int ICollection<long>.Count => Value.Length;
        bool ICollection<long>.IsReadOnly => Value.IsReadOnly;
        public long[] Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.LongArray;
        public LongArrayNbtTag()
        {
            Value = [];
        
        }
        public LongArrayNbtTag(int size)
        {
            Value = new long[size];
        }
        public LongArrayNbtTag(long[] value)
        {
            Value = value;
        }
        public long this[int index]
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
        void ICollection<long>.Add(long item)
        {
            ((ICollection<long>)Value).Add(item);
        }
        void IList<long>.Insert(int index, long item)
        {
            ((IList<long>)Value).Insert(index, item);
        }
        bool ICollection<long>.Remove(long item)
        {
            return ((ICollection<long>)Value).Remove(item);
        }
        void IList<long>.RemoveAt(int index)
        {
            ((IList<long>)Value).RemoveAt(index);
        }
        public void Clear()
        {
            ((ICollection<long>)Value).Clear();
        }
        public void CopyTo(long[] array, int arrayIndex)
        {
            Value.CopyTo(array, arrayIndex);
        }
        public bool Contains(long item)
        {
            return Value.Contains(item);
        }
        public int IndexOf(long item)
        {
            return Value.IndexOf(item);
        }
        public IEnumerator<long> GetEnumerator()
        {
            return (Value.GetEnumerator() as IEnumerator<long>)!;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<long>)Value).GetEnumerator();
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
        public override LongArrayNbtTag Copy()
        {
            long[] original = Value;
            long[] copy = new long[Value.Length];
            Buffer.BlockCopy(original, 0, copy, 0, copy.Length * sizeof(long));
            return new(copy);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            stream.WriteS64AS32(Value);
        }
        public override string ToString()
        {
            return $"[L;{string.Join(',', Value.Select(value => $"{value}L"))}]";
        }
        internal static LongArrayNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS64AS32());
        }
        public static explicit operator ByteNbtTag(LongArrayNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Length);
        }
        public static explicit operator ShortNbtTag(LongArrayNbtTag nbt)
        {
            return new((short)nbt.Value.Length);
        }
        public static explicit operator IntNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator LongNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator FloatNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator DoubleNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator ByteArrayNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (sbyte)value).ToArray());
        }
        public static explicit operator StringNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Length.ToString());
        }
        public static explicit operator ListNbtTag(LongArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(LongArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(LongArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (int)value).ToArray());
        }
        public static implicit operator long[](LongArrayNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator LongArrayNbtTag(long[] value)
        {
            return new(value);
        }
        public static implicit operator ulong[](LongArrayNbtTag nbt)
        {
            return MemoryMarshal.Cast<long, ulong>(nbt.Value).ToArray();
        }
        public static implicit operator LongArrayNbtTag(ulong[] value)
        {
            return new(MemoryMarshal.Cast<ulong, long>(value).ToArray());
        }
    }
}