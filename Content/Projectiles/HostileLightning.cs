﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.HostileLightning
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class HostileLightning : ModProjectile
  {
    private float colorlerp;
    private bool playedsound;
    private Color DrawColor = Color.Cyan;

    public virtual string Texture => "Terraria/Images/Projectile_466";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 20;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.scale = 0.65f;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.alpha = 100;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = true;
      this.Projectile.timeLeft = 120;
      this.Projectile.penetrate = -1;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 100.0)
        this.DrawColor = new Color(231, 174, 254);
      int num1 = (int) this.Projectile.localAI[1];
      Lighting.AddLight(((Entity) this.Projectile).Center, (float) ((int) ((Color) ref this.DrawColor).A / (int) byte.MaxValue), (float) ((int) ((Color) ref this.DrawColor).G / (int) byte.MaxValue), (float) ((int) ((Color) ref this.DrawColor).G / (int) byte.MaxValue));
      this.colorlerp += 0.15f;
      ++this.Projectile.localAI[0];
      if (!this.playedsound)
      {
        SoundStyle soundStyle = SoundID.Item122;
        ((SoundStyle) ref soundStyle).Volume = 0.5f;
        ((SoundStyle) ref soundStyle).Pitch = -0.5f;
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        this.playedsound = true;
      }
      if (Utils.NextBool(Main.rand, 6))
      {
        if (this.Projectile.extraUpdates >= 1 && !Utils.NextBool(Main.rand, this.Projectile.extraUpdates))
          return;
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num2 = this.Projectile.rotation + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 3.1415927410125732 / 2.0);
          float num3 = (float) (Main.rand.NextDouble() * 0.800000011920929 + 1.0);
          Vector2 vector2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2).\u002Ector((float) Math.Cos((double) num2) * num3, (float) Math.Sin((double) num2) * num3);
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 226, vector2.X, vector2.Y, 0, this.DrawColor, 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = 1.2f;
          Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
        }
        if (!Utils.NextBool(Main.rand, 5))
          return;
        int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width)), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 31, 0.0f, 0.0f, 100, this.DrawColor, 1.5f);
        Dust dust = Main.dust[index];
        dust.shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
        Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
      }
      float num4 = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
      ((Entity) this.Projectile).velocity = Utils.RotatedBy(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.ai[0], new Vector2()), num4), (double) this.Projectile.ai[1] * (Math.Floor(Math.Sin(((double) this.Projectile.localAI[0] - 0.78539818525314331) * 2.0)) + 0.5) * 3.1415927410125732 / 4.0, new Vector2());
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
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
      int num1 = Color.op_Equality(this.DrawColor, new Color(231, 174, 254)) ? 100 : 0;
      float num2 = (float) ((double) this.Projectile.rotation + 1.5707963705062866 + (Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 3.1415927410125732 / 2.0);
      float num3 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) Math.Cos((double) num2) * num3, (float) Math.Sin((double) num2) * num3);
      for (int index1 = 0; index1 < this.Projectile.oldPos.Length; ++index1)
      {
        int index2 = Dust.NewDust(this.Projectile.oldPos[index1], 0, 0, 229, vector2.X, vector2.Y, 0, this.DrawColor, 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].scale = 1.7f;
        Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(num1, Main.LocalPlayer);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(144, 180, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(144, 120, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.Lerp(Color.White, this.DrawColor, (float) (0.6600000262260437 + Math.Sin((double) this.colorlerp) / 3.0)));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Rectangle bounds = texture2D.Bounds;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(bounds), 2f);
      Color color = Color.Lerp(this.Projectile.GetAlpha(lightColor), Color.Black, 0.4f);
      for (int index1 = 1; index1 < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index1)
      {
        if (!Vector2.op_Equality(this.Projectile.oldPos[index1], Vector2.Zero) && !Vector2.op_Equality(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]))
        {
          Vector2 vector2_2 = Vector2.op_Subtraction(this.Projectile.oldPos[index1 - 1], this.Projectile.oldPos[index1]);
          int num1 = (int) ((Vector2) ref vector2_2).Length();
          float num2 = this.Projectile.scale * (float) Math.Sin((double) index1 * 0.5 / 3.1415927410125732);
          ((Vector2) ref vector2_2).Normalize();
          for (int index2 = 0; index2 < num1; index2 += 3)
          {
            Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index1], Vector2.op_Multiply(vector2_2, (float) index2));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), color, this.Projectile.rotation, vector2_1, num2 + 0.15f, (SpriteEffects) 1, 0.0f);
          }
        }
      }
      return false;
    }

    public virtual void PostDraw(Color lightColor)
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
          int num1 = (int) ((Vector2) ref vector2_2).Length();
          float num2 = this.Projectile.scale * (float) Math.Sin((double) index1 * 0.5 / 3.1415927410125732);
          ((Vector2) ref vector2_2).Normalize();
          for (int index2 = 0; index2 < num1; index2 += 3)
          {
            Vector2 vector2_3 = Vector2.op_Addition(this.Projectile.oldPos[index1], Vector2.op_Multiply(vector2_2, (float) index2));
            Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(bounds), alpha, this.Projectile.rotation, vector2_1, num2, (SpriteEffects) 1, 0.0f);
          }
        }
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }
  }
}
