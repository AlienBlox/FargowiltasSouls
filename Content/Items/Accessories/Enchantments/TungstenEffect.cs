// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.TungstenEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class TungstenEffect : AccessoryEffect
  {
    public static List<int> TungstenAlwaysAffectProjType;
    public static List<int> TungstenAlwaysAffectProjStyle;
    public static List<int> TungstenNeverAffectProjType;
    public static List<int> TungstenNeverAffectProjStyle;

    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<TungstenEnchant>();

    public override void ModifyHitNPCWithItem(
      Player player,
      Item item,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if (!player.FargoSouls().ForceEffect<TungstenEnchant>() && item.shoot != 0)
        return;
      TungstenEffect.TungstenModifyDamage(player, ref modifiers);
    }

    public override void ModifyHitNPCWithProj(
      Player player,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if ((double) proj.FargoSouls().TungstenScale == 1.0)
        return;
      TungstenEffect.TungstenModifyDamage(player, ref modifiers);
    }

    public override void PostUpdateMiscEffects(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.TungstenCD <= 0)
        return;
      --fargoSoulsPlayer.TungstenCD;
    }

    public static float TungstenIncreaseWeaponSize(FargoSoulsPlayer modPlayer)
    {
      return (float) (1.0 + (modPlayer.ForceEffect<TungstenEnchant>() ? 2.0 : 1.0));
    }

    public static bool TungstenAlwaysAffectProj(Projectile projectile)
    {
      return ProjectileID.Sets.IsAWhip[projectile.type] || TungstenEffect.TungstenAlwaysAffectProjType.Contains(projectile.type) || TungstenEffect.TungstenAlwaysAffectProjStyle.Contains(projectile.aiStyle);
    }

    public static bool TungstenNeverAffectsProj(Projectile projectile)
    {
      return TungstenEffect.TungstenNeverAffectProjType.Contains(projectile.type) || TungstenEffect.TungstenNeverAffectProjStyle.Contains(projectile.type);
    }

    public static void TungstenIncreaseProjSize(
      Projectile projectile,
      FargoSoulsPlayer modPlayer,
      IEntitySource source)
    {
      if (TungstenEffect.TungstenNeverAffectsProj(projectile))
        return;
      bool flag1 = false;
      bool flag2 = true;
      if (TungstenEffect.TungstenAlwaysAffectProj(projectile))
      {
        flag1 = true;
        flag2 = false;
      }
      else if (FargoSoulsUtil.OnSpawnEnchCanAffectProjectile(projectile, false))
      {
        if (FargoSoulsUtil.IsProjSourceItemUseReal(projectile, source))
        {
          if (modPlayer.TungstenCD == 0)
            flag1 = true;
        }
        else if (source is EntitySource_Parent entitySourceParent && entitySourceParent.Entity is Projectile entity)
        {
          if ((double) entity.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TungstenScale != 1.0)
          {
            flag1 = true;
            flag2 = false;
          }
          else if ((entity.minion || entity.sentry || ProjectileID.Sets.IsAWhip[entity.type]) && modPlayer.TungstenCD == 0)
            flag1 = true;
        }
      }
      if (!flag1)
        return;
      bool flag3 = modPlayer.ForceEffect<TungstenEnchant>();
      float num = flag3 ? 3f : 2f;
      ((Entity) projectile).position = ((Entity) projectile).Center;
      projectile.scale *= num;
      ((Entity) projectile).width = (int) ((double) ((Entity) projectile).width * (double) num);
      ((Entity) projectile).height = (int) ((double) ((Entity) projectile).height * (double) num);
      ((Entity) projectile).Center = ((Entity) projectile).position;
      projectile.GetGlobalProjectile<FargoSoulsGlobalProjectile>().TungstenScale = num;
      if (projectile.aiStyle == 19 || projectile.aiStyle == 161)
      {
        Projectile projectile1 = projectile;
        ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, num);
      }
      if (!flag2)
        return;
      modPlayer.TungstenCD = 40;
      if (modPlayer.Eternity)
      {
        modPlayer.TungstenCD = 0;
      }
      else
      {
        if (!flag3)
          return;
        modPlayer.TungstenCD /= 2;
      }
    }

    public static void TungstenModifyDamage(Player player, ref NPC.HitModifiers modifiers)
    {
      bool flag = player.FargoSouls().ForceEffect<TungstenEnchant>();
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, flag ? 1.14f : 1.07f);
    }

    static TungstenEffect()
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 8);
      Span<int> span1 = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span1[num1] = 699;
      int num2 = num1 + 1;
      span1[num2] = 595;
      int num3 = num2 + 1;
      span1[num3] = 735;
      int num4 = num3 + 1;
      span1[num4] = 877;
      int num5 = num4 + 1;
      span1[num5] = 879;
      int num6 = num5 + 1;
      span1[num6] = 878;
      int num7 = num6 + 1;
      span1[num7] = ModContent.ProjectileType<PrismaRegaliaProj>();
      int num8 = num7 + 1;
      span1[num8] = ModContent.ProjectileType<BaronTuskShrapnel>();
      int num9 = num8 + 1;
      TungstenEffect.TungstenAlwaysAffectProjType = intList1;
      List<int> intList2 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList2, 4);
      Span<int> span2 = CollectionsMarshal.AsSpan<int>(intList2);
      int num10 = 0;
      span2[num10] = 19;
      int num11 = num10 + 1;
      span2[num11] = 99;
      int num12 = num11 + 1;
      span2[num12] = 161;
      int num13 = num12 + 1;
      span2[num13] = 15;
      num9 = num13 + 1;
      TungstenEffect.TungstenAlwaysAffectProjStyle = intList2;
      List<int> intList3 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList3, 3);
      Span<int> span3 = CollectionsMarshal.AsSpan<int>(intList3);
      int num14 = 0;
      span3[num14] = ModContent.ProjectileType<FishStickProjTornado>();
      int num15 = num14 + 1;
      span3[num15] = ModContent.ProjectileType<FishStickWhirlpool>();
      int num16 = num15 + 1;
      span3[num16] = 509;
      num9 = num16 + 1;
      TungstenEffect.TungstenNeverAffectProjType = intList3;
      TungstenEffect.TungstenNeverAffectProjStyle = new List<int>();
    }
  }
}
