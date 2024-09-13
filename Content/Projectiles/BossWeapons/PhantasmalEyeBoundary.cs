// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PhantasmalEyeBoundary
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PhantasmalEyeBoundary : PhantasmalEyeHoming
  {
    public override string Texture => "Terraria/Images/Projectile_452";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.timeLeft = 180;
      this.Projectile.extraUpdates = 2;
    }

    public override void AI()
    {
      if (this.Projectile.timeLeft % this.Projectile.MaxUpdates == 0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, Vector2.op_Subtraction(((Entity) Main.player[this.Projectile.owner]).position, ((Entity) Main.player[this.Projectile.owner]).oldPosition));
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if ((double) this.Projectile.localAI[0] < (double) ProjectileID.Sets.TrailCacheLength[this.Projectile.type])
        this.Projectile.localAI[0] += 0.1f;
      else
        this.Projectile.localAI[0] = (float) ProjectileID.Sets.TrailCacheLength[this.Projectile.type];
      this.Projectile.localAI[1] += 0.25f;
    }
  }
}
