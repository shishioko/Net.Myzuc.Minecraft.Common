using System.Drawing;
using System.Globalization;
using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.Nbt;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents.NbtConverters
{
    internal sealed class ChatColorNbtConverter : NbtConverter<Color>
    {
        public override NbtTag? SerializeValue(Color data, NbtSerializerContext context)
        {
            return new StringNbtTag($"#{(data.ToArgb() & 0x00FFFFFF):X6}");
        }
        public override Color DeserializeValue(NbtTag? nbt, NbtSerializerContext context)
        {
            if (nbt is null) throw new SerializationException();
            string code = nbt.As<StringNbtTag>().Value;
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
    }
}