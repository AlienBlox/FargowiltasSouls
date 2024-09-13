// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.MechFlail
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
  public class MechFlail : ModProjectile
  {
    private int eyeSpawn;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 58;
      ((Entity) this.Projectile).height = 52;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.extraUpdates = 1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
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
        Main.player[this.Projectile.owner].itemAnimation = Math.Max(Main.player[this.Projectile.owner].itemAnimation, 5);
        Main.player[this.Projectile.owner].itemTime = Math.Max(Main.player[this.Projectile.owner].itemTime, 5);
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
          if ((double) num3 > 700.0)
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
        else if ((double) this.Projectile.ai[0] == 1.0)
        {
          this.Projectile.tileCollide = false;
          this.Projectile.rotation = (float) Math.Atan2((double) num2, (double) num1) - 1.57f;
          if ((double) num3 < 50.0)
            this.Projectile.Kill();
          float num4 = (float) (30.0 / (double) num3);
          float num5 = num1 * num4;
          float num6 = num2 * num4;
          ((Entity) this.Projectile).velocity.X = num5;
          ((Entity) this.Projectile).velocity.Y = num6;
          if ((double) ((Entity) this.Projectile).velocity.X < 0.0)
            this.Projectile.spriteDirection = 1;
          else
            this.Projectile.spriteDirection = -1;
        }
        if (this.eyeSpawn != 0)
          --this.eyeSpawn;
        if ((double) this.Projectile.ai[0] != 1.0 || this.Projectile.owner != Main.myPlayer || this.eyeSpawn != 0)
          return;
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center), -1f);
        ((Vector2) ref vector2_2).Normalize();
        Vector2 vector2_3 = Utils.RotatedBy(Vector2.op_Multiply(vector2_2, (float) Main.rand.Next(45, 65) * 0.1f), (Main.rand.NextDouble() - 0.5) * 1.5707963705062866, new Vector2());
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, vector2_3.X, vector2_3.Y, ModContent.ProjectileType<MechEyeProjectile>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -10f, 0.0f, 0.0f);
        this.eyeSpawn = 10;
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if ((double) this.Projectile.ai[0] != 1.0 && this.Projectile.owner == Main.myPlayer && Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<MechEyeProjectile>()] < 60)
      {
        for (int index1 = 0; index1 < 6; ++index1)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 3.0 * ((double) index1 + Main.rand.NextDouble()), new Vector2()), (float) Main.rand.Next(45, 65) * 0.3f);
          int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, vector2.X, vector2.Y, ModContent.ProjectileType<MechEyeProjectile>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, -10f, 0.0f, 0.0f);
          if (index2 != Main.maxProjectiles)
            Main.projectile[index2].timeLeft = 120;
        }
      }
      this.Projectile.ai[0] = 1f;
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 30;
      height = 30;
      return true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.Projectile.ai[0] = 1f;
      return false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/MechFlailChain", (AssetRequestMode) 1).Value;
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
      Color alpha1 = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(alpha1, this.Projectile.Opacity), 0.75f), 0.5f), ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          Vector2 vector2_6 = Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0));
          float num4 = MathHelper.Lerp(this.Projectile.oldRot[(int) index1], this.Projectile.oldRot[index2], (float) (1.0 - (double) index1 % 1.0));
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_6, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num4, vector2_5, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_5, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
