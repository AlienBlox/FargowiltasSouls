// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.IronEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class IronEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(152, 142, 131);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 40000;
    }

    public virtual void UpdateInventory(Player player) => IronEnchant.AddEffects(player, this.Item);

    public virtual void UpdateVanity(Player player) => IronEnchant.AddEffects(player, this.Item);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      IronEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.AddEffect<IronEffect>(item);
      player.FargoSouls().IronRecipes = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(90, 1).AddIngredient(81, 1).AddIngredient(77, 1).AddIngredient(7, 1).AddIngredient(35, 1).AddIngredient(4282, 1).AddTile(26).Register();
    }
  }
}
