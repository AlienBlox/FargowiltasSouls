// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomFlocko3
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
  public class AbomFlocko3 : AbomFlocko
  {
    public override string Texture => "Terraria/Images/NPC_352";

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>());
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        Vector2 center = ((Entity) npc).Center;
        center.X += this.Projectile.ai[1];
        center.Y -= 1100f;
        Vector2 vector2_1 = Vector2.op_Subtraction(center, ((Entity) this.Projectile).Center);
        if ((double) ((Vector2) ref vector2_1).Length() > 10.0)
        {
          vector2_1 = Vector2.op_Division(vector2_1, 8f);
          ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 23f), vector2_1), 24f);
        }
        else if ((double) ((Vector2) ref ((Entity) this.Projectile).velocity).Length() < 12.0)
        {
          Projectile projectile = this.Projectile;
          ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, 1.05f);
        }
        if ((double) ++this.Projectile.localAI[0] > 180.0 && (double) ++this.Projectile.localAI[1] > ((double) npc.localAI[3] > 1.0 ? 4.0 : 2.0))
        {
          SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
          this.Projectile.localAI[1] = 0.0f;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_2).\u002Ector((float) Main.rand.Next(-1000, 1001), (float) Main.rand.Next(-1000, 1001));
            ((Vector2) ref vector2_2).Normalize();
            vector2_2 = Vector2.op_Multiply(vector2_2, 6f);
            vector2_2.X /= 2f;
            Projectile.NewProjectile(Entity.InheritSource((Entity) this.Projectile), Vector2.op_Addition(((Entity) this.Projectile).Center, Vector2.op_Multiply(vector2_2, 4f)), vector2_2, ModContent.ProjectileType<AbomFrostShard>(), this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
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
