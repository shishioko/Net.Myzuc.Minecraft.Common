using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Objects.ChatComponents
{
    public sealed record KeybindChatComponent : ChatComponent
    {
        protected override string Type => "keybind";
        [JsonRequired]
        [JsonPropertyName("keybind")]
        public string Keybind { get; set; } = "";
        
        public KeybindChatComponent(string keybind)
        {
            Keybind = keybind;
        }
        internal KeybindChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Keybind = nbt["keybind"].Get<StringNbtTag>();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["keybind"] = (StringNbtTag)Keybind;
            return nbt;
        }
    }
}