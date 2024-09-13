// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.CursedFlameHostile2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class CursedFlameHostile2 : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_96";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[96];
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(96);
      this.AIType = 96;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.timeLeft = 0;
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.200000002980232), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.200000002980232), 100, new Color(), 2f * this.Projectile.scale);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(new Vector2(((Entity) this.Projectile).position.X, ((Entity) this.Projectile).position.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 75, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.200000002980232), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.200000002980232), 100, new Color(), 1f * this.Projectile.scale);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(39, 120, true, false);
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
