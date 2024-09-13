// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Souls.ArrowRain
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Souls
{
  public class ArrowRain : ModProjectile
  {
    private bool launchArrow = true;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 1;
      ((Entity) this.Projectile).height = 1;
      this.Projectile.friendly = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 330;
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual void AI()
    {
      NPC npc = Main.npc[(int) this.Projectile.ai[1]];
      if (((Entity) npc).active)
        ((Entity) this.Projectile).Center = new Vector2(((Entity) npc).Center.X, ((Entity) npc).Center.Y - 1000f);
      if (this.Projectile.timeLeft > 300)
        return;
      if (this.launchArrow)
      {
        Vector2 vector2_1;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_1).\u002Ector(((Entity) this.Projectile).Center.X + (float) Main.rand.Next(-100, 100), ((Entity) this.Projectile).Center.Y + (float) Main.rand.Next(-75, 75));
        Vector2 vector2_2;
        if (((Entity) npc).active)
        {
          vector2_2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) this.Projectile).position)), 25f);
        }
        else
        {
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_2).\u002Ector(0.0f, 5f);
        }
        int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), vector2_1, vector2_2, (int) this.Projectile.ai[0], this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
        Main.projectile[index].noDropItem = true;
        this.launchArrow = false;
      }
      else
        this.launchArrow = true;
    }
  }
}
