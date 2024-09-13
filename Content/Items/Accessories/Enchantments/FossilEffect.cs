// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.FossilEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class FossilEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) null;

    public override bool IgnoresMutantPresence => true;

    public override void OnHurt(Player player, Player.HurtInfo info)
    {
      player.immune = true;
      player.immuneTime = Math.Max(player.immuneTime, 60);
    }

    public static void FossilRevive(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.Eternity)
      {
        Revive(player.statLifeMax2 / 2 > 200 ? player.statLifeMax2 / 2 : 200, 10800);
        FargoSoulsUtil.XWay(30, ((Entity) player).GetSource_Misc("FossilEnchant"), ((Entity) player).Center, ModContent.ProjectileType<FossilBone>(), 15f, 0, 0.0f);
      }
      else if (fargoSoulsPlayer.TerrariaSoul)
      {
        Revive(200, 14400);
        FargoSoulsUtil.XWay(25, ((Entity) player).GetSource_Misc("FossilEnchant"), ((Entity) player).Center, ModContent.ProjectileType<FossilBone>(), 15f, 0, 0.0f);
      }
      else
      {
        int num = fargoSoulsPlayer.ForceEffect<FossilEnchant>() ? 1 : 0;
        Revive(num != 0 ? 200 : 50, 18000);
        FargoSoulsUtil.XWay(num != 0 ? 20 : 10, ((Entity) player).GetSource_Misc("FossilEnchant"), ((Entity) player).Center, ModContent.ProjectileType<FossilBone>(), 15f, 0, 0.0f);
      }

      void Revive(int healAmount, int reviveCooldown)
      {
        player.statLife = healAmount;
        player.HealEffect(healAmount, true);
        player.immune = true;
        player.immuneTime = 120;
        player.hurtCooldowns[0] = 120;
        player.hurtCooldowns[1] = 120;
        int length = player.buffType.Length;
        for (int index1 = 0; index1 < length; ++index1)
        {
          int num = player.buffTime[index1];
          if (num > 0)
          {
            int index2 = player.buffType[index1];
            if (index2 > 0 && num > 5 && Main.debuff[index2] && !Main.buffNoTimeDisplay[index2] && !BuffID.Sets.NurseCannotRemoveDebuff[index2])
            {
              player.DelBuff(index1);
              --index1;
              --length;
            }
          }
        }
        string textValue = Language.GetTextValue("Mods." + FargowiltasSouls.FargowiltasSouls.Instance.Name + ".Message.Revived");
        CombatText.NewText(((Entity) player).Hitbox, Color.SandyBrown, textValue, true, false);
        Main.NewText((object) textValue, new Color?(Color.SandyBrown));
        player.AddBuff(ModContent.BuffType<FossilReviveCDBuff>(), reviveCooldown, true, false);
      }
    }
  }
}
