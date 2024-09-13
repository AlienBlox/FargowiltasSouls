﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.RoseTintedVisorDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class RoseTintedVisorDeathray : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    private const int MaxLength = 1000;
    private int Length = 10;
    private bool CanHit = true;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/DeviDeathray";

    public RoseTintedVisorDeathray()
      : base(30f, drawDistance: 1000)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 30;
      this.Projectile.DamageType = DamageClass.Magic;
    }

    public virtual bool? CanHitNPC(NPC target)
    {
      return !this.CanHit ? new bool?(false) : base.CanHitNPC(target);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.CanHit = false;
      int length = this.Length;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) target).Center, ((Entity) this.Projectile).Center);
      int num = (int) ((Vector2) ref vector2_1).Length();
      if (length <= num)
        return;
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) target).Center, ((Entity) this.Projectile).Center);
      this.Length = (int) ((Vector2) ref vector2_2).Length();
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (this.CanHit && this.Length < 1000)
        this.Length += 60;
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
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 3f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
        float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
        this.Projectile.rotation = num2 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2);
        float length = 3f;
        int width = ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
        {
          Vector2 vector2_1 = nullable.Value;
        }
        float[] numArray = new float[(int) length];
        for (int index = 0; index < numArray.Length; ++index)
          numArray[index] = (float) this.Length;
        float num3 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num3 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 1; ++index1)
        {
          float num4 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num5 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num4) * num5, (float) Math.Sin((double) num4) * num5);
          int index2 = Dust.NewDust(vector2_2, 0, 0, 86, vector2_3.X, vector2_3.Y, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
        }
        if (Utils.NextBool(Main.rand, 5))
        {
          Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
          int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 86, 0.0f, 0.0f, 100, new Color(), 1f);
          Dust dust = Main.dust[index];
          dust.noGravity = true;
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
        }
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        for (int index3 = 0; index3 < 1; ++index3)
        {
          float num6 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num7 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_5;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_5).\u002Ector((float) Math.Cos((double) num6) * num7, (float) Math.Sin((double) num6) * num7);
          int index4 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 86, vector2_5.X, vector2_5.Y, 0, new Color(), 1f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].scale = 1f;
        }
      }
    }

    public float WidthFunction(float _)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 1.0);
    }

    public static Color ColorFunction(float _)
    {
      Color deepPink = Color.DeepPink;
      ((Color) ref deepPink).A = (byte) 0;
      return deepPink;
    }

    public override bool PreDraw(ref Color lightColor) => false;

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      if (this.Projectile.hide)
        return;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.GenericDeathray");
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), this.Projectile.localAI[1]), 1.1f));
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(center, vector2, (float) index / ((float) vector2Array.Length - 1f));
      shader.TrySetParameter("mainColor", (object) new Color(240, 220, 240, 0));
      FargosTextureRegistry.GenericStreak.Value.SetTexture1();
      shader.TrySetParameter("stretchAmount", (object) 3);
      shader.TrySetParameter("scrollSpeed", (object) 1f);
      shader.TrySetParameter("uColorFadeScaler", (object) 0.8f);
      shader.TrySetParameter("useFadeIn", (object) false);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), RoseTintedVisorDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (RoseTintedVisorDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(10));
    }
  }
}
