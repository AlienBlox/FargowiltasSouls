// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.PalladiumEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class PalladiumEnchant : BaseEnchant
  {
    public override Color nameColor => new Color(245, 172, 40);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<PalladiumEffect>(this.Item);
      player.AddEffect<PalladiumHealing>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyPallaHead", 1).AddIngredient(1208, 1).AddIngredient(1209, 1).AddIngredient(5097, 1).AddIngredient(3006, 1).AddIngredient(1483, 1).AddTile(125).Register();
    }
  }
}
