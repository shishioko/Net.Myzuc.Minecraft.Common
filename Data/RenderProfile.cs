using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public class RenderProfile
    {
        [JsonConverter(typeof(ArrayGuidJsonConverter))]
        [JsonPropertyName("id")]
        public Guid? Guid = null;
        [JsonPropertyName("name")]
        public string? Name = null;
        [JsonPropertyName("properties")]
        public Property[]? Properties = null;
        [JsonPropertyName("texture")]
        public string? Skin = null;
        [JsonPropertyName("cape")]
        public string? Cape = null;
        [JsonPropertyName("elytra")]
        public string? Elytra = null;
        [JsonPropertyName("model")]
        public PlayerModelType? ModelType = null;
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