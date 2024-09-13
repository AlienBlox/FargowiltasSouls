// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.LihzahrdSpikyBallFriendly
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class LihzahrdSpikyBallFriendly : ModProjectile
  {
    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(185);
      this.AIType = 185;
      this.Projectile.hostile = false;
      this.Projectile.trap = false;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(70, 300, false);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(70, 300, true, false);
    }
  }
}
