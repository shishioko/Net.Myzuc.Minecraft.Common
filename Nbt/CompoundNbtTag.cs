using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Nbt
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
        public override CompoundNbtTag Copy()
        {
            return new(Value.Select(kvp => new KeyValuePair<string, NbtTag>(kvp.Key, kvp.Value.Copy())));
        }
        public override NbtTag Merge(NbtTag nbt)
        {
            if (nbt is not CompoundNbtTag compound) return nbt;
            Dictionary<string, NbtTag> map = Copy().Value;
            foreach (KeyValuePair<string, NbtTag> kvp in compound)
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
                new StringNbtTag(kvp.Key).SerializeValue(stream);
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
                string name = StringNbtTag.DeserializeValue(stream).Value;
                NbtTag value = NbtTag.DeserializeValue(stream, valueKind);
                data[name] = value;
            }
            return new(data);
        }
        public static explicit operator ByteNbtTag(CompoundNbtTag nbt)
        {
            return new((sbyte)nbt.Value.Count);
        }
        public static explicit operator ShortNbtTag(CompoundNbtTag nbt)
        {
            return new((short)nbt.Value.Count);
        }
        public static explicit operator IntNbtTag(CompoundNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator LongNbtTag(CompoundNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator FloatNbtTag(CompoundNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator DoubleNbtTag(CompoundNbtTag nbt)
        {
            return new(nbt.Value.Count);
        }
        public static explicit operator ByteArrayNbtTag(CompoundNbtTag nbt)
        {
            return new();
        }
        public static explicit operator StringNbtTag(CompoundNbtTag nbt)
        {
            return new(nbt.Value.Count.ToString());
        }
        public static explicit operator ListNbtTag(CompoundNbtTag nbt)
        {
            return new();
        }
        public static explicit operator IntArrayNbtTag(CompoundNbtTag nbt)
        {
            return new();
        }
        public static explicit operator LongArrayNbtTag(CompoundNbtTag nbt)
        {
            return new();
        }
    }
}