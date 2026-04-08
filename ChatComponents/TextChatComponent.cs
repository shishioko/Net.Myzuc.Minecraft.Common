using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
   
    public sealed class TextChatComponent : ChatComponent
    {
        protected override string Type => "text";
        [JsonRequired]
        [JsonPropertyName("text")] public string Text { get; set; }
        public TextChatComponent(string text = "")
        {
            Text = text;
        }
    }
}