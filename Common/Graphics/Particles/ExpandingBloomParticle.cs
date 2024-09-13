// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Common.Graphics.Particles.ExpandingBloomParticle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;

#nullable disable
namespace FargowiltasSouls.Common.Graphics.Particles
{
  public class ExpandingBloomParticle : BaseExpandingParticle
  {
    public ExpandingBloomParticle(
      Vector2 position,
      Vector2 velocity,
      Color drawColor,
      Vector2 startScale,
      Vector2 endScale,
      int lifetime,
      bool useExtraBloom = false,
      Color? extraBloomColor = null)
      : base(position, velocity, drawColor, startScale, endScale, lifetime, useExtraBloom, extraBloomColor)
    {
    }
  }
}
