// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.FrozenTurtleEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class FrozenTurtleEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<ColossusHeader>();

    public override int ToggleItemType => 1253;

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      if ((double) player.statLife > (double) player.statLifeMax2 * 0.5)
        return;
      player.AddBuff(62, 5, true, false);
    }
  }
}
