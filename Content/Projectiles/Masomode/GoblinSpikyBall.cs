// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.GoblinSpikyBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class GoblinSpikyBall : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(24);
      this.AIType = 24;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 180;
      this.Projectile.scale = 1.5f;
    }

    public virtual void AI()
    {
      int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
      Main.dust[index].noGravity = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      this.Projectile.timeLeft = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, 0.0f, 0.0f, 100, new Color(), 3f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
  }
}
