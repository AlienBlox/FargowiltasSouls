// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.GoldShellProj
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class GoldShellProj : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 56;
      ((Entity) this.Projectile).height = 56;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 18000;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.GoldShell = false;
      if (!fargoSoulsPlayer.GoldShell)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).position.X = ((Entity) Main.player[this.Projectile.owner]).Center.X - (float) (((Entity) this.Projectile).width / 2);
        ((Entity) this.Projectile).position.Y = ((Entity) Main.player[this.Projectile.owner]).Center.Y - (float) (((Entity) this.Projectile).height / 2);
      }
    }
  }
}
