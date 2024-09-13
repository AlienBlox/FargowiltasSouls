// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearThrown
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearThrown : ModProjectile
  {
    private float scaletimer;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 10;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 180;
      this.Projectile.extraUpdates = 1;
      this.Projectile.alpha = 0;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num = 0.0f;
      Vector2 vector2_1 = Vector2.op_Multiply((float) (200.0 / 2.0) * this.Projectile.scale, Utils.ToRotationVector2(this.Projectile.rotation - MathHelper.ToRadians(135f)));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), vector2_2, vector2_3, 8f * this.Projectile.scale, ref num) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index1].noGravity = true;
      int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 15, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index2].noGravity = true;
      if ((double) --this.Projectile.localAI[0] < 0.0)
      {
        this.Projectile.localAI[0] = 3f;
        if (this.Projectile.owner == Main.myPlayer)
        {
          Vector2 vector2 = Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), Math.PI / 2.0, new Vector2());
          int index3 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(16f, vector2), ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 1f, 0.0f, 0.0f);
          if (index3 != Main.maxProjectiles)
            Main.projectile[index3].DamageType = this.Projectile.DamageType;
          int index4 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(16f, Vector2.op_UnaryNegation(vector2)), ModContent.ProjectileType<PhantasmalSphere>(), this.Projectile.damage, this.Projectile.knockBack / 2f, this.Projectile.owner, 1f, 0.0f, 0.0f);
          if (index4 != Main.maxProjectiles)
            Main.projectile[index4].DamageType = this.Projectile.DamageType;
        }
      }
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        if (this.Projectile.owner == Main.myPlayer)
        {
          int index5 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Normalize(((Entity) this.Projectile).velocity), ModContent.ProjectileType<HentaiSpearDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, ((Vector2) ref ((Entity) this.Projectile).velocity).Length() * (float) this.Projectile.MaxUpdates, 0.0f);
          if (index5 != Main.maxProjectiles)
            Main.projectile[index5].DamageType = this.Projectile.DamageType;
        }
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
      ++this.scaletimer;
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.owner == Main.myPlayer)
      {
        int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        if (index != Main.maxProjectiles)
          Main.projectile[index].DamageType = this.Projectile.DamageType;
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantEye_Glow", (AssetRequestMode) 1).Value;
      int num1 = texture2D.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color1 = Color.Lerp(Color.Lerp(new Color(51, (int) byte.MaxValue, 191, 0), Color.Transparent, 0.82f), Color.Lerp(new Color(194, (int) byte.MaxValue, 242, 0), Color.Transparent, 0.6f), (float) (0.5 + Math.Sin((double) this.scaletimer / 7.0) / 2.0));
      Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 28f));
      for (int index = 0; index < 3; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, Utils.RotatedBy(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 20f), 0.62831854820251465 - (double) index * 3.1415927410125732 / 5.0, new Vector2())), Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 20f));
        float num3 = this.Projectile.scale + (float) Math.Sin((double) this.scaletimer / 7.0) / 7f;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_3, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num3 * 1.25f, (SpriteEffects) 0, 0.0f);
      }
      for (int index = ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - 1; index > 0; --index)
      {
        Color color2 = Color.op_Multiply(color1, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        float num4 = this.Projectile.scale * (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] + (float) Math.Sin((double) this.scaletimer / 7.0) / 7f;
        Vector2 vector2_4 = Vector2.op_Subtraction(this.Projectile.oldPos[index], Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitX), 14f));
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_4, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num4 * 1.25f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
    }
  }
}
