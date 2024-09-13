// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.MutantStatueGift
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables
{
  public class MutantStatueGift : MutantStatue
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.createTile = ModContent.TileType<FargowiltasSouls.Content.Tiles.MutantStatueGift>();
    }

    public override void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<MutantStatue>(), 1).AddIngredient(ModContent.ItemType<Masochist>(), 1).Register();
    }
  }
}
