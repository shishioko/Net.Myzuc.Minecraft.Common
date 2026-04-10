using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.ChatComponents.JsonConverters;
using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract record ChatComponent
    {
        [JsonInclude]
        [JsonPropertyName("type")]
        protected abstract string Type { get; }
        [JsonPropertyName("extra")]
        public IReadOnlyList<ChatComponent>? Children { get; set; } = null;
        [JsonPropertyName("color")]
        public ChatColor? Color { get; init; } = null;
        [JsonPropertyName("font")]
        public string? Font { get; init; } = null;
        [JsonPropertyName("bold")]
        public bool? Bold { get; init; } = null;
        [JsonPropertyName("italic")]
        public bool? Italic { get; init; } = null;
        [JsonPropertyName("underlined")]
        public bool? Underlined { get; init; } = null;
        [JsonPropertyName("strikethrough")]
        public bool? Strikethrough { get; init; } = null;
        [JsonPropertyName("obfuscated")]
        public bool? Obfuscated { get; init; } = null;
        [JsonPropertyName("shadow_color")]
        public Color? ShadowColor { get; init; } = null;
        protected internal ChatComponent()
        {
            
        }
        
        internal ChatComponent(CompoundNbtTag nbt)
        {
            if (nbt.ContainsKey("extra"))
            {
                Children = nbt["extra"].Get<ListNbtTag>().Select(Deserialize).ToList();
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
        
        internal virtual CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = new();
            nbt["type"] = (StringNbtTag)Type;
            if (Children is not null)
            {
                nbt["extra"] = new ListNbtTag(Children.Select(entry => entry.Serialize()).ToList());
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

        internal static ChatComponent Deserialize(NbtTag? nbt)
        {
            return nbt switch
            {
                ListNbtTag listNbt => new TextChatComponent()
                {
                    Children = listNbt.Select(Deserialize).ToList()
                },
                StringNbtTag stringNbt => new TextChatComponent(stringNbt.Value),
                CompoundNbtTag compound => nbt switch
                {
                    _ when compound.ContainsKey("type") => compound["type"].Get<StringNbtTag>().Value switch
                    {
                        "text" => new TextChatComponent(compound),
                        "translatable" => new TranslatableChatComponent(compound),
                        "keybind" => new KeybindChatComponent(compound),
                        "object" => ObjectChatComponent.Deserialize(compound),
                        _ => throw new SerializationException()
                    },
                    _ when compound.ContainsKey("text") => new TextChatComponent(compound),
                    _ when compound.ContainsKey("translate") => new TranslatableChatComponent(compound),
                    _ when compound.ContainsKey("keybind") => new KeybindChatComponent(compound),
                    _ when compound.ContainsKey("object") => ObjectChatComponent.Deserialize(compound),
                    _ => new TextChatComponent(compound),
                },
                _ =>  throw new SerializationException(),
            };
        }
        
        public static implicit operator ChatComponent(string data)
        {
            return new TextChatComponent(data);
        }
        public static implicit operator ChatComponent(ChatComponent[] data)
        {
            return new TextChatComponent()
            {
                Children = data,
            };
        }
        public static implicit operator ChatComponent(string[] data)
        {
            return new TextChatComponent()
            {
                Children = data.Select<string, ChatComponent>(child => new TextChatComponent(child)).ToList(),
            };
        }
    }
}