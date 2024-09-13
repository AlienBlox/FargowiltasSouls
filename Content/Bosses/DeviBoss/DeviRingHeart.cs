// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviRingHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviRingHeart : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/DeviBoss/DeviEnergyHeart";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.hostile = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 480;
      this.Projectile.alpha = 200;
    }

    public virtual void AI()
    {
      ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[1] / (2.0 * Math.PI * (double) this.Projectile.ai[0] * (double) ++this.Projectile.localAI[0]), new Vector2());
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 20;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
    }

    public virtual void OnKill(int timeleft)
    {
      FargoSoulsUtil.HeartDust(((Entity) this.Projectile).Center, addedVel: new Vector2());
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f));
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
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
