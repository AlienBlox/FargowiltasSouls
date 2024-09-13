// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.WoodEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class WoodEnchant : BaseEnchant
  {
    public override Color nameColor => new Color(151, 107, 75);

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      BestiaryUnlockProgressReport bestiaryProgressReport = Main.GetBestiaryProgressReport();
      double num = Math.Round((double) ((BestiaryUnlockProgressReport) ref bestiaryProgressReport).CompletionPercent / 2.0 * 100.0, 2);
      int index1 = tooltips.FindIndex((Predicate<TooltipLine>) (line => line.Name == "Tooltip3"));
      if (index1 != -1)
      {
        tooltips[index1].Text = string.Format(tooltips[index1].Text, (object) num);
      }
      else
      {
        int index2 = tooltips.FindIndex((Predicate<TooltipLine>) (line => line.Name == "SocialDesc"));
        if (index2 == -1)
          return;
        tooltips.RemoveAt(index2);
        ItemTooltip itemTooltip = ItemTooltip.FromLocalization(this.Tooltip);
        tooltips.Insert(index2, new TooltipLine(((ModType) this).Mod, "WoodEnchantVanity0", itemTooltip.GetLine(1)));
        tooltips.Insert(index2 + 1, new TooltipLine(((ModType) this).Mod, "WoodEnchantVanity1", itemTooltip.GetLine(2)));
        tooltips.Insert(index2 + 2, new TooltipLine(((ModType) this).Mod, "WoodEnchantVanity2", string.Format(itemTooltip.GetLine(3), (object) num)));
      }
    }

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 10000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      WoodEnchant.WoodEffect(player, this.Item);
    }

    public virtual void UpdateVanity(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
    }

    public virtual void UpdateInventory(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
    }

    public static void WoodEffect(Player player, Item item)
    {
      player.AddEffect<WoodCompletionEffect>(item);
      player.FargoSouls().WoodEnchantDiscount = true;
    }

    public static void WoodDiscount(Item[] items)
    {
      BestiaryUnlockProgressReport bestiaryProgressReport = Main.GetBestiaryProgressReport();
      float num = (float) (1.0 - (double) ((BestiaryUnlockProgressReport) ref bestiaryProgressReport).CompletionPercent / 2.0);
      foreach (Item obj in items)
      {
        if (obj != null)
        {
          int? nullable = !obj.shopCustomPrice.HasValue ? new int?(obj.value) : obj.shopCustomPrice;
          obj.shopCustomPrice = new int?((int) ((double) nullable.Value * (double) num));
        }
      }
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(727, 1).AddIngredient(728, 1).AddIngredient(729, 1).AddIngredient(313, 1).AddIngredient(4009, 1).AddRecipeGroup("FargowiltasSouls:AnySquirrel", 1).AddTile(26).Register();
    }
  }
}
