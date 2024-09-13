// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.GrazeRing
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class GrazeRing : GlowRingHollow
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/GlowRingHollow";

    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.alpha = 0;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.color = Color.HotPink;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public override void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!((Entity) player).active || player.dead || player.ghost || this.Projectile.owner == Main.myPlayer && (!fargoSoulsPlayer.Graze || !player.HasEffect<MasoGrazeRing>()))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (fargoSoulsPlayer.CirnoGraze)
          this.color = Color.Cyan;
        else if (fargoSoulsPlayer.DeviGraze)
          this.color = Color.HotPink;
        float num = 42f + fargoSoulsPlayer.GrazeRadius;
        this.Projectile.timeLeft = 2;
        ((Entity) this.Projectile).Center = ((Entity) player).Center;
        this.Projectile.Opacity = (float) Main.mouseTextColor / (float) byte.MaxValue;
        this.Projectile.Opacity *= this.Projectile.Opacity;
        this.Projectile.scale = (float) ((double) num * 2.0 / 1000.0);
        ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
        ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = (int) (1000.0 * (double) this.Projectile.scale);
        ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      }
    }

    public override Color? GetAlpha(Color lightColor)
    {
      Color? alpha = base.GetAlpha(lightColor);
      float num = 0.8f;
      Color? nullable = alpha.HasValue ? new Color?(Color.op_Multiply(alpha.GetValueOrDefault(), num)) : new Color?();
      float opacity = this.Projectile.Opacity;
      return !nullable.HasValue ? new Color?() : new Color?(Color.op_Multiply(nullable.GetValueOrDefault(), opacity));
    }
  }
}
