// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.HiveStaff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class HiveStaff : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      ItemID.Sets.ShimmerTransformToItem[this.Type] = ModContent.ItemType<TheSmallSting>();
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 15;
      this.Item.mana = 10;
      this.Item.knockBack = 0.25f;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.sentry = true;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 15;
      this.Item.useAnimation = 15;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item78);
      this.Item.value = 50000;
      this.Item.rare = 3;
      this.Item.shoot = ModContent.ProjectileType<HiveSentry>();
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
      Vector2 mouseWorld = Main.MouseWorld;
      Projectile.NewProjectile((IEntitySource) source, mouseWorld.X, mouseWorld.Y - 10f, 0.0f, 0.0f, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      player.UpdateMaxTurrets();
      return false;
    }
  }
}
