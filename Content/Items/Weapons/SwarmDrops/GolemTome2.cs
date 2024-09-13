// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.GolemTome2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class GolemTome2 : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 358;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 38;
      ((Entity) this.Item).height = 46;
      this.Item.useTime = 60;
      this.Item.useAnimation = 60;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.rare = 11;
      this.Item.mana = 24;
      this.Item.UseSound = new SoundStyle?(SoundID.Item21);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<GolemHeadProj>();
      this.Item.shootSpeed = 10f;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      velocity = Utils.RotatedByRandom(velocity, (double) MathHelper.ToRadians(15f));
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "RockSlide", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerGolem"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
