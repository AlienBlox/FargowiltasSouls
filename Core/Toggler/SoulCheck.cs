// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Toggler.SoulCheck
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Toggler
{
  public static class SoulCheck
  {
    public static Toggle GetToggle<T>(this Player player) where T : AccessoryEffect
    {
      return player.GetToggle((AccessoryEffect) ModContent.GetInstance<T>());
    }

    public static Toggle GetToggle(this Player player, AccessoryEffect effect)
    {
      Toggle toggle;
      return !player.FargoSouls().Toggler.Toggles.TryGetValue(effect, out toggle) ? (Toggle) null : toggle;
    }

    public static bool GetToggleValue<T>(this Player player) where T : AccessoryEffect
    {
      return player.GetToggleValue((AccessoryEffect) ModContent.GetInstance<T>());
    }

    public static bool GetToggleValue(this Player player, AccessoryEffect effect, bool skipChecks = false)
    {
      Toggle toggle = player.GetToggle(effect);
      return toggle != null && (skipChecks || (!effect.MinionEffect && !effect.ExtraAttackEffect || !player.FargoSouls().PrimeSoulActive) && (!player.FargoSouls().MutantPresence || effect.IgnoresMutantPresence)) && toggle.ToggleBool;
    }

    public static bool GetPlayerBoolValue(this Player player, AccessoryEffect effect)
    {
      return player.GetToggle(effect).ToggleBool;
    }

    public static void SetToggleValue<T>(this Player player, bool value) where T : AccessoryEffect
    {
      player.SetToggleValue((AccessoryEffect) ModContent.GetInstance<T>(), value);
    }

    public static void SetToggleValue(this Player player, AccessoryEffect effect, bool value)
    {
      if (player.FargoSouls().Toggler.Toggles.ContainsKey(effect))
        player.FargoSouls().Toggler.Toggles[effect].ToggleBool = value;
      else
        FargowiltasSouls.FargowiltasSouls.Instance.Logger.Warn((object) ("Expected toggle not found: " + effect.Name));
    }
  }
}
