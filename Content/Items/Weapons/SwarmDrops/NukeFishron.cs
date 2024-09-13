// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.NukeFishron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class NukeFishron : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 634;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 40;
      this.Item.useAnimation = 40;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 7.7f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item62);
      this.Item.useAmmo = AmmoID.Rocket;
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<FishNuke>();
      this.Item.shootSpeed = 7f;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-12f, 0.0f));

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      Projectile.NewProjectile((IEntitySource) source, position, velocity, this.Item.shoot, damage, knockback, ((Entity) player).whoAmI, -1f, 0.0f, 0.0f);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<FishStick>(), 1).AddIngredient(ModContent.ItemType<AbomEnergy>(), 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerFish"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
