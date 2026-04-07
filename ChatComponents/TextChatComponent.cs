using System.Text.Json.Nodes;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
   
    public sealed class TextChatComponent : ChatComponent
    {
        public string Text;
        public TextChatComponent(string text = "") : base("text")
        {
            Text = text;
        }
        internal override void Serialize(JsonObject json)
        {
            json["text"] = Text;
            base.Serialize(json);
        }
    }
}