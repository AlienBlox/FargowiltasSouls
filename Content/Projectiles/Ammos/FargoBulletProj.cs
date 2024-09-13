// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Ammos.FargoBulletProj
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
  public class FargoBulletProj : ModProjectile
  {
    private int _bounce = 6;
    private readonly int[] dusts = new int[5]
    {
      130,
      55,
      133,
      131,
      132
    };
    private int currentDust;

    public virtual void SetStaticDefaults()
    {
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 12;
      ((Entity) this.Projectile).height = 12;
      this.Projectile.aiStyle = 1;
      this.Projectile.friendly = true;
      this.Projectile.DamageType = DamageClass.Ranged;
      this.Projectile.penetrate = -1;
      this.Projectile.timeLeft = 200;
      this.Projectile.alpha = (int) byte.MaxValue;
      this.Projectile.light = 0.5f;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = true;
      this.Projectile.extraUpdates = 1;
      this.AIType = 14;
      this.Projectile.usesLocalNPCImmunity = true;
      this.Projectile.localNPCHitCooldown = 2;
    }

    public virtual void AI()
    {
      for (int index1 = 0; index1 < 6; ++index1)
      {
        float num1 = ((Entity) this.Projectile).position.X - ((Entity) this.Projectile).velocity.X / 10f * (float) index1;
        float num2 = ((Entity) this.Projectile).position.Y - ((Entity) this.Projectile).velocity.Y / 10f * (float) index1;
        int index2 = Dust.NewDust(new Vector2(num1, num2), 1, 1, this.dusts[this.currentDust], 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].alpha = this.Projectile.alpha;
        Main.dust[index2].position.X = num1;
        Main.dust[index2].position.Y = num2;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.0f);
        Main.dust[index2].noGravity = true;
      }
      ++this.currentDust;
      if (this.currentDust > 4)
        this.currentDust = 0;
      float num3 = (float) Math.Sqrt((double) ((Entity) this.Projectile).velocity.X * (double) ((Entity) this.Projectile).velocity.X + (double) ((Entity) this.Projectile).velocity.Y * (double) ((Entity) this.Projectile).velocity.Y);
      float num4 = this.Projectile.localAI[0];
      if ((double) num4 == 0.0)
      {
        this.Projectile.localAI[0] = num3;
        num4 = num3;
      }
      if (this.Projectile.alpha > 0)
        this.Projectile.alpha -= 25;
      if (this.Projectile.alpha < 0)
        this.Projectile.alpha = 0;
      float num5 = ((Entity) this.Projectile).position.X;
      float num6 = ((Entity) this.Projectile).position.Y;
      float num7 = 300f;
      bool flag = false;
      int num8 = 0;
      if ((double) this.Projectile.ai[1] == 0.0)
      {
        for (int index = 0; index < 200; ++index)
        {
          if (Main.npc[index].CanBeChasedBy((object) this.Projectile, false) && ((double) this.Projectile.ai[1] == 0.0 || (double) this.Projectile.ai[1] == (double) (index + 1)))
          {
            float num9 = ((Entity) Main.npc[index]).position.X + (float) ((Entity) Main.npc[index]).width / 2f;
            float num10 = ((Entity) Main.npc[index]).position.Y + (float) ((Entity) Main.npc[index]).height / 2f;
            float num11 = Math.Abs(((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width / 2f - num9) + Math.Abs(((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height / 2f - num10);
            if ((double) num11 < (double) num7 && Collision.CanHit(new Vector2(((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width / 2f, ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height / 2f), 1, 1, ((Entity) Main.npc[index]).position, ((Entity) Main.npc[index]).width, ((Entity) Main.npc[index]).height))
            {
              num7 = num11;
              num5 = num9;
              num6 = num10;
              flag = true;
              num8 = index;
            }
          }
        }
        if (flag)
          this.Projectile.ai[1] = (float) (num8 + 1);
        flag = false;
      }
      if ((double) this.Projectile.ai[1] > 0.0)
      {
        int index = (int) ((double) this.Projectile.ai[1] - 1.0);
        if (((Entity) Main.npc[index]).active && Main.npc[index].CanBeChasedBy((object) this.Projectile, true) && !Main.npc[index].dontTakeDamage)
        {
          float num12 = ((Entity) Main.npc[index]).position.X + (float) (((Entity) Main.npc[index]).width / 2);
          float num13 = ((Entity) Main.npc[index]).position.Y + (float) (((Entity) Main.npc[index]).height / 2);
          if ((double) Math.Abs(((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2) - num12) + (double) Math.Abs(((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2) - num13) < 1000.0)
          {
            flag = true;
            num5 = ((Entity) Main.npc[index]).position.X + (float) (((Entity) Main.npc[index]).width / 2);
            num6 = ((Entity) Main.npc[index]).position.Y + (float) (((Entity) Main.npc[index]).height / 2);
          }
        }
        else
          this.Projectile.ai[1] = 0.0f;
      }
      if (!this.Projectile.friendly)
        flag = false;
      if (!flag)
        return;
      double num14 = (double) num4;
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(((Entity) this.Projectile).position.X + (float) ((Entity) this.Projectile).width * 0.5f, ((Entity) this.Projectile).position.Y + (float) ((Entity) this.Projectile).height * 0.5f);
      float num15 = num5 - vector2.X;
      float num16 = num6 - vector2.Y;
      double num17 = Math.Sqrt((double) num15 * (double) num15 + (double) num16 * (double) num16);
      float num18 = (float) (num14 / num17);
      float num19 = num15 * num18;
      float num20 = num16 * num18;
      int num21 = 8;
      ((Entity) this.Projectile).velocity.X = (((Entity) this.Projectile).velocity.X * (float) (num21 - 1) + num19) / (float) num21;
      ((Entity) this.Projectile).velocity.Y = (((Entity) this.Projectile).velocity.Y * (float) (num21 - 1) + num20) / (float) num21;
    }

    public virtual bool OnTileCollide(Vector2 oldVelocity)
    {
      this.OnHit();
      if (this._bounce > 1)
      {
        Collision.HitTiles(((Entity) this.Projectile).position, ((Entity) this.Projectile).velocity, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height);
        SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
        --this._bounce;
        if ((double) ((Entity) this.Projectile).velocity.X != (double) oldVelocity.X)
          ((Entity) this.Projectile).velocity.X = -oldVelocity.X;
        if ((double) ((Entity) this.Projectile).velocity.Y != (double) oldVelocity.Y)
          ((Entity) this.Projectile).velocity.Y = -oldVelocity.Y;
      }
      else
        this.Projectile.Kill();
      return false;
    }

    public virtual void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
      this.OnHit();
      target.AddBuff(39, 600, false);
      target.AddBuff(69, 600, false);
      target.AddBuff(70, 600, false);
      if (Utils.NextBool(Main.rand, 3))
        target.AddBuff(31, 180, false);
      else
        target.AddBuff(31, 60, false);
      target.AddBuff(72, 120, false);
    }

    public void OnHit()
    {
      SoundEngine.PlaySound(ref SoundID.Dig, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 68, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
        Main.dust[index2].scale *= 0.9f;
      }
      for (int index = 0; index < 3; ++index)
      {
        float num1 = (float) (-(double) ((Entity) this.Projectile).velocity.X * (double) Main.rand.Next(40, 70) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        float num2 = (float) (-(double) ((Entity) this.Projectile).velocity.Y * (double) Main.rand.Next(40, 70) * 0.0099999997764825821 + (double) Main.rand.Next(-20, 21) * 0.40000000596046448);
        Projectile.NewProjectile(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position.X + num1, ((Entity) this.Projectile).position.Y + num2, num1, num2, 90, this.Projectile.damage, 0.0f, this.Projectile.owner, 0.0f, 0.0f, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index = 0; index < 7; ++index)
        Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
      for (int index3 = 0; index3 < 3; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 3f);
        int index5 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      }
      int index6 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), new Vector2(((Entity) this.Projectile).position.X - 10f, ((Entity) this.Projectile).position.Y - 10f), new Vector2(), Main.rand.Next(61, 64), 1f);
      Gore gore = Main.gore[index6];
      gore.velocity = Vector2.op_Multiply(gore.velocity, 0.3f);
      Main.gore[index6].velocity.X += (float) Main.rand.Next(-10, 11) * 0.05f;
      Main.gore[index6].velocity.Y += (float) Main.rand.Next(-10, 11) * 0.05f;
      if (this.Projectile.owner != Main.myPlayer)
        return;
      this.Projectile.localAI[1] = -1f;
      this.Projectile.maxPenetrate = 0;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X + (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y + (float) (((Entity) this.Projectile).height / 2);
      ((Entity) this.Projectile).width = 80;
      ((Entity) this.Projectile).height = 80;
      ((Entity) this.Projectile).position.X = ((Entity) this.Projectile).position.X - (float) (((Entity) this.Projectile).width / 2);
      ((Entity) this.Projectile).position.Y = ((Entity) this.Projectile).position.Y - (float) (((Entity) this.Projectile).height / 2);
      this.Projectile.Damage();
    }

    public virtual void OnKill(int timeleft)
    {
      this.OnHit();
      SoundEngine.PlaySound(ref SoundID.Item10, new Vector2?(((Entity) this.Projectile).position), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 10; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, 171, 0.0f, 0.0f, 100, new Color(), 1f);
        Main.dust[index2].scale = (float) Main.rand.Next(1, 10) * 0.1f;
        Main.dust[index2].noGravity = true;
        Main.dust[index2].fadeIn = 1.5f;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 0.75f);
      }
      for (int index3 = 0; index3 < 10; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Projectile).position, ((Entity) this.Projectile).width, ((Entity) this.Projectile).height, Main.rand.Next(139, 143), (float) (-(double) ((Entity) this.Projectile).velocity.X * 0.30000001192092896), (float) (-(double) ((Entity) this.Projectile).velocity.Y * 0.30000001192092896), 0, new Color(), 1.2f);
        Main.dust[index4].velocity.X += (float) Main.rand.Next(-50, 51) * 0.01f;
        Main.dust[index4].velocity.Y += (float) Main.rand.Next(-50, 51) * 0.01f;
        Main.dust[index4].velocity.X *= (float) (1.0 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
        Main.dust[index4].velocity.Y *= (float) (1.0 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
        Main.dust[index4].velocity.X += (float) Main.rand.Next(-50, 51) * 0.05f;
        Main.dust[index4].velocity.Y += (float) Main.rand.Next(-50, 51) * 0.05f;
        Main.dust[index4].scale *= (float) (1.0 + (double) Main.rand.Next(-30, 31) * 0.0099999997764825821);
      }
      for (int index5 = 0; index5 < 5; ++index5)
      {
        int num = Main.rand.Next(276, 283);
        int index6 = Gore.NewGore(((Entity) this.Projectile).GetSource_FromThis((string) null), ((Entity) this.Projectile).position, Vector2.op_Multiply(Vector2.op_UnaryNegation(((Entity) this.Projectile).velocity), 0.3f), num, 1f);
        Main.gore[index6].velocity.X += (float) Main.rand.Next(-50, 51) * 0.01f;
        Main.gore[index6].velocity.Y += (float) Main.rand.Next(-50, 51) * 0.01f;
        Main.gore[index6].velocity.X *= (float) (1.0 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
        Main.gore[index6].velocity.Y *= (float) (1.0 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
        Main.gore[index6].scale *= (float) (1.0 + (double) Main.rand.Next(-20, 21) * 0.0099999997764825821);
        Main.gore[index6].velocity.X += (float) Main.rand.Next(-50, 51) * 0.05f;
        Main.gore[index6].velocity.Y += (float) Main.rand.Next(-50, 51) * 0.05f;
      }
    }
  }
}
