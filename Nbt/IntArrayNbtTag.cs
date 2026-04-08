using System.Collections;
using System.Runtime.InteropServices;
using Me.Shiokawaii.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
{
    public sealed record IntArrayNbtTag : NbtTag, IList<int>
    {
        public int Length => Value.Length;
        int ICollection<int>.Count => Value.Length;
        bool ICollection<int>.IsReadOnly => Value.IsReadOnly;
        public int[] Value { get; set; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.IntArray;
        public IntArrayNbtTag()
        {
            Value = [];
        }
        public IntArrayNbtTag(int size)
        {
            Value = new int[size];
        }
        public IntArrayNbtTag(int[] value)
        {
            Value = value;
        }
        public int this[int index]
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
        void ICollection<int>.Add(int item)
        {
            ((ICollection<int>)Value).Add(item);
        }
        void IList<int>.Insert(int index, int item)
        {
            ((IList<int>)Value).Insert(index, item);
        }
        bool ICollection<int>.Remove(int item)
        {
            return ((ICollection<int>)Value).Remove(item);
        }
        void IList<int>.RemoveAt(int index)
        {
            ((IList<int>)Value).RemoveAt(index);
        }
        public void Clear()
        {
            ((ICollection<int>)Value).Clear();
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            Value.CopyTo(array, arrayIndex);
        }
        public bool Contains(int item)
        {
            return Value.Contains(item);
        }
        public int IndexOf(int item)
        {
            return Value.IndexOf(item);
        }
        public IEnumerator<int> GetEnumerator()
        {
            return ((IEnumerable<int>)Value).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Value).GetEnumerator();
        }
        public override IntArrayNbtTag Copy()
        {
            int[] original = Value;
            int[] copy = new int[Value.Length];
            Buffer.BlockCopy(original, 0, copy, 0, copy.Length * sizeof(int));
            return new(copy);
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            int[] data = Value;
            stream.WriteS32(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                stream.WriteS32(data[i]);
            }
        }
        public override string ToString()
        {
            return $"[I;{string.Join(',', Value.Select(value => $"{value}I"))}]";
        }
        internal static IntArrayNbtTag DeserializeValue(Stream stream)
        {
            int[] data = new int[stream.ReadS32()];
            for (int i = 0; i < data.Length; i++) data[i] = stream.ReadS32();
            return new(data);
        }
        public static explicit operator ByteNbtTag(IntArrayNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Length);
        }
        public static explicit operator ShortNbtTag(IntArrayNbtTag nbt)
        {
            return new((short)nbt.Value.Length);
        }
        public static explicit operator IntNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator LongNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator FloatNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator DoubleNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Length);
        }
        public static explicit operator ByteArrayNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (sbyte)value).ToArray());
        }
        public static explicit operator StringNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Length.ToString());
        }
        public static explicit operator ListNbtTag(IntArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator CompoundNbtTag(IntArrayNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(IntArrayNbtTag nbt)
        {
            return new(nbt.Value.Select(value => (long)value).ToArray());
        }
        public static implicit operator int[](IntArrayNbtTag nbt)
        {
            return nbt.Value;
        }
        public static implicit operator IntArrayNbtTag(int[] value)
        {
            return new(value);
        }
        public static implicit operator uint[](IntArrayNbtTag nbt)
        {
            return MemoryMarshal.Cast<int, uint>(nbt.Value).ToArray();
        }
        public static implicit operator IntArrayNbtTag(uint[] value)
        {
            return new(MemoryMarshal.Cast<uint, int>(value).ToArray());
        }
        public static implicit operator Guid(IntArrayNbtTag nbt)
        {
            byte[] data = MemoryMarshal.Cast<int, byte>(nbt.Value).ToArray();
            return new(data);
        }
        public static implicit operator IntArrayNbtTag(Guid value)
        {
            int[] data = MemoryMarshal.Cast<byte, int>(value.ToByteArray(true)).ToArray();
            return new(data);
        }
    }
}