// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.SwarmDrops.OpticStaffEX
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Projectiles.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.SwarmDrops
{
  public class OpticStaffEX : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.StaffMinionSlotsRequired[this.Item.type] = 4f;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 312;
      this.Item.mana = 10;
      this.Item.DamageType = DamageClass.Summon;
      ((Entity) this.Item).width = 24;
      ((Entity) this.Item).height = 24;
      this.Item.useAnimation = 37;
      this.Item.useTime = 37;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 3f;
      this.Item.UseSound = new SoundStyle?(SoundID.Item82);
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
      this.Item.rare = 11;
      this.Item.buffType = ModContent.BuffType<TwinsEXBuff>();
      this.Item.shoot = ModContent.ProjectileType<OpticRetinazer>();
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
      player.AddBuff(this.Item.buffType, 2, true, false);
      Vector2 mouseWorld = Main.MouseWorld;
      velocity = Utils.RotatedBy(velocity, Math.PI / 2.0, new Vector2());
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, ModContent.ProjectileType<OpticRetinazer>(), this.Item.damage, knockback, new Vector2(), velocity);
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, ModContent.ProjectileType<OpticSpazmatism>(), this.Item.damage, knockback, new Vector2(), Vector2.op_UnaryNegation(velocity));
      return false;
    }
  }
}
