﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SkeletonArcherArrow
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class SkeletonArcherArrow : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(282);
      this.AIType = 282;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.arrow = false;
      this.Projectile.hostile = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(70, 180, true, false);
    }
  }
}