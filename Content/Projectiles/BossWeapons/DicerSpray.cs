// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.DicerSpray
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  internal class DicerSpray : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_484";

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(484);
      this.AIType = 484;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 120;
    }
  }
}
