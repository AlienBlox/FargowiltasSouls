// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.GoldToPiggy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class GoldToPiggy : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WillHeader>();

    public override int ToggleItemType => ModContent.ItemType<GoldEnchant>();

    public override bool IgnoresMutantPresence => true;

    public override void PostUpdateEquips(Player player)
    {
      for (int index = 50; index <= 53; ++index)
      {
        if (!player.inventory[index].IsAir && player.inventory[index].IsACoin)
          player.FargoSouls().GoldEnchMoveCoins = true;
      }
    }
  }
}
