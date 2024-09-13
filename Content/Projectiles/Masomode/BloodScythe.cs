// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.BloodScythe
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class BloodScythe : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(44);
      this.Projectile.aiStyle = -1;
      this.Projectile.hostile = true;
      this.Projectile.friendly = false;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.timeLeft = 300;
      this.Projectile.tileCollide = false;
      this.CooldownSlot = 1;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      this.Projectile.rotation += 0.8f;
      if ((double) ++this.Projectile.localAI[1] > 30.0 && (double) this.Projectile.localAI[1] < 120.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.03f);
      }
      Vector2 vector2 = Utils.RotatedByRandom(Utils.RotatedBy(new Vector2(0.0f, -20f), (double) this.Projectile.rotation, new Vector2()), 0.52359879016876221);
      int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 229, 0.0f, 0.0f, 150, new Color(), 1f);
      Dust dust = Main.dust[index];
      dust.position = Vector2.op_Addition(dust.position, vector2);
      float num = (float) (Main.rand.Next(20, 31) / 10);
      Main.dust[index].velocity = Vector2.op_Division(((Entity) this.Projectile).velocity, num);
      Main.dust[index].noGravity = true;
      if (this.Projectile.timeLeft >= 180)
        return;
      this.Projectile.tileCollide = true;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);

    public virtual void OnHitPlayer(Player target, Player.HurtInfo info)
    {
      if (WorldSavingSystem.MasochistModeReal)
      {
        target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
        target.AddBuff(30, 600, true, false);
        target.AddBuff(163, 15, true, false);
      }
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 120, true, false);
    }
  }
}
