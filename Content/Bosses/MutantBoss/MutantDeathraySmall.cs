// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantDeathraySmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantDeathraySmall : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathrayML";
    }

    public MutantDeathraySmall()
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
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      float num1 = 0.3f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime || WorldSavingSystem.MasochistModeReal)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 0.6f * num1;
        if ((double) this.Projectile.scale > (double) num1)
          this.Projectile.scale = num1;
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
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
      }
    }

    public override bool PreDraw(ref Color lightColor) => false;

    public float WidthFunction(float trailInterpolant)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 1.2999999523162842);
    }

    public static Color ColorFunction(float trailInterpolant)
    {
      Color color = FargoSoulsUtil.AprilFools ? Color.Red : Color.Cyan;
      ((Color) ref color).A = (byte) 100;
      return color;
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      if (this.Projectile.hide)
        return;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.GenericDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance), 1.1f));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 150f));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      FargosTextureRegistry.MutantStreak.Value.SetTexture1();
      shader.TrySetParameter("mainColor", (object) (FargoSoulsUtil.AprilFools ? new Color((int) byte.MaxValue, (int) byte.MaxValue, 183, 100) : new Color(183, 252, 253, 100)));
      shader.TrySetParameter("stretchAmount", (object) 3);
      shader.TrySetParameter("scrollSpeed", (object) 2f);
      shader.TrySetParameter("uColorFadeScaler", (object) 1f);
      shader.TrySetParameter("useFadeIn", (object) true);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), MutantDeathraySmall.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (MutantDeathraySmall.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(20));
    }
  }
}
