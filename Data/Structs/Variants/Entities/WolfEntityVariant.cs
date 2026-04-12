using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Entities
{
    public sealed class WolfEntityVariant : IRegistryEntry, INbtSerializable<WolfEntityVariant>
    {
        public static Identifier RegistryId => "minecraft:wolf_variant";

        public WolfEntityVariantAssetInfo AdultAssets { get; set; } = new();
        public WolfEntityVariantAssetInfo BabyAssets { get; set; } = new();
        public IList<SpawnConditionInfo> SpawnCondition { get; set; } = [];

        public WolfEntityVariant()
        {
            
        }
        private WolfEntityVariant(CompoundNbtTag nbt)
        {
            AdultAssets = Nbt.Nbt.FromNbt<WolfEntityVariantAssetInfo>(nbt["assets"]);
            BabyAssets = Nbt.Nbt.FromNbt<WolfEntityVariantAssetInfo>(nbt["baby_assets"]);
            SpawnCondition = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["assets"] = Nbt.Nbt.ToNbt(AdultAssets);
            nbt["baby_assets"] = Nbt.Nbt.ToNbt(BabyAssets);
            nbt["spawn_conditions"] = new ListNbtTag(SpawnCondition.Select(Nbt.Nbt.ToNbt));
            return nbt;
        }
        public static WolfEntityVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}