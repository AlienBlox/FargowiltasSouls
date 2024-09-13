// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.QueenFlocko
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.AbomBoss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class QueenFlocko : AbomFlocko
  {
    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 150;
    }

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 345);
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        Player player = Main.player[npc.target];
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
        if ((double) ++this.Projectile.localAI[1] > 120.0)
        {
          this.Projectile.localAI[1] = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item120, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) player).Center), 7f);
            for (int index1 = -1; index1 <= 1; ++index1)
            {
              Vector2 vector2_3 = Utils.RotatedBy(vector2_2, (double) MathHelper.ToRadians(4f) * (double) index1, new Vector2());
              vector2_3.X = (float) (((double) ((Entity) player).Center.X - (double) ((Entity) this.Projectile).Center.X) / 100.0);
              int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, vector2_3, 348, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
              if (index2 != Main.maxProjectiles)
                Main.projectile[index2].timeLeft = 101;
            }
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
