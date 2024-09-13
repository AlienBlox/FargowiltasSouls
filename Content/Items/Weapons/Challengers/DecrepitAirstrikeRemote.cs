// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Challengers.DecrepitAirstrikeRemote
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Challengers
{
  public class DecrepitAirstrikeRemote : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 375;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 50;
      ((Entity) this.Item).width = 82;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 45;
      this.Item.useAnimation = 45;
      this.Item.useStyle = 5;
      this.Item.knockBack = 15f;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.rare = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item66);
      this.Item.autoReuse = false;
      this.Item.shoot = ModContent.ProjectileType<DecrepitAirstrike>();
      this.Item.shootSpeed = 1f;
      this.Item.noMelee = true;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(0.0f, 0.0f));

    public virtual bool CanUseItem(Player player)
    {
      return player.ownedProjectileCounts[this.Item.shoot] < 1;
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
      Vector2 mouseWorld = Main.MouseWorld;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (((Entity) projectile).active && projectile.owner == ((Entity) player).whoAmI && projectile.sentry)
        {
          ((Entity) projectile).active = false;
          projectile.Kill();
        }
      }
      for (int index = 0; index < player.maxTurrets; ++index)
        Projectile.NewProjectile((IEntitySource) source, mouseWorld.X, mouseWorld.Y, 0.0f, 0.0f, type, damage, knockback, ((Entity) player).whoAmI, (float) (index != 0 ? 1 : 0), 0.0f, (float) player.maxTurrets);
      Projectile.NewProjectile((IEntitySource) source, mouseWorld.X, mouseWorld.Y, 0.0f, 0.0f, ModContent.ProjectileType<GlowRingHollow>(), damage, knockback, ((Entity) player).whoAmI, 14f, 150f, 0.0f);
      player.UpdateMaxTurrets();
      return false;
    }

    public virtual bool? UseItem(Player player) => base.UseItem(player);

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient<BanishedBaronBag>(2).AddTile(220).DisableDecraft().Register();
    }
  }
}
