// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CopperEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CopperEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<TerraHeader>();

    public override int ToggleItemType => ModContent.ItemType<CopperEnchant>();

    public override bool ExtraAttackEffect => true;

    public override void PostUpdateEquips(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.CopperProcCD <= 0)
        return;
      --fargoSoulsPlayer.CopperProcCD;
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
      bool flag = target.HasBuff(103) && Utils.NextBool(Main.rand, 4);
      if (!(hitInfo.Crit | flag))
        return;
      CopperEffect.CopperProc(player, target);
    }

    public static void CopperProc(Player player, NPC target)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (fargoSoulsPlayer.CopperProcCD != 0)
        return;
      int num1 = fargoSoulsPlayer.ForceEffect<CopperEnchant>() ? 1 : 0;
      target.AddBuff(144, 180, false);
      int dmg = 50;
      int num2 = 1;
      int num3 = 300;
      if (num1 != 0)
      {
        dmg = 200;
        num2 = 5;
        num3 = 150;
      }
      List<int> intList = new List<int>();
      float num4 = 500f;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        NPC npc1 = (NPC) null;
        for (int index2 = 0; index2 < Main.maxNPCs; ++index2)
        {
          NPC npc2 = Main.npc[index2];
          if (((Entity) npc2).active && ((Entity) npc2).whoAmI != ((Entity) target).whoAmI && npc2.CanBeChasedBy((object) null, false) && (double) ((Entity) npc2).Distance(((Entity) target).Center) < (double) num4 && !intList.Contains(((Entity) npc2).whoAmI) && Collision.CanHitLine(((Entity) npc2).Center, 0, 0, ((Entity) target).Center, 0, 0))
          {
            npc1 = npc2;
            num4 = ((Entity) npc2).Distance(((Entity) target).Center);
          }
        }
        if (npc1 != null)
        {
          intList.Add(((Entity) npc1).whoAmI);
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc1).Center, ((Entity) target).Center);
          Vector2 vel = Vector2.op_Multiply(Vector2.Normalize(vector2), 20f);
          int num5 = FargoSoulsUtil.HighestDamageTypeScaling(fargoSoulsPlayer.Player, dmg);
          FargoSoulsUtil.NewProjectileDirectSafe(fargoSoulsPlayer.Player.GetSource_ItemUse(fargoSoulsPlayer.Player.HeldItem, (string) null), ((Entity) target).Center, vel, ModContent.ProjectileType<CopperLightning>(), num5, 0.0f, ((Entity) fargoSoulsPlayer.Player).whoAmI, Utils.ToRotation(vector2), (float) num5);
          target = npc1;
        }
        else
          break;
      }
      fargoSoulsPlayer.CopperProcCD = num3;
    }
  }
}
