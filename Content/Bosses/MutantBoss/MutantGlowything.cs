// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantGlowything
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantGlowything : ModProjectile
  {
    private Vector2 spawnPoint;

    public virtual void SetStaticDefaults() => ((ModType) this).SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.aiStyle = -1;
      this.Projectile.scale = 0.5f;
      this.Projectile.alpha = 0;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = this.Projectile.ai[0];
      if (Vector2.op_Equality(this.spawnPoint, Vector2.Zero))
        this.spawnPoint = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).Center = Vector2.op_Addition(this.spawnPoint, Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.ai[0], new Vector2()), 96f), this.Projectile.scale));
      if ((double) this.Projectile.scale < 4.0)
      {
        this.Projectile.scale += 0.2f;
      }
      else
      {
        this.Projectile.scale = 4f;
        this.Projectile.alpha += 10;
      }
      if (this.Projectile.alpha <= (int) byte.MaxValue)
        return;
      this.Projectile.Kill();
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int height = texture2D.Height;
      int num = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num, texture2D.Width, height);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color;
      // ISSUE: explicit constructor call
      ((Color) ref color).\u002Ector((int) byte.MaxValue, 0, 0, 0);
      float scale = this.Projectile.scale;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(color), this.Projectile.rotation, vector2, scale * 2f, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
