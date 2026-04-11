using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data.Structs;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record PlayerObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "player";
        [JsonRequired]
        [JsonPropertyName("player")]
        public UnresolvedProfile Player { get; set; } = new();
        [JsonPropertyName("hat")]
        public bool DisplayHat { get; set; } = true;
        
        public PlayerObjectChatComponent(UnresolvedProfile player)
        {
            Player = player;
        }
        internal PlayerObjectChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Player = Nbt.Nbt.FromNbt<UnresolvedProfile>(nbt["player"]);
            DisplayHat = nbt["hat"].Get<ByteNbtTag>();
        }
        
        protected override CompoundNbtTag ToNbt()
        {
            CompoundNbtTag nbt = base.ToNbt();
            nbt["player"] = Nbt.Nbt.ToNbt(Player);
            nbt["hat"] = (ByteNbtTag)DisplayHat;
            return nbt;
        }
    }
}