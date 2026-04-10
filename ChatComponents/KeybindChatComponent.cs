using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

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
        
        internal KeybindChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Keybind = nbt["keybind"].Get<StringNbtTag>();
        }

        internal override CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = base.Serialize();
            nbt["keybind"] = (StringNbtTag)Keybind;
            return nbt;
        }
    }
}