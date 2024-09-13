﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.CursedDeathrayWOFS
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class CursedDeathrayWOFS : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/CursedDeathray";
    }

    public CursedDeathrayWOFS()
      : base(240f, 0.5f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 113);
      if (npc != null && npc.GetGlobalNPC<WallofFlesh>().WorldEvilAttackCycleTimer < 270)
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        ((Entity) this.Projectile).position.X += 700f * this.Projectile.ai[0];
        ((Entity) this.Projectile).position.Y -= 1500f;
        if (npc.life > npc.lifeMax / 10)
          ((Entity) this.Projectile).position.X -= 24f * this.Projectile.ai[0];
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        float num1 = 0.2f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 10f * num1;
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
            numArray[index] = 3000f;
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
            int index2 = Dust.NewDust(vector2_2, 0, 0, 244, vector2_3.X, vector2_3.Y, 0, new Color(), 1f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].scale = 1.7f;
          }
          if (!Utils.NextBool(Main.rand, 5))
            return;
          Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
          int index3 = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index3];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index3].velocity.Y = -Math.Abs(Main.dust[index3].velocity.Y);
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual bool? CanDamage() => new bool?(false);
  }
}
