using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

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

        internal TextChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Text = nbt["text"].Get<StringNbtTag>();
        }

        internal override CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = base.Serialize();
            nbt["text"] = (StringNbtTag)Text;
            return nbt;
        }
    }
}