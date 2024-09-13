// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeSplittingProjSmall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeSplittingProjSmall : LifeProjSmall
  {
    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Lifelight/LifeProjSmall";

    public override void AI()
    {
      if ((double) this.Projectile.ai[0] == 45.0)
      {
        int damage = this.Projectile.damage;
        float num = 3f;
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 2f);
        if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 15.0)
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(15f, Vector2.Normalize(((Entity) this.Projectile).velocity));
        Vector2 vector2_1 = Utils.RotatedBy(((Entity) this.Projectile).velocity, -1.0 * Math.PI / 3.0, new Vector2());
        Vector2 vector2_2 = Utils.RotatedBy(((Entity) this.Projectile).velocity, Math.PI / 3.0, new Vector2());
        if (FargoSoulsUtil.HostCheck)
        {
          int index = -1;
          if ((double) this.Projectile.ai[1] != 11.0 && (double) this.Projectile.ai[1] != 9.0)
            index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_1, ModContent.ProjectileType<LifeProjSmall>(), damage, num, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if ((double) this.Projectile.ai[1] != 10.0 && (double) this.Projectile.ai[1] != 8.0)
            index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_2, ModContent.ProjectileType<LifeProjSmall>(), damage, num, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index != -1 && index != Main.maxProjectiles)
          {
            Main.projectile[index].hostile = this.Projectile.hostile;
            Main.projectile[index].friendly = this.Projectile.friendly;
          }
        }
      }
      base.AI();
    }
  }
}
