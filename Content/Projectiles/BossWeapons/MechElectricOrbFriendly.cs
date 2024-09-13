// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.MechElectricOrbFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class MechElectricOrbFriendly : MechElectricOrb
  {
    private bool hasIframes = true;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/MechElectricOrb";
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 75;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = -1;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!(source is EntitySource_Parent entitySourceParent) || !(entitySourceParent.Entity is Projectile entity) || entity.type != ModContent.ProjectileType<RefractorBlaster2Held>())
        return;
      this.Projectile.penetrate = 1;
      this.hasIframes = false;
    }

    public override void AI()
    {
      this.Projectile.rotation += (float) (((double) Math.Abs(((Entity) this.Projectile).velocity.X) + (double) Math.Abs(((Entity) this.Projectile).velocity.Y)) * 0.0099999997764825821) * (float) ((Entity) this.Projectile).direction;
      this.Projectile.soundDelay = 0;
      if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 22.0)
      {
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 22f);
      }
      Projectile projectile1 = this.Projectile;
      ((Entity) projectile1).velocity = Vector2.op_Multiply(((Entity) projectile1).velocity, 1.02f);
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (!this.hasIframes)
        return;
      target.immune[this.Projectile.owner] = 6;
    }

    public virtual bool PreKill(int timeleft)
    {
      int num1 = 10;
      int num2 = 3;
      float colorType = this.ColorType;
      int num3 = (double) colorType == 1.0 ? 59 : ((double) colorType == 3.0 ? 61 : ((double) colorType == 2.0 ? 64 : 60));
      for (int index = 0; index < num1; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num3, ((Entity) this.Projectile).velocity.X * 0.1f, ((Entity) this.Projectile).velocity.Y * 0.1f, 150, new Color(), 1.2f);
      for (int index = 0; index < num2; ++index)
      {
        Main.rand.Next(16, 18);
        int type = this.Projectile.type;
      }
      for (int index = 0; index < 10; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, num3, ((Entity) this.Projectile).velocity.X * 0.1f, ((Entity) this.Projectile).velocity.Y * 0.1f, 150, new Color(), 1.2f);
      return false;
    }
  }
}
