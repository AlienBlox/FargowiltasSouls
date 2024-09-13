// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.TurtleShield
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class TurtleShield : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 7;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 54;
      ((Entity) this.Projectile).height = 54;
      this.Projectile.penetrate = 1;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      ((Entity) this.Projectile).Center = ((Entity) player).Center;
      if (this.Projectile.frame != 6)
      {
        ++this.Projectile.frameCounter;
        if (this.Projectile.frameCounter >= 4)
        {
          this.Projectile.frameCounter = 0;
          this.Projectile.frame = (this.Projectile.frame + 1) % 7;
        }
      }
      if (fargoSoulsPlayer.TurtleShellHP <= 3)
        this.Projectile.localAI[0] = 1f;
      if (fargoSoulsPlayer.ShellHide)
        return;
      this.Projectile.Kill();
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundStyle soundStyle = SoundID.Item27;
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return (double) this.Projectile.localAI[0] == 1.0 ? new Color?(new Color((int) byte.MaxValue, 132, 105)) : base.GetAlpha(lightColor);
    }
  }
}
