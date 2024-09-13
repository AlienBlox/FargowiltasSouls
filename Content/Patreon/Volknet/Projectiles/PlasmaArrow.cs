// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Volknet.Projectiles.PlasmaArrow
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Volknet.Projectiles
{
  public class PlasmaArrow : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 15;
      ((Entity) this.Projectile).height = 15;
      this.Projectile.scale = 0.5f;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.penetrate = 10;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 20;
      this.Projectile.arrow = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      ++this.Projectile.localAI[1];
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 30.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.04f);
      }
      if ((double) this.Projectile.localAI[1] > 10.0 && !Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
        this.Projectile.tileCollide = true;
      this.Projectile.alpha -= 50;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, 0.4f), this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      this.Projectile.ai[0] = (float) (((double) this.Projectile.ai[0] + 1.0) % 40.0);
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      for (float num = 0.0f; (double) num < 3.0; ++num)
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), Vector2.op_Multiply(Utils.ToRotationVector2((float) ((double) num * 6.2831854820251465 / 3.0 + (double) this.Projectile.ai[0] / 40.0 * 6.2831854820251465)), 2f)), new Rectangle?(), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, Vector2.op_Division(Utils.Size(texture2D), 2f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index = 0; index < 10; ++index)
      {
        Dust dust = Dust.NewDustDirect(((Entity) this.Projectile).position, 20, 20, 157, 0.0f, 0.0f, 0, new Color(), 3f);
        dust.velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(dust.position, ((Entity) this.Projectile).Center)), (float) Main.rand.Next(20));
        dust.noGravity = true;
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      for (int index = 0; index < 10; ++index)
      {
        Dust dust = Dust.NewDustDirect(((Entity) this.Projectile).position, 20, 20, 157, 0.0f, 0.0f, 0, new Color(), 3f);
        dust.velocity = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(dust.position, ((Entity) this.Projectile).Center)), (float) Main.rand.Next(20));
        dust.noGravity = true;
      }
      target.AddBuff(195, 600, false);
      target.AddBuff(31, 600, false);
    }
  }
}
