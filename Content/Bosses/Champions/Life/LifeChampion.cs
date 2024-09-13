// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Life.LifeChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Life
{
  [AutoloadBossHead]
  public class LifeChampion : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 8;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 5);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 46;
      int num2 = num1 + 1;
      span[num2] = 68;
      int num3 = num2 + 1;
      span[num3] = ModContent.BuffType<LethargicBuff>();
      int num4 = num3 + 1;
      span[num4] = ModContent.BuffType<ClippedWingsBuff>();
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<HellFireMarkedBuff>();
      int num6 = num5 + 1;
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Scale = 0.5f;
      bestiaryDrawModifiers1.PortraitScale = new float?(0.5f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[3]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheHallow,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.DayTime,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.IsABestiaryIconDummy ? new Color?(this.NPC.GetBestiaryEntryColor()) : base.GetAlpha(drawColor);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 130;
      ((Entity) this.NPC).height = 130;
      this.NPC.damage = 160;
      this.NPC.defense = 0;
      this.NPC.lifeMax = 55000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit5);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath7);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(1, 0, 0, 0);
      this.NPC.boss = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Champions") : 81;
      this.NPC.dontTakeDamage = true;
      this.NPC.alpha = (int) byte.MaxValue;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      if ((double) this.NPC.localAI[3] == 0.0 || ((double) this.NPC.ai[0] == 2.0 || (double) this.NPC.ai[0] == 8.0) && (double) this.NPC.ai[3] == 0.0)
        return false;
      CooldownSlot = 1;
      return true;
    }

    public virtual void SendExtraAI(BinaryWriter writer) => writer.Write(this.NPC.localAI[3]);

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[3] = reader.ReadSingle();
    }

    public virtual void AI()
    {
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        if (!this.NPC.HasValidTarget)
          this.NPC.TargetClosest(false);
        if ((double) this.NPC.ai[2] < 0.10000000149011612)
          ((Entity) this.NPC).Center = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, Vector2.op_Multiply(Vector2.UnitY, 300f));
        this.NPC.ai[2] += 0.00555555569f;
        this.NPC.alpha = (int) ((double) byte.MaxValue * (1.0 - (double) this.NPC.ai[2]));
        if (this.NPC.alpha < 0)
          this.NPC.alpha = 0;
        if (this.NPC.alpha > (int) byte.MaxValue)
          this.NPC.alpha = (int) byte.MaxValue;
        if ((double) this.NPC.ai[2] <= 1.0)
          return;
        this.NPC.localAI[3] = 1f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.netUpdate = true;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(-20f, Utils.RotatedByRandom(Vector2.UnitY, 1.5707963705062866));
        SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -4f, 0.0f);
        if (!WorldSavingSystem.EternityMode)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<LifeRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }
      else
      {
        this.NPC.dontTakeDamage = false;
        this.NPC.alpha = 0;
        Player player = Main.player[this.NPC.target];
        if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0)
          this.NPC.timeLeft = 600;
        switch (this.NPC.ai[0])
        {
          case -3f:
            if (!Main.dayTime || !((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              this.NPC.noTileCollide = true;
              this.NPC.noGravity = true;
              --((Entity) this.NPC).velocity.Y;
              break;
            }
            ((Entity) this.NPC).velocity = Vector2.Zero;
            this.NPC.ai[1] -= 2f * (float) Math.PI / 447f;
            this.NPC.ai[3] += 2f * (float) Math.PI / 447f;
            if ((double) --this.NPC.ai[2] < 0.0)
            {
              this.NPC.localAI[1] = (float) ((double) this.NPC.localAI[1] == 0.0 ? 1 : 0);
              this.NPC.ai[2] = (double) this.NPC.localAI[1] == 1.0 ? 90f : 30f;
              if ((double) this.NPC.ai[1] < 360.0 && FargoSoulsUtil.HostCheck)
              {
                int num1 = (double) this.NPC.localAI[1] == 1.0 ? ModContent.ProjectileType<LifeDeathraySmall2>() : ModContent.ProjectileType<LifeDeathray2>();
                int num2 = 3;
                for (int index = 0; index < num2; ++index)
                {
                  float num3 = 6.28318548f / (float) num2 * (float) index;
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3] + (double) num3, new Vector2()), num1, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f * (float) Math.PI / 447f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[1] + (double) num3, new Vector2()), num1, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, -2f * (float) Math.PI / 447f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                }
              }
            }
            if ((double) --this.NPC.localAI[0] < 0.0)
            {
              this.NPC.localAI[0] = 47f;
              if (FargoSoulsUtil.HostCheck)
              {
                int num4 = 14;
                float num5 = Utils.NextFloat(Main.rand, 6.28318548f);
                for (int index = 0; index < num4; ++index)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(4f, 0.0f), (double) num5 + Math.PI / (double) num4 * 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<ChampionBee>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                break;
              }
              break;
            }
            break;
          case -2f:
            NPC npc1 = this.NPC;
            ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.97f);
            if ((double) this.NPC.ai[1] > 180.0)
            {
              this.NPC.localAI[0] = 0.0f;
              this.NPC.localAI[2] = 2f;
            }
            if ((double) ++this.NPC.ai[1] == 180.0)
            {
              SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (WorldSavingSystem.MasochistModeReal)
              {
                int num = this.NPC.lifeMax / 3 - this.NPC.life;
                this.NPC.life += num;
                CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num, false, false);
              }
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -4f, 0.0f);
                break;
              }
              break;
            }
            if ((double) this.NPC.ai[1] > 240.0)
            {
              this.NPC.ai[0] = -3f;
              this.NPC.ai[1] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case -1f:
            NPC npc2 = this.NPC;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.97f);
            if ((double) this.NPC.ai[1] > 180.0)
              this.NPC.localAI[2] = 1f;
            if ((double) ++this.NPC.ai[1] == 180.0)
            {
              SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (WorldSavingSystem.MasochistModeReal)
              {
                int num = this.NPC.lifeMax - this.NPC.life;
                this.NPC.life += num;
                CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num, false, false);
              }
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -4f, 0.0f);
                break;
              }
              break;
            }
            if ((double) this.NPC.ai[1] > 240.0)
            {
              this.NPC.ai[0] = this.NPC.ai[3];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 0.0f:
          case 3f:
          case 5f:
          case 7f:
          case 10f:
            if (!Main.dayTime || !((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              this.NPC.noTileCollide = true;
              this.NPC.noGravity = true;
              --((Entity) this.NPC).velocity.Y;
              break;
            }
            Vector2 center1 = ((Entity) player).Center;
            center1.Y -= 300f;
            if ((double) ((Entity) this.NPC).Distance(center1) > 50.0)
              this.Movement(center1, 0.18f, 24f, true);
            if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 200.0)
              this.Movement(center1, 0.24f, 24f, true);
            if ((double) ++this.NPC.ai[1] > 150.0)
            {
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
            }
            if ((double) this.NPC.localAI[2] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.66)
            {
              float num = this.NPC.ai[0];
              this.NPC.ai[0] = -1f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = num;
              this.NPC.netUpdate = true;
            }
            if ((double) this.NPC.localAI[2] == 1.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.33 && WorldSavingSystem.EternityMode)
            {
              this.NPC.ai[0] = -2f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 1f:
            NPC npc3 = this.NPC;
            ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, 0.95f);
            if ((double) ++this.NPC.ai[1] > ((double) this.NPC.localAI[2] == 1.0 ? 2.0 : 3.0))
            {
              SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] -= 0.00171859562f * this.NPC.ai[3];
              if ((double) this.NPC.ai[2] < -3.1415927410125732)
                this.NPC.ai[2] += 6.28318548f;
              if (FargoSoulsUtil.HostCheck)
              {
                int num = (double) this.NPC.localAI[2] == 1.0 ? 4 : 3;
                for (int index = 0; index < num; ++index)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(6f, 0.0f), (double) this.NPC.ai[2] + Math.PI / (double) num * 2.0 * (double) index, new Vector2()), ModContent.ProjectileType<ChampionBee>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.ai[3] > 300.0)
            {
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 2f:
          case 8f:
            if ((double) this.NPC.ai[3] == 0.0)
            {
              if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0)
              {
                this.NPC.TargetClosest(false);
                if (this.NPC.timeLeft > 30)
                  this.NPC.timeLeft = 30;
                this.NPC.noTileCollide = true;
                this.NPC.noGravity = true;
                --((Entity) this.NPC).velocity.Y;
                return;
              }
              if ((double) this.NPC.ai[2] == 0.0)
              {
                this.NPC.ai[2] = ((Entity) this.NPC).Center.Y;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 7f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              }
              if ((double) ((Entity) this.NPC).Center.Y > (double) this.NPC.ai[2] + 1000.0)
              {
                Vector2 targetPos;
                // ISSUE: explicit constructor call
                ((Vector2) ref targetPos).\u002Ector(((Entity) player).Center.X, this.NPC.ai[2] + 1100f);
                this.Movement(targetPos, 1.2f, 24f);
                if ((double) Math.Abs(((Entity) player).Center.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2) && (double) ++this.NPC.ai[1] > ((double) this.NPC.localAI[2] == 1.0 ? 30.0 : 60.0))
                {
                  SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                  ++this.NPC.ai[3];
                  this.NPC.ai[1] = 0.0f;
                  this.NPC.netUpdate = true;
                  break;
                }
                break;
              }
              ((Entity) this.NPC).velocity.X *= 0.95f;
              ((Entity) this.NPC).velocity.Y += 0.6f;
              break;
            }
            ((Entity) this.NPC).velocity.X = 0.0f;
            ((Entity) this.NPC).velocity.Y = -36f;
            if ((double) ++this.NPC.ai[1] > 1.0)
            {
              this.NPC.ai[1] = 0.0f;
              this.NPC.localAI[0] = (double) this.NPC.localAI[0] == 1.0 ? -1f : 1f;
              Vector2 velocity = Vector2.op_Multiply(5f, Utils.RotatedBy(Vector2.UnitX, Math.PI * (Main.rand.NextDouble() - 0.5), new Vector2()));
              velocity.X *= this.NPC.localAI[0];
              FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<LesserFairy>(), ((Entity) this.NPC).whoAmI, target: this.NPC.target, velocity: velocity);
            }
            if ((double) ((Entity) this.NPC).Center.Y < (double) ((Entity) player).Center.Y - 600.0)
            {
              ((Entity) this.NPC).velocity.Y *= -0.25f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 4f:
            NPC npc4 = this.NPC;
            ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, 0.9f);
            if ((double) this.NPC.ai[3] == 0.0)
              this.NPC.ai[3] = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -1f : 1f;
            if ((double) ++this.NPC.ai[2] > ((double) this.NPC.localAI[2] == 1.0 ? 40.0 : 60.0))
            {
              this.NPC.ai[2] = 0.0f;
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if ((double) this.NPC.localAI[0] > 0.0)
                this.NPC.localAI[0] = -1f;
              else
                this.NPC.localAI[0] = 1f;
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 center2 = ((Entity) this.NPC).Center;
                center2.X += 1200f * this.NPC.ai[3];
                center2.Y += (float) (1200.0 * -(double) this.NPC.localAI[0]);
                int num6 = (double) this.NPC.localAI[2] == 1.0 ? 30 : 20;
                int num7 = (double) this.NPC.localAI[2] == 1.0 ? 180 : 250;
                center2.Y += Utils.NextFloat(Main.rand, (float) num7);
                for (int index = 0; index < num6; ++index)
                {
                  center2.Y += (float) num7 * this.NPC.localAI[0];
                  Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(center2, ((Entity) this.NPC).Center), 40f);
                  float num8 = ((double) this.NPC.localAI[2] == 1.0 ? 8f : 6f) * -this.NPC.ai[3];
                  float num9 = (float) (6.0 * -(double) this.NPC.localAI[0]);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<ChampionBeetle>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num8, num9, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 440.0)
            {
              this.NPC.localAI[0] = 0.0f;
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 6f:
            NPC npc5 = this.NPC;
            ((Entity) npc5).velocity = Vector2.op_Multiply(((Entity) npc5).velocity, 0.98f);
            if ((double) ++this.NPC.ai[2] > ((double) this.NPC.localAI[2] == 1.0 ? 45.0 : 60.0))
            {
              if ((double) ++this.NPC.ai[3] > ((double) this.NPC.localAI[2] == 1.0 ? 4.0 : 7.0))
              {
                this.NPC.ai[3] = 0.0f;
                if (FargoSoulsUtil.HostCheck)
                {
                  Vector2 vector2 = Vector2.op_Division(Vector2.op_Multiply(2f, Utils.RotatedBy(new Vector2(Utils.NextFloat(Main.rand, 1000f), 0.0f), Main.rand.NextDouble() * (-1.0 * Math.PI), new Vector2())), 60f);
                  float num = (float) (-(double) ((Vector2) ref vector2).Length() / 60.0);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<LifeFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 60f, num, 0.0f);
                }
              }
              if ((double) this.NPC.ai[2] > ((double) this.NPC.localAI[2] == 1.0 ? 120.0 : 100.0))
              {
                this.NPC.netUpdate = true;
                this.NPC.ai[2] = 0.0f;
              }
            }
            if ((double) ++this.NPC.ai[1] > 480.0)
            {
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 9f:
            NPC npc6 = this.NPC;
            ((Entity) npc6).velocity = Vector2.op_Multiply(((Entity) npc6).velocity, 0.95f);
            this.NPC.ai[3] += (float) (6.2831854820251465 / ((double) this.NPC.localAI[2] == 1.0 ? -300.0 : 360.0));
            if ((double) --this.NPC.ai[2] < 0.0)
            {
              this.NPC.ai[2] = 59f;
              if ((double) this.NPC.ai[1] > 90.0)
                this.NPC.localAI[1] = (float) ((double) this.NPC.localAI[1] == 0.0 ? 1 : 0);
              if ((double) this.NPC.ai[1] < 420.0 && FargoSoulsUtil.HostCheck)
              {
                int num10 = (double) this.NPC.localAI[1] == 0.0 ? ModContent.ProjectileType<LifeDeathraySmall>() : ModContent.ProjectileType<LifeDeathray>();
                int num11 = (double) this.NPC.localAI[2] == 1.0 ? 6 : 4;
                for (int index = 0; index < num11; ++index)
                {
                  float num12 = 6.28318548f / (float) num11 * (float) index;
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3] + (double) num12, new Vector2()), num10, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num12, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 450.0)
            {
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 11f:
            NPC npc7 = this.NPC;
            ((Entity) npc7).velocity = Vector2.op_Multiply(((Entity) npc7).velocity, 0.98f);
            if ((double) ++this.NPC.ai[2] > ((double) this.NPC.localAI[2] == 1.0 ? 75.0 : 100.0))
            {
              if ((double) ++this.NPC.ai[3] > 5.0)
              {
                this.NPC.ai[3] = 0.0f;
                SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                if (FargoSoulsUtil.HostCheck)
                {
                  Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
                  vector2_1.X += (float) Main.rand.Next(-75, 76);
                  vector2_1.Y += (float) Main.rand.Next(-75, 76);
                  Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, vector2_1), 90f);
                  float num = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<CactusMine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, num, 0.0f);
                }
              }
              if ((double) this.NPC.ai[2] > 130.0)
              {
                this.NPC.netUpdate = true;
                this.NPC.ai[2] = 0.0f;
              }
            }
            if ((double) ++this.NPC.ai[1] > 480.0)
            {
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0.0f;
        }
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(new Vector2(300f, 200f), this.NPC.scale)), (int) (600.0 * (double) this.NPC.scale), (int) (400.0 * (double) this.NPC.scale), 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() <= 1.0 || (double) this.NPC.ai[0] == 2.0 || (double) this.NPC.ai[0] == 8.0 || !this.NPC.HasValidTarget)
          return;
        ((Entity) this.NPC).position.Y += ((Entity) player).velocity.Y / 3f;
      }
    }

    public virtual void UpdateLifeRegen(ref int damage)
    {
      if (this.NPC.lifeRegen >= 0)
        return;
      this.NPC.lifeRegen /= 2;
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 4.0)
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y >= frameHeight * Main.npcFrameCount[this.NPC.type])
        this.NPC.frame.Y = 0;
      if (this.NPC.IsABestiaryIconDummy)
        return;
      this.NPC.rotation = MathHelper.WrapAngle(this.NPC.rotation + (float) (6.2831854820251465 / (double) Main.npcFrameCount[this.NPC.type] / 2.0));
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f, bool fastY = false)
    {
      if ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X)
      {
        ((Entity) this.NPC).velocity.X += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X < 0.0)
          ((Entity) this.NPC).velocity.X += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.X -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.X > 0.0)
          ((Entity) this.NPC).velocity.X -= speedModifier * 2f;
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.NPC).velocity.Y += fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= fastY ? speedModifier * 2f : speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 10f);
      if (((double) this.NPC.localAI[2] != 0.0 || (double) this.NPC.life >= (double) this.NPC.lifeMax * 0.66) && ((double) this.NPC.localAI[2] != 1.0 || (double) this.NPC.life >= (double) this.NPC.lifeMax * 0.33 || !WorldSavingSystem.EternityMode))
        return;
      ((NPC.HitModifiers) ref modifiers).SetMaxDamage(1);
      ((NPC.HitModifiers) ref modifiers).DisableCrit();
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<PurifiedBuff>(), 300, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life <= 0)
      {
        for (int index = 1; index <= 4; ++index)
        {
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
          if (!Main.dedServ)
          {
            IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
            Vector2 vector2_2 = vector2_1;
            Vector2 velocity = ((Entity) this.NPC).velocity;
            string name = ((ModType) this).Mod.Name;
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(8, 1);
            interpolatedStringHandler.AppendLiteral("LifeGore");
            interpolatedStringHandler.AppendFormatted<int>(index);
            string stringAndClear = interpolatedStringHandler.ToStringAndClear();
            int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
            double scale = (double) this.NPC.scale;
            Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
          }
        }
      }
      if (!Main.remixWorld || !Utils.NextBool(Main.rand, 10))
        return;
      int num = 781;
      if (!FargoSoulsUtil.HostCheck)
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).position, ((Entity) this.NPC).Size, num, 1, false, 0, false, false);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[4], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<LifeForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<LifeChampionRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<LifeDye>(), 1, 1, 1));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if (this.NPC.alpha != 0 && !this.NPC.IsABestiaryIconDummy)
        return false;
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color color1 = drawColor;
      Color color2 = this.NPC.IsABestiaryIconDummy ? Color.White : this.NPC.GetAlpha(color1);
      SpriteEffects spriteEffects = (SpriteEffects) 0;
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color2, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      int num1 = this.NPC.frame.Y / (texture2D1.Height / Main.npcFrameCount[this.NPC.type]);
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Life/LifeChampion_Wings", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Life/LifeChampion_WingsGlow", (AssetRequestMode) 1).Value;
      int num2 = texture2D2.Height / Main.npcFrameCount[this.NPC.type];
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, num1 * num2, texture2D2.Width, num2);
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(rectangle), 2f);
      Color color3 = Color.White;
      if (!this.NPC.IsABestiaryIconDummy)
        color3 = Color.op_Multiply(color3, this.NPC.Opacity);
      float num3 = (float) (2.0 * (double) this.NPC.scale * (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.10000000149011612 + 0.949999988079071));
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Vector2 oldPo = this.NPC.oldPos[index];
        float num4 = 0.0f;
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color3, 0.5f / (float) index), num4, vector2_2, num3, spriteEffects, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(new Color(1f, 0.647f, 0.839f)).UseSecondaryColor(Color.CornflowerBlue);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData));
        ((DrawData) ref drawData).Draw(spriteBatch);
      }
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), color3, 0.0f, vector2_2, this.NPC.scale * 2f, spriteEffects, 0.0f);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      DrawData drawData1;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData1).\u002Ector(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(rectangle), Color.op_Multiply(color3, 0.5f), 0.0f, vector2_2, this.NPC.scale * 2f, spriteEffects, 0.0f);
      GameShaders.Misc["LCWingShader"].UseColor(new Color(1f, 0.647f, 0.839f)).UseSecondaryColor(Color.Goldenrod);
      GameShaders.Misc["LCWingShader"].Apply(new DrawData?(drawData1));
      ((DrawData) ref drawData1).Draw(spriteBatch);
      return false;
    }

    public virtual void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      if ((double) this.NPC.ai[0] == 9.0 && (double) this.NPC.ai[1] < 420.0)
        return;
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/Effects/LifeStar", (AssetRequestMode) 1).Value;
      Rectangle rectangle;
      // ISSUE: explicit constructor call
      ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
      float num = (double) this.NPC.localAI[3] == 0.0 ? this.NPC.ai[2] * Utils.NextFloat(Main.rand, 1f, 2.5f) : (Main.cursorScale + 0.3f) * Utils.NextFloat(Main.rand, 0.8f, 1.2f);
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) (texture2D.Width / 2) + num, (float) (texture2D.Height / 2) + num);
      spriteBatch.Draw(texture2D, Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Rectangle?(rectangle), Color.HotPink, 0.0f, vector2, num, (SpriteEffects) 0, 0.0f);
      DrawData drawData;
      // ISSUE: explicit constructor call
      ((DrawData) ref drawData).\u002Ector(texture2D, Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Rectangle?(rectangle), Color.White, 0.0f, vector2, num, (SpriteEffects) 0, 0.0f);
      GameShaders.Misc["LCWingShader"].UseColor(Color.Goldenrod).UseSecondaryColor(Color.HotPink);
      GameShaders.Misc["LCWingShader"].Apply(new DrawData?());
      ((DrawData) ref drawData).Draw(spriteBatch);
      if (this.NPC.IsABestiaryIconDummy)
        return;
      spriteBatch.End();
      spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
    }
  }
}
