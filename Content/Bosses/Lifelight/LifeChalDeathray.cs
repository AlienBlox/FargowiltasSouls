// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeChalDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeChalDeathray : BaseDeathray, IPixelatedPrimitiveRenderer
  {
    private PixelationPrimitiveLayer LayerToRenderTo => (PixelationPrimitiveLayer) 3;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomDeathray";

    public LifeChalDeathray()
      : base(3600f)
    {
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft *= 10;
    }

    public virtual void AI()
    {
      Vector2? nullable = new Vector2?();
      if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
        ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], ModContent.NPCType<LifeChallenger>());
      if (npc == null || !((Entity) npc).active || npc.type != ModContent.NPCType<LifeChallenger>())
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        LifeChallenger modNpc = (LifeChallenger) npc.ModNPC;
        this.Projectile.rotation = Utils.ToRotation(Utils.RotatedBy(modNpc.LockVector1, modNpc.rot, new Vector2()));
        ((Entity) this.Projectile).velocity = Utils.ToRotationVector2(this.Projectile.rotation);
        this.maxTime = this.Projectile.ai[2];
        if (Utils.HasNaNs(((Entity) this.Projectile).velocity) || Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(Vector2.UnitY);
        double num1 = (double) this.Projectile.localAI[0];
        float num2 = 1f;
        ++this.Projectile.localAI[0];
        if ((double) this.Projectile.localAI[0] >= (double) this.maxTime)
        {
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) Math.Sin((double) this.Projectile.localAI[0] * 3.1415927410125732 / (double) this.maxTime) * 2f * num2;
          if ((double) this.Projectile.scale > (double) num2)
            this.Projectile.scale = num2;
          float num3 = Utils.ToRotation(((Entity) this.Projectile).velocity) + this.Projectile.ai[0];
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
          if (Utils.NextBool(Main.rand, 5))
          {
            Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.Projectile).velocity, 1.5707963705062866, new Vector2()), (float) Main.rand.NextDouble() - 0.5f), (float) ((Entity) this.Projectile).width);
            int index = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_2, vector2_4), Vector2.op_Multiply(Vector2.One, 4f)), 8, 8, 244, 0.0f, 0.0f, 100, new Color(), 1.5f);
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
            Main.dust[index].velocity.Y = -Math.Abs(Main.dust[index].velocity.Y);
          }
          this.SpawnSparks();
        }
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public void SpawnSparks()
    {
      if (!Utils.NextBool(Main.rand, (int) MathHelper.Lerp(3f, 20f, Utils.GetLerpValue(1f, 0.0f, this.Projectile.scale, false))) || Main.netMode == 2)
        return;
      Vector2 worldPosition = Vector2.op_Addition(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, 30f)), Utils.NextVector2Circular(Main.rand, 10f, 10f));
      Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(Utils.NextFloat(Main.rand, 6.28318548f)), Utils.NextFloat(Main.rand, 7f, 15f));
      Color color = Color.Lerp(Color.Gold, Color.OrangeRed, Utils.NextFloat(Main.rand, 0.0f, 0.5f));
      if (Utils.NextBool(Main.rand, 3))
        color = Color.Lerp(color, Color.Pink, Utils.NextFloat(Main.rand, 0.0f, 0.6f));
      Vector2 velocity = vector2;
      Color drawColor = color;
      double scale = (double) Utils.NextFloat(Main.rand, 1.5f, 1.9f);
      int lifetime = Main.rand.Next(25, 45);
      Color? bloomColor = new Color?(Color.PaleGoldenrod);
      new SparkParticle(worldPosition, velocity, drawColor, (float) scale, lifetime, bloomColor: bloomColor).Spawn();
    }

    public override bool PreDraw(ref Color lightColor) => false;

    public float WidthFunction(float completionRatio)
    {
      return this.Projectile.scale * (float) ((Entity) this.Projectile).width;
    }

    public Color ColorFunction(float completionRatio)
    {
      return Color.Lerp(Color.Lerp(Color.Gold, Color.PaleGoldenrod, (float) ((double) MathF.Sin((float) ((double) Main.GlobalTimeWrappedHourly * -3.2000000476837158 + (double) completionRatio * 23.0)) * 0.5 + 0.5) * 0.67f), Color.White, 0.1f);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.LifelightDeathray");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      Vector2 center = ((Entity) this.Projectile).Center;
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(((Entity) this.Projectile).velocity, this.Projectile.localAI[1]));
      Vector2[] vector2Array = new Vector2[8];
      for (int index = 0; index < vector2Array.Length; ++index)
        vector2Array[index] = Vector2.Lerp(center, vector2_1, (float) index / (float) vector2Array.Length);
      shader.TrySetParameter("mainColor", (object) Color.Pink);
      FargosTextureRegistry.SmokyNoise.Value.SetTexture1();
      FargosTextureRegistry.WavyNoise.Value.SetTexture2();
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) vector2Array, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), new PrimitiveSettings.VertexColorFunction((object) this, __methodptr(ColorFunction)), (PrimitiveSettings.VertexOffsetFunction) null, true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(40));
      Vector2 vector2_2 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(Utils.SafeNormalize(((Entity) this.Projectile).velocity, Vector2.Zero), 30f)), Main.screenPosition);
      double lerpValue = (double) Utils.GetLerpValue(0.0f, 5f, this.Projectile.localAI[0], true);
      float num1 = MathHelper.Lerp(0.05f, 0.175f, FargoSoulsUtil.SineInOut(this.Projectile.scale));
      Texture2D texture2D1 = FargosTextureRegistry.BloomFlareTexture.Value;
      Texture2D texture2D2 = FargosTextureRegistry.BloomParticleTexture.Value;
      float num2 = Main.GlobalTimeWrappedHourly * 1.1f;
      Color orangeRed = Color.OrangeRed;
      ((Color) ref orangeRed).A = (byte) 0;
      Color color1 = Color.op_Multiply(orangeRed, 0.7f);
      Color gold1 = Color.Gold;
      ((Color) ref gold1).A = (byte) 0;
      Color color2 = Color.op_Multiply(gold1, 0.7f);
      float num3 = MathHelper.Lerp(0.3f, 0.4f, (float) ((1.0 + (double) MathF.Sin(3.14159274f * Main.GlobalTimeWrappedHourly)) * 0.5));
      SpriteBatch spriteBatch1 = Main.spriteBatch;
      Texture2D texture2D3 = texture2D2;
      Vector2 vector2_3 = Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition);
      Rectangle? nullable1 = new Rectangle?();
      Color gold2 = Color.Gold;
      ((Color) ref gold2).A = (byte) 0;
      Color color3 = Color.op_Multiply(gold2, num3);
      Vector2 vector2_4 = Vector2.op_Multiply(Utils.Size(texture2D2), 0.5f);
      spriteBatch1.Draw(texture2D3, vector2_3, nullable1, color3, 0.0f, vector2_4, 10f, (SpriteEffects) 0, 0.0f);
      float num4 = MathHelper.Lerp(1.2f, 1.6f, (float) ((1.0 + (double) MathF.Sin((float) (3.1415927410125732 * (double) Main.GlobalTimeWrappedHourly * 9.5))) * 0.5));
      SpriteBatch spriteBatch2 = Main.spriteBatch;
      Texture2D texture2D4 = texture2D2;
      Vector2 vector2_5 = vector2_2;
      Rectangle? nullable2 = new Rectangle?();
      Color paleGoldenrod = Color.PaleGoldenrod;
      ((Color) ref paleGoldenrod).A = (byte) 0;
      Color color4 = paleGoldenrod;
      Vector2 vector2_6 = Vector2.op_Multiply(Utils.Size(texture2D2), 0.5f);
      double num5 = (double) num4;
      spriteBatch2.Draw(texture2D4, vector2_5, nullable2, color4, 0.0f, vector2_6, (float) num5, (SpriteEffects) 0, 0.0f);
      Main.spriteBatch.Draw(texture2D1, vector2_2, new Rectangle?(), color1, num2, Vector2.op_Multiply(Utils.Size(texture2D1), 0.5f), num1, (SpriteEffects) 0, 0.0f);
      Main.spriteBatch.Draw(texture2D1, vector2_2, new Rectangle?(), color2, -num2, Vector2.op_Multiply(Utils.Size(texture2D1), 0.5f), num1, (SpriteEffects) 0, 0.0f);
    }

    public void DrawPixelPrimitives(SpriteBatch spriteBatch)
    {
    }
  }
}
