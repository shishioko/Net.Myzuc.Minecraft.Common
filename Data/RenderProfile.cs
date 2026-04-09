using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public class RenderProfile
    {
        [JsonConverter(typeof(GuidNbtJsonConverter))]
        [JsonPropertyName("id")] public Guid? Guid { get; set; } = null;
        [JsonPropertyName("name")] public string? Name { get; set; } = null;
        [JsonPropertyName("properties")] public Property[]? Properties { get; set; } = null;
        [JsonPropertyName("texture")] public string? Skin { get; set; } = null;
        [JsonPropertyName("cape")] public string? Cape { get; set; } = null;
        [JsonPropertyName("elytra")] public string? Elytra { get; set; } = null;
        [JsonPropertyName("model")] public PlayerModelType? ModelType { get; set; } = null;
        public RenderProfile()
        {
            
        }
        public RenderProfile(Guid guid)
        {
            Guid = guid;
        }
        public RenderProfile(string name)
        {
            Name = name;
        }
        public RenderProfile(GameProfile profile)
        {
            Guid = profile.Guid;
            Name = profile.Name;
            Properties = profile.Properties;
        }
    }
}