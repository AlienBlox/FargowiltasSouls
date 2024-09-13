// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.SkeletronHead
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Bosses.Champions.Shadow;
using FargowiltasSouls.Content.Bosses.DeviBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
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
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class SkeletronHead : EModeNPCBehaviour
  {
    public int ReticleTarget;
    public int BabyGuardianTimer;
    public bool DGDaytime;
    public int DGSpeedRampup;
    public int MasoArmsTimer;
    public bool InPhase2;
    public bool DroppedSummon;
    public bool SpawnedArms;
    public bool HasSaidEndure;
    public bool FirstCycle;
    public int SpawnGrace;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(35);

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.damage = (int) ((double) npc.damage * 1.1499999761581421);
      npc.lifeMax = (int) ((double) npc.lifeMax * 0.800000011920929);
    }

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.ReticleTarget);
      binaryWriter.Write7BitEncodedInt(this.BabyGuardianTimer);
      binaryWriter.Write7BitEncodedInt(this.DGSpeedRampup);
      bitWriter.WriteBit(this.InPhase2);
      binaryWriter.Write7BitEncodedInt(this.SpawnGrace);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.ReticleTarget = binaryReader.Read7BitEncodedInt();
      this.BabyGuardianTimer = binaryReader.Read7BitEncodedInt();
      this.DGSpeedRampup = binaryReader.Read7BitEncodedInt();
      this.InPhase2 = bitReader.ReadBit();
      this.SpawnGrace = binaryReader.Read7BitEncodedInt();
    }

    public virtual void OnSpawn(NPC npc, IEntitySource source)
    {
      if (WorldSavingSystem.SwarmActive || !WorldSavingSystem.EternityMode)
        return;
      this.SpawnGrace = 240;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
    {
      return this.SpawnGrace <= 0 && base.CanHitPlayer(npc, target, ref cooldownSlot);
    }

    private static int BabyGuardianTimerRefresh(NPC npc)
    {
      return WorldSavingSystem.MasochistModeReal || !NPC.AnyNPCs(36) || (double) npc.life <= (double) npc.lifeMax * 0.25 ? 180 : 240;
    }

    private void GrowHands(NPC npc, bool secondSet = false)
    {
      for (int ai0 = -1; ai0 < 2; ai0 += 2)
      {
        int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 36, ((Entity) npc).whoAmI, (float) ai0, (float) ((Entity) npc).whoAmI, target: npc.target, velocity: new Vector2());
        NPC npc1 = Main.npc[index];
        if (secondSet && index != Main.maxNPCs)
          npc1.GetGlobalNPC<SkeletronHand>().secondSet = true;
        if ((double) npc.ai[1] == 1.0 || (double) npc.ai[1] == 2.0)
        {
          npc1.localAI[2] = (float) (((Entity) npc1).width + ((Entity) npc).width);
          npc1.GetGlobalNPC<SkeletronHand>().AttackTimer = 215;
        }
      }
      FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.RegrowArms", new Color(175, 75, (int) byte.MaxValue), (object) npc.FullName);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.skeleBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if (this.SpawnGrace > 0)
        --this.SpawnGrace;
      npc.HitSound = !SkeletronHead.ArmDR(npc) ? new SoundStyle?(SoundID.NPCHit2) : new SoundStyle?(SoundID.NPCHit4);
      if (!this.SpawnedArms && (double) npc.life < (double) npc.lifeMax * 0.5)
      {
        if (NPC.AnyNPCs(36))
        {
          npc.life = (int) Math.Round((double) npc.lifeMax * 0.5) + 10;
        }
        else
        {
          this.SpawnedArms = true;
          this.GrowHands(npc);
          if (WorldSavingSystem.MasochistModeReal)
            this.GrowHands(npc, true);
        }
      }
      if ((double) npc.ai[1] == 0.0)
      {
        if ((double) npc.ai[2] == 710.0 && FargoSoulsUtil.HostCheck && !WorldSavingSystem.MasochistModeReal)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<TargetingReticle>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
        if ((double) npc.ai[2] < 795.0)
          this.ReticleTarget = npc.target;
      }
      if ((double) npc.ai[1] == 1.0 || (double) npc.ai[1] == 2.0)
      {
        if (this.ReticleTarget > -1 && this.ReticleTarget < (int) byte.MaxValue)
        {
          int num = SkeletronHead.BabyGuardianTimerRefresh(npc);
          if (this.BabyGuardianTimer > num)
            this.BabyGuardianTimer = num;
          npc.target = this.ReticleTarget;
          this.ReticleTarget = -1;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          if (!npc.HasValidTarget)
            npc.TargetClosest(false);
          if ((double) npc.ai[1] == 1.0)
          {
            this.FirstCycle = true;
            this.CrossGuardianAttack(npc);
          }
        }
        float num1 = (float) npc.life / (float) npc.lifeMax;
        float num2 = 20f;
        if (!WorldSavingSystem.MasochistModeReal)
          num2 += 100f * num1;
        if ((double) ++npc.localAI[2] >= (double) num2)
        {
          npc.localAI[2] = 0.0f;
          if ((double) num2 > 0.0 && npc.HasPlayerTarget && FargoSoulsUtil.HostCheck && (!NPC.AnyNPCs(36) || (double) npc.ai[1] == 2.0))
          {
            Vector2 vector2_1 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center)), 6f);
            for (int index = 0; index < 8; ++index)
            {
              Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(vector2_1, Math.PI / 4.0 * (double) index, new Vector2()), Vector2.op_Multiply(((Entity) npc).velocity, 1f - num1));
              vector2_2.Y -= Math.Abs(vector2_2.X) * 0.2f;
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, ModContent.ProjectileType<SkeletronBone>(), npc.defDamage / 9 * 2, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        if ((double) npc.life < (double) npc.lifeMax * 0.75 && (double) npc.ai[1] == 1.0 && --this.BabyGuardianTimer < 0)
        {
          this.BabyGuardianTimer = SkeletronHead.BabyGuardianTimerRefresh(npc);
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            this.SprayHomingBabies(npc);
            if (WorldSavingSystem.MasochistModeReal && !NPC.AnyNPCs(36) && !this.FirstCycle)
              this.DungeonGuardianAttack(npc);
          }
          this.FirstCycle = false;
        }
      }
      else
      {
        if ((double) npc.ai[2] == 0.0)
        {
          npc.TargetClosest(false);
          npc.ai[2] = 1f;
        }
        if ((double) npc.life < (double) npc.lifeMax * 0.75 && --this.BabyGuardianTimer < 0)
        {
          this.BabyGuardianTimer = SkeletronHead.BabyGuardianTimerRefresh(npc);
          if (!WorldSavingSystem.MasochistModeReal)
            this.BabyGuardianTimer += 60;
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          for (int index1 = -1; index1 <= 1; ++index1)
          {
            if (index1 != 0)
            {
              float num3 = (float) (1.0 - (double) npc.life / (double) npc.lifeMax) * 1.33333337f;
              if ((double) num3 > 1.0 || WorldSavingSystem.MasochistModeReal)
                num3 = 1f;
              int num4 = (int) (14.0 * (double) num3);
              Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), (double) MathHelper.ToRadians(40f) * (double) index1, new Vector2());
              for (int index2 = 0; index2 < num4; ++index2)
              {
                if (FargoSoulsUtil.HostCheck)
                {
                  float num5 = (float) (1.0 + 9.0 * (double) index2 / 14.0);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(num5, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(10f) * (double) index1 * (double) index2, new Vector2())), ModContent.ProjectileType<SkeletronGuardian2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
          }
          if (FargoSoulsUtil.HostCheck)
          {
            float num = 10f;
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(num, ((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center)), ModContent.ProjectileType<SkeletronGuardian2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) npc.ai[1] == 2.0)
      {
        npc.defense = 9999;
        npc.damage = npc.defDamage * 15;
        if (!Main.dayTime && !WorldSavingSystem.MasochistModeReal && ++this.DGSpeedRampup < 120)
        {
          NPC npc1 = npc;
          ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Division(Vector2.op_Multiply(((Entity) npc).velocity, (float) (120 - this.DGSpeedRampup)), 120f));
        }
        if (Main.dayTime && !Main.remixWorld)
          npc.Transform(68);
      }
      EModeUtils.DropSummon(npc, "SuspiciousSkull", NPC.downedBoss3, ref this.DroppedSummon);
      return flag;
    }

    public override void SafePostAI(NPC npc)
    {
      if (!SkeletronHead.ArmDR(npc) && (double) npc.ai[1] != 2.0)
        npc.defense = 10;
      base.SafePostAI(npc);
    }

    private void CrossGuardianAttack(NPC npc)
    {
      if (!WorldSavingSystem.MasochistModeReal)
      {
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == ModContent.ProjectileType<SkeletronGuardian2>())
            Main.projectile[index].Kill();
        }
      }
      if ((double) npc.life < (double) npc.lifeMax * 0.75 && !WorldSavingSystem.MasochistModeReal || !FargoSoulsUtil.HostCheck)
        return;
      for (int index1 = 0; index1 < 4; ++index1)
      {
        for (int index2 = -2; index2 <= 2; ++index2)
        {
          Vector2 vector2_1;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_1).\u002Ector(1200f, (float) (80 * index2));
          Vector2 vector2_2 = Vector2.op_Multiply(-8f, Vector2.UnitX);
          vector2_1 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(vector2_1, Math.PI / 2.0 * ((double) index1 + 0.5), new Vector2()));
          Vector2 vector2_3 = Utils.RotatedBy(vector2_2, Math.PI / 2.0 * ((double) index1 + 0.5), new Vector2());
          int index3 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_3, ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index3 != Main.maxProjectiles)
            Main.projectile[index3].timeLeft = 151;
        }
      }
    }

    private void SprayHomingBabies(NPC npc)
    {
      float num1 = (float) (1.0 - (double) npc.life / (double) npc.lifeMax) * 1.33333337f;
      if ((double) num1 > 1.0 || WorldSavingSystem.MasochistModeReal)
        num1 = 1f;
      int num2 = (int) (30.0 * (double) num1);
      for (int index = 0; index < num2; ++index)
      {
        double num3 = (double) Utils.NextFloat(Main.rand, 3f, 9f);
        Vector2 vector2 = Vector2.op_Multiply((float) num3, Utils.RotatedBy(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), Math.PI * (Main.rand.NextDouble() - 0.5), new Vector2()));
        float num4 = (float) (num3 / (60.0 + (double) Utils.NextFloat(Main.rand, (float) (num2 * 2))));
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<SkeletronGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, num4, 0.0f);
      }
    }

    private void DungeonGuardianAttack(NPC npc)
    {
      switch (Main.rand.Next(4))
      {
        case 0:
          for (int index1 = 0; index1 < 4; ++index1)
          {
            for (int index2 = -2; index2 <= 2; ++index2)
            {
              Vector2 vector2_1;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_1).\u002Ector(1200f, (float) (80 * index2));
              Vector2 vector2_2 = Vector2.op_Multiply(-8f, Vector2.UnitX);
              vector2_1 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(vector2_1, Math.PI / 2.0 * (double) index1, new Vector2()));
              Vector2 vector2_3 = Utils.RotatedBy(vector2_2, Math.PI / 2.0 * (double) index1, new Vector2());
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_3, ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          break;
        case 1:
          Vector2 vector2_4 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
          for (int index3 = 0; index3 < 16; ++index3)
          {
            int index4 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1000f, Utils.RotatedBy(vector2_4, Math.PI / 8.0 * (double) index3, new Vector2()))), Vector2.op_Multiply(-8f, Utils.RotatedBy(vector2_4, Math.PI / 8.0 * (double) index3, new Vector2())), ModContent.ProjectileType<DeviGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            if (index4 != Main.maxProjectiles)
              Main.projectile[index4].light = 1f;
          }
          break;
        case 2:
          Vector2 vector2_5 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
          vector2_5.X += (float) Main.rand.Next(-20, 21);
          vector2_5.Y += (float) Main.rand.Next(-20, 21);
          ((Vector2) ref vector2_5).Normalize();
          vector2_5 = Vector2.op_Multiply(vector2_5, 3f);
          for (int index = 0; index < 6; ++index)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2_5, Math.PI / 3.0 * (double) index, new Vector2()), 270, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, -1f, 0.0f, 0.0f);
          break;
        case 3:
          this.CrossGuardianAttack(npc);
          break;
        default:
          this.SprayHomingBabies(npc);
          break;
      }
    }

    private static bool ArmDR(NPC npc)
    {
      return !WorldSavingSystem.SwarmActive && ((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == 36 && (double) n.ai[1] == (double) ((Entity) npc).whoAmI));
    }

    private static float GetDR(NPC npc)
    {
      if (!SkeletronHead.ArmDR(npc))
        return 1f;
      float lifePercent = npc.GetLifePercent();
      return npc.GetGlobalNPC<SkeletronHead>().SpawnedArms ? lifePercent : Math.Max(lifePercent - 0.5f, 0.0f);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, SkeletronHead.GetDR(npc));
    }

    public virtual void UpdateLifeRegen(NPC npc, ref int damage)
    {
      if (npc.lifeRegen >= 0)
        return;
      npc.lifeRegen = (int) ((double) npc.lifeRegen * (double) SkeletronHead.GetDR(npc));
    }

    public virtual bool CheckDead(NPC npc)
    {
      if ((double) npc.ai[1] == 2.0 || WorldSavingSystem.SwarmActive)
        return base.CheckDead(npc);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.life = npc.lifeMax / 176;
      if (npc.life < 50)
        npc.life = 50;
      npc.defense = 9999;
      npc.damage = npc.defDamage * 15;
      npc.ai[1] = 2f;
      npc.netUpdate = true;
      EModeNPCBehaviour.NetSync(npc);
      if (!this.HasSaidEndure)
      {
        this.HasSaidEndure = true;
        FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.GuardianForm", new Color(175, 75, (int) byte.MaxValue), (object) npc.FullName);
      }
      return false;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<LethargicBuff>(), 300, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 19);
      EModeNPCBehaviour.LoadGoreRange(recolor, 54, 57);
      EModeNPCBehaviour.LoadProjectile(recolor, 270);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.BoneArm, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm, "Arm_Bone");
    }
  }
}
