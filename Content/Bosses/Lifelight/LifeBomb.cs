// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeBomb : ModProjectile, IPixelatedPrimitiveRenderer
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailingMode[this.Type] = 1;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 20;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 22;
      ((Entity) this.Projectile).height = 22;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = true;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      this.Projectile.ai[0] += 2f;
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0, new Color(), 0.25f);
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) - 1.57079637f;
      if ((double) this.Projectile.ai[0] >= 60.0)
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.96f);
      if ((double) this.Projectile.ai[0] < 100.0)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      int damage = this.Projectile.damage;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2), ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2), 0.0f, 0.0f, ModContent.ProjectileType<LifeBombExplosion>(), damage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY));
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      for (int index = 0; index < 12; ++index)
      {
        Vector2 vector2_3 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), 1f);
        Color color = Color.op_Multiply(new Color(1f, 1f, 0.0f, 0.0f), 0.7f);
        Main.spriteBatch.Draw(texture2D, Vector2.op_Addition(vector2_1, vector2_3), new Rectangle?(rectangle), this.Projectile.GetAlpha(color), this.Projectile.rotation, vector2_2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, vector2_1, new Rectangle?(rectangle), this.Projectile.GetAlpha(lightColor), this.Projectile.rotation, vector2_2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.6000000238418579), 3.5f, completionRatio);
    }

    public Color ColorFunction(float completionRatio)
    {
      return Color.op_Multiply(Color.Lerp(Color.Gold, Color.Transparent, completionRatio), 0.7f);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.Projectile.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), new PrimitiveSettings.VertexColorFunction((object) this, __methodptr(ColorFunction)), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__10_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(44));
    }
  }
}
