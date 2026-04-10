using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
   
    public sealed class TextChatComponent : ChatComponent
    {
        protected override string Type => "text";
        [JsonRequired] [NbtRequired]
        [JsonPropertyName("text")] [NbtProperty("text")]
        public string Text { get; set; }
        public TextChatComponent(string text = "")
        {
            Text = text;
        }
    }
}