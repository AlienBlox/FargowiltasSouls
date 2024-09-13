// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MimicHallowStar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MimicHallowStar : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_92";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(92);
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.Projectile.light = 1f;
      this.CooldownSlot = 1;
    }

    public virtual void PostAI()
    {
      this.Projectile.tileCollide = false;
      this.Projectile.light = 1f;
    }
  }
}
