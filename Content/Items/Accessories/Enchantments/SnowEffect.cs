// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.SnowEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class SnowEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<NatureHeader>();

    public override int ToggleItemType => ModContent.ItemType<SnowEnchant>();

    public override void OnHitNPCEither(
      Player player,
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      int baseDamage,
      Projectile projectile,
      Item item)
    {
      target.AddBuff(44, 120, false);
    }

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      int num1 = player.HasEffect<FrostEffect>() ? 10 : 5;
      int type = ModContent.ProjectileType<FrostIcicle>();
      if (fargoSoulsPlayer.icicleCD <= 0 && fargoSoulsPlayer.IcicleCount < num1 && player.ownedProjectileCounts[type] < num1)
      {
        ++fargoSoulsPlayer.IcicleCount;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          Projectile projectile = Main.projectile[index];
          if (((Entity) projectile).active && projectile.type == type && projectile.owner == ((Entity) player).whoAmI)
          {
            ((Entity) projectile).active = false;
            projectile.netUpdate = true;
          }
        }
        for (int index = 0; index < fargoSoulsPlayer.IcicleCount; ++index)
        {
          float ai1 = (float) (360.0 / (double) fargoSoulsPlayer.IcicleCount * (double) index * (Math.PI / 180.0));
          FargoSoulsUtil.NewProjectileDirectSafe(this.GetSource_EffectItem(player), ((Entity) player).Center, Vector2.Zero, type, 0, 0.0f, ((Entity) player).whoAmI, 5f, ai1);
        }
        float num2 = 1.5f;
        if (fargoSoulsPlayer.IcicleCount % num1 == 0)
          num2 = 3f;
        for (int index1 = 0; index1 < 20; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 9) * 6.2831854820251465 / 20.0, new Vector2()), ((Entity) player).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) player).Center);
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 15, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = num2;
          Main.dust[index2].velocity = vector2_2;
          if (fargoSoulsPlayer.IcicleCount % num1 == 0)
          {
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
          }
        }
        fargoSoulsPlayer.icicleCD = 30;
      }
      if (fargoSoulsPlayer.icicleCD > 0)
        --fargoSoulsPlayer.icicleCD;
      if (fargoSoulsPlayer.IcicleCount < 1 || !player.controlUseItem || !player.HeldItem.IsWeapon() || player.HeldItem.createTile != -1 || player.HeldItem.createWall != -1 || player.HeldItem.ammo != AmmoID.None)
        return;
      int dmg = fargoSoulsPlayer.ForceEffect<FrostEnchant>() ? 100 : (player.HasEffect<FrostEffect>() ? 50 : 20);
      for (int index3 = 0; index3 < Main.maxProjectiles; ++index3)
      {
        Projectile projectile = Main.projectile[index3];
        if (((Entity) projectile).active && projectile.type == type && projectile.owner == ((Entity) player).whoAmI)
        {
          bool flag = player.HasEffect<FrostEffect>();
          Vector2 vector2 = Vector2.op_Multiply(Utils.SafeNormalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) projectile).Center), Vector2.op_UnaryNegation(Vector2.UnitY)), flag ? 20f : 10f);
          int num3 = flag ? 337 : 166;
          int index4 = Projectile.NewProjectile(this.GetSource_EffectItem(player), ((Entity) projectile).Center, vector2, num3, FargoSoulsUtil.HighestDamageTypeScaling(player, dmg), 1f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
          if (index4 != Main.maxProjectiles)
          {
            Main.projectile[index4].FargoSouls().CanSplit = false;
            Main.projectile[index4].FargoSouls().FrostFreeze = true;
          }
          projectile.Kill();
        }
      }
      fargoSoulsPlayer.IcicleCount = 0;
      fargoSoulsPlayer.icicleCD = 120;
    }
  }
}
