// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.FishStick
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
  public class FishStick : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 120;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 16;
      this.Item.useAnimation = 16;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.value = Item.sellPrice(0, 6, 0, 0);
      this.Item.rare = 8;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<FishStickProj>();
      this.Item.shootSpeed = 35f;
      this.Item.noUseGraphic = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      this.Item.shoot = player.altFunctionUse == 2 ? ModContent.ProjectileType<FishStickWhirlpool>() : ModContent.ProjectileType<FishStickProj>();
      return base.CanUseItem(player);
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
      if (((Entity) player).whoAmI == Main.myPlayer && player.altFunctionUse != 2)
        FishStickProj.ShootSharks(Main.MouseWorld, ((Vector2) ref velocity).Length(), (IEntitySource) source, damage, knockback);
      return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
  }
}
