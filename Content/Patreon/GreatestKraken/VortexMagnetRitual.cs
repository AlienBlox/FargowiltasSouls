// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.GreatestKraken.VortexMagnetRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.GreatestKraken
{
  public class VortexMagnetRitual : PatreonModItem
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      base.SetStaticDefaults();
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 333;
      this.Item.DamageType = DamageClass.Magic;
      this.Item.useTime = 16;
      this.Item.useAnimation = 16;
      this.Item.knockBack = 4f;
      this.Item.mana = 15;
      this.Item.useStyle = 5;
      this.Item.autoReuse = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<VortexRitualProj>();
      this.Item.shootSpeed = 12f;
      this.Item.channel = true;
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 30;
      this.Item.value = Item.sellPrice(0, 12, 0, 0);
      this.Item.rare = 10;
      this.Item.UseSound = new SoundStyle?(SoundID.Item21);
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
      if (player.ownedProjectileCounts[ModContent.ProjectileType<VortexRitualProj>()] <= 0)
      {
        Vector2 mouseWorld = Main.MouseWorld;
        Projectile.NewProjectile((IEntitySource) source, mouseWorld, Vector2.Zero, ModContent.ProjectileType<VortexRitualProj>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, 300f, 0.0f);
      }
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1266, 1).AddIngredient(3456, 35).AddIngredient(3467, 5).AddIngredient(ModContent.ItemType<CelestialRune>(), 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerCultist"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
