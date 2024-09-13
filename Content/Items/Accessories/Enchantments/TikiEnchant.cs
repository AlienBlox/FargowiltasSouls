// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.TikiEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class TikiEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(86, 165, 43);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 7;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      TikiEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      if (player.FargoSouls().ForceEffect<TikiEnchant>())
        player.whipRangeMultiplier += 0.2f;
      player.AddEffect<TikiEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1159, 1).AddIngredient(1160, 1).AddIngredient(1161, 1).AddIngredient(986, 1).AddIngredient(1157, 1).AddIngredient(2584, 1).AddTile(125).Register();
    }
  }
}
