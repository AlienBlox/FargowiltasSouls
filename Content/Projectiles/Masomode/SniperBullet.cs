// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.SniperBullet
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class SniperBullet : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(242);
      this.AIType = 242;
      this.Projectile.DamageType = DamageClass.Default;
      this.Projectile.friendly = false;
      this.Projectile.hostile = true;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 1800, true, false);
    }

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 68, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index2].scale *= 0.9f;
      }
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 24; ++index)
      {
        float num1 = (float) (-(double) ((Entity) this.Projectile).velocity.X * (double) Main.rand.Next(30, 60) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        float num2 = (float) (-(double) ((Entity) this.Projectile).velocity.Y * (double) Main.rand.Next(30, 60) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).position.X + num1, ((Entity) this.Projectile).position.Y + num2, num1, num2, ModContent.ProjectileType<SniperBulletShard>(), this.Projectile.damage / 2, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
    }
  }
}
