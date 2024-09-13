// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.DeviBigDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public class DeviBigDeathray : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/DeviDeathray";

    public static List<Asset<Texture2D>> RingTextures
    {
      get
      {
        List<Asset<Texture2D>> ringTextures = new List<Asset<Texture2D>>();
        CollectionsMarshal.SetCount<Asset<Texture2D>>(ringTextures, 4);
        Span<Asset<Texture2D>> span = CollectionsMarshal.AsSpan<Asset<Texture2D>>(ringTextures);
        int num1 = 0;
        span[num1] = FargosTextureRegistry.DeviRingTexture;
        int num2 = num1 + 1;
        span[num2] = FargosTextureRegistry.DeviRing2Texture;
        int num3 = num2 + 1;
        span[num3] = FargosTextureRegistry.DeviRing3Texture;
        int num4 = num3 + 1;
        span[num4] = FargosTextureRegistry.DeviRing4Texture;
        int num5 = num4 + 1;
        return ringTextures;
      }
    }

    public DeviBigDeathray()
      : base(180f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual void AI()
    {
      if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>());
      if (npc != null)
      {
        ((Entity) this.Projectile).Center = Vector2.op_Addition(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 300f)), Utils.NextVector2Circular(Main.rand, 20f, 20f));
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        if ((double) this.Projectile.localAI[0] == 0.0 && !Main.dedServ)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/DeviBigDeathray", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        }
        float num1 = 17f;
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
            if (Utils.NextBool(Main.rand, 3))
            {
              float num6 = (float) index3 + Utils.NextFloat(Main.rand, -100f, 100f);
              if ((double) num6 < 0.0)
                num6 = 0.0f;
              if ((double) num6 > (double) numArray[0])
                num6 = numArray[0];
              int index4 = Dust.NewDust(Vector2.op_Addition(((Entity) this.Projectile).position, Vector2.op_Multiply(((Entity) this.Projectile).velocity, num6)), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 86, 0.0f, 0.0f, 0, new Color(), Utils.NextFloat(Main.rand, 4f, 8f));
              Main.dust[index4].noGravity = true;
              Dust dust1 = Main.dust[index4];
              dust1.velocity = Vector2.op_Addition(dust1.velocity, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.5f));
              Dust dust2 = Main.dust[index4];
              dust2.velocity = Vector2.op_Multiply(dust2.velocity, Utils.NextFloat(Main.rand, 12f, 24f));
            }
          }
        }
      }
      else
        this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(69, 2, true, false);
      target.AddBuff(195, 2, true, false);
      target.AddBuff(36, 2, true, false);
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 360, true, false);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 1800, true, false);
      ((Entity) target).velocity.X = 0.0f;
      ((Entity) target).velocity.Y = -0.4f;
    }

    public float WidthFunction(float trailInterpolant)
    {
      return (float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 0.699999988079071);
    }

    public static Color[] DeviColors
    {
      get
      {
        return new Color[4]
        {
          new Color(216, 108, 224, 100),
          new Color(232, 140, 240, 100),
          new Color(224, 16, 216, 100),
          new Color(240, 220, 240, 100)
        };
      }
    }

    public static Color ColorFunction(float trailInterpolant)
    {
      Color color = Color.op_Multiply(Color.Lerp(Color.MediumVioletRed, Color.Purple, (float) ((0.5 * (1.0 + Math.Sin(1.5 * (double) Main.GlobalTimeWrappedHourly % 1.0)) + (1.0 - (double) trailInterpolant)) / 2.0)), 2f);
      ((Color) ref color).A = (byte) 100;
      return color;
    }

    public override bool PreDraw(ref Color lightColor) => false;

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero) || this.Projectile.hide)
        return;
      ManagedShader shader1 = ShaderManager.GetShader("FargowiltasSouls.DeviTouhouDeathray");
      ManagedShader shader2 = ShaderManager.GetShader("FargowiltasSouls.DeviRing");
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), (float) this.drawDistance));
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 300f));
      Vector2[] baseDrawPoints = new Vector2[8];
      for (int index = 0; index < baseDrawPoints.Length; ++index)
        baseDrawPoints[index] = Vector2.Lerp(vector2_2, vector2_1, (float) index / ((float) baseDrawPoints.Length - 1f));
      this.DrawRings(baseDrawPoints, true, shader2);
      shader1.TrySetParameter("mainColor", (object) Color.op_Multiply(new Color((int) byte.MaxValue, 180, 243, 100), 2f));
      FargosTextureRegistry.DeviBackStreak.Value.SetTexture1();
      FargosTextureRegistry.DeviInnerStreak.Value.SetTexture2();
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) baseDrawPoints, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), DeviBigDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (DeviBigDeathray.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader1, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(50));
      this.DrawRings(baseDrawPoints, false, shader2);
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Projectiles/GlowRing", (AssetRequestMode) 2).Value;
    }

    public float RingWidthFunction(float trailInterpolant) => this.Projectile.scale * 5f;

    public static Color RingColorFunction(float trailInterpolant)
    {
      Color color = Color.op_Multiply(Color.Lerp(Color.Blue, Color.Red, trailInterpolant), 2f);
      ((Color) ref color).A = (byte) 100;
      return color;
    }

    private void DrawRings(Vector2[] baseDrawPoints, bool inBackground, ManagedShader ring)
    {
      Vector2 vector2_1 = Vector2.op_Multiply(Utils.RotatedBy(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.UnitY), 1.5707963705062866, new Vector2()), 1250f);
      int index1 = 0;
      for (int index2 = 1; index2 <= baseDrawPoints.Length / 2; ++index2)
      {
        Vector2 baseDrawPoint = baseDrawPoints[index2];
        float num1 = MathHelper.Lerp(1.05f, 0.85f, (float) index2 / (float) baseDrawPoints.Length);
        vector2_1 = Vector2.op_Multiply(vector2_1, num1);
        Vector2 vector2_2 = Vector2.op_Subtraction(baseDrawPoint, Vector2.op_Multiply(vector2_1, 0.5f));
        Vector2[] vector2Array = new Vector2[10];
        for (int index3 = 0; index3 < vector2Array.Length; ++index3)
        {
          vector2Array[index3] = Vector2.Lerp(vector2_2, Vector2.op_Addition(vector2_2, vector2_1), (float) index3 / (float) vector2Array.Length);
          Vector2 vector2_3 = Vector2.op_Addition(vector2_2, Vector2.op_Division(vector2_1, 2f));
          Vector2 vector2_4 = Vector2.op_Subtraction(vector2_3, vector2Array[index3]);
          Vector2 vector2_5 = Vector2.op_Subtraction(vector2_3, vector2_2);
          float num2 = MathHelper.SmoothStep(1f, 0.0f, ((Vector2) ref vector2_4).Length() / ((Vector2) ref vector2_5).Length());
          if (inBackground)
          {
            ref Vector2 local = ref vector2Array[index3];
            local = Vector2.op_Addition(local, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2), 75f));
          }
          else
          {
            ref Vector2 local = ref vector2Array[index3];
            local = Vector2.op_Subtraction(local, Vector2.op_Multiply(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num2), 75f));
          }
        }
        ring.TrySetParameter("mainColor", (object) new Color(216, 108, 224, 100));
        DeviBigDeathray.RingTextures[index1].Value.SetTexture1();
        ring.TrySetParameter("stretchAmount", (object) 0.2f);
        float num3 = MathHelper.Lerp(1f, 1.3f, (float) (1 - index2 / (baseDrawPoints.Length / 2 - 1)));
        ring.TrySetParameter("scrollSpeed", (object) num3);
        ring.TrySetParameter("reverseDirection", (object) inBackground);
        float num4 = 1f;
        if (inBackground)
          num4 = 0.5f;
        ring.TrySetParameter("opacity", (object) num4);
        // ISSUE: method pointer
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method pointer
        PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(RingWidthFunction)), DeviBigDeathray.\u003C\u003EO.\u003C1\u003E__RingColorFunction ?? (DeviBigDeathray.\u003C\u003EO.\u003C1\u003E__RingColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(RingColorFunction))), (PrimitiveSettings.VertexOffsetFunction) null, true, true, ring, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(30));
        ++index1;
      }
    }
  }
}
