// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeTransitionBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeTransitionBomb : LifeBomb
  {
    private Vector2 OriginalPosition = Vector2.Zero;
    private Vector2 DesiredPosition = Vector2.Zero;

    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Lifelight/LifeBomb";

    public override void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        this.OriginalPosition = ((Entity) this.Projectile).position;
        this.DesiredPosition = new Vector2(this.Projectile.ai[1], this.Projectile.ai[2]);
        this.Projectile.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, this.DesiredPosition)) - 1.57079637f;
      }
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 87, ((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y, 0, new Color(), 0.25f);
      ++this.Projectile.ai[0];
      ((Entity) this.Projectile).Center = Vector2.Lerp(this.OriginalPosition, this.DesiredPosition, Math.Min(this.Projectile.ai[0] / 90f, 1f));
      if ((double) this.Projectile.ai[0] < 100.0)
        return;
      this.Projectile.Kill();
    }
  }
}
