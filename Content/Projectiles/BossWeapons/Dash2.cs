// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.Dash2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class Dash2 : Dash
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/Dash";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 900 * (this.Projectile.extraUpdates + 1);
    }
  }
}
