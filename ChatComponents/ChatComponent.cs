using System.Drawing;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    [JsonConverter(typeof(ChatComponentJsonConverter))]
    public abstract class ChatComponent
    {
        private readonly string Type;
        public IEnumerable<ChatComponent>? Children = null;
        public Color? Color = null;
        public string? Font = null;
        public bool? Bold = null;
        public bool? Italic = null;
        public bool? Underlined = null;
        public bool? Strikethrough = null;
        public bool? Obfuscated = null;
        public Color? ShadowColor = null;
        protected internal ChatComponent(string type)
        {
            Type = type;
        }

        internal virtual void Serialize(JsonObject json)
        {
            json["type"] = Type;
            if (Children is not null)
            {
                json["extra"] = new JsonArray(Children.Select(
                    child =>
                    {
                        JsonObject childJson = new();
                        child.Serialize(childJson);
                        return childJson;
                    }
                ).ToArray());
            }
            if (Color.HasValue) json["color"] = $"#{(Color.Value.ToArgb() & 0x00FFFFFF):X6}";
            if (Font is not null) json["font"] = Font;
            if (Bold.HasValue) json["bold"] = Bold.Value;
            if (Italic.HasValue) json["italic"] = Italic.Value;
            if (Underlined.HasValue) json["underlined"] = Underlined.Value;
            if (Strikethrough.HasValue) json["strikethrough"] = Strikethrough.Value;
            if (Obfuscated.HasValue) json["obfuscated"] = Obfuscated.Value;
            if (ShadowColor.HasValue) json["shadow_color"] = ShadowColor.Value.ToArgb();
            //todo: more stuff
        }
        /*internal static ChatComponent Deserialize(JsonElement json)
        {
            
        }*/
    }
}