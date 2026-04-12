using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Entities
{
    public sealed class CowEntityVariant : IRegistryEntry, INbtSerializable<CowEntityVariant>
    {
        public static Identifier RegistryId => "minecraft:cow_variant";

        public Identifier AdultTexture { get; set; } = new();
        public Identifier? BabyTexture { get; set; } = null;
        public Identifier? Model { get; set; } = null;
        public IList<SpawnConditionInfo>? SpawnCondition { get; set; } = null;

        public CowEntityVariant()
        {
            
        }
        private CowEntityVariant(CompoundNbtTag nbt)
        {
            AdultTexture = nbt["asset_id"].As<StringNbtTag>().Value;
            if (nbt.ContainsKey("baby_asset_id"))
            {
                BabyTexture = nbt["baby_asset_id"].As<StringNbtTag>().Value;
            }
            if (nbt.ContainsKey("model"))
            {
                Model = nbt["model"].As<StringNbtTag>().Value;
            }
            if (nbt.ContainsKey("spawn_conditions"))
            {
                SpawnCondition = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
            }
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["asset_id"] = (StringNbtTag)(string)AdultTexture;
            if (BabyTexture.HasValue)
            {
                nbt["baby_asset_id"] = (StringNbtTag)(string)BabyTexture;
            }
            if (Model.HasValue)
            {
                nbt["model"] = (StringNbtTag)(string)Model;
            }
            if (SpawnCondition?.Count > 0)
            {
                nbt["spawn_conditions"] = new ListNbtTag(SpawnCondition.Select(Nbt.Nbt.ToNbt));
            }
            return nbt;
        }
        public static CowEntityVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}