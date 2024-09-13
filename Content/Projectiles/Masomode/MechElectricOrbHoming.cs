// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MechElectricOrbHoming
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MechElectricOrbHoming : MechElectricOrb
  {
    public virtual string Texture
    {
      get => "FargowiltasSouls/Content/Projectiles/Masomode/MechElectricOrb";
    }

    public override void AI()
    {
      base.AI();
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        int num = (double) this.Projectile.ai[0] <= -1.0 ? 0 : ((double) this.Projectile.ai[0] < (double) byte.MaxValue ? 1 : 0);
        bool flag = false;
        if (num != 0)
        {
          float rotation1 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center);
          float rotation2 = Utils.ToRotation(vector2);
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation1, rotation2, 0.2f), new Vector2());
          if ((double) ((Vector2) ref vector2).Length() < 300.0 || (double) ((Vector2) ref vector2).Length() > 3000.0 || !((Entity) Main.player[(int) this.Projectile.ai[0]]).active || Main.player[(int) this.Projectile.ai[0]].dead || Main.player[(int) this.Projectile.ai[0]].ghost)
            flag = true;
        }
        if (!(num == 0 | flag))
          return;
        this.Projectile.ai[1] = 1f;
        this.Projectile.netUpdate = true;
        this.Projectile.timeLeft = 180;
        ((Vector2) ref ((Entity) this.Projectile).velocity).Normalize();
      }
      else if ((double) this.Projectile.ai[1] > 0.0)
      {
        if ((double) this.Projectile.ai[0] > -1.0 && (double) this.Projectile.ai[0] < (double) byte.MaxValue)
        {
          if ((double) ++this.Projectile.ai[1] >= 100.0)
            return;
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.035f);
          float rotation3 = Utils.ToRotation(((Entity) this.Projectile).velocity);
          float rotation4 = Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.player[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center));
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(new Vector2(((Vector2) ref ((Entity) this.Projectile).velocity).Length(), 0.0f), (double) Utils.AngleLerp(rotation3, rotation4, 0.035f), new Vector2());
        }
        else
        {
          if ((double) ++this.Projectile.ai[1] >= 75.0)
            return;
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.06f);
        }
      }
      else
        ++this.Projectile.ai[1];
    }
  }
}
