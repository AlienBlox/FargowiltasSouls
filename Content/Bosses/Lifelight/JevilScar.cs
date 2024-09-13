// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.JevilScar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class JevilScar : ModProjectile
  {
    private float rotspeed;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/ChallengerItems/EnchantedLifebladeProjectile";
    }

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 4;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 54;
      ((Entity) this.Projectile).height = 54;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      int num1 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X;
      int num2 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y;
      if (Math.Abs(num1) > targetHitbox.Width / 2)
        num1 = targetHitbox.Width / 2 * Math.Sign(num1);
      if (Math.Abs(num2) > targetHitbox.Height / 2)
        num2 = targetHitbox.Height / 2 * Math.Sign(num2);
      int num3 = ((Rectangle) ref projHitbox).Center.X - ((Rectangle) ref targetHitbox).Center.X - num1;
      int num4 = ((Rectangle) ref projHitbox).Center.Y - ((Rectangle) ref targetHitbox).Center.Y - num2;
      return new bool?(Math.Sqrt((double) (num3 * num3 + num4 * num4)) <= (double) (((Entity) this.Projectile).width / 2));
    }

    public virtual void AI()
    {
      if (this.Projectile.frameCounter > 4)
      {
        this.Projectile.frame %= 3;
        this.Projectile.frameCounter = 0;
      }
      ++this.Projectile.frameCounter;
      if ((double) this.Projectile.ai[0] > 30.0)
      {
        if ((double) this.Projectile.ai[0] == 31.0)
          this.Projectile.ai[1] = (float) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (((Entity) Main.player[(int) this.Projectile.ai[1]]).active && !Main.player[(int) this.Projectile.ai[1]].dead)
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[1]]).Center, ((Entity) this.Projectile).Center);
          float num1 = 18f;
          float num2 = 48f;
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, num1);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2 - 1f), vector2_2), num2);
          if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          {
            ((Entity) this.Projectile).velocity.X = -0.15f;
            ((Entity) this.Projectile).velocity.Y = -0.05f;
          }
        }
      }
      if ((double) this.Projectile.ai[0] > 1200.0 || NPC.CountNPCS(ModContent.NPCType<LifeChallenger>()) < 1)
      {
        this.Projectile.alpha += 17;
        this.Projectile.hostile = false;
      }
      if (this.Projectile.alpha >= 240)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
      if ((double) this.rotspeed == 0.0)
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 0.7853982f;
      if ((double) this.rotspeed < 0.31415927410125732)
        this.rotspeed += (float) Math.PI / 900f;
      this.Projectile.rotation += this.rotspeed;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 100), this.Projectile.Opacity));
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
      Color alpha = this.Projectile.GetAlpha(lightColor);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.5f)
      {
        Color color1 = Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, 51, 153), this.Projectile.Opacity), 0.5f);
        ((Color) ref color1).A = (byte) ((uint) ((Color) ref alpha).A / 2U);
        float num3 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color2 = Color.op_Multiply(color1, num3 * num3);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num4 = this.Projectile.oldRot[index2];
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color2, num4, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), alpha, this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
