// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FlightBall
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FlightBall : LightBall
  {
    public virtual string Texture => "FargowiltasSouls/Content/Projectiles/Masomode/LightBall";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.timeLeft = 600;
      this.Projectile.extraUpdates = 0;
    }

    public override void AI()
    {
      if ((double) ++this.Projectile.localAI[0] == 0.0)
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 2; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 246, ((Entity) this.Projectile).velocity.X * 0.2f, ((Entity) this.Projectile).velocity.Y * 0.2f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity.X *= 0.6f;
        Main.dust[index2].velocity.Y *= 0.6f;
        if ((double) ((Entity) this.Projectile).velocity.X != 0.0)
          this.Projectile.spriteDirection = ((Entity) this.Projectile).direction = (double) ((Entity) this.Projectile).velocity.X > 0.0 ? 1 : -1;
        this.Projectile.rotation += 0.3f * (float) ((Entity) this.Projectile).direction;
      }
      bool flag = true;
      if ((double) this.Projectile.localAI[0] > 90.0)
      {
        int closest = (int) Player.FindClosest(((Entity) this.Projectile).Center, 0, 0);
        switch (closest)
        {
          case -1:
          case (int) byte.MaxValue:
            break;
          default:
            if (((Entity) Main.player[closest]).active && !Main.player[closest].dead && !Main.player[closest].ghost && (double) ((Entity) Main.player[closest]).Distance(((Entity) this.Projectile).Center) < 80.0)
            {
              flag = false;
              ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[closest]).Center), 9f);
              ++this.Projectile.timeLeft;
              if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.player[closest]).Hitbox))
              {
                Main.player[closest].wingTime = (float) Main.player[closest].wingTimeMax;
                this.Projectile.Kill();
                return;
              }
              break;
            }
            break;
        }
      }
      if (!flag)
        return;
      Projectile projectile = this.Projectile;
      ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.95f);
    }
  }
}
