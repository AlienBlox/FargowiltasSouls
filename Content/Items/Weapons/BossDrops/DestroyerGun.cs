// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.DestroyerGun
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
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class DestroyerGun : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 45;
      this.Item.mana = 10;
      this.Item.DamageType = DamageClass.Summon;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useTime = 70;
      this.Item.useAnimation = 70;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 1.5f;
      this.Item.UseSound = new SoundStyle?(SoundID.NPCDeath13);
      this.Item.value = 50000;
      this.Item.rare = 5;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<DestroyerHead>();
      this.Item.shootSpeed = 10f;
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
  }
}
