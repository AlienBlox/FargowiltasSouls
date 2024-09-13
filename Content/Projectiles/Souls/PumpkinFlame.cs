// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.PumpkinFlame
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class PumpkinFlame : ModProjectile
  {
    public virtual string Texture => FargoSoulsUtil.EmptyTexture;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(400);
      this.AIType = 400;
      ((Entity) this.Projectile).width = 14;
      ((Entity) this.Projectile).height = 16;
      this.Projectile.DamageType = DamageClass.Generic;
    }

    public virtual void AI()
    {
      if (((Entity) this.Projectile).wet)
        this.Projectile.Kill();
      int index1 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1f);
      Main.dust[index1].position.X -= 2f;
      Main.dust[index1].position.Y += 2f;
      Main.dust[index1].scale += (float) Main.rand.Next(50) * 0.01f;
      Main.dust[index1].noGravity = true;
      Main.dust[index1].velocity.Y -= 2f;
      if (Utils.NextBool(Main.rand))
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1f);
        Main.dust[index2].position.X -= 2f;
        Main.dust[index2].position.Y += 2f;
        Main.dust[index2].scale += (float) (0.30000001192092896 + (double) Main.rand.Next(50) * 0.0099999997764825821);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
      }
      if ((double) ((Entity) this.Projectile).velocity.Y < 0.25 && (double) ((Entity) this.Projectile).velocity.Y > 0.15)
        ((Entity) this.Projectile).velocity.X = ((Entity) this.Projectile).velocity.X * 0.8f;
      this.Projectile.rotation = (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.05000000074505806);
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }
  }
}
