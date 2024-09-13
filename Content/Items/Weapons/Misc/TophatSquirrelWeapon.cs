// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Misc.TophatSquirrelWeapon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Content.Projectiles.Critters;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Misc
{
  public class TophatSquirrelWeapon : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 22;
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 8;
      this.Item.useAnimation = 45;
      this.Item.useTime = 45;
      this.Item.DamageType = DamageClass.Magic;
      this.Item.noMelee = true;
      this.Item.noUseGraphic = true;
      this.Item.useStyle = 1;
      this.Item.knockBack = 6.6f;
      this.Item.mana = 66;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<TopHatSquirrelProj>();
      this.Item.shootSpeed = 8f;
      this.Item.value = Item.sellPrice(0, 20, 0, 0);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<TopHatSquirrelCaught>(), 10).AddIngredient(1006, 5).AddIngredient(547, 3).AddIngredient(549, 3).AddIngredient(548, 3).AddIngredient(520, 3).AddIngredient(521, 3).AddTile(134).Register();
    }
  }
}
