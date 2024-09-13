// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.VanillaEternity.QueenBee
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Common.Utilities;
using FargowiltasSouls.Content.Bosses.Champions.Will;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Content.NPCs.EternityModeNPCs;
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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.VanillaEternity
{
  public class QueenBee : EModeNPCBehaviour
  {
    public int HiveThrowTimer;
    public int StingerRingTimer;
    public int BeeSwarmTimer = 600;
    public int ForgorDeathrayTimer;
    public int EnrageFactor;
    public bool SpawnedRoyalSubjectWave1;
    public bool SpawnedRoyalSubjectWave2;
    public bool InPhase2;
    public bool DroppedSummon;

    public override NPCMatcher CreateMatcher() => new NPCMatcher().MatchType(222);

    public virtual void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
    {
      base.SendExtraAI(npc, bitWriter, binaryWriter);
      binaryWriter.Write7BitEncodedInt(this.HiveThrowTimer);
      binaryWriter.Write7BitEncodedInt(this.StingerRingTimer);
      binaryWriter.Write7BitEncodedInt(this.BeeSwarmTimer);
      bitWriter.WriteBit(this.SpawnedRoyalSubjectWave1);
      bitWriter.WriteBit(this.SpawnedRoyalSubjectWave2);
      bitWriter.WriteBit(this.InPhase2);
    }

    public virtual void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
    {
      base.ReceiveExtraAI(npc, bitReader, binaryReader);
      this.HiveThrowTimer = binaryReader.Read7BitEncodedInt();
      this.StingerRingTimer = binaryReader.Read7BitEncodedInt();
      this.BeeSwarmTimer = binaryReader.Read7BitEncodedInt();
      this.SpawnedRoyalSubjectWave1 = bitReader.ReadBit();
      this.SpawnedRoyalSubjectWave2 = bitReader.ReadBit();
      this.InPhase2 = bitReader.ReadBit();
    }

    public virtual void SetDefaults(NPC npc)
    {
      ((GlobalType<NPC, GlobalNPC>) this).SetDefaults(npc);
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.4005);
    }

    public override void OnFirstTick(NPC npc)
    {
      base.OnFirstTick(npc);
      npc.buffImmune[20] = true;
    }

    public override bool SafePreAI(NPC npc)
    {
      bool flag = base.SafePreAI(npc);
      EModeGlobalNPC.beeBoss = ((Entity) npc).whoAmI;
      if (WorldSavingSystem.SwarmActive)
        return flag;
      if ((double) npc.ai[0] == 2.0 && npc.HasValidTarget)
      {
        float num = Math.Min(++npc.ai[1] / 3000f, 1f);
        ((Entity) npc).velocity = Vector2.Lerp(((Entity) npc).velocity, Vector2.op_Multiply(((Entity) npc).DirectionTo(((Entity) Main.player[npc.target]).Center), ((Vector2) ref ((Entity) npc).velocity).Length()), num);
      }
      if (npc.HasPlayerTarget && npc.HasValidTarget && (!Main.player[npc.target].ZoneJungle || (double) ((Entity) Main.player[npc.target]).position.Y < Main.worldSurface * 16.0))
      {
        if (++this.EnrageFactor == 300)
          FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.EMode.QueenBeeEnrage", new Color(175, 75, (int) byte.MaxValue));
        if (this.EnrageFactor > 300)
        {
          float num = Utils.NextFloat(Main.rand, 0.03f, 0.18f);
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, new Vector2((float) (3 * ((Entity) npc).direction), 15f)), Vector2.op_Multiply(Utils.NextFloat(Main.rand, 8f, 24f), Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f)), ModContent.ProjectileType<Bee>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, 1.5f), 0.0f, Main.myPlayer, (float) npc.target, Utils.NextBool(Main.rand) ? -num : num, 0.0f);
        }
      }
      else
        this.EnrageFactor = 0;
      if (!this.SpawnedRoyalSubjectWave1 && npc.life < npc.lifeMax / 3 * 2 && npc.HasPlayerTarget)
      {
        this.SpawnedRoyalSubjectWave1 = true;
        Vector2 spawnPos;
        // ISSUE: explicit constructor call
        ((Vector2) ref spawnPos).\u002Ector(((Entity) npc).position.X + (float) (((Entity) npc).width / 2) + (float) (Main.rand.Next(20) * ((Entity) npc).direction), ((Entity) npc).position.Y + (float) ((Entity) npc).height * 0.8f);
        int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<RoyalSubject>(), velocity: new Vector2((float) Main.rand.Next(-200, 201) * 0.1f, (float) Main.rand.Next(-200, 201) * 0.1f));
        if (index != Main.maxNPCs)
          Main.npc[index].localAI[0] = 60f;
        FargoSoulsUtil.PrintLocalization("Announcement.HasAwoken", new Color(175, 75, (int) byte.MaxValue), (object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".NPCs.RoyalSubject.DisplayName"));
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (!this.SpawnedRoyalSubjectWave2 && npc.life < npc.lifeMax / 3 && npc.HasPlayerTarget)
      {
        this.SpawnedRoyalSubjectWave2 = true;
        if (WorldSavingSystem.MasochistModeReal)
          this.SpawnedRoyalSubjectWave1 = false;
        Vector2 spawnPos;
        // ISSUE: explicit constructor call
        ((Vector2) ref spawnPos).\u002Ector(((Entity) npc).position.X + (float) (((Entity) npc).width / 2) + (float) (Main.rand.Next(20) * ((Entity) npc).direction), ((Entity) npc).position.Y + (float) ((Entity) npc).height * 0.8f);
        int index = FargoSoulsUtil.NewNPCEasy(((Entity) npc).GetSource_FromAI((string) null), spawnPos, ModContent.NPCType<RoyalSubject>(), velocity: new Vector2((float) Main.rand.Next(-200, 201) * 0.1f, (float) Main.rand.Next(-200, 201) * 0.1f));
        if (index != Main.maxNPCs)
          Main.npc[index].localAI[0] = 60f;
        FargoSoulsUtil.PrintLocalization("Announcement.HasAwoken", new Color(175, 75, (int) byte.MaxValue), (object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".NPCs.RoyalSubject.DisplayName"));
        NPC.SpawnOnPlayer(npc.target, ModContent.NPCType<RoyalSubject>());
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (!this.InPhase2 && npc.life < npc.lifeMax / 2)
      {
        this.InPhase2 = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
        if (WorldSavingSystem.MasochistModeReal)
          this.SpawnedRoyalSubjectWave1 = false;
        npc.netUpdate = true;
        EModeNPCBehaviour.NetSync(npc);
      }
      if (NPC.AnyNPCs(ModContent.NPCType<RoyalSubject>()))
      {
        npc.HitSound = new SoundStyle?(SoundID.NPCHit4);
        npc.color = new Color((int) sbyte.MaxValue, (int) sbyte.MaxValue, (int) sbyte.MaxValue);
        int index1 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 1, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index1].noGravity = true;
        int index2 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, 1, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        if (!Main.getGoodWorld)
        {
          if ((double) npc.ai[0] == 0.0 && (double) npc.ai[1] % 2.0 == 0.0)
          {
            npc.ai[0] = 3f;
            npc.ai[1] = 0.0f;
            npc.netUpdate = true;
          }
          if ((double) npc.ai[0] == 3.0 && (double) npc.ai[1] > 1.0 && !WorldSavingSystem.MasochistModeReal)
            npc.ai[1] -= 0.5f;
        }
      }
      else
      {
        npc.HitSound = new SoundStyle?(SoundID.NPCHit1);
        npc.color = new Color();
        if (this.InPhase2 && this.HiveThrowTimer % 2 == 0)
          ++this.HiveThrowTimer;
      }
      if (WorldSavingSystem.MasochistModeReal)
      {
        ++this.HiveThrowTimer;
        if (this.ForgorDeathrayTimer > 0 && --this.ForgorDeathrayTimer % 10 == 0 && npc.HasValidTarget && FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(2000f, Vector2.UnitY)), Vector2.UnitY, ModContent.ProjectileType<WillDeathraySmall>(), (int) ((double) npc.damage * 0.75), 0.0f, Main.myPlayer, ((Entity) Main.player[npc.target]).Center.X, (float) ((Entity) npc).whoAmI, 1f);
          for (int index = 0; index < 22; ++index)
          {
            Vector2 vector2_1 = Vector2.op_Subtraction(Vector2.op_Multiply(Vector2.UnitX, Utils.NextFloat(Main.rand, -100f, 100f)), Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, 90f), (float) index));
            Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(1980f, Vector2.UnitY)), vector2_1);
            Vector2 vector2_3;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_3).\u002Ector(Utils.NextFloat(Main.rand, -0.1f, 0.1f), 22f);
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), vector2_2, vector2_3, ModContent.ProjectileType<RoyalSubjectProjectile>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if (this.InPhase2)
      {
        if (++this.HiveThrowTimer > 570 && this.BeeSwarmTimer <= 600 && ((double) npc.ai[0] == 3.0 || (double) npc.ai[0] == 1.0))
        {
          this.HiveThrowTimer = 0;
          npc.netUpdate = true;
          EModeNPCBehaviour.NetSync(npc);
          float num = 75f;
          Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Subtraction(((Entity) Main.player[npc.target]).Center, Vector2.op_Multiply(Vector2.UnitY, 16f)), ((Entity) npc).Center), Vector2.op_Multiply(((Entity) Main.player[npc.target]).velocity, 30f));
          vector2.X /= num;
          vector2.Y = (float) ((double) vector2.Y / (double) num - 0.125 * (double) num);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, vector2, ModContent.ProjectileType<Beehive>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage), 0.0f, Main.myPlayer, num - 5f, 0.0f, 0.0f);
        }
        if ((double) npc.ai[0] == 0.0 && (double) npc.ai[1] == 1.0)
        {
          npc.ai[0] = 3f;
          npc.ai[1] = 0.0f;
          npc.netUpdate = true;
        }
      }
      if ((double) npc.ai[0] == 3.0 || (double) npc.ai[0] == 1.0)
      {
        if (this.InPhase2 && ++this.BeeSwarmTimer > 600)
        {
          if (this.BeeSwarmTimer < 720)
          {
            if (this.BeeSwarmTimer == 601)
            {
              npc.netUpdate = true;
              EModeNPCBehaviour.NetSync(npc);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) npc).whoAmI, (float) npc.type, 0.0f);
              if (npc.HasValidTarget)
                SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.player[npc.target]).Center), (SoundUpdateCallback) null);
              if (WorldSavingSystem.MasochistModeReal)
                this.BeeSwarmTimer += 30;
            }
            if (Collision.CanHitLine(((Entity) npc).Center, 0, 0, ((Entity) Main.player[npc.target]).Center, 0, 0))
            {
              NPC npc1 = npc;
              ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.975f);
            }
            else if (this.BeeSwarmTimer > 630)
            {
              --this.BeeSwarmTimer;
              return true;
            }
          }
          else if (this.BeeSwarmTimer < 840)
          {
            ((Entity) npc).velocity = Vector2.Zero;
            if (this.BeeSwarmTimer % 2 == 0 && FargoSoulsUtil.HostCheck)
            {
              for (int index = -1; index <= 1; index += 2)
                Projectile.NewProjectile(((Entity) npc).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) npc).Center, new Vector2((float) (3 * ((Entity) npc).direction), 15f)), Vector2.op_Multiply((float) index * Utils.NextFloat(Main.rand, 9f, 18f), Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians(Utils.NextFloat(Main.rand, -45f, 45f)), new Vector2())), ModContent.ProjectileType<Bee>(), FargoSoulsUtil.ScaledProjectileDamage(npc.defDamage, WorldSavingSystem.MasochistModeReal ? 1.33333337f : 1f), 0.0f, Main.myPlayer, (float) npc.target, Utils.NextBool(Main.rand) ? -0.025f : 0.025f, 0.0f);
            }
          }
          else if (this.BeeSwarmTimer > 870)
          {
            this.BeeSwarmTimer = 0;
            this.HiveThrowTimer -= 60;
            npc.netUpdate = true;
            EModeNPCBehaviour.NetSync(npc);
            npc.ai[0] = 0.0f;
            npc.ai[1] = 4f;
            npc.ai[2] = -44f;
            npc.ai[3] = 0.0f;
          }
          if (npc.netUpdate)
          {
            npc.netUpdate = false;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) npc).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          return false;
        }
        int num = WorldSavingSystem.MasochistModeReal ? 90 : 120;
        if (++this.StingerRingTimer > num * 3)
          this.StingerRingTimer = 0;
        if (this.StingerRingTimer % num == 0)
        {
          float speed = WorldSavingSystem.MasochistModeReal ? 6f : 5f;
          if (FargoSoulsUtil.HostCheck)
            FargoSoulsUtil.XWay(this.StingerRingTimer == num * 3 ? 16 : 8, ((Entity) npc).GetSource_FromThis((string) null), ((Entity) npc).Center, 719, speed, 11, 1f);
        }
      }
      if ((double) npc.ai[0] == 0.0 && (double) npc.ai[1] == 4.0 && (double) npc.ai[2] < 0.0)
      {
        if ((double) npc.ai[2] == -44.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) npc).Center), (SoundUpdateCallback) null);
          for (int index3 = 0; index3 < 44; ++index3)
          {
            int index4 = Dust.NewDust(((Entity) npc).position, ((Entity) npc).width, ((Entity) npc).height, Utils.NextBool(Main.rand) ? 152 : 153, ((Entity) npc).velocity.X * 0.2f, ((Entity) npc).velocity.Y * 0.2f, 0, new Color(), 1f);
            Main.dust[index4].scale = Utils.NextFloat(Main.rand, 1f, 3f);
            Dust dust1 = Main.dust[index4];
            dust1.velocity = Vector2.op_Multiply(dust1.velocity, Utils.NextFloat(Main.rand, 4.4f));
            Main.dust[index4].noGravity = Utils.NextBool(Main.rand);
            if (Main.dust[index4].noGravity)
            {
              Main.dust[index4].scale *= 2.2f;
              Dust dust2 = Main.dust[index4];
              dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4.4f);
            }
          }
          if (WorldSavingSystem.MasochistModeReal)
            npc.ai[2] = 0.0f;
          this.ForgorDeathrayTimer = 95;
          if (Main.getGoodWorld)
            this.ForgorDeathrayTimer += 60;
        }
        NPC npc2 = npc;
        ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.95f);
        ++npc.ai[2];
        return false;
      }
      if (WorldSavingSystem.MasochistModeReal && (double) npc.ai[0] == 0.0 && (double) npc.ai[1] % 2.0 == 0.0 && npc.HasValidTarget && (double) Math.Abs(((Entity) Main.player[npc.target]).Center.Y - ((Entity) npc).Center.Y) > (double) ((Entity) npc).velocity.Y * 2.0)
        ((Entity) npc).position.Y += ((Entity) npc).velocity.Y;
      EModeUtils.DropSummon(npc, "Abeemination2", NPC.downedQueenBee, ref this.DroppedSummon);
      return flag;
    }

    public override void SafePostAI(NPC npc)
    {
      base.SafePostAI(npc);
      if (npc.HasValidTarget && (!npc.HasPlayerTarget || (double) ((Entity) npc).Distance(((Entity) Main.player[npc.target]).Center) <= 3000.0) || npc.timeLeft <= 60)
        return;
      npc.timeLeft = 60;
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if ((int) (Main.time / 60.0 - 30.0) % 60 != 22)
        return;
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.ItemType<TwentyTwoPainting>(), 1, false, 0, false, false);
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      base.OnHitPlayer(npc, target, hurtInfo);
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 300, true, false);
      target.AddBuff(ModContent.BuffType<SwarmingBuff>(), 600, true, false);
      if ((double) npc.ai[0] != 0.0)
        return;
      target.AddBuff(36, 300, true, false);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.SwarmActive && NPC.AnyNPCs(ModContent.NPCType<RoyalSubject>()))
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Division(local, 2f);
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public override void LoadSprites(NPC npc, bool recolor)
    {
      base.LoadSprites(npc, recolor);
      EModeNPCBehaviour.LoadNPCSprite(recolor, npc.type);
      EModeNPCBehaviour.LoadBossHeadSprite(recolor, 14);
      EModeNPCBehaviour.LoadGoreRange(recolor, 303, 308);
    }
  }
}
