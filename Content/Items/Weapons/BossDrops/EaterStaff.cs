// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.EaterStaff
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class EaterStaff : SoulsItem
  {
    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.mana = 10;
      this.Item.damage = 12;
      this.Item.useStyle = 1;
      this.Item.shootSpeed = 10f;
      this.Item.shoot = ModContent.ProjectileType<EaterHead>();
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 28;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      this.Item.useAnimation = 36;
      this.Item.useTime = 36;
      this.Item.rare = 2;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.buffType = ModContent.BuffType<EaterMinionBuff>();
      this.Item.buffTime = 3600;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.value = 40000;
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
      float slotsUsed = 0.0f;
      ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x => ((Entity) x).active && x.owner == ((Entity) player).whoAmI && (double) x.minionSlots > 0.0)).ToList<Projectile>().ForEach((Action<Projectile>) (x => slotsUsed += x.minionSlots));
      if ((double) player.maxMinions - (double) slotsUsed < 1.0)
        return false;
      int num = -1;
      int index1 = -1;
      for (int index2 = 0; index2 < Main.maxProjectiles; ++index2)
      {
        Projectile projectile = Main.projectile[index2];
        if (((Entity) projectile).active && projectile.owner == ((Entity) player).whoAmI)
        {
          if (num == -1 && projectile.type == ModContent.ProjectileType<EaterHead>())
            num = index2;
          if (index1 == -1 && projectile.type == ModContent.ProjectileType<EaterTail>())
            index1 = index2;
          if (num != -1 && index1 != -1)
            break;
        }
      }
      if (num == -1 && index1 == -1)
      {
        int index3 = FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, Vector2.Zero, ModContent.ProjectileType<EaterHead>(), this.Item.damage, knockback, ((Entity) player).whoAmI);
        int index4 = 0;
        for (int index5 = 0; index5 < 4; ++index5)
        {
          index3 = FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, Vector2.Zero, ModContent.ProjectileType<EaterBody>(), this.Item.damage, knockback, ((Entity) player).whoAmI, (float) Main.projectile[index3].identity);
          index4 = index3;
        }
        int index6 = FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, Vector2.Zero, ModContent.ProjectileType<EaterTail>(), this.Item.damage, knockback, ((Entity) player).whoAmI, (float) Main.projectile[index3].identity);
        Main.projectile[index4].localAI[1] = (float) Main.projectile[index6].identity;
        Main.projectile[index4].netUpdate = true;
      }
      else
      {
        int index7 = (int) Main.projectile[index1].ai[0];
        int index8 = 0;
        for (int index9 = 0; index9 < 4; ++index9)
        {
          int projectileByIdentity = FargoSoulsUtil.GetProjectileByIdentity(((Entity) player).whoAmI, Main.projectile[index7].identity, Array.Empty<int>());
          index8 = FargoSoulsUtil.NewSummonProjectile((IEntitySource) source, position, velocity, ModContent.ProjectileType<EaterBody>(), this.Item.damage, knockback, ((Entity) player).whoAmI, (float) projectileByIdentity);
          index7 = index8;
        }
        Main.projectile[index8].localAI[1] = (float) Main.projectile[index1].identity;
        Main.projectile[index1].ai[0] = (float) FargoSoulsUtil.GetProjectileByIdentity(((Entity) player).whoAmI, Main.projectile[index8].identity, Array.Empty<int>());
        Main.projectile[index1].netUpdate = true;
        Main.projectile[index1].ai[1] = 1f;
      }
      return false;
    }
  }
}
