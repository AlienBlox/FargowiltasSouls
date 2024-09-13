// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.FargoGlobalItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class FargoGlobalItem : GlobalItem
  {
    public static List<int> TungstenAlwaysAffects;
    private static int infiniteLoopHackFix;

    public virtual void SetDefaults(Item item)
    {
      if (item.type != 27 && item.type != 154)
        return;
      item.ammo = item.type;
    }

    public virtual void UpdateAccessory(Item item, Player player, bool hideVisual)
    {
      if ((double) player.manaCost > 0.0)
        return;
      player.manaCost = 0.0f;
    }

    public virtual void ModifyManaCost(Item item, Player player, ref float reduce, ref float mult)
    {
    }

    public virtual void GrabRange(Item item, Player player, ref int grabRange)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI != Main.myPlayer || !player.HasEffect<IronEffect>() || item.type == 71 || item.type == 72 || item.type == 73 || item.type == 74 || item.type == 1734 || item.type == 1735 || item.type == 184 || item.type == 1867 || item.type == 1868 || item.type == 58)
        return;
      int num = 160;
      if (fargoSoulsPlayer.ForceEffect<IronEnchant>())
        num = 320;
      if (fargoSoulsPlayer.TerrariaSoul)
        num = 640;
      grabRange += num;
    }

    public virtual bool OnPickup(Item item, Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (((Entity) player).whoAmI == Main.myPlayer && player.HasEffect<GoldToPiggy>())
        fargoSoulsPlayer.GoldEnchMoveCoins = true;
      return base.OnPickup(item, player);
    }

    public virtual void PickAmmo(
      Item weapon,
      Item ammo,
      Player player,
      ref int type,
      ref float speed,
      ref StatModifier damage,
      ref float knockback)
    {
      if (weapon.CountsAsClass(DamageClass.Ranged) && player.FargoSouls().Jammed)
        type = 178;
      if (weapon.type != 905)
        return;
      if (ammo.type == 71 || ammo.type == ModContent.Find<ModItem>("Fargowiltas", "CopperCoinBag").Type)
        type = 158;
      if (ammo.type == 72 || ammo.type == ModContent.Find<ModItem>("Fargowiltas", "SilverCoinBag").Type)
        type = 159;
      if (ammo.type == 73 || ammo.type == ModContent.Find<ModItem>("Fargowiltas", "GoldCoinBag").Type)
        type = 160;
      if (ammo.type != 74 && ammo.type != ModContent.Find<ModItem>("Fargowiltas", "PlatinumCoinBag").Type)
        return;
      type = 161;
    }

    public virtual void OnConsumeItem(Item item, Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (item.healLife <= 0)
        return;
      if (player.HasEffect<HallowEffect>())
      {
        fargoSoulsPlayer.HallowHealTime = 6 * fargoSoulsPlayer.GetHealMultiplier(item.healLife);
        HallowEffect.HealRepel(player);
      }
      fargoSoulsPlayer.StatLifePrevious += fargoSoulsPlayer.GetHealMultiplier(item.healLife);
    }

    public virtual bool ConsumeItem(Item item, Player player)
    {
      return (!player.FargoSouls().BuilderMode || item.createTile <= 0 && item.createWall <= 0) && base.ConsumeItem(item, player);
    }

    public virtual void ModifyItemScale(Item item, Player player, ref float scale)
    {
      FargoSoulsPlayer modPlayer = player.FargoSouls();
      if (!player.HasEffect<TungstenEffect>() || item.IsAir || (!item.IsWeapon() || item.noMelee) && !FargoGlobalItem.TungstenAlwaysAffects.Contains(item.type))
        return;
      scale *= TungstenEffect.TungstenIncreaseWeaponSize(modPlayer);
    }

    public virtual void ModifyWeaponKnockback(Item item, Player player, ref StatModifier knockback)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!fargoSoulsPlayer.UniverseSoul && !fargoSoulsPlayer.Eternity)
        return;
      knockback = StatModifier.op_Multiply(knockback, 2f);
    }

    public virtual bool? CanAutoReuseItem(Item item, Player player)
    {
      if (item.ModItem != null && item.ModItem.CanAutoReuseItem(player).HasValue)
        return item.ModItem.CanAutoReuseItem(player);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.Berserked)
        return new bool?(true);
      return fargoSoulsPlayer.BoxofGizmos && item.DamageType == DamageClass.Default && item.damage <= 0 ? new bool?(true) : base.CanAutoReuseItem(item, player);
    }

    public virtual bool CanUseItem(Item item, Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.NoUsingItems > 0)
        return false;
      if (player.HasBuff(ModContent.BuffType<GoldenStasisBuff>()))
      {
        if (item.type != 1326)
          return false;
        player.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
      }
      if (item.CountsAsClass(DamageClass.Magic) && player.FargoSouls().ReverseManaFlow)
      {
        int num = (int) ((double) item.mana / (1.0 - (double) player.endurance) + (double) Player.DefenseStat.op_Implicit(player.statDefense));
        player.Hurt(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.ReverseManaFlow", (object) player.name)), num, 0, false, false, -1, true, 0.0f, 0.0f, 4.5f);
        player.immune = false;
        player.immuneTime = 0;
      }
      if (fargoSoulsPlayer.BuilderMode && (item.createTile != -1 || item.createWall != -1) && item.type != 74 && item.type != 73)
      {
        item.useTime = 1;
        item.useAnimation = 1;
      }
      if (item.IsWeapon() && player.HasAmmo(item) && (item.mana <= 0 || player.statMana >= item.mana) && item.type != 1338 && item.type != 929 && item.useTime > 0 && item.createTile == -1 && item.createWall == -1 && item.ammo == AmmoID.None)
      {
        fargoSoulsPlayer.TryAdditionalAttacks(item.damage, item.DamageType);
        player.AccessoryEffects().TryAdditionalAttacks(item.damage, item.DamageType);
      }
      if (item.type == 1326 && player.chaosState)
        player.FargoSouls().WasHurtBySomething = true;
      if (item.IsWeaponWithDamageClass())
        player.FargoSouls().WeaponUseTimer = 2 + (int) Math.Round((double) (Math.Max(item.useTime, item.useAnimation) + item.reuseDelay) / (double) player.FargoSouls().AttackSpeed);
      return true;
    }

    public virtual bool? UseItem(Item item, Player player)
    {
      if (item.type == 1326)
      {
        player.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
        if (player.FargoSouls().CrystalEnchantActive)
          player.AddBuff(ModContent.BuffType<FirstStrikeBuff>(), 60, true, false);
      }
      return base.UseItem(item, player);
    }

    public virtual void HoldItem(Item item, Player player)
    {
      if (item.type == 1299 && NPC.AnyNPCs(637))
      {
        for (int index1 = 0; index1 < Main.maxNPCs; ++index1)
        {
          if (((Entity) Main.npc[index1]).active && Main.npc[index1].type == 637)
          {
            NPC npc = Main.npc[index1];
            for (int index2 = 0; index2 < Main.maxItems; ++index2)
            {
              if (((Entity) Main.item[index2]).active && Main.item[index2].type == 3124)
              {
                double num1 = (double) ((Entity) npc).Distance(((Entity) Main.item[index2]).Center);
                Vector2 size = ((Entity) npc).Size;
                double num2 = (double) ((Vector2) ref size).Length();
                if (num1 < num2)
                {
                  double num3 = (double) Utils.Distance(Main.MouseWorld, ((Entity) npc).Center);
                  size = ((Entity) npc).Size;
                  double num4 = (double) ((Vector2) ref size).Length();
                  if (num3 < num4)
                  {
                    Item.NewItem(player.GetSource_ItemUse(item, (string) null), ((Entity) npc).Center, ModContent.ItemType<WiresPainting>(), 1, false, 0, false, false);
                    ((Entity) Main.item[index2]).active = false;
                    ((Entity) npc).active = false;
                    return;
                  }
                }
              }
            }
          }
        }
      }
      base.HoldItem(item, player);
    }

    public virtual void ModifyShootStats(
      Item item,
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.Eternity)
      {
        velocity = Vector2.op_Multiply(velocity, 2f);
      }
      else
      {
        if (!fargoSoulsPlayer.UniverseSoul)
          return;
        velocity = Vector2.op_Multiply(velocity, 1.5f);
      }
    }

    public virtual bool WingUpdate(int wings, Player player, bool inUse)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.HasEffect<JungleJump>() & inUse)
      {
        fargoSoulsPlayer.CanJungleJump = false;
        if (fargoSoulsPlayer.JungleCD == 0)
        {
          int num = 1;
          if (fargoSoulsPlayer.ChlorophyteEnchantActive)
            ++num;
          if (fargoSoulsPlayer.ForceEffect<JungleEnchant>())
            ++num;
          fargoSoulsPlayer.JungleCD = 18 - num * num;
          int dmg = 12 * num * num - 5;
          SoundStyle soundStyle = SoundID.Item62;
          ((SoundStyle) ref soundStyle).Volume = 0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
          foreach (Projectile projectile in FargoSoulsUtil.XWay(10, player.GetSource_EffectItem<JungleJump>(), new Vector2(((Entity) player).Center.X, ((Entity) player).Center.Y + (float) (((Entity) player).height / 2)), 228, 3f, FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 0.0f))
            ++projectile.extraUpdates;
          fargoSoulsPlayer.JungleCD = 24;
        }
      }
      if (player.HasEffect<BeeEffect>() & inUse)
      {
        if (fargoSoulsPlayer.BeeCD == 0)
        {
          int num = player.ForceEffect<BeeEffect>() ? 88 : 22;
          Projectile.NewProjectile(player.GetSource_Accessory(player.EffectItem<BeeEffect>(), (string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<BeeFlower>(), num, 0.5f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          fargoSoulsPlayer.BeeCD = 50;
        }
        if (fargoSoulsPlayer.BeeCD > 0)
          --fargoSoulsPlayer.BeeCD;
      }
      return base.WingUpdate(wings, player, inUse);
    }

    public virtual void VerticalWingSpeeds(
      Item item,
      Player player,
      ref float ascentWhenFalling,
      ref float ascentWhenRising,
      ref float maxCanAscendMultiplier,
      ref float maxAscentMultiplier,
      ref float constantAscend)
    {
      base.VerticalWingSpeeds(item, player, ref ascentWhenFalling, ref ascentWhenRising, ref maxCanAscendMultiplier, ref maxAscentMultiplier, ref constantAscend);
    }

    public virtual void HorizontalWingSpeeds(
      Item item,
      Player player,
      ref float speed,
      ref float acceleration)
    {
      base.HorizontalWingSpeeds(item, player, ref speed, ref acceleration);
    }

    public virtual void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
      if (item.type != 4923)
        return;
      tooltips.Add(new TooltipLine(((ModType) this).Mod, "StarlightTungsten", Language.GetTextValue("Mods.FargowiltasSouls.Items.Extra.StarlightTungsten")));
    }

    public virtual bool AllowPrefix(Item item, int pre)
    {
      if (!Main.gameMenu && ((Entity) Main.LocalPlayer).active && Main.LocalPlayer.FargoSouls().SecurityWallet)
      {
        switch (pre)
        {
          case 1:
          case 3:
          case 7:
          case 8:
          case 9:
          case 10:
          case 11:
          case 12:
          case 13:
          case 14:
          case 16:
          case 18:
          case 19:
          case 22:
          case 23:
          case 24:
          case 27:
          case 29:
          case 30:
          case 31:
          case 32:
          case 33:
          case 35:
          case 36:
          case 38:
          case 39:
          case 40:
          case 41:
          case 42:
          case 45:
          case 47:
          case 48:
          case 49:
          case 50:
          case 51:
          case 52:
          case 54:
          case 55:
          case 56:
          case 58:
          case 61:
          case 62:
          case 63:
          case 69:
          case 70:
          case 73:
          case 74:
          case 77:
          case 78:
          case 79:
            if (++FargoGlobalItem.infiniteLoopHackFix < 30)
              return false;
            break;
        }
      }
      FargoGlobalItem.infiniteLoopHackFix = 0;
      return base.AllowPrefix(item, pre);
    }

    static FargoGlobalItem()
    {
      List<int> intList = new List<int>();
      CollectionsMarshal.SetCount<int>(intList, 8);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList);
      int num1 = 0;
      span[num1] = 757;
      int num2 = num1 + 1;
      span[num2] = 273;
      int num3 = num2 + 1;
      span[num3] = 675;
      int num4 = num3 + 1;
      span[num4] = 368;
      int num5 = num4 + 1;
      span[num5] = 674;
      int num6 = num5 + 1;
      span[num6] = 1826;
      int num7 = num6 + 1;
      span[num7] = ModContent.ItemType<TheBaronsTusk>();
      int num8 = num7 + 1;
      span[num8] = 5095;
      int num9 = num8 + 1;
      FargoGlobalItem.TungstenAlwaysAffects = intList;
    }
  }
}
