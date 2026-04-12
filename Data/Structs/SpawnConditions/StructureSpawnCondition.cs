using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions
{
    public sealed record StructureSpawnCondition : SpawnCondition
    {
        protected override string Type => "structure";

        public IList<Identifier> Structures { get; set; } = [];
        
        public StructureSpawnCondition()
        {
            
        }
        public StructureSpawnCondition(IList<Identifier> structures)
        {
            Structures = structures;
        }
        
        internal StructureSpawnCondition(CompoundNbtTag nbt) : base(nbt)
        {
            Structures = nbt["structures"].As<ListNbtTag>().Select(Nbt.Nbt.FromNbt<Identifier>).ToList();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["structures"] = new ListNbtTag(Structures.Select(Nbt.Nbt.ToNbt));
            return nbt;
        }
    }
}