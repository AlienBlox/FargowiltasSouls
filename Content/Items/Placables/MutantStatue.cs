// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.MutantStatue
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables
{
  public class MutantStatue : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 99;
      this.Item.useTurn = true;
      this.Item.autoReuse = true;
      this.Item.rare = 1;
      this.Item.useAnimation = 15;
      this.Item.useTime = 15;
      this.Item.useStyle = 1;
      this.Item.consumable = true;
      this.Item.createTile = ModContent.TileType<FargowiltasSouls.Content.Tiles.MutantStatue>();
    }

    public virtual void AddRecipes()
    {
      ModItem modItem;
      if (!ModContent.TryFind<ModItem>("Fargowiltas/Mutant", ref modItem))
        return;
      this.CreateRecipe(1).AddIngredient(3, 50).AddIngredient(modItem, 1).AddTile(283).Register();
    }
  }
}
