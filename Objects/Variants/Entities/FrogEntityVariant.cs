using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Objects.SpawnConditions;
using Net.Myzuc.Minecraft.Common.Primitives;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Objects.Variants.Entities
{
    public sealed class FrogEntityVariant : IRegistryEntry, INbtSerializable<FrogEntityVariant>
    {
        static Identifier IRegistryEntry.RegistryId => "minecraft:frog_variant";

        public Identifier Texture { get; set; } = new();
        public IList<SpawnConditionInfo>? SpawnConditions { get; set; } = null;

        public FrogEntityVariant()
        {
            
        }
        private FrogEntityVariant(CompoundNbtTag nbt)
        {
            Texture = nbt["asset_id"].As<StringNbtTag>().Value;
            if (nbt.ContainsKey("spawn_conditions"))
            {
                SpawnConditions = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
            }
        }
        NbtTag INbtSerializable<FrogEntityVariant>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["asset_id"] = (StringNbtTag)(string)Texture;
            if (SpawnConditions?.Count > 0)
            {
                nbt["spawn_conditions"] = new ListNbtTag(SpawnConditions.Select(Nbt.Nbt.ToNbt));
            }
            return nbt;
        }
        static FrogEntityVariant INbtSerializable<FrogEntityVariant>.FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}