using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
   
    public sealed record TextChatComponent : ChatComponent
    {
        protected override string Type => "text";
        [JsonRequired]
        [JsonPropertyName("text")]
        public string Text { get; init; }
        public TextChatComponent(string text = "")
        {
            Text = text;
        }
    }
}