// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviGuardianHarmless
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviGuardianHarmless : DeviGuardian
  {
    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.Opacity = 0.5f;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void PostAI()
    {
      base.PostAI();
      this.Projectile.Opacity -= 0.008333334f;
    }
  }
}
