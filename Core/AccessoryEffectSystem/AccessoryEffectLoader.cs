// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.AccessoryEffectSystem.AccessoryEffectLoader
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.AccessoryEffectSystem
{
  public static class AccessoryEffectLoader
  {
    public static List<AccessoryEffect> AccessoryEffects = new List<AccessoryEffect>();

    internal static void Register(AccessoryEffect effect)
    {
      effect.Index = AccessoryEffectLoader.AccessoryEffects.Count;
      AccessoryEffectLoader.AccessoryEffects.Add(effect);
      ToggleLoader.RegisterToggle(new Toggle(effect, effect.Mod.Name));
    }

    public static bool AddEffect<T>(this Player player, Item item) where T : AccessoryEffect
    {
      AccessoryEffect instance = (AccessoryEffect) ModContent.GetInstance<T>();
      AccessoryEffectPlayer accessoryEffectPlayer = player.AccessoryEffects();
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      accessoryEffectPlayer.EquippedEffects[instance.Index] = true;
      accessoryEffectPlayer.EffectItems[instance.Index] = item;
      if (instance.MinionEffect || instance.ExtraAttackEffect)
      {
        if (instance.MinionEffect && fargoSoulsPlayer.Toggler_MinionsDisabled || instance.ExtraAttackEffect && fargoSoulsPlayer.Toggler_ExtraAttacksDisabled)
          return false;
        if (fargoSoulsPlayer.PrimeSoulActive)
        {
          if (!player.HasEffect(instance))
            ++fargoSoulsPlayer.PrimeSoulItemCount;
          return false;
        }
      }
      if (!instance.IgnoresMutantPresence && instance.HasToggle && fargoSoulsPlayer.MutantPresence)
        return false;
      if (instance.HasToggle)
      {
        SoulsItem soulsItem = item == null || !(item.ModItem is SoulsItem modItem) ? (SoulsItem) null : modItem;
        if (!player.GetToggleValue(instance, true))
        {
          if (soulsItem != null)
            soulsItem.HasDisabledEffects = SoulConfig.Instance.ItemDisabledTooltip;
          return false;
        }
        if (soulsItem != null)
          soulsItem.HasDisabledEffects = SoulConfig.Instance.ItemDisabledTooltip && AccessoryEffectLoader.AccessoryEffects.Any<AccessoryEffect>((Func<AccessoryEffect, bool>) (e => !player.GetToggleValue(e, true) && e.EffectItem(player) == item));
      }
      if (accessoryEffectPlayer.ActiveEffects[instance.Index])
        return false;
      accessoryEffectPlayer.ActiveEffects[instance.Index] = true;
      return true;
    }

    public static bool HasEffect<T>(this Player player) where T : AccessoryEffect
    {
      return player.HasEffect((AccessoryEffect) ModContent.GetInstance<T>());
    }

    public static bool HasEffect(this Player player, AccessoryEffect accessoryEffect)
    {
      return player.AccessoryEffects().ActiveEffects[accessoryEffect.Index];
    }

    public static Item EffectItem<T>(this Player player) where T : AccessoryEffect
    {
      return player.AccessoryEffects().EffectItems[ModContent.GetInstance<T>().Index];
    }

    public static IEntitySource GetSource_EffectItem<T>(this Player player) where T : AccessoryEffect
    {
      return ModContent.GetInstance<T>().GetSource_EffectItem(player);
    }

    public static T EffectType<T>() where T : AccessoryEffect => ModContent.GetInstance<T>();

    public static AccessoryEffect EffectType(string internalName)
    {
      return ModContent.Find<AccessoryEffect>(internalName);
    }
  }
}
