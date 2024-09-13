// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GuardianDeathraySmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Deathrays;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GuardianDeathraySmall : BaseDeathray
  {
    private Vector2 offset;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/GuardianDeathray";
    }

    public GuardianDeathraySmall()
      : base(30f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 68) != null)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
          this.offset = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) Main.npc[(int) this.Projectile.ai[1]]).Center);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.npc[(int) this.Projectile.ai[1]]).Center, this.offset);
      }
      else if ((double) this.Projectile.ai[0] > -1.0)
      {
        if ((double) this.Projectile.localAI[0] == 0.0)
          this.offset = Vector2.op_Subtraction(((Entity) this.Projectile).Center, ((Entity) Main.player[(int) this.Projectile.ai[0]]).Center);
        ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center, this.offset);
      }
      else
      {
        this.Projectile.Kill();
        return;
      }
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      float num1 = 0.15f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 1f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
        this.Projectile.rotation = num2;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2 + 1.57079637f);
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
        float num3 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num3 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num4 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num5 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num4) * num5, (float) Math.Sin((double) num4) * num5);
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
  }
}
