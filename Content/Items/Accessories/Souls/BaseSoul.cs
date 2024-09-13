// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.BaseSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public abstract class BaseSoul : SoulsItem
  {
    protected virtual Color? nameColor => new Color?();

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      ItemID.Sets.ItemNoGravity[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      TooltipLine tooltipLine;
      if (!this.nameColor.HasValue || !tooltips.TryFindTooltipLine("ItemName", out tooltipLine))
        return;
      tooltipLine.OverrideColor = this.nameColor;
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.value = 1000000;
      this.Item.rare = 11;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
