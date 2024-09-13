// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CoffinWaveShot
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CoffinWaveShot : ModProjectile, IPixelatedPrimitiveRenderer
  {
    private static readonly Color GlowColor = new Color(224, 196, 252, 0);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 10;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.timeLeft = 180;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] < 12.0)
      {
        ++this.Projectile.localAI[0];
        this.Projectile.scale = MathHelper.Lerp(0.0f, 1f, this.Projectile.localAI[0] / 12f);
      }
      ProjectileID.Sets.TrailCacheLength[this.Type] = 8;
      ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) (1.57079637f * ((double) this.Projectile.ai[0] == 0.0 ? 0.06f : 0.03f) * MathF.Sin((float) (6.2831854820251465 * ((double) this.Projectile.ai[1] / 50.0)))), new Vector2());
      float num = WorldSavingSystem.MasochistModeReal ? 1.02f : 1.016f;
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 15.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, num);
      }
      ++this.Projectile.ai[1];
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 240, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.25), 0.0f, completionRatio);
    }

    public static Color ColorFunction(float completionRatio)
    {
      Color color1 = Color.Lerp(Color.Lerp(Color.MediumPurple, Color.DeepPink, 0.5f), CoffinWaveShot.GlowColor, 0.5f);
      Color glowColor = CoffinWaveShot.GlowColor;
      ((Color) ref glowColor).A = (byte) 100;
      Color color2 = Color.op_Multiply(glowColor, 0.5f);
      double num = (double) completionRatio;
      return Color.Lerp(color1, color2, (float) num);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.Projectile.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), CoffinWaveShot.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (CoffinWaveShot.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__8_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(44));
    }
  }
}
