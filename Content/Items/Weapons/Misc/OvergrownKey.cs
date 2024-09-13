// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Misc.OvergrownKey
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Minions;
using FargowiltasSouls.Content.Projectiles.JungleMimic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.Misc
{
  public class OvergrownKey : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.StaffMinionSlotsRequired[this.Item.type] = 2f;
    }

    public virtual void SetDefaults()
    {
      this.Item.mana = 10;
      this.Item.damage = 47;
      this.Item.useStyle = 5;
      this.Item.shootSpeed = 14f;
      ((Entity) this.Item).width = 36;
      ((Entity) this.Item).height = 16;
      this.Item.UseSound = new SoundStyle?(SoundID.Item77);
      this.Item.useAnimation = 37;
      this.Item.useTime = 37;
      this.Item.noMelee = true;
      this.Item.value = Item.sellPrice(0, 8, 0, 0);
      this.Item.knockBack = 2f;
      this.Item.rare = 4;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.shoot = ModContent.ProjectileType<JungleMimicSummon>();
      this.Item.buffType = ModContent.BuffType<JungleMimicSummonBuff>();
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
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, type, this.Item.damage, knockback, new Vector2(), new Vector2());
      return false;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(0.0f, 0.0f));
  }
}
