// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.NPCs;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.MutantBoss
{
  [AutoloadBossHead]
  public class MutantBoss : ModNPC
  {
    public bool playerInvulTriggered;
    public int ritualProj;
    public int spriteProj;
    public int ringProj;
    private bool droppedSummon;
    public Queue<float> attackHistory = new Queue<float>();
    public int attackCount;
    public int hyper;
    public float endTimeVariance;
    public bool ShouldDrawAura;
    public float AuraScale = 1f;
    public Vector2 AuraCenter = Vector2.Zero;
    private string TownNPCName;
    public const int HyperMax = 5;
    private bool spawned;
    private const int ObnoxiousQuoteCount = 71;
    private const string GFBLocPath = "Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.";
    private const int MUTANT_SWORD_SPACING = 80;
    private const int MUTANT_SWORD_MAX = 12;

    public virtual string Texture
    {
      get
      {
        return "FargowiltasSouls/Content/Bosses/MutantBoss/MutantBoss" + FargoSoulsUtil.TryAprilFoolsTexture;
      }
    }

    private Player player => Main.player[this.NPC.target];

    public ref float AttackChoice => ref this.NPC.ai[0];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.MustAlwaysDraw[this.Type] = true;
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 13);
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
      span[num7] = ModContent.BuffType<MutantNibbleBuff>();
      int num8 = num7 + 1;
      span[num8] = ModContent.BuffType<OceanicMaulBuff>();
      int num9 = num8 + 1;
      span[num9] = ModContent.BuffType<LightningRodBuff>();
      int num10 = num9 + 1;
      span[num10] = ModContent.BuffType<SadismBuff>();
      int num11 = num10 + 1;
      span[num11] = ModContent.BuffType<GodEaterBuff>();
      int num12 = num11 + 1;
      span[num12] = ModContent.BuffType<TimeFrozenBuff>();
      int num13 = num12 + 1;
      span[num13] = ModContent.BuffType<LeadPoisonBuff>();
      int num14 = num13 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 120;
      ((Entity) this.NPC).height = 120;
      if (Main.getGoodWorld)
      {
        ((Entity) this.NPC).width = 20;
        ((Entity) this.NPC).height = 42;
      }
      this.NPC.damage = 444;
      this.NPC.defense = (int) byte.MaxValue;
      this.NPC.value = (float) Item.buyPrice(7, 0, 0, 0);
      this.NPC.lifeMax = Main.expertMode ? 7700000 : 3500000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit57);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.npcSlots = 50f;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.netAlways = true;
      this.NPC.timeLeft = NPC.activeTime * 30;
      if (WorldSavingSystem.AngryMutant)
      {
        this.NPC.damage *= 17;
        this.NPC.defense *= 10;
      }
      Mod mod;
      if (Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
        this.Music = MusicLoader.GetMusicSlot(mod, WorldSavingSystem.MasochistModeReal ? "Assets/Music/rePrologue" : "Assets/Music/SteelRed");
      else
        this.Music = 83;
      this.SceneEffectPriority = (SceneEffectPriority) 8;
      if (!FargoSoulsUtil.AprilFools)
        return;
      this.NPC.GivenName = Language.GetTextValue("Mods.FargowiltasSouls.NPCs.MutantBoss_April.DisplayName");
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.damage = (int) Math.Round((double) this.NPC.damage * 0.5);
      this.NPC.lifeMax = (int) Math.Round((double) this.NPC.lifeMax * 0.5 * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return WorldSavingSystem.MasochistModeReal && (double) ((Entity) this.NPC).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) target, ((Entity) this.NPC).Center)) < 42.0 && (double) this.AttackChoice > -1.0;
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return target.type != ModContent.NPCType<Deviantt>() && target.type != ModContent.NPCType<Abominationn>() && target.type != ModContent.NPCType<Mutant>() && base.CanHitNPC(target);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.endTimeVariance);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.endTimeVariance = reader.ReadSingle();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      ModNPC modNpc;
      if (ModContent.TryFind<ModNPC>("Fargowiltas", "Mutant", ref modNpc))
      {
        int firstNpc = NPC.FindFirstNPC(modNpc.Type);
        if (firstNpc != -1 && firstNpc != Main.maxNPCs)
        {
          ((Entity) this.NPC).Bottom = ((Entity) Main.npc[firstNpc]).Bottom;
          this.TownNPCName = Main.npc[firstNpc].GivenName;
          Main.npc[firstNpc].life = 0;
          ((Entity) Main.npc[firstNpc]).active = false;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, firstNpc, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      this.AuraCenter = ((Entity) this.NPC).Center;
    }

    public virtual bool PreAI()
    {
      if (WorldSavingSystem.MasochistModeReal && !Main.dedServ && !Main.LocalPlayer.ItemTimeIsZero && (Main.LocalPlayer.HeldItem.type == 1326 || Main.LocalPlayer.HeldItem.type == 5335))
        Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), 600, true, false);
      return base.PreAI();
    }

    public virtual void AI()
    {
      EModeGlobalNPC.mutantBoss = ((Entity) this.NPC).whoAmI;
      this.NPC.dontTakeDamage = (double) this.AttackChoice < 0.0;
      this.ShouldDrawAura = false;
      this.ManageAurasAndPreSpawn();
      this.ManageNeededProjectiles();
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) this.player).Center.X ? 1 : -1;
      bool flag = true;
      switch (this.AttackChoice)
      {
        case -7f:
          this.DyingAnimationAndHandling();
          break;
        case -6f:
          this.DyingDramaticPause();
          break;
        case -5f:
          this.FinalSpark();
          break;
        case -4f:
          this.BoundaryBulletHellP3();
          break;
        case -3f:
          this.OkuuSpheresP3();
          break;
        case -2f:
          this.VoidRaysP3();
          break;
        case -1f:
          flag = this.Phase3Transition();
          break;
        case 0.0f:
          this.SpearTossDirectP1AndChecks();
          break;
        case 1f:
          this.OkuuSpheresP1();
          break;
        case 2f:
          this.PrepareTrueEyeDiveP1();
          break;
        case 3f:
        case 32f:
          this.TrueEyeDive();
          break;
        case 4f:
          this.PrepareSpearDashDirectP1();
          break;
        case 5f:
          this.SpearDashDirectP1();
          break;
        case 6f:
          this.WhileDashingP1();
          break;
        case 7f:
          this.ApproachForNextAttackP1();
          break;
        case 8f:
          this.VoidRaysP1();
          break;
        case 9f:
          this.BoundaryBulletHellAndSwordP1();
          break;
        case 10f:
          this.Phase2Transition();
          break;
        case 11f:
        case 16f:
          this.ApproachForNextAttackP2();
          break;
        case 12f:
          this.VoidRaysP2();
          break;
        case 13f:
          this.PrepareSpearDashPredictiveP2();
          break;
        case 14f:
          this.SpearDashPredictiveP2();
          break;
        case 15f:
          this.WhileDashingP2();
          break;
        case 17f:
          this.BoundaryBulletHellP2();
          break;
        case 18f:
          ++this.AttackChoice;
          break;
        case 19f:
          this.PillarDunk();
          break;
        case 20f:
          this.EOCStarSickles();
          break;
        case 21f:
          this.PrepareSpearDashDirectP2();
          break;
        case 22f:
          this.SpearDashDirectP2();
          break;
        case 23f:
          if ((double) this.NPC.ai[1] % 3.0 == 0.0)
          {
            ++this.NPC.ai[1];
            goto case 15f;
          }
          else
            goto case 15f;
        case 24f:
          this.SpawnDestroyersForPredictiveThrow();
          break;
        case 25f:
          this.SpearTossPredictiveP2();
          break;
        case 26f:
          this.PrepareMechRayFan();
          break;
        case 27f:
          this.MechRayFan();
          break;
        case 28f:
          ++this.AttackChoice;
          break;
        case 29f:
          this.PrepareFishron1();
          break;
        case 30f:
        case 38f:
          this.SpawnFishrons();
          break;
        case 31f:
          this.PrepareTrueEyeDiveP2();
          break;
        case 33f:
          this.PrepareNuke();
          break;
        case 34f:
          this.Nuke();
          break;
        case 35f:
          this.PrepareSlimeRain();
          break;
        case 36f:
          this.SlimeRain();
          break;
        case 37f:
          this.PrepareFishron2();
          break;
        case 39f:
          this.PrepareOkuuSpheresP2();
          break;
        case 40f:
          this.OkuuSpheresP2();
          break;
        case 41f:
          this.SpearTossDirectP2();
          break;
        case 42f:
          this.PrepareTwinRangsAndCrystals();
          break;
        case 43f:
          this.TwinRangsAndCrystals();
          break;
        case 44f:
          this.EmpressSwordWave();
          break;
        case 45f:
          this.PrepareMutantSword();
          break;
        case 46f:
          this.MutantSword();
          break;
        case 52f:
          this.P2NextAttackPause();
          break;
        default:
          this.AttackChoice = 11f;
          goto case 11f;
      }
      this.AuraScale = (double) this.AttackChoice != 1.0 ? ((double) this.AttackChoice == 5.0 || (double) this.AttackChoice == 6.0 ? MathHelper.Lerp(this.AuraScale, 1.25f, 0.1f) : MathHelper.Lerp(this.AuraScale, 1f, 0.1f)) : MathHelper.Lerp(this.AuraScale, 0.7f, 0.02f);
      if (!WorldSavingSystem.MasochistModeReal || (double) this.AttackChoice != 5.0 && (double) this.AttackChoice != 6.0)
        this.AuraCenter = Vector2.Lerp(this.AuraCenter, ((Entity) this.NPC).Center, 0.3f);
      if (WorldSavingSystem.EternityMode && ((double) this.AttackChoice < 0.0 || (double) this.AttackChoice > 10.0 || (double) this.AttackChoice == 10.0 && (double) this.NPC.ai[1] > 150.0))
      {
        Main.dayTime = false;
        Main.time = 16200.0;
        Main.raining = false;
        Main.rainTime = 0.0;
        Main.maxRaining = 0.0f;
        Main.bloodMoon = false;
      }
      if ((((double) this.AttackChoice >= 0.0 ? 0 : (this.NPC.life > 1 ? 1 : 0)) & (flag ? 1 : 0)) != 0)
      {
        int num = 2580;
        if (WorldSavingSystem.MasochistModeReal)
          num = Main.getGoodWorld ? 5000 : 4350;
        this.NPC.life -= this.NPC.lifeMax / num;
        if (this.NPC.life < 1)
          this.NPC.life = 1;
      }
      if (this.player.immune || this.player.hurtCooldowns[0] != 0 || this.player.hurtCooldowns[1] != 0)
        this.playerInvulTriggered = true;
      if (WorldSavingSystem.EternityMode && WorldSavingSystem.DownedAbom && !WorldSavingSystem.DownedMutant && FargoSoulsUtil.HostCheck && this.NPC.HasPlayerTarget && !this.droppedSummon)
      {
        Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.player).Hitbox, ModContent.ItemType<MutantsCurse>(), 1, false, 0, false, false);
        this.droppedSummon = true;
      }
      if (!WorldSavingSystem.MasochistModeReal || !Main.getGoodWorld || ++this.hyper <= 6)
        return;
      this.hyper = 0;
      this.NPC.AI();
    }

    private void ManageAurasAndPreSpawn()
    {
      if (!this.spawned)
      {
        this.spawned = true;
        int lifeMax = this.NPC.lifeMax;
        if (WorldSavingSystem.AngryMutant)
        {
          this.NPC.lifeMax *= 100;
          if (this.NPC.lifeMax < lifeMax)
            this.NPC.lifeMax = int.MaxValue;
        }
        this.NPC.life = this.NPC.lifeMax;
        if (this.player.FargoSouls().TerrariaSoul && WorldSavingSystem.MasochistModeReal)
          this.EdgyBossText(this.GFBQuote(1));
      }
      if (WorldSavingSystem.MasochistModeReal && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2, true, false);
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(true);
        if (this.NPC.timeLeft < 30)
          this.NPC.timeLeft = 30;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) >= 1500.0)
          return;
        this.NPC.localAI[3] = 1f;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.EdgyBossText(this.GFBQuote(2));
        if (!FargoSoulsUtil.HostCheck || !WorldSavingSystem.AngryMutant || !WorldSavingSystem.MasochistModeReal)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<BossRush>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      }
      else if ((double) this.NPC.localAI[3] == 1.0)
      {
        this.ShouldDrawAura = true;
        FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss.ArenaAura(this.AuraCenter, (float) (2000.0 * (double) this.AuraScale), true, -1, new Color(), ModContent.BuffType<GodEaterBuff>(), ModContent.BuffType<MutantFangBuff>());
      }
      else
      {
        if (!((Entity) Main.LocalPlayer).active || (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) >= 3000.0)
          return;
        if (Main.expertMode)
        {
          Main.LocalPlayer.AddBuff(ModContent.BuffType<MutantPresenceBuff>(), 2, true, false);
          if (Main.getGoodWorld)
            Main.LocalPlayer.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 2, true, false);
        }
        if (!WorldSavingSystem.EternityMode || (double) this.AttackChoice >= 0.0 || (double) this.AttackChoice <= -6.0)
          return;
        Main.LocalPlayer.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 2, true, false);
        if (!WorldSavingSystem.MasochistModeReal)
          return;
        Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeStopCDBuff>(), 2, true, false);
      }
    }

    private void ManageNeededProjectiles()
    {
      if (!FargoSoulsUtil.HostCheck)
        return;
      if (WorldSavingSystem.EternityMode && (double) this.AttackChoice != -7.0 && ((double) this.AttackChoice < 0.0 || (double) this.AttackChoice > 10.0))
      {
        if (FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
        {
          ModContent.ProjectileType<MutantRitual>()
        }) == null)
          this.ritualProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }
      if (FargoSoulsUtil.ProjectileExists(this.ringProj, new int[1]
      {
        ModContent.ProjectileType<MutantRitual5>()
      }) == null)
        this.ringProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual5>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      if (FargoSoulsUtil.ProjectileExists(this.spriteProj, new int[1]
      {
        ModContent.ProjectileType<MutantBossProjectile>()
      }) != null)
        return;
      if (Main.netMode == 0)
      {
        int index1 = 0;
        for (int index2 = 999; index2 >= 0; --index2)
        {
          if (!((Entity) Main.projectile[index2]).active)
          {
            index1 = index2;
            break;
          }
        }
        if (index1 < 0)
          return;
        Projectile projectile = Main.projectile[index1];
        projectile.SetDefaults(ModContent.ProjectileType<MutantBossProjectile>());
        ((Entity) projectile).Center = ((Entity) this.NPC).Center;
        projectile.owner = Main.myPlayer;
        ((Entity) projectile).velocity.X = 0.0f;
        ((Entity) projectile).velocity.Y = 0.0f;
        projectile.damage = 0;
        projectile.knockBack = 0.0f;
        projectile.identity = index1;
        projectile.gfxOffY = 0.0f;
        projectile.stepSpeed = 1f;
        projectile.ai[1] = (float) ((Entity) this.NPC).whoAmI;
        this.spriteProj = index1;
      }
      else
        this.spriteProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantBossProjectile>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
    }

    private void ChooseNextAttack(params int[] args)
    {
      float num1 = this.AttackChoice + 1f;
      this.AttackChoice = 52f;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = num1;
      this.NPC.ai[3] = 0.0f;
      this.NPC.localAI[0] = 0.0f;
      this.NPC.localAI[1] = 0.0f;
      this.NPC.localAI[2] = 0.0f;
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.RandomObnoxiousQuote());
      if (WorldSavingSystem.EternityMode)
      {
        bool flag1 = (double) this.NPC.localAI[3] >= 3.0 && (WorldSavingSystem.MasochistModeReal || (double) Utils.NextFloat(Main.rand, 0.8f) + 0.20000000298023224 > Math.Pow((double) this.NPC.life / (double) this.NPC.lifeMax, 2.0));
        if (FargoSoulsUtil.HostCheck)
        {
          Queue<float> floatQueue = new Queue<float>((IEnumerable<float>) this.attackHistory);
          if (flag1)
            this.NPC.ai[2] = (float) Utils.Next<int>(Main.rand, args);
          while (floatQueue.Count > 0)
          {
            bool flag2 = false;
            for (int index = 0; index < 5; ++index)
            {
              if (!floatQueue.Contains(this.NPC.ai[2]))
              {
                flag2 = true;
                break;
              }
              this.NPC.ai[2] = (float) Utils.Next<int>(Main.rand, args);
            }
            if (!flag2)
            {
              double num2 = (double) floatQueue.Dequeue();
            }
            else
              break;
          }
        }
      }
      if (FargoSoulsUtil.HostCheck)
      {
        int num3 = WorldSavingSystem.MasochistModeReal ? 12 : 18;
        if ((double) this.attackCount++ > (double) num3 * 1.25)
        {
          this.attackCount = 0;
          num3 /= 4;
        }
        this.attackHistory.Enqueue(this.NPC.ai[2]);
        while (this.attackHistory.Count > num3)
        {
          double num4 = (double) this.attackHistory.Dequeue();
        }
      }
      this.endTimeVariance = WorldSavingSystem.MasochistModeReal ? Utils.NextFloat(Main.rand, -0.5f, 1f) : 0.0f;
    }

    private void P1NextAttackOrMasoOptions(float sourceAI)
    {
      if (WorldSavingSystem.MasochistModeReal && Utils.NextBool(Main.rand, 3))
      {
        this.AttackChoice = (float) Utils.Next<int>(Main.rand, new int[7]
        {
          0,
          1,
          2,
          4,
          7,
          9,
          9
        });
        if ((double) this.AttackChoice == (double) sourceAI)
          this.AttackChoice = (double) sourceAI == 9.0 ? 0.0f : 9f;
        bool flag = false;
        if ((double) this.AttackChoice == 9.0 && ((double) sourceAI == 1.0 || (double) sourceAI == 2.0 || (double) sourceAI == 7.0))
          flag = true;
        if (((double) this.AttackChoice == 0.0 || (double) this.AttackChoice == 7.0) && (double) sourceAI == 2.0)
          flag = true;
        if (flag)
          this.AttackChoice = 4f;
        else if ((double) this.AttackChoice == 9.0 && Utils.NextBool(Main.rand))
          this.NPC.localAI[2] = 1f;
        else
          this.NPC.localAI[2] = 0.0f;
      }
      else if ((double) this.AttackChoice == 9.0 && (double) this.NPC.localAI[2] == 0.0)
      {
        this.NPC.localAI[2] = 1f;
      }
      else
      {
        ++this.AttackChoice;
        this.NPC.localAI[2] = 0.0f;
      }
      if ((double) this.AttackChoice >= 10.0)
        this.AttackChoice = 0.0f;
      this.EdgyBossText(this.RandomObnoxiousQuote());
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      this.NPC.localAI[0] = 0.0f;
      this.NPC.localAI[1] = 0.0f;
      this.NPC.netUpdate = true;
    }

    private void SpawnSphereRing(
      int max,
      float speed,
      int damage,
      float rotationModifier,
      float offset = 0.0f)
    {
      if (Main.netMode == 1)
        return;
      float num1 = 6.28318548f / (float) max;
      int num2 = ModContent.ProjectileType<MutantSphereRing>();
      for (int index = 0; index < max; ++index)
      {
        Vector2 vector2 = Vector2.op_Multiply(speed, Utils.RotatedBy(Vector2.UnitY, (double) num1 * (double) index + (double) offset, new Vector2()));
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, num2, damage, 0.0f, Main.myPlayer, rotationModifier * (float) this.NPC.spriteDirection, speed, 0.0f);
      }
      SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
    }

    private bool AliveCheck(Player p, bool forceDespawn = false)
    {
      if (WorldSavingSystem.SwarmActive | forceDespawn || (!((Entity) p).active || p.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) p).Center) > 3000.0) && (double) this.NPC.localAI[3] > 0.0)
      {
        this.NPC.TargetClosest(true);
        p = Main.player[this.NPC.target];
        if (WorldSavingSystem.SwarmActive | forceDespawn || !((Entity) p).active || p.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) p).Center) > 3000.0)
        {
          if (this.NPC.timeLeft > 30)
            this.NPC.timeLeft = 30;
          --((Entity) this.NPC).velocity.Y;
          if (this.NPC.timeLeft == 1)
          {
            this.EdgyBossText(this.GFBQuote(36));
            if ((double) ((Entity) this.NPC).position.Y < 0.0)
              ((Entity) this.NPC).position.Y = 0.0f;
            ModNPC modNpc;
            if (FargoSoulsUtil.HostCheck && ModContent.TryFind<ModNPC>("Fargowiltas", "Mutant", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
            {
              FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
              int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, modNpc.Type, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
              if (index != Main.maxNPCs)
              {
                Main.npc[index].homeless = true;
                if (this.TownNPCName != null)
                  Main.npc[index].GivenName = this.TownNPCName;
                if (Main.netMode == 2)
                  NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
          }
          return false;
        }
      }
      if (this.NPC.timeLeft < 3600)
        this.NPC.timeLeft = 3600;
      if ((double) ((Entity) this.player).Center.Y / 16.0 <= Main.worldSurface)
        return true;
      ((Entity) this.NPC).velocity.X *= 0.95f;
      --((Entity) this.NPC).velocity.Y;
      if ((double) ((Entity) this.NPC).velocity.Y < -32.0)
        ((Entity) this.NPC).velocity.Y = -32f;
      return false;
    }

    private bool Phase2Check()
    {
      if (!Main.expertMode || this.NPC.life >= this.NPC.lifeMax / 2)
        return false;
      if (FargoSoulsUtil.HostCheck)
      {
        this.AttackChoice = 10f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.netUpdate = true;
        FargoSoulsUtil.ClearHostileProjectiles(1, ((Entity) this.NPC).whoAmI);
        this.EdgyBossText(this.GFBQuote(3));
      }
      return true;
    }

    private void Movement(Vector2 target, float speed, bool fastX = true, bool obeySpeedCap = true)
    {
      float num1 = 1f;
      float num2 = 24f;
      if (WorldSavingSystem.MasochistModeReal)
      {
        speed *= 2f;
        num1 *= 2f;
        num2 *= 1.5f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).Center.X - target.X) > 10.0)
      {
        if ((double) ((Entity) this.NPC).Center.X < (double) target.X)
        {
          ((Entity) this.NPC).velocity.X += speed;
          if ((double) ((Entity) this.NPC).velocity.X < 0.0)
            ((Entity) this.NPC).velocity.X += speed * (fastX ? 2f : 1f) * num1;
        }
        else
        {
          ((Entity) this.NPC).velocity.X -= speed;
          if ((double) ((Entity) this.NPC).velocity.X > 0.0)
            ((Entity) this.NPC).velocity.X -= speed * (fastX ? 2f : 1f) * num1;
        }
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) target.Y)
      {
        ((Entity) this.NPC).velocity.Y += speed;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speed * 2f * num1;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speed;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speed * 2f * num1;
      }
      if (!obeySpeedCap)
        return;
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) num2)
        ((Entity) this.NPC).velocity.X = num2 * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) num2)
        return;
      ((Entity) this.NPC).velocity.Y = num2 * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    private void DramaticTransition(bool fightIsOver, bool normalAnimation = true)
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if (fightIsOver)
      {
        Main.player[this.NPC.target].ClearBuff(ModContent.BuffType<MutantFangBuff>());
        Main.player[this.NPC.target].ClearBuff(ModContent.BuffType<AbomRebirthBuff>());
      }
      SoundStyle soundStyle = SoundID.Item27;
      ((SoundStyle) ref soundStyle).Volume = 1.5f;
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if (normalAnimation && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantBomb>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      float num1 = fightIsOver ? (float) Main.player[this.NPC.target].statLifeMax2 / 4f : (float) (this.NPC.lifeMax - this.NPC.life) + (float) this.NPC.lifeMax * 0.1f;
      for (int index = 0; index < 40; ++index)
      {
        int num2 = (int) ((double) Utils.NextFloat(Main.rand, 0.9f, 1.1f) * (double) num1 / 40.0);
        Vector2 vector2 = normalAnimation ? Vector2.op_Multiply(Utils.NextFloat(Main.rand, 2f, 18f), Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 6.2831854820251465))) : Vector2.op_Multiply(0.1f, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 0.15707963705062866 * (double) index, new Vector2())));
        float num3 = fightIsOver ? (float) (-((Entity) Main.player[this.NPC.target]).whoAmI - 1) : (float) ((Entity) this.NPC).whoAmI;
        float num4 = ((Vector2) ref vector2).Length() / (float) Main.rand.Next(fightIsOver ? 90 : 150, 180);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantHeal>(), num2, 0.0f, Main.myPlayer, num3, num4, 0.0f);
      }
    }

    private void EModeSpecialEffects()
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      GameModeData gameModeInfo = Main.GameModeInfo;
      if (((GameModeData) ref gameModeInfo).IsJourneyMode && ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).Enabled)
        ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).SetPowerInfo(false);
      if (!((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:MutantBoss"].IsActive())
        ((EffectManager<CustomSky>) SkyManager.Instance).Activate("FargowiltasSouls:MutantBoss", new Vector2(), Array.Empty<object>());
      Mod mod;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
        return;
      if (WorldSavingSystem.MasochistModeReal && mod.Version >= Version.Parse("0.1.1"))
        this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Storia");
      else
        this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/rePrologue");
    }

    private void TryMasoP3Theme()
    {
      Mod mod;
      if (!WorldSavingSystem.MasochistModeReal || !Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) || !(mod.Version >= Version.Parse("0.1.1.3")))
        return;
      this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/StoriaShort");
    }

    private void FancyFireballs(int repeats)
    {
      float num1 = 0.0f;
      for (int index = 0; index < repeats; ++index)
        num1 = MathHelper.Lerp(num1, 1f, 0.08f);
      float num2 = (float) (1600.0 * (1.0 - (double) num1));
      float num3 = 6.28318548f * num1;
      for (int index1 = 0; index1 < 6; ++index1)
      {
        int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(num2, Utils.RotatedBy(Vector2.UnitX, (double) num3 + 1.0471975803375244 * (double) index1, new Vector2()))), 0, 0, FargoSoulsUtil.AprilFools ? 259 : 229, ((Entity) this.NPC).velocity.X * 0.3f, ((Entity) this.NPC).velocity.Y * 0.3f, 0, Color.White, 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].scale = (float) (6.0 - 4.0 * (double) num1);
      }
    }

    private void EdgyBossText(string text)
    {
      if (!Main.zenithWorld)
        return;
      Color cyan = Color.Cyan;
      FargoSoulsUtil.PrintText(text, cyan);
      CombatText.NewText(((Entity) this.NPC).Hitbox, cyan, text, true, false);
    }

    private string RandomObnoxiousQuote()
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(6, 2);
      interpolatedStringHandler.AppendFormatted("Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.");
      interpolatedStringHandler.AppendLiteral("Random");
      interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(71));
      return Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
    }

    private string GFBQuote(int num)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 2);
      interpolatedStringHandler.AppendFormatted("Mods.FargowiltasSouls.NPCs.MutantBoss.GFBText.");
      interpolatedStringHandler.AppendLiteral("Quote");
      interpolatedStringHandler.AppendFormatted<int>(num);
      return Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
    }

    private void SpearTossDirectP1AndChecks()
    {
      if (!this.AliveCheck(this.player) || this.Phase2Check())
        return;
      this.NPC.localAI[2] = 0.0f;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
        this.Movement(center, (double) this.NPC.localAI[3] > 0.0 ? 0.5f : 2f, obeySpeedCap: (double) this.NPC.localAI[3] > 0.0);
      if ((double) this.NPC.ai[3] == 0.0)
      {
        this.NPC.ai[3] = WorldSavingSystem.MasochistModeReal ? (float) Main.rand.Next(2, 8) : 5f;
        this.NPC.netUpdate = true;
      }
      if ((double) this.NPC.localAI[3] > 0.0)
        ++this.NPC.ai[1];
      if ((double) this.NPC.ai[1] < 145.0)
        this.NPC.localAI[0] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))));
      if ((double) this.NPC.ai[1] > 150.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[1] = 60f;
        if ((double) ++this.NPC.ai[2] > (double) this.NPC.ai[3])
        {
          this.P1NextAttackOrMasoOptions(this.AttackChoice);
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center), 2f);
        }
        else if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.localAI[0]), 25f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, 0.0f, 0.0f);
          if (WorldSavingSystem.MasochistModeReal)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(vector2), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(vector2)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        this.NPC.localAI[0] = 0.0f;
      }
      else
      {
        if ((double) this.NPC.ai[1] != 61.0 || (double) this.NPC.ai[2] >= (double) this.NPC.ai[3] || !FargoSoulsUtil.HostCheck)
          return;
        if (WorldSavingSystem.EternityMode && WorldSavingSystem.SkipMutantP1 >= 10 && !WorldSavingSystem.MasochistModeReal)
        {
          this.AttackChoice = 10f;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.localAI[0] = 0.0f;
          this.NPC.netUpdate = true;
          if (WorldSavingSystem.SkipMutantP1 == 10)
            FargoSoulsUtil.PrintLocalization("Mods." + ((ModType) this).Mod.Name + ".NPCs.MutantBoss.SkipP1", Color.LimeGreen);
          if (WorldSavingSystem.SkipMutantP1 < 10)
            return;
          this.NPC.ai[2] = 1f;
        }
        else
        {
          if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[2] == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              int num1 = Main.rand.Next(2);
              if (FargoSoulsUtil.AprilFools)
                num1 = 0;
              for (int index1 = 0; index1 < 8; ++index1)
              {
                Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), (double) MathHelper.ToRadians(120f)), 10f);
                float num2 = (float) (0.800000011920929 + 0.40000000596046448 * (double) index1 / 5.0);
                int index2 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerHead>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, num2, (float) num1);
                Main.projectile[index2].timeLeft = 90 * ((int) this.NPC.ai[3] + 1) + 30 + index1 * 6;
                int num3 = Main.rand.Next(8, 19);
                for (int index3 = 0; index3 < num3; ++index3)
                  index2 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerBody>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) Main.projectile[index2].identity, 0.0f, (float) num1);
                int index4 = index2;
                int index5 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerTail>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) Main.projectile[index2].identity, 0.0f, (float) num1);
                Main.projectile[index4].localAI[1] = (float) Main.projectile[index5].identity;
                Main.projectile[index4].netUpdate = true;
              }
            }
          }
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0.0f, Main.myPlayer, 85f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 3f, 0.0f);
        }
      }
    }

    private void OkuuSpheresP1()
    {
      if (this.Phase2Check())
        return;
      if (WorldSavingSystem.MasochistModeReal)
        ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) --this.NPC.ai[1] >= 0.0)
        return;
      this.NPC.netUpdate = true;
      float num1 = WorldSavingSystem.MasochistModeReal ? 3f : 1f;
      this.NPC.ai[1] = 90f / num1;
      if ((double) ++this.NPC.ai[2] > 4.0 * (double) num1)
      {
        if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[2] <= 6.0 * (double) num1)
          return;
        this.P1NextAttackOrMasoOptions(this.AttackChoice);
      }
      else
      {
        this.EdgyBossText(this.RandomObnoxiousQuote());
        int max = WorldSavingSystem.MasochistModeReal ? 9 : 6;
        float speed = WorldSavingSystem.MasochistModeReal ? 10f : 9f;
        int num2 = WorldSavingSystem.MasochistModeReal ? ((double) this.NPC.ai[2] % 2.0 == 0.0 ? 1 : -1) : 1;
        this.SpawnSphereRing(max, speed, (int) (0.8 * (double) FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage)), 1f * (float) num2);
        this.SpawnSphereRing(max, speed, (int) (0.8 * (double) FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage)), -0.5f * (float) num2);
      }
    }

    private void PrepareTrueEyeDiveP1()
    {
      if (!this.AliveCheck(this.player) || this.Phase2Check())
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (700 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      center.Y -= 400f;
      this.Movement(center, 0.6f);
      if ((double) ((Entity) this.NPC).Distance(center) >= 50.0 && (double) ++this.NPC.ai[1] <= 180.0)
        return;
      ((Entity) this.NPC).velocity.X = (float) (35.0 * ((double) ((Entity) this.NPC).position.X < (double) ((Entity) this.player).position.X ? 1.0 : -1.0));
      if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
        ((Entity) this.NPC).velocity.Y *= -1f;
      ((Entity) this.NPC).velocity.Y *= 0.3f;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.netUpdate = true;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      this.EdgyBossText(this.RandomObnoxiousQuote());
    }

    private void TrueEyeDive()
    {
      if ((double) this.NPC.ai[3] == 0.0)
        this.NPC.ai[3] = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      if ((double) this.NPC.ai[2] > 3.0)
      {
        Vector2 center = ((Entity) this.player).Center;
        center.X += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) this.player).Center.X ? -500f : 500f;
        if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
          this.Movement(center, 0.3f);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.99f);
      }
      if ((double) --this.NPC.ai[1] >= 0.0)
        return;
      this.NPC.ai[1] = 15f;
      int num1 = WorldSavingSystem.MasochistModeReal ? 6 : 3;
      int num2 = WorldSavingSystem.MasochistModeReal ? 3 : 5;
      if ((double) ++this.NPC.ai[2] > (double) (num1 + num2))
      {
        if ((double) this.AttackChoice == 3.0)
          this.P1NextAttackOrMasoOptions(2f);
        else
          this.ChooseNextAttack(13, 19, 21, 24, 33, 33, 33, 39, 41, 44);
      }
      else
      {
        if ((double) this.NPC.ai[2] > (double) num1)
          return;
        if (FargoSoulsUtil.HostCheck)
        {
          float num3 = (float) ((double) this.NPC.ai[2] / (double) num1 * 3.0);
          int num4 = (double) num3 > 1.0 ? ((double) num3 > 2.0 ? ModContent.ProjectileType<MutantTrueEyeR>() : ModContent.ProjectileType<MutantTrueEyeS>()) : ModContent.ProjectileType<MutantTrueEyeL>();
          int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, num4, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, (float) this.NPC.target, 0.0f, 0.0f);
          if (index != Main.maxProjectiles)
          {
            Main.projectile[index].localAI[1] = this.NPC.ai[3];
            Main.projectile[index].netUpdate = true;
          }
        }
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 135, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
        }
      }
    }

    private void PrepareSpearDashDirectP1()
    {
      if (this.Phase2Check())
        return;
      if ((double) this.NPC.ai[3] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.ai[3] = 1f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 240f, 0.0f);
        this.EdgyBossText(this.GFBQuote(4));
      }
      if ((double) ++this.NPC.ai[1] > 240.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        ++this.AttackChoice;
        this.NPC.ai[3] = 0.0f;
        this.NPC.netUpdate = true;
      }
      Vector2 center = ((Entity) this.player).Center;
      if ((double) ((Entity) this.NPC).Top.Y < (double) ((Entity) this.player).Bottom.Y)
        center.X += 600f * (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      center.Y += 400f;
      this.Movement(center, 0.7f, false);
    }

    private void SpearDashDirectP1()
    {
      if (this.Phase2Check())
        return;
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
      if ((double) this.NPC.ai[3] == 0.0)
        this.NPC.ai[3] = WorldSavingSystem.MasochistModeReal ? (float) Main.rand.Next(3, 15) : 10f;
      if ((double) ++this.NPC.ai[1] <= (double) this.NPC.ai[3])
        return;
      this.NPC.netUpdate = true;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      if ((double) ++this.NPC.ai[2] > 5.0)
      {
        this.P1NextAttackOrMasoOptions(4f);
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(WorldSavingSystem.MasochistModeReal ? 45f : 30f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, ((Entity) this.player).velocity)));
        if (FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
          if (WorldSavingSystem.MasochistModeReal)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(((Entity) this.NPC).velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(((Entity) this.NPC).velocity)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        this.EdgyBossText(this.GFBQuote(5));
      }
    }

    private void WhileDashingP1()
    {
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) ++this.NPC.ai[1] <= 30.0 || !this.AliveCheck(this.player))
        return;
      this.NPC.netUpdate = true;
      --this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
    }

    private void ApproachForNextAttackP1()
    {
      if (!this.AliveCheck(this.player) || this.Phase2Check())
        return;
      Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center), 250f));
      if ((double) ((Entity) this.NPC).Distance(target) > 50.0 && (double) ++this.NPC.ai[2] < 180.0)
      {
        this.Movement(target, 0.5f);
      }
      else
      {
        this.NPC.netUpdate = true;
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center));
        this.NPC.ai[3] = 0.314159274f;
        if ((double) ((Entity) this.player).Center.X >= (double) ((Entity) this.NPC).Center.X)
          return;
        this.NPC.ai[3] *= -1f;
      }
    }

    private void VoidRaysP1()
    {
      if (this.Phase2Check())
        return;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) --this.NPC.ai[1] >= 0.0)
        return;
      if (FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(2f, 0.0f), (double) this.NPC.ai[2], new Vector2()), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      this.NPC.ai[1] = WorldSavingSystem.MasochistModeReal ? 3f : 5f;
      this.NPC.ai[2] += this.NPC.ai[3];
      if ((double) this.NPC.localAI[0]++ == 20.0 || (double) this.NPC.localAI[0] == 40.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[2] -= this.NPC.ai[3] / (WorldSavingSystem.MasochistModeReal ? 3f : 2f);
        this.EdgyBossText(this.GFBQuote(6));
      }
      else
      {
        if ((double) this.NPC.localAI[0] < (WorldSavingSystem.MasochistModeReal ? 60.0 : 40.0))
          return;
        this.P1NextAttackOrMasoOptions(7f);
      }
    }

    private void BoundaryBulletHellAndSwordP1()
    {
      switch ((int) this.NPC.localAI[2])
      {
        case 0:
          if ((double) this.NPC.ai[3] == 0.0)
          {
            if (!this.AliveCheck(this.player))
              break;
            this.NPC.ai[3] = 1f;
            this.NPC.localAI[0] = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
            this.EdgyBossText(this.GFBQuote(7));
          }
          if (this.Phase2Check())
            break;
          ((Entity) this.NPC).velocity = Vector2.Zero;
          if ((double) ++this.NPC.ai[1] > 2.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] += WorldSavingSystem.MasochistModeReal ? (float) (0.000818123109638691 * ((double) this.NPC.ai[3] - 300.0)) * this.NPC.localAI[0] : (float) Math.PI / 77f;
            if (FargoSoulsUtil.HostCheck)
            {
              int num = WorldSavingSystem.MasochistModeReal ? 5 : 4;
              for (int index = 0; index < num; ++index)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(0.0f, -7f), (double) this.NPC.ai[2] + 6.2831854820251465 / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[3] <= (WorldSavingSystem.MasochistModeReal ? 360.0 : 240.0))
            break;
          this.P1NextAttackOrMasoOptions(this.AttackChoice);
          break;
        case 1:
          this.PrepareMutantSword();
          break;
        case 2:
          this.MutantSword();
          break;
      }
    }

    private void PrepareMutantSword()
    {
      if ((double) this.AttackChoice == 9.0 && ((Entity) Main.LocalPlayer).active && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && Main.expertMode)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<PurgedBuff>(), 2, true, false);
      int num1 = (double) this.AttackChoice == 9.0 || (double) this.NPC.localAI[2] % 2.0 != 1.0 ? 1 : -1;
      if ((double) this.NPC.ai[2] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        Vector2 center = ((Entity) this.player).Center;
        center.X += (float) (420 * Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X));
        center.Y -= (float) (210 * num1);
        this.Movement(center, 1.2f);
        if ((double) ++this.NPC.localAI[0] <= 30.0 && !WorldSavingSystem.MasochistModeReal || (double) ((Entity) this.NPC).Distance(center) >= 64.0)
          return;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.localAI[1] = (float) Math.Sign(((Entity) this.player).Center.X - ((Entity) this.NPC).Center.X);
        float num2 = (float) (0.78539818525314331 * -(double) this.NPC.localAI[1]);
        this.NPC.ai[2] = (float) ((double) num2 * -4.0 / 20.0) * (float) num1;
        if (num1 < 0)
          num2 += (float) (1.5707963705062866 * -(double) this.NPC.localAI[1]);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) num2, new Vector2()), -80f);
          for (int index = 0; index < 12; ++index)
            MakeSword(Vector2.op_Multiply(vector2, (float) index), (float) (80 * index));
          for (int index = -1; index <= 1; index += 2)
          {
            MakeSword(Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(26.5f * (float) index), new Vector2()), 180f);
            MakeSword(Utils.RotatedBy(vector2, (double) MathHelper.ToRadians((float) (40 * index)), new Vector2()), 240f);
          }
        }
        this.EdgyBossText(this.GFBQuote(8));
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        int num3 = 90;
        this.FancyFireballs((int) ((double) this.NPC.ai[1] / (double) num3 * 60.0));
        if ((double) ++this.NPC.ai[1] > (double) num3)
        {
          if ((double) this.AttackChoice != 9.0)
            ++this.AttackChoice;
          ++this.NPC.localAI[2];
          Vector2 center = ((Entity) this.player).Center;
          center.X -= 300f * this.NPC.ai[2];
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.NPC).Center), 20f);
          this.NPC.ai[1] = 0.0f;
          this.NPC.netUpdate = true;
        }
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.localAI[1]);
      }

      void MakeSword(Vector2 pos, float spacing, float rotation = 0.0f)
      {
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, pos), Vector2.Zero, ModContent.ProjectileType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, spacing, 0.0f);
      }
    }

    private void MutantSword()
    {
      if ((double) this.AttackChoice == 9.0 && ((Entity) Main.LocalPlayer).active && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && Main.expertMode)
        Main.LocalPlayer.AddBuff(ModContent.BuffType<PurgedBuff>(), 2, true, false);
      this.NPC.ai[3] += this.NPC.ai[2];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.localAI[1]);
      if ((double) this.NPC.ai[1] == 20.0)
      {
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        if (WorldSavingSystem.EternityMode && (double) this.AttackChoice != 9.0 || WorldSavingSystem.MasochistModeReal)
        {
          if (!Main.dedServ)
          {
            SoundStyle thunder = SoundID.Thunder;
            ((SoundStyle) ref thunder).Pitch = -0.5f;
            SoundEngine.PlaySound(ref thunder, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          }
          Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(480f, Vector2.op_Multiply((float) Math.Sign(this.NPC.localAI[1]), Utils.RotatedBy(Vector2.UnitX, 0.78539818525314331 * (double) Math.Sign(this.NPC.ai[2]), new Vector2()))));
          Vector2 vector2_2 = ((Entity) this.player).DirectionFrom(vector2_1);
          for (int index = 0; index < 8; ++index)
          {
            Vector2 vector2_3 = Utils.RotatedBy(vector2_2, 0.78539818525314331 * (double) index, new Vector2());
            float num = index <= 2 || index == 6 ? 48f : 24f;
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(vector2_1, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.NPC).width / 2), (float) (((Entity) this.NPC).height / 2))), Vector2.Zero, FargoSoulsUtil.AprilFools ? ModContent.ProjectileType<MoonLordSunBlast>() : ModContent.ProjectileType<MoonLordMoonBlast>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, MathHelper.WrapAngle(Utils.ToRotation(vector2_3)), num, 0.0f);
          }
        }
      }
      if ((double) ++this.NPC.ai[1] <= 25.0)
        return;
      if ((double) this.AttackChoice == 9.0)
        this.P1NextAttackOrMasoOptions(this.AttackChoice);
      else if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.localAI[2] < 3.0 * ((double) this.endTimeVariance + 0.5))
      {
        --this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
        this.NPC.netUpdate = true;
      }
      else
        this.ChooseNextAttack(13, 21, 24, 29, 31, 33, 37, 41, 42, 44);
    }

    private void Phase2Transition()
    {
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
      this.NPC.dontTakeDamage = true;
      if (this.NPC.buffType[0] != 0)
        this.NPC.DelBuff(0);
      this.EModeSpecialEffects();
      if ((double) this.NPC.ai[2] == 0.0)
      {
        if ((double) this.NPC.ai[1] < 60.0 && !Main.dedServ && ((Entity) Main.LocalPlayer).active)
          FargoSoulsUtil.ScreenshakeRumble(6f);
      }
      else
        ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) this.NPC.ai[1] < 240.0 && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0)
      {
        Main.LocalPlayer.controlUseItem = false;
        Main.LocalPlayer.controlUseTile = false;
        Main.LocalPlayer.FargoSouls().NoUsingItems = 2;
      }
      if ((double) this.NPC.ai[1] == 0.0)
      {
        FargoSoulsUtil.ClearAllProjectiles(2, ((Entity) this.NPC).whoAmI);
        if (WorldSavingSystem.EternityMode)
        {
          this.DramaticTransition(false, (double) this.NPC.ai[2] == 0.0);
          if (FargoSoulsUtil.HostCheck)
          {
            this.ritualProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            if (WorldSavingSystem.MasochistModeReal)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual2>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual3>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantRitual4>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
          }
        }
      }
      else if ((double) this.NPC.ai[1] == 150.0)
      {
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 5f, 0.0f, 0.0f);
        if (WorldSavingSystem.EternityMode && WorldSavingSystem.SkipMutantP1 <= 10)
        {
          ++WorldSavingSystem.SkipMutantP1;
          if (Main.netMode == 2)
            NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
        for (int index1 = 0; index1 < 50; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) Main.LocalPlayer).position, ((Entity) Main.LocalPlayer).width, ((Entity) Main.LocalPlayer).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
        }
        if (this.player.FargoSouls().TerrariaSoul)
          this.EdgyBossText(this.GFBQuote(1));
      }
      else if ((double) this.NPC.ai[1] > 150.0)
        this.NPC.localAI[3] = 3f;
      if ((double) ++this.NPC.ai[1] <= 270.0)
        return;
      if (WorldSavingSystem.EternityMode)
      {
        this.NPC.life = this.NPC.lifeMax;
        this.AttackChoice = (float) Utils.Next<int>(Main.rand, new int[13]
        {
          11,
          13,
          16,
          19,
          20,
          21,
          24,
          26,
          29,
          35,
          37,
          39,
          42
        });
      }
      else
        ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.netUpdate = true;
      this.attackHistory.Enqueue(this.AttackChoice);
    }

    private void ApproachForNextAttackP2()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center), 300f));
      if ((double) ((Entity) this.NPC).Distance(target) > 50.0 && (double) ++this.NPC.ai[2] < 180.0)
      {
        this.Movement(target, 0.8f);
      }
      else
      {
        this.NPC.netUpdate = true;
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center));
        this.NPC.ai[3] = 0.314159274f;
        this.NPC.localAI[0] = 0.0f;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if ((double) ((Entity) this.player).Center.X >= (double) ((Entity) this.NPC).Center.X)
          return;
        this.NPC.ai[3] *= -1f;
      }
    }

    private void VoidRaysP2()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) --this.NPC.ai[1] >= 0.0)
        return;
      if (FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(2f, 0.0f), (double) this.NPC.ai[2], new Vector2()), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      this.NPC.ai[1] = 3f;
      this.NPC.ai[2] += this.NPC.ai[3];
      if ((double) this.NPC.localAI[0]++ == 20.0 || (double) this.NPC.localAI[0] == 40.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[2] -= this.NPC.ai[3] / (WorldSavingSystem.MasochistModeReal ? 3f : 2f);
        if ((double) this.NPC.localAI[0] == 21.0 && (double) this.endTimeVariance > 0.33000001311302185 || (double) this.NPC.localAI[0] == 41.0 && (double) this.endTimeVariance < -0.33000001311302185)
          this.NPC.localAI[0] = 60f;
        this.EdgyBossText(this.GFBQuote(6));
      }
      else
      {
        if ((double) this.NPC.localAI[0] < 60.0)
          return;
        this.ChooseNextAttack(13, 19, 21, 24, 31, 39, 41, 42);
      }
    }

    private void PrepareSpearDashPredictiveP2()
    {
      if ((double) this.NPC.ai[3] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.ai[3] = 1f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 180f, 0.0f);
        this.EdgyBossText(this.GFBQuote(9));
      }
      if ((double) ++this.NPC.ai[1] > 180.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.netUpdate = true;
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[3] = 0.0f;
      }
      Vector2 center = ((Entity) this.player).Center;
      center.Y += 400f * (float) Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) this.player).Center.Y);
      this.Movement(center, 0.7f, false);
      if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) >= 200.0)
        return;
      this.Movement(Vector2.op_Addition(((Entity) this.NPC).Center, ((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center)), 1.4f);
    }

    private void SpearDashPredictiveP2()
    {
      if ((double) this.NPC.localAI[1] == 0.0)
      {
        if (WorldSavingSystem.EternityMode)
          this.NPC.localAI[1] = (float) Main.rand.Next(WorldSavingSystem.MasochistModeReal ? 3 : 5, 9);
        else
          this.NPC.localAI[1] = 5f;
      }
      if ((double) this.NPC.ai[1] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        if ((double) this.NPC.ai[2] == (double) this.NPC.localAI[1] - 1.0)
        {
          if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) > 450.0)
          {
            this.Movement(((Entity) this.player).Center, 0.6f);
            return;
          }
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.75f);
        }
        if ((double) this.NPC.ai[2] < (double) this.NPC.localAI[1])
        {
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0.0f, Main.myPlayer, 55f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          if ((double) this.NPC.ai[2] == (double) this.NPC.localAI[1] - 1.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 4f, 0.0f);
          }
        }
      }
      NPC npc1 = this.NPC;
      ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
      if ((double) this.NPC.ai[1] < 55.0)
        this.NPC.localAI[0] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))));
      int num1 = 60;
      if ((double) this.NPC.ai[2] == (double) this.NPC.localAI[1] - 1.0)
        num1 = 80;
      if (WorldSavingSystem.MasochistModeReal && ((double) this.NPC.ai[2] == 0.0 || (double) this.NPC.ai[2] >= (double) this.NPC.localAI[1]))
        num1 = 0;
      if ((double) ++this.NPC.ai[1] <= (double) num1)
        return;
      this.NPC.netUpdate = true;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      if ((double) ++this.NPC.ai[2] > (double) this.NPC.localAI[1])
      {
        this.ChooseNextAttack(16, 19, 20, 26, 29, 31, 33, 39, 42, 44, 45);
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.localAI[0]), 45f);
        float num2 = 0.0f;
        if ((double) this.NPC.ai[2] == (double) this.NPC.localAI[1])
          num2 = -2f;
        if (FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(((Entity) this.NPC).velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(((Entity) this.NPC).velocity)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num2, 0.0f);
        }
        this.EdgyBossText(this.GFBQuote(10));
      }
      this.NPC.localAI[0] = 0.0f;
    }

    private void WhileDashingP2()
    {
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) ++this.NPC.ai[1] <= 30.0 || !this.AliveCheck(this.player))
        return;
      this.NPC.netUpdate = true;
      --this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      if ((double) this.AttackChoice != 14.0 || (double) this.NPC.ai[2] != (double) this.NPC.localAI[1] - 1.0 || (double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) <= 450.0)
        return;
      ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center), 16f);
    }

    private void BoundaryBulletHellP2()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) this.NPC.localAI[0] == 0.0)
      {
        this.NPC.localAI[0] = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -2f, 0.0f);
        this.EdgyBossText(this.GFBQuote(11));
        if (WorldSavingSystem.MasochistModeReal)
          this.NPC.ai[2] = Utils.NextFloat(Main.rand, 3.14159274f);
      }
      if ((double) this.NPC.ai[3] > 60.0 && (double) ++this.NPC.ai[1] > 2.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] += 0.0008181231f * this.NPC.ai[3] * this.NPC.localAI[0];
        if ((double) this.NPC.ai[2] > 3.1415927410125732)
          this.NPC.ai[2] -= 6.28318548f;
        if (FargoSoulsUtil.HostCheck)
        {
          int num = 4;
          if (WorldSavingSystem.EternityMode)
            ++num;
          if (WorldSavingSystem.MasochistModeReal)
            ++num;
          for (int index = 0; index < num; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(0.0f, -6f), (double) this.NPC.ai[2] + 2.0 * Math.PI / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) ++this.NPC.ai[3] <= (double) (420 + (int) (300.0 * (double) this.endTimeVariance)))
        return;
      this.ChooseNextAttack(11, 13, 19, 20, 21, 24, WorldSavingSystem.MasochistModeReal ? 31 : 26, 33, 41, 44);
    }

    private void PillarDunk()
    {
      if (!this.AliveCheck(this.player))
        return;
      int num1 = 60;
      if (Main.zenithWorld && (double) this.NPC.ai[1] > 180.0)
        this.player.confused = true;
      if ((double) this.NPC.ai[2] == 0.0 && (double) this.NPC.ai[3] == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Clone(-1f, 1f, (float) (num1 * 4));
          Clone(1f, -1f, (float) (num1 * 2));
          Clone(1f, 1f, (float) (num1 * 3));
          if (WorldSavingSystem.MasochistModeReal)
          {
            Clone(1f, 1f, (float) (num1 * 6));
            if (Main.getGoodWorld)
            {
              Clone(-1f, 1f, (float) (num1 * 7));
              Clone(1f, -1f, (float) (num1 * 8));
            }
          }
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.player).Center, new Vector2(0.0f, -4f), ModContent.ProjectileType<BrainofConfusion>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        this.EdgyBossText(this.GFBQuote(12));
        this.NPC.netUpdate = true;
        this.NPC.ai[2] = ((Entity) this.NPC).Center.X;
        this.NPC.ai[3] = ((Entity) this.NPC).Center.Y;
        for (int index = 0; index < Main.maxProjectiles; ++index)
        {
          if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<MutantRitual>() && (double) Main.projectile[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
          {
            this.NPC.ai[2] = ((Entity) Main.projectile[index]).Center.X;
            this.NPC.ai[3] = ((Entity) Main.projectile[index]).Center.Y;
            break;
          }
        }
        Vector2 vector2 = Vector2.op_Multiply(1000f, Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians(45f), new Vector2()));
        if (Utils.NextBool(Main.rand))
        {
          if ((double) ((Entity) this.player).Center.X > (double) this.NPC.ai[2])
            vector2.X *= -1f;
          if (Utils.NextBool(Main.rand))
            vector2.Y *= -1f;
        }
        else
        {
          if (Utils.NextBool(Main.rand))
            vector2.X *= -1f;
          if ((double) ((Entity) this.player).Center.Y > (double) this.NPC.ai[3])
            vector2.Y *= -1f;
        }
        this.NPC.localAI[1] = this.NPC.ai[2];
        this.NPC.localAI[2] = this.NPC.ai[3];
        this.NPC.ai[2] = ((Vector2) ref vector2).Length();
        this.NPC.ai[3] = Utils.ToRotation(vector2);
      }
      Vector2 center = ((Entity) this.player).Center;
      center.X += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) this.player).Center.X ? -700f : 700f;
      center.Y += (double) this.NPC.ai[1] < 240.0 ? 400f : 150f;
      if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
        this.Movement(center, 1f);
      int num2 = 240 + num1 * 4 + 60;
      if (WorldSavingSystem.MasochistModeReal)
        num2 += num1 * 2;
      this.NPC.localAI[0] = (float) num2 - this.NPC.ai[1];
      this.NPC.localAI[0] += (float) (60.0 + 60.0 * (1.0 - (double) this.NPC.ai[1] / (double) num2));
      if ((double) ++this.NPC.ai[1] > (double) num2)
        this.ChooseNextAttack(11, 13, 20, 21, 26, 33, 41, 44);
      else if ((double) this.NPC.ai[1] == (double) num1)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, -5f), ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 3f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }
      else
      {
        if (!WorldSavingSystem.MasochistModeReal || (double) this.NPC.ai[1] != (double) (num1 * 5) || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, -5f), ModContent.ProjectileType<MutantPillar>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 1f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }

      void Clone(float ai1, float ai2, float ai3)
      {
        FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<MutantIllusion>(), ((Entity) this.NPC).whoAmI, (float) ((Entity) this.NPC).whoAmI, ai1, ai2, ai3, velocity: new Vector2());
      }
    }

    private void EOCStarSickles()
    {
      if (!this.AliveCheck(this.player))
        return;
      if ((double) this.NPC.ai[1] == 0.0)
      {
        float num = 0.0f;
        if (WorldSavingSystem.MasochistModeReal)
        {
          num = 30f;
          this.NPC.ai[1] = 30f;
        }
        if (FargoSoulsUtil.HostCheck)
        {
          int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<MutantEyeOfCthulhu>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, num, 0.0f);
          if (WorldSavingSystem.MasochistModeReal && index != Main.maxProjectiles)
            Main.projectile[index].timeLeft -= 30;
        }
      }
      if ((double) this.NPC.ai[1] < 120.0)
      {
        this.NPC.ai[2] = ((Entity) this.player).Center.X;
        this.NPC.ai[3] = ((Entity) this.player).Center.Y;
      }
      if ((double) this.NPC.ai[1] == 120.0)
        this.EdgyBossText(this.GFBQuote(13));
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(this.NPC.ai[2], this.NPC.ai[3]);
      Vector2 target = Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.NPC).DirectionFrom(vector2), (double) MathHelper.ToRadians(-5f), new Vector2()), 450f));
      if ((double) ((Entity) this.NPC).Distance(target) > 50.0)
        this.Movement(target, 0.25f);
      if ((double) ++this.NPC.ai[1] <= 450.0)
        return;
      this.ChooseNextAttack(11, 13, 16, 21, 26, 29, 31, 33, 35, 37, 41, 44, 45);
    }

    private void PrepareSpearDashDirectP2()
    {
      if ((double) this.NPC.ai[3] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.ai[3] = 1f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearSpin>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 180f, 0.0f);
        this.EdgyBossText(this.GFBQuote(14));
      }
      if ((double) ++this.NPC.ai[1] > 180.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.netUpdate = true;
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[3] = 0.0f;
      }
      Vector2 center = ((Entity) this.player).Center;
      center.Y += 450f * (float) Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) this.player).Center.Y);
      this.Movement(center, 0.7f, false);
      if ((double) ((Entity) this.NPC).Distance(((Entity) this.player).Center) >= 200.0)
        return;
      this.Movement(Vector2.op_Addition(((Entity) this.NPC).Center, ((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center)), 1.4f);
    }

    private void SpearDashDirectP2()
    {
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
      if ((double) this.NPC.localAI[1] == 0.0)
      {
        if (WorldSavingSystem.EternityMode)
          this.NPC.localAI[1] = (float) Main.rand.Next(WorldSavingSystem.MasochistModeReal ? 3 : 5, 9);
        else
          this.NPC.localAI[1] = 5f;
      }
      if ((double) ++this.NPC.ai[1] <= (WorldSavingSystem.EternityMode ? 5.0 : 20.0))
        return;
      this.NPC.netUpdate = true;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      if ((double) ++this.NPC.ai[2] > (double) this.NPC.localAI[1])
      {
        if (WorldSavingSystem.MasochistModeReal)
          this.ChooseNextAttack(11, 13, 16, 19, 20, 31, 33, 35, 39, 42, 44);
        else
          this.ChooseNextAttack(11, 16, 26, 29, 31, 35, 37, 39, 42, 44);
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center), WorldSavingSystem.MasochistModeReal ? 60f : 45f);
        if (FargoSoulsUtil.HostCheck)
        {
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(((Entity) this.NPC).velocity), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(((Entity) this.NPC).velocity)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearDash>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
        }
      }
      this.EdgyBossText(this.GFBQuote(15));
    }

    private void SpawnDestroyersForPredictiveThrow()
    {
      if (!this.AliveCheck(this.player))
        return;
      if (WorldSavingSystem.EternityMode)
      {
        Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), 500f));
        if ((double) Math.Abs(target.X - ((Entity) this.player).Center.X) < 150.0)
        {
          target.X = ((Entity) this.player).Center.X + (float) (150 * Math.Sign(target.X - ((Entity) this.player).Center.X));
          this.Movement(target, 0.3f);
        }
        if ((double) ((Entity) this.NPC).Distance(target) > 50.0)
          this.Movement(target, 0.9f);
      }
      else
      {
        Vector2 center = ((Entity) this.player).Center;
        center.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
        if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
          this.Movement(center, 0.4f);
      }
      if ((double) this.NPC.localAI[1] == 0.0)
      {
        if (WorldSavingSystem.EternityMode)
          this.NPC.localAI[1] = (float) Main.rand.Next(WorldSavingSystem.MasochistModeReal ? 3 : 5, 9);
        else
          this.NPC.localAI[1] = 5f;
        this.NPC.localAI[2] = (float) Main.rand.Next(2);
        this.EdgyBossText(this.GFBQuote(16));
      }
      if ((double) ++this.NPC.ai[1] <= 60.0)
        return;
      this.NPC.netUpdate = true;
      this.NPC.ai[1] = 30f;
      int num1 = 3;
      if (WorldSavingSystem.EternityMode)
        num1 += 2;
      if (WorldSavingSystem.MasochistModeReal)
      {
        num1 += 2;
        this.NPC.ai[1] += 15f;
      }
      if ((double) ++this.NPC.ai[2] > (double) num1)
      {
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
      }
      else
      {
        SoundEngine.PlaySound(ref SoundID.NPCDeath13, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), (double) MathHelper.ToRadians(120f)), 10f);
        float num2 = (float) (0.800000011920929 + 0.40000000596046448 * (double) this.NPC.ai[2] / 5.0);
        if (WorldSavingSystem.MasochistModeReal)
          num2 += 0.4f;
        float num3 = this.NPC.localAI[2];
        if (FargoSoulsUtil.AprilFools)
          num3 = 0.0f;
        int index1 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerHead>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, num2, num3);
        Main.projectile[index1].timeLeft = 30 * (num1 - (int) this.NPC.ai[2]) + 60 * (int) this.NPC.localAI[1] + 30 + (int) this.NPC.ai[2] * 6;
        int num4 = Main.rand.Next(8, 19);
        for (int index2 = 0; index2 < num4; ++index2)
          index1 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerBody>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) Main.projectile[index1].identity, 0.0f, num3);
        int index3 = index1;
        int index4 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantDestroyerTail>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) Main.projectile[index1].identity, 0.0f, num3);
        Main.projectile[index3].localAI[1] = (float) Main.projectile[index4].identity;
        Main.projectile[index3].netUpdate = true;
      }
    }

    private void SpearTossPredictiveP2()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      if ((double) ((Entity) this.NPC).Distance(center) > 25.0)
        this.Movement(center, 0.8f);
      if ((double) ++this.NPC.ai[1] > 60.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[1] = 0.0f;
        bool flag = true;
        if ((double) ++this.NPC.ai[2] > (double) this.NPC.localAI[1])
        {
          flag = false;
          if (WorldSavingSystem.MasochistModeReal)
            this.ChooseNextAttack(11, 19, 20, 29, 31, 33, 35, 37, 39, 42, 44, 45);
          else
            this.ChooseNextAttack(11, 19, 20, 26, 26, 26, 29, 31, 33, 35, 37, 39, 42, 44);
        }
        if (!flag && !WorldSavingSystem.MasochistModeReal || !FargoSoulsUtil.HostCheck)
          return;
        Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))), 30f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(vector2), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(vector2)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, 1f, 0.0f);
      }
      else
      {
        if ((double) this.NPC.ai[1] != 1.0 || (double) this.NPC.ai[2] >= (double) this.NPC.localAI[1] && !WorldSavingSystem.MasochistModeReal || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.player).velocity, 30f))), ModContent.ProjectileType<MutantDeathrayAim>(), 0, 0.0f, Main.myPlayer, 60f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 2f, 0.0f);
      }
    }

    private void PrepareMechRayFan()
    {
      if ((double) this.NPC.ai[1] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        if (WorldSavingSystem.MasochistModeReal)
          this.NPC.ai[1] = 31f;
      }
      if ((double) this.NPC.ai[1] == 30.0)
      {
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 125f, 0.0f);
        this.EdgyBossText(this.GFBQuote(17));
      }
      Vector2 target;
      if ((double) this.NPC.ai[1] < 30.0)
      {
        target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), (double) MathHelper.ToRadians(15f), new Vector2()), 500f));
        if ((double) ((Entity) this.NPC).Distance(target) > 50.0)
          this.Movement(target, 0.3f);
      }
      else
      {
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 6, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].noLight = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
        }
        target = ((Entity) this.player).Center;
        target.X += (float) (600 * ((double) ((Entity) this.NPC).Center.X < (double) target.X ? -1 : 1));
        this.Movement(target, 1.2f, false);
      }
      if ((double) ++this.NPC.ai[1] <= 150.0 && (!WorldSavingSystem.MasochistModeReal || (double) ((Entity) this.NPC).Distance(target) >= 64.0))
        return;
      this.NPC.netUpdate = true;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
    }

    private void MechRayFan()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) this.NPC.ai[2] == 0.0)
        this.NPC.ai[2] = Utils.NextBool(Main.rand) ? -1f : 1f;
      if ((double) this.NPC.ai[3] == 0.0 && FargoSoulsUtil.HostCheck)
      {
        int num = 7;
        for (int index = 0; index <= num; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[2] * (double) index * 3.1415927410125732 / (double) num, new Vector2()), 6f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2), Vector2.Zero, ModContent.ProjectileType<MutantGlowything>(), 0, 0.0f, Main.myPlayer, Utils.ToRotation(vector2), (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
      }
      int num1 = 390;
      if ((double) this.NPC.ai[3] > (WorldSavingSystem.MasochistModeReal ? 45.0 : 60.0) && (double) this.NPC.ai[3] < 240.0 && (double) ++this.NPC.ai[1] > 10.0)
      {
        this.NPC.ai[1] = 0.0f;
        if (FargoSoulsUtil.HostCheck)
        {
          float turnRotation = (float) ((double) MathHelper.ToRadians(245f) * (double) this.NPC.ai[2] / 80.0);
          int timeBeforeAttackEnds = num1 - (int) this.NPC.ai[3];
          SpawnRay(((Entity) this.NPC).Center, 8f * this.NPC.ai[2], turnRotation);
          SpawnRay(((Entity) this.NPC).Center, (float) (-8.0 * (double) this.NPC.ai[2] + 180.0), -turnRotation);
          if (WorldSavingSystem.MasochistModeReal)
          {
            Vector2 pos = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(this.NPC.ai[2] * -1200f, Vector2.UnitY));
            SpawnRay(pos, (float) (8.0 * (double) this.NPC.ai[2] + 180.0), turnRotation);
            SpawnRay(pos, -8f * this.NPC.ai[2], -turnRotation);
          }

          void SpawnRay(Vector2 pos, float angleInDegrees, float turnRotation)
          {
            int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), pos, Utils.ToRotationVector2(MathHelper.ToRadians(angleInDegrees)), ModContent.ProjectileType<MutantDeathray3>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, turnRotation, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            if (index == Main.maxProjectiles || Main.projectile[index].timeLeft <= timeBeforeAttackEnds)
              return;
            Main.projectile[index].timeLeft = timeBeforeAttackEnds;
          }
        }
      }
      if ((double) this.NPC.ai[3] < 180.0 && (double) ++this.NPC.localAI[0] > 1.0)
      {
        this.NPC.localAI[0] = 0.0f;
        SpawnPrime(15f, 0.0f);
      }
      if ((double) ++this.NPC.ai[3] <= (double) num1)
        return;
      if (WorldSavingSystem.EternityMode)
      {
        this.ChooseNextAttack(11, 13, 16, 19, 21, 24, 29, 31, 33, 35, 37, 39, 41, 42, 45);
      }
      else
      {
        this.AttackChoice = 11f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
      }
      this.NPC.netUpdate = true;

      void SpawnPrime(float varianceInDegrees, float rotationInDegrees)
      {
        SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (!FargoSoulsUtil.HostCheck)
          return;
        float num = (Utils.NextBool(Main.rand) ? -1f : 1f) * Utils.NextFloat(Main.rand, 1400f, 1800f);
        float radians = MathHelper.ToRadians(varianceInDegrees);
        Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitY, this.NPC.ai[2]), 600f));
        Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(num, Utils.RotatedBy(Utils.RotatedByRandom(Vector2.UnitY, (double) radians), (double) MathHelper.ToRadians(rotationInDegrees), new Vector2())));
        Vector2 vector2_3 = Vector2.op_Multiply(32f, Vector2.Normalize(Vector2.op_Subtraction(vector2_1, vector2_2)));
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_2, vector2_3, ModContent.ProjectileType<MutantGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
    }

    private void PrepareFishron1()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 target;
      // ISSUE: explicit constructor call
      ((Vector2) ref target).\u002Ector(((Entity) this.player).Center.X, ((Entity) this.player).Center.Y + (float) (600 * Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) this.player).Center.Y)));
      this.Movement(target, 1.4f, false);
      if ((double) this.NPC.ai[1] == 0.0)
        this.NPC.ai[2] = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      if ((double) ++this.NPC.ai[1] <= 60.0 && (double) ((Entity) this.NPC).Distance(target) >= 64.0)
        return;
      ((Entity) this.NPC).velocity.X = 30f * this.NPC.ai[2];
      ((Entity) this.NPC).velocity.Y = 0.0f;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.GFBQuote(18));
    }

    private void SpawnFishrons()
    {
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.97f);
      if ((double) this.NPC.ai[1] == 0.0)
        this.NPC.ai[2] = (float) (Utils.NextBool(Main.rand) ? 1 : 0);
      int num1 = WorldSavingSystem.MasochistModeReal ? 3 : 2;
      if ((double) this.NPC.ai[1] % 3.0 == 0.0 && (double) this.NPC.ai[1] <= (double) (3 * num1))
      {
        if (FargoSoulsUtil.HostCheck)
        {
          int num2 = (double) this.NPC.ai[0] == 30.0 ? ModContent.ProjectileType<MutantFishron>() : ModContent.ProjectileType<MutantShadowHand>();
          for (int index1 = -1; index1 <= 1; index1 += 2)
          {
            int num3 = (int) this.NPC.ai[1] / 3;
            for (int index2 = -num3; index2 <= num3; ++index2)
            {
              if (Math.Abs(index2) == num3)
              {
                float num4 = 1.04719758f / (float) (num1 + 1);
                Vector2 vector2 = (double) this.NPC.ai[2] == 0.0 ? Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitY, (double) num4 * (double) index2, new Vector2()), -450f), (float) index1) : Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, (double) num4 * (double) index2, new Vector2()), 475f), (float) index1);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, num2, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, vector2.X, vector2.Y, 0.0f);
              }
            }
          }
        }
        for (int index3 = 0; index3 < 30; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 135, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].noLight = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
        }
      }
      if ((double) ++this.NPC.ai[1] <= (WorldSavingSystem.MasochistModeReal ? 60.0 : 120.0))
        return;
      this.ChooseNextAttack(13, 19, 20, 21, WorldSavingSystem.MasochistModeReal ? 44 : 26, 31, 31, 31, 33, 35, 39, 41, 42, 44);
    }

    private void PrepareTrueEyeDiveP2()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (400 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      center.Y += 400f;
      this.Movement(center, 1.2f);
      if ((double) ++this.NPC.ai[1] <= 60.0)
        return;
      ((Entity) this.NPC).velocity.X = (float) (30.0 * ((double) ((Entity) this.NPC).position.X < (double) ((Entity) this.player).position.X ? 1.0 : -1.0));
      if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
        ((Entity) this.NPC).velocity.Y *= -1f;
      ((Entity) this.NPC).velocity.Y *= 0.3f;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.netUpdate = true;
    }

    private void PrepareNuke()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (400 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      center.Y -= 400f;
      this.Movement(center, 1.2f, false);
      if ((double) ++this.NPC.ai[1] <= 60.0)
        return;
      SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if (FargoSoulsUtil.HostCheck)
      {
        float num1 = 0.2f;
        float num2 = WorldSavingSystem.MasochistModeReal ? 120f : 180f;
        Vector2 vector2 = Vector2.op_Subtraction(((Entity) this.player).Center, ((Entity) this.NPC).Center);
        vector2.X /= num2;
        vector2.Y = (float) ((double) vector2.Y / (double) num2 - 0.5 * (double) num1 * (double) num2);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantNuke>(), WorldSavingSystem.MasochistModeReal ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : 0, 0.0f, Main.myPlayer, num1, 0.0f, 0.0f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantFishronRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      }
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      if (Math.Sign(((Entity) this.player).Center.X - ((Entity) this.NPC).Center.X) == Math.Sign(((Entity) this.NPC).velocity.X))
        ((Entity) this.NPC).velocity.X *= -1f;
      if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
        ((Entity) this.NPC).velocity.Y *= -1f;
      ((Vector2) ref ((Entity) this.NPC).velocity).Normalize();
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 3f);
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.GFBQuote(19));
    }

    private void Nuke()
    {
      if (!this.AliveCheck(this.player))
        return;
      this.Movement((double) ((Entity) this.NPC).Bottom.Y < (double) ((Entity) this.player).Top.Y ? Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Vector2.op_Multiply(300f, Vector2.UnitX), (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X))) : Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(30f, Utils.RotatedBy(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), (double) MathHelper.ToRadians(60f) * (double) Math.Sign(((Entity) this.player).Center.X - ((Entity) this.NPC).Center.X), new Vector2()))), 0.1f);
      int num1 = WorldSavingSystem.MasochistModeReal ? 3 : 2;
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > (double) num1)
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), (float) num1);
      if ((double) this.NPC.ai[1] > (WorldSavingSystem.MasochistModeReal ? 120.0 : 180.0))
      {
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          FargoSoulsUtil.ScreenshakeRumble(6f);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 center = ((Entity) this.NPC).Center;
          center.Y -= 100f;
          for (int index = 0; index < 3; ++index)
          {
            Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.NextVector2Circular(Main.rand, 1200f, 1200f));
            if ((double) Vector2.Distance(center, vector2_1) < 350.0)
            {
              Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, center);
              ((Vector2) ref vector2_2).Normalize();
              vector2_1 = Vector2.op_Addition(center, Vector2.op_Multiply(vector2_2, Utils.NextFloat(Main.rand, 350f, 1200f)));
            }
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, Vector2.Zero, ModContent.ProjectileType<MutantBomb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) ++this.NPC.ai[1] > 360.0 + 210.0 * (double) this.endTimeVariance)
        this.ChooseNextAttack(11, 13, 16, 19, 24, WorldSavingSystem.MasochistModeReal ? 26 : 29, 31, 35, 37, 39, 41, 42);
      if ((double) this.NPC.ai[1] <= 45.0)
        return;
      for (int index = 0; index < 20; ++index)
      {
        Vector2 vector2 = new Vector2();
        vector2.Y -= 100f;
        double num2 = Main.rand.NextDouble() * 2.0 * Math.PI;
        vector2.X += (float) (Math.Sin(num2) * 150.0);
        vector2.Y += (float) (Math.Cos(num2) * 150.0);
        Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), new Vector2(4f, 4f)), 0, 0, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 100, Color.White, 1.5f)];
        dust1.velocity = ((Entity) this.NPC).velocity;
        if (Utils.NextBool(Main.rand, 3))
        {
          Dust dust2 = dust1;
          dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), 5f));
        }
        dust1.noGravity = true;
      }
    }

    private void PrepareSlimeRain()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (700 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      center.Y += 200f;
      this.Movement(center, 2f);
      if ((double) ++this.NPC.ai[2] <= 30.0 && (!WorldSavingSystem.MasochistModeReal || (double) ((Entity) this.NPC).Distance(center) >= 64.0))
        return;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.GFBQuote(20));
    }

    private void SlimeRain()
    {
      if ((double) this.NPC.ai[3] == 0.0)
      {
        this.NPC.ai[3] = 1f;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSlimeRain>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      }
      if ((double) this.NPC.ai[1] == 0.0)
      {
        int num = (double) this.NPC.localAI[0] == 0.0 ? 1 : 0;
        this.NPC.localAI[0] = (float) (Main.rand.Next(5, 9) * 120);
        if (num != 0)
        {
          if ((double) ((Entity) this.player).Center.X < (double) ((Entity) this.NPC).Center.X && (double) this.NPC.localAI[0] > 1200.0)
            this.NPC.localAI[0] -= 1200f;
          else if ((double) ((Entity) this.player).Center.X > (double) ((Entity) this.NPC).Center.X && (double) this.NPC.localAI[0] < 1200.0)
            this.NPC.localAI[0] += 1200f;
        }
        else if ((double) ((Entity) this.player).Center.X < (double) ((Entity) this.NPC).Center.X && (double) this.NPC.localAI[0] < 1200.0)
          this.NPC.localAI[0] += 1200f;
        else if ((double) ((Entity) this.player).Center.X > (double) ((Entity) this.NPC).Center.X && (double) this.NPC.localAI[0] > 1200.0)
          this.NPC.localAI[0] -= 1200f;
        this.NPC.localAI[0] += 60f;
        Vector2 center = ((Entity) this.NPC).Center;
        center.X -= 1200f;
        for (int index = -360; index <= 2760; index += 120)
        {
          if (FargoSoulsUtil.HostCheck && index + 60 != (int) this.NPC.localAI[0])
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), (float) ((double) center.X + (double) index + 60.0), center.Y, 0.0f, 0.0f, ModContent.ProjectileType<MutantReticle>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        if (WorldSavingSystem.MasochistModeReal)
        {
          this.NPC.ai[1] += 20f;
          this.NPC.ai[2] += 20f;
        }
      }
      if ((double) this.NPC.ai[1] > 120.0 && (double) this.NPC.ai[1] % 5.0 == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.player).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 center = ((Entity) this.NPC).Center;
          center.X -= 1200f;
          float off = -1300f;
          for (int index = -360; index <= 2760; index += 75)
          {
            float num = (float) (index + Main.rand.Next(75));
            if ((double) Math.Abs(num - this.NPC.localAI[0]) >= 110.0)
            {
              Vector2 pos = center;
              pos.X += num;
              Vector2 vel = Vector2.op_Multiply(Vector2.UnitY, Utils.NextFloat(Main.rand, 15f, 20f));
              Slime(pos, off, vel);
            }
          }
          Slime(Vector2.op_Addition(center, Vector2.op_Multiply(Vector2.UnitX, this.NPC.localAI[0] + 110f)), off, Vector2.op_Multiply(Vector2.UnitY, 20f));
          Slime(Vector2.op_Addition(center, Vector2.op_Multiply(Vector2.UnitX, this.NPC.localAI[0] - 110f)), off, Vector2.op_Multiply(Vector2.UnitY, 20f));
        }
      }
      if ((double) ++this.NPC.ai[1] > 180.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.ai[1] = 0.0f;
      }
      if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[1] == 120.0 && (double) this.NPC.ai[2] < 480.0 && Utils.NextBool(Main.rand, 3))
        this.NPC.ai[2] = 480f;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if (WorldSavingSystem.MasochistModeReal)
      {
        if ((double) this.NPC.ai[2] == 480.0)
        {
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.EdgyBossText(this.GFBQuote(21));
        }
        if ((double) this.NPC.ai[2] > 510.0)
        {
          if ((double) this.NPC.ai[1] > 170.0)
            this.NPC.ai[1] -= 30f;
          if ((double) this.NPC.localAI[1] == 0.0)
            this.NPC.localAI[1] = (float) Math.Sign(((Entity) this.NPC).Center.X - (((Entity) this.NPC).Center.X - 1200f + this.NPC.localAI[0]));
          NPC npc = this.NPC;
          ((Entity) npc).Center = Vector2.op_Addition(((Entity) npc).Center, Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Multiply(Vector2.UnitX, 1000f), 240f), this.NPC.localAI[1]));
        }
      }
      int num1 = 540;
      if (WorldSavingSystem.MasochistModeReal)
        num1 += 240 + (int) (300.0 * (double) this.endTimeVariance) - 30;
      if ((double) ++this.NPC.ai[2] <= (double) num1)
        return;
      this.ChooseNextAttack(11, 16, 19, 20, WorldSavingSystem.MasochistModeReal ? 26 : 29, 31, 33, 37, 39, 41, 42, 45);

      void Slime(Vector2 pos, float off, Vector2 vel)
      {
        int num1 = !WorldSavingSystem.MasochistModeReal || (double) this.NPC.ai[2] >= 360.0 || !Utils.NextBool(Main.rand) ? 1 : -1;
        Vector2 vector2 = Vector2.op_Addition(pos, Vector2.op_Multiply(Vector2.op_Multiply(off, Vector2.UnitY), (float) num1));
        float num2 = FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
        {
          ModContent.ProjectileType<MutantRitual>()
        }) == null ? 0.0f : ((Entity) this.NPC).Distance(((Entity) Main.projectile[this.ritualProj]).Center);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.op_Multiply(Vector2.op_Multiply(vel, (float) num1), 2f), ModContent.ProjectileType<MutantSlimeBall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num2, 0.0f, 0.0f);
      }
    }

    private void QueenSlimeRain()
    {
      if ((double) this.NPC.ai[3] == 0.0)
      {
        this.NPC.ai[3] = 1f;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSlimeRain>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
      }
      if ((double) this.NPC.ai[1] == 0.0)
      {
        this.NPC.localAI[0] = (float) (Main.rand.Next(6, 9) * 120);
        if ((double) ((Entity) this.player).Center.X > (double) ((Entity) this.NPC).Center.X)
          this.NPC.localAI[0] += 600f;
        this.NPC.localAI[0] += 60f;
        Vector2 center = ((Entity) this.NPC).Center;
        center.X -= 1200f;
        for (int index = -360; index <= 2760; index += 120)
        {
          if (FargoSoulsUtil.HostCheck && index + 60 != (int) this.NPC.localAI[0])
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), (float) ((double) center.X + (double) index + 60.0), center.Y, 0.0f, 0.0f, ModContent.ProjectileType<MutantReticle>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 1f);
        }
      }
      if ((double) this.NPC.ai[1] > 60.0 && (double) this.NPC.ai[1] % 3.0 == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.player).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          int frame = Main.rand.Next(3);
          Vector2 center = ((Entity) this.NPC).Center;
          center.X -= 1200f;
          float off = -1300f;
          for (int index = 0; index < 2400; index += 110)
          {
            float num1 = this.NPC.localAI[0] + 110f + (float) index;
            if ((double) center.X + (double) num1 < (double) ((Entity) this.NPC).Center.X + 1200.0)
              Slime(Vector2.op_Addition(center, Vector2.op_Multiply(Vector2.UnitX, num1)), off, Vector2.op_Multiply(Vector2.UnitY, 20f));
            float num2 = this.NPC.localAI[0] - 110f - (float) index;
            if ((double) center.X + (double) num2 > (double) ((Entity) this.NPC).Center.X - 1200.0)
              Slime(Vector2.op_Addition(center, Vector2.op_Multiply(Vector2.UnitX, num2)), off, Vector2.op_Multiply(Vector2.UnitY, 20f));
          }

          void Slime(Vector2 pos, float off, Vector2 vel)
          {
            Vector2 vector2 = Vector2.op_Addition(pos, Vector2.op_Multiply(Vector2.op_Multiply(off, Vector2.UnitY), 1f));
            float num = FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
            {
              ModContent.ProjectileType<MutantRitual>()
            }) == null ? 0.0f : ((Entity) this.NPC).Distance(((Entity) Main.projectile[this.ritualProj]).Center);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.op_Multiply(vel, 1f), ModContent.ProjectileType<MutantSlimeSpike>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num, 0.0f, (float) frame);
          }
        }
      }
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) this.NPC.ai[1] == 60.0)
      {
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.EdgyBossText(this.GFBQuote(21));
      }
      if ((double) this.NPC.ai[1] > 60.0 && (double) --this.NPC.ai[2] < 0.0)
      {
        float num3 = WorldSavingSystem.MasochistModeReal ? 7f : 6f;
        if ((double) --this.NPC.localAI[2] < 0.0)
        {
          float num4 = ((Entity) this.NPC).Center.X - 1200f + this.NPC.localAI[0];
          this.NPC.localAI[1] = (float) Math.Sign(((Entity) this.NPC).Center.X - num4);
          float num5 = Math.Abs(((Entity) this.NPC).Center.X + 1200f * this.NPC.localAI[1] - num4);
          this.NPC.localAI[2] = MathHelper.Lerp(Math.Abs(((Entity) this.NPC).Center.X - num4) + 100f, num5, Utils.NextFloat(Main.rand, 0.6f)) / num3;
          this.NPC.ai[2] = WorldSavingSystem.MasochistModeReal ? 15f : 30f;
        }
        this.NPC.localAI[0] += num3 * this.NPC.localAI[1];
      }
      if ((double) ++this.NPC.ai[1] <= (double) (420 + (int) (300.0 * (double) this.endTimeVariance)))
        return;
      this.ChooseNextAttack(11, 16, 19, 20, WorldSavingSystem.MasochistModeReal ? 26 : 29, 31, 33, 37, 39, 41, 42, 45);
    }

    private void PrepareFishron2()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (400 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      center.Y -= 400f;
      this.Movement(center, 0.9f);
      if ((double) ++this.NPC.ai[1] <= 60.0 && (!WorldSavingSystem.MasochistModeReal || (double) ((Entity) this.NPC).Distance(center) >= 32.0))
        return;
      ((Entity) this.NPC).velocity.X = (float) (35.0 * ((double) ((Entity) this.NPC).position.X < (double) ((Entity) this.player).position.X ? 1.0 : -1.0));
      ((Entity) this.NPC).velocity.Y = 10f;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.GFBQuote(18));
    }

    private void PrepareOkuuSpheresP2()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.player, ((Entity) this.NPC).Center), 450f));
      if ((double) ++this.NPC.ai[1] < 180.0 && (double) ((Entity) this.NPC).Distance(target) > 50.0)
      {
        this.Movement(target, 0.8f);
      }
      else
      {
        this.NPC.netUpdate = true;
        ++this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
      }
    }

    private void OkuuSpheresP2()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      int num = 420 + (int) (300.0 * (double) this.endTimeVariance);
      if ((double) ++this.NPC.ai[1] > 10.0 && (double) this.NPC.ai[3] > 60.0 && (double) this.NPC.ai[3] < (double) (num - 60))
      {
        this.NPC.ai[1] = 0.0f;
        float offset = (float) ((double) MathHelper.ToRadians(60f) * ((double) this.NPC.ai[3] - 45.0) / 240.0) * this.NPC.ai[2];
        int max = WorldSavingSystem.MasochistModeReal ? 10 : 9;
        float speed = WorldSavingSystem.MasochistModeReal ? 11f : 10f;
        this.SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), -1f, offset);
        this.SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 1f, offset);
      }
      if ((double) this.NPC.ai[2] == 0.0)
      {
        this.NPC.ai[2] = Utils.NextBool(Main.rand) ? -1f : 1f;
        this.NPC.ai[3] = Utils.NextFloat(Main.rand, 6.28318548f);
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -2f, 0.0f);
        this.EdgyBossText(this.GFBQuote(22));
      }
      if ((double) ++this.NPC.ai[3] > (double) num)
        this.ChooseNextAttack(13, 19, 20, WorldSavingSystem.MasochistModeReal ? 13 : 26, WorldSavingSystem.MasochistModeReal ? 44 : 33, 41, 44);
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    private void SpearTossDirectP2()
    {
      if (!this.AliveCheck(this.player))
        return;
      if ((double) this.NPC.ai[1] == 0.0)
      {
        this.NPC.localAI[0] = MathHelper.WrapAngle(Utils.ToRotation(Vector2.op_Subtraction(((Entity) this.NPC).Center, ((Entity) this.player).Center)));
        if (WorldSavingSystem.EternityMode)
          this.NPC.localAI[1] = (float) Main.rand.Next(WorldSavingSystem.MasochistModeReal ? 3 : 5, 9);
        else
          this.NPC.localAI[1] = 5f;
        if (WorldSavingSystem.MasochistModeReal)
        {
          this.NPC.localAI[1] += (float) Main.rand.Next(6);
          if (Main.getGoodWorld)
            this.NPC.localAI[1] += 5f;
        }
        this.NPC.localAI[2] = Utils.NextBool(Main.rand) ? -1f : 1f;
        this.NPC.netUpdate = true;
      }
      Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(500f, Utils.RotatedBy(Vector2.UnitX, Math.PI / 150.0 * (double) this.NPC.ai[3] * (double) this.NPC.localAI[2] + (double) this.NPC.localAI[0], new Vector2())));
      if ((double) ((Entity) this.NPC).Distance(target) > 25.0)
        this.Movement(target, 0.6f);
      ++this.NPC.ai[3];
      if ((double) ++this.NPC.ai[1] > 180.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[1] = 150f;
        bool flag = true;
        if ((double) ++this.NPC.ai[2] > (double) this.NPC.localAI[1])
        {
          if (Main.getGoodWorld)
            this.ChooseNextAttack(11, 16, 19, 20, WorldSavingSystem.MasochistModeReal ? 44 : 26, 31, 33, 42, 44, 45);
          else
            this.ChooseNextAttack(11, 16, 19, 20, WorldSavingSystem.MasochistModeReal ? 44 : 26, 31, 33, 35, 42, 44, 45);
          flag = false;
        }
        if (!flag && !WorldSavingSystem.MasochistModeReal)
          return;
        Attack();
      }
      else if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[1] == 165.0)
        Attack();
      else if ((double) this.NPC.ai[1] == 151.0)
      {
        if ((double) this.NPC.ai[2] <= 0.0 || (double) this.NPC.ai[2] >= (double) this.NPC.localAI[1] && !WorldSavingSystem.MasochistModeReal || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 1f, 0.0f);
      }
      else
      {
        if ((double) this.NPC.ai[1] != 1.0 || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MutantSpearAim>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -1f, 0.0f);
      }

      void Attack()
      {
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) this.player).Center), 30f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(vector2), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.Normalize(vector2)), ModContent.ProjectileType<MutantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<MutantSpearThrown>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, 0.0f, 0.0f);
        }
        this.EdgyBossText(this.RandomObnoxiousQuote());
      }
    }

    private void PrepareTwinRangsAndCrystals()
    {
      if (!this.AliveCheck(this.player))
        return;
      Vector2 center = ((Entity) this.player).Center;
      center.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
      if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
        this.Movement(center, 0.8f);
      if ((double) ++this.NPC.ai[1] <= 45.0)
        return;
      this.NPC.netUpdate = true;
      ++this.AttackChoice;
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      this.EdgyBossText(this.GFBQuote(23));
    }

    private void TwinRangsAndCrystals()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) this.NPC.ai[3] == 0.0)
      {
        this.NPC.localAI[0] = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center));
        if (!WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
        {
          for (int index = 0; index < 4; ++index)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index, new Vector2()), 525f)), Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index + Math.PI / 4.0, new Vector2()), 350f)), Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 2f, 0.0f, 0.0f);
          }
        }
      }
      int num1 = WorldSavingSystem.MasochistModeReal ? 12 : 15;
      int num2 = WorldSavingSystem.MasochistModeReal ? 5 : 4;
      if ((double) this.NPC.ai[3] % (double) num1 == 0.0 && (double) this.NPC.ai[3] < (double) (num1 * num2) && FargoSoulsUtil.HostCheck)
      {
        float num3 = 6.28318548f / (float) num2 * this.NPC.ai[3] / (float) num1 + this.NPC.localAI[0];
        int num4 = 60;
        float num5 = (float) (120.0 + (double) this.NPC.ai[3] / (double) num1 * (WorldSavingSystem.MasochistModeReal ? 40.0 : 50.0));
        int index1 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(300f / (float) num4, Utils.RotatedBy(Vector2.UnitX, (double) num3, new Vector2())), ModContent.ProjectileType<MutantMark2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) num4, (float) num4 + num5, 0.0f);
        if (index1 != Main.maxProjectiles)
        {
          float num6 = 1.2566371f;
          for (int index2 = 0; index2 < 5; ++index2)
          {
            float num7 = num6 * (float) index2 + num3;
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(125f, 0.0f), (double) num7, new Vector2()));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<MutantCrystalLeaf>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) Main.projectile[index1].identity, num7, 0.0f);
          }
        }
      }
      if ((double) this.NPC.ai[3] > 45.0 && (double) --this.NPC.ai[1] < 0.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[1] = 20f;
        this.NPC.ai[2] = (double) this.NPC.ai[2] > 0.0 ? -1f : 1f;
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck && (double) this.NPC.ai[3] < 330.0)
        {
          float num8 = 10.995575f;
          float num9 = 12.2173052f;
          float num10 = (float) ((double) num8 * (double) num8 / 525.0) * this.NPC.ai[2];
          float num11 = (float) ((double) num9 * (double) num9 / 350.0 * -(double) this.NPC.ai[2]);
          float num12 = WorldSavingSystem.MasochistModeReal ? 0.7853982f : 0.0f;
          for (int index = 0; index < 4; ++index)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index + (double) num12, new Vector2()), num8), ModContent.ProjectileType<MutantRetirang>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num10, 300f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index + Math.PI / 4.0 + (double) num12, new Vector2()), num9), ModContent.ProjectileType<MutantSpazmarang>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num11, 180f, 0.0f);
          }
        }
      }
      if ((double) ++this.NPC.ai[3] <= 450.0)
        return;
      this.ChooseNextAttack(11, 13, 16, 21, 24, 26, 29, 31, 33, 35, 39, 41, 44, 45);
    }

    private void EmpressSwordWave()
    {
      if (!this.AliveCheck(this.player))
        return;
      if (!WorldSavingSystem.EternityMode)
      {
        ++this.AttackChoice;
      }
      else
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        int num1 = WorldSavingSystem.MasochistModeReal ? 48 : 60;
        int num2 = 4 + (int) Math.Round(3.0 * (double) this.endTimeVariance);
        int num3 = 90;
        if ((double) this.NPC.ai[1] == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.NPC.ai[3] = Utils.NextFloat(Main.rand, 6.28318548f);
          this.EdgyBossText(this.GFBQuote(24));
        }
        if ((double) this.NPC.ai[1] >= (double) num3 && (double) this.NPC.ai[1] < (double) (num3 + num1 * num2) && (double) --this.NPC.ai[2] < 0.0)
        {
          this.NPC.ai[2] = (float) num1;
          SoundEngine.PlaySound(ref SoundID.Item163, new Vector2?(((Entity) this.player).Center), (SoundUpdateCallback) null);
          if ((double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center)) - this.NPC.ai[3])) > 1.5707963705062866)
            this.NPC.ai[3] += 3.14159274f;
          int num4 = WorldSavingSystem.MasochistModeReal ? 16 : 12;
          float num5 = (float) (3200 / num4);
          float num6 = this.NPC.ai[3];
          Vector2 vector2_1 = Vector2.op_UnaryNegation(Utils.ToRotationVector2(num6));
          Vector2 vector2_2 = ((Entity) this.player).Center;
          Vector2 center = ((Entity) this.NPC).Center;
          Vector2 vector2_3;
          for (float num7 = 0.0f; (double) num7 < 1200.0; num7 += num5)
          {
            Vector2 vector2_4 = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(num5, Utils.ToRotationVector2(num6)));
            vector2_3 = Vector2.op_Subtraction(center, vector2_4);
            double num8 = (double) ((Vector2) ref vector2_3).Length();
            vector2_3 = Vector2.op_Subtraction(center, vector2_2);
            double num9 = (double) ((Vector2) ref vector2_3).Length();
            if (num8 <= num9)
              vector2_2 = vector2_4;
            else
              break;
          }
          float num10 = 0.0f;
          while ((double) num10 < 1200.0)
            num10 += num5;
          float num11 = 2f * (float) Math.Sqrt(2.0 * (double) num10 * (double) num10);
          int num12 = 0;
          for (int index = -num4; index <= num4; ++index)
          {
            Vector2 vector2_5 = Vector2.op_Addition(vector2_2, Vector2.op_Multiply(vector2_1, num10));
            Vector2 vector2_6 = vector2_1;
            vector2_3 = new Vector2();
            Vector2 vector2_7 = vector2_3;
            Vector2 vector2_8 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(vector2_6, 1.5707963705062866, vector2_7), num5), (float) index);
            Vector2 pos = Vector2.op_Addition(vector2_5, vector2_8);
            float ai1 = (float) num12++ / (float) ((double) num4 * 2.0 + 1.0);
            Vector2 vel = Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f);
            if (WorldSavingSystem.MasochistModeReal)
            {
              if ((double) ((Vector2) ref vel).Length() < 0.5)
                vel = Vector2.op_Multiply(0.5f, Utils.SafeNormalize(vel, Vector2.UnitX));
              vel = Vector2.op_Multiply(vel, 2f);
            }
            Sword(pos, num6 + 0.7853982f, ai1, vel);
            Sword(pos, num6 - 0.7853982f, ai1, vel);
            if (WorldSavingSystem.MasochistModeReal)
            {
              Sword(Vector2.op_Addition(pos, Vector2.op_Multiply(num11, Utils.ToRotationVector2(num6 + 0.7853982f))), (float) ((double) num6 + 0.78539818525314331 + 3.1415927410125732), ai1, vel);
              Sword(Vector2.op_Addition(pos, Vector2.op_Multiply(num11, Utils.ToRotationVector2(num6 - 0.7853982f))), (float) ((double) num6 - 0.78539818525314331 + 3.1415927410125732), ai1, vel);
            }
          }
          this.NPC.ai[3] += (float) (0.78539818525314331 * (Utils.NextBool(Main.rand) ? -1.0 : 1.0) + (double) Utils.NextFloat(Main.rand, 0.3926991f) * (Utils.NextBool(Main.rand) ? -1.0 : 1.0));
          this.NPC.netUpdate = true;
        }
        int num13 = num3 + num1 * num2 + 40;
        if ((double) this.NPC.ai[1] == (double) num13)
        {
          MegaSwordSwarm(((Entity) this.player).Center);
          this.NPC.localAI[0] = ((Entity) this.player).Center.X;
          this.NPC.localAI[1] = ((Entity) this.player).Center.Y;
        }
        if (WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[1] == (double) (num13 + 30))
        {
          for (int index = -1; index <= 1; index += 2)
            MegaSwordSwarm(Vector2.op_Addition(new Vector2(this.NPC.localAI[0], this.NPC.localAI[1]), Vector2.op_Multiply((float) (600 * index), Utils.ToRotationVector2(this.NPC.ai[3]))));
        }
        if ((double) ++this.NPC.ai[1] <= (double) (num13 + (WorldSavingSystem.MasochistModeReal ? 60 : 30)))
          return;
        this.ChooseNextAttack(11, 13, 16, 21, WorldSavingSystem.MasochistModeReal ? 26 : 24, 29, 31, 35, 37, 39, 41, 45);
      }

      void Sword(Vector2 pos, float ai0, float ai1, Vector2 vel)
      {
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(pos, Vector2.op_Multiply(vel, 60f)), vel, 919, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, ai0, ai1, 0.0f);
      }

      void MegaSwordSwarm(Vector2 target)
      {
        SoundEngine.PlaySound(ref SoundID.Item164, new Vector2?(((Entity) this.player).Center), (SoundUpdateCallback) null);
        float num1 = this.NPC.ai[3];
        float radians = MathHelper.ToRadians(10f);
        int num2 = 60;
        for (int index = 0; index < num2; ++index)
        {
          float num3 = Utils.NextFloat(Main.rand, radians, 3.14159274f - radians);
          Vector2 vector2_1 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 600f, 2400f), Utils.ToRotationVector2(num1 + num3));
          if (Utils.NextBool(Main.rand))
            vector2_1 = Vector2.op_Multiply(vector2_1, -1f);
          Vector2 pos = Vector2.op_Addition(target, vector2_1);
          Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Subtraction(target, pos), 60f);
          Sword(pos, Utils.ToRotation(vector2_2), (float) index / (float) num2, Vector2.op_Multiply(Vector2.op_UnaryNegation(vector2_2), 0.75f));
        }
        this.EdgyBossText(this.GFBQuote(25));
      }
    }

    private void SANSGOLEM()
    {
      this.Movement(Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), 300f)), 0.3f);
      int num1 = WorldSavingSystem.MasochistModeReal ? 50 : 70;
      if ((double) this.NPC.ai[1] > 0.0 && (double) this.NPC.ai[1] % (double) num1 == 0.0)
      {
        this.EdgyBossText(this.GFBQuote(35));
        float num2 = this.NPC.ai[2];
        while ((double) this.NPC.ai[2] == (double) num2)
          this.NPC.ai[2] = (float) Main.rand.Next(-1, 2);
        Vector2 vector2_1 = FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
        {
          ModContent.ProjectileType<MutantRitual>()
        }) == null ? ((Entity) this.player).Center : ((Entity) Main.projectile[this.ritualProj]).Center;
        float num3 = 150f;
        float num4 = (float) ((double) num3 / 3.0 * 0.75);
        vector2_1.Y += num3 * this.NPC.ai[2];
        vector2_1.Y += Utils.NextFloat(Main.rand, -num4, num4);
        for (int index1 = -1; index1 <= 1; index1 += 2)
        {
          float num5 = Utils.NextFloat(Main.rand, 8f, 20f);
          for (int index2 = -1; index2 <= 1; index2 += 2)
          {
            float num6 = WorldSavingSystem.MasochistModeReal ? 120f : 150f;
            Vector2 vector2_2 = vector2_1;
            vector2_2.X += num5 * 60f * (float) index1;
            vector2_2.Y += num6 * (float) index2;
            int num7 = 50;
            Vector2 vector2_3 = Vector2.op_Division(Vector2.op_Subtraction(vector2_2, ((Entity) this.NPC).Center), (float) num7);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_3, ModContent.ProjectileType<MutantSansHead>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) num7, num5 * (float) -index1, (float) index2);
          }
        }
      }
      if ((double) ++this.NPC.ai[1] <= (double) (num1 * 7 - 5 + num1 * (int) Math.Round(4.0 * (double) this.endTimeVariance)))
        return;
      this.ChooseNextAttack(13, 19, 20, 21, 24, 31, 33, 35, 41, 44);
    }

    private void P2NextAttackPause()
    {
      if (!this.AliveCheck(this.player))
        return;
      this.EModeSpecialEffects();
      Vector2 target = Vector2.op_Addition(((Entity) this.player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center), 400f));
      this.Movement(target, 0.3f);
      if ((double) ((Entity) this.NPC).Distance(target) > 200.0)
        this.Movement(target, 0.3f);
      if ((double) ++this.NPC.ai[1] <= 60.0 && ((double) ((Entity) this.NPC).Distance(target) >= 200.0 || (double) this.NPC.ai[1] <= ((double) this.NPC.localAI[3] >= 3.0 ? 15.0 : 30.0)))
        return;
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, WorldSavingSystem.MasochistModeReal ? 0.25f : 0.75f);
      this.AttackChoice = this.NPC.ai[2];
      this.NPC.ai[1] = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.netUpdate = true;
      this.EdgyBossText(this.RandomObnoxiousQuote());
    }

    private bool Phase3Transition()
    {
      bool flag = true;
      this.NPC.localAI[3] = 3f;
      this.EModeSpecialEffects();
      if (this.NPC.buffType[0] != 0)
        this.NPC.DelBuff(0);
      if ((double) this.NPC.ai[1] == 0.0)
      {
        this.NPC.life = this.NPC.lifeMax;
        this.DramaticTransition(true);
      }
      if ((double) this.NPC.ai[1] < 60.0 && !Main.dedServ && ((Entity) Main.LocalPlayer).active)
        FargoSoulsUtil.ScreenshakeRumble(6f);
      if ((double) this.NPC.ai[1] == 360.0)
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if ((double) ++this.NPC.ai[1] > 480.0)
      {
        flag = false;
        if (!this.AliveCheck(this.player))
          return flag;
        Vector2 center = ((Entity) this.player).Center;
        center.Y -= 300f;
        this.Movement(center, 1f, obeySpeedCap: false);
        if ((double) ((Entity) this.NPC).Distance(center) < 50.0 || (double) this.NPC.ai[1] > 720.0)
        {
          this.NPC.netUpdate = true;
          ((Entity) this.NPC).velocity = Vector2.Zero;
          this.NPC.localAI[0] = 0.0f;
          --this.AttackChoice;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center));
          this.NPC.ai[3] = 0.157079637f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if ((double) ((Entity) this.player).Center.X < (double) ((Entity) this.NPC).Center.X)
            this.NPC.ai[3] *= -1f;
          this.EdgyBossText(this.GFBQuote(26));
        }
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
        if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0)
        {
          Main.LocalPlayer.controlUseItem = false;
          Main.LocalPlayer.controlUseTile = false;
          Main.LocalPlayer.FargoSouls().NoUsingItems = 2;
        }
        if ((double) --this.NPC.localAI[0] < 0.0)
        {
          this.NPC.localAI[0] = (float) Main.rand.Next(15);
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height)));
            int num = ModContent.ProjectileType<MutantBombSmall>();
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      return flag;
    }

    private void VoidRaysP3()
    {
      if ((double) --this.NPC.ai[1] < 0.0)
      {
        if (FargoSoulsUtil.HostCheck)
        {
          float num = !WorldSavingSystem.MasochistModeReal || (double) this.NPC.localAI[0] > 40.0 ? 2f : 4f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(num, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[2], new Vector2())), ModContent.ProjectileType<MutantMark1>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        this.NPC.ai[1] = 1f;
        this.NPC.ai[2] += this.NPC.ai[3];
        if ((double) this.NPC.localAI[0] < 30.0)
        {
          this.EModeSpecialEffects();
          this.TryMasoP3Theme();
        }
        if ((double) this.NPC.localAI[0]++ == 40.0 || (double) this.NPC.localAI[0] == 80.0 || (double) this.NPC.localAI[0] == 120.0)
        {
          this.NPC.netUpdate = true;
          this.NPC.ai[2] -= this.NPC.ai[3] / (WorldSavingSystem.MasochistModeReal ? 3f : 2f);
        }
        else if ((double) this.NPC.localAI[0] >= (WorldSavingSystem.MasochistModeReal ? 160.0 : 120.0))
        {
          this.NPC.netUpdate = true;
          --this.AttackChoice;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.localAI[0] = 0.0f;
          this.EdgyBossText(this.GFBQuote(27));
        }
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      ((Entity) this.NPC).velocity = Vector2.Zero;
    }

    private void OkuuSpheresP3()
    {
      if ((double) this.NPC.ai[2] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.ai[2] = Utils.NextBool(Main.rand) ? -1f : 1f;
        this.NPC.ai[3] = Utils.NextFloat(Main.rand, 6.28318548f);
      }
      int num = 480;
      if (WorldSavingSystem.MasochistModeReal)
        num += 360;
      if ((double) ++this.NPC.ai[1] > 10.0 && (double) this.NPC.ai[3] > 60.0 && (double) this.NPC.ai[3] < (double) (num - 120))
      {
        this.NPC.ai[1] = 0.0f;
        float offset = (float) ((double) MathHelper.ToRadians(45f) * ((double) this.NPC.ai[3] - 60.0) / 240.0) * this.NPC.ai[2];
        int max = WorldSavingSystem.MasochistModeReal ? 11 : 10;
        float speed = WorldSavingSystem.MasochistModeReal ? 11f : 10f;
        this.SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), -0.75f, offset);
        this.SpawnSphereRing(max, speed, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.75f, offset);
      }
      if ((double) this.NPC.ai[3] < 30.0)
      {
        this.EModeSpecialEffects();
        this.TryMasoP3Theme();
      }
      if ((double) this.NPC.ai[3] == (double) (num / 2))
        this.EdgyBossText(this.GFBQuote(28));
      if ((double) ++this.NPC.ai[3] > (double) num)
      {
        this.NPC.netUpdate = true;
        --this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.EdgyBossText(this.GFBQuote(29));
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      ((Entity) this.NPC).velocity = Vector2.Zero;
    }

    private void BoundaryBulletHellP3()
    {
      if ((double) this.NPC.localAI[0] == 0.0)
      {
        if (!this.AliveCheck(this.player))
          return;
        this.NPC.localAI[0] = (float) Math.Sign(((Entity) this.NPC).Center.X - ((Entity) this.player).Center.X);
      }
      if ((double) ++this.NPC.ai[1] > 3.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] += (float) (0.0014959964901208878 * (double) this.NPC.ai[3] * (double) this.NPC.localAI[0] * (WorldSavingSystem.MasochistModeReal ? 2.0 : 1.0));
        if ((double) this.NPC.ai[2] > 3.1415927410125732)
          this.NPC.ai[2] -= 6.28318548f;
        if (FargoSoulsUtil.HostCheck)
        {
          int num = WorldSavingSystem.MasochistModeReal ? 10 : 8;
          for (int index = 0; index < num; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(0.0f, -6f), (double) this.NPC.ai[2] + 6.2831854820251465 / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<MutantEye>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) this.NPC.ai[3] < 30.0)
      {
        this.EModeSpecialEffects();
        this.TryMasoP3Theme();
      }
      int num1 = 360;
      if (WorldSavingSystem.MasochistModeReal)
        num1 += 360;
      if ((double) this.NPC.ai[3] == (double) (num1 / 2))
        this.EdgyBossText(this.GFBQuote(30));
      if ((double) ++this.NPC.ai[3] > (double) num1)
      {
        --this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.netUpdate = true;
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
      ((Entity) this.NPC).velocity = Vector2.Zero;
    }

    private void FinalSpark()
    {
      if (!this.AliveCheck(this.player))
        return;
      if ((double) --this.NPC.localAI[0] < 0.0)
      {
        this.NPC.localAI[0] = (float) Main.rand.Next(30);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height)));
          int num = ModContent.ProjectileType<MutantBombSmall>();
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) ++this.NPC.ai[1] > ((!WorldSavingSystem.MasochistModeReal ? 0 : ((double) this.NPC.ai[2] >= 330.0 ? 1 : 0)) != 0 ? 100.0 : 120.0))
      {
        this.NPC.ai[1] = 0.0f;
        this.EModeSpecialEffects();
        this.TryMasoP3Theme();
        if (FargoSoulsUtil.HostCheck)
        {
          int max = 10;
          int damage = FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
          this.SpawnSphereRing(max, 6f, damage, 0.5f);
          this.SpawnSphereRing(max, 6f, damage, -0.5f);
        }
      }
      if ((double) this.NPC.ai[2] == 0.0)
      {
        if (!WorldSavingSystem.MasochistModeReal)
          this.NPC.localAI[1] = 1f;
      }
      else if ((double) this.NPC.ai[2] == 330.0)
      {
        if ((double) this.NPC.localAI[1] == 0.0)
        {
          this.NPC.localAI[1] = 1f;
          this.NPC.ai[2] -= 780f;
          this.NPC.ai[3] -= MathHelper.ToRadians(20f);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3], new Vector2()), ModContent.ProjectileType<MutantGiantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.5f), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          this.NPC.netUpdate = true;
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < 8; ++index)
            {
              float num = (float) index - 0.5f;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.ToRotationVector2(this.NPC.ai[3] + 0.7853982f * num), ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 13f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
          }
        }
      }
      if ((double) this.NPC.ai[2] < 420.0)
      {
        if ((double) this.NPC.localAI[1] == 0.0 || (double) this.NPC.ai[2] > 330.0)
          this.NPC.ai[3] = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) this.player).Center));
      }
      else
      {
        if (!Main.dedServ)
        {
          ShaderManager.GetFilter("FargowiltasSouls.FinalSpark").Activate();
          if (SoulConfig.Instance.ForcedFilters && Main.WaveQuality == 0)
            Main.WaveQuality = 1;
        }
        if ((double) this.NPC.ai[1] % 3.0 == 0.0 && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(24f, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3], new Vector2())), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0.0f, Main.myPlayer, Utils.NextFloat(Main.rand, 0.5f, 1.25f) * (Utils.NextBool(Main.rand) ? -1f : 1f), (float) Main.rand.Next(10, 60), 0.0f);
      }
      int num1 = 1020;
      if (WorldSavingSystem.MasochistModeReal)
        num1 += 180;
      if ((double) ++this.NPC.ai[2] > (double) num1 && this.NPC.life <= 1)
      {
        this.NPC.netUpdate = true;
        --this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        FargoSoulsUtil.ClearAllProjectiles(2, ((Entity) this.NPC).whoAmI);
      }
      else if ((double) this.NPC.ai[2] == 420.0)
      {
        this.NPC.netUpdate = true;
        this.NPC.ai[3] += MathHelper.ToRadians(20f) * (WorldSavingSystem.MasochistModeReal ? 1f : -1f);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3], new Vector2()), ModContent.ProjectileType<MutantGiantDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.5f), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }
      else if ((double) this.NPC.ai[2] < 300.0 && (double) this.NPC.localAI[1] != 0.0)
      {
        float num2 = 0.99f;
        if ((double) this.NPC.ai[2] >= 60.0)
          num2 = 0.79f;
        if ((double) this.NPC.ai[2] >= 120.0)
          num2 = 0.58f;
        if ((double) this.NPC.ai[2] >= 180.0)
          num2 = 0.43f;
        if ((double) this.NPC.ai[2] >= 240.0)
          num2 = 0.33f;
        for (int index = 0; index < 9; ++index)
        {
          if ((double) Utils.NextFloat(Main.rand) >= (double) num2)
          {
            float num3 = Utils.NextFloat(Main.rand) * 6.283185f;
            float num4 = Utils.NextFloat(Main.rand);
            Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num3), (float) (110.0 + 600.0 * (double) num4))), 229, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num3 - 3.141593f), (float) (14.0 + 8.0 * (double) num4))), 0, new Color(), 1f);
            dust.scale = 0.9f;
            dust.fadeIn = (float) (1.1499999761581421 + (double) num4 * 0.30000001192092896);
            dust.noGravity = true;
          }
        }
      }
      SpinLaser(WorldSavingSystem.MasochistModeReal && (double) this.NPC.ai[2] >= 420.0);
      if (this.AliveCheck(this.player))
        this.NPC.localAI[2] = 0.0f;
      else
        ++this.NPC.localAI[2];
      ((Entity) this.NPC).velocity = Vector2.Zero;

      void SpinLaser(bool useMasoSpeed)
      {
        float rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center));
        float num1 = MathHelper.WrapAngle(rotation - this.NPC.ai[3]);
        float num2 = Math.Min((float) (Math.PI / 180.0 * (useMasoSpeed ? 1.1000000238418579 : 1.0)), Math.Abs(num1)) * (float) Math.Sign(num1);
        if (useMasoSpeed)
        {
          num2 *= 1.1f;
          float num3 = Utils.AngleLerp(this.NPC.ai[3], rotation, 0.015f) - this.NPC.ai[3];
          if ((double) Math.Abs(MathHelper.WrapAngle(num3)) > (double) Math.Abs(MathHelper.WrapAngle(num2)))
            num2 = num3;
        }
        this.NPC.ai[3] += num2;
        this.EdgyBossText(this.GFBQuote(31));
      }
    }

    private void DyingDramaticPause()
    {
      if (!this.AliveCheck(this.player))
        return;
      this.NPC.ai[3] -= (float) Math.PI / 360f;
      ((Entity) this.NPC).velocity = Vector2.Zero;
      if ((double) ++this.NPC.ai[1] > 120.0)
      {
        this.NPC.netUpdate = true;
        --this.AttackChoice;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[3] = -1.57079637f;
        this.NPC.netUpdate = true;
        if (FargoSoulsUtil.HostCheck)
        {
          int num = WorldSavingSystem.MasochistModeReal ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.5f) : 0;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, -1f), ModContent.ProjectileType<MutantGiantDeathray2>(), num, 0.0f, Main.myPlayer, 1f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        this.EdgyBossText(this.GFBQuote(32));
      }
      if ((double) --this.NPC.localAI[0] < 0.0)
      {
        this.NPC.localAI[0] = (float) Main.rand.Next(15);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height)));
          int num = ModContent.ProjectileType<MutantBomb>();
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
      }
    }

    private void DyingAnimationAndHandling()
    {
      ((Entity) this.NPC).velocity = Vector2.Zero;
      for (int index1 = 0; index1 < 5; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
      }
      if ((double) --this.NPC.localAI[0] < 0.0)
      {
        this.NPC.localAI[0] = (float) Main.rand.Next(5);
        if (FargoSoulsUtil.HostCheck)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.NextVector2Circular(Main.rand, 240f, 240f));
          int num = ModContent.ProjectileType<MutantBomb>();
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) ++this.NPC.ai[1] % 3.0 == 0.0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(24f, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.ai[3], new Vector2())), ModContent.ProjectileType<MutantEyeWavy>(), 0, 0.0f, Main.myPlayer, Utils.NextFloat(Main.rand, 0.75f, 1.5f) * (Utils.NextBool(Main.rand) ? -1f : 1f), (float) Main.rand.Next(10, 90), 0.0f);
      if (++this.NPC.alpha <= (int) byte.MaxValue)
        return;
      this.NPC.alpha = (int) byte.MaxValue;
      this.NPC.life = 0;
      this.NPC.dontTakeDamage = false;
      this.NPC.checkDead();
      ModNPC modNpc;
      if (FargoSoulsUtil.HostCheck && ModContent.TryFind<ModNPC>("Fargowiltas", "Mutant", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
      {
        int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, modNpc.Type, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
        if (index != Main.maxNPCs)
        {
          Main.npc[index].homeless = true;
          if (this.TownNPCName != null)
            Main.npc[index].GivenName = this.TownNPCName;
          if (Main.netMode == 2)
            NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
        }
      }
      this.EdgyBossText(this.GFBQuote(33));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.FargoSouls().MaxLifeReduction += 100;
        target.AddBuff(ModContent.BuffType<OceanicMaulBuff>(), 5400, true, false);
        target.AddBuff(ModContent.BuffType<MutantFangBuff>(), 180, true, false);
      }
      target.AddBuff(ModContent.BuffType<CurseoftheMoonBuff>(), 600, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, FargoSoulsUtil.AprilFools ? 259 : 229, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Main.dust[index2].noLight = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.AngryMutant)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.07f);
    }

    public virtual bool CheckDead()
    {
      if ((double) this.AttackChoice == -7.0)
        return true;
      this.NPC.life = 1;
      ((Entity) this.NPC).active = true;
      if (FargoSoulsUtil.HostCheck && (double) this.AttackChoice > -1.0)
      {
        this.AttackChoice = WorldSavingSystem.EternityMode ? ((double) this.AttackChoice >= 10.0 ? -1f : 10f) : -6f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
        this.NPC.localAI[2] = 0.0f;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
        FargoSoulsUtil.ClearAllProjectiles(2, ((Entity) this.NPC).whoAmI, (double) this.AttackChoice < 0.0);
        this.EdgyBossText(this.GFBQuote(34));
      }
      return false;
    }

    public virtual void OnKill()
    {
      base.OnKill();
      if (WorldSavingSystem.MasochistModeReal || !this.playerInvulTriggered && WorldSavingSystem.EternityMode)
        Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).Hitbox, ModContent.ItemType<PhantasmalEnergy>(), 1, false, 0, false, false);
      if (WorldSavingSystem.EternityMode)
      {
        if (((Entity) Main.LocalPlayer).active)
        {
          if (!Main.LocalPlayer.FargoSouls().Toggler.CanPlayMaso && Main.netMode != 2)
            Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.MasochistModeUnlocked"), new Color?(new Color(51, (int) byte.MaxValue, 191, 0)));
          Main.LocalPlayer.FargoSouls().Toggler.CanPlayMaso = true;
        }
        WorldSavingSystem.CanPlayMaso = true;
      }
      WorldSavingSystem.SkipMutantP1 = 0;
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedMutant, -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npcLoot);
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<MutantBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<MutantTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<MutantRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<SpawnSack>(), 4));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new EModeDropCondition());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<FargowiltasSouls.Content.Items.Accessories.Masomode.MutantEye>()), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 4.0)
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
        return;
      this.NPC.frame.Y = 0;
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_2 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_3 = vector2_1;
      Rectangle? nullable = new Rectangle?(frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_4 = vector2_2;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D, vector2_3, nullable, alpha, (float) rotation, vector2_4, (float) scale, spriteEffects2, 0.0f);
      Vector2 position = Vector2.op_Addition(Vector2.op_Subtraction(this.AuraCenter, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      if (this.ShouldDrawAura)
        FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss.DrawAura(spriteBatch, position, this.AuraScale);
      return false;
    }

    public static void DrawAura(SpriteBatch spriteBatch, Vector2 position, float auraScale)
    {
      Color color = FargoSoulsUtil.AprilFools ? Color.Red : Color.CadetBlue;
      ((Color) ref color).A = (byte) 0;
      spriteBatch.Draw(FargosTextureRegistry.SoftEdgeRing.Value, position, new Rectangle?(), Color.op_Multiply(color, 0.7f), 0.0f, Vector2.op_Multiply(Utils.Size(FargosTextureRegistry.SoftEdgeRing.Value), 0.5f), 9.2f * auraScale, (SpriteEffects) 0, 0.0f);
    }

    public static void ArenaAura(
      Vector2 center,
      float distance,
      bool reverse = false,
      int dustid = -1,
      Color color = default (Color),
      params int[] buffs)
    {
      Player localPlayer = Main.LocalPlayer;
      if (buffs.Length == 0 || buffs[0] < 0)
        return;
      float num = Utils.Distance(center, ((Entity) localPlayer).Center);
      if (!((Entity) localPlayer).active || localPlayer.dead || localPlayer.ghost || (reverse ? ((double) num <= (double) distance ? 0 : ((double) num < (double) Math.Max(3000f, distance * 2f) ? 1 : 0)) : ((double) num < (double) distance ? 1 : 0)) == 0)
        return;
      foreach (int buff in buffs)
        FargoSoulsUtil.AddDebuffFixedDuration(localPlayer, buff, 2);
    }
  }
}
