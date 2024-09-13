// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeRuneHitbox
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeRuneHitbox : ModProjectile, IPixelatedPrimitiveRenderer
  {
    public int Timer;
    private const string PartsPath = "FargowiltasSouls/Assets/ExtraTextures/LifelightParts/";

    public virtual string Texture => "FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Rune1";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailingMode[this.Type] = 1;
      ProjectileID.Sets.TrailCacheLength[this.Type] = 20;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 18;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = true;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 6000;
    }

    public virtual bool? CanDamage() => this.Timer <= 15 ? new bool?(false) : base.CanDamage();

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      float num = 0.0f;
      return Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), ((Entity) this.Projectile).Center, this.Projectile.oldPos[1], (float) ((Entity) this.Projectile).width, ref num) ? new bool?(true) : new bool?(false);
    }

    private Color GetColor()
    {
      int num = (int) this.Projectile.ai[1];
      if (num % 3 == 0)
        return Color.Cyan;
      return num % 3 == 1 ? Color.Goldenrod : Color.DeepPink;
    }

    public virtual void AI()
    {
      NPC npc = Main.npc[(int) this.Projectile.ai[0]];
      if (!npc.TypeAlive<LifeChallenger>())
        this.Projectile.Kill();
      float num1 = npc.localAI[0];
      float num2 = npc.localAI[1] + 6.28318548f / (float) (int) npc.localAI[2] * (float) (int) this.Projectile.ai[1];
      Vector2 vector2 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num2), num1));
      this.Projectile.rotation = num2 + 1.57079637f;
      ((Entity) this.Projectile).Center = vector2;
      ++this.Timer;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      int num = (int) this.Projectile.ai[1];
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      interpolatedStringHandler.AppendLiteral("Rune");
      interpolatedStringHandler.AppendFormatted<int>(num + 1);
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/" + interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 1).Value;
      for (int index = 0; index < 12; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index / 12.0)), 1f);
        Color color = num % 3 != 0 ? (num % 3 != 1 ? Color.op_Multiply(new Color(1f, 0.7529412f, 0.796078444f, 0.0f), 0.7f) : Color.op_Multiply(new Color(1f, 1f, 0.0f, 0.0f), 0.7f)) : Color.op_Multiply(new Color(0.0f, 1f, 1f, 0.0f), 0.7f);
        Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).Center, vector2), Main.screenPosition), new Rectangle?(), color, this.Projectile.rotation, Vector2.op_Multiply(Utils.Size(texture2D), 0.5f), this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      }
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) (texture2D.Width / 2), (float) (texture2D.Height / 2));
      Main.EntitySpriteDraw(texture2D, Vector2.op_Subtraction(((Entity) this.Projectile).Center, Main.screenPosition), new Rectangle?(), Color.White, this.Projectile.rotation, vector2_1, this.Projectile.scale, (SpriteEffects) 0, 0.0f);
      return false;
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep((float) ((double) this.Projectile.scale * (double) ((Entity) this.Projectile).width * 1.2999999523162842), 3.5f, completionRatio);
    }

    public Color ColorFunction(float completionRatio)
    {
      return Color.op_Multiply(Color.Lerp(this.GetColor(), Color.Transparent, completionRatio), 0.7f);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.Projectile.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), new PrimitiveSettings.VertexColorFunction((object) this, __methodptr(ColorFunction)), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__14_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(44));
    }
  }
}
