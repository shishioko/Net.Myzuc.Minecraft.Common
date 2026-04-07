using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Objects.JsonConverters
{
    internal class HexColorJsonSerializer : JsonConverter<Color>
    {
        public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string code = reader.GetString() ?? throw new NoNullAllowedException();
            if (code.StartsWith('#'))
            {
                code = code[1..];
                return Color.FromArgb((int)uint.Parse(code, NumberStyles.HexNumber));
            }
            return code switch
            {
                "black" => Color.FromArgb(255, 0, 0, 0),
                "dark_blue" => Color.FromArgb(255, 0, 0, 170),
                "dark_green" => Color.FromArgb(255, 0, 170, 0),
                "dark_aqua" => Color.FromArgb(255, 0, 170, 170),
                "dark_red" => Color.FromArgb(255, 170, 0, 0),
                "dark_purple" => Color.FromArgb(255, 170, 0, 170),
                "gold" => Color.FromArgb(255, 255, 170, 0),
                "gray" => Color.FromArgb(255, 170, 170, 170),
                "dark_gray" => Color.FromArgb(255, 85, 85, 85),
                "blue" => Color.FromArgb(255, 85, 85, 255),
                "green" => Color.FromArgb(255, 85, 255, 85),
                "aqua" => Color.FromArgb(255, 85, 255, 255),
                "red" => Color.FromArgb(255, 255, 85, 85),
                "light_purple" => Color.FromArgb(255, 255, 85, 255),
                "yellow" => Color.FromArgb(255, 255, 255, 85),
                "white" => Color.FromArgb(255, 255, 255, 255),
                _ => throw new ArgumentException($"Unknown Color \"{code}\"!")
            };
        }
        public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
        {
            writer.WriteStringValue($"#{(value.ToArgb() & 0x00FFFFFF):X6}");
        }
    }
}