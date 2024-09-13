// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PhantasmalFlail
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PhantasmalFlail : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 58;
      ((Entity) this.Projectile).height = 52;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = 1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      if (this.Projectile.timeLeft == 120)
        this.Projectile.ai[0] = 1f;
      if (Main.player[this.Projectile.owner].dead)
      {
        this.Projectile.Kill();
      }
      else
      {
        Main.player[this.Projectile.owner].itemAnimation = 5;
        Main.player[this.Projectile.owner].itemTime = 5;
        if (this.Projectile.alpha == 0)
        {
          if ((double) ((Entity) this.Projectile).position.X + (double) (((Entity) this.Projectile).width / 2) > (double) ((Entity) Main.player[this.Projectile.owner]).position.X + (double) (((Entity) Main.player[this.Projectile.owner]).width / 2))
            Main.player[this.Projectile.owner].ChangeDir(1);
          else
            Main.player[this.Projectile.owner].ChangeDir(-1);
        }
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width * 0.5f, ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height * 0.5f);
        float num1 = ((Entity) Main.player[this.Projectile.owner]).position.X + (float) (((Entity) Main.player[this.Projectile.owner]).width / 2) - vector2_1.X;
        float num2 = ((Entity) Main.player[this.Projectile.owner]).position.Y + (float) (((Entity) Main.player[this.Projectile.owner]).height / 2) - vector2_1.Y;
        float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
        if ((double) this.Projectile.ai[0] == 0.0)
        {
          if ((double) num3 > 600.0)
            this.Projectile.ai[0] = 1f;
          this.Projectile.rotation = (float) Math.Atan2((double) ((Entity) this.Projectile).velocity.Y, (double) ((Entity) this.Projectile).velocity.X) + 1.57f;
          ++this.Projectile.ai[1];
          if ((double) this.Projectile.ai[1] > 8.0)
            this.Projectile.ai[1] = 8f;
          if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
            this.Projectile.spriteDirection = -1;
          else
            this.Projectile.spriteDirection = 1;
        }
        else
        {
          if ((double) this.Projectile.ai[0] != 1.0)
            return;
          if ((double) this.Projectile.localAI[1] == 0.0)
          {
            this.Projectile.localAI[1] = 1f;
            if (this.Projectile.owner == Main.myPlayer)
            {
              for (int index = 0; index < 8; ++index)
              {
                Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Utils.RotatedBy(new Vector2(100f, 0.0f), 0.78539818525314331 * (double) index, new Vector2()));
                Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, Vector2.Zero, ModContent.ProjectileType<PhantasmalSphere2>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              }
              float num4 = Utils.NextFloat(Main.rand, 6.28318548f);
              for (int index1 = 0; index1 < 32; ++index1)
              {
                Vector2 vector2_3 = Vector2.op_Multiply(10f, Utils.RotatedBy(Vector2.UnitX, (double) num4 + 0.19634954631328583 * (double) index1, new Vector2()));
                int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_3, ModContent.ProjectileType<PhantasmalBolt>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
                if (index2 != Main.maxProjectiles)
                  Main.projectile[index2].timeLeft /= 2;
              }
            }
          }
          this.Projectile.tileCollide = false;
          this.Projectile.rotation = (float) Math.Atan2((double) num2, (double) num1) - 1.57f;
          if ((double) num3 < 50.0)
            this.Projectile.Kill();
          float num5 = (float) (30.0 / (double) num3);
          float num6 = num1 * num5;
          float num7 = num2 * num5;
          ((Entity) this.Projectile).velocity.X = num6 * 2f;
          ((Entity) this.Projectile).velocity.Y = num7 * 2f;
          if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
            this.Projectile.spriteDirection = 1;
          else
            this.Projectile.spriteDirection = -1;
        }
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.penetrate < 0)
        target.immune[this.Projectile.owner] = 1;
      this.Projectile.penetrate = -1;
      this.Projectile.maxPenetrate = -1;
      int index1 = ModContent.ProjectileType<PhantasmalEyeLeashProj>();
      if (Main.player[this.Projectile.owner].ownedProjectileCounts[index1] < 100)
      {
        int num1 = 1200;
        for (int index2 = 0; index2 < 15; ++index2)
        {
          Vector2 vector2_1 = new Vector2();
          double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
          vector2_1.X += (float) Math.Sin(num2) * (float) num1;
          vector2_1.Y += (float) Math.Cos(num2) * (float) num1;
          Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) target).Center, vector2_1), new Vector2(4f, 4f));
          Vector2 vector2_3 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) target).Center, vector2_2)), 50f);
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, vector2_3, index1, this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, -10f, 0.0f, 0.0f);
        }
      }
      this.Projectile.ai[0] = 1f;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/PhantasmalLeashFlailChain", (AssetRequestMode) 1).Value;
      Vector2 vector2_1 = ((Entity) this.Projectile).Center;
      Vector2 mountedCenter = Main.player[this.Projectile.owner].MountedCenter;
      Rectangle? nullable = new Rectangle?();
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector((float) texture2D1.Width * 0.5f, (float) texture2D1.Height * 0.5f);
      float height = (float) texture2D1.Height;
      Vector2 vector2_3 = Vector2.op_Subtraction(mountedCenter, vector2_1);
      float num1 = (float) Math.Atan2((double) vector2_3.Y, (double) vector2_3.X) - 1.57f;
      bool flag = true;
      if (float.IsNaN(vector2_1.X) && float.IsNaN(vector2_1.Y))
        flag = false;
      if (float.IsNaN(vector2_3.X) && float.IsNaN(vector2_3.Y))
        flag = false;
      while (flag)
      {
        if ((double) ((Vector2) ref vector2_3).Length() < (double) height + 1.0)
        {
          flag = false;
        }
        else
        {
          Vector2 vector2_4 = vector2_3;
          ((Vector2) ref vector2_4).Normalize();
          vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(vector2_4, height));
          vector2_3 = Vector2.op_Subtraction(mountedCenter, vector2_1);
          Color alpha = this.Projectile.GetAlpha(Lighting.GetColor((int) vector2_1.X / 16, (int) ((double) vector2_1.Y / 16.0)));
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Subtraction(vector2_1, Main.screenPosition), nullable, alpha, num1, vector2_2, 1f, (SpriteEffects) 0, 0.0f);
        }
      }
      Texture2D texture2D2 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num2 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num3 = num2 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num3, texture2D2.Width, num2);
      Vector2 vector2_5 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color1 = Color.op_Multiply(Color.White, this.Projectile.Opacity);
        ((Color) ref color1).A = (byte) 0;
        Color color2 = Color.op_Multiply(color1, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          Vector2 vector2_6 = Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0));
          float num4 = MathHelper.Lerp(this.Projectile.oldRot[(int) index1], this.Projectile.oldRot[index2], (float) (1.0 - (double) index1 % 1.0));
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_6, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num4, vector2_5, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_5, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
