// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Armor.StyxChestplate
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
  public class StyxChestplate : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 18;
      ((Entity) this.Item).height = 18;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.defense = 35;
    }

    public virtual void UpdateEquip(Player player)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Generic);
      local = StatModifier.op_Addition(local, 0.15f);
      player.GetCritChance(DamageClass.Generic) += 10f;
      player.endurance += 0.1f;
      player.lifeRegen += 4;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(548, 15).AddIngredient(3467, 5).AddIngredient(ModContent.ItemType<AbomEnergy>(), 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
