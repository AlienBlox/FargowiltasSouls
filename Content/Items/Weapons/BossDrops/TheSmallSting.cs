// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.TheSmallSting
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class TheSmallSting : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ItemID.Sets.ShimmerTransformToItem[this.Type] = ModContent.ItemType<HiveStaff>();
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 45;
      this.Item.crit = 0;
      this.Item.DamageType = DamageClass.Ranged;
      this.Item.useTime = 36;
      this.Item.useAnimation = 36;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 1.5f;
      this.Item.value = 50000;
      this.Item.rare = 3;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<SmallStinger>();
      this.Item.useAmmo = AmmoID.Dart;
      this.Item.UseSound = new SoundStyle?(SoundID.Item97);
      this.Item.shootSpeed = 40f;
      ((Entity) this.Item).width = 44;
      ((Entity) this.Item).height = 16;
    }

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      type = this.Item.shoot;
      float num = 1f;
      if (player.strongBees)
        num += 0.1f;
      damage = (int) ((double) damage * (double) num);
      knockback = (float) (int) ((double) knockback * (double) num);
    }

    public override void SafeModifyTooltips(List<TooltipLine> tooltips)
    {
      tooltips.Remove(tooltips.FirstOrDefault<TooltipLine>((Func<TooltipLine, bool>) (line => line.Name == "CritChance" && line.Mod == "Terraria")));
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-10f, 0.0f));

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand);
  }
}
