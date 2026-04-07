using System.Text.Json.Nodes;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public class KeybindChatComponent : ChatComponent
    {
        public string Keybind;
        public KeybindChatComponent(string keybind = "") : base("keybind")
        {
            Keybind = keybind;
        }
        internal override void Serialize(JsonObject json)
        {
            json["keybind"] = Keybind;
            base.Serialize(json);
        }
    }
}