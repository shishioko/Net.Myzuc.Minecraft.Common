using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract record ChatComponent : INbtSerializable<ChatComponent>
    {
        [JsonInclude]
        [JsonPropertyName("type")]
        protected abstract string Type { get; }
        [JsonPropertyName("extra")]
        public IList<ChatComponent>? Children { get; set; } = null;
        [JsonPropertyName("color")]
        public ChatColor? Color { get; set; } = null;
        [JsonPropertyName("font")]
        public string? Font { get; set; } = null;
        [JsonPropertyName("bold")]
        public bool? Bold { get; set; } = null;
        [JsonPropertyName("italic")]
        public bool? Italic { get; set; } = null;
        [JsonPropertyName("underlined")]
        public bool? Underlined { get; set; } = null;
        [JsonPropertyName("strikethrough")]
        public bool? Strikethrough { get; set; } = null;
        [JsonPropertyName("obfuscated")]
        public bool? Obfuscated { get; set; } = null;
        [JsonPropertyName("shadow_color")]
        public Color? ShadowColor { get; set; } = null;
        
        protected internal ChatComponent()
        {
            
        }
        protected internal ChatComponent(CompoundNbtTag nbt)
        {
            if (nbt.ContainsKey("extra"))
            {
                Children = nbt["extra"].Get<ListNbtTag>().Select(Nbt.Nbt.FromNbt<ChatComponent>).ToList();
            }
            if (nbt.ContainsKey("color")) Color = new(nbt["color"].Get<StringNbtTag>());
            if (nbt.ContainsKey("font")) Font = nbt["font"].Get<StringNbtTag>();
            if (nbt.ContainsKey("bold")) Bold = nbt["bold"].Get<ByteNbtTag>();
            if (nbt.ContainsKey("italic")) Italic = nbt["italic"].Get<ByteNbtTag>();
            if (nbt.ContainsKey("underlined")) Underlined = nbt["underlined"].Get<ByteNbtTag>();
            if (nbt.ContainsKey("strikethrough")) Strikethrough = nbt["strikethrough"].Get<ByteNbtTag>();
            if (nbt.ContainsKey("obfuscated")) Obfuscated = nbt["obfuscated"].Get<ByteNbtTag>();
            if (nbt.ContainsKey("shadow_color")) ShadowColor = new(nbt["shadow_color"].Get<StringNbtTag>());
        }
        
        protected virtual CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["type"] = (StringNbtTag)Type;
            if (Children is not null)
            {
                nbt["extra"] = new ListNbtTag(Children.Select(entry => entry.ToNbt()).ToList());
            }
            if (Color.HasValue) nbt["color"] = (StringNbtTag)Color.Value.ToString();
            if (Font is not null) nbt["font"] = (StringNbtTag)Font;
            if (Bold is not null) nbt["bold"] = (ByteNbtTag)Bold;
            if (Italic is not null) nbt["italic"] = (ByteNbtTag)Italic;
            if (Underlined is not null) nbt["underlined"] = (ByteNbtTag)Underlined;
            if (Strikethrough is not null) nbt["strikethrough"] = (ByteNbtTag)Strikethrough;
            if (Obfuscated is not null) nbt["obfuscated"] = (ByteNbtTag)Obfuscated;
            if (ShadowColor.HasValue) nbt["shadow_color"] = (IntNbtTag)ShadowColor.Value.Argb;
            return nbt;
        }
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, Global.JsonSerializerOptions);
        }
        
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
        public static implicit operator ChatComponent(string[] data)
        {
            return new TextChatComponent()
            {
                Children = data.Select<string, ChatComponent>(child => new TextChatComponent(child)).ToList(),
            };
        }
        public static implicit operator ChatComponent(ChatComponent[] data)
        {
            return new TextChatComponent()
            {
                Children = data.ToList(),
            };
        }
        public static implicit operator ChatComponent(List<ChatComponent> data)
        {
            return new TextChatComponent()
            {
                Children = data,
            };
        }
        
        NbtTag INbtSerializable<ChatComponent>.ToNbt()
        {
            return ToNbt();
        }
        static ChatComponent INbtSerializable<ChatComponent>.FromNbt(NbtTag nbt)
        {
            return nbt switch
            {
                ListNbtTag listNbt => new TextChatComponent()
                {
                    Children = listNbt.Select(Nbt.Nbt.FromNbt<ChatComponent>).ToList()
                },
                StringNbtTag stringNbt => new TextChatComponent(stringNbt.Value),
                _ => nbt.As<CompoundNbtTag>() switch
                {
                    var compoundNbt when compoundNbt.ContainsKey("type") => compoundNbt["type"].Get<StringNbtTag>().Value switch
                    {
                        "text" => new TextChatComponent(compoundNbt),
                        "translatable" => new TranslatableChatComponent(compoundNbt),
                        "keybind" => new KeybindChatComponent(compoundNbt),
                        "object" => ObjectChatComponent.FromNbt(compoundNbt),
                        _ => throw new SerializationException()
                    },
                    var compoundNbt when compoundNbt.ContainsKey("text") => new TextChatComponent(compoundNbt),
                    var compoundNbt when compoundNbt.ContainsKey("translate") => new TranslatableChatComponent(compoundNbt),
                    var compoundNbt when compoundNbt.ContainsKey("keybind") => new KeybindChatComponent(compoundNbt),
                    var compoundNbt when compoundNbt.ContainsKey("object") => ObjectChatComponent.FromNbt(compoundNbt),
                    var compoundNbt => new TextChatComponent(compoundNbt),
                },
            };
        }
    }
}