// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.DragonFireballBoom
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class DragonFireballBoom : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_612";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = Main.projFrames[612];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.scale = 2f;
      this.Projectile.tileCollide = false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.Fuchsia);

    public virtual void AI()
    {
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter > 3)
      {
        ++this.Projectile.frame;
        this.Projectile.frameCounter = 0;
      }
      if (this.Projectile.frame <= Main.projFrames[this.Projectile.type])
        return;
      this.Projectile.Kill();
    }
  }
}
