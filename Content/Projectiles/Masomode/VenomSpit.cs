// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.VenomSpit
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
  public class VenomSpit : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_472";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.aiStyle = 1;
      this.AIType = 472;
      this.Projectile.hostile = true;
      this.Projectile.timeLeft = 300;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 30, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 0.45f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.9f));
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) ((Color) ref lightColor).G, (int) byte.MaxValue, (int) ((Color) ref lightColor).A));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(70, 180, true, false);
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
