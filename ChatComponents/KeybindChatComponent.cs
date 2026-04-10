using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record KeybindChatComponent : ChatComponent
    {
        protected override string Type => "keybind";
        [JsonRequired]
        [JsonPropertyName("keybind")]
        public string Keybind { get; init; }
        public KeybindChatComponent(string keybind = "")
        {
            Keybind = keybind;
        }
    }
}