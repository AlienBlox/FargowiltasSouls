// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Materials.EternalEnergy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Materials
{
  public class EternalEnergy : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 30;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 30;
      this.Item.rare = 11;
      this.Item.useStyle = 2;
      this.Item.useAnimation = 17;
      this.Item.useTime = 17;
      this.Item.consumable = true;
      this.Item.buffType = ModContent.BuffType<SadismBuff>();
      this.Item.buffTime = 25200;
      this.Item.UseSound = new SoundStyle?(SoundID.Item3);
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
    }

    public override void SafeModifyTooltips(List<TooltipLine> list)
    {
      foreach (TooltipLine tooltipLine in list)
      {
        if (tooltipLine.Mod == "Terraria" && tooltipLine.Name == "ItemName")
          tooltipLine.OverrideColor = new Color?(Main.DiscoColor);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
    }
  }
}
