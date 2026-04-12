using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Objects.SpawnConditions
{
    public sealed record BiomeSpawnCondition : SpawnCondition
    {
        protected override string Type => "biome";

        public IList<Identifier> Biomes { get; set; } = [];
        
        public BiomeSpawnCondition()
        {
            
        }
        public BiomeSpawnCondition(IList<Identifier> biomes)
        {
            Biomes = biomes;
        }
        
        internal BiomeSpawnCondition(CompoundNbtTag nbt) : base(nbt)
        {
            Biomes = nbt["biomes"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<Identifier>).ToList();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["biomes"] = new ListNbtTag(Biomes.Select(Nbt.Nbt.ToNbt));
            return nbt;
        }
    }
}