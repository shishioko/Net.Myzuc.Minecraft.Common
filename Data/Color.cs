using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Net.Myzuc.Minecraft.Common.Data
{
    public readonly struct Color
    {
        [IgnoreDataMember]
        public Color Opaque
        {
            get
            {
                return new(0, R, G, B);
            }
        }
        [IgnoreDataMember]
        public int Argb { get; }
        [IgnoreDataMember]
        public byte A
        {
            get
            {
                return (byte)(Argb >> 24);
            }
        }
        [IgnoreDataMember]
        public byte R
        {
            get
            {
                return (byte)(Argb >> 16);
            }
        }
        [IgnoreDataMember]
        public byte G
        {
            get
            {
                return (byte)(Argb >> 8);
            }
        }
        [IgnoreDataMember]
        public byte B
        {
            get
            {
                return (byte)Argb;
            }
        }
        [IgnoreDataMember]
        public System.Drawing.Color ColorValue
        {
            get
            {
                return System.Drawing.Color.FromArgb(Argb);
            }
        }
        [IgnoreDataMember]
        public ConsoleColor? LegacyColor
        {
            get
            {
                return Argb switch
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
        public string String
        {
            get
            {
                return A != 255 ? $"#{Argb:X6}" : $"#{Argb:X8}";
            }
        }
        [IgnoreDataMember]
        public string? LegacyString
        {
            get
            {
                return Argb switch
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
        public Color()
        {
            Argb = 0;
        }
        public Color(int argb)
        {
            Argb = argb;
        }
        public Color(byte a, byte r, byte g, byte b)
        {
            Argb = (a << 24) | (r << 16) | (g << 8) | b;
        }
        public Color(byte r, byte g, byte b)
        {
            Argb = (r << 16) | (g << 8) | b;
        }
        public Color(System.Drawing.Color color)
        {
            Argb = color.ToArgb();
        }
        public Color(ConsoleColor color)
        {
            Argb = color switch
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
        [JsonConstructor]
        public Color(string @string)
        {
            if (@string.StartsWith('#'))
            {
                Argb = int.Parse(@string[1..], NumberStyles.HexNumber);
            }
            else
            {
                Argb = @string switch
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
                    _ => throw new ArgumentException($"Unrecognized legacy color '{@string}'!", nameof(@string))
                };
            }
        }
        public override string ToString()
        {
            return String;
        }
        public static Color operator +(Color a)
        {
            return new(a.Argb);
        }
        public static Color operator -(Color a)
        {
            return new(~a.Argb);
        }
        public static Color operator +(Color a, Color b)
        {
            return new((byte)(a.A + b.A), (byte)(a.R + b.R), (byte)(a.G + b.G), (byte)(a.B + b.B));
        }
        public static Color operator -(Color a, Color b)
        {
            return new((byte)(a.A - b.A), (byte)(a.R - b.R), (byte)(a.G - b.G), (byte)(a.B - b.B));
        }
        public static Color operator *(Color a, Color b)
        {
            return new(
                (byte)((a.A / 255.0f) * (b.A / 255.0f) * 255.0f), 
                (byte)((a.R / 255.0f) * (b.R / 255.0f) * 255.0f), 
                (byte)((a.G / 255.0f) * (b.G / 255.0f) * 255.0f), 
                (byte)((a.B / 255.0f) * (b.B / 255.0f) * 255.0f)
            );
        }
    }
}