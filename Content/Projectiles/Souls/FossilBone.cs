// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.FossilBone
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class FossilBone : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_21";

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      this.Projectile.CloneDefaults(21);
      this.Projectile.aiStyle = -1;
      this.Projectile.timeLeft = 900;
      this.Projectile.tileCollide = true;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      this.Projectile.rotation += 0.2f;
      bool flag = true;
      if ((double) ++this.Projectile.localAI[2] > 90.0)
      {
        int whoAmI = ((Entity) player).whoAmI;
        switch (whoAmI)
        {
          case -1:
          case (int) byte.MaxValue:
            break;
          default:
            if (((Entity) Main.player[whoAmI]).active && !Main.player[whoAmI].dead && !Main.player[whoAmI].ghost && (double) ((Entity) Main.player[whoAmI]).Distance(((Entity) this.Projectile).Center) < 80.0)
            {
              flag = false;
              ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) Main.player[whoAmI]).Center), 9f);
              ++this.Projectile.timeLeft;
              if (this.Projectile.Colliding(((Entity) this.Projectile).Hitbox, ((Entity) Main.player[whoAmI]).Hitbox))
              {
                player.FargoSouls().HealPlayer(20);
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

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.SandyBrown);

    public virtual void OnKill(int timeLeft)
    {
      for (int index1 = 0; index1 < 16; ++index1)
      {
        Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 7) * 6.2831854820251465 / 16.0, new Vector2()), ((Entity) this.Projectile).Center);
        Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.Projectile).Center);
        int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 0, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].velocity = vector2_2;
      }
    }
  }
}
