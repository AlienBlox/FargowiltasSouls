﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.LifeRevitalizer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables
{
  public class LifeRevitalizer : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 34;
      ((Entity) this.Item).height = 34;
      this.Item.maxStack = 15;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.rare = -12;
      this.Item.useAnimation = 15;
      this.Item.useTime = 15;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.createTile = ModContent.TileType<LifeRevitalizerPlaced>();
      this.Item.expert = true;
    }
  }
}