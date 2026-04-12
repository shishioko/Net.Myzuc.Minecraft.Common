using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.Primitives.JsonConverters;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Primitives
{
    [JsonConverter(typeof(IdentifierJsonConverter))]
    public readonly record struct Identifier : IBinarySerializable<Identifier>, INbtSerializable<Identifier>
    {
        private const string ValidNamespaceCharacters = "#abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_";
        private const string ValidValueCharacters = "#abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_/";
        
        [IgnoreDataMember]
        public string FullIdentifier
        {
            get
            {
                return $"{Namespace}:{Value}";
            }
        }
        public string Namespace { get; }
        public string Value { get; }
        public Identifier()
        {
            Namespace = "minecraft";
            Value = "unknown";
        }
        public Identifier(string @namespace, string value)
        {
            Namespace = @namespace;
            Value = value;
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Value.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Value}'!");
        }
        public Identifier(string fullIdentifier)
        {
            string[] parts = fullIdentifier.Split(':');
            if (parts.Length > 2) throw new ArgumentException($"Invalid identifier '{fullIdentifier}'!");
            Namespace = parts.Length > 1 ? parts[0] : "minecraft";
            Value = parts[^1];
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Value.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Value}'!");
        }
        
        public override string ToString()
        {
            return FullIdentifier;
        }
        
        public static implicit operator Identifier(string value)
        {
            return new(value);
        }
        public static implicit operator string(Identifier value)
        {
            return value.FullIdentifier;
        }
        
        static Identifier IBinarySerializable<Identifier>.Deserialize(Stream stream)
        {
            return new(stream.ReadT16AS32V());
        }
        void IBinarySerializable<Identifier>.Serialize(Stream stream)
        {
            stream.WriteT16AS32V(FullIdentifier);
        }
        static Identifier INbtSerializable<Identifier>.FromNbt(NbtTag nbt)
        {
            StringNbtTag stringNbt = nbt.As<StringNbtTag>();
            return new(stringNbt);
        }
        NbtTag INbtSerializable<Identifier>.ToNbt()
        {
            return (StringNbtTag)FullIdentifier;
        }
    }
}