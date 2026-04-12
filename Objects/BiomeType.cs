using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Objects.Variants;
using Net.Myzuc.Minecraft.Common.Primitives;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record BiomeType : INbtSerializable<BiomeType>, IRegistryEntry
    {
        static Identifier IRegistryEntry.RegistryId => "minecraft:worldgen/biome";

        public bool Precipitation { get; set; } = false;
        public float Temperature { get; set; } = 0.0f;
        public string? TemperatureModifier { get; set; } = null;
        public float Downfall { get; set; } = 0.0f;
        public BiomeAmbientInformation Ambient { get; set; } = new();
        //todo: environment attributes
        //todo: carvers
        //todo: features
        public float? WorldgenMobSpawns { get; set; } = null;
        //todo: spawners
        //todo: spawn costs
        
        public BiomeType()
        {
            
        }
        
        NbtTag INbtSerializable<BiomeType>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["has_precipitation"] = (ByteNbtTag)Precipitation;
            nbt["temperature"] = (FloatNbtTag)Temperature;
            if (TemperatureModifier is not null)
            {
                nbt["temperature_modifier"] = (StringNbtTag)TemperatureModifier;
            }
            nbt["downfall"] = (FloatNbtTag)Downfall;
            nbt["effects"] = Nbt.Nbt.ToNbt(Ambient);
            //todo: environment attributes
            nbt["carvers"] = new ListNbtTag(); //todo: carvers
            nbt["features"] = new ListNbtTag(); //todo: features
            if (WorldgenMobSpawns is not null)
            {
                nbt["creature_spawn_probability"] = (FloatNbtTag)WorldgenMobSpawns;
            }
            nbt["spawners"] = new CompoundNbtTag(); //todo: spawners
            nbt["spawn_costs"] = new CompoundNbtTag(); //todo: spawn costs
            return nbt;
        }
        static BiomeType INbtSerializable<BiomeType>.FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            BiomeType data = new();
            data.Precipitation = compoundNbt["has_precipitation"].As<ByteNbtTag>();
            data.Temperature = compoundNbt["temperature"].As<FloatNbtTag>();
            if (compoundNbt.ContainsKey("temperature_modifier"))
            {
                data.TemperatureModifier = compoundNbt["temperature_modifier"].As<StringNbtTag>();
            }
            data.Downfall = compoundNbt["downfall"].As<FloatNbtTag>();
            data.Ambient = Nbt.Nbt.FromNbt<BiomeAmbientInformation>(compoundNbt["effects"]);
            //todo: environment attributes
            //todo: carvers
            //todo: features
            //WorldgenMobSpawns { get; set; } = null;
            if (compoundNbt.ContainsKey("creature_spawn_probability"))
            {
                data.WorldgenMobSpawns = compoundNbt["creature_spawn_probability"].As<FloatNbtTag>();
            }
            //todo: spawners
            //todo: spawn costs
            return data;
        }
    }
}