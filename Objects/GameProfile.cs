using System.Text.Json.Serialization;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.IO;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public class GameProfile
    {
        [JsonPropertyName("id")]
        public Guid Guid = Guid.Empty;
        [JsonPropertyName("name")]
        public string Name = string.Empty;
        [JsonPropertyName("properties")]
        public Property[] Properties = [];
        public GameProfile()
        {
            
        }
        public GameProfile(Guid id, string name)
        {
            Guid = id;
            Name = name;
        }
        
        internal static void Serialize(Stream stream, GameProfile gameProfile)
        {
            stream.WriteGuid(gameProfile.Guid);
            stream.WriteMinecraftString(gameProfile.Name);
            stream.WriteS32V(gameProfile.Properties.Length);
            foreach(Property property in gameProfile.Properties)
            {
                stream.WriteMinecraftString(property.Name);
                stream.WriteMinecraftString(property.Value);
                stream.WriteBool(property.Signature is not null);
                if (property.Signature is not null)
                {
                    stream.WriteMinecraftString(property.Signature);
                }
            }
        }
        internal static GameProfile Deserialize(Stream stream)
        {
            GameProfile profile = new();
            profile.Guid = stream.ReadGuid();
            profile.Name = stream.ReadMinecraftString();
            profile.Properties = new Property[stream.ReadS32V()];
            for (int i = 0; i < profile.Properties.Length; i++)
            {
                Property property = new();
                property.Name = stream.ReadMinecraftString();
                property.Value = stream.ReadMinecraftString();
                if (stream.ReadBool())
                {
                    property.Signature = stream.ReadMinecraftString();
                }
                profile.Properties[i] = property;
            }
            return profile;
        }
    }
}
