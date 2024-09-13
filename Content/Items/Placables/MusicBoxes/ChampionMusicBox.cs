﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.MusicBoxes.ChampionMusicBox
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Tiles.MusicBoxes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables.MusicBoxes
{
  public class ChampionMusicBox : ModItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
        return;
      MusicLoader.AddMusicBox(((ModType) this).Mod, MusicLoader.GetMusicSlot(mod, "Assets/Music/Champions"), ModContent.ItemType<ChampionMusicBox>(), ModContent.TileType<ChampionMusicBoxSheet>(), 0);
    }

    public virtual void ModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(Main.DiscoColor);
      }
    }

    public virtual void SetDefaults()
    {
      this.Item.useStyle = 1;
      this.Item.useTurn = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.autoReuse = true;
      this.Item.consumable = true;
      this.Item.createTile = ModContent.TileType<ChampionMusicBoxSheet>();
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 7, 0, 0);
      this.Item.accessory = true;
    }
  }
}
