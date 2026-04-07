using System.Text.Json.Nodes;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public class TranslatableChatComponent : ChatComponent
    {
        public string TranslationKey;
        public IEnumerable<ChatComponent>? Arguments = null;
        public string? Fallback = null;
        public TranslatableChatComponent(string translationKey) : base("translatable")
        {
            TranslationKey = translationKey;
        }
        internal override void Serialize(JsonObject json)
        {
            json["translate"] = TranslationKey;
            if (Arguments is not null)
            {
                json["with"] = new JsonArray(Arguments.Select(
                    child =>
                    {
                        JsonObject childJson = new();
                        child.Serialize(childJson);
                        return childJson;
                    }
                ).ToArray());
            }
            if (Fallback is not null) json["fallback"] = Fallback;
            base.Serialize(json);
        }   
    }
}