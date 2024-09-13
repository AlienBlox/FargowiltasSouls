// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.JungleMimic.AcornProjectileExplosion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.JungleMimic
{
  public class AcornProjectileExplosion : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 140;
      ((Entity) this.Projectile).height = 140;
      this.Projectile.friendly = true;
      this.Projectile.ignoreWater = false;
      this.Projectile.tileCollide = false;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 2;
      this.Projectile.DamageType = DamageClass.Ranged;
    }

    public virtual void OnKill(int timeLeft)
    {
      Lighting.AddLight(((Entity) this.Projectile).Center, (float) ((double) ((int) byte.MaxValue - this.Projectile.alpha) * 0.34999999403953552 / (double) byte.MaxValue), (float) ((double) ((int) byte.MaxValue - this.Projectile.alpha) * 0.34999999403953552 / (double) byte.MaxValue), (float) ((double) ((int) byte.MaxValue - this.Projectile.alpha) * 0.0 / (double) byte.MaxValue));
      for (int index1 = 0; index1 < 30; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 150, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
      }
      for (int index3 = 0; index3 < 3; ++index3)
      {
        float num = 0.33f;
        if (index3 == 1)
          num = 0.66f;
        if (index3 == 2)
          num = 1f;
        int index4 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) this.Projectile).position.X + (double) (((Entity) this.Projectile).width / 2) - 24.0), (float) ((double) ((Entity) this.Projectile).position.Y + (double) (((Entity) this.Projectile).height / 2) - 24.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index4];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
        ++Main.gore[index4].velocity.X;
        ++Main.gore[index4].velocity.Y;
        int index5 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) this.Projectile).position.X + (double) (((Entity) this.Projectile).width / 2) - 24.0), (float) ((double) ((Entity) this.Projectile).position.Y + (double) (((Entity) this.Projectile).height / 2) - 24.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore2 = Main.gore[index5];
        gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
        --Main.gore[index5].velocity.X;
        ++Main.gore[index5].velocity.Y;
        int index6 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) this.Projectile).position.X + (double) (((Entity) this.Projectile).width / 2) - 24.0), (float) ((double) ((Entity) this.Projectile).position.Y + (double) (((Entity) this.Projectile).height / 2) - 24.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore3 = Main.gore[index6];
        gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
        ++Main.gore[index6].velocity.X;
        --Main.gore[index6].velocity.Y;
        int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2((float) ((double) ((Entity) this.Projectile).position.X + (double) (((Entity) this.Projectile).width / 2) - 24.0), (float) ((double) ((Entity) this.Projectile).position.Y + (double) (((Entity) this.Projectile).height / 2) - 24.0)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore4 = Main.gore[index7];
        gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
        --Main.gore[index7].velocity.X;
        --Main.gore[index7].velocity.Y;
      }
    }
  }
}
