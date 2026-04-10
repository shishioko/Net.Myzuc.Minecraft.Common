using System.Collections;
using System.Runtime.InteropServices;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
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
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(ByteArrayNbtTag)) throw new InvalidDataException();
            NbtTag nbt = this;
            return (TNbtTag)nbt;
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
                var type when type == typeof(ByteArrayNbtTag) => this,
                var type when type == typeof(StringNbtTag) => new StringNbtTag(Value.Length.ToString()),
                var type when type == typeof(ListNbtTag) => new ListNbtTag(),
                var type when type == typeof(CompoundNbtTag) => new CompoundNbtTag(),
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
                _ => throw new ArgumentException()
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
            stream.WriteS8AS32(Value);
        }
        public override string ToString()
        {
            return $"[B;{string.Join(',', Value.Select(value => $"{value}B"))}]";
        }
        internal static ByteArrayNbtTag DeserializeValue(Stream stream)
        {
            return new(stream.ReadS8AS32());
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