// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Deerclops
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
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
  public class Deerclops : EModeNPCBehaviour
  {
    public int BerserkSpeedupTimer;
    public int TeleportTimer;
    public int WalkingSpeedUpTimer;
    public bool EnteredPhase2;
    public bool EnteredPhase3;
    public bool DoLaserAttack;
    public bool DroppedSummon;
    public int ForceDespawnTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(668);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.BerserkSpeedupTimer);
      binaryWriter.Write7BitEncodedInt(this.TeleportTimer);
      binaryWriter.Write7BitEncodedInt(this.WalkingSpeedUpTimer);
      bitWriter.WriteBit(this.EnteredPhase2);
      bitWriter.WriteBit(this.EnteredPhase3);
      bitWriter.WriteBit(this.DoLaserAttack);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.BerserkSpeedupTimer = binaryReader.Read7BitEncodedInt();
      this.TeleportTimer = binaryReader.Read7BitEncodedInt();
      this.WalkingSpeedUpTimer = binaryReader.Read7BitEncodedInt();
      this.EnteredPhase2 = bitReader.ReadBit();
      this.EnteredPhase3 = bitReader.ReadBit();
      this.DoLaserAttack = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.25, (MidpointRounding) 0);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[44] = true;
      npc.buffImmune[324] = true;
      npc.buffImmune[46] = true;
      npc.buffImmune[47] = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return npc.alpha <= 0 && base.CanHitPlayer(npc, target, ref CooldownSlot);
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.deerBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      --this.BerserkSpeedupTimer;
      if ((double) npc.localAI[3] > 0.0 || this.EnteredPhase3)
        ++npc.localAI[2];
      if ((double) npc.ai[0] != 0.0)
      {
        npc.alpha -= 10;
        if (npc.alpha < 0)
          npc.alpha = 0;
        if (this.EnteredPhase3)
          ++npc.localAI[2];
      }
      ++this.TeleportTimer;
      if (this.EnteredPhase3)
        ++this.TeleportTimer;
      if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.ghost && !Main.LocalPlayer.dead && (double) ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center) < 1000.0)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<LowGroundBuff>(), 2, true, false);
      switch ((int) npc.ai[0])
      {
        case 0:
          if (++this.WalkingSpeedUpTimer > 900)
            this.WalkingSpeedUpTimer = 900;
          ((Entity) npc).position.X += (float) ((double) ((Entity) npc).velocity.X * (double) Math.Max(0, this.WalkingSpeedUpTimer - 90) / 90.0);
          if (this.TeleportTimer < 780)
          {
            if (this.EnteredPhase3)
              ((Entity) npc).position.X += ((Entity) npc).velocity.X;
            if ((double) ((Entity) npc).velocity.Y == 0.0)
            {
              if (this.EnteredPhase2)
                ((Entity) npc).position.X += ((Entity) npc).velocity.X;
              if (this.BerserkSpeedupTimer > 0)
                ((Entity) npc).position.X += (float) ((double) ((Entity) npc).velocity.X * 4.0 * (double) this.BerserkSpeedupTimer / 600.0);
            }
          }
          if (this.EnteredPhase2)
          {
            if (!this.EnteredPhase3 && (double) npc.life < (double) npc.lifeMax * 0.33)
            {
              npc.ai[0] = 3f;
              npc.ai[1] = 0.0f;
              npc.netUpdate = true;
              break;
            }
            if (this.TeleportTimer > 780)
            {
              this.WalkingSpeedUpTimer = 0;
              ((Entity) npc).velocity.X *= 0.9f;
              npc.dontTakeDamage = true;
              npc.localAI[1] = 0.0f;
              if (this.EnteredPhase2 && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.ghost && !Main.LocalPlayer.dead && (double) ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center) < 1600.0)
              {
                FargoSoulsUtil.AddDebuffFixedDuration(Main.LocalPlayer, 22, 2);
                FargoSoulsUtil.AddDebuffFixedDuration(Main.LocalPlayer, 80, 2);
              }
              if (npc.alpha == 0)
              {
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
                Deerclops.SpawnFreezeHands(npc);
              }
              npc.alpha += 5;
              if (npc.alpha > (int) byte.MaxValue)
              {
                npc.alpha = (int) byte.MaxValue;
                npc.localAI[3] = 30f;
                if (npc.HasPlayerTarget)
                {
                  float num1 = (float) (224 * Math.Sign(((Entity) npc).Center.X - ((Entity) Main.player[npc.target]).Center.X)) * -1f;
                  if (this.TeleportTimer == 790)
                  {
                    if (Utils.NextBool(Main.rand))
                      num1 *= -1f;
                    if (Main.netMode == 2)
                      NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    this.DoLaserAttack = !this.DoLaserAttack;
                    EModeNPCBehaviour.NetSync(npc);
                  }
                  ((Entity) npc).Bottom = Vector2.op_Addition(((Entity) Main.player[npc.target]).Bottom, Vector2.op_Multiply(num1, Vector2.UnitX));
                  ((Entity) npc).direction = Math.Sign(((Entity) Main.player[npc.target]).Center.X - ((Entity) npc).Center.X);
                  ((Entity) npc).velocity.X = 3.4f * (float) ((Entity) npc).direction;
                  ((Entity) npc).velocity.Y = 0.0f;
                  int num2 = 180;
                  if (this.EnteredPhase3)
                    num2 -= 30;
                  if (WorldSavingSystem.MasochistModeReal)
                    num2 -= 30;
                  if (this.TeleportTimer > 780 + num2)
                  {
                    this.TeleportTimer = 0;
                    ((Entity) npc).velocity.X = 0.0f;
                    npc.ai[0] = 4f;
                    npc.ai[1] = 0.0f;
                    EModeNPCBehaviour.NetSync(npc);
                    if (Main.netMode == 2)
                      NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                  }
                }
              }
              else
              {
                this.TeleportTimer = 780;
                if ((double) npc.localAI[3] > 0.0)
                  npc.localAI[3] -= 3f;
              }
              return false;
            }
            break;
          }
          if ((double) npc.life < (double) npc.lifeMax * 0.66)
          {
            npc.ai[0] = 3f;
            npc.ai[1] = 0.0f;
            npc.netUpdate = true;
            break;
          }
          break;
        case 1:
          this.WalkingSpeedUpTimer = 0;
          if ((double) npc.ai[1] < 30.0 && WorldSavingSystem.MasochistModeReal)
          {
            npc.ai[1] += 0.5f;
            npc.frameCounter += 0.5;
            break;
          }
          break;
        case 3:
          this.WalkingSpeedUpTimer = 0;
          if (!WorldSavingSystem.MasochistModeReal && (double) npc.ai[1] < 30.0)
          {
            npc.ai[1] -= 0.5f;
            npc.frameCounter -= 0.5;
          }
          if (this.EnteredPhase2)
          {
            npc.localAI[1] = 0.0f;
            npc.localAI[3] = 30f;
            if ((double) npc.ai[1] > 30.0)
            {
              Main.dayTime = false;
              Main.time = 16200.0;
            }
          }
          else if ((double) npc.life < (double) npc.lifeMax * 0.66)
          {
            this.EnteredPhase2 = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (this.EnteredPhase3)
          {
            if (!Main.dedServ)
              FargoSoulsUtil.ScreenshakeRumble(6f);
            if ((double) npc.ai[1] > 30.0 && npc.HasValidTarget)
              ((Entity) npc).position = Vector2.Lerp(((Entity) npc).position, Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(450f, Vector2.UnitY)), 0.2f);
          }
          else if ((double) npc.life < (double) npc.lifeMax * 0.33)
          {
            this.EnteredPhase3 = true;
            EModeNPCBehaviour.NetSync(npc);
          }
          if (this.EnteredPhase3 || WorldSavingSystem.MasochistModeReal)
          {
            this.BerserkSpeedupTimer = 600;
            break;
          }
          break;
        case 4:
          this.WalkingSpeedUpTimer = 0;
          int num3 = 100;
          if (this.EnteredPhase3)
            num3 *= 2;
          if (this.TeleportTimer > 780 - num3)
            this.TeleportTimer = 780 - num3;
          if ((double) npc.ai[1] == 0.0)
          {
            if (this.EnteredPhase2)
            {
              if (npc.alpha == 0)
                this.DoLaserAttack = Utils.NextBool(Main.rand);
              EModeNPCBehaviour.NetSync(npc);
              if (FargoSoulsUtil.HostCheck && this.DoLaserAttack)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
            }
            if (FargoSoulsUtil.HostCheck && !this.DoLaserAttack)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, 683, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          Vector2 vector2 = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(new Vector2((float) (64 * ((Entity) npc).direction), -24f), npc.scale));
          if (WorldSavingSystem.MasochistModeReal && (double) npc.ai[1] < 35.0)
            ++npc.ai[1];
          if (this.DoLaserAttack && (double) npc.ai[1] >= 70.0)
          {
            if (this.EnteredPhase3)
            {
              float num4 = 0.33f;
              if ((double) npc.ai[1] == 70.0)
              {
                float num5 = 55.60606f * 5f;
                float num6 = (float) (3.1415927410125732 * (WorldSavingSystem.MasochistModeReal ? 1.0 : 0.800000011920929)) / num5 * (float) -((Entity) npc).direction;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.UnitY, ModContent.ProjectileType<DeerclopsDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 2f), 0.0f, Main.myPlayer, num6, num5, 0.0f);
              }
              npc.ai[1] += num4;
              if ((double) npc.ai[1] < 90.0)
                return false;
            }
            else
            {
              npc.ai[1] += 0.33f;
              if ((double) npc.ai[1] >= 89.0)
              {
                npc.ai[0] = 2f;
                npc.ai[1] = 0.0f;
                npc.frameCounter = 0.0;
                npc.netUpdate = true;
                break;
              }
            }
            if ((double) npc.ai[1] < 90.0)
              return false;
            break;
          }
          break;
        case 5:
          if ((double) npc.ai[1] == 30.0 && npc.HasValidTarget && (double) Math.Abs(((Entity) npc).Center.X - ((Entity) Main.player[npc.target]).Center.X) < 48.0 && (double) ((Entity) Main.player[npc.target]).Bottom.Y < (double) ((Entity) npc).Top.Y - 80.0)
          {
            Deerclops.SpawnFreezeHands(npc);
            break;
          }
          break;
        case 6:
          if (++this.ForceDespawnTimer > 180 && FargoSoulsUtil.HostCheck)
          {
            npc.ai[0] = 8f;
            npc.ai[1] = 0.0f;
            npc.localAI[1] = 0.0f;
            npc.netUpdate = true;
            break;
          }
          break;
      }
      if (this.EnteredPhase3 && ((double) npc.ai[0] != 0.0 || npc.alpha <= 0))
      {
        npc.localAI[3] += 3f;
        if ((double) npc.localAI[3] > 30.0)
          npc.localAI[3] = 30f;
      }
      EModeUtils.DropSummon(npc, "DeerThing2", NPC.downedDeerclops, ref this.DroppedSummon);
      return flag;
    }

    private static void SpawnFreezeHands(NPC npc)
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 12; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(16f * Utils.NextFloat(Main.rand, 6f, 36f), Utils.RotatedBy(Vector2.UnitX, 0.52359879016876221 * ((double) index + (double) Utils.NextFloat(Main.rand)), new Vector2())));
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<DeerclopsHand>(), 0, 0.0f, Main.myPlayer, (float) npc.target, 0.0f, 0.0f);
      }
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(44, 90, true, false);
      target.AddBuff(36, 90, true, false);
      if (WorldSavingSystem.MasochistModeReal)
        target.AddBuff(ModContent.BuffType<MarkedforDeathBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<HypothermiaBuff>(), 1200, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 39);
      EModeNPCBehaviour.LoadGore(recolor, 1270);
      EModeNPCBehaviour.LoadGore(recolor, 1271);
      EModeNPCBehaviour.LoadGore(recolor, 1272);
      EModeNPCBehaviour.LoadGore(recolor, 1273);
      EModeNPCBehaviour.LoadGore(recolor, 1274);
    }
  }
}
