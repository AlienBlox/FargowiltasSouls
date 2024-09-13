// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.PrismaRegalia
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class PrismaRegalia : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.mana = 0;
      this.Item.damage = 185;
      this.Item.DamageType = DamageClass.MeleeNoSpeed;
      ((Entity) this.Item).width = 64;
      ((Entity) this.Item).height = 64;
      this.Item.useTime = 25;
      this.Item.useAnimation = 25;
      this.Item.useStyle = 5;
      this.Item.knockBack = 6f;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
      this.Item.rare = 8;
      this.Item.UseSound = new SoundStyle?();
      this.Item.autoReuse = false;
      this.Item.channel = true;
      this.Item.shoot = ModContent.ProjectileType<PrismaRegaliaProj>();
      this.Item.shootSpeed = 4f;
      this.Item.noUseGraphic = true;
      this.Item.noMelee = true;
    }

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
      return base.CanUseItem(player);
    }
  }
}
