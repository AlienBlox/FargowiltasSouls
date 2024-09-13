// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.GaiaPlate
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Armor
{
  [AutoloadEquip]
  public class GaiaPlate : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 8;
      this.Item.value = Item.sellPrice(0, 6, 0, 0);
      this.Item.defense = 20;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.1f);
      player.GetCritChance(DamageClass.Generic) += 5f;
      player.endurance += 0.1f;
      player.lifeRegen += 2;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2218, 6).AddIngredient(1552, 9).AddIngredient(3261, 9).AddIngredient(1729, 150).AddTile(412).Register();
    }
  }
}
