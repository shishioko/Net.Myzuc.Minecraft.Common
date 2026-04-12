using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Sounds
{
    public sealed record WolfSoundVariantAssetInfo : INbtSerializable<WolfSoundVariantAssetInfo>
    {
        public SoundEvent AmbientSound { get; set; } = new();
        public SoundEvent DeathSound { get; set; } = new();
        public SoundEvent GrowlSound { get; set; } = new();
        public SoundEvent HurtSound { get; set; } = new();
        public SoundEvent PantSound { get; set; } = new();
        public SoundEvent WhineSound { get; set; } = new();

        public WolfSoundVariantAssetInfo()
        {
            
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["ambient_sound"] = Nbt.Nbt.ToNbt(AmbientSound);
            nbt["death_sound"] = Nbt.Nbt.ToNbt(DeathSound);
            nbt["growl_sound"] = Nbt.Nbt.ToNbt(GrowlSound);
            nbt["hurt_sound"] = Nbt.Nbt.ToNbt(HurtSound);
            nbt["pant_sound"] = Nbt.Nbt.ToNbt(PantSound);
            nbt["whine_sound"] = Nbt.Nbt.ToNbt(WhineSound);
            return nbt;
        }
        public static WolfSoundVariantAssetInfo FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            WolfSoundVariantAssetInfo data = new();
            data.AmbientSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["ambient_sound"]);
            data.DeathSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["death_sound"]);
            data.GrowlSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["growl_sound"]);
            data.HurtSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["hurt_sound"]);
            data.PantSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["pant_sound"]);
            data.WhineSound = Nbt.Nbt.FromNbt<SoundEvent>(compoundNbt["whine_sound"]);
            return data;
        }
    }
}