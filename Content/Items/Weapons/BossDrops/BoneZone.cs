// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.BoneZone
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
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class BoneZone : SoulsItem
  {
    private int counter = 1;
    private static readonly int[] RiffVariants = new int[4]
    {
      1,
      2,
      3,
      4
    };
    private static readonly SoundStyle badtothebone;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 12;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 54;
      ((Entity) this.Item).height = 14;
      this.Item.useTime = this.Item.useAnimation = 24;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 1.5f;
      this.Item.UseSound = new SoundStyle?(BoneZone.badtothebone);
      this.Item.value = 50000;
      this.Item.rare = 3;
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<Bonez>();
      this.Item.shootSpeed = 5.5f;
      this.Item.useAmmo = 154;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-30f, 4f));

    public virtual bool Shoot(
      Player player,
      EntitySource_ItemUse_WithAmmo source,
      Vector2 position,
      Vector2 velocity,
      int type,
      int damage,
      float knockback)
    {
      int num;
      if (this.counter > 2)
      {
        num = 585;
        this.counter = 0;
      }
      else
        num = ModContent.ProjectileType<Bonez>();
      Main.projectile[Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), position, velocity, num, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f)].DamageType = DamageClass.Ranged;
      ++this.counter;
      return false;
    }

    public virtual void ModifyWeaponDamage(Player player, ref StatModifier damage)
    {
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => !Utils.NextBool(Main.rand, 3);

    static BoneZone()
    {
      SoundStyle soundStyle;
      // ISSUE: explicit constructor call
      ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Boneriff/boneriff", (SoundType) 0);
      ((SoundStyle) ref soundStyle).Variants = ReadOnlySpan<int>.op_Implicit(BoneZone.RiffVariants);
      ((SoundStyle) ref soundStyle).Volume = 0.15f;
      BoneZone.badtothebone = soundStyle;
    }
  }
}
