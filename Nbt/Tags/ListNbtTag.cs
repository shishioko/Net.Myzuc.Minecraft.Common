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
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(ListNbtTag)) throw new InvalidDataException();
            NbtTag nbt = this;
            return (TNbtTag)nbt;
        }
        public override TNbtTag As<TNbtTag>()
        {
            NbtTag nbt = typeof(TNbtTag) switch
            {
                var type when type == typeof(NbtTag) => this,
                var type when type == typeof(ByteNbtTag) => new ByteNbtTag((sbyte)Value.Count),
                var type when type == typeof(ShortNbtTag) => new ShortNbtTag((short)Value.Count),
                var type when type == typeof(IntNbtTag) => new IntNbtTag(Value.Count),
                var type when type == typeof(LongNbtTag) => new LongNbtTag(Value.Count),
                var type when type == typeof(FloatNbtTag) => new FloatNbtTag(Value.Count),
                var type when type == typeof(DoubleNbtTag) => new DoubleNbtTag(Value.Count),
                var type when type == typeof(ByteArrayNbtTag) => new ByteArrayNbtTag(),
                var type when type == typeof(StringNbtTag) => new StringNbtTag(Value.Count.ToString()),
                var type when type == typeof(ListNbtTag) => this,
                var type when type == typeof(CompoundNbtTag) => new CompoundNbtTag(),
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
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
            stream.WriteS32(Value.Count);
            if (valueKind == NbtValueKind.End) return;
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
            NbtTag[] data = new NbtTag[stream.ReadS32()];
            if (valueKind == NbtValueKind.End) return new();
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = NbtTag.DeserializeValue(stream, valueKind)!;
            }
            if (valueKind == NbtValueKind.Compound)
            {
                data = data.Select(
                    nbt =>
                    {
                        CompoundNbtTag compoundNbt = (nbt as CompoundNbtTag)!;
                        if (compoundNbt.Count == 1 && compoundNbt.ContainsKey(string.Empty)) return compoundNbt[string.Empty];
                        return nbt;
                    }
                ).ToArray();
            }
            return new(data);
        }
    }
}