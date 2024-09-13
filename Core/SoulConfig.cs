// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.SoulConfig
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.ComponentModel;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ModLoader.Config;

#nullable disable
namespace FargowiltasSouls.Core
{
  internal class SoulConfig : ModConfig
  {
    public static SoulConfig Instance;
    private const string ModName = "FargowiltasSouls";
    [DefaultValue(true)]
    public bool HideTogglerWhenInventoryIsClosed;
    [DefaultValue(true)]
    public bool ItemDisabledTooltip;
    [DefaultValue(false)]
    public bool ToggleSearchReset;
    [DefaultValue(true)]
    public bool DeviChatter;
    [DefaultValue(false)]
    public bool BigTossMode;
    [DefaultValue(false)]
    public bool PerformanceMode;
    [DefaultValue(true)]
    public bool ForcedFilters;
    [Header("Maso")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool BossRecolors;
    [DefaultValue(true)]
    public bool PrecisionSealIsHold;
    private const float max4kX = 3840f;
    [Increment(1f)]
    [Range(0.0f, 3840f)]
    [DefaultValue(610f)]
    public float OncomingMutantX;
    private const float max4kY = 2160f;
    [Increment(1f)]
    [Range(0.0f, 2160f)]
    [DefaultValue(250f)]
    public float OncomingMutantY;
    [Header("Patreon")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonRoomba;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonOrb;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonFishingRod;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonDoor;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonWolf;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonDove;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonKingSlime;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonFishron;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonPlant;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonDevious;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonVortex;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonPrime;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonCrimetroid;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonNanoCore;
    [DefaultValue(true)]
    [ReloadRequired]
    public bool PatreonROB;

    public virtual void OnLoaded() => SoulConfig.Instance = this;

    public virtual ConfigScope Mode => (ConfigScope) 0;

    [OnDeserialized]
    internal void OnDeserializedMethod(StreamingContext context)
    {
      this.OncomingMutantX = Utils.Clamp<float>(this.OncomingMutantX, 0.0f, 3840f);
      this.OncomingMutantY = Utils.Clamp<float>(this.OncomingMutantY, 0.0f, 2160f);
    }
  }
}
