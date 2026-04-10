using System.Text.Json.Serialization;
using Net.Myzuc.Minecraft.Common.Data;
using Net.Myzuc.Minecraft.Common.Nbt.Tags;

namespace Net.Myzuc.Minecraft.Common.ChatComponents
{
    public sealed record PlayerObjectChatComponent : ObjectChatComponent
    {
        protected override string Object => "player";
        [JsonRequired]
        [JsonPropertyName("player")]
        public UnresolvedProfile Player { get; init; }
        [JsonPropertyName("hat")]
        public bool DisplayHat { get; init; } = true;
        public PlayerObjectChatComponent(UnresolvedProfile player)
        {
            Player = player;
        }
        
        internal PlayerObjectChatComponent(CompoundNbtTag nbt) : base(nbt)
        {
            Player = new(nbt["player"].Get<CompoundNbtTag>());
            DisplayHat = nbt["hat"].Get<ByteNbtTag>();
        }

        internal override CompoundNbtTag Serialize()
        {
            CompoundNbtTag nbt = base.Serialize();
            nbt["player"] = Player.Serialize();
            nbt["hat"] = (ByteNbtTag)DisplayHat;
            return nbt;
        }
    }
}