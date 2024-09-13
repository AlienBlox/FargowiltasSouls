// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.EyeFire2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class EyeFire2 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_96";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(101);
      this.AIType = 101;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
      this.Projectile.alpha = 0;
    }

    public virtual void AI() => this.Projectile.rotation += 0.3f;

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f * this.Projectile.scale);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f * this.Projectile.scale);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (Utils.NextBool(Main.rand, 6))
        target.AddBuff(39, 480, true, false);
      else if (Utils.NextBool(Main.rand, 4))
      {
        target.AddBuff(39, 300, true, false);
      }
      else
      {
        if (!Utils.NextBool(Main.rand))
          return;
        target.AddBuff(39, 180, true, false);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color(200, 200, 200, 25), this.Projectile.Opacity));
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
      this.Projectile.GetAlpha(lightColor);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
