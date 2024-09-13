// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearArc
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearArc : ModProjectile
  {
    public int counter;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 20;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.scale = 0.5f;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Throwing;
      this.Projectile.alpha = 100;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 3;
      this.Projectile.timeLeft = 120 * (this.Projectile.extraUpdates + 1);
      this.Projectile.penetrate = -1;
    }

    public virtual void AI()
    {
      if (this.counter == 0)
      {
        this.Projectile.timeLeft = (int) this.Projectile.ai[1] * this.Projectile.MaxUpdates;
        this.Projectile.ai[1] = (float) Main.rand.Next(100);
        this.Projectile.netUpdate = true;
      }
      if (++this.counter > 15 * this.Projectile.MaxUpdates)
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      ++this.Projectile.frameCounter;
      Lighting.AddLight(((Entity) this.Projectile).Center, 0.3f, 0.45f, 0.5f);
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || this.Projectile.frameCounter < this.Projectile.extraUpdates * 2)
        return;
      this.Projectile.frameCounter = 0;
      float num1 = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
      UnifiedRandom unifiedRandom = new UnifiedRandom((int) this.Projectile.ai[1]);
      int num2 = 0;
      Vector2 vector2 = Vector2.op_UnaryNegation(Vector2.UnitY);
      Vector2 rotationVector2;
      do
      {
        int num3 = unifiedRandom.Next();
        this.Projectile.ai[1] = (float) num3;
        rotationVector2 = Utils.ToRotationVector2((float) ((double) (num3 % 100) / 100.0 * 6.28318548202515));
        if ((double) rotationVector2.Y > 0.0)
          --rotationVector2.Y;
        bool flag = false;
        if ((double) rotationVector2.Y > -0.0199999995529652)
          flag = true;
        if ((double) rotationVector2.X * (double) (this.Projectile.extraUpdates + 1) * 2.0 * (double) num1 + (double) this.Projectile.localAI[0] > 40.0)
          flag = true;
        if ((double) rotationVector2.X * (double) (this.Projectile.extraUpdates + 1) * 2.0 * (double) num1 + (double) this.Projectile.localAI[0] < -40.0)
          flag = true;
        if (!flag)
          goto label_18;
      }
      while (num2++ < 100);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      this.Projectile.localAI[1] = 1f;
      goto label_19;
label_18:
      vector2 = rotationVector2;
label_19:
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || (double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 4.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.ai[0], new Vector2()), Math.PI / 4.0), 7f);
        this.Projectile.ai[1] = (float) Main.rand.Next(100);
      }
      else
      {
        this.Projectile.localAI[0] += (float) ((double) vector2.X * (double) (this.Projectile.extraUpdates + 1) * 2.0) * num1;
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.RotatedBy(vector2, (double) this.Projectile.ai[0] + 1.5707963705062866, new Vector2()), num1);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      }
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      for (int index = 0; index < this.Projectile.oldPos.Length && ((double) this.Projectile.oldPos[index].X != 0.0 || (double) this.Projectile.oldPos[index].Y != 0.0); ++index)
      {
        Rectangle rectangle = projHitbox;
        rectangle.X = (int) this.Projectile.oldPos[index].X;
        rectangle.Y = (int) this.Projectile.oldPos[index].Y;
        if (((Rectangle) ref rectangle).Intersects(targetHitbox))
          return new bool?(true);
      }
      return new bool?(false);
    }

    public virtual void OnKill(int timeLeft)
    {
      float num1 = (float) ((double) this.Projectile.rotation + 1.5707963705062866 + (Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 3.1415927410125732 / 2.0);
      float num2 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) Math.Cos((double) num1) * num2, (float) Math.Sin((double) num1) * num2);
      for (int index1 = 0; index1 < this.Projectile.oldPos.Length; ++index1)
      {
        int index2 = Dust.NewDust(this.Projectile.oldPos[index1], 0, 0, 229, vector2.X, vector2.Y, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].scale = 1.7f;
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      target.immune[this.Projectile.owner] = 1;
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue)));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index1 = 1; index1 < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index1)
      {
        if (!Vector2.op_Equality(this.Projectile.oldPos[index1], Vector2.Zero) && !Vector2.op_Equality(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]))
        {
          Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]);
          int num = (int) ((Vector2) ref vector2_2).Length();
          ((Vector2) ref vector2_2).Normalize();
          for (int index2 = 0; index2 < num; index2 += 5)
          {
            Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index1], Vector2.op_Multiply(vector2_2, (float) index2));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 1, 0.0f);
          }
        }
      }
      return false;
    }
  }
}
