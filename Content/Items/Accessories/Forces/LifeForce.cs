// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.LifeForce
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
  public class LifeForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[5]
      {
        ModContent.ItemType<PumpkinEnchant>(),
        ModContent.ItemType<BeeEnchant>(),
        ModContent.ItemType<SpiderEnchant>(),
        ModContent.ItemType<TurtleEnchant>(),
        ModContent.ItemType<BeetleEnchant>()
      };
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      this.SetActive(player);
      player.AddEffect<BeeEffect>(this.Item);
      player.AddEffect<SpiderEffect>(this.Item);
      BeetleEnchant.AddEffects(player, this.Item);
      player.AddEffect<PumpkinEffect>(this.Item);
      TurtleEnchant.AddEffects(player, this.Item);
      fargoSoulsPlayer.CactusImmune = true;
      player.AddEffect<CactusEffect>(this.Item);
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
