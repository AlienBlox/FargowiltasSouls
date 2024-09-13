// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Particles.ElectricSpark
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
  public class ElectricSpark : Particle
  {
    public readonly bool UseBloom;
    public Color BloomColor;

    public virtual string AtlasTextureName => "FargowiltasSouls.ElectricSpark";

    public virtual BlendState BlendState => BlendState.Additive;

    public static int FadeTime => 15;

    public ElectricSpark(
      Vector2 worldPosition,
      Vector2 velocity,
      Color drawColor,
      float scale,
      int lifetime,
      bool useBloom = true,
      Color? bloomColor = null)
    {
      this.Position = worldPosition;
      this.Velocity = velocity;
      this.DrawColor = drawColor;
      this.Scale = new Vector2(scale);
      this.Lifetime = lifetime;
      this.UseBloom = useBloom;
      bloomColor.GetValueOrDefault();
      if (!bloomColor.HasValue)
        bloomColor = new Color?(Color.White);
      this.BloomColor = bloomColor.Value;
    }

    public virtual void Update()
    {
      this.Velocity = Vector2.op_Multiply(this.Velocity, 0.95f);
      this.Scale = Vector2.op_Multiply(this.Scale, 0.95f);
      this.Opacity = FargoSoulsUtil.SineInOut(1f - this.LifetimeRatio);
      this.Rotation = Utils.ToRotation(this.Velocity) + 1.57079637f;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      AtlasTexture lightningTexture = this.Texture;
      int lightningFrames = 5;
      this.Frame = new Rectangle?(GetRandomLightningFrame());
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch, lightningTexture, Vector2.op_Subtraction(this.Position, Main.screenPosition), this.Frame, Color.op_Multiply(this.DrawColor, this.Opacity), this.Rotation, new Vector2?(), new Vector2?(this.Scale), (SpriteEffects) 0);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch, lightningTexture, Vector2.op_Subtraction(this.Position, Main.screenPosition), this.Frame, Color.op_Multiply(this.DrawColor, this.Opacity), this.Rotation, new Vector2?(), new Vector2?(Vector2.op_Multiply(this.Scale, new Vector2(0.45f, 1f))), (SpriteEffects) 0);
      if (!this.UseBloom)
        return;
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch, lightningTexture, Vector2.op_Subtraction(this.Position, Main.screenPosition), this.Frame, Color.op_Multiply(Color.op_Multiply(this.BloomColor, 0.5f), this.Opacity), this.Rotation, new Vector2?(), new Vector2?(this.Scale), (SpriteEffects) 0);

      Rectangle GetRandomLightningFrame()
      {
        int num1 = lightningTexture.Frame.Height / lightningFrames;
        int num2 = Main.rand.Next(lightningFrames);
        return new Rectangle(0, num1 * num2, lightningTexture.Frame.Width, num1);
      }
    }
  }
}
