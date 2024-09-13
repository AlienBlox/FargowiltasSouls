// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Projectiles.Masomode.FishronRitual
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Projectiles.Masomode
{
  public class FishronRitual : ModProjectile
  {
    public virtual void SetStaticDefaults() => ((ModType) this).SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Projectile).width = 320;
      ((Entity) this.Projectile).height = 320;
      this.Projectile.ignoreWater = true;
      this.Projectile.tileCollide = false;
      this.Projectile.timeLeft = 300;
      this.Projectile.alpha = (int) byte.MaxValue;
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.Projectile.extraUpdates = 1;
    }

    public virtual void AI()
    {
      NPC npc = FargoSoulsUtil.NPCExists(this.Projectile.ai[1], 370);
      if (npc == null)
      {
        this.Projectile.Kill();
      }
      else
      {
        ((Entity) this.Projectile).Center = ((Entity) npc).Center;
        if ((double) this.Projectile.localAI[0] == 0.0)
        {
          this.Projectile.localAI[0] = 1f;
          this.Projectile.netUpdate = true;
        }
        if ((double) this.Projectile.localAI[1] == 0.0)
        {
          this.Projectile.alpha -= 17;
          if ((double) npc.ai[0] % 5.0 == 1.0)
            this.Projectile.localAI[1] = 1f;
        }
        else
          this.Projectile.alpha += 9;
        if (this.Projectile.alpha < 0)
          this.Projectile.alpha = 0;
        if (this.Projectile.alpha > (int) byte.MaxValue)
        {
          if ((double) npc.ai[0] < 4.0 && Main.netMode == 2)
          {
            ModPacket packet = ((ModType) this).Mod.GetPacket(256);
            ((BinaryWriter) packet).Write((byte) 6);
            ((BinaryWriter) packet).Write((int) this.Projectile.ai[1]);
            ((BinaryWriter) packet).Write((int) this.Projectile.ai[0] * 25);
            packet.Send(-1, -1);
          }
          this.Projectile.Kill();
        }
        else
        {
          this.Projectile.scale = (float) (1.0 - (double) this.Projectile.alpha / (double) byte.MaxValue);
          this.Projectile.rotation += (float) Math.PI / 70f;
          if (this.Projectile.alpha == 0)
          {
            for (int index1 = 0; index1 < 2; ++index1)
            {
              float num1 = (float) Main.rand.Next(2, 4);
              float num2 = this.Projectile.scale * 0.6f;
              if (index1 == 1)
              {
                num2 *= 0.42f;
                num1 *= -0.75f;
              }
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
              ((Vector2) ref vector2).Normalize();
              int index2 = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 100, new Color(), 2f);
              Main.dust[index2].noGravity = true;
              Main.dust[index2].noLight = true;
              Dust dust1 = Main.dust[index2];
              dust1.position = Vector2.op_Addition(dust1.position, Vector2.op_Multiply(Vector2.op_Multiply(vector2, 204f), num2));
              Main.dust[index2].velocity = Vector2.op_Multiply(vector2, -num1);
              if (Utils.NextBool(Main.rand, 8))
              {
                Dust dust2 = Main.dust[index2];
                dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
                Main.dust[index2].scale += 0.5f;
              }
            }
          }
          if ((double) npc.ai[0] < 4.0 && this.Projectile.timeLeft <= 240 && this.Projectile.timeLeft >= 180)
          {
            npc.FargoSouls().MutantNibble = false;
            npc.FargoSouls().LifePrevious = int.MaxValue;
            while (npc.buffType[0] != 0)
              npc.DelBuff(0);
            if ((double) this.Projectile.ai[2] == 0.0)
              npc.lifeMax = (int) this.Projectile.ai[0] * 5000;
            if (npc.lifeMax <= 0)
              npc.lifeMax = int.MaxValue;
            int num = (int) ((double) (npc.lifeMax / 30) * (double) Utils.NextFloat(Main.rand, 1f, 1.1f));
            npc.life += num;
            if (npc.life > npc.lifeMax)
              npc.life = npc.lifeMax;
            CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, num, false, false);
            npc.netUpdate = true;
          }
          int num3 = (300 - this.Projectile.timeLeft) / 60;
          double scale = (double) this.Projectile.scale;
          float num4 = (float) Main.rand.Next(1, 3);
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector((float) Main.rand.Next(-10, 11), (float) Main.rand.Next(-10, 11));
          ((Vector2) ref vector2_1).Normalize();
          int index = Dust.NewDust(((Entity) this.Projectile).Center, 0, 0, 135, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index].noGravity = true;
          Main.dust[index].noLight = true;
          Main.dust[index].velocity = Vector2.op_Multiply(vector2_1, num4);
          if (Utils.NextBool(Main.rand))
          {
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
            Main.dust[index].scale += 0.5f;
          }
          Main.dust[index].fadeIn = 2f;
          Lighting.AddLight(((Entity) this.Projectile).Center, 0.4f, 0.9f, 1.1f);
        }
      }
    }

    public virtual bool? CanDamage() => new bool?(false);

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
