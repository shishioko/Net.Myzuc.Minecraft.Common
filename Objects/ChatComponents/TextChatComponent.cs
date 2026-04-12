using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Objects.ChatComponents
{
   
    public sealed record TextChatComponent : ChatComponent
    {
        protected override string Type => "text";
        [JsonRequired]
        [JsonPropertyName("text")]
        public string Text { get; set; } = "";
        
        public TextChatComponent()
        {
            
        }
        public TextChatComponent(string text)
        {
            Text = text;
        }
        internal TextChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Text = nbt["text"].Get<StringNbtTag>();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["text"] = (StringNbtTag)Text;
            return nbt;
        }
    }
}