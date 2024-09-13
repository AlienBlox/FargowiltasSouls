// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Purified.PrimeStaff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Purified
{
  public class PrimeStaff : PatreonModItem
  {
    public int counter;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ItemID.Sets.StaffMinionSlotsRequired[this.Item.type] = 1f;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 60;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 10;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 28;
      this.Item.useTime = 36;
      this.Item.useAnimation = 36;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 3f;
      this.Item.rare = 5;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      this.Item.shoot = ModContent.ProjectileType<PrimeMinionProj>();
      this.Item.shootSpeed = 10f;
      this.Item.buffType = ModContent.BuffType<PrimeMinionBuff>();
      this.Item.autoReuse = true;
      this.Item.value = Item.sellPrice(0, 8, 0, 0);
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
      player.AddBuff(ModContent.BuffType<PrimeMinionBuff>(), 2, true, false);
      Vector2 mouseWorld = Main.MouseWorld;
      float num1 = 0.0f;
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x => (double) x.minionSlots > 0.0 && x.owner == ((Entity) player).whoAmI && ((Entity) x).active)))
        num1 += projectile.minionSlots;
      if ((double) num1 < (double) player.maxMinions)
      {
        if (player.ownedProjectileCounts[type] == 0)
          FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, mouseWorld, Vector2.Zero, type, this.Item.damage, knockback, ((Entity) player).whoAmI);
        if (++this.counter >= 4)
          this.counter = 0;
        int num2;
        switch (this.counter)
        {
          case 0:
            num2 = ModContent.ProjectileType<PrimeMinionVice>();
            break;
          case 1:
            num2 = ModContent.ProjectileType<PrimeMinionSaw>();
            break;
          case 2:
            num2 = ModContent.ProjectileType<PrimeMinionLaserGun>();
            break;
          default:
            num2 = ModContent.ProjectileType<PrimeMinionCannon>();
            break;
        }
        int type1 = num2;
        FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, mouseWorld, Utils.NextVector2Circular(Main.rand, 10f, 10f), type1, this.Item.damage, knockback, ((Entity) player).whoAmI);
      }
      return false;
    }
  }
}
