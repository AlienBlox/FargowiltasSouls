// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.Spazmatism
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
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
  public class Spazmatism : EModeNPCBehaviour
  {
    public int ProjectileTimer;
    public int FlameWheelSpreadTimer;
    public int FlameWheelCount;
    public int MechElectricOrbTimer;
    public int P3DashPhaseDelay;
    public bool ForcedPhase2OnSpawn;
    public bool HasSaidEndure;
    public bool Resist;
    public float RealRotation;
    public int RespawnTimer;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(126);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.ProjectileTimer);
      binaryWriter.Write7BitEncodedInt(this.FlameWheelSpreadTimer);
      binaryWriter.Write7BitEncodedInt(this.FlameWheelCount);
      binaryWriter.Write7BitEncodedInt(this.MechElectricOrbTimer);
      binaryWriter.Write7BitEncodedInt(this.P3DashPhaseDelay);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.ProjectileTimer = binaryReader.Read7BitEncodedInt();
      this.FlameWheelSpreadTimer = binaryReader.Read7BitEncodedInt();
      this.FlameWheelCount = binaryReader.Read7BitEncodedInt();
      this.MechElectricOrbTimer = binaryReader.Read7BitEncodedInt();
      this.P3DashPhaseDelay = binaryReader.Read7BitEncodedInt();
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[68] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.spazBoss = ((Entity) npc).whoAmI;
      this.Resist = false;
      if (WorldSavingSystem.SwarmActive)
        return true;
      if ((double) npc.ai[0] == 1.0 || (double) npc.ai[0] == 2.0)
        this.Resist = true;
      NPC npc1 = FargoSoulsUtil.NPCExists(EModeGlobalNPC.retiBoss, new int[1]
      {
        125
      });
      if (WorldSavingSystem.MasochistModeReal && npc1 == null && npc.HasValidTarget && ++this.RespawnTimer > 600)
      {
        this.RespawnTimer = 0;
        if (FargoSoulsUtil.HostCheck)
        {
          int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, new Vector2(Utils.NextFloat(Main.rand, -1000f, 1000f), Utils.NextFloat(Main.rand, -800f, -600f))), 125, target: npc.target, velocity: new Vector2());
          if (index != Main.maxNPCs)
          {
            Main.npc[index].life = Main.npc[index].lifeMax / 4;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.TwinsRevive", new Color(175, 75, (int) byte.MaxValue), (object) Main.npc[index].FullName);
          }
        }
      }
      float num1 = (float) npc.life / (float) npc.lifeMax;
      if (WorldSavingSystem.MasochistModeReal)
        num1 *= num1;
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
              NetMessage.SendData(23, -1, -1, (NetworkText) null, EModeGlobalNPC.retiBoss, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        return true;
      }
      if ((double) npc.ai[0] < 4.0)
      {
        if (npc.life <= npc.lifeMax / 2)
        {
          npc.ai[0] = 4f;
          npc.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          if (!WorldSavingSystem.MasochistModeReal)
            this.P3DashPhaseDelay = 75;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 398f, 0.0f);
          int buffIndex = npc.FindBuffIndex(39);
          if (buffIndex != -1)
            npc.DelBuff(buffIndex);
          npc.buffImmune[39] = true;
          npc.buffImmune[24] = true;
          npc.buffImmune[323] = true;
          npc.buffImmune[153] = true;
          npc.buffImmune[44] = true;
          npc.buffImmune[324] = true;
        }
        if (npc1 != null && (double) npc1.ai[0] >= 4.0 && npc1.GetGlobalNPC<Retinazer>().DeathrayState != 0 && npc1.GetGlobalNPC<Retinazer>().DeathrayState != 3 && !WorldSavingSystem.MasochistModeReal)
        {
          NPC npc2 = npc;
          ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.98f);
          if (!this.TryWatchHarmlessly(npc))
            return false;
        }
      }
      else
      {
        if (Main.rand.Next(4) < 3)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) npc).position, new Vector2(2f, 2f)), ((Entity) npc).width + 4, ((Entity) npc).height + 4, 89, ((Entity) npc).velocity.X * 0.4f, ((Entity) npc).velocity.Y * 0.4f, 100, new Color(), 3.5f);
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
        if ((double) npc.ai[1] == 0.0)
        {
          this.Resist = true;
          if (npc1 != null && ((double) npc1.ai[0] < 4.0 || npc1.GetGlobalNPC<Retinazer>().DeathrayState == 0 || npc1.GetGlobalNPC<Retinazer>().DeathrayState == 3))
          {
            npc.ai[1] = 1f;
            npc.ai[2] = 0.0f;
            npc.ai[3] = 0.0f;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            EModeNPCBehaviour.NetSync(npc);
            return false;
          }
          if (npc.HasValidTarget && npc1 != null)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) npc1).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc1, ((Entity) npc).Center), 100f));
            ((Entity) npc).velocity = Vector2.op_Division(Vector2.op_Subtraction(vector2, ((Entity) npc).Center), 60f);
            float num2 = (float) (0.02042035199701786 * (npc1.GetGlobalNPC<Retinazer>().StoredDirectionToPlayer ? 1.0 : -1.0));
            if (WorldSavingSystem.MasochistModeReal)
              num2 *= -1f;
            npc.rotation += (float) ((double) num2 * (double) this.ProjectileTimer / 20.0);
            this.RealRotation += num2;
            if (this.FlameWheelSpreadTimer < 0)
              this.FlameWheelSpreadTimer = 0;
            if (this.FlameWheelCount == 0)
            {
              this.FlameWheelCount = 2;
              if ((double) num1 < 0.375)
                this.FlameWheelCount = 3;
              if ((double) num1 < 0.25)
                this.FlameWheelCount = 4;
              if ((double) num1 < 0.125 || WorldSavingSystem.MasochistModeReal)
                this.FlameWheelCount = 5;
              this.ProjectileTimer = 0;
            }
            if (++this.FlameWheelSpreadTimer < 30)
            {
              npc.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) npc1).Center)) - 1.57079637f;
              this.RealRotation = npc.rotation;
            }
            else if (++this.ProjectileTimer % 15 == 0 && FargoSoulsUtil.HostCheck)
            {
              float num3 = 12f * Math.Min((float) (this.FlameWheelSpreadTimer - 30) / 120f, 1f);
              int num4 = (int) ((double) num3 / 12.0 * 90.0);
              float num5 = this.RealRotation + 1.57079637f;
              if (num4 > 5)
              {
                for (int index1 = 0; index1 < this.FlameWheelCount; ++index1)
                {
                  int index2 = Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(num3, Utils.ToRotationVector2(num5 + 6.28318548f / (float) this.FlameWheelCount * (float) index1)), ModContent.ProjectileType<MechElectricOrb>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 3f);
                  if (index2 != Main.maxProjectiles)
                    Main.projectile[index2].timeLeft = num4;
                }
              }
            }
            return false;
          }
        }
        else
        {
          if (npc1 != null && (double) npc1.ai[0] >= 4.0 && npc1.GetGlobalNPC<Retinazer>().DeathrayState != 0 && npc1.GetGlobalNPC<Retinazer>().DeathrayState != 3)
          {
            npc.ai[1] = 0.0f;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            EModeNPCBehaviour.NetSync(npc);
            return false;
          }
          this.FlameWheelCount = 0;
          if (this.FlameWheelSpreadTimer > 0)
          {
            this.P3DashPhaseDelay = Math.Min(this.FlameWheelSpreadTimer, 75);
            this.FlameWheelSpreadTimer = 0;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            EModeNPCBehaviour.NetSync(npc);
          }
          if (this.P3DashPhaseDelay > 0)
          {
            --this.P3DashPhaseDelay;
            if (!this.TryWatchHarmlessly(npc))
              return false;
          }
          if ((double) npc.ai[2] > 50.0)
            npc.ai[2] -= num1;
          else if (npc.HasValidTarget && ++this.ProjectileTimer > 3)
          {
            this.ProjectileTimer = 0;
            if (FargoSoulsUtil.HostCheck)
            {
              float num6 = (float) ((1.0 - (double) num1) * 0.800000011920929);
              float num7 = (float) (9.0 * (double) num1 * 2.0);
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.op_Multiply(num6, Utils.RotatedBy(((Entity) npc).velocity, (double) MathHelper.ToRadians(Utils.NextFloat(Main.rand, -num7, num7)), new Vector2())), 101, FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        if (WorldSavingSystem.MasochistModeReal && npc1 == null && --this.MechElectricOrbTimer < 0)
        {
          this.MechElectricOrbTimer = 150;
          if (FargoSoulsUtil.HostCheck && npc.HasPlayerTarget)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, ((Entity) npc).Center);
            ((Vector2) ref vector2).Normalize();
            vector2 = Vector2.op_Multiply(vector2, 14f);
            for (int index = 0; index < 8; ++index)
              Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Utils.RotatedBy(vector2, Math.PI / 4.0 * (double) index, new Vector2()), ModContent.ProjectileType<MechElectricOrb>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 3f);
          }
        }
      }
      return true;
    }

    private bool TryWatchHarmlessly(NPC npc)
    {
      this.Resist = true;
      if (!npc.HasValidTarget)
        return true;
      if ((double) npc.rotation > 3.1415927410125732)
        npc.rotation -= 6.28318548f;
      if ((double) npc.rotation < -3.1415927410125732)
        npc.rotation += 6.28318548f;
      float num = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) npc, ((Entity) Main.player[npc.target]).Center)) - 1.57079637f;
      if ((double) num > 3.1415927410125732)
        num -= 6.28318548f;
      if ((double) num < -3.1415927410125732)
        num += 6.28318548f;
      npc.rotation = MathHelper.Lerp(npc.rotation, num, 0.07f);
      return false;
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

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return (double) npc.ai[1] != 0.0 || this.FlameWheelSpreadTimer >= 30;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if ((double) npc.ai[0] < 4.0)
        return;
      target.AddBuff(39, 300, true, false);
    }

    public virtual Color? GetAlpha(NPC npc, Color drawColor)
    {
      return (double) npc.ai[0] >= 4.0 ? new Color?(new Color((int) ((Color) ref drawColor).R / 2, (int) byte.MaxValue, (int) ((Color) ref drawColor).B / 2)) : base.GetAlpha(npc, drawColor);
    }

    public virtual bool CheckDead(NPC npc)
    {
      if (WorldSavingSystem.SwarmActive || WorldSavingSystem.MasochistModeReal)
        return base.CheckDead(npc);
      if (!FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.retiBoss, 125) || Main.npc[EModeGlobalNPC.retiBoss].life <= 1)
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
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 16);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 21);
      EModeNPCBehaviour.LoadGoreRange(recolor, 143, 146);
      EModeNPCBehaviour.LoadSpecial(recolor, ref TextureAssets.Chain12, ref FargowiltasSouls.FargowiltasSouls.TextureBuffer.Chain12, "Chain12");
    }
  }
}
