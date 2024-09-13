// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AnglerEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AnglerEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(113, 125, 109);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 100000;
      this.Item.rare = 1;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().FishSoul1 = true;
      player.fishingSkill += 10;
      player.accFishingLine = true;
      player.accTackleBox = true;
      player.accLavaFishing = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2367, 1).AddIngredient(2368, 1).AddIngredient(2369, 1).AddIngredient(5064, 1).AddIngredient(4325, 1).AddIngredient(2292, 1).AddTile(26).Register();
    }
  }
}
