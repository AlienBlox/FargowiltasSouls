// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MothronZenith
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MothronZenith : ModProjectile
  {
    private int dustTimer;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 11;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 8;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 8;
      ((Entity) this.Projectile).height = 8;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 240;
      this.Projectile.aiStyle = -1;
      this.Projectile.penetrate = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.scale = 1.5f;
      this.Projectile.hide = true;
      this.CooldownSlot = 1;
    }

    public virtual bool? CanDamage()
    {
      return this.Projectile.alpha > 0 ? new bool?(false) : base.CanDamage();
    }

    public virtual bool CanHitPlayer(Player target)
    {
      return target.hurtCooldowns[this.CooldownSlot] <= 0 && base.CanHitPlayer(target);
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      if (((Rectangle) ref projHitbox).Intersects(targetHitbox))
        return new bool?(true);
      float num1;
      switch (this.Projectile.frame)
      {
        case 1:
          num1 = 40f;
          break;
        case 2:
          num1 = 54f;
          break;
        case 3:
          num1 = 50f;
          break;
        case 4:
          num1 = 44f;
          break;
        case 5:
          num1 = 56f;
          break;
        case 6:
          num1 = 50f;
          break;
        case 7:
          num1 = 32f;
          break;
        case 8:
          num1 = 34f;
          break;
        case 9:
          num1 = 46f;
          break;
        case 10:
          num1 = 34f;
          break;
        default:
          num1 = 56f;
          break;
      }
      float num2 = (float) Math.Sqrt(2.0 * (double) num1 * (double) num1) * 0.8f;
      float num3 = 0.0f;
      Vector2 vector2_1 = Vector2.op_Multiply(num2 / 2f * this.Projectile.scale, Utils.ToRotationVector2(this.Projectile.rotation));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, vector2_1);
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), vector2_2, vector2_3, 8f * this.Projectile.scale, ref num3) ? new bool?(true) : new bool?(false);
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.localAI[1] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 100f;
        this.Projectile.hide = false;
        this.Projectile.rotation = (float) (2.0 * (double) Utils.NextFloat(Main.rand, 6.28318548f) * (Utils.NextBool(Main.rand) ? -1.0 : 1.0));
        this.Projectile.frame = Main.rand.Next(Main.projFrames[this.Projectile.type]);
      }
      if (++this.dustTimer == 15)
      {
        this.MakeDust();
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.Projectile.ai[0] == -1.0)
      {
        if ((double) ++this.Projectile.localAI[0] <= 100.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Vector2) ref ((Entity) this.Projectile).velocity).Length() - this.Projectile.localAI[1], Vector2.Normalize(((Entity) this.Projectile).velocity));
          this.Projectile.rotation = MathHelper.Lerp(this.Projectile.rotation, (double) this.Projectile.ai[1] > 0.0 ? 0.0f : 3.14159274f, 0.05f);
          this.Projectile.spriteDirection = (int) this.Projectile.ai[1];
          if ((double) this.Projectile.localAI[0] == 100.0)
            SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        else
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(36f * this.Projectile.ai[1], Vector2.UnitX);
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        }
      }
      else
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 477);
        if (npc == null || (double) npc.ai[0] < 3.0 && this.dustTimer > 15)
        {
          this.Projectile.Kill();
          return;
        }
        ++this.Projectile.timeLeft;
        this.Projectile.spriteDirection = Math.Sign(this.Projectile.ai[1]);
        this.Projectile.ai[1] += (float) Math.PI / 30f * (float) this.Projectile.spriteDirection;
        this.Projectile.rotation = this.Projectile.ai[1];
        float num = 120f;
        if (this.Projectile.spriteDirection < 0)
          num *= 2f;
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(num, Utils.ToRotationVector2(this.Projectile.ai[1])));
        if ((double) npc.ai[0] < 4.0)
          this.Projectile.alpha -= 4;
      }
      this.Projectile.alpha -= 4;
      if (this.Projectile.alpha >= 0)
        return;
      this.Projectile.alpha = 0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Utils.Next<int>(Main.rand, (IList<int>) FargowiltasSouls.FargowiltasSouls.DebuffIDs);
        if (!target.buffImmune[index2] && !Main.buffNoTimeDisplay[index2])
        {
          target.AddBuff(index2, 240, true, false);
          if (target.HasBuff(index2))
            break;
        }
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.dustTimer < 15)
        return;
      this.MakeDust();
      SoundEngine.PlaySound(ref SoundID.NPCDeath52, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    private void MakeDust()
    {
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 91, 0.0f, 0.0f, 0, this.SwordColor, 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
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
      SpriteEffects spriteEffects = this.Projectile.spriteDirection < 0 ? (SpriteEffects) 1 : (SpriteEffects) 0;
      float num3 = MathHelper.ToRadians(45f) * (float) this.Projectile.spriteDirection;
      if (this.Projectile.spriteDirection < 0)
        num3 += 3.14159274f;
      Color color1 = Color.op_Multiply(this.SwordColor, this.Projectile.Opacity);
      ((Color) ref color1).A = (byte) 20;
      for (float index1 = 0.0f; (double) index1 < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; index1 += 0.25f)
      {
        Color color2 = Color.op_Multiply(color1, 0.5f);
        float num4 = ((float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index1) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
        Color color3 = Color.op_Multiply(color2, num4 * num4);
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          float num5 = this.Projectile.oldRot[index2] + num3;
          Vector2 vector2_2 = Vector2.op_Addition(Vector2.Lerp(this.Projectile.oldPos[(int) index1], this.Projectile.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.Projectile).Size, 2f));
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(vector2_2, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color3, num5, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), color1, this.Projectile.rotation + num3, vector2_1, this.Projectile.scale * 1.2f, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation + num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    private Color SwordColor
    {
      get
      {
        switch (this.Projectile.frame)
        {
          case 1:
            return Color.Yellow;
          case 2:
            return Color.Orange;
          case 3:
            return Color.Cyan;
          case 4:
            return Color.Green;
          case 5:
            return Color.HotPink;
          case 6:
            return Color.OrangeRed;
          case 7:
            return Color.Orange;
          case 8:
            return Color.Red;
          case 9:
            return Color.LimeGreen;
          case 10:
            return Color.Blue;
          default:
            return Color.MintCream;
        }
      }
    }
  }
}
