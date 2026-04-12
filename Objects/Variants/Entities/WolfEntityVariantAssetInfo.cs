using Net.Myzuc.Minecraft.Common.IO;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;
using Net.Myzuc.Minecraft.Common.Primitives;

namespace Net.Myzuc.Minecraft.Common.Objects.Variants.Entities
{
    public sealed record WolfEntityVariantAssetInfo : INbtSerializable<WolfEntityVariantAssetInfo>
    {
        public Identifier AngryTexture { get; set; } = new();
        public Identifier WildTexture { get; set; } = new();
        public Identifier TameTexture { get; set; } = new();

        public WolfEntityVariantAssetInfo()
        {
            
        }
        NbtTag INbtSerializable<WolfEntityVariantAssetInfo>.ToNbt()
        {
            CompoundNbtTag nbt = new();
            nbt["angry"] = (StringNbtTag)(string)AngryTexture;
            nbt["wild"] = (StringNbtTag)(string)WildTexture;
            nbt["tame"] = (StringNbtTag)(string)TameTexture;
            return nbt;
        }
        static WolfEntityVariantAssetInfo INbtSerializable<WolfEntityVariantAssetInfo>.FromNbt(NbtTag nbt)
        {
            CompoundNbtTag compoundNbt = nbt.As<CompoundNbtTag>();
            WolfEntityVariantAssetInfo data = new();
            data.AngryTexture = compoundNbt["angry"].As<StringNbtTag>().Value;
            data.WildTexture = compoundNbt["wild"].As<StringNbtTag>().Value;
            data.TameTexture = compoundNbt["tame"].As<StringNbtTag>().Value;
            return data;
        }
    }
}