// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.PhantasmalDeathrayWOF
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class PhantasmalDeathrayWOF : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayWOF";
    }

    public PhantasmalDeathrayWOF()
      : base(90f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 114);
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) (((Entity) npc).width - 36), Utils.RotatedBy(Vector2.UnitX, (double) npc.rotation + (((Entity) npc).direction > 0 ? 0.0 : 3.1415927410125732), new Vector2())));
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zombie_104", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 1f;
        if (WorldSavingSystem.MasochistModeReal)
        {
          num1 = Utils.NextFloat(Main.rand, 2.5f, 5f);
          FargoSoulsUtil.ScreenshakeRumble(6f);
        }
        else
          FargoSoulsUtil.ScreenshakeRumble(3f);
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 5f * num1;
          if ((double) this.Projectile.scale > (double) num1)
            this.Projectile.scale = num1;
          ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(npc.rotation + (((Entity) npc).direction > 0 ? 0.0f : 3.14159274f));
          this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
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
          if (Utils.NextBool(Main.rand, 5))
          {
            Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
            int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
            Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
          }
          for (int index3 = 0; (double) index3 < (double) numArray[0]; index3 += 100)
          {
            if (Utils.NextBool(Main.rand, 5))
            {
              float num5 = (float) index3 + Utils.NextFloat(Main.rand, -100f, 100f);
              if ((double) num5 < 0.0)
                num5 = 0.0f;
              if ((double) num5 > (double) numArray[0])
                num5 = numArray[0];
              int index4 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, num5)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 92, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index4].noGravity = true;
              Dust dust = Main.dust[index4];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
            }
          }
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 1800, true, false);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(ModContent.BuffType<UnstableBuff>(), 300, true, false);
    }
  }
}
