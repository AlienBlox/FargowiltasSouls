// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviEnergyHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Core.Globals;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  public class DeviEnergyHeart : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 30;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.penetrate = -1;
      this.Projectile.hostile = true;
      this.Projectile.tileCollide = false;
      this.Projectile.ignoreWater = true;
      this.Projectile.aiStyle = -1;
      this.CooldownSlot = 1;
      this.Projectile.alpha = 150;
      this.Projectile.timeLeft = 90;
      this.Projectile.FargoSouls().DeletionImmuneRank = 1;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item44, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      if (this.Projectile.alpha >= 60)
        this.Projectile.alpha -= 10;
      this.Projectile.rotation = this.Projectile.ai[0];
      this.Projectile.scale += 0.01f;
      float num = ((Vector2) ref ((Entity) this.Projectile).velocity).Length() + this.Projectile.ai[1];
      ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), num);
    }

    public virtual void OnKill(int timeLeft)
    {
      FargoSoulsUtil.HeartDust(((Entity) this.Projectile).Center, this.Projectile.rotation + 1.57079637f, new Vector2());
      int num = 0;
      while (num < 5)
        ++num;
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>()) || !FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 4; ++index)
        Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.Projectile.rotation + 1.5707963705062866 * (double) index, new Vector2()), ModContent.ProjectileType<DeviDeathray>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
