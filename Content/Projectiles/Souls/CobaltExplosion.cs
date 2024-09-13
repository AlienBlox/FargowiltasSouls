// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.CobaltExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class CobaltExplosion : ModProjectile
  {
    private int fpf = 4;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Type] = 5;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 5;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 0;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 120;
      ((Entity) this.Projectile).height = 120;
      this.Projectile.aiStyle = 0;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = Main.projFrames[this.Type] * this.fpf;
      this.Projectile.tileCollide = false;
      this.Projectile.light = 0.75f;
      this.Projectile.ignoreWater = true;
      this.AIType = 14;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = -1;
      this.Projectile.FargoSouls().DeletionImmuneRank = 2;
      this.Projectile.scale = 1.5f;
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      this.Projectile.rotation = Utils.NextFloat(Main.rand, 6.28318548f);
    }

    public virtual void AI()
    {
      if (++this.Projectile.frameCounter > this.fpf)
      {
        this.Projectile.frameCounter = 0;
        ++this.Projectile.frame;
      }
      if (this.Projectile.frame >= Main.projFrames[this.Type])
        this.Projectile.Kill();
      ((Entity) this.Projectile).velocity = Vector2.Zero;
    }

    public virtual void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
    {
      target.AddBuff(204, 600, false);
    }

    public virtual bool PreDraw(ref Color lightColor)
    {
      FargoSoulsUtil.GenericProjectileDraw(this.Projectile, lightColor);
      return false;
    }
  }
}
