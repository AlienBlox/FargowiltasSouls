// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HentaiSpearDive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HentaiSpearDive : HentaiSpear
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear";

    public override void AI()
    {
      base.AI();
      ++this.Projectile.localAI[0];
    }

    public override bool? CanDamage()
    {
      return (double) this.Projectile.localAI[0] > 2.0 ? new bool?(true) : new bool?();
    }
  }
}
