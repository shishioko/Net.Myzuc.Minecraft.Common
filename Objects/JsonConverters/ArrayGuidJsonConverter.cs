using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class ArrayGuidJsonConverter : JsonConverter<Guid>
    {
        public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
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