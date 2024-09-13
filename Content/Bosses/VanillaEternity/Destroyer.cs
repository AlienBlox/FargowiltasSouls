// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Destroyer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
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
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Destroyer : EModeNPCBehaviour
  {
    public int AttackModeTimer;
    public int CoilRadius;
    public int LaserTimer;
    public int SecondaryAttackTimer;
    public int RotationDirection = 1;
    public bool InPhase2;
    public bool IsCoiling;
    public bool PrepareToCoil;
    public bool DroppedSummon;
    public const int P2_ATTACK_SPACING = 480;
    public const int P2_COIL_BEGIN_TIME = 1920;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(134);

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
      if (Main.getGoodWorld)
        cooldownSlot = 1;
      return base.CanHitPlayer(npc, target, ref cooldownSlot);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.AttackModeTimer);
      binaryWriter.Write7BitEncodedInt(this.CoilRadius);
      binaryWriter.Write7BitEncodedInt(this.LaserTimer);
      binaryWriter.Write7BitEncodedInt(this.SecondaryAttackTimer);
      binaryWriter.Write7BitEncodedInt(this.RotationDirection);
      bitWriter.WriteBit(this.InPhase2);
      bitWriter.WriteBit(this.IsCoiling);
      bitWriter.WriteBit(this.PrepareToCoil);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.AttackModeTimer = binaryReader.Read7BitEncodedInt();
      this.CoilRadius = binaryReader.Read7BitEncodedInt();
      this.LaserTimer = binaryReader.Read7BitEncodedInt();
      this.SecondaryAttackTimer = binaryReader.Read7BitEncodedInt();
      this.RotationDirection = binaryReader.Read7BitEncodedInt();
      this.InPhase2 = bitReader.ReadBit();
      this.IsCoiling = bitReader.ReadBit();
      this.PrepareToCoil = bitReader.ReadBit();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      npc.buffImmune[46] = false;
      npc.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = false;
    }

    private static int ProjectileDamage(NPC npc)
    {
      return FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.444444448f);
    }

    private void CoilAI(NPC npc)
    {
      npc.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = true;
      npc.netUpdate = true;
      ((Entity) npc).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), 20f);
      NPC npc1 = npc;
      ((Entity) npc1).velocity = Vector2.op_Addition(((Entity) npc1).velocity, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(((Entity) npc).velocity, 1.5707963705062866 * (double) this.RotationDirection, new Vector2()), ((Vector2) ref ((Entity) npc).velocity).Length()), (float) this.CoilRadius));
      npc.rotation = Utils.ToRotation(((Entity) npc).velocity) + 1.57079637f;
      if (this.AttackModeTimer == 0)
        this.LaserTimer = 0;
      if (npc.life < npc.lifeMax / 10)
      {
        if ((double) npc.localAI[2] >= 0.0)
        {
          npc.localAI[2] = WorldSavingSystem.MasochistModeReal ? -60f : 0.0f;
          this.AttackModeTimer = 0;
        }
        if ((double) --npc.localAI[2] < -120.0)
        {
          npc.localAI[2] += WorldSavingSystem.MasochistModeReal ? 3f : 6f;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), 14f);
            int num = ModContent.ProjectileType<MechElectricOrbHoming>();
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, num, Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, (float) npc.target, 0.0f, 1f);
          }
        }
        if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0)
        {
          npc.TargetClosest(false);
          if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0)
          {
            this.AttackModeTimer = 0;
            this.CoilRadius = 0;
            this.IsCoiling = false;
            this.PrepareToCoil = false;
            EModeNPCBehaviour.NetSync(npc);
          }
        }
        ++this.AttackModeTimer;
      }
      else
      {
        if ((double) --npc.localAI[2] < 0.0)
        {
          npc.localAI[2] = Main.player[npc.target].HasBuff(ModContent.BuffType<LightningRodBuff>()) ? 110f : 65f;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            double radians = (double) MathHelper.ToRadians(30f);
            ((Vector2) ref vector2_1).Normalize();
            Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 14f);
            int num1 = ModContent.ProjectileType<MechElectricOrbHoming>();
            int num2 = -5;
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2_2, -radians, new Vector2()), num1, Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, (float) npc.target, (float) num2, 1f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, num1, Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, (float) npc.target, (float) num2, 1f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2_2, radians, new Vector2()), num1, Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, (float) npc.target, (float) num2, 1f);
          }
        }
        if (++this.AttackModeTimer > 300)
        {
          this.AttackModeTimer = 0;
          this.CoilRadius = 0;
          this.IsCoiling = false;
          this.PrepareToCoil = false;
          EModeNPCBehaviour.NetSync(npc);
        }
      }
      Vector2 vector2_3 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, 1.5707963705062866 * (double) this.RotationDirection, new Vector2())), 600f));
      if (++this.LaserTimer > 95)
      {
        this.LaserTimer = 0;
        if (FargoSoulsUtil.HostCheck)
        {
          float num3 = (float) npc.life / (float) npc.lifeMax;
          if (WorldSavingSystem.MasochistModeReal)
            num3 = 0.0f;
          int num4 = (int) (14.0 - 10.0 * (double) num3);
          if (num4 % 2 != 0)
            ++num4;
          for (int index = 0; index < num4; ++index)
          {
            Vector2 vector2_4 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, vector2_3), 2.0 * Math.PI / (double) num4 * (double) index, new Vector2());
            Vector2 vector2_5 = Vector2.op_Subtraction(vector2_3, Vector2.op_Multiply(vector2_4, 600f));
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_5, Vector2.op_Multiply(0.2f, vector2_4), ModContent.ProjectileType<DestroyerLaser>(), Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, 1f, 134f, 0.0f);
          }
        }
      }
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2_6 = new Vector2();
        double num = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2_6.X += (float) (Math.Sin(num) * 600.0);
        vector2_6.Y += (float) (Math.Cos(num) * 600.0);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(vector2_3, vector2_6), new Vector2(4f, 4f)), 0, 0, 112, 0.0f, 0.0f, 100, Color.White, 1f)];
        dust1.velocity = Vector2.Zero;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2_6), 5f));
        }
        dust1.noGravity = true;
      }
      Player player1 = Main.player[npc.target];
      if (!((Entity) player1).active || player1.dead)
        return;
      float num5 = ((Entity) player1).Distance(vector2_3);
      if ((double) num5 <= 600.0 || (double) num5 >= 3000.0)
        return;
      Vector2 vector2_7 = Vector2.op_Subtraction(vector2_3, ((Entity) player1).Center);
      float num6 = ((Vector2) ref vector2_7).Length() - 600f;
      ((Vector2) ref vector2_7).Normalize();
      Vector2 vector2_8 = Vector2.op_Multiply(vector2_7, (double) num6 < 34.0 ? num6 : 34f);
      Player player2 = player1;
      ((Entity) player2).position = Vector2.op_Addition(((Entity) player2).position, vector2_8);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) player1).position, ((Entity) player1).width, ((Entity) player1).height, 112, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
      }
    }

    private void NonCoilAI(NPC npc)
    {
      npc.buffImmune[ModContent.BuffType<TimeFrozenBuff>()] = false;
      npc.localAI[2] = 0.0f;
      float maxSpeed = 16f;
      float num15 = 0.1f;
      float num16 = 0.15f;
      bool flag1 = this.AttackModeTimer < 120;
      float flySpeedModifierRatio = (float) npc.life / (float) npc.lifeMax;
      if ((double) flySpeedModifierRatio > 0.5)
        flySpeedModifierRatio = 0.5f;
      if (flag1)
        flySpeedModifierRatio = 0.0f;
      if (npc.HasValidTarget)
      {
        if (!flag1)
        {
          float num = ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center);
          if ((double) num < 600.0)
            maxSpeed *= 0.5f;
          else if ((double) num > 900.0)
          {
            num15 *= 2f;
            num16 *= 2f;
          }
        }
        float num1 = ((Vector2) ref ((Entity) Main.player[npc.target]).velocity).Length() * 1.5f;
        bool flag2 = (double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) npc).velocity) - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)))) < (double) MathHelper.ToRadians(45f);
        if ((double) maxSpeed < (double) num1 & flag2)
          maxSpeed = num1;
      }
      Vector2 target = ((Entity) Main.player[npc.target]).Center;
      if (this.PrepareToCoil)
      {
        num15 = 0.4f;
        num16 = 0.5f;
        target = Vector2.op_Addition(target, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) Main.player[npc.target], ((Entity) npc).Center), 600f));
        if (++this.AttackModeTimer > 120)
          maxSpeed *= 2f;
        if ((double) ((Entity) npc).Distance(target) < 50.0)
        {
          this.AttackModeTimer = 0;
          this.CoilRadius = (int) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center);
          this.IsCoiling = true;
          this.RotationDirection = Math.Sign(MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - Utils.ToRotation(((Entity) npc).velocity)));
          ((Entity) npc).velocity = Vector2.op_Multiply(20f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), -1.5707963705062866 * (double) this.RotationDirection, new Vector2()));
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (npc.life < npc.lifeMax / 10)
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && (Main.projectile[index].type == ModContent.ProjectileType<MechElectricOrbHoming>() || Main.projectile[index].type == ModContent.ProjectileType<MechElectricOrbDestroyer>() || Main.projectile[index].type == ModContent.ProjectileType<DestroyerLaser>() || Main.projectile[index].type == 100))
                Main.projectile[index].Kill();
            }
          }
        }
      }
      else
      {
        if (npc.life < npc.lifeMax / 10)
        {
          if (this.AttackModeTimer < 1920)
          {
            this.AttackModeTimer = 1920;
            EModeNPCBehaviour.NetSync(npc);
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          }
        }
        else
          this.NonCoilAttacksAI(npc, ref num15, ref num16, ref target, ref maxSpeed, ref flySpeedModifierRatio);
        if (++this.AttackModeTimer > 1920)
        {
          this.AttackModeTimer = 0;
          this.PrepareToCoil = true;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        else if (this.AttackModeTimer == 1800)
        {
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 6f, (float) ((Entity) npc).whoAmI, 0.0f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 6f, (float) ((Entity) npc).whoAmI, 0.0f);
          }
        }
      }
      Destroyer.MovementAI(npc, target, num15, num16, maxSpeed);
      NPC npc1 = npc;
      ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f - flySpeedModifierRatio));
    }

    private void NonCoilAttacksAI(
      NPC npc,
      ref float num15,
      ref float num16,
      ref Vector2 target,
      ref float maxSpeed,
      ref float flySpeedModifierRatio)
    {
      int num1 = 1440;
      int num2 = 960;
      if (FargoSoulsUtil.HostCheck && this.AttackModeTimer > 360 && this.AttackModeTimer < 900 && this.AttackModeTimer % (WorldSavingSystem.MasochistModeReal ? 40 : 120) == 12)
      {
        List<NPC> list = ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == 139)).ToList<NPC>();
        int count = list.Count;
        int index1 = Main.rand.Next(count);
        int num3 = 0;
        for (int index2 = 0; index2 < count; ++index2)
        {
          NPC npc1 = list[index1];
          if (!npc1.GetGlobalNPC<Probe>().ShootLaser)
          {
            npc1.GetGlobalNPC<Probe>().ShootLaser = true;
            npc1.GetGlobalNPC<Probe>().AttackTimer = 0;
            if (++num3 < 2)
              index1 += Main.rand.Next(count);
            else
              break;
          }
          index1 = (index1 + 1) % count;
        }
      }
      int num4 = 4;
      if ((double) npc.life < (double) npc.lifeMax * 0.75)
        num4 = 5;
      if ((double) npc.life < (double) npc.lifeMax * 0.5)
        num4 = 6;
      if ((double) npc.life < (double) npc.lifeMax * 0.25)
        num4 = 7;
      int num5 = num1 + num4 * 50;
      if (this.AttackModeTimer == num1)
        this.SecondaryAttackTimer = 0;
      if (this.AttackModeTimer >= num1 && this.AttackModeTimer <= num5 + 90)
      {
        if (WorldSavingSystem.MasochistModeReal)
        {
          num15 *= 0.75f;
          num16 *= 0.75f;
        }
        else if ((double) ((Entity) npc).Distance(target) < 600.0)
        {
          target = Vector2.op_Addition(target, Vector2.op_Multiply(((Entity) npc).DirectionFrom(target), 1000f));
          num15 = 0.4f;
          num16 = 0.5f;
        }
        else
        {
          target = Vector2.op_Addition(target, Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, target), 1.5707963705062866, new Vector2()), 1200f));
          if ((double) ((Entity) npc).Distance(target) < 1200.0)
          {
            maxSpeed *= 0.5f;
          }
          else
          {
            num15 *= 2f;
            num16 *= 2f;
          }
        }
        if (this.AttackModeTimer < num5 && this.AttackModeTimer % 50 == 0)
        {
          Vector2 targetPos = ((Entity) Main.player[npc.target]).Center;
          List<int> intList = new List<int>();
          foreach (NPC npc2 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.realLife == ((Entity) npc).whoAmI && (double) ((Entity) n).Distance(targetPos) < 1600.0)))
            intList.Add(((Entity) npc2).whoAmI);
          NPC npc3 = intList.Count > 0 ? Main.npc[Utils.Next<int>(Main.rand, (IList<int>) intList)] : npc;
          targetPos = Vector2.op_Addition(targetPos, Vector2.op_Multiply(((Entity) npc3).DirectionFrom(targetPos), Math.Min(300f, ((Entity) npc3).Distance(targetPos))));
          float rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc3, targetPos));
          int num6 = (int) ((0.5 + 0.5 * Math.Sin(3.1415927410125732 / (double) (num4 - 1) * (double) this.SecondaryAttackTimer++)) * (8.0 - 7.0 * (double) npc.life / (double) npc.lifeMax));
          if (num6 > 6)
            num6 = 6;
          for (int index = -num6; index <= num6; ++index)
          {
            Vector2 vector2_1 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc3, targetPos), 1.5707963705062866, new Vector2());
            float num7 = (float) (1000 / Math.Max(num6, 1) * index);
            int num8 = 30 + Math.Abs(index) * 5;
            Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Subtraction(Vector2.op_Addition(targetPos, Vector2.op_Multiply(vector2_1, num7)), ((Entity) npc3).Center), (float) num8);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc3).Center, Vector2.op_Multiply(vector2_2, 2f), ModContent.ProjectileType<MechElectricOrbDestroyer>(), Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, rotation, (float) -num8, 1f);
          }
        }
      }
      if (this.AttackModeTimer == num2 - 120)
      {
        this.SecondaryAttackTimer = 0;
        if (FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 9f, (float) ((Entity) npc).whoAmI, 0.0f);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 9f, (float) ((Entity) npc).whoAmI, 0.0f);
        }
      }
      if (this.AttackModeTimer <= num2 || this.AttackModeTimer >= num2 + 420)
        return;
      flySpeedModifierRatio /= 2f;
      if (this.SecondaryAttackTimer == 0)
      {
        if ((double) maxSpeed < 16.0)
          maxSpeed = 16f;
        maxSpeed *= 1.5f;
        num15 *= 10f;
        num16 *= 10f;
        if ((double) ((Entity) npc).Distance(target) >= 400.0)
          return;
        this.SecondaryAttackTimer = 1;
        ((Entity) npc).velocity = Vector2.op_Multiply(20f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, target));
        if (!WorldSavingSystem.MasochistModeReal)
        {
          float num9 = MathHelper.WrapAngle(Utils.ToRotation(((Entity) Main.player[npc.target]).velocity) - Utils.ToRotation(((Entity) npc).velocity));
          ((Entity) npc).velocity = Utils.RotatedBy(((Entity) npc).velocity, (double) MathHelper.ToRadians(30f) * (double) -Math.Sign(num9), new Vector2());
        }
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      else
      {
        double num10 = (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, target)) - (double) Utils.ToRotation(((Entity) npc).velocity);
        while (num10 > Math.PI)
          num10 -= 2.0 * Math.PI;
        while (num10 < -1.0 * Math.PI)
          num10 += 2.0 * Math.PI;
        int num11 = Math.Sign(num10);
        if (Math.Abs(num10) >= (double) MathHelper.ToRadians(45f))
        {
          if (WorldSavingSystem.MasochistModeReal)
            maxSpeed /= 2f;
          else if ((double) maxSpeed > 4.0)
            maxSpeed = 4f;
          if ((double) ((Vector2) ref ((Entity) npc).velocity).Length() > (double) maxSpeed)
          {
            NPC npc4 = npc;
            ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, 0.986f);
          }
          float num12 = 15f;
          num15 /= num12;
          num16 /= num12;
        }
        ((Entity) npc).velocity = Utils.RotatedBy(((Entity) npc).velocity, (double) MathHelper.ToRadians(0.3f) * (double) num11, new Vector2());
        if (this.AttackModeTimer >= num2 + 300 || ++this.SecondaryAttackTimer % 90 != 20)
          return;
        bool flag1 = Utils.NextBool(Main.rand);
        bool flag2 = true;
        foreach (NPC npc5 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.realLife == ((Entity) npc).whoAmI)))
        {
          flag2 = !flag2;
          if (flag2)
          {
            if (FargoSoulsUtil.HostCheck && (double) Utils.NextFloat(Main.rand) > (double) (npc.life / npc.lifeMax))
            {
              float radians = MathHelper.ToRadians(10f);
              float num13 = npc5.rotation + (flag1 ? 0.0f : 3.14159274f) + Utils.NextFloat(Main.rand, -radians, radians);
              int index = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc5).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), Destroyer.ProjectileDamage(npc), 0.0f, Main.myPlayer, 11f, (float) ((Entity) npc5).whoAmI, 0.0f);
              if (index != Main.maxProjectiles)
              {
                Main.projectile[index].localAI[1] = num13;
                if (Main.netMode == 2)
                  NetMessage.SendData(27, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
            flag1 = !flag1;
            if (Utils.NextBool(Main.rand, 5))
              flag1 = !flag1;
          }
        }
      }
    }

    private static void MovementAI(
      NPC npc,
      Vector2 target,
      float num15,
      float num16,
      float maxSpeed)
    {
      double x1 = (double) target.X;
      float y = target.Y;
      double x2 = (double) ((Entity) npc).Center.X;
      float num1 = (float) (x1 - x2);
      float num2 = y - ((Entity) npc).Center.Y;
      Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
      float num3 = (float) Math.Sqrt((double) num1 * (double) num1 + (double) num2 * (double) num2);
      float num4 = Math.Abs(num1);
      float num5 = Math.Abs(num2);
      float num6 = maxSpeed / num3;
      float num7 = num1 * num6;
      float num8 = num2 * num6;
      if (((double) ((Entity) npc).velocity.X > 0.0 && (double) num7 > 0.0 || (double) ((Entity) npc).velocity.X < 0.0 && (double) num7 < 0.0) && ((double) ((Entity) npc).velocity.Y > 0.0 && (double) num8 > 0.0 || (double) ((Entity) npc).velocity.Y < 0.0 && (double) num8 < 0.0))
      {
        if ((double) ((Entity) npc).velocity.X < (double) num7)
          ((Entity) npc).velocity.X += num16;
        else if ((double) ((Entity) npc).velocity.X > (double) num7)
          ((Entity) npc).velocity.X -= num16;
        if ((double) ((Entity) npc).velocity.Y < (double) num8)
          ((Entity) npc).velocity.Y += num16;
        else if ((double) ((Entity) npc).velocity.Y > (double) num8)
          ((Entity) npc).velocity.Y -= num16;
      }
      if ((double) ((Entity) npc).velocity.X > 0.0 && (double) num7 > 0.0 || (double) ((Entity) npc).velocity.X < 0.0 && (double) num7 < 0.0 || (double) ((Entity) npc).velocity.Y > 0.0 && (double) num8 > 0.0 || (double) ((Entity) npc).velocity.Y < 0.0 && (double) num8 < 0.0)
      {
        if ((double) ((Entity) npc).velocity.X < (double) num7)
          ((Entity) npc).velocity.X += num15;
        else if ((double) ((Entity) npc).velocity.X > (double) num7)
          ((Entity) npc).velocity.X -= num15;
        if ((double) ((Entity) npc).velocity.Y < (double) num8)
          ((Entity) npc).velocity.Y += num15;
        else if ((double) ((Entity) npc).velocity.Y > (double) num8)
          ((Entity) npc).velocity.Y -= num15;
        if ((double) Math.Abs(num8) < (double) maxSpeed * 0.20000000298023224 && ((double) ((Entity) npc).velocity.X > 0.0 && (double) num7 < 0.0 || (double) ((Entity) npc).velocity.X < 0.0 && (double) num7 > 0.0))
        {
          if ((double) ((Entity) npc).velocity.Y > 0.0)
            ((Entity) npc).velocity.Y += num15 * 2f;
          else
            ((Entity) npc).velocity.Y -= num15 * 2f;
        }
        if ((double) Math.Abs(num7) < (double) maxSpeed * 0.20000000298023224 && ((double) ((Entity) npc).velocity.Y > 0.0 && (double) num8 < 0.0 || (double) ((Entity) npc).velocity.Y < 0.0 && (double) num8 > 0.0))
        {
          if ((double) ((Entity) npc).velocity.X > 0.0)
            ((Entity) npc).velocity.X += num15 * 2f;
          else
            ((Entity) npc).velocity.X -= num15 * 2f;
        }
      }
      else if ((double) num4 > (double) num5)
      {
        if ((double) ((Entity) npc).velocity.X < (double) num7)
          ((Entity) npc).velocity.X += num15 * 1.1f;
        else if ((double) ((Entity) npc).velocity.X > (double) num7)
          ((Entity) npc).velocity.X -= num15 * 1.1f;
        if ((double) Math.Abs(((Entity) npc).velocity.X) + (double) Math.Abs(((Entity) npc).velocity.Y) < (double) maxSpeed * 0.5)
        {
          if ((double) ((Entity) npc).velocity.Y > 0.0)
            ((Entity) npc).velocity.Y += num15;
          else
            ((Entity) npc).velocity.Y -= num15;
        }
      }
      else
      {
        if ((double) ((Entity) npc).velocity.Y < (double) num8)
          ((Entity) npc).velocity.Y += num15 * 1.1f;
        else if ((double) ((Entity) npc).velocity.Y > (double) num8)
          ((Entity) npc).velocity.Y -= num15 * 1.1f;
        if ((double) Math.Abs(((Entity) npc).velocity.X) + (double) Math.Abs(((Entity) npc).velocity.Y) < (double) maxSpeed * 0.5)
        {
          if ((double) ((Entity) npc).velocity.X > 0.0)
            ((Entity) npc).velocity.X += num15;
          else
            ((Entity) npc).velocity.X -= num15;
        }
      }
      npc.rotation = (float) Math.Atan2((double) ((Entity) npc).velocity.Y, (double) ((Entity) npc).velocity.X) + 1.57f;
      npc.netUpdate = true;
      npc.localAI[0] = 1f;
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.destroyBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return true;
      if (!this.InPhase2)
      {
        if (npc.life < (int) ((double) npc.lifeMax * (WorldSavingSystem.MasochistModeReal ? 0.95 : 0.75)))
        {
          this.InPhase2 = true;
          this.AttackModeTimer = 1920;
          npc.netUpdate = true;
          if (npc.HasPlayerTarget)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
        }
        EModeUtils.DropSummon(npc, "MechWorm", NPC.downedMechBoss1, ref this.DroppedSummon, Main.hardMode);
        return true;
      }
      if (npc.HasValidTarget && (!Main.dayTime || Main.remixWorld))
      {
        npc.timeLeft = 600;
        if (this.IsCoiling)
          this.CoilAI(npc);
        else
          this.NonCoilAI(npc);
        if (Main.netMode == 2 && npc.netUpdate && --npc.netSpam < 0)
        {
          npc.netUpdate = false;
          npc.netSpam = 5;
          NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        return false;
      }
      ++((Entity) npc).velocity.Y;
      if (npc.timeLeft > 60)
        npc.timeLeft = 60;
      return true;
    }

    public override void ModifyHitByAnything(
      NPC npc,
      Player player,
      ref NPC.HitModifiers modifiers)
    {
      base.ModifyHitByAnything(npc, player, ref modifiers);
      if (this.IsCoiling)
      {
        if (npc.life < npc.lifeMax / 10)
        {
          float num = Math.Min(1f, (float) this.AttackModeTimer / 480f);
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, num);
        }
        else
        {
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 0.1f);
        }
      }
      else
      {
        if (!this.PrepareToCoil && this.AttackModeTimer < 1800 && npc.life >= npc.lifeMax / 10)
          return;
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.1f);
      }
    }

    public override void SafeModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      base.SafeModifyHitByProjectile(npc, projectile, ref modifiers);
      if (projectile.numHits > 0 && !FargoSoulsUtil.IsSummonDamage(projectile))
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, (float) (0.66666668653488159 + 0.3333333432674408 / (double) projectile.numHits));
      }
      if (projectile.type == 239)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      if (projectile.type != 476)
        return;
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 0.75f);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(144, 60, true, false);
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 25);
      EModeNPCBehaviour.LoadGore(recolor, 156);
      for (int index = 1; index <= 3; ++index)
        EModeNPCBehaviour.LoadDest(recolor, index - 1);
    }
  }
}
