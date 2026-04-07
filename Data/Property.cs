using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public struct Property
    {
        [JsonPropertyName("name")]
        public string Name = "";
        [JsonPropertyName("value")]
        public string Value = "";
        [JsonPropertyName("signature")]
        public string? Signature = null;
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