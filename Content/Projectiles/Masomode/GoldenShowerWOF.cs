// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GoldenShowerWOF
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
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
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GoldenShowerWOF : ModProjectile, IPixelatedPrimitiveRenderer
  {
    public virtual string Texture => "Terraria/Images/Projectile_288";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 15;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 15;
      ((Entity) this.Projectile).height = 15;
      this.Projectile.aiStyle = -1;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 300;
      this.Projectile.hostile = true;
      this.Projectile.extraUpdates = 2;
    }

    public virtual bool? CanCutTiles() => new bool?(false);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[1] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item17, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if ((double) --this.Projectile.ai[0] < 0.0)
        this.Projectile.tileCollide = true;
      if ((double) this.Projectile.localAI[0] != 0.0)
        return;
      ((Entity) this.Projectile).velocity.Y += 0.5f * this.Projectile.ai[2];
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(69, 900, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      if (this.Projectile.timeLeft > 10)
        this.Projectile.timeLeft = 10;
      return false;
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.2999999523162842), 3.5f, completionRatio);
    }

    public static Color ColorFunction(float completionRatio)
    {
      return Color.op_Multiply(Color.Lerp(new Color(250, 250, 0), Color.Transparent, completionRatio), 0.7f);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      Texture2D texture2D = TextureAssets.Projectile[this.Projectile.type].Value;
      int num1 = TextureAssets.Projectile[this.Projectile.type].Value.Height / Main.projFrames[this.Projectile.type];
      int num2 = num1 * this.Projectile.frame;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num2, texture2D.Width, num1);
      Vector2 vector2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Vector2(0.0f, this.Projectile.gfxOffY)), new Rectangle?(rectangle), Color.White, this.Projectile.rotation, vector2, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
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
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.Projectile.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), GoldenShowerWOF.\u003C\u003EO.\u003C0\u003E__ColorFunction ?? (GoldenShowerWOF.\u003C\u003EO.\u003C0\u003E__ColorFunction = new PrimitiveSettings.VertexColorFunction((object) null, __methodptr(ColorFunction))), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__11_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(5));
    }
  }
}
