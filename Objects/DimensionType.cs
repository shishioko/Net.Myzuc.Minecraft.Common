using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Primitives;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Objects
{
    public sealed record DimensionType : INbtSerializable<DimensionType>, IRegistryEntry
    {
        static Identifier IRegistryEntry.RegistryId => "minecraft:dimension_type";
        
        public double CoordinateScale { get; set; } = 1.0;
        public bool HasSkylight { get; set; } = false;
        public bool HasCeiling { get; set; } = false;
        public bool HasEnderdragon { get; set; } = false;
        public float AmbientLight { get; set; } = 0.0f;
        public bool? HasNoTime { get; set; } = null;
        public int MobSpawnBlocklightLimit { get; set; } = 15;
        public int MobSpawnLight { get; set; } = 15; //todo: implement integer providers
        public int LogicalHeight { get; set; } = 256;
        public int Depth { get; set; } = 0;
        public int Height { get; set; } = 256;
        public Identifier InfiniteBurnBlockTag { get; set; } = "#minecraft:infiniburn_overworld";
        public string? Skybox { get; set; } = null;
        public string? CardinalLight { get; set; } = null;
        //todo: attribute provider thingy
        public Identifier Clock { get; set; } = new();
        public IList<Identifier> Timelines { get; set; } = [ ];
        
        public DimensionType()
        {
            
        }
        NbtTag INbtSerializable<DimensionType>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["coordinate_scale"] = (DoubleNbtTag)CoordinateScale;
            nbt["has_skylight"] = (ByteNbtTag)HasSkylight;
            nbt["has_ceiling"] = (ByteNbtTag)HasCeiling;
            nbt["has_ender_dragon_fight"] = (ByteNbtTag)HasEnderdragon;
            nbt["ambient_light"] = (FloatNbtTag)AmbientLight;
            if (HasNoTime.HasValue)
            {
                nbt["has_fixed_time"] = (ByteNbtTag)HasNoTime.Value;
            }
            nbt["monster_spawn_block_light_limit"] = (IntNbtTag)MobSpawnBlocklightLimit;
            nbt["monster_spawn_light_level"] = (IntNbtTag)MobSpawnLight;
            nbt["logical_height"] = (IntNbtTag)LogicalHeight;
            nbt["min_y"] = (IntNbtTag)Depth;
            nbt["height"] = (IntNbtTag)Height;
            nbt["infiniburn"] = (StringNbtTag)(string)InfiniteBurnBlockTag;
            if (Skybox is not null)
            {
                nbt["skybox"] = (StringNbtTag)Skybox;
            }
            if (CardinalLight is not null)
            {
                nbt["cardinal_light"] = (StringNbtTag)CardinalLight;
            }
            nbt["attributes"] = new CompoundNbtTag(); //todo: attribues
            nbt["default_clock"] = (StringNbtTag)(string)Clock;
            if (Timelines.Count == 1)
            {
                nbt["timelines"] = (StringNbtTag)(string)Timelines.First();
            }
            else
            {
                nbt["timelines"] = new ListNbtTag(Timelines.Select(timeline => (StringNbtTag)(string)timeline));
            }
            return nbt;
        }
        static DimensionType INbtSerializable<DimensionType>.FromNbt(NbtTag nbt)
        {
            throw new NotImplementedException();
        }
    }
}