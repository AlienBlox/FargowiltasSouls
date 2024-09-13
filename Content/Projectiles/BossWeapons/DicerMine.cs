// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.DicerMine
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  internal class DicerMine : ModProjectile
  {
    public int Counter = 1;

    public virtual void SetDefaults()
    {
      this.Projectile.extraUpdates = 0;
      ((Entity) this.Projectile).width = 19;
      ((Entity) this.Projectile).height = 19;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.scale = 1f;
      this.Projectile.timeLeft = 90;
    }

    public virtual void AI()
    {
      if (this.Counter >= 75)
      {
        this.Projectile.scale += 0.1f;
        this.Projectile.rotation += 0.2f;
      }
      ++this.Counter;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      int num = ModContent.ProjectileType<DicerSpray>();
      Vector2 vector2 = Utils.NextBool(Main.rand) ? Vector2.UnitX : Utils.RotatedBy(Vector2.UnitX, Math.PI / 8.0, new Vector2());
      for (int index = 0; index < 8; ++index)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Multiply(5f, Utils.RotatedBy(vector2, Math.PI / 4.0 * (double) index, new Vector2())), num, this.Projectile.damage, this.Projectile.knockBack, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }
  }
}
