using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
   
    public sealed class TextChatComponent : ChatComponent
    {
        [JsonPropertyName("text")] public string Text { get; set; }
        public TextChatComponent(string text = "") : base("text")
        {
            Text = text;
        }
    }
}