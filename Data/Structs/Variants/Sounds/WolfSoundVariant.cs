using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.Data.Structs.Variants.Sounds
{
    public sealed record WolfSoundVariant : INbtSerializable<WolfSoundVariant>
    {
        public WolfSoundVariantAssetInfo AdultAssets { get; set; } = new();
        public WolfSoundVariantAssetInfo BabyAssets { get; set; } = new();

        public WolfSoundVariant()
        {
            
        }
        private WolfSoundVariant(CompoundNbtTag nbt)
        {
            AdultAssets = Nbt.Nbt.FromNbt<WolfSoundVariantAssetInfo>(nbt["adult_sounds"]);
            BabyAssets = Nbt.Nbt.FromNbt<WolfSoundVariantAssetInfo>(nbt["baby_sounds"]);
        }
        
        public NbtTag ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["adult_sounds"] = Nbt.Nbt.ToNbt(AdultAssets);
            nbt["baby_sounds"] = Nbt.Nbt.ToNbt(BabyAssets);
            return nbt;
        }
        public static WolfSoundVariant FromNbt(NbtTag nbt)
        {
            return new(nbt.As<CompoundNbtTag>());
        }
    }
}