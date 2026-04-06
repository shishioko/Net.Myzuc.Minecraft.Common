using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Me.Shiokawaii.IO;
using Net.Myzuc.Minecraft.Common.Objects;

namespace Net.Myzuc.Minecraft.Common.IO
{
    internal static class StreamExtension
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Converters =
            {

            },
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,

            NumberHandling = JsonNumberHandling.Strict,

            IncludeFields = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = false,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
            AllowTrailingCommas = false,
            ReadCommentHandling = JsonCommentHandling.Disallow,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,

            WriteIndented = false,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow,
        };
        
        extension(Stream stream)
        {
            public async Task<string> ReadMinecraftStringAsync()
            {
                byte[] buffer = await stream.ReadU8AAsync(await stream.ReadS32VAsync());
                return Encoding.UTF8.GetString(buffer);
            }
            public string ReadMinecraftString()
            {
                byte[] buffer = stream.ReadU8A(stream.ReadS32V());
                return Encoding.UTF8.GetString(buffer);
            }
            public async Task WriteMinecraftStringAsync(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                await stream.WriteS32VAsync(buffer.Length);
                await stream.WriteU8AAsync(buffer);
            }
            public void WriteMinecraftString(string data)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.WriteS32V(buffer.Length);
                stream.WriteU8A(buffer);
            }
            public async Task<T> ReadMinecraftJsonAsync<T>()
            {
                return JsonSerializer.Deserialize<T>(await stream.ReadMinecraftStringAsync(), JsonSerializerOptions) ?? throw new NullReferenceException();
            }
            public T ReadMinecraftJson<T>()
            {
                return JsonSerializer.Deserialize<T>(stream.ReadMinecraftString(), JsonSerializerOptions) ?? throw new NullReferenceException();
            }
            public async Task WriteMinecraftJsonAsync<T>(T data)
            {
                await stream.WriteMinecraftStringAsync(JsonSerializer.Serialize(data, JsonSerializerOptions));
            }
            public void WriteMinecraftJson<T>(T data)
            {
                stream.WriteMinecraftString(JsonSerializer.Serialize(data, JsonSerializerOptions));
            }

            public void writeGameProfile(GameProfile gameProfile)
            {
                stream.WriteGuid(gameProfile.uuid);
                stream.WriteMinecraftString(gameProfile.username);

                stream.WriteS32V(gameProfile.properties.Length);
                
                foreach(GameProfileProperty property in gameProfile.properties)
                {
                    stream.WriteMinecraftString(property.name);
                    stream.WriteMinecraftString(property.value);
                    
                    stream.WriteBool(property.signature != null);
                    
                    if(property.signature != null) stream.WriteMinecraftString(property.signature);
                }
            }

            public GameProfile readGameProfile()
            {
                GameProfile profile = new GameProfile();

                profile.uuid = stream.ReadGuid();
                profile.username = stream.ReadMinecraftString();

                int len = stream.ReadS32V();

                profile.properties = new GameProfileProperty[16];

                for (int i = 0; i < len; ++i)
                {
                    GameProfileProperty property = new GameProfileProperty();

                    property.name = stream.ReadMinecraftString();
                    property.value = stream.ReadMinecraftString();

                    if (stream.ReadBool())
                    {
                        property.signature = stream.ReadMinecraftString();
                    }
                    else property.signature = null;

                    profile.properties[i] = property;
                }

                return profile;
            }
        }
    }
}