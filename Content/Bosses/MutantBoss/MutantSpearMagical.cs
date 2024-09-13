// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantSpearMagical
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  public class MutantSpearMagical : MutantSpearThrown
  {
    private const int attackTime = 120;
    private const int flySpeed = 25;

    public override string Texture
    {
      get
      {
        return !FargoSoulsUtil.AprilFools ? "FargowiltasSouls/Content/Projectiles/BossWeapons/HentaiSpear" : "FargowiltasSouls/Content/Bosses/MutantBoss/MutantSpear_April";
      }
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 144;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public override void AI()
    {
      if ((double) this.Projectile.ai[0] == 0.0)
      {
        if ((double) this.Projectile.localAI[1] == 0.0)
        {
          this.Projectile.rotation = 6.28318548f + Utils.NextFloat(Main.rand, 6.28318548f);
          if (Utils.NextBool(Main.rand))
            this.Projectile.rotation *= -1f;
        }
        this.Projectile.rotation = MathHelper.Lerp(this.Projectile.rotation, this.Projectile.ai[1], 0.05f);
        if ((double) ++this.Projectile.localAI[1] > 120.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item1, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
          this.Projectile.ai[0] = 1f;
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(25f, Utils.ToRotationVector2(this.Projectile.ai[1]));
        }
      }
      else
      {
        this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + MathHelper.ToRadians(135f);
        if ((double) --this.Projectile.localAI[0] < 0.0)
        {
          this.Projectile.localAI[0] = 4f;
          if ((double) this.Projectile.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MutantSphereSmall>(), this.Projectile.damage, 0.0f, this.Projectile.owner, this.Projectile.ai[0], 0.0f, 0.0f);
        }
      }
      ++this.scaletimer;
    }

    public virtual void OnKill(int timeLeft)
    {
      base.OnKill(timeLeft);
      if (!FargoSoulsUtil.HostCheck)
        return;
      Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordMoonBlast>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, Utils.ToRotation(((Entity) this.Projectile).velocity), 12f, 0.0f);
    }

    public override Color? GetAlpha(Color lightColor)
    {
      Color color = Color.op_Multiply(Color.White, this.Projectile.Opacity);
      ((Color) ref color).A = (byte) ((double) byte.MaxValue * (double) Math.Min(this.Projectile.localAI[1] / 120f, 1f));
      return new Color?(color);
    }

    public override bool PreDraw(ref Color lightColor)
    {
      return (double) this.Projectile.ai[0] == 0.0 || base.PreDraw(ref lightColor);
    }
  }
}
