// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.TorchGodFlame
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class TorchGodFlame : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 3;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 1.5f;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.penetrate = -1;
    }

    public virtual bool? CanDamage()
    {
      return this.Projectile.alpha != 0 ? new bool?(false) : base.CanDamage();
    }

    public virtual void AI()
    {
      if (((Entity) this.Projectile).wet)
      {
        this.Projectile.timeLeft = 0;
      }
      else
      {
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] == 0.0)
          SoundEngine.PlaySound(ref SoundID.Item117, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        float num = (float) Math.PI / 120f * this.Projectile.localAI[0];
        ((Entity) this.Projectile).velocity = new Vector2(this.Projectile.ai[0] * (float) Math.Sin((double) num), this.Projectile.ai[1] * (float) Math.Cos((double) num));
        if ((double) this.Projectile.localAI[0] > 600.0)
        {
          this.Projectile.alpha += 6;
          if (this.Projectile.alpha > (int) byte.MaxValue)
          {
            this.Projectile.alpha = (int) byte.MaxValue;
            this.Projectile.Kill();
            return;
          }
        }
        else
        {
          this.Projectile.alpha -= 6;
          if (this.Projectile.alpha < 0)
            this.Projectile.alpha = 0;
        }
        Lighting.AddLight((int) ((Entity) this.Projectile).Center.X / 16, (int) ((Entity) this.Projectile).Center.Y / 16, 0, this.Projectile.Opacity);
        if (++this.Projectile.frameCounter >= 3)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        if (!Utils.NextBool(Main.rand))
          return;
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust1 = Main.dust[index];
        dust1.velocity = Vector2.op_Addition(dust1.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 2f));
        Main.dust[index].velocity.Y -= 3f;
        Dust dust2 = Main.dust[index];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 0.5f);
        Main.dust[index].scale *= 1.25f;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(24, 60, true, false);
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
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, this.Projectile.scale), 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
