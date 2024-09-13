// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.BrainStaff
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
  public class BrainStaff : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 12;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 10;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 28;
      this.Item.useTime = 36;
      this.Item.useAnimation = 36;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 3f;
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item44);
      this.Item.shoot = ModContent.ProjectileType<BrainMinion>();
      this.Item.shootSpeed = 10f;
      this.Item.autoReuse = true;
      this.Item.value = Item.sellPrice(0, 2, 0, 0);
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
      player.AddBuff(ModContent.BuffType<BrainMinionBuff>(), 2, true, false);
      Vector2 mouseWorld = Main.MouseWorld;
      float num = 0.0f;
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x => (double) x.minionSlots > 0.0 && x.owner == ((Entity) player).whoAmI && ((Entity) x).active)))
        num += projectile.minionSlots;
      if (player.ownedProjectileCounts[type] == 0 && (double) num != (double) player.maxMinions)
        player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, type, this.Item.damage, knockback, new Vector2(), new Vector2());
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, ModContent.ProjectileType<CreeperMinion>(), this.Item.damage, knockback, new Vector2(), Utils.NextVector2Circular(Main.rand, 10f, 10f));
      return false;
    }
  }
}
