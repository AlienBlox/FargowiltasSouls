// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ShroomiteEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ShroomiteEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(0, 140, 244);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 7;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<ShroomiteStealthEffect>(this.Item);
      player.AddEffect<ShroomiteShroomEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyShroomHead", 1).AddIngredient(1549, 1).AddIngredient(1550, 1).AddIngredient(756, 1).AddIngredient(1265, 1).AddIngredient(679, 1).AddTile(125).Register();
    }
  }
}
