// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.CobaltEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class CobaltEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(61, 164, 196);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<CobaltEffect>(this.Item);
      player.AddEffect<AncientCobaltEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyCobaltHead", 1).AddIngredient(374, 1).AddIngredient(375, 1).AddIngredient((Mod) null, "AncientCobaltEnchant", 1).AddIngredient(4423, 10).AddIngredient(3832, 1).AddTile(125).Register();
    }
  }
}
