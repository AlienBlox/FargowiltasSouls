// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Shadow.ShadowChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.BossBars;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Shadow
{
  [AutoloadBossHead]
  public class ShadowChampion : ModNPC
  {
    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 5;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 6);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 24;
      int num4 = num3 + 1;
      span[num4] = 68;
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<LethargicBuff>();
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<ClippedWingsBuff>();
      int num7 = num6 + 1;
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Position = new Vector2(32f, -8f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(0.0f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[4]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCrimson,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.TheCorruption,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 110;
      ((Entity) this.NPC).height = 110;
      this.NPC.damage = 130;
      this.NPC.defense = 60;
      this.NPC.lifeMax = 330000;
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
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.dontTakeDamage = true;
      this.NPC.BossBar = (IBigProgressBar) ModContent.GetInstance<CompositeBossBar>();
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(false);
        this.Movement(((Entity) Main.player[this.NPC.target]).Center, 0.8f, 32f);
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) >= 1500.0)
          return;
        this.NPC.localAI[3] = 1f;
        if (FargoSoulsUtil.HostCheck)
        {
          float num1 = 0.7853982f;
          for (int index = 0; index < 8; ++index)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(110f, 0.0f), (double) num1 * (double) index, new Vector2()));
            int num2 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) vector2.X, (int) vector2.Y, ModContent.NPCType<ShadowOrbNPC>(), 0, (float) ((Entity) this.NPC).whoAmI, 110f, 0.0f, num1 * (float) index, (int) byte.MaxValue);
            if (Main.netMode == 2 && num2 < 200)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, num2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
      }
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      Player player = Main.player[this.NPC.target];
      if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0 && !Main.dayTime)
        this.NPC.timeLeft = 600;
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      if ((double) this.NPC.localAI[3] == 1.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * (WorldSavingSystem.EternityMode ? 0.66 : 5.0))
      {
        this.NPC.localAI[3] = 2f;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
        float num3 = this.NPC.ai[0];
        this.NPC.ai[0] = -1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = num3;
        this.NPC.ai[3] = 0.0f;
        if (FargoSoulsUtil.HostCheck)
        {
          float num4 = 0.3926991f;
          for (int index = 0; index < 16; ++index)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(700f, 0.0f), (double) num4 * (double) index, new Vector2()));
            int num5 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) vector2.X, (int) vector2.Y, ModContent.NPCType<ShadowOrbNPC>(), 0, (float) ((Entity) this.NPC).whoAmI, 700f, 0.0f, num4 * (float) index, (int) byte.MaxValue);
            if (Main.netMode == 2 && num5 < 200)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, num5, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == ModContent.ProjectileType<ShadowClone>())
            Main.projectile[index].Kill();
        }
      }
      else if ((double) this.NPC.localAI[3] == 2.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.33 && WorldSavingSystem.EternityMode)
      {
        this.NPC.localAI[3] = 3f;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
        float num6 = this.NPC.ai[0];
        this.NPC.ai[0] = -1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = num6;
        this.NPC.ai[3] = 0.0f;
        if (FargoSoulsUtil.HostCheck)
        {
          float num7 = 0.2617994f;
          for (int index = 0; index < 24; ++index)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(350f, 0.0f), (double) num7 * (double) index, new Vector2()));
            int num8 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) vector2.X, (int) vector2.Y, ModContent.NPCType<ShadowOrbNPC>(), 0, (float) ((Entity) this.NPC).whoAmI, 350f, 0.0f, num7 * (float) index, (int) byte.MaxValue);
            if (Main.netMode == 2 && num8 < 200)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, num8, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == ModContent.ProjectileType<ShadowClone>())
            Main.projectile[index].Kill();
        }
      }
      if (this.NPC.dontTakeDamage && (double) this.NPC.ai[0] != -1.0 && !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.type == ModContent.NPCType<ShadowOrbNPC>() && (double) n.ai[0] == (double) ((Entity) this.NPC).whoAmI && !n.dontTakeDamage)))
      {
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 80; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitX, 40f), (double) (index1 - 39) * 6.2831854820251465 / 80.0, new Vector2()), ((Entity) this.NPC).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center);
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 27, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity = vector2_2;
        }
        this.NPC.dontTakeDamage = false;
      }
      switch (this.NPC.ai[0])
      {
        case -1f:
          this.NPC.dontTakeDamage = true;
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
          if ((double) this.NPC.ai[1] == 120.0)
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if ((double) ++this.NPC.ai[3] > 9.0 && (double) this.NPC.ai[1] > 120.0)
          {
            this.NPC.ai[3] = 0.0f;
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<ShadowOrbNPC>() && (double) Main.npc[index].ai[0] == (double) ((Entity) this.NPC).whoAmI)
                {
                  Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.npc[index]).Center), Math.PI / 2.0, new Vector2());
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) Main.npc[index]).Center, vector2, 44, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] > 300.0)
          {
            this.NPC.TargetClosest(true);
            this.NPC.ai[0] = this.NPC.ai[2];
            if ((double) this.NPC.ai[0] % 2.0 == 1.0)
              --this.NPC.ai[0];
            if ((double) this.NPC.ai[0] == 6.0)
              this.NPC.ai[0] += 2f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        case 0.0f:
        case 2f:
        case 4f:
        case 8f:
          if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0 || Main.dayTime)
          {
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            --((Entity) this.NPC).velocity.Y;
            break;
          }
          Vector2 targetPos1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos1) > 50.0)
            this.Movement(targetPos1, 0.2f, 24f);
          if ((double) ++this.NPC.ai[1] > 60.0)
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
        case 1f:
          Vector2 targetPos2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos2) > 50.0)
            this.Movement(targetPos2, 0.2f, 24f);
          Dust dust1 = Main.dust[Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 2f)];
          dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
          if ((double) this.NPC.ai[1] == 90.0 && FargoSoulsUtil.HostCheck)
          {
            for (int index3 = -1; index3 <= 1; ++index3)
            {
              if (index3 != 0)
              {
                Vector2 vector2_3 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply((float) index3, (double) this.NPC.localAI[3] == 2.0 ? Vector2.op_Multiply(Vector2.UnitY, 1000f) : Vector2.op_Multiply(Vector2.UnitX, 1000f)));
                for (int index4 = -1; index4 <= 1; ++index4)
                {
                  Vector2 vector2_4 = Utils.RotatedBy(Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_3)), (double) MathHelper.ToRadians(25f) * (double) index4, new Vector2());
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_3, vector2_4, ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) this.NPC.target, -1f, 0.0f);
                }
                if ((double) this.NPC.localAI[3] == 3.0)
                {
                  Vector2 vector2_5 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply((float) index3, Vector2.UnitY), 1000f));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_5, Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_5)), ModContent.ProjectileType<GuardianDeathraySmall>(), 0, 0.0f, Main.myPlayer, (float) this.NPC.target, -1f, 0.0f);
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[2] > 5.0 && (double) this.NPC.ai[1] > 120.0)
          {
            this.NPC.ai[2] = 0.0f;
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index5 = -1; index5 <= 1; ++index5)
              {
                if (index5 != 0)
                {
                  Vector2 vector2_6 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply((float) index5, (double) this.NPC.localAI[3] == 2.0 ? Vector2.op_Multiply(Vector2.UnitY, 1000f) : Vector2.op_Multiply(Vector2.UnitX, 1000f)));
                  for (int index6 = -1; index6 <= 1; ++index6)
                  {
                    Vector2 vector2_7 = Utils.RotatedBy(Utils.RotatedBy(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 20f, 25f), Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_6))), (double) MathHelper.ToRadians(25f) * (double) index6, new Vector2()), (double) MathHelper.ToRadians(5f) * (Main.rand.NextDouble() - 0.5), new Vector2());
                    if (index6 != 0)
                      vector2_7 = Vector2.op_Multiply(vector2_7, 1.75f);
                    int index7 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_6, vector2_7, ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    if (index7 != Main.maxProjectiles)
                      Main.projectile[index7].timeLeft = 240;
                  }
                  if ((double) this.NPC.localAI[3] == 3.0)
                  {
                    Vector2 vector2_8 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Vector2.op_Multiply((float) index5, Vector2.UnitY), 1000f));
                    int index8 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_8, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 20f, 25f), Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, vector2_8))), ModContent.ProjectileType<ShadowGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    if (index8 != Main.maxProjectiles)
                      Main.projectile[index8].timeLeft = 240;
                  }
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] == 120.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            break;
          }
          if ((double) this.NPC.ai[1] > 300.0)
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
        case 3f:
          Vector2 targetPos3 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 600f));
          if ((double) ((Entity) this.NPC).Distance(targetPos3) > 50.0)
            this.Movement(targetPos3, 0.1f, 24f);
          if ((double) this.NPC.localAI[3] == 2.0)
            this.NPC.ai[2] += 0.5f;
          if ((double) ++this.NPC.ai[2] > 60.0)
          {
            this.NPC.ai[2] = 0.0f;
            SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              if ((double) this.NPC.localAI[3] == 3.0)
              {
                for (int index9 = 0; index9 < 3; ++index9)
                {
                  for (int index10 = 0; index10 < 20; ++index10)
                  {
                    Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (Main.rand.NextDouble() - 0.5) + 2.0 * Math.PI / 3.0 * (double) index9, new Vector2());
                    float num9 = Utils.NextFloat(Main.rand, 1.04f, 1.06f);
                    float num10 = Utils.NextFloat(Main.rand, 0.05f);
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<ShadowFlameburst>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, num9, num10, 0.0f);
                  }
                }
              }
              else if ((double) this.NPC.localAI[3] == 2.0)
              {
                for (int index11 = -1; index11 <= 1; ++index11)
                {
                  if (index11 != 0)
                  {
                    for (int index12 = 0; index12 < 25; ++index12)
                    {
                      Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (Main.rand.NextDouble() - 0.5) + Math.PI / 2.0 * (double) index11, new Vector2());
                      float num11 = Utils.NextFloat(Main.rand, 1.04f, 1.06f);
                      float num12 = Utils.NextFloat(Main.rand, 0.06f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<ShadowFlameburst>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, num11, num12, 0.0f);
                    }
                  }
                }
              }
              else
              {
                for (int index = 0; index < 40; ++index)
                {
                  Vector2 vector2 = Vector2.op_Multiply(3f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (Main.rand.NextDouble() - 0.5), new Vector2()));
                  float num13 = 0.0075f;
                  float num14 = Utils.NextFloat(Main.rand, 1.04f, 1.06f);
                  float num15 = Utils.NextFloat(Main.rand, -num13, num13);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<ShadowFlameburst>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, num14, num15, 0.0f);
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] > 300.0)
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
        case 5f:
          Vector2 targetPos4 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos4) > 50.0)
            this.Movement(targetPos4, 0.3f, 24f);
          if ((double) ++this.NPC.ai[2] > ((double) this.NPC.localAI[3] > 1.0 ? 90.0 : 120.0) && (double) this.NPC.ai[1] < 330.0)
          {
            this.NPC.ai[2] = 0.0f;
            SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center), 30f);
              if ((double) this.NPC.localAI[3] == 3.0)
              {
                vector2 = Vector2.op_Multiply(Utils.RotatedBy(vector2, Math.PI / 2.0, new Vector2()), 0.75f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2), ModContent.ProjectileType<ShadowOrbProjectile>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<ShadowOrbProjectile>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > ((double) this.NPC.localAI[3] == 3.0 ? 450.0 : 420.0))
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
        case 6f:
          if ((double) this.NPC.ai[1] == 1.0 && FargoSoulsUtil.HostCheck)
          {
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -5f, 0.0f);
            goto case 0.0f;
          }
          else
            goto case 0.0f;
        case 7f:
          if ((double) ++this.NPC.ai[2] == 1.0)
          {
            SoundEngine.PlaySound(ref SoundID.NPCHit6, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center), 30f), (float) (1.0 + (double) this.NPC.localAI[3] / 3.0 * 0.75));
            this.NPC.netUpdate = true;
          }
          else if ((double) this.NPC.ai[2] == 31.0)
          {
            ((Entity) this.NPC).velocity = Vector2.Zero;
            this.NPC.netUpdate = true;
          }
          else if ((double) this.NPC.ai[2] == 38.0)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 vector2_9 = Utils.RotatedByRandom(new Vector2(12f, 0.0f), 2.0 * Math.PI);
              for (int index = 0; index < 20; ++index)
              {
                Vector2 vector2_10 = Utils.RotatedBy(vector2_9, Math.PI / 3.0 * ((double) index + Main.rand.NextDouble() - 0.5), new Vector2());
                float num16 = (float) Main.rand.Next(10, 80) * (1f / 1000f);
                if (Utils.NextBool(Main.rand))
                  num16 *= -1f;
                float num17 = (float) Main.rand.Next(10, 80) * (1f / 1000f);
                if (Utils.NextBool(Main.rand))
                  num17 *= -1f;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_10, 496, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num17, num16, 0.0f);
              }
            }
          }
          else if ((double) this.NPC.ai[2] > 60.0)
            this.NPC.ai[2] = 0.0f;
          if ((double) ++this.NPC.ai[1] > 330.0)
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
          Vector2 targetPos5 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos5) > 50.0)
            this.Movement(targetPos5, 0.3f, 24f);
          if ((double) this.NPC.ai[2] == 0.0)
          {
            this.NPC.ai[2] = 1f;
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index13 = 0; index13 < 10; ++index13)
              {
                if ((double) this.NPC.localAI[3] != 1.0 || index13 % 2 != 0)
                {
                  for (int index14 = 0; index14 < ((double) this.NPC.localAI[3] == 3.0 ? 2 : 1); ++index14)
                  {
                    Vector2 vector2_11 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 500f, 700f), Utils.RotatedBy(Vector2.UnitX, Main.rand.NextDouble() * 2.0 * Math.PI, new Vector2())));
                    Vector2 vector2_12 = Utils.RotatedBy(((Entity) this.NPC).velocity, Main.rand.NextDouble() * Math.PI * 2.0, new Vector2());
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_11, vector2_12, ModContent.ProjectileType<ShadowClone>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, (float) (60 + 30 * index13), 0.0f);
                  }
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] > 360.0)
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
      if (!this.NPC.dontTakeDamage)
        return;
      for (int index15 = 0; index15 < 3; ++index15)
      {
        int index16 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 27, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index16].noGravity = true;
        Dust dust2 = Main.dust[index16];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4f);
      }
      for (int index17 = 0; index17 < 3; ++index17)
      {
        int index18 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 54, 0.0f, 0.0f, 0, new Color(), 5f);
        Main.dust[index18].noGravity = true;
      }
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 3.0)
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y < frameHeight * 5)
        return;
      this.NPC.frame.Y = 0;
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

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(22, 300, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<ShadowflameBuff>(), 300, true, false);
      target.AddBuff(80, 300, true, false);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[5], -1);
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == ModContent.ProjectileType<ShadowClone>())
          Main.projectile[index].Kill();
      }
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<ShadowForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<ShadowChampionRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<PortableFogMachine>(), 4));
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.dontTakeDamage && !this.NPC.IsABestiaryIconDummy ? new Color?(Color.Black) : new Color?();
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Shadow/ShadowChampion_Trail", (AssetRequestMode) 1).Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      GameShaders.Armor.GetShaderFromItemId(3530).Apply((Entity) this.NPC, new DrawData?());
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(Color.White, 0.25f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), alpha, this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        Main.spriteBatch.End();
        Main.spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;
    }
  }
}
