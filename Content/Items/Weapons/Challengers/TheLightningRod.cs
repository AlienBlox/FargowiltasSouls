// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.TheLightningRod
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class TheLightningRod : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 26;
      this.Item.useStyle = 5;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.shootSpeed = 1f;
      this.Item.knockBack = 6f;
      ((Entity) this.Item).width = 68;
      ((Entity) this.Item).height = 68;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.shoot = ModContent.ProjectileType<TheLightningRodProj>();
      this.Item.value = Item.sellPrice(0, 2, 0, 0);
      this.Item.noMelee = true;
      this.Item.noUseGraphic = true;
      this.Item.useTurn = false;
      this.Item.DamageType = DamageClass.Melee;
      this.Item.autoReuse = true;
    }

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      return player.ownedProjectileCounts[this.Item.shoot] < 1;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup(RecipeGroupID.IronBar, 10).AddIngredient(182, 1).AddIngredient(180, 2).AddIngredient(57, 7).AddIngredient(86, 7).AddTile(16).Register();
      this.CreateRecipe(1).AddRecipeGroup(RecipeGroupID.IronBar, 10).AddIngredient(182, 1).AddIngredient(180, 2).AddIngredient(1257, 7).AddIngredient(1329, 7).AddTile(16).Register();
    }
  }
}
