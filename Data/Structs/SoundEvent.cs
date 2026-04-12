using Net.Myzuc.Minecraft.Common.Data.Primitives;
using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs
{
    public sealed record SoundEvent : INbtSerializable<SoundEvent>
    {
        public Identifier Sound { get; set; } = new();
        public float? Range = null;

        public SoundEvent()
        {
            
        }
        public SoundEvent(Identifier sound)
        {
            Sound = sound;
        }
        public SoundEvent(Identifier sound, float range)
        {
            Sound = sound;
            Range = range;
        }
        NbtTag INbtSerializable<SoundEvent>.ToNbt()
        {
            if (!Range.HasValue)
            {
                return (StringNbtTag)(string)Sound;
            }
            else
            {
                CompoundNbtTag compoundNbt = new();
                compoundNbt["sound_id"] = (StringNbtTag)(string)Sound;
                compoundNbt["range"] = (FloatNbtTag)Range.Value;
                return compoundNbt;
            }
        }
        static SoundEvent INbtSerializable<SoundEvent>.FromNbt(NbtTag nbt)
        {
            if (nbt is StringNbtTag stringNbt)
            {
                return new(stringNbt.Value);
            }
            else
            {
                CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
                SoundEvent data = new();
                data.Sound = compoundNbt["sound_id"].As<StringNbtTag>().Value;
                if (compoundNbt.ContainsKey("range"))
                {
                    data.Range = compoundNbt["range"].As<FloatNbtTag>().Value;
                }
                return data;
            }
        }
    }
}