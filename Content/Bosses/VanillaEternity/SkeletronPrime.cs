// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.SkeletronPrime
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
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
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class SkeletronPrime : EModeNPCBehaviour
  {
    public int DungeonGuardianStartup;
    public int MemorizedTarget;
    public bool FullySpawnedLimbs;
    public bool HaveShotGuardians;
    public int ProjectileAttackTimer;
    public int RocketTimer;
    public bool DroppedSummon;
    public bool HasSaidEndure;
    public bool EndSpin;
    public int limbTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType((int) sbyte.MaxValue);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.DungeonGuardianStartup);
      binaryWriter.Write7BitEncodedInt(this.MemorizedTarget);
      binaryWriter.Write7BitEncodedInt(this.limbTimer);
      bitWriter.WriteBit(this.FullySpawnedLimbs);
      bitWriter.WriteBit(this.HaveShotGuardians);
      bitWriter.WriteBit(this.EndSpin);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.DungeonGuardianStartup = binaryReader.Read7BitEncodedInt();
      this.MemorizedTarget = binaryReader.Read7BitEncodedInt();
      this.limbTimer = binaryReader.Read7BitEncodedInt();
      this.FullySpawnedLimbs = bitReader.ReadBit();
      this.HaveShotGuardians = bitReader.ReadBit();
      this.EndSpin = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) ((double) npc.lifeMax * 1.2);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.primeBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if ((double) npc.ai[1] == 3.0 && npc.timeLeft > 60)
        npc.timeLeft = 60;
      if ((double) npc.ai[1] == 0.0)
      {
        this.HaveShotGuardians = false;
        if ((double) npc.ai[2] == 510.0 && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<TargetingReticle>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
        if ((double) npc.ai[2] < 595.0)
          this.MemorizedTarget = npc.target;
      }
      if ((double) npc.ai[1] == 1.0 && this.MemorizedTarget > -1 && this.MemorizedTarget < (int) byte.MaxValue)
      {
        npc.target = this.MemorizedTarget;
        npc.netUpdate = true;
        this.MemorizedTarget = -1;
        if (!npc.HasValidTarget)
          npc.TargetClosest(false);
      }
      if ((double) npc.ai[0] != 2.0 || WorldSavingSystem.MasochistModeReal)
      {
        if (!this.HaveShotGuardians && (double) npc.ai[1] == 1.0 && (double) npc.ai[2] > 2.0)
        {
          this.HaveShotGuardians = true;
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index1 = 0; index1 < 4; ++index1)
            {
              for (int index2 = -2; index2 <= 2; ++index2)
              {
                Vector2 vector2_1;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_1).\u002Ector(1200f, (float) (80 * index2));
                Vector2 vector2_2 = Vector2.op_Multiply(-10f, Vector2.UnitX);
                vector2_1 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.RotatedBy(vector2_1, Math.PI / 2.0 * (double) index1, new Vector2()));
                Vector2 vector2_3 = Utils.RotatedBy(vector2_2, Math.PI / 2.0 * (double) index1, new Vector2());
                int index3 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_3, ModContent.ProjectileType<PrimeGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (index3 != Main.maxProjectiles)
                  Main.projectile[index3].timeLeft = 121;
              }
            }
          }
        }
        if (++this.RocketTimer >= 360)
        {
          this.RocketTimer = 0;
          if (npc.HasPlayerTarget)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            vector2.X += (float) Main.rand.Next(-20, 21);
            vector2.Y += (float) Main.rand.Next(-20, 21);
            ((Vector2) ref vector2).Normalize();
            int num = FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage);
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(3f, vector2), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(5f), new Vector2())), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(-5f), new Vector2())), 303, num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          }
        }
      }
      if ((double) npc.ai[0] == 2.0)
      {
        npc.dontTakeDamage = false;
        if ((double) npc.ai[1] == 1.0 && (double) npc.ai[2] > 2.0)
        {
          this.EndSpin = true;
          if (++this.ProjectileAttackTimer > 90)
          {
            this.ProjectileAttackTimer = -30;
            int num1 = npc.defDamage / 3;
            if (FargoSoulsUtil.HostCheck)
            {
              float num2 = (float) npc.life / (float) npc.lifeMax;
              if (WorldSavingSystem.MasochistModeReal)
                num2 = 0.0f;
              int num3 = (int) (7.0 - 6.0 * (double) num2);
              for (int index4 = 0; index4 < 8; ++index4)
              {
                Vector2 vector2 = Vector2.op_Multiply(12f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), Math.PI / 4.0 * (double) index4, new Vector2()));
                for (int index5 = -num3; index5 <= num3; ++index5)
                {
                  int index6 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(2f) * (double) index5, new Vector2()), ModContent.ProjectileType<MechElectricOrb>(), num1, 0.0f, Main.myPlayer, -1f, 180f, 0.0f);
                  Main.projectile[index6].soundDelay = -1;
                  if (index6 != Main.maxProjectiles)
                    Main.projectile[index6].timeLeft = 300;
                }
              }
            }
          }
        }
        else if ((double) npc.ai[1] == 2.0)
        {
          while (npc.buffType[0] != 0)
          {
            npc.buffImmune[npc.buffType[0]] = true;
            npc.DelBuff(0);
          }
          if (!Main.dayTime)
            npc.rotation += 0.5235988f;
          if (!Main.dayTime && !WorldSavingSystem.MasochistModeReal)
          {
            NPC npc1 = npc;
            ((Entity) npc1).position = Vector2.op_Subtraction(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.1f));
            if (++this.DungeonGuardianStartup < 120)
            {
              NPC npc2 = npc;
              ((Entity) npc2).position = Vector2.op_Subtraction(((Entity) npc2).position, Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Multiply(((Entity) npc).velocity, (float) (120 - this.DungeonGuardianStartup)), 120f), 0.9f));
            }
          }
        }
        else
        {
          if (this.EndSpin)
          {
            this.EndSpin = false;
            if (npc.HasPlayerTarget)
            {
              float num = ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center);
              NPC npc3 = npc;
              ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, Math.Max((float) (1.0 - (double) num / 800.0), 0.5f));
              ((Entity) npc).velocity = Vector2.op_UnaryNegation(((Entity) npc).velocity);
            }
          }
          this.ProjectileAttackTimer = 0;
          NPC npc4 = npc;
          ((Entity) npc4).position = Vector2.op_Addition(((Entity) npc4).position, Vector2.op_Division(((Entity) npc).velocity, 4f));
        }
        if (!this.FullySpawnedLimbs && ((double) npc.life < (double) npc.lifeMax * 0.6 || WorldSavingSystem.MasochistModeReal) && (double) npc.ai[1] == 0.0 && (double) npc.ai[3] >= 0.0)
        {
          if (this.limbTimer == 0)
          {
            npc.ai[1] = 0.0f;
            npc.ai[2] = 508f;
            this.limbTimer = 0;
            npc.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.ForceRoar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if (!NPC.AnyNPCs(131))
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 131, ((Entity) npc).whoAmI, 1f, (float) ((Entity) npc).whoAmI, ai3: 150f, target: npc.target, velocity: new Vector2());
            if (!NPC.AnyNPCs(129))
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 129, ((Entity) npc).whoAmI, 1f, (float) ((Entity) npc).whoAmI, target: npc.target, velocity: new Vector2());
            if (!NPC.AnyNPCs(128))
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 128, ((Entity) npc).whoAmI, -1f, (float) ((Entity) npc).whoAmI, ai3: 150f, target: npc.target, velocity: new Vector2());
            if (!NPC.AnyNPCs(130))
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 130, ((Entity) npc).whoAmI, -1f, (float) ((Entity) npc).whoAmI, target: npc.target, velocity: new Vector2());
            FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.RegrowArms", new Color(175, 75, (int) byte.MaxValue), (object) npc.FullName);
            foreach (NPC npc5 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (l => ((Entity) l).active && (double) l.ai[1] == (double) ((Entity) npc).whoAmI)))
            {
              PrimeLimb primeLimb;
              if (npc5.TryGetGlobalNPC<PrimeLimb>(ref primeLimb) && primeLimb.NoContactDamageTimer < 180)
                primeLimb.NoContactDamageTimer = 180;
            }
          }
          ++this.limbTimer;
          if ((double) this.limbTimer == 60.0)
          {
            int[] limbs = new int[4]{ 128, 131, 129, 130 };
            foreach (NPC npc6 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (l => ((Entity) l).active && (double) l.ai[1] == (double) ((Entity) npc).whoAmI && ((IEnumerable<int>) limbs).Contains<int>(l.type))))
            {
              npc6.GetGlobalNPC<PrimeLimb>().IsSwipeLimb = true;
              npc6.ai[2] = 0.0f;
              int num = (npc6.lifeMax - npc6.life) / 2;
              npc6.life += num;
              if (num > 0)
                npc6.HealEffect(num, true);
              npc6.dontTakeDamage = false;
              npc6.netUpdate = true;
              EModeNPCBehaviour.NetSync(npc6);
            }
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 131, ((Entity) npc).whoAmI, -1f, (float) ((Entity) npc).whoAmI, ai3: 150f, target: npc.target, velocity: new Vector2());
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 129, ((Entity) npc).whoAmI, -1f, (float) ((Entity) npc).whoAmI, target: npc.target, velocity: new Vector2());
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 128, ((Entity) npc).whoAmI, 1f, (float) ((Entity) npc).whoAmI, ai3: 150f, target: npc.target, velocity: new Vector2());
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, 130, ((Entity) npc).whoAmI, 1f, (float) ((Entity) npc).whoAmI, target: npc.target, velocity: new Vector2());
          }
          else if (this.limbTimer >= 180)
          {
            this.FullySpawnedLimbs = true;
            this.limbTimer = -1;
            npc.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              int num4 = Utils.NextBool(Main.rand) ? 128 : 131;
              int num5 = Utils.NextBool(Main.rand) ? 129 : 130;
              int[] limbs = new int[4]{ 128, 131, 129, 130 };
              foreach (NPC npc7 in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (l => ((Entity) l).active && (double) l.ai[1] == (double) ((Entity) npc).whoAmI && ((IEnumerable<int>) limbs).Contains<int>(l.type) && !l.GetGlobalNPC<PrimeLimb>().IsSwipeLimb)))
              {
                npc7.GetGlobalNPC<PrimeLimb>().RangedAttackMode = npc.type == num4 || npc.type == num5;
                int lifeMax = npc7.lifeMax;
                npc7.life = Math.Min(npc7.life + npc7.lifeMax / 2, npc7.lifeMax);
                int num6 = lifeMax - npc7.life;
                if (num6 > 0)
                  npc7.HealEffect(num6, true);
                npc7.dontTakeDamage = false;
                npc7.netUpdate = true;
                EModeNPCBehaviour.NetSync(npc7);
              }
            }
          }
        }
      }
      float num7 = ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center);
      if (npc.HasValidTarget && (double) num7 < 300.0 && ((double) npc.ai[1] != 1.0 || (double) npc.ai[2] <= 2.0 || (double) npc.ai[1] == 2.0))
      {
        float num8 = (float) (1.0 * (1.0 - (double) num7 / 300.0));
        NPC npc8 = npc;
        ((Entity) npc8).velocity = Vector2.op_Subtraction(((Entity) npc8).velocity, Vector2.op_Multiply(num8, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)));
      }
      if (npc.HasValidTarget && (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 900.0)
      {
        NPC npc9 = npc;
        ((Entity) npc9).velocity = Vector2.op_Addition(((Entity) npc9).velocity, Vector2.op_Multiply(0.1f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)));
      }
      EModeUtils.DropSummon(npc, "MechSkull", NPC.downedMechBoss3, ref this.DroppedSummon, Main.hardMode);
      return flag;
    }

    public override void SafePostAI(NPC npc)
    {
      if ((double) npc.ai[0] == 2.0)
        return;
      float num = WorldSavingSystem.MasochistModeReal ? 0.8f : 1f;
      if ((double) npc.life > (double) npc.lifeMax * (double) num)
        return;
      npc.ai[0] = 2f;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 480, true, false);
      target.AddBuff(ModContent.BuffType<NanoInjectionBuff>(), 360, true, false);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if ((double) npc.ai[1] == 2.0 || WorldSavingSystem.SwarmActive)
        return base.CheckDead(npc);
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      npc.life = npc.lifeMax / 630;
      if (npc.life < 100)
        npc.life = 100;
      npc.defDefense = 9999;
      npc.defense = 9999;
      npc.defDamage *= 13;
      npc.damage *= 13;
      npc.ai[1] = 2f;
      npc.netUpdate = true;
      if (!this.HasSaidEndure)
      {
        this.HasSaidEndure = true;
        FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.GuardianForm", new Color(175, 75, (int) byte.MaxValue), (object) npc.FullName);
      }
      if (!WorldSavingSystem.MasochistModeReal)
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 128 || Main.npc[index].type == 131 || Main.npc[index].type == 129 || Main.npc[index].type == 130) && (double) Main.npc[index].ai[1] == (double) ((Entity) npc).whoAmI)
          {
            Main.npc[index].life = 0;
            Main.npc[index].HitEffect(0, 10.0, new bool?());
            Main.npc[index].checkDead();
          }
        }
      }
      return false;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 18);
      EModeNPCBehaviour.LoadGoreRange(recolor, 147, 150);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.BoneArm2, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneArm2, "Arm_Bone_2");
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.BoneLaser, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneLaser, "Bone_Laser");
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.BoneEyes, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.BoneEyes, "Bone_Eyes");
    }
  }
}
