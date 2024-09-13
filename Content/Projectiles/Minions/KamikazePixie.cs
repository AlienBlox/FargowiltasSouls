// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Minions.KamikazePixie
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Minions
{
  public class KamikazePixie : ModProjectile
  {
    public int counter;
    private bool foundTarget;
    private float speed = 22f;

    public virtual void SetStaticDefaults()
    {
      Main.projFrames[this.Projectile.type] = 5;
      ProjectileID.Sets.MinionSacrificable[this.Projectile.type] = true;
      ProjectileID.Sets.CultistIsResistantTo[this.Projectile.type] = true;
      ProjectileID.Sets.MinionTargettingFeature[this.Projectile.type] = true;
    }

    public virtual void SetDefaults()
    {
      this.Projectile.netImportant = true;
      ((Entity) this.Projectile).width = 36;
      ((Entity) this.Projectile).height = 30;
      this.Projectile.timeLeft *= 5;
      this.Projectile.friendly = true;
      this.Projectile.minion = true;
      this.Projectile.DamageType = DamageClass.Summon;
      this.Projectile.penetrate = -1;
      this.Projectile.aiStyle = -1;
      this.Projectile.tileCollide = false;
      this.Projectile.minionSlots = 0.333333343f;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 10;
    }

    public virtual bool? CanDamage() => new bool?(this.Projectile.timeLeft <= 0);

    public virtual void AI()
    {
      if ((double) this.Projectile.localAI[0] == 0.0)
      {
        this.Projectile.localAI[0] = 1f;
        SoundEngine.PlaySound(ref SoundID.Pixie, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      }
      Player player = Main.player[this.Projectile.owner];
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        this.Projectile.ai[0] = -1f;
        ((Entity) this.Projectile).velocity = Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.Projectile).Center));
      }
      this.Projectile.rotation = 0.0f;
      this.Projectile.spriteDirection = ((Entity) this.Projectile).direction;
      if (!this.foundTarget && (double) this.Projectile.ai[0] == -1.0)
      {
        this.Projectile.ai[0] = (float) FargoSoulsUtil.FindClosestHostileNPC(((Entity) this.Projectile).Center, 2000f, true);
        this.foundTarget = (double) this.Projectile.ai[0] != -1.0;
        this.Projectile.netUpdate = true;
      }
      this.Projectile.friendly = this.foundTarget;
      if ((double) this.Projectile.ai[1] >= 540.0)
      {
        if ((double) this.Projectile.ai[1] == 540.0)
          this.speed *= 1.2f;
        if (this.foundTarget)
        {
          NPC npc = Main.npc[(int) this.Projectile.ai[0]];
          if (((Entity) npc).active && npc.CanBeChasedBy((object) null, false))
          {
            this.FlyToward(((Entity) npc).Center);
          }
          else
          {
            this.Projectile.ai[0] = -1f;
            this.foundTarget = false;
            this.Projectile.netUpdate = true;
          }
          if ((double) this.Projectile.ai[1] > 900.0 || (double) Vector2.Distance(((Entity) npc).Center, ((Entity) this.Projectile).Center) <= 25.0)
            this.Projectile.Kill();
        }
        else
          this.FlyToward(((Entity) player).position);
      }
      else if (this.foundTarget)
      {
        NPC npc = Main.npc[(int) this.Projectile.ai[0]];
        if (((Entity) npc).active && npc.CanBeChasedBy((object) null, false))
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(((Entity) npc).velocity, 10f));
          this.FlyToward(Vector2.op_Subtraction(((Entity) npc).Center, new Vector2((float) Math.Sin((double) MathHelper.ToRadians(this.Projectile.ai[1] * 6f)), 250f)));
          if ((double) this.Projectile.ai[1] % 30.0 == 0.0 && (double) Vector2.Distance(((Entity) npc).Center, ((Entity) this.Projectile).Center) <= 450.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
            Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) this.Projectile).Center), 10f), ModContent.ProjectileType<LightslingerBombshot>(), (int) ((double) this.Projectile.damage * 0.60000002384185791), this.Projectile.knockBack, Main.myPlayer, 0.0f, (float) ((Entity) this.Projectile).whoAmI, 0.0f);
          }
        }
        else
        {
          this.Projectile.ai[0] = -1f;
          this.foundTarget = false;
          this.Projectile.netUpdate = true;
        }
      }
      else
        this.FlyToward(((Entity) player).position);
      this.Projectile.timeLeft = 2;
      if (((Entity) player).whoAmI == Main.myPlayer && (!((Entity) player).active || player.dead || player.ghost))
      {
        this.Projectile.Kill();
      }
      else
      {
        if (++this.Projectile.frameCounter >= 4)
        {
          this.Projectile.frameCounter = 0;
          if (++this.Projectile.frame >= Main.projFrames[this.Projectile.type])
            this.Projectile.frame = 0;
        }
        ++this.Projectile.ai[1];
      }
    }

    public void FlyToward(Vector2 v)
    {
      float num1 = 15f;
      float num2 = 25f;
      Vector2 vector2_1 = Vector2.op_Subtraction(v, ((Entity) this.Projectile).Center);
      if ((double) ((Vector2) ref vector2_1).Length() > (double) num2)
      {
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, this.speed);
        ((Entity) this.Projectile).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.Projectile).velocity, num1 - 1f), vector2_2), num1);
      }
      else
      {
        if (!Vector2.op_Equality(((Entity) this.Projectile).velocity, Vector2.Zero))
          return;
        ((Entity) this.Projectile).velocity.X = -0.15f;
        ((Entity) this.Projectile).velocity.Y = -0.05f;
      }
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity) => false;

    public virtual bool TileCollideStyle(
      ref int width,
      ref int height,
      ref bool fallThrough,
      ref Vector2 hitboxCenterFrac)
    {
      return base.TileCollideStyle(ref width, ref height, ref fallThrough, ref hitboxCenterFrac);
    }

    public virtual bool PreKill(int timeLeft) => base.PreKill(timeLeft);

    public virtual void OnKill(int timeLeft)
    {
      SoundEngine.PlaySound(ref SoundID.Pixie, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      if (timeLeft != 1)
        return;
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 5, 0.0f, -1f, 0, new Color(), 1f);
        Main.dust[index2].scale += 0.5f;
      }
      this.Projectile.damage *= 6;
      ((Entity) this.Projectile).position = ((Entity) this.Projectile).Center;
      ((Entity) this.Projectile).width = ((Entity) this.Projectile).height = 128;
      ((Entity) this.Projectile).Center = ((Entity) this.Projectile).position;
      if (this.Projectile.owner == Main.myPlayer)
        this.Projectile.Damage();
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      SoundEngine.PlaySound(ref SoundID.NPCDeath7, new Vector2?(((Entity) this.Projectile).Center), (SoundUpdateCallback) null);
      for (int index3 = 0; index3 < 20; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index5 = 0; index5 < 10; ++index5)
      {
        int index6 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 55, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index6].noGravity = true;
        Dust dust1 = Main.dust[index6];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 5f);
        int index7 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 55, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index7];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      for (int index8 = 0; index8 < 4; ++index8)
      {
        int index9 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore1 = Main.gore[index9];
        gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
        Gore gore2 = Main.gore[index9];
        gore2.velocity = Vector2.op_Addition(gore2.velocity, Utils.RotatedBy(new Vector2(1f, 1f), 1.5707963705062866 * (double) index8, new Vector2()));
      }
    }

    public virtual Color? GetAlpha(Color lightColor)
    {
      return new Color?(Color.op_Multiply(new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 610 - (int) Main.mouseTextColor * 2), this.Projectile.Opacity));
    }

    public virtual bool PreDraw(ref Color lightColor) => true;
  }
}
