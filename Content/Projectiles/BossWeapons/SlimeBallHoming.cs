// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SlimeBallHoming
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SlimeBallHoming : SlimeBall
  {
    private int bounce;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/SlimeBall";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.penetrate = 2;
    }

    public override void AI()
    {
      base.AI();
      if (this.bounce == 0)
        this.bounce = this.Projectile.timeLeft - Main.rand.Next(150);
      if (this.Projectile.timeLeft != this.bounce)
        return;
      this.bounce = 0;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Main.MouseWorld), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
      this.Projectile.netUpdate = true;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      base.OnHitNPC(target, hit, damageDone);
      target.immune[this.Projectile.owner] = 9;
    }
  }
}
