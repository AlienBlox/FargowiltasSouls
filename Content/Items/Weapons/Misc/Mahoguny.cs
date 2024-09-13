// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.Misc.Mahoguny
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

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
  public class Mahoguny : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.damage = 59;
      this.Item.DamageType = DamageClass.Ranged;
      ((Entity) this.Item).width = 58;
      ((Entity) this.Item).height = 26;
      this.Item.useTime = 33;
      this.Item.useAnimation = 33;
      this.Item.useStyle = 5;
      this.Item.noMelee = true;
      this.Item.knockBack = 5f;
      this.Item.value = Item.sellPrice(0, 8, 0, 0);
      this.Item.rare = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item61);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<AcornProjectile>();
      this.Item.shootSpeed = 18f;
      this.Item.useAmmo = 27;
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
      Vector2 vector2_1 = Vector2.op_Multiply(Vector2.Normalize(velocity), 46f);
      if (Collision.CanHit(position, 0, 0, Vector2.op_Addition(position, vector2_1), 0, 0))
        position = Vector2.op_Addition(position, vector2_1);
      int num = 2 + Main.rand.Next(2);
      for (int index = 0; index < num; ++index)
      {
        Vector2 vector2_2 = Utils.RotatedByRandom(velocity, (double) MathHelper.ToRadians(30f));
        Projectile.NewProjectile((IEntitySource) source, position.X, position.Y, vector2_2.X, vector2_2.Y, ModContent.ProjectileType<MahogunyLeafProjectile>(), (int) ((double) damage * 0.6), knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      return true;
    }

    public virtual Vector2? HoldoutOffset() => new Vector2?(new Vector2(-3f, -3f));
  }
}
