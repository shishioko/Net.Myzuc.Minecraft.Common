using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class GuidNbtJsonConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            int[] data = JsonSerializer.Deserialize<int[]>(ref reader, options) ?? throw new InvalidDataException();
            ReadOnlySpan<byte> span = MemoryMarshal.Cast<int, byte>(data);
            return new(span);
        }

        public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            ReadOnlySpan<int> span = MemoryMarshal.Cast<byte, int>(value.ToByteArray(true));
            foreach (int number in span)
            {
                writer.WriteNumberValue(number);
            }
            writer.WriteEndArray();
        }
    }
}