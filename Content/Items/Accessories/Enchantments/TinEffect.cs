// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.TinEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class TinEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<TinEnchant>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.Eternity)
      {
        if ((double) fargoSoulsPlayer.TinEternityDamage > 47.5)
          fargoSoulsPlayer.TinEternityDamage = 47.5f;
        if (player.HasEffect<EternityTin>())
        {
          ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
          local = StatModifier.op_Addition(local, fargoSoulsPlayer.TinEternityDamage);
          Player player1 = player;
          player1.statDefense = Player.DefenseStat.op_Addition(player1.statDefense, (int) ((double) fargoSoulsPlayer.TinEternityDamage * 100.0));
        }
      }
      if (fargoSoulsPlayer.TinProcCD <= 0)
        return;
      --fargoSoulsPlayer.TinProcCD;
    }

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      TinEffect.TinOnHitEnemy(player, hitInfo);
    }

    public static void TinOnHitEnemy(Player player, NPC.HitInfo hitInfo)
    {
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (hitInfo.Crit)
        modPlayer.TinCritBuffered = true;
      if (!modPlayer.TinCritBuffered || modPlayer.TinProcCD > 0)
        return;
      modPlayer.TinCritBuffered = false;
      modPlayer.TinCrit += 5f;
      if ((double) modPlayer.TinCrit > (double) modPlayer.TinCritMax)
        modPlayer.TinCrit = modPlayer.TinCritMax;
      else
        CombatText.NewText(((Entity) modPlayer.Player).Hitbox, Color.Yellow, Language.GetTextValue("Mods.FargowiltasSouls.Items.TinEnchant.CritUp"), false, false);
      if (modPlayer.Eternity)
      {
        modPlayer.TinProcCD = 1;
        TryHeal(10, 1);
        modPlayer.TinEternityDamage += 0.05f;
      }
      else if (modPlayer.TerrariaSoul)
      {
        modPlayer.TinProcCD = 15;
        TryHeal(25, 10);
      }
      else if (modPlayer.ForceEffect<TinEnchant>())
        modPlayer.TinProcCD = 30;
      else
        modPlayer.TinProcCD = 60;

      void TryHeal(int healDenominator, int healCooldown)
      {
        int num = ((NPC.HitInfo) ref hitInfo).Damage / healDenominator;
        if ((double) modPlayer.TinCrit < 100.0 || modPlayer.HealTimer > 0 || modPlayer.Player.moonLeech || modPlayer.MutantNibble || num <= 0)
          return;
        modPlayer.HealTimer = healCooldown;
        modPlayer.Player.statLife = Math.Min(modPlayer.Player.statLife + num, modPlayer.Player.statLifeMax2);
        modPlayer.Player.HealEffect(num, true);
      }
    }

    public override void OnHurt(Player player, Player.HurtInfo info) => TinEffect.TinHurt(player);

    public static void TinHurt(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      double tinCrit1 = (double) fargoSoulsPlayer.TinCrit;
      if (fargoSoulsPlayer.Eternity)
      {
        fargoSoulsPlayer.TinCrit = 50f;
        fargoSoulsPlayer.TinEternityDamage = 0.0f;
      }
      else
        fargoSoulsPlayer.TinCrit = !fargoSoulsPlayer.TerrariaSoul ? (!fargoSoulsPlayer.ForceEffect<TinEnchant>() ? 5f : 10f) : 20f;
      double tinCrit2 = (double) fargoSoulsPlayer.TinCrit;
      double num = Math.Round(tinCrit1 - tinCrit2, 1);
      if (num <= 0.0)
        return;
      CombatText.NewText(((Entity) fargoSoulsPlayer.Player).Hitbox, Color.OrangeRed, Language.GetTextValue("Mods.FargowiltasSouls.Items.TinEnchant.CritReset", (object) num), true, false);
    }

    public override void PostUpdateMiscEffects(Player player) => TinEffect.TinPostUpdate(player);

    public static void TinPostUpdate(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.TinCritMax = Math.Max(FargoSoulsUtil.HighestCritChance(fargoSoulsPlayer.Player) * 2f, fargoSoulsPlayer.ForceEffect<TinEnchant>() ? 50f : 15f);
      if ((double) fargoSoulsPlayer.TinCritMax > 100.0)
        fargoSoulsPlayer.TinCritMax = 100f;
      FargoSoulsUtil.AllCritEquals(fargoSoulsPlayer.Player, fargoSoulsPlayer.TinCrit);
    }
  }
}
