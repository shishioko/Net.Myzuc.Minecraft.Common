using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.Primitives.JsonConverters;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Primitives
{
    [JsonConverter(typeof(ColorJsonConverter))]
    public readonly record struct Color : INbtSerializable<Color>
    {
        [IgnoreDataMember]
        public Color Opaque
        {
            get
            {
                return new(255, R, G, B);
            }
        }
        [IgnoreDataMember]
        public Color Transparent
        {
            get
            {
                return new(0, R, G, B);
            }
        }
        [IgnoreDataMember]
        public Color Gray
        {
            get
            {
                byte rgb = (byte)((R + G + B) / 3);
                return new(A, rgb, rgb, rgb);
            }
        }
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
            Argb = (255 << 24) | (r << 16) | (g << 8) | b;
        }
        public Color(string value)
        {
            if (value.StartsWith('#'))
            {
                Argb = int.Parse(value[1..], NumberStyles.HexNumber);
            }
            else
            {
                throw new ArgumentException($"Invalid color '{value}'!", nameof(value));
            }
        }
        public override string ToString()
        {
            return $"#{Argb:X8}";
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
        public static Color operator &(Color a, Color b)
        {
            return new(
                byte.Min(a.A, b.A), 
                byte.Min(a.R, b.R), 
                byte.Min(a.G, b.G), 
                byte.Min(a.B, b.B)
            );
        }
        public static Color operator |(Color a, Color b)
        {
            return new(
                byte.Max(a.A, b.A), 
                byte.Max(a.R, b.R), 
                byte.Max(a.G, b.G), 
                byte.Max(a.B, b.B)
            );
        }
        public static Color operator ^(Color a, Color b)
        {
            return new(
                (byte)((a.A + b.A) / 2), 
                (byte)((a.R + b.R) / 2), 
                (byte)((a.G + b.G) / 2), 
                (byte)((a.B + b.B) / 2)
            );
        }
        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.Argb);
        }
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new(color.ToArgb());
        }
        public static implicit operator Color(byte n)
        {
            return new(n, n, n, n);
        }
        public static implicit operator Color(int n)
        {
            return new((byte)n, (byte)n, (byte)n, (byte)n);
        }
        
        static Color INbtSerializable<Color>.FromNbt(NbtTag nbt)
        {
            if (nbt is not IntNbtTag intNbt) throw new SerializationException();
            return new(intNbt);
        }
        NbtTag INbtSerializable<Color>.ToNbt()
        {
            return (IntNbtTag)Argb;
        }
    }
}