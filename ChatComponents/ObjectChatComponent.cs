using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.ChatComponents.NbtConverters;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [NbtConverter(typeof(ObjectChatComponentNbtConverter))]
    [JsonConverter(typeof(ObjectChatComponentJsonConverter))]
    public abstract class ObjectChatComponent : ChatComponent
    {
        protected override string Type => "object";
        [JsonInclude] [NbtRequired]
        [JsonPropertyName("object")] [NbtProperty("object")]
        protected abstract string Object { get; }
        protected internal ObjectChatComponent()
        {
            
        }
    }
}