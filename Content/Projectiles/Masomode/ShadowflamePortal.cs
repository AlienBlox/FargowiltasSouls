// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.ShadowflamePortal
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
  public class ShadowflamePortal : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_673";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 82;
      ((Entity) this.Projectile).height = 82;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.scale *= 0.5f;
      this.Projectile.timeLeft = 80;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      this.Projectile.alpha -= 12;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 27, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index].noGravity = true;
      Dust dust = Main.dust[index];
      dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      Main.dust[index].scale += 0.5f;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color(200, 150, (int) byte.MaxValue, 150), this.Projectile.Opacity));
    }

    public virtual void OnKill(int timeLeft)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(8f, Utils.RotatedByRandom(Utils.ToRotationVector2(this.Projectile.ai[0]), (double) MathHelper.ToRadians(30f)));
        float num1 = (float) Main.rand.Next(10, 80) * (1f / 1000f);
        if (Utils.NextBool(Main.rand))
          num1 *= -1f;
        float num2 = (float) Main.rand.Next(10, 80) * (1f / 1000f);
        if (Utils.NextBool(Main.rand))
          num2 *= -1f;
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, vector2, 496, this.Projectile.damage, 0.0f, Main.myPlayer, num2, num1, 0.0f);
      }
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
