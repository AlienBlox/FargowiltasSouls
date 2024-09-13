// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.BuilderEffect
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Toggler;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class BuilderEffect : AccessoryEffect
  {
    public override Header ToggleHeader => (Header) Header.GetHeader<WorldShaperHeader>();

    public override int ToggleItemType => ModContent.ItemType<WorldShaperSoul>();

    public override void PostUpdateEquips(Player player)
    {
      if (((Entity) player).whoAmI != Main.myPlayer)
        return;
      player.FargoSouls().BuilderMode = true;
      for (int index = 0; index < TileLoader.TileCount; ++index)
        player.adjTile[index] = true;
      player.tileSpeed += 0.5f;
      player.wallSpeed += 0.5f;
      if (player.HeldItem.createWall == 0)
      {
        Player.tileRangeX += 60;
        Player.tileRangeY += 60;
      }
      else
      {
        Player.tileRangeX += 20;
        Player.tileRangeY += 20;
      }
    }
  }
}
