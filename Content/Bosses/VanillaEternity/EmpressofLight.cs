// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.EmpressofLight
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class EmpressofLight : EModeNPCBehaviour
  {
    public int AttackTimer;
    public int AttackCounter;
    public int P2SwordsAttackCounter;
    public int DashCounter;
    public bool DroppedSummon;
    private float startRotation;
    private Vector2 targetPos;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(636);

    private static int SwordWallCap => !WorldSavingSystem.MasochistModeReal ? 3 : 4;

    public bool DoParallelSwordWalls
    {
      get => this.P2SwordsAttackCounter % EmpressofLight.SwordWallCap > 0;
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackTimer);
      binaryWriter.Write7BitEncodedInt(this.AttackCounter);
      binaryWriter.Write7BitEncodedInt(this.P2SwordsAttackCounter);
      binaryWriter.Write7BitEncodedInt(this.DashCounter);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackTimer = binaryReader.Read7BitEncodedInt();
      this.AttackCounter = binaryReader.Read7BitEncodedInt();
      this.P2SwordsAttackCounter = binaryReader.Read7BitEncodedInt();
      this.DashCounter = binaryReader.Read7BitEncodedInt();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.5, (MidpointRounding) 0);
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return ((double) npc.ai[0] == 8.0 || (double) npc.ai[0] == 9.0) && (double) npc.ai[1] >= 40.0 && base.CanHitPlayer(npc, target, ref CooldownSlot);
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.empressBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return base.SafePreAI(npc);
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<PurgedBuff>(), 2, true, false);
      bool useP2Attacks = (double) npc.ai[3] != 0.0 || WorldSavingSystem.MasochistModeReal;
      switch (npc.ai[0])
      {
        case 1f:
          if ((double) npc.ai[1] == 0.0)
          {
            this.AttackTimer = 0;
            ++this.AttackCounter;
            EModeNPCBehaviour.NetSync(npc);
            break;
          }
          break;
        case 2f:
          if (useP2Attacks && (double) npc.ai[1] > 80.0 && !WorldSavingSystem.MasochistModeReal)
          {
            npc.ai[1] -= 0.5f;
            break;
          }
          break;
        case 4f:
          if ((double) npc.ai[1] == 0.0)
          {
            this.AttackTimer = 0;
            EModeNPCBehaviour.NetSync(npc);
            EmpressofLight.TryRandom(npc);
          }
          if ((double) npc.ai[1] > 97.0 & useP2Attacks)
          {
            this.SwordCircle(npc, 97f);
            break;
          }
          break;
        case 5f:
          if (this.AttackTimer < 2)
          {
            if ((double) npc.ai[1] > 1.0)
              npc.ai[1] -= 0.5f;
            if ((double) npc.ai[1] == 30.0)
            {
              npc.ai[1] = 0.0f;
              ++this.AttackTimer;
            }
          }
          if (useP2Attacks && this.AttackTimer >= 2)
          {
            if ((double) npc.ai[1] == 20.0)
              this.startRotation = Utils.NextFloat(Main.rand, 6.28318548f);
            if ((double) npc.ai[1] >= (WorldSavingSystem.MasochistModeReal ? 20.0 : 35.0) && (double) npc.ai[1] <= 60.0)
            {
              NPC npc1 = npc;
              ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, ((Entity) npc).velocity);
              ((Entity) npc).velocity = Vector2.Zero;
              if ((double) npc.ai[1] % 5.0 == 0.0)
              {
                for (float num = 0.0f; (double) num < 1.0; num += 0.0416666679f)
                {
                  Vector2 vector2 = Utils.RotatedBy(Vector2.UnitY, 1.5707963705062866 + 6.2831854820251465 * (double) num + (double) this.startRotation, new Vector2());
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Utils.RotatedBy(vector2, -1.5707963705062866, new Vector2()), 30f)), Vector2.Zero, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.3f), 0.0f, Main.myPlayer, Utils.ToRotation(vector2), num, 0.0f);
                }
                break;
              }
              break;
            }
            break;
          }
          break;
        case 6f:
          if (!WorldSavingSystem.MasochistModeReal)
          {
            if ((double) npc.ai[1] == 0.0 && this.AttackTimer == 0 && ((IEnumerable<Projectile>) Main.projectile).Count<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == 873)) > 20)
            {
              npc.ai[1] -= 30f;
              npc.netUpdate = true;
            }
            if ((double) npc.ai[1] == 1.0)
              EModeNPCBehaviour.NetSync(npc);
            NPC npc2 = npc;
            ((Entity) npc2).position = Vector2.op_Subtraction(((Entity) npc2).position, Vector2.op_Division(((Entity) npc).velocity, (double) npc.ai[3] == 0.0 ? 2f : 4f));
            break;
          }
          break;
        case 7f:
          int num1 = WorldSavingSystem.MasochistModeReal ? -15 : -45;
          if ((double) npc.ai[1] == 0.0)
          {
            EmpressofLight.TryRandom(npc);
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if ((double) npc.ai[1] == (double) byte.MaxValue)
          {
            this.AttackTimer = num1;
            ++this.P2SwordsAttackCounter;
            this.startRotation = Utils.NextFloat(Main.rand, 6.28318548f);
          }
          if ((double) npc.ai[1] > (double) byte.MaxValue)
          {
            if (this.DoParallelSwordWalls)
            {
              this.ParallelSwordWalls(npc, (float) byte.MaxValue);
              break;
            }
            this.ExcelSpreadsheet(npc, (float) byte.MaxValue);
            break;
          }
          break;
        case 8f:
        case 9f:
          this.Dash(npc, useP2Attacks);
          break;
        case 10f:
          if (npc.dontTakeDamage && (double) npc.ai[1] > 120.0)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(160f, Vector2.UnitY));
            ((Entity) npc).Center = Vector2.Lerp(((Entity) npc).Center, vector2, 0.1f);
            if (npc.life < npc.lifeMax / 2)
            {
              npc.HealEffect(npc.lifeMax / 2 - npc.life, true);
              npc.life = npc.lifeMax / 2;
              break;
            }
            break;
          }
          break;
        case 11f:
          if ((WorldSavingSystem.MasochistModeReal || (double) npc.ai[1] > 40.0) && (double) npc.ai[1] % 3.0 == 0.0 && npc.HasValidTarget)
          {
            Vector2 vector2_1 = ((Entity) Main.player[npc.target]).velocity;
            if (Vector2.op_Equality(vector2_1, Vector2.Zero) || (double) ((Vector2) ref vector2_1).Length() < 1.0)
              vector2_1 = Utils.SafeNormalize(vector2_1, Vector2.op_UnaryNegation(Vector2.UnitY));
            Vector2 vector2_2 = Vector2.op_Multiply(90f, Utils.RotatedBy(vector2_1, 1.5707963705062866, new Vector2()));
            Vector2 vector2_3 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, vector2_2);
            Vector2 vector2_4 = ((Entity) Main.player[npc.target]).DirectionFrom(vector2_3);
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_3, Vector2.Zero, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.3f), 0.0f, Main.myPlayer, Utils.ToRotation(vector2_4), npc.ai[1] / 100f, 0.0f);
              break;
            }
            break;
          }
          break;
        case 12f:
          if ((double) npc.ai[1] < 4.0)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(160f, Vector2.UnitY));
            ((Entity) npc).Center = Vector2.Lerp(((Entity) npc).Center, vector2, 0.1f);
            if ((double) ((Entity) npc).Distance(vector2) > 160.0)
              --npc.ai[1];
          }
          if ((double) npc.ai[1] == 75.0)
            this.startRotation = npc.HasValidTarget ? Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) : 1.57079637f;
          if ((double) npc.ai[1] >= 75.0 && (double) npc.ai[1] < 99.0 && FargoSoulsUtil.HostCheck)
          {
            float num2 = (float) (((double) npc.ai[1] - 75.0) / 24.0);
            if (WorldSavingSystem.MasochistModeReal)
            {
              Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, -(0.2617993950843811 * ((double) npc.ai[1] - 75.0)), new Vector2()));
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(20f, vector2), 873, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.3f), 0.0f, Main.myPlayer, (float) npc.target, num2, 0.0f);
            }
            float radians = MathHelper.ToRadians(24f);
            float num3 = this.startRotation + MathHelper.Lerp(-radians, radians, num2);
            Vector2 rotationVector2 = Utils.ToRotationVector2(num3);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(rotationVector2, 160f)), Vector2.op_Multiply(Vector2.op_Multiply(rotationVector2, 10f), 60f)), Vector2.op_Multiply(Vector2.op_UnaryNegation(rotationVector2), 10f), 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.3f), 0.0f, Main.myPlayer, num3, num2, 0.0f);
          }
          if ((double) npc.ai[1] > 99.0 && !WorldSavingSystem.MasochistModeReal)
          {
            npc.ai[1] -= 0.65f;
            break;
          }
          break;
      }
      EModeUtils.DropSummon(npc, "PrismaticPrimrose", NPC.downedEmpressOfLight, ref this.DroppedSummon, Main.hardMode);
      return true;
    }

    private static void TryRandom(NPC npc)
    {
      if (!WorldSavingSystem.MasochistModeReal || npc.life >= npc.lifeMax / 2)
        return;
      npc.ai[2] += (float) Main.rand.Next(3);
      npc.netUpdate = true;
    }

    private static int BaseProjDmg(NPC npc)
    {
      return !Main.dayTime ? (int) ((double) npc.defDamage * 0.6) : 9999;
    }

    private void SwordCircle(NPC npc, float stop)
    {
      int num1 = 60;
      if (this.AttackTimer == 0)
        SoundEngine.PlaySound(ref SoundID.Item161, new Vector2?(npc.HasValidTarget ? ((Entity) Main.player[npc.target]).Center : ((Entity) npc).Center), (SoundUpdateCallback) null);
      else if (this.AttackTimer == num1)
      {
        this.targetPos = ((Entity) Main.player[npc.target]).Center;
        this.startRotation = npc.HasValidTarget ? Utils.ToRotation(((Entity) Main.player[npc.target]).velocity) : 0.0f;
      }
      ++this.AttackTimer;
      if ((double) ((Entity) Main.player[npc.target]).Distance(this.targetPos) > 600.0)
        this.targetPos = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.player[npc.target], this.targetPos), 600f));
      if (this.AttackTimer % 90 == 30)
        SoundEngine.PlaySound(ref SoundID.Item164, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
      int num2 = WorldSavingSystem.MasochistModeReal ? 210 : 160;
      float num3 = 1.5f;
      if (this.AttackTimer > num1 && (double) this.AttackTimer <= (double) num2 * (double) num3 + (double) num1 && this.AttackTimer % 2 == 0)
      {
        int num4 = WorldSavingSystem.MasochistModeReal ? 3 : 2;
        for (int index1 = 0; index1 < num4; ++index1)
        {
          int num5 = WorldSavingSystem.MasochistModeReal ? -1 : 1;
          float num6 = 6.28318548f / (float) num2 * (float) this.AttackTimer * (float) num5;
          Vector2 vector2_1 = Utils.RotatedBy(Vector2.UnitX, (double) this.startRotation + (double) num6 + 6.2831854820251465 / (double) num4 * (double) index1, new Vector2());
          Vector2 vector2_2 = Vector2.op_Addition(this.targetPos, Vector2.op_Multiply(600f, vector2_1));
          Vector2 vector2_3 = Vector2.Normalize(Vector2.op_Subtraction(this.targetPos, vector2_2));
          float num7 = (float) ((double) (this.AttackTimer - num1) / (double) num2 % 1.0);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_4 = Vector2.op_Multiply(Vector2.op_UnaryNegation(vector2_3), WorldSavingSystem.MasochistModeReal ? 7.5f : 2.5f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(vector2_2, Vector2.op_Multiply(vector2_4, 60f)), vector2_4, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, Utils.ToRotation(vector2_3), num7, 0.0f);
            float radians = MathHelper.ToRadians(45f);
            for (int index2 = -1; index2 <= 1; ++index2)
            {
              Vector2 vector2_5 = Vector2.op_Addition(this.targetPos, Vector2.op_Multiply((float) (128.0 * (double) (index2 + 2) + 600.0), Utils.RotatedBy(vector2_1, 3.1415927410125732 / (double) num4 / 3.0 * (double) (2 - index2) - (double) num6 * 2.0, new Vector2())));
              Vector2 vector2_6 = Vector2.Normalize(Vector2.op_Subtraction(this.targetPos, vector2_5));
              Vector2 vector2_7 = Vector2.op_Multiply(2.5f * (float) (index2 + 2), Utils.RotatedBy(vector2_6, 1.5707963705062866 * (double) index2, new Vector2()));
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(vector2_5, Vector2.op_Multiply(vector2_7, 60f)), vector2_7, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, (float) ((double) Utils.ToRotation(vector2_6) + 3.1415927410125732 + (double) radians * (double) index2), num7, 0.0f);
            }
          }
        }
      }
      if (!npc.HasValidTarget)
      {
        npc.TargetClosest(false);
        if (!npc.HasValidTarget)
          this.AttackTimer += 9000;
      }
      if ((double) this.AttackTimer >= (double) num2 * (double) num3 + (double) (num1 * 2))
        return;
      npc.ai[1] = stop;
    }

    private void ParallelSwordWalls(NPC npc, float stop)
    {
      if (this.AttackTimer == 0)
      {
        SoundEngine.PlaySound(ref SoundID.Item161, new Vector2?(npc.HasValidTarget ? ((Entity) Main.player[npc.target]).Center : ((Entity) npc).Center), (SoundUpdateCallback) null);
        for (float i = 0.0f; (double) i < 1.0; i += 0.07692308f)
        {
          Vector2 spinningpoint = Utils.RotatedBy(Vector2.UnitY, 1.5707963705062866 + 6.2831854820251465 * (double) i + (double) this.startRotation, new Vector2());
          if (FargoSoulsUtil.HostCheck)
          {
            LastingRainbow(Vector2.op_Multiply(6f, spinningpoint), 620);
            LastingRainbow(Vector2.op_Multiply(-7f, spinningpoint), 620);
            LastingRainbow(Vector2.op_Multiply(8.5f, Utils.RotatedBy(spinningpoint, 1.5707963705062866, new Vector2())), 620);
          }

          void LastingRainbow(Vector2 vel, int timeLeft)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(Utils.RotatedBy(spinningpoint, -1.5707963705062866, new Vector2()), 30f));
            int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, vel, 872, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, 0.0f, i, 0.0f);
            if (index == Main.maxProjectiles)
              return;
            Main.projectile[index].timeLeft = timeLeft;
          }
        }
      }
      int num1 = WorldSavingSystem.MasochistModeReal ? 30 : 45;
      int num2 = this.AttackTimer - 15;
      if (num2 == -1)
      {
        this.targetPos = ((Entity) Main.player[npc.target]).Center;
        this.startRotation += 1.57079637f * Utils.NextFloat(Main.rand, 0.9f, 1.1f);
      }
      if (num2 >= 0 && num2 <= num1)
      {
        int num3 = WorldSavingSystem.MasochistModeReal ? 1 : 2;
        if (num2 % num3 == 0)
        {
          for (int index = -1; index <= 1; index += 2)
          {
            float num4 = (float) num2 / (float) num1;
            float startRotation = this.startRotation;
            if (index < 0)
              startRotation += 3.14159274f;
            Vector2 vector2_1 = Vector2.op_Subtraction(Vector2.op_Addition(this.targetPos, Vector2.op_Multiply((float) (1200.0 * (1.0 - (double) num4)) * (float) index, Utils.ToRotationVector2(this.startRotation + 1.57079637f))), Vector2.op_Multiply(1200f, Utils.ToRotationVector2(startRotation)));
            float num5 = startRotation;
            float num6 = (float) num2 / (float) num1;
            Vector2 vector2_2 = Vector2.op_Multiply(-16f, Utils.ToRotationVector2(num5));
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(vector2_1, Vector2.op_Multiply(vector2_2, 60f)), vector2_2, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, num5, num6, 0.0f);
          }
        }
      }
      bool flag = false;
      if (!npc.HasValidTarget)
      {
        npc.TargetClosest(false);
        if (!npc.HasValidTarget)
          flag = true;
      }
      if (++this.AttackTimer <= 105 + num1)
      {
        npc.ai[1] = stop;
      }
      else
      {
        if (flag || this.P2SwordsAttackCounter % EmpressofLight.SwordWallCap == EmpressofLight.SwordWallCap - 1)
          return;
        npc.ai[1] = stop - 1f;
      }
    }

    private void ExcelSpreadsheet(NPC npc, float stop)
    {
      if (this.AttackTimer == 0)
      {
        SoundEngine.PlaySound(ref SoundID.Item161, new Vector2?(npc.HasValidTarget ? ((Entity) Main.player[npc.target]).Center : ((Entity) npc).Center), (SoundUpdateCallback) null);
        this.startRotation = Utils.NextFloat(Main.rand, 6.28318548f);
      }
      int num1 = 30;
      if (++this.AttackTimer > 0)
      {
        if (this.AttackTimer % num1 == 0)
        {
          float num2 = 1f;
          Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Addition(this.targetPos, Vector2.op_Multiply(600f, Utils.RotatedBy(Vector2.UnitX, (double) this.startRotation, new Vector2()))), Vector2.op_Multiply(MathHelper.Lerp(-800f, 800f, num2), Utils.RotatedBy(Vector2.UnitY, (double) this.startRotation, new Vector2())));
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 rotationVector2 = Utils.ToRotationVector2(this.startRotation + 3.14159274f);
            Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(rotationVector2, -1.5707963705062866, new Vector2()), WorldSavingSystem.MasochistModeReal ? 2f : 1f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(vector2_1, Vector2.op_Multiply(60f, vector2_2)), vector2_2, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, Utils.ToRotation(rotationVector2), num2, 0.0f);
          }
          this.targetPos = npc.HasValidTarget ? ((Entity) Main.player[npc.target]).Center : ((Entity) npc).Center;
          this.startRotation += (float) (1.5707963705062866 * (Utils.NextBool(Main.rand) ? -1.0 : 1.0));
          if (Utils.NextBool(Main.rand))
            this.startRotation += 3.14159274f;
          this.startRotation += MathHelper.ToRadians(WorldSavingSystem.MasochistModeReal ? 30f : 15f) * Utils.NextFloat(Main.rand, -1f, 1f);
          if (this.AttackTimer % num1 * 4 == 0)
            SoundEngine.PlaySound(ref SoundID.Item163, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
        }
        if (this.AttackTimer % 3 == 0)
        {
          float num3 = (float) (this.AttackTimer % num1) / (float) num1;
          Vector2 vector2_3 = Vector2.op_Addition(Vector2.op_Addition(this.targetPos, Vector2.op_Multiply(600f, Utils.RotatedBy(Vector2.UnitX, (double) this.startRotation, new Vector2()))), Vector2.op_Multiply(MathHelper.Lerp(-800f, 800f, num3), Utils.RotatedBy(Vector2.UnitY, (double) this.startRotation, new Vector2())));
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 rotationVector2 = Utils.ToRotationVector2(this.startRotation + 3.14159274f);
            Vector2 vector2_4 = Vector2.op_Multiply(Utils.RotatedBy(rotationVector2, -1.5707963705062866, new Vector2()), WorldSavingSystem.MasochistModeReal ? 1.5f : 1f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(vector2_3, Vector2.op_Multiply(60f, vector2_4)), vector2_4, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, Utils.ToRotation(rotationVector2), num3, 0.0f);
          }
        }
      }
      if (!npc.HasValidTarget)
      {
        npc.TargetClosest(false);
        if (!npc.HasValidTarget)
          this.AttackTimer += 9000;
      }
      int num4 = WorldSavingSystem.MasochistModeReal ? 12 : 8;
      if (this.AttackTimer >= num1 * num4 + num1 * 2)
        return;
      npc.ai[1] = stop;
    }

    private void Dash(NPC npc, bool useP2Attacks)
    {
      bool flag1 = this.AttackCounter % 2 == 0;
      if ((double) npc.ai[1] == 0.0)
      {
        this.AttackTimer = 0;
        if (!flag1)
          SoundEngine.PlaySound(ref SoundID.Item164, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
        if (--this.DashCounter <= 0)
        {
          this.DashCounter = 4;
          --npc.ai[2];
        }
        else if (WorldSavingSystem.MasochistModeReal && npc.life < npc.lifeMax / 2)
          npc.ai[2] += (float) Main.rand.Next(3);
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if ((double) npc.ai[1] < 40.0)
      {
        bool flag2 = true;
        if (!flag1)
        {
          Vector2 center = ((Entity) Main.player[npc.target]).Center;
          center.X += (float) (550.0 * ((double) npc.ai[0] == 8.0 ? 1.0 : -1.0));
          ((Entity) npc).Center = Vector2.Lerp(((Entity) npc).Center, center, 0.1f);
          if ((double) ((Entity) npc).Distance(center) < 240.0)
          {
            int num1 = ++this.AttackTimer % 2 == 0 ? -1 : 1;
            float num2 = (float) (((double) npc.ai[1] - 10.0) / 30.0);
            Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 24f) * (float) num1, Vector2.UnitY);
            vector2.X += 30f * (float) Math.Sign(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center).X);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, 873, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, (float) npc.target, num2, 0.0f);
          }
          else
          {
            flag2 = false;
            if ((double) npc.ai[1] > 1.0)
              --npc.ai[1];
          }
        }
        if (flag2 && this.DashCounter == 3)
        {
          if ((double) npc.ai[1] == 0.0)
            SoundEngine.PlaySound(ref SoundID.Item160, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if ((double) npc.ai[1] < 39.0)
            npc.ai[1] -= 0.33f;
          else
            npc.ai[1] = 39f;
        }
      }
      if ((double) npc.ai[1] == 40.0 && flag1)
      {
        float num3 = (double) npc.ai[0] == 8.0 ? 0.0f : 3.14159274f;
        for (int index = -2; index <= 2; ++index)
        {
          if (index != 0)
          {
            float num4 = num3 + MathHelper.ToRadians(20f) / 2f * (float) index;
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 923, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, num4, (float) ((Entity) npc).whoAmI, 0.0f);
          }
        }
      }
      if ((double) npc.ai[1] < 40.0)
        return;
      if (flag1 || !WorldSavingSystem.MasochistModeReal)
        npc.ai[1] -= 0.33f;
      if (!WorldSavingSystem.MasochistModeReal)
        ((Entity) npc).velocity.Y = 0.0f;
      if (!(flag1 & useP2Attacks) || ++this.AttackTimer % 15 != 0)
        return;
      float num5 = (double) npc.ai[0] == 8.0 ? 0.0f : 3.14159274f;
      for (int index = -2; index <= 2; ++index)
      {
        if (index != 0 && FargoSoulsUtil.HostCheck)
        {
          float num6 = num5 + MathHelper.ToRadians(40f) / 2f * (float) index;
          float num7 = (float) (((double) npc.ai[1] - 40.0) / 50.0);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 919, FargoSoulsUtil.ScaledProjectileDamage(EmpressofLight.BaseProjDmg(npc), 1.5f), 0.0f, Main.myPlayer, num6, num7, 0.0f);
        }
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SmiteBuff>(), 1800, true, false);
    }

    public override void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      if (!ProjectileID.Sets.CultistIsResistantTo[projectile.type] || FargoSoulsUtil.IsSummonDamage(projectile))
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.75f);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (npc.life < npc.lifeMax / 2)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.6666667f);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 37);
      EModeNPCBehaviour.LoadExtra(recolor, 156);
      EModeNPCBehaviour.LoadExtra(recolor, 157);
      EModeNPCBehaviour.LoadExtra(recolor, 158);
      EModeNPCBehaviour.LoadExtra(recolor, 159);
      EModeNPCBehaviour.LoadExtra(recolor, 160);
      EModeNPCBehaviour.LoadExtra(recolor, 187);
      EModeNPCBehaviour.LoadExtra(recolor, 188);
    }
  }
}
