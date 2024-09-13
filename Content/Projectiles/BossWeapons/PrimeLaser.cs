// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PrimeLaser
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PrimeLaser : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 5;
      ((Entity) this.Projectile).height = 5;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.extraUpdates = 3;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 120;
      this.Projectile.penetrate = 1;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      if (this.Projectile.alpha > 0)
        this.Projectile.alpha -= 25;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X) + 1.57f;
      Lighting.AddLight((int) ((Entity) this.Projectile).Center.X / 16, (int) ((Entity) this.Projectile).Center.Y / 16, 0.8f, 0.0f, 0.9f);
      float num1 = 100f;
      float num2 = 3f;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.localAI[0] += num2;
        if ((double) this.Projectile.localAI[0] <= (double) num1)
          return;
        this.Projectile.localAI[0] = num1;
      }
      else
      {
        this.Projectile.localAI[0] -= num2;
        if ((double) this.Projectile.localAI[0] > 0.0)
          return;
        this.Projectile.Kill();
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = oldVelocity;
      return true;
    }

    public virtual void OnKill(int timeLeft)
    {
      int num = Main.rand.Next(6, 8);
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) index1), 2f)), 0, 0, 130, 0.0f, 0.0f, 100, new Color((int) byte.MaxValue, 196, 196), 2.1f);
        Dust dust = Main.dust[index2];
        dust.fadeIn = 0.2f;
        dust.scale *= 0.66f;
        dust.velocity = Utils.RotatedByRandom(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 1.25f), 0.2617993950843811);
        Main.dust[index2].noGravity = true;
      }
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Color white = Color.White;
      float num1 = (float) ((double) (TextureAssets.Projectile[this.Projectile.type].Value.Width - ((Entity) this.Projectile).width) * 0.5 + (double) ((Entity) this.Projectile).width * 0.5);
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector((int) Main.screenPosition.X - 500, (int) Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
      Rectangle rect = this.Projectile.getRect();
      if (((Rectangle) ref rect).Intersects(rectangle))
      {
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(((Entity) this.Projectile).position.X - Main.screenPosition.X + num1, ((Entity) this.Projectile).position.Y - Main.screenPosition.Y + (float) (((Entity) this.Projectile).height / 2) + this.Projectile.gfxOffY);
        float num2 = (float) (100.0 * ((double) this.Projectile.ai[0] == 1.0 ? 1.5 : 1.0));
        float num3 = 3f;
        if ((double) this.Projectile.ai[1] == 1.0)
          num2 = (float) (int) this.Projectile.localAI[0];
        for (int index = 1; index <= (int) this.Projectile.localAI[0]; ++index)
        {
          Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), (float) index), num3);
          Color color1 = Color.op_Multiply(this.Projectile.GetAlpha(white), (num2 - (float) index) / num2);
          ((Color) ref color1).A = (byte) 0;
          SpriteBatch spriteBatch = Main.spriteBatch;
          Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
          Vector2 vector2_3 = Vector2.op_Subtraction(vector2_1, vector2_2);
          Rectangle? nullable1 = new Rectangle?();
          Texture2D texture2D2 = texture2D1;
          Vector2 vector2_4 = vector2_3;
          Rectangle? nullable2 = nullable1;
          Color color2 = color1;
          double rotation = (double) this.Projectile.rotation;
          Vector2 vector2_5 = new Vector2(num1, (float) (((Entity) this.Projectile).height / 2));
          double num4 = (double) this.Projectile.scale * ((double) this.Projectile.ai[0] == 1.0 ? 2.0 : 1.0);
          spriteBatch.Draw(texture2D2, vector2_4, nullable2, color2, (float) rotation, vector2_5, (float) num4, (SpriteEffects) 0, 0.0f);
        }
      }
      return false;
    }
  }
}
