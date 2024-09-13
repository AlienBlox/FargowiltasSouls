// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.GoldEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class GoldEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(231, 178, 28);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 150000;
    }

    public virtual void UpdateInventory(Player player) => player.AddEffect<GoldToPiggy>(this.Item);

    public virtual void UpdateVanity(Player player) => player.AddEffect<GoldToPiggy>(this.Item);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<GoldEffect>(this.Item);
      player.AddEffect<GoldToPiggy>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(92, 1).AddIngredient(83, 1).AddIngredient(79, 1).AddIngredient(848, 1).AddIngredient(261, 1).AddIngredient(4022, 1).AddTile(26).Register();
    }
  }
}
