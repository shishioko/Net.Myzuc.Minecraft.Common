using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public abstract class ObjectChatComponent : ChatComponent
    {
        [JsonInclude]
        [JsonPropertyName("object")] private string Object { get; }
        protected internal ObjectChatComponent(string @object) : base("object")
        {
            Object = @object;
        }
    }
}