// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.SlimeSlingingSlasher
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
  public class SlimeSlingingSlasher : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 335;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 48;
      ((Entity) this.Item).height = 64;
      this.Item.useTime = 12;
      this.Item.useAnimation = 12;
      this.Item.useStyle = 1;
      this.Item.knockBack = 6f;
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<SlimeBallHoming>();
      this.Item.shootSpeed = 16f;
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
      int num = 9;
      for (int index = 0; index < num; ++index)
      {
        velocity = Utils.RotatedBy(velocity, (double) MathHelper.ToRadians(45f) * (Main.rand.NextDouble() - 0.5), new Vector2());
        Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      return false;
    }

    public virtual void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 180, false);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "SlimeKingsSlasher", 1).AddIngredient(ModContent.Find<ModItem>("Fargowiltas", "EnergizerSlime"), 1).AddIngredient(3467, 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
