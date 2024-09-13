// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.Bee
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class Bee : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_566";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 4;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 600;
      this.Projectile.aiStyle = -1;
    }

    public virtual void AI()
    {
      this.Projectile.spriteDirection = Math.Sign(((Entity) this.Projectile).velocity.X);
      this.Projectile.rotation = ((Entity) this.Projectile).velocity.X * 0.1f;
      if (++this.Projectile.frameCounter >= 3)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 3)
          this.Projectile.frame = 0;
      }
      if ((((Entity) this.Projectile).wet || ((Entity) this.Projectile).lavaWet) && !((Entity) this.Projectile).honeyWet)
        this.Projectile.Kill();
      if ((double) ++this.Projectile.localAI[0] > 30.0 && (double) this.Projectile.localAI[0] < 90.0)
      {
        float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
        float rotation2 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center));
        ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, this.Projectile.ai[1]), new Vector2());
      }
      this.Projectile.tileCollide = (double) this.Projectile.localAI[0] > 180.0;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(20, 300, true, false);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 600, true, false);
    }
  }
}
