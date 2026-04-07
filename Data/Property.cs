using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public struct Property
    {
        [JsonPropertyName("name")] public string Name { get; set; } = "";
        [JsonPropertyName("value")] public string Value { get; set; } = "";
        [JsonPropertyName("signature")] public string? Signature { get; set; } = null;
        public Property()
        {
            
        }
        public Property(string name, string value, string? signature = null)
        {
            Name = name;
            Value = value;
            Signature = signature;
        }
    }
}