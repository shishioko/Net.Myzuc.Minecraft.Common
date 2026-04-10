using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly record struct UnresolvedProfile
    {
        [JsonConverter(typeof(GuidNbtJsonConverter))]
        [JsonPropertyName("id")]
        public Guid? Guid { get; init; } = null;
        [JsonPropertyName("name")]
        public string? Name { get; init; } = null;
        [JsonPropertyName("properties")]
        public IReadOnlyList<Property>? Properties { get; init; } = null;
        [JsonPropertyName("texture")]
        public string? Skin { get; init; } = null;
        [JsonPropertyName("cape")]
        public string? Cape { get; init; } = null;
        [JsonPropertyName("elytra")]
        public string? Elytra { get; init; } = null;
        [JsonPropertyName("model")]
        public PlayerModelType? ModelType { get; init; } = null;
        public UnresolvedProfile()
        {
            
        }
        public UnresolvedProfile(Guid guid)
        {
            Guid = guid;
        }
        public UnresolvedProfile(string name)
        {
            Name = name;
        }
        public UnresolvedProfile(ResolvedProfile profile)
        {
            Guid = profile.Guid;
            Name = profile.Name;
            Properties = profile.Properties;
        }
    }
}