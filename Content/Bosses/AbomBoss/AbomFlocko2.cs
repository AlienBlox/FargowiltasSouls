// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomFlocko2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  public class AbomFlocko2 : AbomFlocko
  {
    public override bool? CanDamage() => new bool?(false);

    public override void AI()
    {
      if ((double) this.Projectile.ai[0] < 0.0 || (double) this.Projectile.ai[0] >= (double) byte.MaxValue)
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[(int) this.Projectile.ai[0]];
        Vector2 center = ((Entity) player).Center;
        center.X += 700f * this.Projectile.ai[1];
        Vector2 vector2_1 = Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_1).Length() > 100.0)
        {
          vector2_1 = Vector2.op_Division(vector2_1, 8f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2_1), 24f);
        }
        else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
        }
        if ((double) ++this.Projectile.localAI[0] > 90.0 && (double) ++this.Projectile.localAI[1] > 60.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item120, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 7f);
            for (int index = -1; index <= 1; ++index)
              Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), ((Entity) this.Projectile).Center, Utils.RotatedBy(vector2_2, (double) MathHelper.ToRadians(10f) * (double) index, new Vector2()), ModContent.ProjectileType<AbomFrostWave>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          }
        }
        this.Projectile.rotation += (float) ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() / 12.0 * ((double) ((Entity) this.Projectile).velocity.X > 0.0 ? -0.20000000298023224 : 0.20000000298023224));
        if (++this.Projectile.frameCounter <= 3)
          return;
        if (++this.Projectile.frame >= 6)
          this.Projectile.frame = 0;
        this.Projectile.frameCounter = 0;
      }
    }
  }
}
