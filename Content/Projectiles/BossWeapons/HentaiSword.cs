// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSword
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSword : ModProjectile
  {
    private const int MAXTIME = 15;
    public const int MUTANT_SWORD_SPACING = 80;
    private const int MUTANT_SWORD_MAX = 8;

    public virtual string Texture => "Terraria/Images/Projectile_454";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 2;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 40;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.alpha = 0;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
      this.Projectile.FargoSouls().noInteractionWithNPCImmunityFrames = true;
      this.Projectile.timeLeft = 15;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return new bool?(false);
      for (int index = 0; index < 3; ++index)
      {
        double num1 = (double) (640 + ((Entity) this.Projectile).width);
        float num2 = 0.0f;
        double scale = (double) this.Projectile.scale;
        Vector2 vector2_1 = Vector2.op_Multiply((float) (num1 * scale), Vector2.Lerp(Utils.ToRotationVector2(this.Projectile.rotation), Utils.ToRotationVector2(this.Projectile.oldRot[1]), 0.333333343f * (float) index));
        Vector2 center = ((Entity) this.Projectile).Center;
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
        if (Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), center, vector2_2, (float) (((Entity) this.Projectile).width / 2) * this.Projectile.scale, ref num2))
          return new bool?(true);
      }
      return new bool?(false);
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
        this.Projectile.ai[2] = 1f;
      if ((double) this.Projectile.localAI[0]++ > 1.0)
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, 0.20943951606750488 * (double) this.Projectile.ai[0], new Vector2());
      Player player = Main.player[this.Projectile.owner];
      if ((double) this.Projectile.ai[2] != (double) player.gravDir)
      {
        this.Projectile.ai[2] = player.gravDir;
        this.Projectile.ai[0] *= -1f;
        ((Entity) this.Projectile).velocity.Y *= -1f;
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).Center = Vector2.op_Subtraction(player.RotatedRelativePoint(player.MountedCenter, false, true), Vector2.op_Division(((Entity) this.Projectile).velocity, 2f));
      ((Entity) this.Projectile).direction = ((Entity) player).direction;
      player.heldProj = ((Entity) this.Projectile).whoAmI;
      if (player.itemTime < 2)
        player.itemTime = 2;
      if (player.itemAnimation < 2)
        player.itemAnimation = 2;
      if (++this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      for (int index = 0; index < 8; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 80f), this.Projectile.scale), (float) index));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      for (int index = -1; index <= 1; index += 2)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) MathHelper.ToRadians((float) (45 * index)), new Vector2()), 80f), 1.5f), this.Projectile.scale));
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
    }

    public virtual bool? CanDamage()
    {
      this.Projectile.maxPenetrate = 1;
      return new bool?();
    }

    public virtual bool? CanHitNPC(NPC target)
    {
      return this.Projectile.localNPCImmunity[((Entity) target).whoAmI] >= 3 ? new bool?(false) : new bool?();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      ++this.Projectile.localNPCImmunity[((Entity) target).whoAmI];
      if (this.Projectile.owner == Main.myPlayer)
      {
        for (int index = 0; index < 3; ++index)
          Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) target).position, new Vector2((float) Main.rand.Next(((Entity) target).width), (float) Main.rand.Next(((Entity) target).height))), Vector2.Zero, ModContent.ProjectileType<PhantasmalBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, false);
      if (this.Projectile.owner != Main.myPlayer || target.lifeMax <= 5 || Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<HentaiSwordBlast>()] >= 8)
        return;
      Main.player[this.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<HentaiSwordBlast>()] += 8;
      Vector2 center = ((Entity) target).Center;
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      if (!Main.dedServ)
      {
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(center), (SoundUpdateCallback) null);
      }
      Vector2 vector2_1 = Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f);
      for (int index = 0; index < 8; ++index)
      {
        Vector2 vector2_2 = Utils.RotatedBy(vector2_1, 0.78539818525314331 * (double) index, new Vector2());
        float num = 30f;
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Addition(center, Utils.NextVector2Circular(Main.rand, (float) ((Entity) this.Projectile).width, (float) ((Entity) this.Projectile).height)), Vector2.Zero, ModContent.ProjectileType<HentaiSwordBlast>(), this.Projectile.damage, this.Projectile.knockBack * 3f, this.Projectile.owner, MathHelper.WrapAngle(Utils.ToRotation(vector2_2)), num, 0.0f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return false;
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/MutantBoss/MutantSphereGlow", (AssetRequestMode) 1).Value;
      int height = texture2D.Height;
      int num1 = 0;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num1, texture2D.Width, height);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      for (int index1 = 0; index1 < 8; ++index1)
      {
        Color color1 = Color.op_Multiply(Color.Lerp(new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.9f), this.Projectile.Opacity);
        float num2 = MathHelper.Lerp(1f, 0.05f, (float) index1 / 8f);
        for (float index2 = 0.0f; (double) index2 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index2 += num2)
        {
          Color color2 = Color.op_Multiply(color1, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index2) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          float num3 = this.Projectile.scale * ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index2) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
          int index3 = (int) index2 - 1;
          if (index3 >= 0)
          {
            Vector2 vector2_2 = Vector2.Lerp(this.Projectile.oldPos[(int) index2], this.Projectile.oldPos[index3], (float) (1.0 - (double) index2 % 1.0));
            Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Utils.ToRotationVector2(MathHelper.Lerp(this.Projectile.oldRot[(int) index2], this.Projectile.oldRot[index3], (float) (1.0 - (double) index2 % 1.0)))), 80f), this.Projectile.scale), (float) index1);
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(vector2_2, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), vector2_3), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num3 * 1.5f, (SpriteEffects) 0, 0.0f);
          }
        }
        Color color3 = Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.85f);
        Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 80f), this.Projectile.scale), (float) index1);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), vector2_4), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      for (int index4 = -1; index4 <= 1; index4 += 2)
      {
        Color color4 = Color.op_Multiply(Color.Lerp(new Color(196, 247, (int) byte.MaxValue, 0), Color.Transparent, 0.9f), this.Projectile.Opacity);
        float num4 = MathHelper.Lerp(1f, 0.05f, (float) index4 / 8f);
        for (float index5 = 0.0f; (double) index5 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index5 += num4)
        {
          Color color5 = Color.op_Multiply(color4, ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index5) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          float num5 = this.Projectile.scale * ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index5) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
          int index6 = (int) index5 - 1;
          if (index6 >= 0)
          {
            Vector2 vector2_5 = Vector2.Lerp(this.Projectile.oldPos[(int) index5], this.Projectile.oldPos[index6], (float) (1.0 - (double) index5 % 1.0));
            Vector2 vector2_6 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(Utils.ToRotationVector2(MathHelper.Lerp(this.Projectile.oldRot[(int) index5], this.Projectile.oldRot[index6], (float) (1.0 - (double) index5 % 1.0)))), (double) MathHelper.ToRadians((float) (45 * index4)), new Vector2()), 80f), 1.5f), this.Projectile.scale);
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(vector2_5, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), vector2_6), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color5, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, num5 * 1.5f, (SpriteEffects) 0, 0.0f);
          }
        }
        Color color6 = Color.Lerp(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 0), Color.Transparent, 0.85f);
        Vector2 vector2_7 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) MathHelper.ToRadians((float) (45 * index4)), new Vector2()), 80f), 1.5f), this.Projectile.scale);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), vector2_7), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color6, Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f, vector2_1, this.Projectile.scale * 1.5f, (SpriteEffects) 0, 0.0f);
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return;
      for (int index = 0; index < 8; ++index)
      {
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
        int num2 = num1 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
        Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), 80f), this.Projectile.scale), (float) index);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      for (int index = -1; index <= 1; index += 2)
      {
        Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
        int num3 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
        int num4 = num3 * this.Projectile.frame;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, num4, texture2D.Width, num3);
        Vector2 vector2_3 = Vector2.op_Division(Utils.Size(rectangle), 2f);
        Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.Projectile).velocity), (double) MathHelper.ToRadians((float) (45 * index)), new Vector2()), 80f), 1.5f), this.Projectile.scale);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_4), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_3, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
    }
  }
}
