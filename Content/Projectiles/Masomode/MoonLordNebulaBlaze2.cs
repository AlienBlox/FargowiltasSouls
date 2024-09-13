// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.MoonLordNebulaBlaze2
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.VanillaEternity;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class MoonLordNebulaBlaze2 : MoonLordNebulaBlaze
  {
    public int counter;

    public override string Texture => "Terraria/Images/Projectile_634";

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Projectile.timeLeft = 7200;
    }

    public virtual bool? CanDamage()
    {
      bool? nullable = base.CanDamage();
      bool flag = false;
      return nullable.GetValueOrDefault() == flag & nullable.HasValue ? new bool?(false) : new bool?(this.counter > 30 * this.Projectile.MaxUpdates);
    }

    public override void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[0], 398);
      if (npc == null || (double) npc.ai[0] == 2.0)
      {
        this.Projectile.Kill();
      }
      else
      {
        ++this.counter;
        if ((double) this.Projectile.ai[1] == 0.0)
        {
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<LunarRitual>() && (double) Main.projectile[index].ai[1] == (double) ((Entity) npc).whoAmI)
            {
              this.Projectile.localAI[1] = (float) index;
              break;
            }
          }
        }
        Projectile projectile1 = FargoSoulsUtil.ProjectileExists(this.Projectile.localAI[1], ModContent.ProjectileType<LunarRitual>());
        if (projectile1 != null && (double) projectile1.ai[1] == (double) ((Entity) npc).whoAmI)
        {
          if ((double) ((Entity) this.Projectile).Distance(((Entity) projectile1).Center) > 1600.0)
          {
            if (npc.GetGlobalNPC<MoonLordCore>().VulnerabilityState != 2)
            {
              this.Projectile.Kill();
              return;
            }
            ((Entity) this.Projectile).velocity = Vector2.op_UnaryNegation(((Entity) this.Projectile).velocity);
            ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, (double) MathHelper.WrapAngle(2f * (Utils.ToRotation(Vector2.op_Subtraction(((Entity) projectile1).Center, ((Entity) this.Projectile).Center)) - Utils.ToRotation(((Entity) this.Projectile).velocity)) * Utils.NextFloat(Main.rand, 0.8f, 1.2f)), new Vector2());
            Projectile projectile2 = this.Projectile;
            ((Entity) projectile2).position = Vector2.op_Subtraction(((Entity) projectile2).position, ((Entity) projectile1).velocity);
            this.Projectile.netUpdate = true;
          }
          base.AI();
        }
        else
          this.Projectile.Kill();
      }
    }
  }
}
