using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Entities
{
    public sealed class PigEntityVariant : IRegistryEntry, INbtSerializable<PigEntityVariant>
    {
        public static Identifier RegistryId => "minecraft:pig_variant";

        public Identifier AdultTexture { get; set; } = new();
        public Identifier BabyTexture { get; set; } = new();
        public Identifier Model { get; set; } = new();
        public IList<SpawnConditionInfo> SpawnCondition { get; set; } = [];

        public PigEntityVariant()
        {
            
        }
        private PigEntityVariant(CompoundNbtTag nbt)
        {
            AdultTexture = nbt["asset_id"].As<StringNbtTag>().Value;
            BabyTexture = nbt["baby_asset_id"].As<StringNbtTag>().Value;
            Model = nbt["model"].As<StringNbtTag>().Value;
            SpawnCondition = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["asset_id"] = (StringNbtTag)(string)AdultTexture;
            nbt["baby_asset_id"] = (StringNbtTag)(string)BabyTexture;
            nbt["model"] = (StringNbtTag)(string)Model;
            nbt["spawn_conditions"] = new ListNbtTag(SpawnCondition.Select(Nbt.Nbt.ToNbt));
            return nbt;
        }
        public static PigEntityVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}