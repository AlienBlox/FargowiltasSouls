// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.FishStickProjTornado
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
  public class FishStickProjTornado : ModProjectile
  {
    public const int TravelTime = 30;

    public virtual string Texture => FargoSoulsUtil.VanillaTextureProjectile(407);

    public virtual void SetStaticDefaults() => Main.projFrames[this.Type] = Main.projFrames[407];

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 48;
      ((Entity) this.Projectile).height = 48;
      this.Projectile.aiStyle = -1;
      this.AIType = -1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = 1;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.ignoreWater = true;
      this.Projectile.FargoSouls().CanSplit = false;
    }

    private ref float Timer => ref this.Projectile.ai[2];

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if (++this.Projectile.frameCounter > 4 && ++this.Projectile.frame >= Main.projFrames[this.Type])
        this.Projectile.frame = 0;
      if ((double) this.Timer >= 30.0)
        ((Entity) this.Projectile).velocity = Vector2.Zero;
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == ModContent.ProjectileType<FishStickProjTornado>() && p.owner == this.Projectile.owner && p.identity > this.Projectile.identity)))
        projectile.Kill();
      ++this.Timer;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Subtraction(((Entity) projectile).position, oldVelocity);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor);
      return false;
    }
  }
}
