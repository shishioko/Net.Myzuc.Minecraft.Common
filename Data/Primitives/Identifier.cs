using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.Primitives.JsonConverters;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Primitives
{
    [JsonConverter(typeof(IdentifierJsonConverter))]
    public readonly struct Identifier
    {
        private const string ValidNamespaceCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_";
        private const string ValidValueCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_/";
        
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
            Namespace = parts.Length > 1 ? parts[1] : "minecraft";
            Value = parts[^1];
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Value.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Value}'!");
        }
        
        internal Identifier(StringNbtTag nbt)
        {
            Identifier id = new(nbt.Value);
        }
        
        internal StringNbtTag Serialize()
        {
            return FullIdentifier;
        }
        
        public override string ToString()
        {
            return FullIdentifier;
        }
    }
}