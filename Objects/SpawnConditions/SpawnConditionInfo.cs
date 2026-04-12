using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Objects.SpawnConditions
{
    public sealed record SpawnConditionInfo : INbtSerializable<SpawnConditionInfo>
    {
        public SpawnCondition Condition { get; set; }
        public int Priority { get; set; } = 0;

        public SpawnConditionInfo(SpawnCondition condition)
        {
            Condition = condition;
        }
        public SpawnConditionInfo(SpawnCondition condition, int priority)
        {
            Condition = condition;
            Priority = priority;
        }
        
        internal SpawnConditionInfo(CompoundNbtTag nbt)
        {
            Priority = nbt["priority"].As<IntNbtTag>();
            Condition = Nbt.Nbt.FromNbt<SpawnCondition>(nbt["condition"]);
        }
        
        NbtTag INbtSerializable<SpawnConditionInfo>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["priority"] = (IntNbtTag)Priority;
            nbt["condition"] = Nbt.Nbt.ToNbt(Condition);
            return nbt;
        }
        static SpawnConditionInfo INbtSerializable<SpawnConditionInfo>.FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}