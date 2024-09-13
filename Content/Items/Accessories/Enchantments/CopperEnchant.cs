// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CopperEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CopperEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(213, 102, 23);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<CopperEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(89, 1).AddIngredient(80, 1).AddIngredient(76, 1).AddIngredient(3507, 1).AddIngredient(3069, 1).AddIngredient(4062, 1).AddTile(26).Register();
    }
  }
}
