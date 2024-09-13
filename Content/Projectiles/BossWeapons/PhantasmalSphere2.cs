// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.PhantasmalSphere2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class PhantasmalSphere2 : PhantasmalSphere
  {
    public override string Texture => "Terraria/Images/Projectile_454";

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 90;
      this.Projectile.penetrate = -1;
    }

    public override void AI()
    {
      this.Projectile.hide = false;
      if ((double) ++this.Projectile.localAI[0] == 20.0)
      {
        NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1500f), Array.Empty<int>());
        if (npc == null)
          this.Projectile.Kill();
        else
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) npc).velocity, Utils.NextFloat(Main.rand, 60f)))), 32f);
      }
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 20;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
      if (++this.Projectile.frameCounter < 6)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame <= 1)
        return;
      this.Projectile.frame = 0;
    }
  }
}
