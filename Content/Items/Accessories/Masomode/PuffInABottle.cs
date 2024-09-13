// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.PuffInABottle
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class PuffInABottle : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(53);
      this.Item.value = Item.sellPrice(0, 0, 1, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ((ExtraJumpState) ref player.GetJumpState<PuffJump>()).Enable();
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(31, 1).AddIngredient(751, 1).Register();
    }
  }
}
