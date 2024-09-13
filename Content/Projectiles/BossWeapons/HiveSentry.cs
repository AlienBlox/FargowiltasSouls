// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.BossWeapons.HiveSentry
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.BossWeapons
{
  public class HiveSentry : ModProjectile
  {
    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 40;
      ((Entity) this.Projectile).height = 42;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = true;
      this.Projectile.ignoreWater = true;
      this.Projectile.sentry = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 7200;
      this.Projectile.sentry = true;
      this.Projectile.DamageType = DamageClass.Summon;
    }

    public virtual void AI()
    {
      Player player = Main.player[this.Projectile.owner];
      ((Entity) this.Projectile).velocity.Y = ((Entity) this.Projectile).velocity.Y + 0.2f;
      if ((double) ((Entity) this.Projectile).velocity.Y > 16.0)
        ((Entity) this.Projectile).velocity.Y = 16f;
      ++this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] < 120.0)
        return;
      float num1 = 2000f;
      int index1 = -1;
      for (int index2 = 0; index2 < 200; ++index2)
      {
        float num2 = Vector2.Distance(((Entity) this.Projectile).Center, ((Entity) Main.npc[index2]).Center);
        if ((double) num2 < (double) num1 && (double) num2 < 300.0 && Main.npc[index2].CanBeChasedBy((object) this.Projectile, false))
        {
          index1 = index2;
          num1 = num2;
        }
      }
      if (index1 != -1)
      {
        NPC npc = Main.npc[index1];
        for (int index3 = 0; index3 < 10; ++index3)
        {
          int index4 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2((float) Main.rand.Next(-10, 10), (float) Main.rand.Next(-10, 10)), player.beeType(), player.beeDamage(this.Projectile.damage), player.beeKB(0.0f), this.Projectile.owner, 0.0f, 0.0f, 0.0f);
          Main.projectile[index4].DamageType = DamageClass.Summon;
        }
        for (int index5 = 0; index5 < 20; ++index5)
        {
          int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 147, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 2f);
          Main.dust[index6].noGravity = true;
          Dust dust1 = Main.dust[index6];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
          int index7 = Dust.NewDust(new Vector2(((Entity) this.Projectile).Center.X, ((Entity) this.Projectile).Center.Y), ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 59, (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.20000000298023224), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.20000000298023224), 100, new Color(), 1f);
          Dust dust2 = Main.dust[index7];
          dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        }
      }
      this.Projectile.ai[1] = 0.0f;
      float num3 = Vector2.Distance(((Entity) Main.player[this.Projectile.owner]).Center, ((Entity) this.Projectile).Center);
      if ((double) num3 > 2000.0)
      {
        this.Projectile.Kill();
      }
      else
      {
        if ((double) num3 >= 20.0)
          return;
        Main.player[this.Projectile.owner].AddBuff(48, 300, true, false);
      }
    }

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      fallThrough = false;
      return true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      Projectile projectile = this.Projectile;
      ((Entity) projectile).position = Vector2.op_Addition(((Entity) projectile).position, ((Entity) this.Projectile).velocity);
      ((Entity) this.Projectile).velocity = Vector2.Zero;
      return false;
    }
  }
}
