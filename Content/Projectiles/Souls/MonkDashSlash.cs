// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.MonkDashSlash
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class MonkDashSlash : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_729";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 18;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(729);
      this.Projectile.aiStyle = -1;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 200));
    }

    public virtual void AI()
    {
      float num1 = 1.57079637f;
      this.Projectile.alpha -= 10;
      int num2 = 100;
      if (this.Projectile.alpha < num2)
        this.Projectile.alpha = num2;
      if ((double) this.Projectile.ai[0] != 0.0)
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) this.Projectile.ai[0] / (double) (10 * this.Projectile.MaxUpdates), new Vector2());
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + num1;
      this.Projectile.tileCollide = false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Type].Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      int num1 = 18;
      int num2 = 0;
      int num3 = -2;
      float num4 = 1.3f;
      float num5 = 15f;
      for (int index = num1; num3 > 0 && index < num2 || num3 < 0 && index > num2; index += num3)
      {
        Projectile projectile = this.Projectile;
        if (index < projectile.oldPos.Length)
        {
          Color white = Color.White;
          Color alpha = projectile.GetAlpha(white);
          float num6 = (float) (num2 - index);
          if (num3 < 0)
            num6 = (float) (num1 - index);
          Color color = Color.op_Multiply(alpha, num6 / ((float) ProjectileID.Sets.TrailCacheLength[projectile.type] * 1.5f));
          Vector2 oldPo = projectile.oldPos[index];
          float rotation = projectile.rotation;
          SpriteEffects spriteEffects = (SpriteEffects) 0;
          if (ProjectileID.Sets.TrailingMode[projectile.type] == 2 || ProjectileID.Sets.TrailingMode[projectile.type] == 3 || ProjectileID.Sets.TrailingMode[projectile.type] == 4)
            rotation = projectile.oldRot[index];
          if (!Vector2.op_Equality(oldPo, Vector2.Zero))
          {
            Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, Vector2.Zero), Vector2.op_Division(((Entity) projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, projectile.gfxOffY));
            Main.EntitySpriteDraw(texture2D, vector2_2, new Rectangle?(rectangle), color, rotation, vector2_1, MathHelper.Lerp(projectile.scale, num4, (float) index / num5), spriteEffects, 0.0f);
          }
        }
      }
      return false;
    }
  }
}
