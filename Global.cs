using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common
{
    internal static class Global
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            Converters =
            {
                new GuidJsonConverter()
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
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        };
    }
}