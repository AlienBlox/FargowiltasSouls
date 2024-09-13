// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.CirnoBomb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles
{
  public class CirnoBomb : GlowRing
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/GlowRing";

    public override void SetStaticDefaults()
    {
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.alpha = 0;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.color = Color.Cyan;
      this.Projectile.FargoSouls().TimeFreezeImmune = true;
    }

    public override void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
        SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      Player player = Main.player[this.Projectile.owner];
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!((Entity) player).active || player.dead || player.ghost || this.Projectile.owner == Main.myPlayer && (!fargoSoulsPlayer.CirnoGraze || fargoSoulsPlayer.CirnoGrazeCounter < 9999))
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) this.Projectile.ai[0] != 1.0)
          this.Projectile.timeLeft = 2;
        ((Entity) this.Projectile).velocity = Vector2.Lerp(((Entity) this.Projectile).velocity, Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Top, new Vector2(-32f * (float) ((Entity) player).direction, -8f)), ((Entity) this.Projectile).Center), 9f), 0.45f);
        this.Projectile.rotation += 0.209439516f * (float) ((Entity) player).direction;
        this.Projectile.Opacity = (float) Main.mouseTextColor / (float) byte.MaxValue;
        this.Projectile.Opacity *= this.Projectile.Opacity;
        this.Projectile.scale = (float) (fargoSoulsPlayer.CirnoGrazeCounter - 9999) / 540f * 0.7f;
        if ((double) this.Projectile.scale <= 0.01)
          this.Projectile.Kill();
        this.color = Color.op_Multiply(Color.Cyan, this.Projectile.Opacity);
      }
    }

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
      SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if ((double) this.Projectile.ai[0] != 1.0)
        return;
      Player player = Main.player[this.Projectile.owner];
      int freezeRange = 2400;
      int num = 180;
      foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && !n.friendly && n.damage > 0 && (double) ((Entity) player).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) n, ((Entity) player).Center)) < (double) freezeRange && !n.dontTakeDamage && !n.buffImmune[ModContent.BuffType<TimeFrozenBuff>()])))
        npc.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), num, false);
      foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.hostile && p.damage > 0 && (double) ((Entity) player).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) p, ((Entity) player).Center)) < (double) freezeRange && !p.FargoSouls().TimeFreezeImmune && p.FargoSouls().TimeFrozen == 0)))
      {
        projectile.FargoSouls().TimeFrozen = num;
        if (FargoSoulsUtil.CanDeleteProjectile(projectile))
          projectile.FargoSouls().CirnoBurst = (float) num;
      }
      for (int index3 = 0; index3 < 40; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) player).Center, 0, 0, 76, 0.0f, 0.0f, 100, Color.White, 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
      }
      for (int index5 = 0; index5 < 20; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) player).Center, 0, 0, 135, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index6].noGravity = true;
        Dust dust = Main.dust[index6];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
      }
    }
  }
}
