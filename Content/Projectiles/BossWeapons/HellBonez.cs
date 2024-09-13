// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HellBonez
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HellBonez : Bonez
  {
    public override void SetStaticDefaults()
    {
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
    }

    public override void SetDefaults()
    {
      ((Entity) this.Projectile).width = 20;
      ((Entity) this.Projectile).height = 20;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = -1;
      this.Projectile.scale = 2f;
      this.Projectile.timeLeft = 120;
      this.Projectile.extraUpdates = 1;
    }

    public override void AI()
    {
      this.Projectile.rotation += 0.3f * (float) Math.Sign(((Entity) this.Projectile).velocity.X);
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      target.immune[this.Projectile.owner] = 8;
      target.AddBuff(ModContent.BuffType<HellFireBuff>(), 60, false);
    }

    public override void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width / 2, ((Entity) this.Projectile).height / 2, 190, ((Entity) this.Projectile).velocity.X * 0.75f, ((Entity) this.Projectile).velocity.Y * 0.75f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
      }
    }
  }
}
