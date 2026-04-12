using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Registries;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Entities
{
    public sealed class ZombieNautilusEntityVariant : IRegistryEntry, INbtSerializable<ZombieNautilusEntityVariant>
    {
        public static Identifier RegistryId => "minecraft:zombie_nautilus_variant";

        public Identifier Texture { get; set; } = new();
        public Identifier Model { get; set; } = new();
        public IList<SpawnConditionInfo> SpawnCondition { get; set; } = [];

        public ZombieNautilusEntityVariant()
        {
            
        }
        private ZombieNautilusEntityVariant(CompoundNbtTag nbt)
        {
            Texture = nbt["asset_id"].As<StringNbtTag>().Value;
            Model = nbt["model"].As<StringNbtTag>().Value;
            SpawnCondition = nbt["spawn_conditions"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<SpawnConditionInfo>).ToList();
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["asset_id"] = (StringNbtTag)(string)Texture;
            nbt["model"] = (StringNbtTag)(string)Model;
            nbt["spawn_conditions"] = new ListNbtTag(SpawnCondition.Select(Nbt.Nbt.ToNbt));
            return nbt;
        }
        public static ZombieNautilusEntityVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}