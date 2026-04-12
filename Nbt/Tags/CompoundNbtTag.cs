using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt.Tags
{
    public sealed record CompoundNbtTag : NbtTag, IDictionary<string, NbtTag>
    {
        public int Count => Value.Count;
        public ICollection<string> Keys => Value.Keys;
        public ICollection<NbtTag> Values => Value.Values;
        bool ICollection<KeyValuePair<string, NbtTag>>.IsReadOnly => false;
        private Dictionary<string, NbtTag> Value { get; }
        protected internal override NbtValueKind ValueKind => NbtValueKind.Compound;
        public CompoundNbtTag()
        {
            Value = [];
        }
        public CompoundNbtTag(IDictionary<string, NbtTag> value)
        {
            Value = new(value);
        }
        public CompoundNbtTag(IEnumerable<KeyValuePair<string, NbtTag>> value)
        {
            Value = new(value);
        }
        public NbtTag this[string key]
        {
            get
            {
                return Value[key];
            }
            set
            {
                Value[key] = value;
            }
        }
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out NbtTag value)
        {
            return Value.TryGetValue(key, out value);
        }
        public bool ContainsKey(string key)
        {
            return Value.ContainsKey(key);
        }
        public bool Contains(KeyValuePair<string, NbtTag> item)
        {
            return Value.Contains(item);
        }
        public void Add(string key, NbtTag value)
        {
            Value.Add(key, value);
        }
        public void Add(KeyValuePair<string, NbtTag> item)
        {
            Value.Add(item.Key, item.Value);
        }
        public bool Remove(string key)
        {
            return Value.Remove(key);
        }
        public bool Remove(KeyValuePair<string, NbtTag> item)
        {
            return ((ICollection<KeyValuePair<string, NbtTag>>)Value).Remove(item);
        }
        public void Clear()
        {
            Value.Clear();
        }
        void ICollection<KeyValuePair<string, NbtTag>>.CopyTo(KeyValuePair<string, NbtTag>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, NbtTag>>)Value).CopyTo(array, arrayIndex);
        }
        public IEnumerator<KeyValuePair<string, NbtTag>> GetEnumerator()
        {
            return Value.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public override TNbtTag Get<TNbtTag>()
        {
            if (typeof(TNbtTag) != typeof(CompoundNbtTag)) throw new InvalidDataException();
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
                var type when type == typeof(ListNbtTag) => new ListNbtTag(),
                var type when type == typeof(CompoundNbtTag) => this,
                var type when type == typeof(IntArrayNbtTag) => new IntArrayNbtTag(),
                var type when type == typeof(LongArrayNbtTag) => new LongArrayNbtTag(),
                _ => throw new ArgumentException()
            };
            return (TNbtTag)nbt;
        }
        public override CompoundNbtTag Copy()
        {
            return new(Value.Select(kvp => new KeyValuePair<string, NbtTag>(kvp.Key, kvp.Value.Copy())));
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            Dictionary<string, NbtTag> map = Copy().Value;
            foreach (KeyValuePair<string, NbtTag> kvp in compoundNbt)
            {
                map[kvp.Key] = map[kvp.Key].Merge(kvp.Value);
            }
            return new CompoundNbtTag(map);
        }
        internal override void SerializeValue(Stream stream)
        {
            foreach (KeyValuePair<string, NbtTag> kvp in Value)
            {
                stream.WriteS8((sbyte)kvp.Value.ValueKind);
                stream.WriteT16AU16(kvp.Key);
                kvp.Value.SerializeValue(stream);
            }
            stream.WriteS8((sbyte)NbtValueKind.End);
        }
        public override string ToString()
        {
            return $"{{{string.Join(',', Value.Select(kvp => $"{HttpUtility.JavaScriptStringEncode(kvp.Key, true)}: {kvp.Value.ToString()}"))}}}";
        }
        internal static CompoundNbtTag DeserializeValue(Stream stream)
        {
            Dictionary<string, NbtTag> data = [];
            while (true)
            {
                NbtValueKind valueKind = (NbtValueKind)stream.ReadS8();
                if (valueKind == NbtValueKind.End) break;
                string name = stream.ReadT16AU16();
                NbtTag? value = NbtTag.DeserializeValue(stream, valueKind);
                if (value is not null) data[name] = value;
            }
            return new(data);
        }
    }
}