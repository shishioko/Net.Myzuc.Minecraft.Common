using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct Property
    {
        [JsonPropertyName("name")]
        public string Name { get; init; } = "";
        [JsonPropertyName("value")]
        public string Value { get; init; } = "";
        [JsonPropertyName("signature")]
        public string? Signature { get; } = null;
        public Property()
        {
            
        }
        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public Property(string name, string value, string? signature)
        {
            Name = name;
            Value = value;
            Signature = signature;
        }
    }
}