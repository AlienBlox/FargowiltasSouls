// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.BeeFlower
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class BeeFlower : ModProjectile
  {
    public virtual void SetStaticDefaults() => Main.projFrames[this.Projectile.type] = 5;

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 34;
      ((Entity) this.Projectile).height = 34;
      this.Projectile.scale = 1f;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Generic;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 900;
      this.Projectile.penetrate = 1;
      this.Projectile.light = 1f;
    }

    public virtual bool? CanDamage()
    {
      return new bool?(this.Projectile.frame == Main.projFrames[this.Projectile.type] - 1);
    }

    public virtual void AI()
    {
      if (this.Projectile.frame < Main.projFrames[this.Projectile.type] - 1)
      {
        if (++this.Projectile.frameCounter % 60 != 0)
          return;
        ++this.Projectile.frame;
      }
      else
      {
        if (!((Entity) Main.LocalPlayer).active || Main.LocalPlayer.dead || Main.LocalPlayer.ghost)
          return;
        Rectangle hitbox = ((Entity) Main.LocalPlayer).Hitbox;
        if (!((Rectangle) ref hitbox).Intersects(((Entity) this.Projectile).Hitbox))
          return;
        Main.LocalPlayer.AddBuff(48, 900, true, false);
        this.BeeSwarm();
        this.Projectile.Kill();
      }
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => this.BeeSwarm();

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.LiquidsHoneyWater, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
    }

    public void BeeSwarm()
    {
      for (int index1 = 0; index1 < 7; ++index1)
      {
        Vector2 vector2 = Utils.NextVector2FromRectangle(Main.rand, ((Entity) this.Projectile).Hitbox);
        int index2 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.Projectile).Center), 12f), Main.LocalPlayer.beeType(), Main.LocalPlayer.beeDamage((int) ((double) this.Projectile.damage * 0.75)), Main.LocalPlayer.beeKB(this.Projectile.knockBack), ((Entity) Main.LocalPlayer).whoAmI, 0.0f, 0.0f, 0.0f);
        if (index2 != Main.maxProjectiles)
          Main.projectile[index2].DamageType = this.Projectile.DamageType;
      }
    }
  }
}
