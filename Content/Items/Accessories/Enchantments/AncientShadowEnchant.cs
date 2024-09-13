// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AncientShadowEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AncientShadowEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(94, 85, 220);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().AncientShadowEnchantActive = true;
      player.AddEffect<AncientShadowDarkness>(this.Item);
      player.AddEffect<ShadowBalls>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(956, 1).AddIngredient(957, 1).AddIngredient(958, 1).AddIngredient<ShadowEnchant>(1).AddIngredient(3054, 1).AddIngredient(3053, 1).AddTile(125).Register();
    }
  }
}
