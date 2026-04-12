using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record BiomeAmbientInformation : INbtSerializable<BiomeAmbientInformation>
    {
        public Color WaterColor { get; set; }= 4159204;
        public Color? FoliageColor{ get; set; } = null;
        public Color? DryFoliageColor{ get; set; } = null;
        public Color? GrassColor{ get; set; } = null;
        public string? GrassColorModifier{ get; set; } = null;
        
        public BiomeAmbientInformation()
        {
            
        }
        
        NbtTag INbtSerializable<BiomeAmbientInformation>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["water_color"] = (IntNbtTag)WaterColor.Argb;
            if (FoliageColor.HasValue)
            {
                nbt["foliage_color"] = (IntNbtTag)FoliageColor.Value.Argb;
            }
            if (DryFoliageColor.HasValue)
            {
                nbt["dry_foliage_color"] = (IntNbtTag)DryFoliageColor.Value.Argb;
            }
            if (GrassColor.HasValue)
            {
                nbt["grass_color"] = (IntNbtTag)GrassColor.Value.Argb;
            }
            if (GrassColorModifier is not null)
            {
                nbt["grass_color_modifier"] = (StringNbtTag)GrassColorModifier;
            }
            return nbt;
        }
        static BiomeAmbientInformation INbtSerializable<BiomeAmbientInformation>.FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            BiomeAmbientInformation data = new();
            data.WaterColor = (int)compoundNbt["water_color"].As<IntNbtTag>();
            if (compoundNbt.ContainsKey("foliage_color"))
            {
                data.FoliageColor = (int)compoundNbt["foliage_color"].As<IntNbtTag>();
            }
            if (compoundNbt.ContainsKey("dry_foliage_color"))
            {
                data.DryFoliageColor = (int)compoundNbt["dry_foliage_color"].As<IntNbtTag>();
            }
            if (compoundNbt.ContainsKey("grass_color"))
            {
                data.GrassColor = (int)compoundNbt["grass_color"].As<IntNbtTag>();
            }
            if (compoundNbt.ContainsKey("grass_color_modifier"))
            {
                data.GrassColorModifier = (string)compoundNbt["grass_color_modifier"].As<StringNbtTag>();
            }
            return data;
        }
    }
}