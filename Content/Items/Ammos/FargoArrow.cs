// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Ammos.FargoArrow
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
  public class FargoArrow : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 45;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 26;
      this.Item.knockBack = 8f;
      this.Item.rare = 10;
      this.Item.shoot = ModContent.ProjectileType<FargoArrowProj>();
      this.Item.shootSpeed = 6.5f;
      this.Item.ammo = AmmoID.Arrow;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3103, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "FlameQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "FrostburnQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "UnholyQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "BoneQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "JesterQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "HellfireQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "CursedQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "IchorQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "HolyQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "VenomQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "ChlorophyteQuiver").Type, 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "LuminiteQuiver").Type, 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
