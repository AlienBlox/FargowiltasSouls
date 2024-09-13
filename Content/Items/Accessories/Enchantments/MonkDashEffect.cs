// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MonkDashEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using FargowiltasSouls.Core.Systems;
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
  public class MonkDashEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ShadowHeader>();

    public override int ToggleItemType => ModContent.ItemType<MonkEnchant>();

    public static void AddDash(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.FargoDash = DashManager.DashType.Monk;
      fargoSoulsPlayer.HasDash = true;
      if (player.dashDelay != 1)
        return;
      double num = Math.PI / 18.0;
      for (int index1 = 0; index1 < 36; ++index1)
      {
        Vector2 vector2 = Utils.RotatedBy(new Vector2(2f, 2f), num * (double) index1, new Vector2());
        int index2 = Dust.NewDust(((Entity) player).Center, 0, 0, 246, vector2.X, vector2.Y, 100, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
      }
    }

    public static void MonkDash(Player player, int direction)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.CanShinobiTeleport = true;
      float num1 = player.HasEffect<ShinobiDashEffect>() ? 8f : 16f;
      ((Entity) player).velocity.X = num1 * (float) direction;
      player.immune = true;
      int num2 = player.HasEffect<ShinobiDashEffect>() ? 10 : 20;
      fargoSoulsPlayer.MonkDashing = num2;
      player.immuneTime = Math.Max(player.immuneTime, num2);
      player.hurtCooldowns[0] = Math.Max(player.hurtCooldowns[0], num2);
      player.hurtCooldowns[1] = Math.Max(player.hurtCooldowns[1], num2);
      int num3 = fargoSoulsPlayer.ShinobiEnchantActive ? 1 : (fargoSoulsPlayer.ForceEffect<MonkEnchant>() ? 1 : 0);
      bool flag1 = fargoSoulsPlayer.ShinobiEnchantActive && fargoSoulsPlayer.ForceEffect<ShinobiEnchant>();
      Vector2 center = ((Entity) player).Center;
      int num4 = num3 != 0 ? (flag1 ? 1500 : 1000) : 500;
      Projectile.NewProjectile(((Entity) player).GetSource_FromThis((string) null), center, Vector2.Zero, ModContent.ProjectileType<MonkDashDamage>(), num4, 0.0f, -1, 0.0f, 0.0f, 0.0f);
      fargoSoulsPlayer.DashCD = 100;
      player.dashDelay = 100;
      if (player.FargoSouls().IsDashingTimer < 20)
        player.FargoSouls().IsDashingTimer = 20;
      if (Main.netMode == 1)
        NetMessage.SendData(13, -1, -1, (NetworkText) null, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) player).position.X, ((Entity) player).position.Y), ((Entity) player).width, ((Entity) player).height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].position.X += (float) Main.rand.Next(-5, 6);
        Main.dust[index2].position.Y += (float) Main.rand.Next(-5, 6);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
        Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(20) * 0.0099999997764825821);
      }
      int index3 = Gore.NewGore(((Entity) player).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) player).position.X + (double) (((Entity) player).width / 2) - 24.0), (float) ((double) ((Entity) player).position.Y + (double) (((Entity) player).height / 2) - 34.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index3].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
      Main.gore[index3].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
      Gore gore1 = Main.gore[index3];
      gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
      int index4 = Gore.NewGore(((Entity) player).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) player).position.X + (double) (((Entity) player).width / 2) - 24.0), (float) ((double) ((Entity) player).position.Y + (double) (((Entity) player).height / 2) - 14.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
      Main.gore[index4].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
      Main.gore[index4].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
      Gore gore2 = Main.gore[index4];
      gore2.velocity = Vector2.op_Multiply(gore2.velocity, 0.4f);
      if (!fargoSoulsPlayer.CanShinobiTeleport || !player.HasEffect<ShinobiDashEffect>())
        return;
      fargoSoulsPlayer.CanShinobiTeleport = false;
      Vector2 vector2 = ((Entity) player).position;
      bool flag2 = false;
      for (int index5 = 0; index5 <= 320; index5 += 8)
      {
        Vector2 position = ((Entity) player).position;
        position.X += (float) (index5 * direction);
        if (Collision.CanHitLine(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, vector2, ((Entity) player).width, ((Entity) player).height))
        {
          vector2 = position;
        }
        else
        {
          vector2.X -= (float) (18 * direction);
          flag2 = true;
          break;
        }
      }
      if (flag2 && player.HasEffect<ShinobiThroughWalls>())
      {
        while (Collision.SolidCollision(vector2, ((Entity) player).width, ((Entity) player).height))
        {
          if (direction == 1)
            ++vector2.X;
          else
            --vector2.X;
        }
      }
      if ((double) vector2.X <= 50.0 || (double) vector2.X >= (double) (Main.maxTilesX * 16 - 50) || (double) vector2.Y <= 50.0 || (double) vector2.Y >= (double) (Main.maxTilesY * 16 - 50))
        return;
      FargoSoulsUtil.GrossVanillaDodgeDust((Entity) player);
      player.Teleport(vector2, 1, 0);
      FargoSoulsUtil.GrossVanillaDodgeDust((Entity) player);
      NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) ((Entity) player).whoAmI, vector2.X, vector2.Y, 1, 0, 0);
    }
  }
}
