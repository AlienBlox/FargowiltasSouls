// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.RazorbladeTyphoon2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class RazorbladeTyphoon2 : RazorbladeTyphoon
  {
    public override string Texture => "Terraria/Images/Projectile_409";

    public override void AI()
    {
      if ((double) ++this.Projectile.localAI[1] > 30.0 && (double) this.Projectile.localAI[1] < 120.0)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1f + this.Projectile.ai[0]);
      }
      int num = Main.rand.Next(3);
      for (int index1 = 0; index1 < num; ++index1)
      {
        Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) ((double) Utils.NextFloat(Main.rand) * 3.1415927410125732 - 1.5707963705062866)), (float) Main.rand.Next(3, 8));
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 172, vector2.X * 2f, vector2.Y * 2f, 100, new Color(), 1.4f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
        Dust dust2 = Main.dust[index2];
        dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.Projectile).velocity);
      }
      this.Projectile.rotation += (float) (0.20000000298023224 * ((double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1.0 : -1.0));
      ++this.Projectile.frame;
      if (this.Projectile.frame <= 2)
        return;
      this.Projectile.frame = 0;
    }
  }
}
