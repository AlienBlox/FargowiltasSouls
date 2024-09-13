// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.TimberForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Forces
{
  public class TimberForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[7]
      {
        ModContent.ItemType<WoodEnchant>(),
        ModContent.ItemType<BorealWoodEnchant>(),
        ModContent.ItemType<RichMahoganyEnchant>(),
        ModContent.ItemType<EbonwoodEnchant>(),
        ModContent.ItemType<ShadewoodEnchant>(),
        ModContent.ItemType<PalmWoodEnchant>(),
        ModContent.ItemType<PearlwoodEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls();
      this.SetActive(player);
      WoodEnchant.WoodEffect(player, this.Item);
      player.AddEffect<BorealEffect>(this.Item);
      player.AddEffect<MahoganyEffect>(this.Item);
      player.AddEffect<EbonwoodEffect>(this.Item);
      player.AddEffect<ShadewoodEffect>(this.Item);
      player.AddEffect<PalmwoodEffect>(this.Item);
      player.AddEffect<PearlwoodEffect>(this.Item);
    }

    public virtual void UpdateVanity(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
    }

    public virtual void UpdateInventory(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
    }

    public virtual void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe(1);
      foreach (int num in BaseForce.Enchants[this.Type])
        recipe.AddIngredient(num, 1);
      recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
      recipe.Register();
    }
  }
}
