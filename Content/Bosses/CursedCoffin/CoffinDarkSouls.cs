// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.CursedCoffin.CoffinDarkSouls
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.CursedCoffin
{
  public class CoffinDarkSouls : ModProjectile
  {
    private const int TrailLength = 10;
    public int[] oldFrame = new int[10];
    private static readonly Color GlowColor = new Color(224, 196, 252, 0);

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Type] = 10;
      ProjectileID.Sets.TrailingMode[this.Type] = 2;
      Main.projFrames[this.Type] = 4;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.light = 1f;
      this.Projectile.timeLeft = 360;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if ((double) this.Projectile.localAI[0] < 25.0)
      {
        ++this.Projectile.localAI[0];
        this.Projectile.scale = MathHelper.Lerp(0.15f, 1f, this.Projectile.localAI[0] / 25f);
      }
      for (int index1 = 0; index1 < this.oldFrame.Length; ++index1)
      {
        int index2 = this.oldFrame.Length - index1 - 1;
        this.oldFrame[index2] = index2 <= 0 ? this.Projectile.frame : this.oldFrame[index2 - 1];
      }
      this.Projectile.Animate(6);
      if ((double) this.Projectile.ai[1] != 0.0)
        ((Entity) this.Projectile).velocity.Y += this.Projectile.ai[1];
      if (this.Projectile.timeLeft > 20)
        return;
      this.Projectile.Opacity -= 0.05f;
      this.Projectile.scale -= 0.05f;
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
      Rectangle rectangle1;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle1).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(rectangle1), 2f);
      Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(Utils.ToRotationVector2(this.Projectile.rotation), (float) (texture2D.Width - ((Entity) this.Projectile).width)), 2f);
      SpriteEffects spriteEffects = this.Projectile.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.GameViewMatrix.TransformationMatrix);
      for (int index = 0; index < ProjectileID.Sets.TrailCacheLength[this.Projectile.type]; ++index)
      {
        Color glowColor = CoffinDarkSouls.GlowColor;
        ((Color) ref glowColor).A = byte.MaxValue;
        Color color = Color.op_Multiply(glowColor, (float) (ProjectileID.Sets.TrailCacheLength[this.Projectile.type] - index) / (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type]);
        Vector2 oldPo = this.Projectile.oldPos[index];
        float num3 = this.Projectile.oldRot[index];
        int num4 = num1 * this.oldFrame[index];
        Rectangle rectangle2;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle2).\u002Ector(0, num4, texture2D.Width, num1);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(oldPo, vector2_2), Vector2.op_Division(((Entity) this.Projectile).Size, 2f)), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle2), color, num3, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      }
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_2), Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle1), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_1, this.Projectile.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
