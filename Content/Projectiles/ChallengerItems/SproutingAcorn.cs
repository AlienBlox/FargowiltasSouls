// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.SproutingAcorn
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class SproutingAcorn : Acorn
  {
    public override string Texture
    {
      get => "FargowiltasSouls/Content/Bosses/Champions/Timber/TimberAcorn";
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = 1;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      int sproutType = ModContent.ProjectileType<Sprout>();
      if (this.Projectile.owner != Main.myPlayer || ((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == sproutType && p.owner == this.Projectile.owner && (double) p.ai[0] == (double) ((Entity) target).whoAmI)))
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) target).Center, Vector2.Zero, sproutType, this.Projectile.damage, 0.0f, Main.myPlayer, (float) ((Entity) target).whoAmI, 0.0f, 0.0f);
    }

    public override void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index = 0; index < 10; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 7, 0.0f, 0.0f, 0, new Color(), 1f);
    }
  }
}
