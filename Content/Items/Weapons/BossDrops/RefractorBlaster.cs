// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Weapons.BossDrops.RefractorBlaster
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Weapons.BossDrops
{
  public class RefractorBlaster : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(514);
      this.Item.damage = 30;
      this.Item.useTime = 24;
      this.Item.useAnimation = 24;
      this.Item.shootSpeed = 15f;
      this.Item.value = 100000;
      this.Item.rare = 5;
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
      type = ModContent.ProjectileType<PrimeLaser>();
      int index = Projectile.NewProjectile(player.GetSource_ItemUse(this.Item, (string) null), position, velocity, type, damage, knockback, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
      if (index < 1000)
        RefractorBlaster.SplitProj(Main.projectile[index], 21);
      return false;
    }

    public static void SplitProj(Projectile projectile, int number)
    {
      if (number % 2 != 0)
        --number;
      double num1 = 1.5707963705062866 / (double) number;
      for (int index1 = 2; index1 < number / 2; ++index1)
      {
        for (int index2 = 0; index2 < 2; ++index2)
        {
          int num2 = index2 == 0 ? 1 : -1;
          Projectile.NewProjectile(((Entity) projectile).GetSource_FromThis((string) null), ((Entity) projectile).Center, Utils.RotatedBy(((Entity) projectile).velocity, (double) num2 * num1 * (double) (index1 + 1), new Vector2()), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1], 0.0f);
        }
      }
      ((Entity) projectile).active = false;
    }
  }
}
