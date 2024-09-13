// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.IronParry
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
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class IronParry : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 7;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 78;
      ((Entity) this.Projectile).height = 78;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 2f;
    }

    public virtual void AI()
    {
      this.Projectile.light = (float) ((double) this.Projectile.ai[0] != 1.0 ? 1 : 0);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
        SoundEngine.PlaySound(ref SoundID.NPCHit4, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 20; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, 0.0f, 0.0f, 0, new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150), 1.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
      }
      if (++this.Projectile.frameCounter < 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      --this.Projectile.frame;
      this.Projectile.Kill();
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 150));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = texture2D.Width / 2;
      int num2 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num3 = (double) this.Projectile.ai[0] == 0.0 ? 1 : 0;
      int num4 = num3 != 0 ? num1 : 0;
      int num5 = num2 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(num4, num5, num1, num2);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      float scale = this.Projectile.scale;
      if (num3 == 0)
        scale *= 1.5f;
      Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
