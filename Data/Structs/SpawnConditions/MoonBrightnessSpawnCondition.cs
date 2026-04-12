using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.SpawnConditions
{
    public sealed record MoonBrightnessSpawnCondition : SpawnCondition
    {
        public override string Type => "moon_brightness";

        public double Minimum = 0.0;
        public double Maximum = 0.0;
        
        public MoonBrightnessSpawnCondition()
        {
            
        }
        public MoonBrightnessSpawnCondition(double value)
        {
            Minimum = Maximum = value;
        }
        public MoonBrightnessSpawnCondition(double minimum, double maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
        
        internal MoonBrightnessSpawnCondition(CompoundNbtTag nbt) : base(nbt)
        {
            NbtTag range = nbt["range"];
            if (range is DoubleNbtTag doubleNbt)
            {
                Minimum = Maximum = doubleNbt.Value;
            }
            else
            {
                CompoundNbtTag compoundNbt = range.As<CompoundNbtTag>();
                Minimum = compoundNbt["min"].As<DoubleNbtTag>();
                Maximum = compoundNbt["max"].As<DoubleNbtTag>();
            }
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (Minimum == Maximum)
            {
                nbt["range"] = (DoubleNbtTag)Minimum;
            }
            else
            {
                nbt["range"] = new CompoundNbtTag()
                {
                    ["min"] = (DoubleNbtTag)Minimum,
                    ["max"] = (DoubleNbtTag)Maximum,
                };
            }
            return nbt;
        }
    }
}