// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.ChallengerItems.SpiritArrowFlame
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.ChallengerItems
{
  public class SpiritArrowFlame : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_659";

    public virtual void SetStaticDefaults()
    {
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.TrailCacheLength[this.Projectile.type] = 6;
      ProjectileID.Sets.TrailingMode[this.Projectile.type] = 2;
      Main.projFrames[this.Projectile.type] = Main.projFrames[659];
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 14;
      ((Entity) this.Projectile).height = 26;
      this.Projectile.aiStyle = 0;
      this.Projectile.hostile = false;
      this.Projectile.friendly = true;
      this.AIType = 14;
      this.Projectile.penetrate = 1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
    }

    public virtual void AI()
    {
      if (++this.Projectile.frameCounter >= 4)
      {
        this.Projectile.frameCounter = 0;
        if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
          this.Projectile.frame = 0;
      }
      if ((double) this.Projectile.ai[0] == 0.0)
        this.Projectile.ai[1] = -1f;
      if ((double) this.Projectile.ai[0] > 10.0)
      {
        if ((double) this.Projectile.ai[0] % 30.0 == 11.0)
          this.Projectile.ai[1] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 300f, true);
        if ((double) this.Projectile.ai[1] != -1.0)
        {
          if (((Entity) Main.npc[(int) this.Projectile.ai[1]]).active)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.npc[(int) this.Projectile.ai[1]]).Center, ((Entity) this.Projectile).Center);
            double num1 = (double) ((Vector2) ref vector2).Length();
            float num2 = 12f;
            float num3 = 8f;
            if (num1 > 2.0)
            {
              ((Vector2) ref vector2).Normalize();
              vector2 = Vector2.op_Multiply(vector2, num2);
              ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num3 - 1f), vector2), num3);
            }
            else if (Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
            {
              ((Entity) this.Projectile).velocity.X = -0.15f;
              ((Entity) this.Projectile).velocity.Y = -0.05f;
            }
          }
        }
        else
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 0.9f);
          if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 1.0)
            ((Entity) this.Projectile).velocity = Vector2.Zero;
        }
      }
      if ((double) this.Projectile.ai[0] >= 90.0)
        this.Projectile.alpha += 17;
      if (this.Projectile.alpha > 250)
        this.Projectile.Kill();
      ++this.Projectile.ai[0];
    }
  }
}
