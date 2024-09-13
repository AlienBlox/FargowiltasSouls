// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.EridanusFist
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class EridanusFist : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 11;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.alpha = 0;
      this.Projectile.timeLeft = 300;
      this.Projectile.extraUpdates = 1;
      this.Projectile.hide = true;
      this.Projectile.scale = 1.25f;
    }

    public virtual void AI()
    {
      if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) > (double) this.Projectile.ai[0])
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.frame = Main.rand.Next(Main.projFrames[this.Projectile.type]);
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        this.Projectile.hide = false;
        ((Entity) this.Projectile).direction = this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        if (this.Projectile.spriteDirection < 0)
          this.Projectile.rotation += 3.14159274f;
        if (++this.Projectile.frameCounter > 2)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 100, new Color(), 1.2f);
        Main.dust[index].position = Vector2.op_Division(Vector2.op_Addition(Main.dust[index].position, ((Entity) this.Projectile).Center), 2f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity = Vector2.op_Multiply(Main.dust[index].velocity, 0.3f);
        Main.dust[index].velocity = Vector2.op_Subtraction(Main.dust[index].velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.1f));
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 5; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(24, 600, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
