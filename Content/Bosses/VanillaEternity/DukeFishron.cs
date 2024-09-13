// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.DukeFishron
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.NPCMatching;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class DukeFishron : EModeNPCBehaviour
  {
    public int GeneralTimer;
    public int P3Timer;
    public int EXTornadoTimer;
    public bool RemovedInvincibility;
    public bool TakeNoDamageOnHit;
    public bool IsEX;
    public bool SpectralFishronRandom;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(370);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.GeneralTimer);
      binaryWriter.Write7BitEncodedInt(this.P3Timer);
      binaryWriter.Write7BitEncodedInt(this.EXTornadoTimer);
      bitWriter.WriteBit(this.RemovedInvincibility);
      bitWriter.WriteBit(this.TakeNoDamageOnHit);
      bitWriter.WriteBit(this.IsEX);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.GeneralTimer = binaryReader.Read7BitEncodedInt();
      this.P3Timer = binaryReader.Read7BitEncodedInt();
      this.EXTornadoTimer = binaryReader.Read7BitEncodedInt();
      this.RemovedInvincibility = bitReader.ReadBit();
      this.TakeNoDamageOnHit = bitReader.ReadBit();
      this.IsEX = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      if (EModeGlobalNPC.spawnFishronEX)
      {
        this.IsEX = true;
        npc.damage *= 3;
        npc.defense *= 30;
      }
      if (!EModeGlobalNPC.spawnFishronEX && !Main.getGoodWorld)
        return;
      npc.GivenName = Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DukeFishronEX.DisplayName");
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
      if (this.IsEX)
      {
        npc.buffImmune[ModContent.BuffType<GodEaterBuff>()] = true;
        npc.buffImmune[ModContent.BuffType<SadismBuff>()] = true;
        npc.buffImmune[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
        npc.buffImmune[ModContent.BuffType<LightningRodBuff>()] = true;
        npc.defDamage = (int) ((double) npc.defDamage * 1.5);
        npc.defDefense *= 2;
        npc.buffImmune[ModContent.BuffType<FlamesoftheUniverseBuff>()] = true;
        npc.buffImmune[ModContent.BuffType<LightningRodBuff>()] = true;
      }
      if (!this.IsEX && !Main.getGoodWorld)
        return;
      npc.GivenName = Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DukeFishronEX.DisplayName");
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag1 = base.SafePreAI(npc);
      EModeGlobalNPC.fishBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag1;
      bool flag2 = true;
      if (this.IsEX || Main.getGoodWorld)
      {
        npc.FargoSouls().MutantNibble = false;
        npc.FargoSouls().LifePrevious = int.MaxValue;
        while (npc.buffType[0] != 0)
          npc.DelBuff(0);
        if ((double) ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0)
        {
          Main.LocalPlayer.AddBuff(ModContent.BuffType<OceanicSealBuff>(), 2, true, false);
          Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2, true, false);
        }
        if (this.IsEX)
          EModeGlobalNPC.fishBossEX = ((Entity) npc).whoAmI;
        else
          flag2 = false;
        NPC npc1 = npc;
        ((Entity) npc1).position = Vector2.op_Addition(((Entity) npc1).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f));
        switch (npc.ai[0])
        {
          case -1f:
            if ((double) npc.ai[2] == 2.0 && FargoSoulsUtil.HostCheck)
            {
              if (Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FishronRitual>(), 0, 0.0f, Main.myPlayer, (float) npc.lifeMax, (float) ((Entity) npc).whoAmI, (float) (!this.IsEX ? 1 : 0)) == Main.maxProjectiles)
                ((Entity) npc).active = false;
              SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            }
            this.TakeNoDamageOnHit = true;
            break;
          case 0.0f:
            if (!this.RemovedInvincibility)
              npc.dontTakeDamage = false;
            this.TakeNoDamageOnHit = false;
            ++npc.ai[2];
            break;
          case 1f:
          case 6f:
            ++this.GeneralTimer;
            if (this.GeneralTimer > 5)
            {
              this.GeneralTimer = 0;
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 spawnPos;
                // ISSUE: explicit constructor call
                ((Vector2) ref spawnPos).\u002Ector(((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height));
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), spawnPos, 371, velocity: new Vector2());
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center));
                break;
              }
              break;
            }
            break;
          case 2f:
            if ((double) npc.ai[2] == 0.0 && FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 385, 0, 0.0f, Main.myPlayer, 1f, (float) (npc.target + 1), 0.0f);
              break;
            }
            break;
          case 3f:
            if ((double) npc.ai[2] == 60.0 && FargoSoulsUtil.HostCheck)
            {
              float num = 0.196349546f;
              for (int index = 0; index < 32; ++index)
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Vector2.Normalize(Utils.RotatedBy(Vector2.UnitY, (double) num * (double) index, new Vector2())));
              SpawnRazorbladeRing(18, 10f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 1f);
              break;
            }
            break;
          case 4f:
            this.RemovedInvincibility = false;
            this.TakeNoDamageOnHit = true;
            if ((double) npc.ai[2] == 1.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FishronRitual>(), 0, 0.0f, Main.myPlayer, (float) (npc.lifeMax / 4), (float) ((Entity) npc).whoAmI, 0.0f);
            if ((double) npc.ai[2] >= 114.0)
            {
              ++this.GeneralTimer;
              if (this.GeneralTimer > 6)
              {
                this.GeneralTimer = 0;
                int num1 = (int) ((double) npc.lifeMax * (double) Utils.NextFloat(Main.rand, 0.1f, 0.12f));
                npc.life += num1;
                int num2 = (double) npc.ai[0] == 9.0 ? npc.lifeMax / 2 : npc.lifeMax;
                if (npc.life > num2)
                  npc.life = num2;
                CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, num1, false, false);
                break;
              }
              break;
            }
            break;
          case 5f:
            if (!this.RemovedInvincibility)
              npc.dontTakeDamage = false;
            this.TakeNoDamageOnHit = false;
            ++npc.ai[2];
            break;
          case 7f:
            NPC npc2 = npc;
            ((Entity) npc2).position = Vector2.op_Subtraction(((Entity) npc2).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f));
            ++this.GeneralTimer;
            if (this.GeneralTimer > 1 && FargoSoulsUtil.HostCheck)
            {
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, Math.PI / 2.0, new Vector2())));
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, -1.0 * Math.PI / 2.0, new Vector2())));
              break;
            }
            break;
          case 8f:
            if (FargoSoulsUtil.HostCheck && (double) npc.ai[2] == 60.0)
            {
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) npc).direction), (double) npc.rotation, new Vector2()), (float) ((Entity) npc).width + 20f), 2f), ((Entity) npc).Center);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, (float) ((Entity) npc).direction * 2f, 8f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, (float) ((Entity) npc).direction * -2f, 8f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, 0.0f, 2f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              SpawnRazorbladeRing(12, 12.5f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 0.75f);
              SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), -2f);
              break;
            }
            break;
          case 9f:
            if ((double) npc.ai[2] == 1.0)
            {
              for (int index = 0; index < npc.buffImmune.Length; ++index)
                npc.buffImmune[index] = true;
              while (npc.buffTime[0] != 0)
                npc.DelBuff(0);
              npc.defDamage = (int) ((double) npc.defDamage * 1.2000000476837158);
              goto case 4f;
            }
            else
              goto case 4f;
          case 10f:
            this.TakeNoDamageOnHit = false;
            break;
          case 11f:
            if (this.GeneralTimer > 2)
              this.GeneralTimer = 2;
            if (this.GeneralTimer == 2 && FargoSoulsUtil.HostCheck)
            {
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, Math.PI / 2.0, new Vector2())));
              FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, this.IsEX ? ModContent.NPCType<DetonatingBubbleEX>() : 371, velocity: Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, -1.0 * Math.PI / 2.0, new Vector2())));
              goto case 10f;
            }
            else
              goto case 10f;
          case 12f:
            if ((double) npc.ai[2] == 15.0)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                SpawnRazorbladeRing(5, 9f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), 1f, true);
                SpawnRazorbladeRing(5, 9f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.6666667f), -0.5f, true);
                goto case 10f;
              }
              else
                goto case 10f;
            }
            else if ((double) npc.ai[2] == 16.0 && FargoSoulsUtil.HostCheck)
            {
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) npc).direction), (double) npc.rotation, new Vector2()), (float) ((Entity) npc).width + 20f), 2f), ((Entity) npc).Center);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, (float) ((Entity) npc).direction * 2f, 8f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, (float) ((Entity) npc).direction * -2f, 8f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              float num = 0.2617994f;
              for (int index = 0; index < 24; ++index)
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, ModContent.NPCType<DetonatingBubbleEX>(), velocity: Vector2.Normalize(Utils.RotatedBy(((Entity) npc).velocity, (double) num * (double) index, new Vector2())));
              goto case 10f;
            }
            else
              goto case 10f;
        }
      }
      if (flag2)
      {
        NPC npc3 = npc;
        ((Entity) npc3).position = Vector2.op_Addition(((Entity) npc3).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.25f));
        switch (npc.ai[0])
        {
          case -1f:
            if (!this.IsEX)
            {
              npc.dontTakeDamage = true;
              break;
            }
            break;
          case 0.0f:
            if (!this.RemovedInvincibility)
              npc.dontTakeDamage = false;
            if (!Main.player[npc.target].ZoneBeach)
            {
              ++npc.ai[2];
              break;
            }
            break;
          case 1f:
          case 6f:
            if (++this.GeneralTimer > 5)
            {
              this.GeneralTimer = 0;
              if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
              {
                Vector2 spawnPos;
                // ISSUE: explicit constructor call
                ((Vector2) ref spawnPos).\u002Ector(((Entity) npc).position.X + (float) Main.rand.Next(((Entity) npc).width), ((Entity) npc).position.Y + (float) Main.rand.Next(((Entity) npc).height));
                FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), spawnPos, 371, velocity: new Vector2());
                break;
              }
              break;
            }
            break;
          case 2f:
            if ((double) npc.ai[2] == 0.0 && FargoSoulsUtil.HostCheck)
            {
              bool flag3 = Utils.NextBool(Main.rand);
              for (int index = -1; index <= 1; ++index)
              {
                if (index != 0)
                {
                  Vector2 vector2 = flag3 ? Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, -450f), (float) index) : Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, 600f), (float) index);
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, vector2.X, vector2.Y, 0.0f);
                }
              }
              break;
            }
            break;
          case 3f:
            if ((double) npc.ai[2] == 60.0 && FargoSoulsUtil.HostCheck)
            {
              SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 1f);
              break;
            }
            break;
          case 4f:
            if (!this.IsEX)
            {
              npc.dontTakeDamage = true;
              this.RemovedInvincibility = false;
              if ((double) npc.ai[2] == 120.0)
              {
                int num = npc.lifeMax - npc.life;
                npc.life = npc.lifeMax;
                CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, num, false, false);
                break;
              }
              break;
            }
            break;
          case 5f:
            if (!this.RemovedInvincibility)
              npc.dontTakeDamage = false;
            if (!Main.player[npc.target].ZoneBeach)
            {
              ++npc.ai[2];
              break;
            }
            break;
          case 7f:
            NPC npc4 = npc;
            ((Entity) npc4).position = Vector2.op_Subtraction(((Entity) npc4).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.25f));
            if (++this.GeneralTimer > 1)
            {
              this.GeneralTimer = 0;
              if (FargoSoulsUtil.HostCheck)
              {
                if (WorldSavingSystem.MasochistModeReal)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) npc).velocity), Math.PI / 2.0, new Vector2()), ModContent.ProjectileType<RazorbladeTyphoon2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.03f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(0.014035f, Utils.RotatedBy(Vector2.Normalize(((Entity) npc).velocity), -1.0 * Math.PI / 2.0, new Vector2())), ModContent.ProjectileType<RazorbladeTyphoon2>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.08f, 0.0f, 0.0f);
                break;
              }
              break;
            }
            break;
          case 8f:
            if ((double) npc.ai[2] == 0.0)
              this.SpectralFishronRandom = Utils.NextBool(Main.rand);
            if ((double) npc.ai[2] >= 60.0 && (double) npc.ai[2] % 3.0 == 0.0 && (double) npc.ai[2] <= 66.0)
            {
              for (int index1 = -1; index1 <= 1; index1 += 2)
              {
                int num = (int) ((double) npc.ai[2] - 60.0) / 3;
                for (int index2 = -num; index2 <= num; ++index2)
                {
                  if (Math.Abs(index2) == num)
                  {
                    Vector2 vector2 = this.SpectralFishronRandom ? Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, 0.1745329350233078 * (double) index2, new Vector2()), -500f), (float) index1) : Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, 0.1745329350233078 * (double) index2, new Vector2()), 500f), (float) index1);
                    if (FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, vector2.X, vector2.Y, 0.0f);
                  }
                }
              }
            }
            if ((double) npc.ai[2] == 60.0 && FargoSoulsUtil.HostCheck && WorldSavingSystem.MasochistModeReal)
            {
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) npc).direction), (double) npc.rotation, new Vector2()), (float) ((Entity) npc).width + 20f), 2f), ((Entity) npc).Center);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2.X, vector2.Y, 0.0f, 8f, 385, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              SpawnRazorbladeRing(12, 12.5f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.75f);
              SpawnRazorbladeRing(12, 10f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 2f * (float) ((Entity) npc).direction);
              break;
            }
            break;
          case 9f:
            if (!this.IsEX)
            {
              npc.dontTakeDamage = true;
              this.RemovedInvincibility = false;
              if ((double) npc.ai[2] == 90.0 && FargoSoulsUtil.HostCheck)
              {
                int num = ModContent.ProjectileType<RazorbladeTyphoon2>();
                for (int index = 0; index < Main.maxProjectiles; ++index)
                {
                  if (((Entity) Main.projectile[index]).active && (Main.projectile[index].type == 385 || Main.projectile[index].type == num))
                    Main.projectile[index].Kill();
                }
              }
              if ((double) npc.ai[2] == 120.0)
              {
                int num3 = WorldSavingSystem.MasochistModeReal ? npc.lifeMax / 2 : npc.lifeMax / 3;
                int num4 = num3 - npc.life;
                npc.life = num3;
                CombatText.NewText(((Entity) npc).Hitbox, CombatText.HealLife, num4, false, false);
                if (FargoSoulsUtil.HostCheck)
                {
                  for (int index = 0; index < Main.maxProjectiles; ++index)
                  {
                    if (((Entity) Main.projectile[index]).active && (Main.projectile[index].type == 384 || Main.projectile[index].type == 386))
                      Main.projectile[index].Kill();
                  }
                  for (int index = 0; index < Main.maxNPCs; ++index)
                  {
                    if (((Entity) Main.npc[index]).active && (Main.npc[index].type == 372 || Main.npc[index].type == 373))
                    {
                      Main.npc[index].life = 0;
                      Main.npc[index].HitEffect(0, 10.0, new bool?());
                      ((Entity) Main.npc[index]).active = false;
                      if (Main.netMode == 2)
                        NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                  }
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 10f:
            if (!Main.player[npc.target].ZoneBeach || (double) npc.ai[3] > 5.0 && (double) npc.ai[3] < 8.0)
            {
              NPC npc5 = npc;
              ((Entity) npc5).position = Vector2.op_Addition(((Entity) npc5).position, ((Entity) npc).velocity);
              ++npc.ai[2];
              EnrageDust();
            }
            if ((double) npc.ai[3] == 1.0)
            {
              if (this.P3Timer == 0)
                this.SpectralFishronRandom = Utils.NextBool(Main.rand);
              if (++this.P3Timer < 150)
              {
                npc.ai[2] = 0.0f;
                ((Entity) npc).position.Y -= ((Entity) npc).velocity.Y * 0.5f;
                Checks(0);
                break;
              }
              break;
            }
            if ((double) npc.ai[3] == 5.0)
            {
              if ((double) npc.ai[2] == 0.0)
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              npc.ai[2] -= 0.5f;
              NPC npc6 = npc;
              ((Entity) npc6).velocity = Vector2.op_Multiply(((Entity) npc6).velocity, 0.5f);
              EnrageDust();
              break;
            }
            break;
          case 11f:
            if (!Main.player[npc.target].ZoneBeach || (double) npc.ai[3] >= 5.0)
            {
              if ((double) npc.ai[2] == 0.0 && !Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Monster70", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              }
              if (Main.player[npc.target].ZoneBeach)
              {
                NPC npc7 = npc;
                ((Entity) npc7).position = Vector2.op_Addition(((Entity) npc7).position, Vector2.op_Multiply(((Entity) npc).velocity, 0.5f));
              }
              else
              {
                NPC npc8 = npc;
                ((Entity) npc8).position = Vector2.op_Addition(((Entity) npc8).position, ((Entity) npc).velocity);
                ++npc.ai[2];
                int num = (int) ((Entity) Main.player[npc.target]).Center.X / 16;
                if ((num < 500 ? 1 : (num > Main.maxTilesX - 500 ? 1 : 0)) == 0)
                  this.EXTornadoTimer -= 2;
              }
              EnrageDust();
            }
            this.P3Timer = 0;
            if (--this.GeneralTimer < 0)
            {
              this.GeneralTimer = 2;
              if (FargoSoulsUtil.HostCheck)
              {
                if ((double) npc.ai[3] == 2.0 || (double) npc.ai[3] == 3.0)
                {
                  for (int index3 = -1; index3 <= 1; index3 += 2)
                  {
                    for (int index4 = 1; index4 <= 2; ++index4)
                      FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, ModContent.NPCType<DetonatingBubbleNPC>(), velocity: Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) npc).velocity), Math.PI / 2.0 * (double) index3, new Vector2()), (float) index4), 0.5f));
                  }
                }
                if (!Main.player[npc.target].ZoneBeach)
                {
                  float radians = MathHelper.ToRadians(Utils.NextFloat(Main.rand, 1f, 15f));
                  for (int index5 = -1; index5 <= 1; ++index5)
                  {
                    int index6 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(8f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center), (double) radians * (double) index5, new Vector2())), ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    if (index6 != Main.maxProjectiles)
                      Main.projectile[index6].timeLeft = 90;
                  }
                  for (int index7 = -1; index7 <= 1; index7 += 2)
                  {
                    int index8 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(8f, Utils.RotatedBy(Vector2.Normalize(((Entity) npc).velocity), Math.PI / 2.0 * (double) index7, new Vector2())), ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    if (index8 != Main.maxProjectiles)
                      Main.projectile[index8].timeLeft = 90;
                  }
                  break;
                }
                if (WorldSavingSystem.MasochistModeReal)
                {
                  for (int index = -1; index <= 1; index += 2)
                    FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, ModContent.NPCType<DetonatingBubbleNPC>(), velocity: Vector2.op_Multiply(1.5f, Utils.RotatedBy(Vector2.Normalize(((Entity) npc).velocity), Math.PI / 2.0 * (double) index, new Vector2())));
                  break;
                }
                break;
              }
              break;
            }
            break;
          case 12f:
            if (!Main.player[npc.target].ZoneBeach || (double) npc.ai[3] > 5.0 && (double) npc.ai[3] < 8.0)
            {
              if (!Main.player[npc.target].ZoneBeach)
              {
                NPC npc9 = npc;
                ((Entity) npc9).position = Vector2.op_Addition(((Entity) npc9).position, ((Entity) npc).velocity);
              }
              ++npc.ai[2];
              EnrageDust();
            }
            this.GeneralTimer = 0;
            if ((double) npc.ai[2] == 15.0)
            {
              SpawnRazorbladeRing(6, 8f, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), -0.75f);
              break;
            }
            if ((double) npc.ai[2] == 16.0)
            {
              for (int index = -5; index <= 5; ++index)
              {
                Vector2 vector2 = Utils.RotatedBy(((Entity) npc).DirectionFrom(((Entity) Main.player[npc.target]).Center), 0.31415927410125732 * (double) index, new Vector2());
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<FishronBubble>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              break;
            }
            break;
        }
      }
      if (WorldSavingSystem.MasochistModeReal || EModeGlobalNPC.fishBossEX == ((Entity) npc).whoAmI)
        --this.EXTornadoTimer;
      if (this.EXTornadoTimer < 0)
      {
        this.EXTornadoTimer = 600;
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        for (int index9 = -1; index9 <= 1; index9 += 2)
        {
          int num5 = (int) ((Entity) Main.player[npc.target]).Center.X / 16;
          int num6 = (int) ((Entity) Main.player[npc.target]).Center.Y / 16;
          int num7 = num5 + 75 * index9;
          if (num7 >= 0 && num7 < Main.maxTilesX && num6 >= 0 && num6 < Main.maxTilesY)
          {
            Tile tile;
            do
            {
              tile = ((Tilemap) ref Main.tile)[num7, num6];
              if (((Tile) ref tile).HasUnactuatedTile)
              {
                bool[] tileSolid = Main.tileSolid;
                tile = ((Tilemap) ref Main.tile)[num7, num6];
                int index10 = (int) ((Tile) ref tile).TileType;
                if (tileSolid[index10])
                  --num6;
                else
                  break;
              }
              else
                break;
            }
            while (num7 >= 0 && num7 < Main.maxTilesX && num6 >= 0 && num6 < Main.maxTilesY);
            int num8 = num6 - 1;
            int num9 = 0;
            do
            {
              tile = ((Tilemap) ref Main.tile)[num7, num8];
              if (((Tile) ref tile).HasUnactuatedTile)
                goto label_196;
label_192:
              ++num8;
              if (num7 >= 0 && num7 < Main.maxTilesX && num8 >= 0 && num8 < Main.maxTilesY)
                continue;
              goto label_197;
label_196:
              bool[] tileSolidTop = Main.tileSolidTop;
              tile = ((Tilemap) ref Main.tile)[num7, num8];
              int index11 = (int) ((Tile) ref tile).TileType;
              if (!tileSolidTop[index11])
                goto label_192;
              else
                goto label_197;
            }
            while (++num9 <= 32);
            num8 -= 28;
label_197:
            int num10 = num8 - 1;
            Vector2 vector2;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((float) (num7 * 16 + 8), (float) (num10 * 16 + 8));
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) -index9), 6f), 386, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 10f, 25f, 0.0f);
          }
        }
      }
      EModeUtils.DropSummon(npc, "TruffleWorm2", NPC.downedFishron, ref this.DroppedSummon);
      return flag1;

      void SpawnRazorbladeRing(
        int max,
        float speed,
        int damage,
        float rotationModifier,
        bool reduceTimeleft = false)
      {
        if (Main.netMode == 1)
          return;
        float num1 = 6.28318548f / (float) max;
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
        ((Vector2) ref vector2_1).Normalize();
        Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, speed);
        int num2 = ModContent.ProjectileType<RazorbladeTyphoon>();
        for (int index1 = 0; index1 < max; ++index1)
        {
          vector2_2 = Utils.RotatedBy(vector2_2, (double) num1, new Vector2());
          int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2_2, num2, damage, 0.0f, Main.myPlayer, rotationModifier * (float) npc.spriteDirection, speed, 0.0f);
          if (reduceTimeleft && index2 < 1000)
            Main.projectile[index2].timeLeft /= 2;
        }
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
      }

      void EnrageDust()
      {
        int num = 7;
        for (int index1 = 0; index1 < num; ++index1)
        {
          int index2;
          if ((double) ((Vector2) ref ((Entity) npc).velocity).Length() > 10.0)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) npc).velocity), new Vector2((float) (((Entity) npc).width + 50) / 2f, (float) ((Entity) npc).height)), 0.75f), (double) (index1 - (num / 2 - 1)) * Math.PI / (double) num, new Vector2()), ((Entity) npc).Center);
            Vector2 vector2_2 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
            Vector2 vector2_3 = vector2_2;
            index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_3), 0, 0, 88, vector2_2.X * 2f, vector2_2.Y * 2f, 0, new Color(), 1.7f);
          }
          else
            index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 88, ((Entity) npc).velocity.X * 2f, ((Entity) npc).velocity.Y * 2f, 0, new Color(), 1.7f);
          Main.dust[index2].noGravity = true;
          Dust dust1 = Main.dust[index2];
          dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
          Dust dust2 = Main.dust[index2];
          dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) npc).velocity);
        }
      }

      void Checks(int delay)
      {
        int num1 = WorldSavingSystem.MasochistModeReal ? 5 : 4;
        int num2 = this.P3Timer - 30;
        if (num2 < delay || num2 >= 3 * num1 + delay || num2 % 3 != 0 || !FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2 = Vector2.op_Multiply(450f, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 6.2831854820251465 / (double) num1 * ((double) (num2 / 3) + (double) Utils.NextFloat(Main.rand, 0.5f)), new Vector2())));
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<FishronFishron>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, vector2.X, vector2.Y, 0.0f);
      }
    }

    public override void SafePostAI(NPC npc)
    {
      base.SafePostAI(npc);
      if ((double) npc.ai[0] > 9.0)
      {
        npc.dontTakeDamage = false;
        npc.chaseable = true;
      }
      npc.defense = Math.Max(npc.defense, npc.defDefense);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<MutantNibbleBuff>(), 600, true, false);
      target.AddBuff(148, 3600, true, false);
      target.FargoSouls().MaxLifeReduction += 50;
      target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 1200, true, false);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (this.TakeNoDamageOnHit)
        modifiers.Null();
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive)
        return base.CheckDead(npc);
      if ((double) npc.ai[0] <= 9.0)
      {
        npc.life = 1;
        ((Entity) npc).active = true;
        if (FargoSoulsUtil.HostCheck)
        {
          npc.netUpdate = true;
          npc.dontTakeDamage = true;
          this.RemovedInvincibility = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        for (int index1 = 0; index1 < 100; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
          Main.dust[index2].position.X += (float) Main.rand.Next(-20, 21);
          Main.dust[index2].position.Y += (float) Main.rand.Next(-20, 21);
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.5f);
          Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(50) * 0.0099999997764825821);
          if (Utils.NextBool(Main.rand))
          {
            Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(50) * 0.0099999997764825821);
            Main.dust[index2].noGravity = true;
          }
        }
        for (int index3 = 0; index3 < 5; ++index3)
        {
          int index4 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).position, new Vector2((float) Main.rand.Next(((Entity) npc).width), (float) Main.rand.Next(((Entity) npc).height))), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index4].scale = 2f;
          Main.gore[index4].velocity.X = (float) Main.rand.Next(-50, 51) * 0.01f;
          Main.gore[index4].velocity.Y = (float) Main.rand.Next(-50, 51) * 0.01f;
          Gore gore1 = Main.gore[index4];
          gore1.velocity = Vector2.op_Multiply(gore1.velocity, 0.5f);
          int index5 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).position, new Vector2((float) Main.rand.Next(((Entity) npc).width), (float) Main.rand.Next(((Entity) npc).height))), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index5].scale = 2f;
          Main.gore[index5].velocity.X = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Main.gore[index5].velocity.Y = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Gore gore2 = Main.gore[index5];
          gore2.velocity = Vector2.op_Multiply(gore2.velocity, 0.5f);
          int index6 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).position, new Vector2((float) Main.rand.Next(((Entity) npc).width), (float) Main.rand.Next(((Entity) npc).height))), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index6].scale = 2f;
          Main.gore[index6].velocity.X = (float) (-1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Main.gore[index6].velocity.Y = (float) (1.5 + (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Gore gore3 = Main.gore[index6];
          gore3.velocity = Vector2.op_Multiply(gore3.velocity, 0.5f);
          int index7 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).position, new Vector2((float) Main.rand.Next(((Entity) npc).width), (float) Main.rand.Next(((Entity) npc).height))), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index7].scale = 2f;
          Main.gore[index7].velocity.X = (float) (1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Main.gore[index7].velocity.Y = (float) ((double) Main.rand.Next(-50, 51) * 0.0099999997764825821 - 1.5);
          Gore gore4 = Main.gore[index7];
          gore4.velocity = Vector2.op_Multiply(gore4.velocity, 0.5f);
          int index8 = Gore.NewGore(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).position, new Vector2((float) Main.rand.Next(((Entity) npc).width), (float) Main.rand.Next(((Entity) npc).height))), new Vector2(), Main.rand.Next(61, 64), 1f);
          Main.gore[index8].scale = 2f;
          Main.gore[index8].velocity.X = (float) (-1.5 - (double) Main.rand.Next(-50, 51) * 0.0099999997764825821);
          Main.gore[index8].velocity.Y = (float) ((double) Main.rand.Next(-50, 51) * 0.0099999997764825821 - 1.5);
          Gore gore5 = Main.gore[index8];
          gore5.velocity = Vector2.op_Multiply(gore5.velocity, 0.5f);
        }
        return false;
      }
      if (EModeGlobalNPC.fishBossEX == ((Entity) npc).whoAmI)
        WorldSavingSystem.DownedFishronEX = true;
      return base.CheckDead(npc);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 4);
      EModeNPCBehaviour.LoadGoreRange(recolor, 573, 579);
    }
  }
}
