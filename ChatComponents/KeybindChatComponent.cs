using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed class KeybindChatComponent : ChatComponent
    {
        protected override string Type => "keybind";
        [JsonRequired]
        [JsonPropertyName("keybind")]
        public string Keybind { get; set; }
        public KeybindChatComponent(string keybind = "")
        {
            Keybind = keybind;
        }
    }
}