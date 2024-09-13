// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.BrainofCthulhu
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core;
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
  public class BrainofCthulhu : EModeNPCBehaviour
  {
    public int ConfusionTimer;
    public int IllusionTimer;
    public int ForceDespawnTimer;
    public bool EnteredPhase2;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(266);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.ConfusionTimer);
      binaryWriter.Write7BitEncodedInt(this.IllusionTimer);
      bitWriter.WriteBit(this.EnteredPhase2);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.ConfusionTimer = binaryReader.Read7BitEncodedInt();
      this.IllusionTimer = binaryReader.Read7BitEncodedInt();
      this.EnteredPhase2 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.25);
      npc.scale += 0.25f;
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[69] = true;
    }

    public virtual bool CanHitPlayer(NPC npc, Player target, ref int CooldownSlot)
    {
      return npc.alpha == 0;
    }

    public override bool SafePreAI(NPC npc)
    {
      EModeGlobalNPC.brainBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return base.SafePreAI(npc);
      if (((Entity) Main.LocalPlayer).active && Main.LocalPlayer.Eternity().ShorterDebuffsTimer < 2)
        Main.LocalPlayer.Eternity().ShorterDebuffsTimer = 2;
      if (!npc.HasValidTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) > 3000.0)
      {
        if (++this.ForceDespawnTimer > 60)
        {
          ((Entity) npc).velocity.Y += 0.75f;
          if (npc.timeLeft > 60)
            npc.timeLeft = 60;
        }
      }
      else
        this.ForceDespawnTimer = 0;
      if (npc.alpha > 0 && ((double) npc.ai[0] == 2.0 || (double) npc.ai[0] == -3.0) && npc.HasValidTarget)
      {
        Vector2 center = ((Entity) Main.player[npc.target]).Center;
        if ((double) ((Entity) npc).Distance(center) < 360.0)
          ((Entity) npc).Center = Vector2.op_Addition(center, Vector2.op_Multiply(((Entity) npc).DirectionFrom(center), 360f));
      }
      if (this.EnteredPhase2)
      {
        if (npc.alpha > 0 && npc.buffType[0] != 0)
          npc.DelBuff(0);
        int num1 = WorldSavingSystem.MasochistModeReal ? 240 : 300;
        int num2 = num1 - 60;
        if (--this.ConfusionTimer < 0)
        {
          this.ConfusionTimer = num1;
          if (!Main.player[npc.target].HasBuff(31))
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) Main.player[npc.target]).Center);
            Vector2 center = ((Entity) Main.player[npc.target]).Center;
            TelegraphConfusion(new Vector2(center.X + vector2.X, center.Y + vector2.Y));
            TelegraphConfusion(new Vector2(center.X + vector2.X, center.Y - vector2.Y));
            TelegraphConfusion(new Vector2(center.X - vector2.X, center.Y + vector2.Y));
            TelegraphConfusion(new Vector2(center.X - vector2.X, center.Y - vector2.Y));
          }
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
        }
        else if (this.ConfusionTimer == num2)
        {
          if (Main.player[npc.target].HasBuff(31))
          {
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
            TelegraphConfusion(((Entity) npc).Center);
            this.IllusionTimer = 210;
            if (FargoSoulsUtil.HostCheck)
            {
              int type = ModContent.ProjectileType<BrainIllusionProj>();
              int alpha = (int) ((double) byte.MaxValue * (double) npc.life / (double) npc.lifeMax);
              foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == type && (double) p.ai[0] == (double) ((Entity) npc).whoAmI && (double) p.ai[1] == 0.0)))
              {
                if ((double) ((Entity) projectile).Distance(((Entity) Main.player[npc.target]).Center) < 1000.0)
                  SpawnClone(((Entity) projectile).Center);
                projectile.Kill();
              }
              Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) Main.player[npc.target]).Center);
              Vector2 center = ((Entity) Main.player[npc.target]).Center;
              SpawnClone(new Vector2(center.X + vector2.X, center.Y + vector2.Y));
              SpawnClone(new Vector2(center.X + vector2.X, center.Y - vector2.Y));
              SpawnClone(new Vector2(center.X - vector2.X, center.Y + vector2.Y));
              SpawnClone(new Vector2(center.X - vector2.X, center.Y - vector2.Y));

              void SpawnClone(Vector2 center)
              {
                int num = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) center.X, (int) center.Y, ModContent.NPCType<BrainIllusionAttack>(), ((Entity) npc).whoAmI, (float) ((Entity) npc).whoAmI, (float) alpha, 0.0f, 0.0f, (int) byte.MaxValue);
                if (num == Main.maxNPCs)
                  return;
                NetMessage.SendData(23, -1, -1, (NetworkText) null, num, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
          }
          else
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) npc).Center, ((Entity) Main.player[npc.target]).Center);
            Vector2 center = ((Entity) Main.player[npc.target]).Center;
            LaserSpread(new Vector2(center.X + vector2.X, center.Y + vector2.Y));
            LaserSpread(new Vector2(center.X + vector2.X, center.Y - vector2.Y));
            LaserSpread(new Vector2(center.X - vector2.X, center.Y + vector2.Y));
            LaserSpread(new Vector2(center.X - vector2.X, center.Y - vector2.Y));
          }
          if ((double) ((Entity) npc).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && !Main.LocalPlayer.HasBuff(31))
            FargoSoulsUtil.AddDebuffFixedDuration(Main.LocalPlayer, 31, num1 + 10, false);
        }
        if (--this.IllusionTimer < 0)
        {
          this.IllusionTimer = Main.rand.Next(5, 11);
          if (npc.life > npc.lifeMax / 2)
            this.IllusionTimer += 5;
          if (npc.life < npc.lifeMax / 10)
            this.IllusionTimer -= 2;
          if (WorldSavingSystem.MasochistModeReal)
            this.IllusionTimer -= 2;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_1 = Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Utils.NextVector2CircularEdge(Main.rand, 1200f, 1200f));
            Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(Vector2.op_Addition(Vector2.op_Addition(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(((Entity) Main.player[npc.target]).velocity, 45f)), Utils.NextVector2Circular(Main.rand, -600f, 600f)), vector2_1)), Utils.NextFloat(Main.rand, 12f, 48f));
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_1, vector2_2, ModContent.ProjectileType<BrainIllusionProj>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f), 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, 0.0f, 0.0f);
          }
        }
        if (this.IllusionTimer > 60)
        {
          if ((double) npc.ai[0] == -1.0 && (double) npc.localAI[1] < 80.0)
            npc.localAI[1] = 80f;
          if ((double) npc.ai[0] == -3.0 && (double) npc.ai[3] > 200.0)
          {
            npc.dontTakeDamage = true;
            npc.ai[0] = -3f;
            npc.ai[3] = (float) byte.MaxValue;
            npc.alpha = (int) byte.MaxValue;
            return false;
          }
        }
      }
      else if (!npc.dontTakeDamage)
      {
        this.EnteredPhase2 = true;
        if (FargoSoulsUtil.HostCheck)
        {
          int type = (!SoulConfig.Instance.BossRecolors ? 0 : (WorldSavingSystem.EternityMode ? 1 : 0)) != 0 ? ModContent.NPCType<BrainIllusion2>() : ModContent.NPCType<BrainIllusion>();
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, type, ((Entity) npc).whoAmI, (float) ((Entity) npc).whoAmI, -1f, 1f, velocity: new Vector2());
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, type, ((Entity) npc).whoAmI, (float) ((Entity) npc).whoAmI, 1f, -1f, velocity: new Vector2());
          FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, type, ((Entity) npc).whoAmI, (float) ((Entity) npc).whoAmI, 1f, 1f, velocity: new Vector2());
          if (WorldSavingSystem.MasochistModeReal)
            FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), ((Entity) npc).Center, ModContent.NPCType<BrainClone>(), ((Entity) npc).whoAmI, velocity: new Vector2());
          for (int index = 0; index < Main.maxProjectiles; ++index)
          {
            if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<GoldenShowerHoming>())
              Main.projectile[index].Kill();
          }
        }
      }
      EModeUtils.DropSummon(npc, "GoreySpine", NPC.downedBoss2, ref this.DroppedSummon);
      npc.defense = 0;
      npc.defDefense = 0;
      return base.SafePreAI(npc);

      void TelegraphConfusion(Vector2 spawn)
      {
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 8f, 180f, 0.0f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 8f, 200f, 0.0f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), spawn, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 8f, 220f, 0.0f);
      }

      void LaserSpread(Vector2 spawn)
      {
        if (npc.life > npc.lifeMax / 2 && !WorldSavingSystem.MasochistModeReal || !npc.HasValidTarget || !FargoSoulsUtil.HostCheck)
          return;
        int num1 = WorldSavingSystem.MasochistModeReal ? 7 : 3;
        int num2 = WorldSavingSystem.MasochistModeReal ? 2 : 3;
        int num3 = FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.33333337f);
        Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), spawn, new Vector2(0.0f, -4f), ModContent.ProjectileType<BrainofConfusion>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        for (int index = -num1; index <= num1; ++index)
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), spawn, Vector2.op_Multiply(0.2f, Utils.RotatedBy(((Entity) Main.player[npc.target]).DirectionFrom(spawn), (double) MathHelper.ToRadians((float) num2) * (double) index, new Vector2())), ModContent.ProjectileType<DestroyerLaser>(), num3, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (npc.life > 0)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, Math.Max(0.2f, (float) Math.Sqrt((double) npc.life / (double) npc.lifeMax)));
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      target.AddBuff(20, 120, true, false);
      target.AddBuff(22, 120, true, false);
      target.AddBuff(30, 120, true, false);
      target.AddBuff(32, 120, true, false);
      target.AddBuff(33, 120, true, false);
      target.AddBuff(36, 120, true, false);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 23);
      EModeNPCBehaviour.LoadGoreRange(recolor, 392, 402);
    }
  }
}
