// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Whirlpool
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Whirlpool : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_386";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 6;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 75;
      ((Entity) this.Projectile).height = 21;
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 3600;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 0.5f;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.ignoreWater = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      int num1 = 16;
      int num2 = 16;
      float num3 = 1.5f;
      int num4 = 150;
      int num5 = 42;
      if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = -Math.Sign(((Entity) this.Projectile).velocity.X);
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 2)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
      }
      if (this.Projectile.frame >= 6)
        this.Projectile.frame = 0;
      if ((double) this.Projectile.localAI[0] == 0.0 && Main.myPlayer == this.Projectile.owner)
      {
        this.Projectile.localAI[0] = 1f;
        ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2);
        this.Projectile.scale = ((float) (num1 + num2) - this.Projectile.ai[1]) * num3 / (float) (num2 + num1);
        ((Entity) this.Projectile).width = (int) ((double) num4 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) ((double) num5 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X - (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y - (float) (((Entity) this.Projectile).height / 2);
        this.Projectile.netUpdate = true;
      }
      if ((double) this.Projectile.ai[1] != -1.0)
      {
        this.Projectile.scale = ((float) (num1 + num2) - this.Projectile.ai[1]) * num3 / (float) (num2 + num1);
        ((Entity) this.Projectile).width = (int) ((double) num4 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).height = (int) ((double) num5 * (double) this.Projectile.scale);
      }
      if (!Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
      {
        this.Projectile.alpha -= 30;
        if (this.Projectile.alpha < 100)
          this.Projectile.alpha = 100;
      }
      else
      {
        this.Projectile.alpha += 30;
        if (this.Projectile.alpha > 150)
          this.Projectile.alpha = 150;
      }
      if ((double) this.Projectile.ai[0] > 0.0)
        --this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] == 1.0 && (double) this.Projectile.ai[1] > 0.0 && this.Projectile.owner == Main.myPlayer)
      {
        this.Projectile.netUpdate = true;
        Vector2 center = ((Entity) this.Projectile).Center;
        center.Y -= (float) ((double) num5 * (double) this.Projectile.scale / 2.0);
        float num6 = (float) ((double) (num1 + num2) - (double) this.Projectile.ai[1] + 1.0) * num3 / (float) (num2 + num1);
        center.Y -= (float) ((double) num5 * (double) num6 / 2.0);
        center.Y += 2f;
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), center.X, center.Y, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, ModContent.ProjectileType<Whirlpool>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 10f, this.Projectile.ai[1] - 1f, 0.0f);
      }
      if ((double) this.Projectile.ai[0] > 0.0)
        return;
      double num7 = 0.10471975803375244;
      float num8 = (float) ((Entity) this.Projectile).width / 5f;
      if (this.Projectile.type == 386)
        num8 *= 2f;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X - (float) (Math.Cos(num7 * -(double) this.Projectile.ai[0]) - 0.5) * num8 * (float) -(double) ((Entity) this.Projectile).direction;
      --this.Projectile.ai[0];
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X + (float) (Math.Cos(num7 * -(double) this.Projectile.ai[0]) - 0.5) * num8 * (float) -(double) ((Entity) this.Projectile).direction;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 172, (float) (((Entity) this.Projectile).direction * 2), 0.0f, 100, new Color(), 1.4f);
        Dust dust = Main.dust[index2];
        dust.color = Color.CornflowerBlue;
        dust.color = Color.Lerp(dust.color, Color.White, 0.3f);
        dust.noGravity = true;
      }
    }
  }
}
