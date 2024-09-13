// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.SquirrelCoatofArms
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class SquirrelCoatofArms : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
      ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 12;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 1;
      this.Item.maxStack = 20;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = true;
    }

    public virtual bool? UseItem(Player player)
    {
      FargoSoulsUtil.SpawnBossNetcoded(player, ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>());
      return new bool?(true);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("Wood", 20).AddRecipeGroup("FargowiltasSouls:AnySquirrel", 1).AddTile(18).Register();
    }
  }
}
