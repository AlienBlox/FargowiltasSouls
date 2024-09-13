// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.FleshCannon
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class FleshCannon : SoulsItem
  {
    public int counter;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 390;
      this.Item.DamageType = DamageClass.Magic;
      this.Item.channel = true;
      this.Item.mana = 6;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 8;
      this.Item.useAnimation = 8;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item12);
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<Hungry2>();
      this.Item.shootSpeed = 20f;
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
      ++this.counter;
      if (player.ownedProjectileCounts[type] < 1 && this.counter % 7 == 0)
      {
        Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(velocity, 2f), type, damage, knockback, ((Entity) player).whoAmI, 0.0f, (float) damage, 0.0f);
        SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(position), (SoundUpdateCallback) null);
      }
      float num = MathHelper.ToRadians(10f) * (float) Math.Sin(((double) this.counter + 0.2) * Math.PI / 7.0);
      Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(Utils.RotatedBy(velocity, (double) num, new Vector2()), 0.4f), 88, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      Projectile.NewProjectile((IEntitySource) source, position, Vector2.op_Multiply(Utils.RotatedBy(velocity, -(double) num, new Vector2()), 0.4f), ModContent.ProjectileType<FleshLaser>(), damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      if (this.counter >= 14)
        this.counter = 0;
      return false;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "FleshHand", 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerWall"), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
