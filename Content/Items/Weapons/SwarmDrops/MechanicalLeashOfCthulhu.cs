// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.MechanicalLeashOfCthulhu
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Weapons.BossDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class MechanicalLeashOfCthulhu : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 155;
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 10;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.noMelee = true;
      this.Item.useStyle = 5;
      this.Item.autoReuse = true;
      this.Item.useAnimation = 16;
      this.Item.useTime = 16;
      this.Item.knockBack = 6f;
      this.Item.noUseGraphic = true;
      this.Item.shoot = ModContent.ProjectileType<MechFlail>();
      this.Item.shootSpeed = 50f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<LeashOfCthulhu>(), 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerEye"), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
