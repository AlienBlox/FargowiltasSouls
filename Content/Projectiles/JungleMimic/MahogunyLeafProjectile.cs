// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.MahogunyLeafProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class MahogunyLeafProjectile : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      this.AIType = 14;
      ((Entity) this.Projectile).width = 5;
      ((Entity) this.Projectile).height = 9;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
      this.Projectile.ignoreWater = false;
      this.Projectile.tileCollide = true;
    }

    public virtual void AI()
    {
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      if (++this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = ++this.Projectile.frame % Main.projFrames[this.Projectile.type];
      }
      if (!Utils.NextBool(Main.rand, 3))
        return;
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 3, ((Entity) this.Projectile).velocity.X * 0.25f, ((Entity) this.Projectile).velocity.Y * 0.25f, 150, new Color(), 0.7f);
    }

    public virtual void OnKill(int timeLeft)
    {
      Collision.HitTiles(Vector2.op_Addition(((Entity) this.Projectile).position, ((Entity) this.Projectile).velocity), ((Entity) this.Projectile).velocity, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
      SoundEngine.PlaySound(ref SoundID.Grass, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index = 0; index < 35; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 39, ((Entity) this.Projectile).velocity.X * 0.25f, ((Entity) this.Projectile).velocity.Y * 0.25f, 150, new Color(), 0.7f);
    }
  }
}
