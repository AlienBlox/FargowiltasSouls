// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.DeathSkull
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
  public class DeathSkull : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 8;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 42;
      ((Entity) this.Projectile).height = 60;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
    }

    public virtual void AI()
    {
      this.Projectile.timeLeft = 2;
      Player player = Main.player[this.Projectile.owner];
      if (!((Entity) player).active || player.dead || player.ghost || !player.FargoSouls().DeathMarked)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = Vector2.op_Subtraction(((Entity) player).Center, Vector2.op_Multiply(60f, Vector2.UnitY));
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 109, ((Entity) player).velocity.X * 0.4f, ((Entity) player).velocity.Y * 0.4f, 0, new Color(), 1.5f);
          --Main.dust[index].velocity.Y;
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
          Main.dust[index].velocity.Y -= 0.5f;
        }
        if (++this.Projectile.frameCounter <= 2)
          return;
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame < 8)
          return;
        this.Projectile.frame = 0;
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
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
