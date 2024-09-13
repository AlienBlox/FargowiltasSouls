// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.FishStickWhirlpool
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class FishStickWhirlpool : FishStickProj
  {
    public const int TornadoHeight = 12;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/BossWeapons/FishStickProj";
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 30;
    }

    public override void OnKill(int timeLeft)
    {
      base.OnKill(timeLeft);
      if (this.Projectile.owner != Main.myPlayer)
        return;
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.ModProjectile is Whirlpool)))
        projectile.Kill();
      Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<WhirlpoolBase>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 10f, 12f, 0.0f);
    }
  }
}
