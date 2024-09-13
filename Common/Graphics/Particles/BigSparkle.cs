// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Particles.BigSparkle
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
  public class BigSparkle : Particle
  {
    public readonly bool UseBloom;
    public Color BloomColor;

    public virtual string AtlasTextureName => "FargowiltasSouls.BigSparkle";

    public BigSparkle(
      Vector2 worldPosition,
      Vector2 velocity,
      Color drawColor,
      float scale,
      int lifetime,
      float rotation = 0.0f,
      float rotationSpeed = 0.0f,
      bool useBloom = true,
      Color? bloomColor = null)
    {
      this.Position = worldPosition;
      this.Velocity = velocity;
      this.DrawColor = drawColor;
      this.Scale = new Vector2(scale);
      this.Lifetime = lifetime;
      this.Rotation = rotation;
      this.RotationSpeed = rotationSpeed;
      this.UseBloom = useBloom;
      bloomColor.GetValueOrDefault();
      if (!bloomColor.HasValue)
        bloomColor = new Color?(Color.White);
      this.BloomColor = bloomColor.Value;
    }

    public virtual void Update()
    {
      this.Opacity = Utils.GetLerpValue((float) this.Lifetime, (float) (this.Lifetime - 20), (float) this.Time, true);
      this.Velocity = Vector2.op_Multiply(this.Velocity, 0.99f);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      if (this.UseBloom)
      {
        AtlasTexture texture = AtlasManager.GetTexture("FargowiltasSouls.Bloom");
        SpriteBatch spriteBatch1 = spriteBatch;
        AtlasTexture atlasTexture = texture;
        Vector2 vector2 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
        Rectangle? nullable1 = new Rectangle?();
        Color bloomColor = this.BloomColor;
        ((Color) ref bloomColor).A = (byte) 0;
        Color color = Color.op_Multiply(Color.op_Multiply(bloomColor, 0.5f), this.Opacity);
        Vector2? nullable2 = new Vector2?();
        Vector2? nullable3 = new Vector2?(Vector2.op_Multiply(this.Scale, 0.17f));
        Luminance.Common.Utilities.Utilities.Draw(spriteBatch1, atlasTexture, vector2, nullable1, color, 0.0f, nullable2, nullable3, (SpriteEffects) 0);
      }
      SpriteBatch spriteBatch2 = spriteBatch;
      AtlasTexture texture1 = this.Texture;
      Vector2 vector2_1 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
      Rectangle? nullable4 = new Rectangle?();
      Color color1 = this.DrawColor;
      ((Color) ref color1).A = (byte) 0;
      Color color2 = color1;
      double rotation1 = (double) this.Rotation;
      Vector2? nullable5 = new Vector2?();
      Vector2? nullable6 = new Vector2?(this.Scale);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch2, texture1, vector2_1, nullable4, color2, (float) rotation1, nullable5, nullable6, (SpriteEffects) 0);
      if (!this.UseBloom)
        return;
      SpriteBatch spriteBatch3 = spriteBatch;
      AtlasTexture texture2 = this.Texture;
      Vector2 vector2_2 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
      Rectangle? nullable7 = new Rectangle?();
      color1 = this.BloomColor;
      ((Color) ref color1).A = (byte) 0;
      Color color3 = Color.op_Multiply(color1, 0.5f);
      double rotation2 = (double) this.Rotation;
      Vector2? nullable8 = new Vector2?();
      Vector2? nullable9 = new Vector2?(this.Scale);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch3, texture2, vector2_2, nullable7, color3, (float) rotation2, nullable8, nullable9, (SpriteEffects) 0);
    }
  }
}
