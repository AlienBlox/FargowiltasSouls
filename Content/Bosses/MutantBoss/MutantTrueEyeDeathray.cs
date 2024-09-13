// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantTrueEyeDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
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
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantTrueEyeDeathray : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    public virtual string Texture
    {
      get
      {
        return "FargowiltasSouls/Content/Projectiles/Deathrays/" + (FargoSoulsUtil.AprilFools ? "PhantasmalDeathray" : "PhantasmalDeathrayML");
      }
    }

    public MutantTrueEyeDeathray()
      : base(90f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual bool CanHitPlayer(Player target) => target.hurtCooldowns[1] == 0;

    public virtual void AI()
    {
      this.Projectile.hide = false;
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        SoundStyle zombie104 = SoundID.Zombie104;
        ((SoundStyle) ref zombie104).Volume = 0.5f;
        SoundEngine.PlaySound(ref zombie104, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      float num1 = 0.4f;
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
        float num2 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
        this.Projectile.rotation = num2 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num2);
        float length = 3f;
        float width = (float) ((Entity) this.Projectile).width;
        Vector2 center = ((Entity) this.Projectile).Center;
        if (nullable.HasValue)
          center = nullable.Value;
        float[] numArray = new float[(int) length];
        Collision.LaserScan(center, ((Entity) this.Projectile).velocity, width * this.Projectile.scale, 3000f, numArray);
        float num3 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num3 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num3 / length, 0.5f);
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num4 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num5 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector((float) Math.Cos((double) num4) * num5, (float) Math.Sin((double) num4) * num5);
          int index2 = Dust.NewDust(vector2_1, 0, 0, 244, vector2_2.X, vector2_2.Y, 0, new Color(), 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = 1.7f;
        }
        if (Utils.NextBool(Main.rand, 5))
        {
          Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
          int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_1, vector2_3), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
        }
        DelegateMethods.v3_1 = new Vector3(0.3f, 0.65f, 0.7f);
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        Utils.PlotTileLine(((Entity) this.Projectile).Center, Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1])), (float) ((Entity) this.Projectile).width * this.Projectile.scale, MutantTrueEyeDeathray.\u003C\u003EO.\u003C0\u003E__CastLight ?? (MutantTrueEyeDeathray.\u003C\u003EO.\u003C0\u003E__CastLight = new Utils.TileActionAttempt((object) null, __methodptr(CastLight))));
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 360, true, false);
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
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance), 1.1f));
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(center, vector2, (float) index / ((float) vector2Array.Length - 1f));
      FargosTextureRegistry.MutantStreak.Value.SetTexture1();
      shader.TrySetParameter("mainColor", (object) (FargoSoulsUtil.AprilFools ? new Color(253, 252, 183, 100) : new Color(183, 252, 253, 100)));
      shader.TrySetParameter("stretchAmount", (object) 3);
      shader.TrySetParameter("scrollSpeed", (object) 2f);
      shader.TrySetParameter("uColorFadeScaler", (object) 1f);
      shader.TrySetParameter("useFadeIn", (object) true);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), MutantTrueEyeDeathray.\u003C\u003EO.\u003C1\u003E__ColorFunction ?? (MutantTrueEyeDeathray.\u003C\u003EO.\u003C1\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(20));
    }
  }
}
