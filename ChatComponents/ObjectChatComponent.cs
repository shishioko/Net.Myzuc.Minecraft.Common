using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ObjectChatComponentJsonConverter))]
    public abstract class ObjectChatComponent : ChatComponent
    {
        protected override string Type => "object";
        [JsonInclude]
        [JsonPropertyName("object")]
        protected abstract string Object { get; }
        protected internal ObjectChatComponent()
        {
            
        }
    }
}