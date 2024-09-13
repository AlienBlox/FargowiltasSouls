// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantDeathray3
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
  public class MutantDeathray3 : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    private float displayMaxTime;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/PhantasmalDeathray";
    }

    public MutantDeathray3()
      : base(270f, grazeCD: 30)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.CooldownSlot = -1;
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      float num1 = WorldSavingSystem.MasochistModeReal ? 0.9716f : 0.9712f;
      FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>());
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        if (!Main.dedServ)
        {
          SoundStyle zombie104 = SoundID.Zombie104;
          ((SoundStyle) ref zombie104).Volume = 0.5f;
          SoundEngine.PlaySound(ref zombie104, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        this.displayMaxTime = Math.Min(this.maxTime, (float) (this.Projectile.timeLeft + 2));
      }
      float num2 = 1f;
      ++this.Projectile.localAI[0];
      if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
      {
        this.Projectile.Kill();
      }
      else
      {
        this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.displayMaxTime) * 6f * num2;
        if ((double) this.Projectile.scale > (double) num2)
          this.Projectile.scale = num2;
        float rotation = Utils.ToRotation(((Entity) this.Projectile).velocity);
        if ((double) this.Projectile.localAI[0] > 45.0 && (double) this.Projectile.localAI[0] < (double) this.maxTime - 120.0)
          this.Projectile.ai[0] *= num1;
        float num3 = rotation + this.Projectile.ai[0];
        this.Projectile.rotation = num3 - 1.57079637f;
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(num3);
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
        float num4 = 0.0f;
        for (int index = 0; index < numArray.Length; ++index)
          num4 += numArray[index];
        this.Projectile.localAI[1] = MathHelper.Lerp(this.Projectile.localAI[1], num4 / length, 0.5f);
        Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1] - 14f));
        for (int index1 = 0; index1 < 2; ++index1)
        {
          float num5 = Utils.ToRotation(((Entity) this.Projectile).velocity) + (float) ((Utils.NextBool(Main.rand, 2) ? -1.0 : 1.0) * 1.5707963705062866);
          float num6 = (float) (Main.rand.NextDouble() * 2.0 + 2.0);
          Vector2 vector2_3;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_3).\u002Ector((float) Math.Cos((double) num5) * num6, (float) Math.Sin((double) num5) * num6);
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

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
        target.AddBuff(67, 300, true, false);
      }
      target.AddBuff(24, 300, true, false);
    }

    public override bool PreDraw(ref Color lightColor) => false;

    public float WidthFunction(float trailInterpolant)
    {
      return (float) ((double) ((Entity) this.Projectile).width * (double) this.Projectile.scale * 1.2999999523162842);
    }

    public static Color ColorFunction(float trailInterpolant)
    {
      return new Color((int) byte.MaxValue, 0, 0, 0);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      if (this.Projectile.hide)
        return;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.GenericDeathray");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance), 1.1f));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 50f));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) vector2Array.Length - 1f));
      FargosTextureRegistry.MutantStreak.Value.SetTexture1();
      shader.TrySetParameter("mainColor", (object) new Color((int) byte.MaxValue, (int) byte.MaxValue, 183, 100));
      shader.TrySetParameter("stretchAmount", (object) 1);
      shader.TrySetParameter("scrollSpeed", (object) 3f);
      shader.TrySetParameter("uColorFadeScaler", (object) 1f);
      shader.TrySetParameter("useFadeIn", (object) true);
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), MutantDeathray3.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (MutantDeathray3.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(30));
    }
  }
}
