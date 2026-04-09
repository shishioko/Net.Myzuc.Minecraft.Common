using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common
{
    internal static class Global
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            PreferredObjectCreationHandling = JsonObjectCreationHandling.Replace,

            NumberHandling = JsonNumberHandling.AllowReadingFromString,

            IncludeFields = true,
            IgnoreReadOnlyFields = false,
            IgnoreReadOnlyProperties = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = false,
            DictionaryKeyPolicy = JsonNamingPolicy.SnakeCaseLower,
            AllowTrailingCommas = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,

            WriteIndented = false,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip,
        };
    }
}