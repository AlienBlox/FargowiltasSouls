// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.EarthForce
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
  public class EarthForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[6]
      {
        ModContent.ItemType<CobaltEnchant>(),
        ModContent.ItemType<PalladiumEnchant>(),
        ModContent.ItemType<MythrilEnchant>(),
        ModContent.ItemType<OrichalcumEnchant>(),
        ModContent.ItemType<AdamantiteEnchant>(),
        ModContent.ItemType<TitaniumEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.SetActive(player);
      player.AddEffect<AncientCobaltEffect>(this.Item);
      player.AddEffect<CobaltEffect>(this.Item);
      player.AddEffect<PalladiumEffect>(this.Item);
      player.AddEffect<PalladiumHealing>(this.Item);
      player.AddEffect<MythrilEffect>(this.Item);
      player.AddEffect<OrichalcumEffect>(this.Item);
      player.AddEffect<AdamantiteEffect>(this.Item);
      player.AddEffect<TitaniumEffect>(this.Item);
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
