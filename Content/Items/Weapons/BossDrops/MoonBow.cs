// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.MoonBow
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
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
  public class MoonBow : SoulsItem
  {
    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 125;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 28;
      ((Entity) this.Item).height = 62;
      this.Item.useTime = 9;
      this.Item.useAnimation = 9;
      this.Item.useStyle = 5;
      this.Item.knockBack = 2f;
      this.Item.value = Item.sellPrice(0, 15, 0, 0);
      this.Item.rare = 8;
      this.Item.UseSound = new SoundStyle?(SoundID.Item5);
      this.Item.shoot = 1;
      this.Item.autoReuse = true;
      this.Item.shootSpeed = 24f;
      this.Item.useAmmo = AmmoID.Arrow;
      this.Item.noMelee = true;
    }

    public virtual bool AltFunctionUse(Player player) => true;

    public virtual bool CanUseItem(Player player)
    {
      this.Item.useAmmo = player.altFunctionUse == 2 ? AmmoID.None : AmmoID.Arrow;
      this.Item.noUseGraphic = player.altFunctionUse == 2;
      this.Item.UseSound = new SoundStyle?(player.HasBuff<MoonBowBuff>() ? SoundID.Item124 : SoundID.Item5);
      return base.CanUseItem(player);
    }

    public virtual bool CanConsumeAmmo(Item ammo, Player player) => Utils.NextBool(Main.rand, 3);

    public virtual void ModifyShootStats(
      Player player,
      ref Vector2 position,
      ref Vector2 velocity,
      ref int type,
      ref int damage,
      ref float knockback)
    {
      if (player.altFunctionUse == 2)
      {
        type = ModContent.ProjectileType<MoonBowHeld>();
      }
      else
      {
        if (type == 1)
          type = 640;
        Vector2 vector2 = Vector2.op_Addition(position, Utils.RotatedBy(new Vector2(Utils.NextFloat(Main.rand, -12f, 12f), Utils.NextFloat(Main.rand, -28f, 28f)), (double) Utils.ToRotation(velocity), new Vector2()));
        if (Collision.SolidCollision(Vector2.op_Subtraction(vector2, new Vector2(4f, 4f)), 8, 8))
          return;
        position = vector2;
        if (!Vector2.op_Inequality(velocity, Vector2.Zero))
          return;
        velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo(vector2, Main.MouseWorld), ((Vector2) ref velocity).Length());
      }
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
      if (player.HasBuff<MoonBowBuff>())
        Projectile.NewProjectile((IEntitySource) source, ((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, Main.MouseWorld), ((Vector2) ref velocity).Length()), type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
  }
}
