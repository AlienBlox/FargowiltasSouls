// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeScar
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  public class LifeScar : ModProjectile
  {
    private bool init;

    public virtual string Texture => "FargowiltasSouls/Content/Bosses/Lifelight/LifeBombExplosion";

    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 3;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 50;
      ((Entity) this.Projectile).height = 50;
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.Opacity = 0.0f;
    }

    public virtual bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
    {
      return new bool?((double) ((Entity) this.Projectile).Distance(FargoSoulsUtil.ClosestPointInHitbox(targetHitbox, ((Entity) this.Projectile).Center)) < (double) (projHitbox.Width / 2));
    }

    public virtual void AI()
    {
      if (!this.init)
      {
        this.Projectile.rotation = MathHelper.ToRadians((float) Main.rand.Next(360));
        this.init = true;
      }
      if (++this.Projectile.frameCounter >= 5)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= 3)
          this.Projectile.frame = 0;
      }
      this.Projectile.rotation += 0.5f;
      if ((double) ++this.Projectile.localAI[0] <= 60.0)
        this.Projectile.Opacity += 0.05f;
      if ((double) ++this.Projectile.ai[0] >= (double) this.Projectile.ai[1] - 30.0)
        this.Projectile.Opacity -= 0.0166666675f;
      if ((double) this.Projectile.ai[0] >= (double) this.Projectile.ai[1])
        this.Projectile.damage = 0;
      if ((double) this.Projectile.ai[0] > (double) this.Projectile.ai[1] + 30.0)
        this.Projectile.Kill();
      if ((double) this.Projectile.localAI[1] == 0.0)
        this.Projectile.localAI[1] += (float) Main.rand.Next(60);
      this.Projectile.scale = (float) (1.1000000238418579 + 0.10000000149011612 * Math.Sin(0.41887903213500977 * (double) ++this.Projectile.localAI[1]));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 180, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity), 0.9f));
    }
  }
}
