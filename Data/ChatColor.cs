using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.JsonConverters;

namespace Net.Myzuc.Minecraft.Common.Data
{
    [JsonConverter(typeof(ChatColorJsonConverter))]
    public readonly struct ChatColor
    {
        [IgnoreDataMember]
        public ChatColor Gray
        {
            get
            {
                byte rgb = (byte)((R + G + B) / 3);
                return new(rgb, rgb, rgb);
            }
        }
        public int Rgb { get; }
        [IgnoreDataMember]
        public byte R
        {
            get
            {
                return (byte)(Rgb >> 16);
            }
        }
        [IgnoreDataMember]
        public byte G
        {
            get
            {
                return (byte)(Rgb >> 8);
            }
        }
        [IgnoreDataMember]
        public byte B
        {
            get
            {
                return (byte)Rgb;
            }
        }
        [IgnoreDataMember]
        public ConsoleColor? LegacyColorValue
        {
            get
            {
                return Rgb switch
                {
                    -16777216 => ConsoleColor.Black,
                    -16777046 => ConsoleColor.DarkBlue,
                    -16733696 => ConsoleColor.DarkGreen,
                    -16733526 => ConsoleColor.DarkCyan,
                    -5636096 => ConsoleColor.DarkRed,
                    -5635926 => ConsoleColor.DarkMagenta,
                    -22016 => ConsoleColor.DarkYellow,
                    -5592406 => ConsoleColor.Gray,
                    -11184811 => ConsoleColor.DarkGray,
                    -11184641 => ConsoleColor.Blue,
                    -11141291 => ConsoleColor.Green,
                    -11141121 => ConsoleColor.Cyan,
                    -43691 => ConsoleColor.Red,
                    -43521 => ConsoleColor.Magenta,
                    -171 => ConsoleColor.Yellow,
                    -1 => ConsoleColor.White,
                    _ => null
                };
            }
        }
        [IgnoreDataMember]
        public string? LegacyStringValue
        {
            get
            {
                return Rgb switch
                {
                    -16777216 => "black",
                    -16777046 => "dark_blue",
                    -16733696 => "dark_green",
                    -16733526 => "dark_aqua",
                    -5636096 => "dark_red",
                    -5635926 => "dark_purple",
                    -22016 => "gold",
                    -5592406 => "gray",
                    -11184811 => "dark_gray",
                    -11184641 => "blue",
                    -11141291 => "green",
                    -11141121 => "aqua",
                    -43691 => "red",
                    -43521 => "light_purple",
                    -171 => "yellow",
                    -1 => "white",
                    _ => null
                };
            }
        }
        public ChatColor()
        {
            Rgb = 0;
        }
        public ChatColor(int rgb)
        {
            Rgb = rgb & 0x00FFFFFF;
        }
        public ChatColor(byte r, byte g, byte b)
        {
            Rgb = (r << 16) | (g << 8) | b;
        }
        public ChatColor(ConsoleColor color)
        {
            Rgb = color switch
            {
                ConsoleColor.Black => -16777216,
                ConsoleColor.DarkBlue => -16777046,
                ConsoleColor.DarkGreen => -16733696,
                ConsoleColor.DarkCyan => -16733526,
                ConsoleColor.DarkRed => -5636096,
                ConsoleColor.DarkMagenta => -5635926,
                ConsoleColor.DarkYellow => -22016,
                ConsoleColor.Gray => -5592406,
                ConsoleColor.DarkGray => -11184811,
                ConsoleColor.Blue => -11184641,
                ConsoleColor.Green => -11141291,
                ConsoleColor.Cyan => -11141121,
                ConsoleColor.Red => -43691,
                ConsoleColor.Magenta => -43521,
                ConsoleColor.Yellow => -171,
                ConsoleColor.White => -1,
                _ => throw new ArgumentException($"Unrecognized color '{color}'!", nameof(color))
            };
        }
        public ChatColor(string value)
        {
            if (value.StartsWith('#'))
            {
                int rgb = int.Parse(value[1..], NumberStyles.HexNumber);
                if ((rgb & ~0xFF000000) != 0x00000000) throw new ArgumentOutOfRangeException(nameof(rgb));
                Rgb = rgb;
            }
            else
            {
                Rgb = value switch
                {
                    "black" => -16777216,
                    "dark_blue" => -16777046,
                    "dark_green" => -16733696,
                    "dark_aqua" => -16733526,
                    "dark_red" => -5636096,
                    "dark_purple" => -5635926,
                    "gold" => -22016,
                    "gray" => -5592406,
                    "dark_gray" => -11184811,
                    "blue" => -11184641,
                    "green" => -11141291,
                    "aqua" => -11141121,
                    "red" => -43691,
                    "light_purple" => -43521,
                    "yellow" => -171,
                    "white" => -1,
                    _ => throw new ArgumentException($"Unrecognized legacy color '{value}'!", nameof(value))
                };
            }
        }
        public override string ToString()
        {
            return $"#{Rgb:X6}";
        }
        public static Color operator +(ChatColor a)
        {
            return new(a.Rgb);
        }
        public static ChatColor operator -(ChatColor a)
        {
            return new(~a.Rgb);
        }
        public static ChatColor operator +(ChatColor a, ChatColor b)
        {
            return new((byte)(a.R + b.R), (byte)(a.G + b.G), (byte)(a.B + b.B));
        }
        public static ChatColor operator -(ChatColor a, ChatColor b)
        {
            return new((byte)(a.R - b.R), (byte)(a.G - b.G), (byte)(a.B - b.B));
        }
        public static ChatColor operator *(ChatColor a, ChatColor b)
        {
            return new(
                (byte)((a.R / 255.0f) * (b.R / 255.0f) * 255.0f), 
                (byte)((a.G / 255.0f) * (b.G / 255.0f) * 255.0f), 
                (byte)((a.B / 255.0f) * (b.B / 255.0f) * 255.0f)
            );
        }
        public static ChatColor operator &(ChatColor a, ChatColor b)
        {
            return new(
                byte.Min(a.R, b.R), 
                byte.Min(a.G, b.G), 
                byte.Min(a.B, b.B)
            );
        }
        public static ChatColor operator |(ChatColor a, ChatColor b)
        {
            return new(
                byte.Max(a.R, b.R), 
                byte.Max(a.G, b.G), 
                byte.Max(a.B, b.B)
            );
        }
        public static ChatColor operator ^(ChatColor a, ChatColor b)
        {
            return new(
                (byte)((a.R + b.R) / 2), 
                (byte)((a.G + b.G) / 2), 
                (byte)((a.B + b.B) / 2)
            );
        }
        public static implicit operator System.Drawing.Color(ChatColor color)
        {
            return System.Drawing.Color.FromArgb(color.Rgb | ~0x00FFFFFF);
        }
        public static implicit operator ChatColor(System.Drawing.Color color)
        {
            return new(color.ToArgb() & 0x00FFFFFF);
        }
        public static implicit operator Color(ChatColor color)
        {
            return new(color.Rgb | ~0x00FFFFFF);
        }
        public static implicit operator ChatColor(Color color)
        {
            return new(color.Argb & 0x00FFFFFF);
        }
        public static implicit operator ChatColor(byte n)
        {
            return new(n, n, n);
        }
        public static implicit operator ChatColor(int n)
        {
            return new( (byte)n, (byte)n, (byte)n);
        }
    }
}