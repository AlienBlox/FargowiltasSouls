// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.WillForce
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
  public class WillForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[5]
      {
        ModContent.ItemType<GoldEnchant>(),
        ModContent.ItemType<PlatinumEnchant>(),
        ModContent.ItemType<GladiatorEnchant>(),
        ModContent.ItemType<RedRidingEnchant>(),
        ModContent.ItemType<ValhallaKnightEnchant>()
      };
    }

    public virtual void UpdateInventory(Player player) => player.AddEffect<GoldToPiggy>(this.Item);

    public virtual void UpdateVanity(Player player) => player.AddEffect<GoldToPiggy>(this.Item);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.SetActive(player);
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.AddEffect<GoldEffect>(this.Item);
      player.AddEffect<GoldToPiggy>(this.Item);
      Item obj = this.Item;
      fargoSoulsPlayer.PlatinumEffect = obj;
      player.AddEffect<GladiatorBanner>(this.Item);
      player.AddEffect<GladiatorSpears>(this.Item);
      player.AddEffect<RedRidingEffect>(this.Item);
      player.AddEffect<HuntressEffect>(this.Item);
      player.FargoSouls().ValhallaEnchantActive = true;
      player.AddEffect<ValhallaDash>(this.Item);
      SquireEnchant.SquireEffect(player, this.Item);
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
