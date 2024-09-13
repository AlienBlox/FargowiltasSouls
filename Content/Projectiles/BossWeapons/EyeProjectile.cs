﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.EyeProjectile
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class EyeProjectile : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 16;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Melee;
      this.Projectile.penetrate = 1;
      this.Projectile.timeLeft = 180;
      this.AIType = 14;
    }

    public virtual void AI()
    {
      if (!Collision.SolidCollision(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height))
      {
        int index = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
        Main.dust[index].noGravity = true;
      }
      ++this.Projectile.ai[0];
      if ((double) this.Projectile.ai[0] <= 10.0)
        return;
      this.Projectile.ai[0] = 10f;
      NPC npc = FargoSoulsUtil.NPCExists(FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 1000f), Array.Empty<int>());
      if (!npc.Alive())
        return;
      ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 10f), 0.1f);
    }

    public virtual void OnKill(int timeleft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).Center, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 60, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
    }
  }
}
