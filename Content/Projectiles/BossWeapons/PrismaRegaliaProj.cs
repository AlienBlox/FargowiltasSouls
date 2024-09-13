// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PrismaRegaliaProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PrismaRegaliaProj : ModProjectile
  {
    public float maxCharge = 150f;
    public int SwingDirection = 1;
    public float Extension;
    private int OrigAnimMax = 30;
    private bool Charged;
    private Vector2 ChargeVector = Vector2.Zero;

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.aiStyle = -1;
      ((Entity) this.Projectile).width = 244;
      ((Entity) this.Projectile).height = 244;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 3600;
      this.Projectile.tileCollide = false;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 150;
      this.Projectile.DamageType = DamageClass.MeleeNoSpeed;
      this.Projectile.FargoSouls().NinjaCanSpeedup = false;
    }

    public virtual void ModifyDamageHitbox(ref Rectangle hitbox)
    {
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(88f * this.Projectile.scale, 88f * this.Projectile.scale);
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2 velocity = ((Entity) this.Projectile).velocity;
      Vector2 size = ((Entity) this.Projectile).Size;
      double num = (double) ((Vector2) ref size).Length() / 2.0 - (double) ((Vector2) ref vector2_1).Length() / 2.0;
      Vector2 vector2_2 = Vector2.op_Multiply(velocity, (float) num);
      Vector2 vector2_3 = Vector2.op_Addition(center, vector2_2);
      hitbox = new Rectangle((int) ((double) vector2_3.X - (double) vector2_1.X / 2.0), (int) ((double) vector2_3.Y - (double) vector2_1.Y / 2.0), (int) vector2_1.X, (int) vector2_1.Y);
    }

    public virtual void AI()
    {
      ref float local1 = ref this.Projectile.ai[0];
      ref float local2 = ref this.Projectile.ai[1];
      Player player1 = Main.player[this.Projectile.owner];
      player1.heldProj = ((Entity) this.Projectile).whoAmI;
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.OrigAnimMax = player1.itemAnimationMax;
        this.Projectile.localAI[0] = 1f;
      }
      Vector2 size1 = ((Entity) this.Projectile).Size;
      float num1 = ((Vector2) ref size1).Length() / 2f;
      Vector2 size2 = ((Entity) this.Projectile).Size;
      float num2 = -((Vector2) ref size2).Length() / 6f;
      if (player1.channel)
      {
        ((Entity) this.Projectile).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player1, Main.MouseWorld);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(player1.MountedCenter, Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2));
        this.Projectile.friendly = false;
        if ((double) local1 < (double) this.maxCharge)
          ++local1;
        if ((double) local1 == (double) ((int) this.maxCharge - 1) && ((Entity) player1).whoAmI == Main.myPlayer)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ChargeSound", (SoundType) 0);
          ref SoundStyle local3 = ref soundStyle;
          Vector2 center = ((Entity) this.Projectile).Center;
          Vector2 velocity = ((Entity) this.Projectile).velocity;
          Vector2 size3 = ((Entity) this.Projectile).Size;
          double num3 = (double) ((Vector2) ref size3).Length();
          Vector2 vector2 = Vector2.op_Division(Vector2.op_Multiply(velocity, (float) num3), 2f);
          Vector2? nullable = new Vector2?(Vector2.op_Addition(center, vector2));
          SoundEngine.PlaySound(ref local3, nullable, (SoundUpdateCallback) null);
        }
        this.Projectile.localAI[1] = local1;
        bool flag = (double) local1 >= (double) this.maxCharge - 1.0;
        Player player2 = player1;
        Vector2 size4 = ((Entity) this.Projectile).Size;
        double distance = (double) ((Vector2) ref size4).Length() * 0.949999988079071;
        Color color = flag ? Color.HotPink : Color.DeepPink;
        int particleType = flag ? 1 : 0;
        FargoSoulsUtil.AuraParticles((Entity) player2, (float) distance, color, particleType: particleType);
      }
      else
      {
        this.Projectile.friendly = true;
        if ((double) local1 > -1.0)
        {
          this.Projectile.damage = (int) ((double) this.Projectile.damage * (1.1499999761581421 + (double) local1 / 60.0));
          this.Charged = (double) local1 == (double) this.maxCharge;
          local1 = -1f;
        }
        int num4 = (int) ((double) this.OrigAnimMax / 1.5);
        int num5 = this.OrigAnimMax / 5;
        if ((double) local2 == 0.0)
          this.SwingDirection = Utils.NextBool(Main.rand, 2) ? 1 : -1;
        float num6 = 13f;
        this.Projectile.localNPCHitCooldown = this.OrigAnimMax;
        if (this.Projectile.timeLeft > this.OrigAnimMax)
          this.Projectile.timeLeft = this.OrigAnimMax;
        if ((double) local2 <= (double) (num4 / 2))
        {
          this.Extension = local2 / (float) (num4 / 2);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) (this.SwingDirection * this.Projectile.spriteDirection) * (-1.0 * Math.PI) / ((double) num6 * (double) this.OrigAnimMax), new Vector2());
        }
        else if ((double) local2 <= (double) (num4 / 2 + num5))
        {
          this.Extension = 1f;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) (this.SwingDirection * this.Projectile.spriteDirection) * (1.5 * (double) num4 / (double) num5) * Math.PI / ((double) num6 * (double) this.OrigAnimMax), new Vector2());
        }
        else
        {
          this.Projectile.friendly = false;
          this.Extension = ((float) (num4 + num5) - local2) / (float) (num4 / 2);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) (this.SwingDirection * this.Projectile.spriteDirection) * (-1.0 * Math.PI) / ((double) num6 * (double) this.OrigAnimMax), new Vector2());
        }
        if ((double) local2 == (double) (num4 / 2))
        {
          float num7 = this.Charged ? -1f : 0.0f;
          SoundStyle soundStyle = SoundID.Item1;
          ((SoundStyle) ref soundStyle).Pitch = num7;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
        }
        ++local2;
        ((Entity) this.Projectile).velocity = Vector2.Normalize(((Entity) this.Projectile).velocity);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(player1.MountedCenter, Vector2.SmoothStep(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2), Vector2.op_Multiply(((Entity) this.Projectile).velocity, num1), this.Extension));
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      player1.ChangeDir(((Entity) this.Projectile).direction);
      player1.itemRotation = Utils.ToRotation(Vector2.op_Multiply(((Entity) this.Projectile).velocity, (float) ((Entity) this.Projectile).direction));
      player1.itemTime = 2;
      player1.itemAnimation = 2;
      if (this.Projectile.spriteDirection == -1)
        this.Projectile.rotation += MathHelper.ToRadians(-45f) + 3.14159274f;
      else
        this.Projectile.rotation += MathHelper.ToRadians(-135f) + 3.14159274f;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2 velocity = ((Entity) this.Projectile).velocity;
      Vector2 size = ((Entity) this.Projectile).Size;
      double num1 = (double) ((Vector2) ref size).Length() / 2.0;
      Vector2 vector2_1 = Vector2.op_Multiply(velocity, (float) num1);
      Vector2 vector2_2 = Vector2.op_Addition(center, vector2_1);
      int num2 = 0;
      if (this.Charged)
        num2 = 4;
      if ((double) this.Extension > 0.675000011920929)
      {
        SoundEngine.PlaySound(ref SoundID.Item68, new Vector2?(vector2_2), (SoundUpdateCallback) null);
        num2 += 3;
      }
      if (num2 == 0)
        return;
      for (int index1 = 0; index1 < num2; ++index1)
      {
        int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_2, Vector2.op_Multiply(Utils.ToRotationVector2(Utils.NextFloat(Main.rand, 6.28318548f)), 10f), 931, this.Projectile.damage / 6, this.Projectile.knockBack, this.Projectile.owner, -1f, Utils.NextFloat(Main.rand, 1f), 0.0f);
        if (Main.projectile[index2] != null && index2 != Main.maxProjectiles)
          Main.projectile[index2].DamageType = DamageClass.MeleeNoSpeed;
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
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection == -1 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      Color alpha = this.Projectile.GetAlpha(lightColor);
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY));
      if (!Main.player[this.Projectile.owner].channel)
      {
        for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
        {
          Color color = Color.op_Multiply(alpha, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
          Vector2 oldPo = this.Projectile.oldPos[index];
          float rotation = this.Projectile.rotation;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color, rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      for (int index = 0; index < 16; ++index)
      {
        float num3 = (float) (4.0 * ((double) this.Projectile.localAI[1] / (double) this.maxCharge));
        Vector2 vector2_3 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), num3);
        Color color = Color.op_Multiply(Main.DiscoColor, 0.3f);
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(vector2_2, vector2_3), new Rectangle?(), color, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, vector2_2, new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
