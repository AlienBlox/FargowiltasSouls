// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.RockSlide
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class RockSlide : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 88;
      this.Item.DamageType = DamageClass.Magic;
      ((Entity) this.Item).width = 38;
      ((Entity) this.Item).height = 46;
      this.Item.useTime = 12;
      this.Item.useAnimation = 12;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 2f;
      this.Item.value = 100000;
      this.Item.rare = 8;
      this.Item.mana = 10;
      this.Item.UseSound = new SoundStyle?(SoundID.Item21);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<GolemGib>();
      this.Item.shootSpeed = 12f;
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
      float shootSpeed = this.Item.shootSpeed;
      int damage1 = this.Item.damage;
      float knockBack = this.Item.knockBack;
      float weaponKnockback = player.GetWeaponKnockback(this.Item, knockBack);
      player.itemTime = this.Item.useTime;
      Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, false, true);
      Utils.RotatedBy(Vector2.UnitX, (double) player.fullRotation, new Vector2());
      float f1 = (float) Main.mouseX + Main.screenPosition.X - vector2.X;
      float f2 = (float) Main.mouseY + Main.screenPosition.Y - vector2.Y;
      if ((double) player.gravDir == -1.0)
        f2 = Main.screenPosition.Y + (float) Main.screenHeight - (float) Main.mouseY - vector2.Y;
      float num1 = (float) Math.Sqrt((double) f1 * (double) f1 + (double) f2 * (double) f2);
      float num2;
      if (float.IsNaN(f1) && float.IsNaN(f2) || (double) f1 == 0.0 && (double) f2 == 0.0)
      {
        f1 = (float) ((Entity) player).direction;
        f2 = 0.0f;
        num2 = shootSpeed;
      }
      else
        num2 = shootSpeed / num1;
      float num3 = f1 * num2;
      float num4 = f2 * num2;
      int num5 = 2;
      if (Utils.NextBool(Main.rand))
        ++num5;
      if (Utils.NextBool(Main.rand, 4))
        ++num5;
      if (Utils.NextBool(Main.rand, 8))
        ++num5;
      if (Utils.NextBool(Main.rand, 16))
        ++num5;
      for (int index = 0; index < num5; ++index)
      {
        float num6 = num3;
        float num7 = num4;
        float num8 = 0.05f * (float) index;
        float num9 = num6 + (float) Main.rand.Next(-25, 26) * num8;
        float num10 = num7 + (float) Main.rand.Next(-25, 26) * num8;
        float num11 = (float) Math.Sqrt((double) num9 * (double) num9 + (double) num10 * (double) num10);
        float num12 = shootSpeed / num11;
        float num13 = num9 * num12;
        float num14 = num10 * num12;
        Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), position.X, position.Y, num13, num14, ModContent.ProjectileType<GolemGib>(), damage1, weaponKnockback, Main.myPlayer, 0.0f, (float) Main.rand.Next(1, 12), 0.0f);
      }
      return false;
    }
  }
}
