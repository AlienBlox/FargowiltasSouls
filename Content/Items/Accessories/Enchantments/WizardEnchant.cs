// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.WizardEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class WizardEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(50, 80, 193);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 100000;
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      base.SafeModifyTooltips(tooltips);
      TooltipLine tooltipLine1;
      if (tooltips.TryFindTooltipLine("ItemName", out tooltipLine1))
        tooltipLine1.OverrideColor = new Color?(this.nameColor);
      FargoSoulsPlayer fargoSoulsPlayer = Main.LocalPlayer.FargoSouls();
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      foreach (BaseEnchant equippedEnchant in fargoSoulsPlayer.EquippedEnchants)
      {
        if (equippedEnchant.Type != this.Type)
        {
          if (fargoSoulsPlayer.ForceEffect(new int?(equippedEnchant.Type)))
          {
            if (equippedEnchant.wizardEffect().Length != 0)
            {
              List<TooltipLine> tooltipLineList = tooltips;
              Mod mod = ((ModType) equippedEnchant).Mod;
              interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
              interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.WizardEffect.Active"));
              interpolatedStringHandler.AppendLiteral(" [i:");
              interpolatedStringHandler.AppendFormatted<int>(equippedEnchant.Item.type);
              interpolatedStringHandler.AppendLiteral("]: ");
              string str = interpolatedStringHandler.ToStringAndClear() + equippedEnchant.wizardEffect();
              TooltipLine tooltipLine2 = new TooltipLine(mod, "wizard", str);
              tooltipLineList.Add(tooltipLine2);
            }
          }
          else if (equippedEnchant.wizardEffect().Length != 0)
          {
            List<TooltipLine> tooltipLineList = tooltips;
            Mod mod = ((ModType) equippedEnchant).Mod;
            interpolatedStringHandler = new DefaultInterpolatedStringHandler(7, 2);
            interpolatedStringHandler.AppendFormatted(Language.GetTextValue("Mods.FargowiltasSouls.WizardEffect.Inactive"));
            interpolatedStringHandler.AppendLiteral(" [i:");
            interpolatedStringHandler.AppendFormatted<int>(equippedEnchant.Item.type);
            interpolatedStringHandler.AppendLiteral("]: ");
            string str = interpolatedStringHandler.ToStringAndClear() + equippedEnchant.wizardEffect();
            TooltipLine tooltipLine3 = new TooltipLine(mod, "wizard", str);
            tooltipLineList.Add(tooltipLine3);
            tooltips[tooltips.Count - 1].OverrideColor = new Color?(Color.Gray);
          }
        }
      }
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().WizardEnchantActive = true;
      player.FargoSouls().WizardTooltips = true;
    }

    public virtual void UpdateVanity(Player player) => player.FargoSouls().WizardTooltips = true;

    public virtual void UpdateInventory(Player player) => player.FargoSouls().WizardTooltips = true;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(238, 1).AddIngredient(1284, 1).AddIngredient(1285, 1).AddIngredient(1286, 1).AddIngredient(1287, 1).AddIngredient(1576, 1).AddTile(125).Register();
    }
  }
}
