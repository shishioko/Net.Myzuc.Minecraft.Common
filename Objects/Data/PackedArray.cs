using System.Collections;
using System.Diagnostics.Contracts;

namespace Net.Myzuc.Minecraft.Common.Objects.Data
{
    public sealed class PackedArray : IList<int>
    {
        private sealed class Enumerator : IEnumerator<int> //idk i wrote this like 3 years ago
        {
            private readonly PackedArray Data;
            private int Index;
            private int Last;
            public int Current => Last;
            object IEnumerator.Current => Last;
            public Enumerator(PackedArray data)
            {
                Data = data;
                Index = -1;
                Last = 0;
            }
            public void Dispose()
            {

            }
            public bool MoveNext()
            {
                if (Index + 1 == Data.Length) return false;
                Index++;
                Last = Data[Index];
                return true;
            }
            public void Reset()
            {
                Index = -1;
                Last = 0;
            }
        }
        
        int ICollection<int>.Count => Length;
        public bool IsReadOnly => Buffer.IsReadOnly;
        
        public int Bits { get; }
        public int Length { get; }
        public ulong[] Buffer { get; } //todo: adjust class to use long
        
        public PackedArray(int bits, int length)
        {
            Contract.Requires(bits > 0);
            Contract.Requires(bits <= 32);
            Contract.Requires(length > 0);
            Bits = bits;
            Length = length;
            Buffer = new ulong[(length - 1) / (64 / bits) + 1];
        }
        public PackedArray(int bits, int length, ulong[] buffer)
        {
            Contract.Requires(bits > 0);
            Contract.Requires(bits <= 32);
            Contract.Requires(length > 0);
            Contract.Requires(length <= buffer.Length * (64 / bits));
            Bits = bits;
            Length = length;
            Buffer = buffer;
        }
        
        public int this[int index]
        {
            get
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < Length);
                int pack = 64 / Bits;
                return (int)(Buffer[index / pack] >> index % pack * Bits & (1UL << Bits) - 1UL);
            }
            set
            {
                Contract.Requires(index >= 0);
                Contract.Requires(index < Length);
                Contract.Requires(value < 1 << Bits);
                int pack = 64 / Bits;
                int high = index / pack;
                int low = index % pack;
                Buffer[high] &= ~((1UL << Bits) - 1UL << low * Bits);
                Buffer[high] |= (ulong)value << low * Bits;
            }
        }
        
        void ICollection<int>.Add(int item)
        {
            throw new InvalidOperationException();
        }
        void IList<int>.Insert(int index, int item)
        {
            throw new InvalidOperationException();
        }
        bool ICollection<int>.Remove(int item)
        {
            throw new InvalidOperationException();
        }
        void IList<int>.RemoveAt(int index)
        {
            throw new InvalidOperationException();
        }
        public void Clear()
        {
            Array.Clear(Buffer);
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            for (int i = arrayIndex; i < Length; i++)
            {
                array[i - arrayIndex] = this[i];
            }
        }
        public bool Contains(int item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return true;
            }
            return false;
        }
        public int IndexOf(int item)
        {
            for (int i = 0; i < Length; i++)
            {
                if (this[i] == item) return i;
            }
            return -1;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return new Enumerator(this);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public PackedArray Resize(byte bits, int length)
        {
            PackedArray destination = new(bits, length);
            for (int i = 0; i < length && i < Length; i++)
            {
                destination[i] = this[i] & ((1 << bits) - 1);
            }
            return destination;
        }
    }
}
