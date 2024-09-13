// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Deathrays.MutantSpecialDeathray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Deathrays
{
  public abstract class MutantSpecialDeathray : BaseDeathray
  {
    private bool spawned;

    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Deathrays/MutantSpecialDeathray";
    }

    public MutantSpecialDeathray(int maxTime)
      : base((float) maxTime)
    {
    }

    public MutantSpecialDeathray(int maxTime, float hitboxModifier)
      : base((float) maxTime, hitboxModifier: hitboxModifier)
    {
    }

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.projFrames[this.Projectile.type] = 16;
    }

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        this.Projectile.frame = (int) Math.Abs((long) Main.GameUpdateCount % (long) Main.projFrames[this.Projectile.type]);
      }
      ++this.Projectile.frameCounter;
      if (++this.Projectile.frameCounter > 3)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      if (!Utils.NextBool(Main.rand, 10))
        return;
      this.Projectile.spriteDirection *= -1;
    }
  }
}
