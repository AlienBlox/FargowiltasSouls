// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.PatreonModItem
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon
{
  public abstract class PatreonModItem : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      TooltipLine tooltipLine = new TooltipLine(((ModType) this).Mod, "tooltip", Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Items.Extra.PatreonItem"))
      {
        OverrideColor = new Color?(Color.Orange)
      };
      tooltips.Add(tooltipLine);
    }
  }
}
