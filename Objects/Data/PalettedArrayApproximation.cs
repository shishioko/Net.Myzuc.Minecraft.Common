using System.Collections;
using System.Runtime.InteropServices;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Objects.Data
{
    public sealed class PalettedArrayApproximation : IList<int>, IBinarySerializable<PalettedArrayApproximation>
    {
        int ICollection<int>.Count => Container.Length;
        bool ICollection<int>.IsReadOnly => Container.IsReadOnly;
        
        public PackedArray Container { get; set; }

        public PalettedArrayApproximation(int length, int directBits)
        {
            Container = new(directBits, length);
        }
        
        
        public int this[int index]
        {
            get
            {
                return Container[index];
            }
            set
            {
                Container[index] = value;
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
            Container.Clear();
        }
        public void CopyTo(int[] array, int arrayIndex)
        {
            Container.CopyTo(array, arrayIndex);
        }
        public bool Contains(int item)
        {
            return Container.Contains(item);
        }
        public int IndexOf(int item)
        {
            return Container.IndexOf(item);
        }
        public IEnumerator<int> GetEnumerator()
        {
            return Container.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Container).GetEnumerator();
        }
        void IBinarySerializable<PalettedArrayApproximation>.Serialize(Stream stream)
        {
            SerializeDirect(Container, stream);
        }
        static PalettedArrayApproximation IBinarySerializable<PalettedArrayApproximation>.Deserialize(Stream stream)
        {
            throw new NotImplementedException();
        }
        
        private static void Serialize(int[] data, Stream stream, int indirectMinimum, int indirectMaximum, int direct)
        {
            Dictionary<int, int> palette = [];
            for (int i = 0; i < data.Length; i++)
            {
                int value = data[i];
                if (!palette.ContainsKey(value)) palette[data[i]] = palette.Count;
            }
            int bits = palette.Count > 0 ? int.Log2(palette.Count - 1) + 1 : 0;
            if (bits <= 0)
            {
                SerializeSingle(palette.Count > 0 ? palette.First().Key : 0, stream);
                return;
            }
            if (bits < indirectMaximum)
            {
                if (bits < indirectMinimum) bits = indirectMinimum;
                PackedArray packed = new(bits, data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    int value = data[i];
                    packed[i] = palette[value];
                }
                SerializeIndirect(packed, palette.Select(kvp => kvp.Key).ToList(), stream);
                return;
            }
            
            {
                bits = direct;
                PackedArray packed = new(bits, data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    int value = data[i];
                    packed[i] = value;
                }
                return;
            }
        }
        private static void SerializeSingle(int value, Stream stream)
        {
            stream.WriteU8(0);
            stream.WriteS32V(value);
        }
        private static void SerializeIndirect(PackedArray data, IList<int> palette, Stream stream)
        {
            stream.WriteU8((byte)data.Bits);
            stream.WriteS32V(palette.Count);
            foreach (int value in palette)
            {
                stream.WriteS32V(value);
            }
            stream.WriteS64A(MemoryMarshal.Cast<ulong,long>(data.Buffer));
        }
        private static void SerializeDirect(PackedArray data, Stream stream)
        {
            stream.WriteU8((byte)data.Bits);
            stream.WriteS64A(MemoryMarshal.Cast<ulong,long>(data.Buffer));
        }
        
        //todo: deserialization
        // https://minecraft.wiki/w/Java_Edition_protocol/Chunk_format#Data_Array_format
    }
}