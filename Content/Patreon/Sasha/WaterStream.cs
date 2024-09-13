// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Sasha.WaterStream
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Sasha
{
  public class WaterStream : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(22);
      this.AIType = 22;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 60;
    }
  }
}
