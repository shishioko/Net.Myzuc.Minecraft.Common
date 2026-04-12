using System.Runtime.Serialization;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Objects.SpawnConditions
{
    public abstract record SpawnCondition : INbtSerializable<SpawnCondition>
    {
        protected abstract string Type { get; }

        internal SpawnCondition()
        {
            
        }
        internal SpawnCondition(CompoundNbtTag nbt)
        {
            
        }

        protected virtual CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["type"] = (StringNbtTag)Type;
            return nbt;
        }
        
        NbtTag INbtSerializable<SpawnCondition>.ToNbt()
        {
            return ToNbt();
        }
        static SpawnCondition INbtSerializable<SpawnCondition>.FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            return compoundNbt["type"].As<StringNbtTag>().Value switch
            {
                "biome" => new BiomeSpawnCondition(compoundNbt),
                "structure" => new StructureSpawnCondition(compoundNbt),
                "moon_brightness" => new MoonBrightnessSpawnCondition(compoundNbt),
                _ => throw new SerializationException()
            };
        }
    }
}