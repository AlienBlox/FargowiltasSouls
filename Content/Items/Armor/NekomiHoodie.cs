// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.NekomiHoodie
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Armor
{
  [AutoloadEquip]
  public class NekomiHoodie : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 4;
      this.Item.value = Item.sellPrice(0, 1, 50, 0);
      this.Item.defense = 11;
    }

    public virtual void UpdateEquip(Player player)
    {
      ++player.lifeRegen;
      player.endurance += 0.05f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(225, 10).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 5).AddTile(86).Register();
    }
  }
}
