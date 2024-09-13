// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DemonKing.StaffOfUnleashedOcean
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DemonKing
{
  public class StaffOfUnleashedOcean : PatreonModItem
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ItemID.Sets.StaffMinionSlotsRequired[this.Item.type] = 1f;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 115;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 10;
      ((Entity) this.Item).width = 26;
      ((Entity) this.Item).height = 28;
      this.Item.useTime = 36;
      this.Item.useAnimation = 36;
      this.Item.useStyle = 1;
      this.Item.noMelee = true;
      this.Item.knockBack = 4f;
      this.Item.rare = 11;
      this.Item.UseSound = new SoundStyle?(SoundID.Zombie20);
      this.Item.shoot = ModContent.ProjectileType<DukeFishronMinion>();
      this.Item.shootSpeed = 10f;
      this.Item.buffType = ModContent.BuffType<DukeFishronBuff>();
      this.Item.autoReuse = true;
      this.Item.value = Item.sellPrice(0, 25, 0, 0);
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
      player.SpawnMinionOnCursor((IEntitySource) source, ((Entity) player).whoAmI, type, this.Item.damage, knockback, new Vector2(), velocity);
      return false;
    }
  }
}
