// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Retiglaive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Retiglaive : ModProjectile
  {
    private bool empowered;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.empowered);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.empowered = reader.ReadBoolean();
    }

    public virtual void SetDefaults()
    {
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.friendly = true;
      this.Projectile.light = 0.4f;
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.extraUpdates = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual bool PreAI()
    {
      if ((double) this.Projectile.ai[0] != 1.0)
        return true;
      ++this.Projectile.ai[1];
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * -0.4f;
      if ((double) this.Projectile.ai[1] <= 50.0)
      {
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.Zero, 0.1f);
        if ((double) this.Projectile.ai[1] % 10.0 == 0.0)
        {
          Vector2 vector2_1 = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center));
          if ((double) this.Projectile.ai[1] > 10.0)
            vector2_1 = Utils.RotatedByRandom(vector2_1, Math.PI / 24.0);
          float num1 = 24f;
          for (int index1 = 0; (double) index1 < (double) num1; ++index1)
          {
            int num2 = 235;
            Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, 0.0f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * (6.2831854820251465 / (double) num1), new Vector2())), new Vector2(1f, 4f))), (double) Utils.ToRotation(vector2_1), new Vector2());
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, num2, 0.0f, 0.0f, 150, new Color((int) byte.MaxValue, 153, 145), 1f);
            Main.dust[index2].scale = 1.5f;
            Main.dust[index2].fadeIn = 1.3f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(vector2_2, this.Projectile.scale), 1.5f));
            Main.dust[index2].velocity = Utils.SafeNormalize(vector2_2, Vector2.UnitY);
          }
          Player player = Main.player[this.Projectile.owner];
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_1, ModContent.ProjectileType<RetiDeathray>(), this.Projectile.damage, 1f, this.Projectile.owner, 0.0f, (float) this.Projectile.identity, 0.0f);
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.op_UnaryNegation(vector2_1), 8f);
          if (this.empowered)
          {
            float num3 = Utils.NextFloat(Main.rand, 6.28318548f);
            for (int index3 = 0; index3 < 3; ++index3)
            {
              int index4 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(1.25f, Utils.RotatedBy(vector2_1, 2.0943951606750488 * (double) index3 + (double) num3, new Vector2())), ModContent.ProjectileType<MechElectricOrbHomingFriendly>(), this.Projectile.damage, 1f, this.Projectile.owner, -1f, 0.0f, 0.0f);
              if (index4 != Main.maxProjectiles)
              {
                Main.projectile[index4].DamageType = DamageClass.Melee;
                Main.projectile[index4].timeLeft = 90;
              }
            }
          }
        }
      }
      if ((double) this.Projectile.ai[1] > 60.0)
      {
        this.Projectile.ai[1] = 15f;
        this.Projectile.ai[0] = 2f;
      }
      return false;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[0] == (double) ModContent.ProjectileType<Spazmaglaive>())
      {
        this.empowered = true;
        this.Projectile.ai[0] = 0.0f;
      }
      else if ((double) this.Projectile.ai[0] == (double) ModContent.ProjectileType<Retiglaive>())
        this.Projectile.ai[0] = 0.0f;
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        ++this.Projectile.ai[1];
        if ((double) this.Projectile.ai[1] > 30.0)
        {
          this.Projectile.ai[0] = 1f;
          this.Projectile.ai[1] = 0.0f;
          this.Projectile.netUpdate = true;
        }
      }
      else if ((double) this.Projectile.ai[0] == 2.0)
      {
        this.Projectile.ai[1] += 0.6f;
        float num = (double) this.Projectile.ai[1] < 40.0 ? 0.07f : 0.3f;
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center)), this.Projectile.ai[1]), num);
        if ((double) ((Entity) this.Projectile).Distance(((Entity) Main.player[this.Projectile.owner]).Center) <= 30.0)
          this.Projectile.Kill();
      }
      this.Projectile.rotation += (float) ((Entity) this.Projectile).direction * -0.4f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width = 22;
      height = 22;
      return true;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D1 = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D1.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color alpha = this.Projectile.GetAlpha(lightColor);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        if (index < 4)
        {
          Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num3, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
        }
        if (this.empowered)
        {
          Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpearSpinGlow", (AssetRequestMode) 1).Value;
          Color color;
          // ISSUE: explicit constructor call
          ((Color) ref color).\u002Ector((int) byte.MaxValue, 50, 50);
          color = Color.Lerp(color, Color.Transparent, 0.6f);
          float num4 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(), color, num3, Vector2.op_Division(Utils.Size(texture2D2), 2f), num4, (SpriteEffects) 0, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }
  }
}
