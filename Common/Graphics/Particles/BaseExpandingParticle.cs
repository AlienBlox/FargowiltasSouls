// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Particles.BaseExpandingParticle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Common.Graphics.Particles
{
  public abstract class BaseExpandingParticle : Particle
  {
    public readonly Vector2 StartScale;
    public readonly Vector2 EndScale;
    public Color BloomColor;
    public readonly bool UseBloom;

    public virtual string AtlasTextureName => "FargowiltasSouls.Bloom";

    public virtual Vector2 DrawScale => Vector2.op_Multiply(this.Scale, 0.3f);

    public BaseExpandingParticle(
      Vector2 position,
      Vector2 velocity,
      Color drawColor,
      Vector2 startScale,
      Vector2 endScale,
      int lifetime,
      bool useExtraBloom = false,
      Color? extraBloomColor = null)
    {
      this.Position = position;
      this.Velocity = velocity;
      this.DrawColor = drawColor;
      this.Scale = this.StartScale = startScale;
      this.EndScale = endScale;
      this.Lifetime = lifetime;
      this.UseBloom = useExtraBloom;
      extraBloomColor.GetValueOrDefault();
      if (!extraBloomColor.HasValue)
        extraBloomColor = new Color?(Color.White);
      this.BloomColor = extraBloomColor.Value;
    }

    public virtual void Update()
    {
      this.Opacity = MathHelper.Lerp(1f, 0.0f, FargoSoulsUtil.SineInOut(this.LifetimeRatio));
      this.Scale = Vector2.Lerp(this.StartScale, this.EndScale, FargoSoulsUtil.SineInOut(this.LifetimeRatio));
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      SpriteBatch spriteBatch1 = spriteBatch;
      AtlasTexture texture1 = this.Texture;
      Vector2 vector2_1 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
      Rectangle? frame = this.Frame;
      Color drawColor = this.DrawColor;
      ((Color) ref drawColor).A = (byte) 0;
      Color color1 = Color.op_Multiply(drawColor, this.Opacity);
      double rotation1 = (double) this.Rotation;
      Vector2? nullable1 = new Vector2?();
      Vector2? nullable2 = new Vector2?(this.DrawScale);
      SpriteEffects spriteDirection1 = Luminance.Common.Utilities.Utilities.ToSpriteDirection(this.Direction);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch1, texture1, vector2_1, frame, color1, (float) rotation1, nullable1, nullable2, spriteDirection1);
      if (!this.UseBloom)
        return;
      SpriteBatch spriteBatch2 = spriteBatch;
      AtlasTexture texture2 = this.Texture;
      Vector2 vector2_2 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
      Rectangle? nullable3 = new Rectangle?();
      Color bloomColor = this.BloomColor;
      ((Color) ref bloomColor).A = (byte) 0;
      Color color2 = Color.op_Multiply(Color.op_Multiply(bloomColor, 0.4f), this.Opacity);
      double rotation2 = (double) this.Rotation;
      Vector2? nullable4 = new Vector2?();
      Vector2? nullable5 = new Vector2?(Vector2.op_Multiply(this.DrawScale, 0.66f));
      SpriteEffects spriteDirection2 = Luminance.Common.Utilities.Utilities.ToSpriteDirection(this.Direction);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch2, texture2, vector2_2, nullable3, color2, (float) rotation2, nullable4, nullable5, spriteDirection2);
    }
  }
}
