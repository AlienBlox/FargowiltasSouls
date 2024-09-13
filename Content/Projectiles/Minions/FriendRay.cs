﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.FriendRay
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class FriendRay : BaseDeathray
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/DeviDeathray";

    public FriendRay()
      : base(120f)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.FargoSouls().CanSplit = false;
      this.Projectile.hide = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 0;
    }

    public virtual void DrawBehind(
      int index,
      List<int> behindNPCsAndTiles,
      List<int> behindNPCs,
      List<int> behindProjectiles,
      List<int> overPlayers,
      List<int> overWiresUI)
    {
      behindProjectiles.Add(index);
    }

    public virtual void AI()
    {
      if (this.Projectile.timeLeft % 5 == 0)
        this.Projectile.netUpdate = true;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (this.Projectile.owner == Main.myPlayer)
      {
        Vector2 vector2 = Vector2.op_Multiply(300f, ((Entity) Main.player[this.Projectile.owner]).DirectionFrom(Main.MouseWorld));
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.player[this.Projectile.owner]).Center, Utils.RotatedBy(vector2, (double) this.Projectile.ai[0], new Vector2()));
        ((Entity) this.Projectile).velocity = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, ((Entity) this.Projectile).Center));
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      float num1 = 0.5f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) (Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * (double) num1 * 5.0);
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        this.Projectile.rotation = rotation - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(rotation);
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2_1 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = 2000f;
        float num2 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num2 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num2 / length, 0.5f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num4 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num3) * num4, (float) Math.Sin((double) num3) * num4);
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 86, vector2_3.X, vector2_3.Y, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = 1.7f;
        }
        for (int index3 = 0; index3 < 2; ++index3)
        {
          float num5 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num6 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_4;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_4).\u002Ector((float) Math.Cos((double) num5) * num6, (float) Math.Sin((double) num5) * num6);
          int index4 = Dust.NewDust(vector2_2, 0, 0, 86, vector2_4.X, vector2_4.Y, 0, new Color(), 1f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].scale = 1.7f;
        }
        if (Utils.NextBool(Main.rand, 5))
        {
          Vector2 vector2_5 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
          int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_5), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 86, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index];
          dust.noGravity = true;
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
        }
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(119, 300, false);
    }
  }
}
