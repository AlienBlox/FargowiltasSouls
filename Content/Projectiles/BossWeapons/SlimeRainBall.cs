// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.SlimeRainBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.MutantBoss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class SlimeRainBall : MutantSlimeBall
  {
    public override string Texture => "FargowiltasSouls/Content/Projectiles/BossWeapons/SlimeBall";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = 1;
      this.CooldownSlot = -1;
    }

    public override void AI()
    {
      this.Projectile.tileCollide = (double) --this.Projectile.ai[1] < 0.0;
      base.AI();
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.AddBuff(137, 240, false);
    }

    public override void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(137, 240, true, false);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      return new Color?(new Color((int) ((Color) ref lightColor).R, (int) ((Color) ref lightColor).G, (int) byte.MaxValue, (int) ((Color) ref lightColor).A));
    }
  }
}
