// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.BrainMinion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class BrainMinion : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 11;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 74;
      ((Entity) this.Projectile).height = 70;
      this.Projectile.netImportant = true;
      this.Projectile.friendly = true;
      this.Projectile.timeLeft = 18000;
      this.Projectile.penetrate = -1;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (player.dead)
        fargoSoulsPlayer.BrainMinion = false;
      if (fargoSoulsPlayer.BrainMinion)
        this.Projectile.timeLeft = 2;
      ++this.Projectile.frameCounter;
      if (this.Projectile.frameCounter >= 8)
      {
        this.Projectile.frameCounter = 0;
        this.Projectile.frame = (this.Projectile.frame + 1) % 11;
      }
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 0.05f);
      ++this.Projectile.ai[0];
      this.Projectile.alpha = (int) (Math.Cos((double) this.Projectile.ai[0] * 6.2831854820251465 / 180.0) * 122.5 + 122.5);
      if ((double) this.Projectile.ai[0] != 180.0)
        return;
      ((Entity) this.Projectile).Center = Vector2.op_Addition(((Entity) player).Center, Utils.NextVector2CircularEdge(Main.rand, 300f, 300f));
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 8f);
      this.Projectile.netUpdate = true;
      this.Projectile.ai[0] = 0.0f;
    }
  }
}
