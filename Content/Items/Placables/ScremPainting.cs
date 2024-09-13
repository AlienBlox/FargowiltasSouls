// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.ScremPainting
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables
{
  public class ScremPainting : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(new Color(Main.DiscoR, 51, (int) byte.MaxValue - (int) ((double) Main.DiscoR * 0.4)));
      }
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 999;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 15;
      this.Item.useTime = 10;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.rare = 11;
      this.Item.createTile = ModContent.TileType<ScremPaintingSheet>();
    }
  }
}
