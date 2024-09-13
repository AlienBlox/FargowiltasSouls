// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.AbomSpecialDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public abstract class AbomSpecialDeathray : BaseDeathray
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/AbomSpecialDeathray";
    }

    public AbomSpecialDeathray(int maxTime)
      : base((float) maxTime)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 11;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] % (double) this.Projectile.MaxUpdates != 0.0 || ++this.Projectile.frameCounter <= 2)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.frame = 0;
    }
  }
}
