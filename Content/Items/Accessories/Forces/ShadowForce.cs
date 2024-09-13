// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.ShadowForce
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Forces
{
  public class ShadowForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[7]
      {
        ModContent.ItemType<NinjaEnchant>(),
        ModContent.ItemType<AncientShadowEnchant>(),
        ModContent.ItemType<CrystalAssassinEnchant>(),
        ModContent.ItemType<SpookyEnchant>(),
        ModContent.ItemType<ShinobiEnchant>(),
        ModContent.ItemType<DarkArtistEnchant>(),
        ModContent.ItemType<NecroEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.SetActive(player);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.AddEffect<NinjaEffect>(this.Item);
      fargoSoulsPlayer.ApprenticeEnchantActive = true;
      fargoSoulsPlayer.DarkArtistEnchantActive = true;
      player.AddEffect<ApprenticeSupport>(this.Item);
      player.AddEffect<DarkArtistMinion>(this.Item);
      player.AddEffect<NecroEffect>(this.Item);
      fargoSoulsPlayer.AncientShadowEnchantActive = true;
      player.AddEffect<ShadowBalls>(this.Item);
      player.AddEffect<AncientShadowDarkness>(this.Item);
      ShinobiEnchant.AddEffects(player, this.Item);
      CrystalAssassinEnchant.AddEffects(player, this.Item);
      player.AddEffect<SpookyEffect>(this.Item);
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
