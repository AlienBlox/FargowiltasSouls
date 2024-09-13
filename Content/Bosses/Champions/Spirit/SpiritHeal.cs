// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Spirit.SpiritHeal
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Spirit
{
  public class SpiritHeal : SpiritSpirit
  {
    public virtual bool? CanDamage() => new bool?(false);

    public override void AI()
    {
      if ((double) --this.Projectile.ai[1] < 0.0 && (double) this.Projectile.ai[1] > -300.0)
      {
        NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], ModContent.NPCType<SpiritChampion>());
        if (npc.TypeAlive<SpiritChampion>())
        {
          if ((double) ((Entity) this.Projectile).Distance(((Entity) npc).Center) > 50.0)
          {
            for (int index = 0; index < 3; ++index)
            {
              Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, ((Entity) npc).Center), 3f);
              ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, 29f), vector2), 30f);
            }
          }
          else if (FargoSoulsUtil.HostCheck)
          {
            npc.life += this.Projectile.damage;
            npc.HealEffect(this.Projectile.damage, true);
            if (npc.life > npc.lifeMax)
              npc.life = npc.lifeMax;
            this.Projectile.Kill();
          }
        }
        else
          this.Projectile.Kill();
      }
      for (int index = 0; index < 3; ++index)
      {
        Projectile projectile = this.Projectile;
        ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      }
    }
  }
}
