// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.DestroyerGun2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
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
  public class DestroyerGun2 : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 275;
      this.Item.mana = 30;
      this.Item.DamageType = DamageClass.Summon;
      ((Entity) this.Item).width = 126;
      ((Entity) this.Item).height = 38;
      this.Item.useAnimation = 70;
      this.Item.useTime = 70;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 1.5f;
      this.Item.UseSound = new SoundStyle?(SoundID.NPCDeath13);
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<DestroyerHead2>();
      this.Item.shootSpeed = 18f;
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
      FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, velocity, type, this.Item.damage, knockback, ((Entity) player).whoAmI);
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "DestroyerGun", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerDestroy"), 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
