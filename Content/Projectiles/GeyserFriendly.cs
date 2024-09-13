﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GeyserFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GeyserFriendly : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(654);
      this.Projectile.trap = false;
      this.Projectile.hostile = false;
      this.Projectile.DamageType = DamageClass.Melee;
      this.AIType = 654;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(24, 600, false);
    }
  }
}
