// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.FlightMasteryWings
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public abstract class FlightMasteryWings : BaseSoul
  {
    public abstract bool HasSupersonicSpeed { get; }

    public virtual void VerticalWingSpeeds(
      Player player,
      ref float ascentWhenFalling,
      ref float ascentWhenRising,
      ref float maxCanAscendMultiplier,
      ref float maxAscentMultiplier,
      ref float constantAscend)
    {
      player.wingsLogic = 45;
      ascentWhenFalling = 0.85f;
      if (player.HasEffect<FlightMasteryGravity>())
        ascentWhenFalling *= 1.5f;
      ascentWhenRising = 0.25f;
      maxCanAscendMultiplier = 1f;
      maxAscentMultiplier = 1.75f;
      constantAscend = 0.135f;
      if (!player.controlUp)
        return;
      ascentWhenFalling *= 6f;
      ascentWhenRising *= 6f;
      constantAscend *= 6f;
    }

    public virtual void HorizontalWingSpeeds(
      Player player,
      ref float speed,
      ref float acceleration)
    {
      speed = 18f;
      acceleration = 0.75f;
      if (!this.HasSupersonicSpeed || !player.HasEffect<SupersonicSpeedEffect>())
        return;
      speed = 25f;
    }
  }
}
