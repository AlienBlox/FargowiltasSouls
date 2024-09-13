// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Ammos.FargoArrowProj
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
namespace FargowiltasSouls.Content.Projectiles.Ammos
{
  public class FargoArrowProj : ModProjectile
  {
    private readonly int[] dusts = new int[5]
    {
      130,
      55,
      133,
      131,
      132
    };
    private int currentDust;
    private int timer;
    private Vector2 velocity;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 10;
      ((Entity) this.Projectile).height = 10;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.arrow = true;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 200;
      this.Projectile.light = 1f;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = true;
      this.Projectile.extraUpdates = 1;
      this.AIType = 1;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 2;
    }

    public virtual void AI()
    {
      Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, this.dusts[this.currentDust], ((Entity) this.Projectile).velocity.X * 0.5f, ((Entity) this.Projectile).velocity.Y * 0.5f, 150, new Color(), 1.2f);
      ++this.currentDust;
      if (this.currentDust > 4)
        this.currentDust = 0;
      if ((double) this.Projectile.localAI[0] == 0.0 && (double) this.Projectile.localAI[1] == 0.0)
      {
        this.Projectile.localAI[0] = ((Entity) this.Projectile).Center.X;
        this.Projectile.localAI[1] = ((Entity) this.Projectile).Center.Y;
        this.velocity = new Vector2(((Entity) this.Projectile).velocity.X, ((Entity) this.Projectile).velocity.Y);
      }
      ++this.timer;
      if (this.timer < 60)
        return;
      Player player = Main.player[this.Projectile.owner];
      int num1 = Main.rand.Next(5, 10);
      for (int index1 = 0; index1 < num1; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 220, 0.0f, 0.0f, 100, new Color(), 0.5f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.6f);
        --Main.dust[index2].velocity.Y;
        Main.dust[index2].position = Vector2.Lerp(Main.dust[index2].position, ((Entity) this.Projectile).Center, 0.5f);
        Main.dust[index2].noGravity = true;
      }
      int num2 = 1;
      int nextSlot = Projectile.GetNextSlot();
      if (Main.ProjectileUpdateLoopIndex < nextSlot && Main.ProjectileUpdateLoopIndex != -1)
        ++num2;
      int index = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), this.Projectile.localAI[0], this.Projectile.localAI[1], this.velocity.X, this.velocity.Y, 640, this.Projectile.damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, (float) num2, 0.0f);
      this.timer = 0;
      Main.projectile[index].localNPCHitCooldown = 5;
      Main.projectile[index].usesLocalNPCImmunity = true;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.OnHit();
      ++this.Projectile.ai[1];
      if ((double) this.Projectile.ai[1] == 1.0)
        this.Projectile.damage = (int) ((double) this.Projectile.damage * 0.6600000262260437);
      if ((double) this.Projectile.ai[1] >= 10.0)
        this.Projectile.Kill();
      ((Entity) this.Projectile).velocity.X = -this.velocity.X;
      ((Entity) this.Projectile).velocity.Y = -this.velocity.Y;
      int targetWithLineOfSight = this.Projectile.FindTargetWithLineOfSight(800f);
      if (targetWithLineOfSight != -1)
      {
        NPC npc = Main.npc[targetWithLineOfSight];
        float num = ((Entity) this.Projectile).Distance(((Entity) npc).Center);
        Vector2 vector2 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitY), MathHelper.Lerp((float) ((Entity) npc).height * 0.1f, (float) ((Entity) npc).height * 0.5f, Utils.GetLerpValue(0.0f, 300f, num, false)));
        ((Entity) this.Projectile).velocity = Vector2.op_Multiply(Utils.SafeNormalize(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Projectile, Vector2.op_Addition(((Entity) npc).Center, vector2)), Vector2.op_UnaryNegation(Vector2.UnitY)), ((Vector2) ref ((Entity) this.Projectile).velocity).Length());
        this.Projectile.netUpdate = true;
      }
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.OnHit();
      target.AddBuff(24, 600, false);
      target.AddBuff(44, 600, false);
      target.AddBuff(39, 600, false);
      target.AddBuff(69, 600, false);
      target.AddBuff(70, 600, false);
    }

    public void OnHit()
    {
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index = 0; index < 10; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 58, ((Entity) this.Projectile).velocity.X * 0.1f, ((Entity) this.Projectile).velocity.Y * 0.1f, 150, new Color(), 1.2f);
      for (int index = 0; index < 3; ++index)
      {
        if (!Main.dedServ)
          Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(((Entity) this.Projectile).velocity.X * 0.05f, ((Entity) this.Projectile).velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
      }
      float num1 = ((Entity) this.Projectile).position.X + (float) Main.rand.Next(-400, 400);
      float num2 = ((Entity) this.Projectile).position.Y - (float) Main.rand.Next(600, 900);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(num1, num2);
      float num3 = ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2) - vector2.X;
      float num4 = ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2) - vector2.Y;
      float num5 = 22f / (float) Math.Sqrt((double) num3 * (double) num3 + (double) num4 * (double) num4);
      float num6 = num3 * num5;
      float num7 = num4 * num5;
      int damage = this.Projectile.damage;
      int index1 = Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), num1, num2, num6, num7, 92, damage, this.Projectile.knockBack, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      Main.projectile[index1].ai[1] = ((Entity) this.Projectile).position.Y;
      Main.projectile[index1].ai[0] = 1f;
      Main.projectile[index1].localNPCHitCooldown = 2;
      Main.projectile[index1].usesLocalNPCImmunity = true;
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index2 = 0; index2 < 10; ++index2)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      for (int index3 = 0; index3 < 5; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      int index6 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
      Gore gore1 = Main.gore[index6];
      gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.4f);
      Main.gore[index6].velocity.X += (float) Main.rand.Next(-10, 11) * 0.1f;
      Main.gore[index6].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.1f;
      int index7 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, new Vector2(), Main.rand.Next(61, 64), 1f);
      Gore gore2 = Main.gore[index7];
      gore2.velocity = Vector2.op_Multiply(gore2.velocity, 0.4f);
      Main.gore[index7].velocity.X += (float) Main.rand.Next(-10, 11) * 0.1f;
      Main.gore[index7].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.1f;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      this.Projectile.penetrate = -1;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2);
      ((Entity) this.Projectile).width = 64;
      ((Entity) this.Projectile).height = 64;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X - (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y - (float) (((Entity) this.Projectile).height / 2);
      this.Projectile.Damage();
    }

    public virtual void OnKill(int timeleft) => this.OnHit();
  }
}
