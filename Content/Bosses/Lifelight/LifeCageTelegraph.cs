// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeCageTelegraph
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeCageTelegraph : ModProjectile
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Lifelight/LifeCageProjectile";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 800;
      ((Entity) this.Projectile).height = 800;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = false;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.alpha = 0;
      this.Projectile.light = 2f;
    }

    public virtual void AI()
    {
      ((Entity) this.Projectile).Center = ((Entity) Main.player[(int) this.Projectile.ai[1]]).Center;
      if ((double) this.Projectile.ai[0] > 240.0)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 0; index1 < 26; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
        {
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector(((Entity) this.Projectile).Center.X - 300f + (float) (600 * index2), ((Entity) this.Projectile).Center.Y - 300f + (float) (24 * index1));
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector(((Entity) this.Projectile).Center.X - 300f + (float) (24 * index1), ((Entity) this.Projectile).Center.Y - 300f + (float) (600 * index2));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }
  }
}
