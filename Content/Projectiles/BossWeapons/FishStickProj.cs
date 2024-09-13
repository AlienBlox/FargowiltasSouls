// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.FishStickProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class FishStickProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 48;
      ((Entity) this.Projectile).height = 48;
      this.Projectile.aiStyle = 1;
      this.AIType = 507;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = 1;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.ignoreWater = true;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(-45f);
      int num = 3;
      for (int index1 = 0; index1 < num; ++index1)
      {
        Vector2 position = ((Entity) this.Projectile).position;
        Vector2 vector2_1 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
        Vector2 vector2_2 = vector2_1;
        int index2 = Dust.NewDust(Vector2.op_Addition(position, vector2_2), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 172, vector2_1.X * 2f, vector2_1.Y * 2f, 100, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      width /= 2;
      height /= 2;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public static void ShootSharks(
      Vector2 target,
      float speed,
      IEntitySource source,
      int damage,
      float knockback,
      int whoAmI = -1)
    {
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.ModProjectile is Whirlpool && p.owner == Main.myPlayer)))
      {
        if (Utils.NextBool(Main.rand))
        {
          float num1 = speed;
          Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Addition(Vector2.op_Subtraction(target, ((Entity) projectile).Center), Utils.NextVector2Circular(Main.rand, 32f, 32f))), num1), Utils.NextFloat(Main.rand, 1f, 1.5f));
          int num2 = damage / 12;
          Projectile.NewProjectile(source, ((Entity) projectile).Center, vector2, ModContent.ProjectileType<FishStickShark>(), num2, knockback, Main.myPlayer, 0.0f, 0.0f, (float) whoAmI);
        }
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      FishStickProj.ShootSharks(((Entity) target).Center, ((Vector2) ref ((Entity) this.Projectile).velocity).Length(), ((Entity) this.Projectile).GetSource_FromThis((string) null), this.Projectile.damage, this.Projectile.knockBack, ((Entity) target).whoAmI);
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
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
      this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      float num3 = this.Projectile.spriteDirection > 0 ? 1.57079637f : 3.14159274f;
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index += 2)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.op_Multiply(Color.White, this.Projectile.Opacity), 0.75f), (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num4 = this.Projectile.oldRot[index] + num3;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, num4, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation + num3, vector2, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
