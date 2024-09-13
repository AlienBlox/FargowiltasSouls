// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AshWoodEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AshWoodEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(139, 116, 100);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 10000;
    }

    public static void PassiveEffect(Player player)
    {
    }

    public virtual void UpdateInventory(Player player) => AshWoodEnchant.PassiveEffect(player);

    public virtual void UpdateVanity(Player player) => AshWoodEnchant.PassiveEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      AshWoodEnchant.PassiveEffect(player);
      player.AddEffect<AshWoodEffect>(this.Item);
      player.AddEffect<AshWoodFireballs>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(5279, 1).AddIngredient(5280, 1).AddIngredient(5281, 1).AddIngredient(318, 1).AddIngredient(5277, 1).AddIngredient(23, 50).AddTile(26).Register();
    }
  }
}
