using System.Text.Json;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Objects.JsonConverters;

namespace Net.Myzuc.Minecraft.Common
{
    internal static class Global
    {
        public static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.General)
        {
            Converters =
            {
                new GuidStringJsonConverter(),
            },
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            ReadCommentHandling = JsonCommentHandling.Disallow,
            WriteIndented = false,
        };
    }
}