// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Retinazer
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
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class Retinazer : EModeNPCBehaviour
  {
    public int DeathrayState;
    public int AuraRadiusCounter;
    public int MechElectricOrbTimer;
    public bool StoredDirectionToPlayer;
    public bool ForcedPhase2OnSpawn;
    public bool DroppedSummon;
    public bool HasSaidEndure;
    public bool Resist;
    public int RespawnTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(125);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.DeathrayState);
      binaryWriter.Write7BitEncodedInt(this.AuraRadiusCounter);
      binaryWriter.Write7BitEncodedInt(this.MechElectricOrbTimer);
      bitWriter.WriteBit(this.StoredDirectionToPlayer);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      this.DeathrayState = binaryReader.Read7BitEncodedInt();
      this.AuraRadiusCounter = binaryReader.Read7BitEncodedInt();
      this.MechElectricOrbTimer = binaryReader.Read7BitEncodedInt();
      this.StoredDirectionToPlayer = bitReader.ReadBit();
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
      EModeGlobalNPC.retiBoss = ((Entity) npc).whoAmI;
      this.Resist = false;
      if (WorldSavingSystem.SwarmActive)
        return true;
      if ((double) npc.ai[0] == 1.0 || (double) npc.ai[0] == 2.0)
        this.Resist = true;
      NPC npc1 = FargoSoulsUtil.NPCExists(EModeGlobalNPC.spazBoss, new int[1]
      {
        126
      });
      if (WorldSavingSystem.MasochistModeReal && npc1 == null && npc.HasValidTarget && ++this.RespawnTimer > 600)
      {
        this.RespawnTimer = 0;
        if (FargoSoulsUtil.HostCheck)
        {
          int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, new Vector2(Utils.NextFloat(Main.rand, -1000f, 1000f), Utils.NextFloat(Main.rand, -800f, -600f))), 126, target: npc.target, velocity: new Vector2());
          if (index != Main.maxNPCs)
          {
            Main.npc[index].life = Main.npc[index].lifeMax / 4;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.TwinsRevive", new Color(175, 75, (int) byte.MaxValue), (object) Main.npc[index].FullName);
          }
        }
      }
      if (!this.ForcedPhase2OnSpawn)
      {
        this.ForcedPhase2OnSpawn = true;
        npc.ai[0] = 1f;
        npc.ai[1] = 0.0f;
        npc.ai[2] = 0.0f;
        npc.ai[3] = 0.0f;
        npc.netUpdate = true;
      }
      if (npc.life <= npc.lifeMax / 2 || npc.dontTakeDamage)
      {
        npc.dontTakeDamage = npc.life == 1 || !npc.HasValidTarget;
        if (npc.life != 1 && npc.HasValidTarget)
          npc.dontTakeDamage = false;
        if (npc.dontTakeDamage && npc.HasValidTarget && (npc1 == null || npc1.life == 1))
          npc.dontTakeDamage = false;
      }
      if (Main.dayTime && !Main.remixWorld)
      {
        if ((double) ((Entity) npc).velocity.Y > 0.0)
          ((Entity) npc).velocity.Y = 0.0f;
        ((Entity) npc).velocity.Y -= 0.5f;
        npc.dontTakeDamage = true;
        if (npc1 != null)
        {
          if (npc.timeLeft < 60)
            npc.timeLeft = 60;
          if (npc1.timeLeft < 60)
            npc1.timeLeft = 60;
          npc.TargetClosest(false);
          npc1.TargetClosest(false);
          if ((double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 2000.0 && (double) ((Entity) npc1).Distance(((Entity) Main.player[npc1.target]).Center) > 2000.0 && FargoSoulsUtil.HostCheck)
          {
            ((Entity) npc).active = false;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            ((Entity) npc1).active = false;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, EModeGlobalNPC.spazBoss, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        return true;
      }
      if ((double) npc.ai[0] < 4.0)
      {
        if (npc.life <= npc.lifeMax / 2)
        {
          npc.ai[0] = 604f;
          npc.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 11f, (float) ((Entity) npc).whoAmI, 0.0f);
        }
      }
      else
      {
        if (WorldSavingSystem.MasochistModeReal && npc1 == null && --this.MechElectricOrbTimer < 0)
        {
          this.MechElectricOrbTimer = 240;
          if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            ((Vector2) ref vector2).Normalize();
            vector2 = Vector2.op_Multiply(vector2, 10f);
            for (int index = 0; index < 12; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, Math.PI / 6.0 * (double) index, new Vector2()), ModContent.ProjectileType<MechElectricOrb>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 2f);
          }
        }
        if (Main.rand.Next(4) < 3)
        {
          int num = this.DeathrayState != 0 ? 90 : 138;
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) npc).position, new Vector2(2f, 2f)), ((Entity) npc).width + 4, ((Entity) npc).height + 4, num, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 3.5f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
          Main.dust[index].velocity.Y -= 0.5f;
          if (Utils.NextBool(Main.rand, 4))
          {
            Main.dust[index].noGravity = false;
            Main.dust[index].scale *= 0.5f;
          }
        }
        if ((double) npc.localAI[1] >= ((double) npc.ai[1] == 0.0 ? 175.0 : 55.0))
        {
          npc.localAI[1] = 0.0f;
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply((float) (((Entity) npc).width - 24), vector2)), vector2, ModContent.ProjectileType<MechElectricOrbTwins>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, (float) npc.target, 0.0f, 2f);
        }
        if (this.DeathrayState == 0 || this.DeathrayState == 3)
        {
          --this.AuraRadiusCounter;
          if (this.AuraRadiusCounter < 0)
            this.AuraRadiusCounter = 0;
        }
        else
        {
          ++this.AuraRadiusCounter;
          if (this.AuraRadiusCounter > 180)
            this.AuraRadiusCounter = 180;
        }
        float distance = (float) (2000.0 - (double) (1200 * this.AuraRadiusCounter) / 180.0);
        if (WorldSavingSystem.MasochistModeReal)
          distance *= 0.75f;
        if ((double) distance < 1999.0)
          EModeGlobalNPC.Aura(npc, distance, true, 6, new Color(), ModContent.BuffType<OiledBuff>(), WorldSavingSystem.MasochistModeReal ? ModContent.BuffType<GodEaterBuff>() : 24, 67);
        float num1 = (float) Math.PI / 100f;
        if (WorldSavingSystem.MasochistModeReal)
          num1 *= 1.05f;
        ++npc.ai[0];
        switch (this.DeathrayState)
        {
          case 0:
            if (!npc.HasValidTarget)
            {
              --npc.ai[0];
              if (npc1 == null)
                ((Entity) npc).velocity.Y -= 0.5f;
            }
            if ((double) npc.ai[0] > 604.0)
            {
              npc.ai[0] = 4f;
              if (npc.HasPlayerTarget)
              {
                npc.rotation = (double) ((Entity) npc).Center.X < (double) ((Entity) Main.player[npc.target]).Center.X ? 0.0f : 3.14159274f;
                npc.rotation -= 1.57079637f;
                ++this.DeathrayState;
                npc.ai[3] = -npc.rotation;
                if ((double) --npc.ai[2] > 295.0)
                  npc.ai[2] = 295f;
                this.StoredDirectionToPlayer = (double) ((Entity) Main.player[npc.target]).Center.X - (double) ((Entity) npc).Center.X < 0.0;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
                SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
              }
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              EModeNPCBehaviour.NetSync(npc);
              break;
            }
            break;
          case 1:
            NPC npc2 = npc;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, (float) (1.0 - ((double) npc.ai[0] - 4.0) / 120.0));
            npc.localAI[1] = 0.0f;
            npc.ai[3] -= (float) (((double) npc.ai[0] - 4.0) / 120.0 * (double) num1 * (this.StoredDirectionToPlayer ? 1.0 : -1.0));
            npc.rotation = -npc.ai[3];
            if ((double) npc.ai[0] == 35.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 9f, (float) ((Entity) npc).whoAmI, 0.0f);
            if ((double) npc.ai[0] >= 155.0)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2 = Utils.RotatedBy(Vector2.UnitX, (double) npc.rotation, new Vector2());
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<RetinazerDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) npc).whoAmI, 0.0f);
              }
              ++this.DeathrayState;
              npc.ai[0] = 4f;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              EModeNPCBehaviour.NetSync(npc);
            }
            return false;
          case 2:
            ((Entity) npc).velocity = Vector2.Zero;
            npc.localAI[1] = 0.0f;
            npc.ai[3] -= num1 * (this.StoredDirectionToPlayer ? 1f : -1f);
            npc.rotation = -npc.ai[3];
            if ((double) npc.ai[0] >= 244.0)
            {
              ++this.DeathrayState;
              npc.ai[0] = 4f;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              EModeNPCBehaviour.NetSync(npc);
            }
            else if (!npc.HasValidTarget)
            {
              npc.TargetClosest(false);
              if (!npc.HasValidTarget)
                npc.ai[0] = 244f;
            }
            return false;
          case 3:
            NPC npc3 = npc;
            ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, (float) (((double) npc.ai[0] - 4.0) / 60.0));
            npc.localAI[1] = 0.0f;
            npc.ai[3] -= (float) ((1.0 - ((double) npc.ai[0] - 4.0) / 60.0) * (double) num1 * (this.StoredDirectionToPlayer ? 1.0 : -1.0));
            npc.rotation = -npc.ai[3];
            if ((double) npc.ai[0] >= 64.0)
            {
              this.DeathrayState = 0;
              npc.ai[0] = 4f;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              EModeNPCBehaviour.NetSync(npc);
            }
            return false;
          default:
            this.DeathrayState = 0;
            npc.ai[0] = 4f;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
            break;
        }
      }
      if (this.DeathrayState > 0)
        this.Resist = true;
      EModeUtils.DropSummon(npc, "MechEye", NPC.downedMechBoss2, ref this.DroppedSummon, Main.hardMode);
      return true;
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (this.Resist)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      return (double) npc.ai[0] >= 4.0 ? new Color?(this.DeathrayState != 0 ? new Color((int) byte.MaxValue, (int) ((Color) ref drawColor).G / 2, (int) ((Color) ref drawColor).B / 2) : new Color((float) byte.MaxValue, (float) ((Color) ref drawColor).G * 0.66f, 0.0f)) : base.GetAlpha(npc, drawColor);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive || WorldSavingSystem.MasochistModeReal)
        return base.CheckDead(npc);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.spazBoss, 126) || Main.npc[EModeGlobalNPC.spazBoss].life <= 1)
        return base.CheckDead(npc);
      npc.life = 1;
      ((Entity) npc).active = true;
      if (FargoSoulsUtil.HostCheck)
        npc.netUpdate = true;
      if (!this.HasSaidEndure)
      {
        this.HasSaidEndure = true;
        FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.TwinsEndure", new Color(175, 75, (int) byte.MaxValue), (object) npc.FullName);
      }
      return false;
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 15);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 20);
      EModeNPCBehaviour.LoadGoreRange(recolor, 143, 146);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Chain12, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain12, "Chain12");
    }
  }
}
