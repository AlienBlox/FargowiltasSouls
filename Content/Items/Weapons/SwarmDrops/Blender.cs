// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.Blender
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class Blender : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.useStyle = 5;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.noUseGraphic = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.DamageType = DamageClass.Melee;
      this.Item.channel = true;
      this.Item.noMelee = true;
      this.Item.shoot = ModContent.ProjectileType<BlenderYoyoProj>();
      this.Item.useAnimation = 25;
      this.Item.useTime = 25;
      this.Item.shootSpeed = 16f;
      this.Item.knockBack = 2.5f;
      this.Item.damage = 512;
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.rare = 11;
    }

    public virtual void HoldItem(Player player) => player.stringColor = 5;

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "Dicer", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerPlant"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
