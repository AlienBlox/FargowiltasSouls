// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.StyxGazerArmor
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.BossWeapons;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class StyxGazerArmor : StyxGazer
  {
    public override void AI()
    {
      this.Projectile.damage = FargoSoulsUtil.HighestDamageTypeScaling(Main.player[this.Projectile.owner], 666);
      base.AI();
      Main.player[this.Projectile.owner].itemTime = 0;
      Main.player[this.Projectile.owner].itemAnimation = 0;
      if (Main.player[this.Projectile.owner].reuseDelay >= 17)
        return;
      Main.player[this.Projectile.owner].reuseDelay = 17;
    }

    public override bool? CanHitNPC(NPC target)
    {
      return this.Projectile.localNPCImmunity[((Entity) target).whoAmI] >= 15 ? new bool?(false) : new bool?();
    }
  }
}
