// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sasha.Bubble
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sasha
{
  public class Bubble : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(410);
      this.AIType = 410;
      this.Projectile.DamageType = DamageClass.Magic;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 2f;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 7;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.owner != Main.myPlayer)
        return;
      Vector2 vector2 = Vector2.op_Multiply(5f, Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI));
      for (int index = 0; index < 4; ++index)
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Utils.RotatedBy(vector2, Math.PI / 2.0 * (double) index, new Vector2()), 22, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }
  }
}
