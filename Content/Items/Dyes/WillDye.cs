﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Dyes.WillDye
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;

#nullable disable
namespace FargowiltasSouls.Content.Items.Dyes
{
  public class WillDye : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public virtual void SetDefaults()
    {
      this.Item.maxStack = 99;
      this.Item.rare = 3;
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.value = Item.sellPrice(0, 2, 50, 0);
    }
  }
}
