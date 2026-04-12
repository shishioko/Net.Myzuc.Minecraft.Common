using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Objects.SpawnConditions;
using Net.Myzuc.Minecraft.Common.Primitives;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Objects.Variants.Entities
{
    public sealed class WolfEntityVariant : IRegistryEntry, INbtSerializable<WolfEntityVariant>
    {
        public static Identifier RegistryId => "minecraft:wolf_variant";

        public WolfEntityVariantAssetInfo AdultAssets { get; set; } = new();
        public WolfEntityVariantAssetInfo? BabyAssets { get; set; } = null;
        public IList<SpawnConditionInfo>? SpawnCondition { get; set; } = null;

        public WolfEntityVariant()
        {
            
        }
        private WolfEntityVariant(CompoundNbtTag nbt)
        {
            AdultAssets = Nbt.Nbt.FromNbt<WolfEntityVariantAssetInfo>(nbt["assets"]);
            if (nbt.ContainsKey("baby_assets"))
            {
                BabyAssets = Nbt.Nbt.FromNbt<WolfEntityVariantAssetInfo>(nbt["baby_assets"]);
            }
            if (nbt.ContainsKey("spawn_conditions"))
            {
                SpawnCondition = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
            }
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["assets"] = Nbt.Nbt.ToNbt(AdultAssets);
            if (BabyAssets is not null)
            {
                nbt["baby_assets"] = Nbt.Nbt.ToNbt(BabyAssets);
            }
            if (SpawnCondition?.Count > 0)
            {
                nbt["spawn_conditions"] = new ListNbtTag(SpawnCondition.Select(Nbt.Nbt.ToNbt));
            }
            return nbt;
        }
        public static WolfEntityVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}