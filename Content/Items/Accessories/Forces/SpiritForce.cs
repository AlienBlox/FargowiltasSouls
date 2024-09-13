// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.SpiritForce
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
  public class SpiritForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[6]
      {
        ModContent.ItemType<FossilEnchant>(),
        ModContent.ItemType<ForbiddenEnchant>(),
        ModContent.ItemType<HallowEnchant>(),
        ModContent.ItemType<AncientHallowEnchant>(),
        ModContent.ItemType<TikiEnchant>(),
        ModContent.ItemType<SpectreEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.SetActive(player);
      player.AddEffect<FossilEffect>(this.Item);
      player.AddEffect<FossilBones>(this.Item);
      ForbiddenEnchant.AddEffects(player, this.Item);
      player.AddEffect<HallowEffect>(this.Item);
      AncientHallowEnchant.AddEffects(player, this.Item);
      TikiEnchant.AddEffects(player, this.Item);
      player.AddEffect<SpectreEffect>(this.Item);
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
