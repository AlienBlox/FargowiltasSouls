// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Forces.TerraForce
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
  [AutoloadEquip]
  public class TerraForce : BaseForce
  {
    public override void SetStaticDefaults()
    {
      BaseForce.Enchants[this.Type] = new int[7]
      {
        ModContent.ItemType<CopperEnchant>(),
        ModContent.ItemType<TinEnchant>(),
        ModContent.ItemType<IronEnchant>(),
        ModContent.ItemType<LeadEnchant>(),
        ModContent.ItemType<SilverEnchant>(),
        ModContent.ItemType<TungstenEnchant>(),
        ModContent.ItemType<ObsidianEnchant>()
      };
    }

    public virtual void UpdateInventory(Player player)
    {
      AshWoodEnchant.PassiveEffect(player);
      IronEnchant.AddEffects(player, this.Item);
    }

    public virtual void UpdateVanity(Player player)
    {
      AshWoodEnchant.PassiveEffect(player);
      IronEnchant.AddEffects(player, this.Item);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls();
      this.SetActive(player);
      player.AddEffect<CopperEffect>(this.Item);
      player.AddEffect<TinEffect>(this.Item);
      IronEnchant.AddEffects(player, this.Item);
      player.AddEffect<LeadEffect>(this.Item);
      player.AddEffect<LeadPoisonEffect>(this.Item);
      player.AddEffect<SilverEffect>(this.Item);
      player.AddEffect<TungstenEffect>(this.Item);
      ObsidianEnchant.AddEffects(player, this.Item);
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
