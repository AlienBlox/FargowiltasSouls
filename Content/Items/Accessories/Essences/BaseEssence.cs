// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Essences.BaseEssence
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Essences
{
  public abstract class BaseEssence : SoulsItem
  {
    public abstract Color nameColor { get; }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      TooltipLine tooltipLine;
      if (!tooltips.TryFindTooltipLine("ItemName", out tooltipLine))
        return;
      tooltipLine.OverrideColor = new Color?(this.nameColor);
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.value = 150000;
      this.Item.rare = 5;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
