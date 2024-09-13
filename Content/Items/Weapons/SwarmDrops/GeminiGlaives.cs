// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.GeminiGlaives
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class GeminiGlaives : SoulsItem
  {
    private int lastThrown;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.ShimmerTransformToItem[this.Type] = ModContent.ItemType<OpticStaffEX>();
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 454;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 30;
      this.Item.useTime = 10;
      this.Item.useAnimation = 10;
      this.Item.reuseDelay = 20;
      this.Item.noUseGraphic = true;
      this.Item.useStyle = 1;
      this.Item.knockBack = 3f;
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.rare = 11;
      this.Item.shootSpeed = 20f;
      this.Item.shoot = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.ownedProjectileCounts[ModContent.ProjectileType<Retiglaive>()] > 0 || player.ownedProjectileCounts[ModContent.ProjectileType<Spazmaglaive>()] > 0)
        return false;
      if (player.altFunctionUse == 2)
      {
        this.Item.shoot = ModContent.ProjectileType<Retiglaive>();
        this.Item.shootSpeed = 15f;
      }
      else
      {
        this.Item.shoot = ModContent.ProjectileType<Spazmaglaive>();
        this.Item.shootSpeed = 45f;
      }
      return true;
    }

    public virtual bool CanShoot(Player player)
    {
      return player.ownedProjectileCounts[ModContent.ProjectileType<Retiglaive>()] <= 0 && player.ownedProjectileCounts[ModContent.ProjectileType<Spazmaglaive>()] <= 0;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      if (this.lastThrown == type)
        return;
      damage = (int) ((double) damage * 1.5);
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
      for (int index = -1; index <= 1; ++index)
        Projectile.NewProjectile((IEntitySource) source, position, Utils.RotatedBy(velocity, (double) MathHelper.ToRadians(30f) * (double) index, new Vector2()), type, damage, knockback, ((Entity) player).whoAmI, (float) this.lastThrown, 0.0f, 0.0f);
      this.lastThrown = type;
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "TwinRangs", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerTwins"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
