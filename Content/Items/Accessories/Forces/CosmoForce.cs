// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.CosmoForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Forces
{
  public class CosmoForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[6]
      {
        ModContent.ItemType<MeteorEnchant>(),
        ModContent.ItemType<WizardEnchant>(),
        ModContent.ItemType<SolarEnchant>(),
        ModContent.ItemType<VortexEnchant>(),
        ModContent.ItemType<NebulaEnchant>(),
        ModContent.ItemType<StardustEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      this.SetActive(player);
      fargoSoulsPlayer.WizardEnchantActive = true;
      MeteorEnchant.AddEffects(player, this.Item);
      player.AddEffect<SolarEffect>(this.Item);
      player.AddEffect<SolarFlareEffect>(this.Item);
      VortexEnchant.AddEffects(player, this.Item);
      player.AddEffect<NebulaEffect>(this.Item);
      player.AddEffect<StardustMinionEffect>(this.Item);
      player.AddEffect<StardustEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      Recipe recipe = this.CreateRecipe(1);
      foreach (int num in BaseForce.Enchants[this.Type])
        recipe.AddIngredient(num, 1);
      recipe.AddIngredient(ModContent.ItemType<Eridanium>(), 5);
      recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
      recipe.Register();
    }
  }
}
