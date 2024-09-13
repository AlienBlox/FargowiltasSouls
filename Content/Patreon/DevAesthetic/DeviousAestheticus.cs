// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DevAesthetic.DeviousAestheticus
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
namespace FargowiltasSouls.Content.Patreon.DevAesthetic
{
  public class DeviousAestheticus : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      this.Item.damage = 366;
      this.Item.DamageType = DamageClass.Summon;
      this.Item.mana = 10;
      ((Entity) this.Item).width = 40;
      ((Entity) this.Item).height = 40;
      this.Item.useTime = 10;
      this.Item.useAnimation = 10;
      this.Item.useStyle = 1;
      this.Item.knockBack = 1f;
      this.Item.value = Item.sellPrice(0, 20, 0, 0);
      this.Item.rare = 2;
      this.Item.UseSound = new SoundStyle?(SoundID.Item1);
      this.Item.autoReuse = true;
      this.Item.shoot = ModContent.ProjectileType<DevRocket>();
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
      damage = (int) ((double) damage / 4.0 * 1.3);
      float num1 = 0.0f;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        if (((Entity) Main.projectile[index]).active && !Main.projectile[index].hostile && Main.projectile[index].owner == ((Entity) player).whoAmI && (double) Main.projectile[index].minionSlots > 0.0)
          num1 += Main.projectile[index].minionSlots;
      }
      float num2 = (float) player.maxMinions - num1;
      if ((double) num2 < 1.0)
        num2 = 1f;
      if ((double) num2 > 7.0)
        num2 = 7f;
      float radians = MathHelper.ToRadians(17.1428566f);
      if ((double) num2 % 2.0 == 0.0)
      {
        Vector2 vector2 = Utils.RotatedBy(velocity, (double) radians * (-(double) num2 / 2.0 + 0.5), new Vector2());
        for (int index = 0; (double) index < (double) num2; ++index)
          Projectile.NewProjectile((IEntitySource) source, position, Utils.RotatedBy(vector2, (double) radians * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)), new Vector2()), type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      else
      {
        Vector2 vector2 = velocity;
        int num3 = (int) num2 / 2;
        for (int index = -num3; index <= num3; ++index)
          Projectile.NewProjectile((IEntitySource) source, position, Utils.RotatedBy(vector2, (double) radians * ((double) index + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)), new Vector2()), type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      return false;
    }
  }
}
