using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class KeybindChatComponent : ChatComponent
    {
        [JsonPropertyName("keybind")]
        public string Keybind;
        public KeybindChatComponent(string keybind = "") : base("keybind")
        {
            Keybind = keybind;
        }
    }
}