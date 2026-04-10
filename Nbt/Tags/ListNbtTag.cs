using System.Collections;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record ListNbtTag : NbtTag, IList<NbtTag>
    {
        public int Count => Value.Count;
        public bool IsReadOnly => false;
        private List<NbtTag> Value { get; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.List;
        public ListNbtTag()
        {
            Value = [];
        }
        public ListNbtTag(IList<NbtTag> value)
        {
            Value = new(value);
        }
        public ListNbtTag(IEnumerable<NbtTag> value)
        {
            Value = new(value);
        }
        public NbtTag this[int index]
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
        public void Add(NbtTag item)
        {
            Value.Add(item);
        }
        public void Insert(int index, NbtTag item)
        {
            Value.Insert(index, item);
        }
        public bool Remove(NbtTag item)
        {
            return Value.Remove(item);
        }
        public void RemoveAt(int index)
        {
            Value.RemoveAt(index);
        }
        public void Clear()
        {
            Value.Clear();
        }
        public void CopyTo(NbtTag[] array, int arrayIndex)
        {
            Value.CopyTo(array, arrayIndex);
        }
        public bool Contains(NbtTag item)
        {
            return Value.Contains(item);
        }
        public int IndexOf(NbtTag item)
        {
            return Value.IndexOf(item);
        }
        public IEnumerator<NbtTag> GetEnumerator()
        {
            return ((IEnumerable<NbtTag>)Value).GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Value).GetEnumerator();
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
        public override ListNbtTag Copy()
        {
            return new(Value.Select(nbt => nbt.Copy()));
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            return nbt;
        }
        internal override void SerializeValue(Stream stream)
        {
            NbtValueKind valueKind = Value.Any() ? Value.First().ValueKind : NbtValueKind.End;
            bool heterogenous = Value.Any(nbt => nbt.ValueKind != valueKind);
            if (heterogenous)
            {
                ListNbtTag serialNbt = new ListNbtTag(Value.Select(
                    nbt =>
                    {
                        Dictionary<string, NbtTag> map = new()
                        {
                            {"", nbt}
                        };
                        return new CompoundNbtTag(map);
                    }
                ));
                serialNbt.SerializeValue(stream);
                return;
            }
            stream.WriteS8((sbyte)valueKind);
            if (valueKind == NbtValueKind.End) return;
            stream.WriteS32(Value.Count);
            foreach (NbtTag nbt in Value)
            {
                nbt.SerializeValue(stream);
            }
        }
        public override string ToString()
        {
            return $"[{string.Join(',', Value.Select(nbt => nbt.ToString()))}]";
        }
        internal static ListNbtTag DeserializeValue(Stream stream)
        {
            NbtValueKind valueKind = (NbtValueKind)stream.ReadS8();
            if (valueKind == NbtValueKind.End) return new();
            NbtTag[] data = new NbtTag[stream.ReadS32()];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = NbtTag.DeserializeValue(stream, valueKind)!;
            }
            if (valueKind == NbtValueKind.Compound)
            {
                data = data.Select(
                    nbt =>
                    {
                        CompoundNbtTag compound = (nbt as CompoundNbtTag)!;
                        if (compound.Count == 1 && compound.ContainsKey(string.Empty)) return compound[string.Empty];
                        return nbt;
                    }
                ).ToArray();
            }
            return new(data);
        }
        public static explicit operator ByteNbtTag(ListNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Count);
        }
        public static explicit operator ShortNbtTag(ListNbtTag nbt)
        {
            return new((short)nbt.Value.Count);
        }
        public static explicit operator IntNbtTag(ListNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator LongNbtTag(ListNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator FloatNbtTag(ListNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator DoubleNbtTag(ListNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator ByteArrayNbtTag(ListNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(ListNbtTag nbt)
        {
            return new(nbt.Value.Count.ToString());
        }
        public static explicit operator CompoundNbtTag(ListNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(ListNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(ListNbtTag nbt)
        {
            return new();
        }
    }
}