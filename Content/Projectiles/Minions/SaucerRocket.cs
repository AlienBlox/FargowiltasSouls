// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.SaucerRocket
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class SaucerRocket : ModProjectile
  {
    public virtual string Texture => "Terraria/Images/Projectile_448";

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 3;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionShot[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 14;
      ((Entity) this.Projectile).height = 14;
      this.Projectile.aiStyle = -1;
      this.Projectile.friendly = true;
      this.Projectile.alpha = 0;
      this.Projectile.timeLeft = 600;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.tileCollide = false;
    }

    public virtual void AI()
    {
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        ++this.Projectile.timeLeft;
        if ((double) --this.Projectile.ai[1] == 0.0)
        {
          ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.Projectile).velocity), ((Vector2) ref ((Entity) this.Projectile).velocity).Length() + 6f);
          this.Projectile.netUpdate = true;
          for (int index1 = 0; index1 < 8; ++index1)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, -8f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index1 * 3.14159274101257 / 4.0, new Vector2())), new Vector2(2f, 8f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
            int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 228, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].scale = 1.5f;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
            Main.dust[index2].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.0f);
          }
        }
      }
      else
      {
        if ((double) this.Projectile.ai[0] >= 0.0 && (double) this.Projectile.ai[0] < (double) Main.maxNPCs && Main.npc[(int) this.Projectile.ai[0]].CanBeChasedBy((object) null, false))
        {
          double num = (double) Utils.ToRotation(Vector2.op_Subtraction(((Entity) Main.npc[(int) this.Projectile.ai[0]]).Center, ((Entity) this.Projectile).Center)) - (double) Utils.ToRotation(((Entity) this.Projectile).velocity);
          if (num > Math.PI)
            num -= 2.0 * Math.PI;
          if (num < -1.0 * Math.PI)
            num += 2.0 * Math.PI;
          ((Entity) this.Projectile).velocity = Utils.RotatedBy(((Entity) this.Projectile).velocity, num * 0.20000000298023224, new Vector2());
        }
        else
        {
          this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPCPrioritizingMinionFocus(this.Projectile, 1000f, true, new Vector2());
          this.Projectile.netUpdate = true;
          if ((double) this.Projectile.ai[0] == -1.0)
            this.Projectile.Kill();
        }
        this.Projectile.tileCollide = true;
        if ((double) ++this.Projectile.localAI[0] > 5.0)
        {
          this.Projectile.localAI[0] = 0.0f;
          for (int index3 = 0; index3 < 4; ++index3)
          {
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, -8f), Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index3 * 3.14159274101257 / 4.0, new Vector2())), new Vector2(2f, 4f))), (double) this.Projectile.rotation - 1.5707963705062866, new Vector2());
            int index4 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 228, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index4].scale = 1.5f;
            Main.dust[index4].noGravity = true;
            Main.dust[index4].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2);
            Main.dust[index4].velocity = Vector2.op_Multiply(((Entity) this.Projectile).velocity, 0.0f);
          }
        }
      }
      this.Projectile.rotation = Utils.ToRotation(((Entity) this.Projectile).velocity) + 1.57079637f;
      Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) this.Projectile.rotation, new Vector2()), 8f), 2f);
      int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 228, 0.0f, 0.0f, 0, new Color(), 1f);
      Main.dust[index].position = Vector2.op_Addition(((Entity) this.Projectile).Center, vector2_1);
      Main.dust[index].scale = 1f;
      Main.dust[index].noGravity = true;
      if (++this.Projectile.frameCounter < 3)
        return;
      this.Projectile.frameCounter = 0;
      if (++this.Projectile.frame < 3)
        return;
      this.Projectile.frame = 0;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (this.Projectile.penetrate >= 0)
        return;
      target.immune[this.Projectile.owner] = 0;
    }

    public virtual void OnKill(int timeLeft)
    {
      if (this.Projectile.penetrate <= -1)
        return;
      this.Projectile.penetrate = -1;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 112;
      ((Entity) this.Projectile).position.X -= (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y -= (float) (((Entity) this.Projectile).height / 2);
      for (int index = 0; index < 4; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      for (int index1 = 0; index1 < 40; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 228, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index3 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 228, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
        Main.dust[index3].noGravity = true;
      }
      for (int index4 = 0; index4 < 1; ++index4)
      {
        int index5 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Projectile).position, new Vector2((float) (((Entity) this.Projectile).width * Main.rand.Next(100)) / 100f, (float) (((Entity) this.Projectile).height * Main.rand.Next(100)) / 100f)), Vector2.op_Multiply(Vector2.One, 10f)), new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index5];
        gore.velocity = Vector2.op_Multiply(gore.velocity, 0.3f);
        Main.gore[index5].velocity.X += (float) Main.rand.Next(-10, 11) * 0.05f;
        Main.gore[index5].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.05f;
      }
      this.Projectile.Damage();
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(Color.White, this.Projectile.Opacity));
    }
  }
}
