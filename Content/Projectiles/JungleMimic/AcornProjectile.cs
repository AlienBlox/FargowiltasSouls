// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.AcornProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class AcornProjectile : ModProjectile
  {
    public float bounce = 1f;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.aiStyle = 0;
      ((Entity) this.Projectile).width = 18;
      ((Entity) this.Projectile).height = 28;
      this.Projectile.friendly = true;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
      this.Projectile.ignoreWater = false;
      this.Projectile.tileCollide = true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      ++this.bounce;
      if ((double) this.bounce == 4.0)
        this.Projectile.Kill();
      if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X && (double) Math.Abs(oldVelocity.X) > 0.10000000149011612)
        ((Entity) this.Projectile).velocity.X = oldVelocity.X * -0.8f;
      if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y && (double) Math.Abs(oldVelocity.Y) > 0.10000000149011612)
        ((Entity) this.Projectile).velocity.Y = oldVelocity.Y * -0.8f;
      return false;
    }

    public virtual void AI()
    {
      this.Projectile.rotation += 0.2f * (float) ((Entity) this.Projectile).direction;
      if ((double) ((Entity) this.Projectile).velocity.Y < 0.0)
        ((Entity) this.Projectile).velocity.Y += 0.3f;
      else
        ((Entity) this.Projectile).velocity.Y += 0.4f;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner == Main.myPlayer)
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<AcornProjectileExplosion>(), this.Projectile.damage / 2, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2);
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 120;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X - (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y - (float) (((Entity) this.Projectile).height / 2);
    }
  }
}
