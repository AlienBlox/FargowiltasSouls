// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureBullet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  public class NatureBullet : ModProjectile
  {
    public int stopped;

    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/SniperBullet";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(286);
      this.Projectile.aiStyle = -1;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      if (this.Projectile.alpha > 0)
      {
        this.Projectile.alpha -= 7;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
      }
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        this.Projectile.localAI[1] = ((Vector2) ref ((Entity) this.Projectile).velocity).Length();
        SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.hide = false;
      if ((double) --this.Projectile.ai[0] < 0.0 && (double) this.Projectile.ai[0] > (double) (-40 * this.Projectile.MaxUpdates))
      {
        ((Entity) this.Projectile).velocity = Vector2.Zero;
        this.Projectile.hide = true;
        if (Utils.NextBool(Main.rand))
        {
          int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
        }
      }
      else if ((double) this.Projectile.ai[0] == (double) (-40 * this.Projectile.MaxUpdates))
      {
        SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        if (closest != -1)
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center), this.Projectile.localAI[1]);
        else
          this.Projectile.Kill();
      }
      else if ((double) this.Projectile.ai[0] < (double) (-40 * this.Projectile.MaxUpdates))
      {
        this.Projectile.tileCollide = true;
        this.Projectile.ignoreWater = false;
      }
      if (!Vector2.op_Inequality(((Entity) this.Projectile).velocity, Vector2.Zero))
        return;
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(44, 180, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(46, 180, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 68, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index2].scale *= 0.9f;
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 6; ++index)
      {
        float num1 = (float) (-(double) ((Entity) this.Projectile).velocity.X * (double) Main.rand.Next(30, 60) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        float num2 = (float) (-(double) ((Entity) this.Projectile).velocity.Y * (double) Main.rand.Next(30, 60) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y, num1, num2, ModContent.ProjectileType<SniperBulletShard>(), 0, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
