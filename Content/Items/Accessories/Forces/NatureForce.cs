// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.NatureForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Forces
{
  public class NatureForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[6]
      {
        ModContent.ItemType<CrimsonEnchant>(),
        ModContent.ItemType<MoltenEnchant>(),
        ModContent.ItemType<RainEnchant>(),
        ModContent.ItemType<FrostEnchant>(),
        ModContent.ItemType<ChlorophyteEnchant>(),
        ModContent.ItemType<ShroomiteEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.SetActive(player);
      player.AddEffect<CrimsonEffect>(this.Item);
      player.AddEffect<MoltenEffect>(this.Item);
      RainEnchant.AddEffects(player, this.Item);
      player.AddEffect<FrostEffect>(this.Item);
      player.AddEffect<SnowEffect>(this.Item);
      ChlorophyteEnchant.AddEffects(player, this.Item);
      player.AddEffect<ShroomiteStealthEffect>(this.Item);
      player.AddEffect<ShroomiteShroomEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe(1);
      foreach (int num in BaseForce.Enchants[this.Type])
        recipe.AddIngredient(num, 1);
      recipe.AddTile<CrucibleCosmosSheet>();
      recipe.Register();
    }
  }
}
