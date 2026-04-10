namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly struct Identifier
    {
        private const string ValidNamespaceCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_";
        private const string ValidValueCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWYZ0123456789.-_/";
        
        public string Namespace { get; }
        public string Value { get; }
        public Identifier(string identifier)
        {
            string[] parts = identifier.Split(':');
            if (parts.Length > 2) throw new ArgumentException($"Invalid identifier '{identifier}'!");
            Namespace = parts.Length > 1 ? parts[1] : "minecraft";
            Value = parts[^1];
            if (Namespace.Any(c => !ValidNamespaceCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier namespace '{Namespace}'!");
            if (Value.Any(c => !ValidValueCharacters.Contains(c))) throw new ArgumentException($"Invalid identifier value '{Value}'!");
        }
        public override string ToString()
        {
            return $"{Namespace}:{Value}";
        }
    }
}