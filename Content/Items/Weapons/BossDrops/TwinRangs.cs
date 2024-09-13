// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.TwinRangs
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
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class TwinRangs : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 50;
      this.Item.DamageType = DamageClass.Melee;
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 30;
      this.Item.useTime = 25;
      this.Item.useAnimation = 25;
      this.Item.noUseGraphic = true;
      this.Item.useStyle = 1;
      this.Item.knockBack = 3f;
      this.Item.value = 100000;
      this.Item.rare = 5;
      this.Item.shootSpeed = 20f;
      this.Item.shoot = 1;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      if (player.altFunctionUse == 2)
      {
        this.Item.shoot = ModContent.ProjectileType<Retirang>();
        this.Item.shootSpeed = 20f;
      }
      else
      {
        this.Item.shoot = ModContent.ProjectileType<Spazmarang>();
        this.Item.shootSpeed = 30f;
      }
      return true;
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
      if (player.altFunctionUse == 2)
        damage = (int) ((double) damage * 0.75);
      Projectile.NewProjectile((IEntitySource) source, position, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      return false;
    }
  }
}
