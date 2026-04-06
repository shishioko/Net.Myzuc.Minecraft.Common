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
        }
    }
}