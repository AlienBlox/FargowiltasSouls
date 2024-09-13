// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.Snowstorm
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class Snowstorm : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetDefaults()
    {
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 2;
      ((Entity) this.Projectile).width = 1;
      ((Entity) this.Projectile).height = 1;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      ++this.Projectile.timeLeft;
      if (player.dead || !((Entity) player).active || !player.HasEffect<SnowEffect>())
        this.Projectile.Kill();
      if (player == Main.LocalPlayer)
        ((Entity) this.Projectile).Center = Main.MouseWorld;
      int num1 = 50;
      if ((fargoSoulsPlayer.ForceEffect<SnowEnchant>() ? 1 : (fargoSoulsPlayer.ForceEffect<FrostEnchant>() ? 1 : 0)) != 0)
        num1 = 100;
      for (int index = 0; index < 15; ++index)
      {
        Vector2 vector2 = new Vector2();
        double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2.X += (float) Math.Sin(num2) * (float) Main.rand.Next(num1 + 1);
        vector2.Y += (float) Math.Cos(num2) * (float) Main.rand.Next(num1 + 1);
        Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), new Vector2(4f, 4f)), 0, 0, 76, 0.0f, 0.0f, 100, Color.White, 0.75f)].noGravity = true;
      }
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (((Entity) projectile).active && projectile.hostile && projectile.damage > 0 && (double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) projectile, ((Entity) this.Projectile).Center)) < (double) num1 && FargoSoulsUtil.CanDeleteProjectile(projectile))
        {
          FargoSoulsGlobalProjectile globalProjectile = projectile.FargoSouls();
          globalProjectile.ChilledProj = true;
          globalProjectile.ChilledTimer = 15;
          this.Projectile.netUpdate = true;
        }
      }
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        NPC npc = Main.npc[index];
        if (((Entity) npc).active && !npc.friendly && npc.damage > 0 && (double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) npc, ((Entity) this.Projectile).Center)) < (double) num1 && !npc.dontTakeDamage)
        {
          npc.FargoSouls().SnowChilled = true;
          npc.FargoSouls().SnowChilledTimer = 15;
          npc.netUpdate = true;
        }
      }
    }
  }
}
