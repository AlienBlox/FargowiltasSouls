// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.FinalUpgrades.TheBiggestSting
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Weapons.SwarmDrops;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.FinalUpgrades
{
  public class TheBiggestSting : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 22;
      ((Entity) this.Item).height = 22;
      this.Item.damage = 7744;
      this.Item.useTime = this.Item.useAnimation = 6;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2.2f;
      this.Item.value = 500000;
      this.Item.rare = 11;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<BigStinger>();
      this.Item.useAmmo = AmmoID.Dart;
      this.Item.UseSound = new SoundStyle?(SoundID.Item97);
      this.Item.shootSpeed = 15f;
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
      for (int index1 = 0; index1 < 3; ++index1)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(velocity, 1.0471975803375244), Utils.NextFloat(Main.rand, 0.8f, 1.1f));
        int index2 = Projectile.NewProjectile((IEntitySource) source, position, vector2, 55, damage / 3, knockback, -1, 0.0f, 0.0f, 0.0f);
        if (index2.IsWithinBounds(Main.maxProjectiles))
        {
          Main.projectile[index2].friendly = true;
          Main.projectile[index2].hostile = false;
        }
      }
      return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = this.Item.shoot;
      float num = 1f;
      if (player.strongBees)
        num += 0.1f;
      damage = (int) ((double) damage * (double) num);
      knockback = (float) (int) ((double) knockback * (double) num);
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-30f, 0.0f));

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand, 3);

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<TheBigSting>(), 1).AddIngredient(ModContent.ItemType<EternalEnergy>(), 15).AddTile(ModContent.TileType<CrucibleCosmosSheet>()).Register();
    }
  }
}
