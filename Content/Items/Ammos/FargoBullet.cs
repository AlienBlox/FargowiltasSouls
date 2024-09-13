// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Ammos.FargoBullet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Projectiles.Ammos;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Ammos
{
  public class FargoBullet : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 20;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 26;
      this.Item.knockBack = 4f;
      this.Item.rare = 10;
      this.Item.shoot = ModContent.ProjectileType<FargoBulletProj>();
      this.Item.shootSpeed = 15f;
      this.Item.ammo = AmmoID.Bullet;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3104, 1).AddRecipeGroup("Fargowiltas:AnySilverPouch", 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "MeteorPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "CursedPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "IchorPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "CrystalPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "VelocityPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "VenomPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "ExplosivePouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "GoldenPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "PartyPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "ChlorophytePouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "NanoPouch").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "LuminitePouch").Type, 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
