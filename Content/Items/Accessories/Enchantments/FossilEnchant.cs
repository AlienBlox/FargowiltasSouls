// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.FossilEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class FossilEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(140, 92, 59);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 40000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<FossilEffect>(this.Item);
      player.AddEffect<FossilBones>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3374, 1).AddIngredient(3375, 1).AddIngredient(3376, 1).AddIngredient(3379, 100).AddIngredient(3377, 1).AddIngredient(3772, 1).AddTile(26).Register();
    }
  }
}
