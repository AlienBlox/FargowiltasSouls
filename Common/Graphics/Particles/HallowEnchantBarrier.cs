// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Particles.HallowEnchantBarrier
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
  public class HallowEnchantBarrier : Particle
  {
    public readonly bool UseBloom;
    public readonly float BaseOpacity = 1f;
    public int CurrentFrame;

    public virtual string AtlasTextureName => "FargowiltasSouls.HallowEnchantBarrier";

    public virtual int FrameCount => 4;

    public virtual BlendState BlendState => BlendState.NonPremultiplied;

    public HallowEnchantBarrier(
      Vector2 worldPosition,
      Vector2 velocity,
      float scale,
      int lifetime,
      float baseOpacity = 1f,
      float rotation = 0.0f,
      float rotationSpeed = 0.0f)
    {
      this.Position = worldPosition;
      this.Velocity = velocity;
      this.DrawColor = Color.White;
      this.Scale = new Vector2(scale);
      this.Lifetime = lifetime;
      this.Rotation = rotation;
      this.RotationSpeed = rotationSpeed;
      this.UseBloom = false;
      this.BaseOpacity = baseOpacity;
      this.CurrentFrame = Main.rand.Next(base.FrameCount);
    }

    public virtual void Update()
    {
      this.Opacity = Utils.GetLerpValue((float) this.Lifetime, (float) (this.Lifetime - 20), (float) this.Time, true);
      this.Velocity = Vector2.op_Multiply(this.Velocity, 0.99f);
      this.CurrentFrame = (int) ((double) (this.Time * base.FrameCount) / (double) this.Lifetime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
      int num = this.Texture.Frame.Height / base.FrameCount;
      this.Frame = new Rectangle?(new Rectangle(0, this.CurrentFrame * num, this.Texture.Frame.Width, num));
      Vector2 vector2 = Vector2.op_Subtraction(this.Position, Main.screenPosition);
      Luminance.Common.Utilities.Utilities.Draw(spriteBatch, this.Texture, vector2, this.Frame, Color.op_Multiply(Color.op_Multiply(this.DrawColor, this.Opacity), this.BaseOpacity), this.Rotation, new Vector2?(), new Vector2?(this.Scale), (SpriteEffects) 0);
    }
  }
}
