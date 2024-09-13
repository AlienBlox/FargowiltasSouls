// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Lifelight.LifeChallenger
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Common.Graphics.Particles;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.ChallengerItems;
using FargowiltasSouls.Core.Systems;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Lifelight
{
  [AutoloadBossHead]
  public class LifeChallenger : ModNPC, IPixelatedPrimitiveRenderer
  {
    private const int DefaultHeight = 200;
    private const int DefaultWidth = 200;
    private bool flyfast;
    private bool Flying = true;
    private bool Charging;
    private bool HitPlayer;
    private bool AttackF1;
    private int Attacking = -1;
    private int dustcounter;
    private bool shoot;
    public Vector2 LockVector1 = new Vector2(0.0f, 0.0f);
    private Vector2 LockVector2 = new Vector2(0.0f, 0.0f);
    private Vector2 LockVector3 = new Vector2(0.0f, 0.0f);
    private Vector2 AuraCenter = new Vector2(0.0f, 0.0f);
    private double rotspeed;
    public double rot;
    private int firstblaster = 2;
    private bool UseTrueOriginAI;
    private bool useDR;
    private bool phaseProtectionDR;
    private bool DoAura;
    private int flyTimer = 9000;
    public bool PhaseOne = true;
    private List<Vector4> chunklist = new List<Vector4>();
    public const float DefaultRuneDistance = 100f;
    public float RuneDistance = 100f;
    private bool DrawRunes = true;
    public const float DefaultChunkDistance = 65f;
    public float ChunkDistance = 1000f;
    public int RuneFormation;
    public int InternalRuneFormation;
    public int RuneFormationTimer;
    public const int FormationTime = 60;
    public float GunRotation;
    public int PyramidPhase;
    public int PyramidTimer;
    public const int PyramidAnimationTime = 60;
    private float BodyRotation;
    public float RPS = 0.1f;
    private bool Draw;
    private readonly List<int> availablestates = new List<int>();
    public int state;
    private int oldstate = 999;
    private int statecount = 10;
    public bool Variant;
    private int P1state = -2;
    private int oldP1state;
    private readonly int P1statecount = 6;
    public const int ChunkCount = 50;
    public const int RuneCount = 12;
    private const int ChunkSpriteCount = 12;
    private const string PartsPath = "FargowiltasSouls/Assets/ExtraTextures/LifelightParts/";
    private List<LifeChallenger.Rune> PostdrawRunes = new List<LifeChallenger.Rune>();

    private int P2Threshold => !Main.expertMode ? 0 : (int) ((double) this.NPC.lifeMax * 0.75);

    private int SansThreshold
    {
      get
      {
        return !WorldSavingSystem.MasochistModeReal || !this.UseTrueOriginAI ? 0 : this.NPC.lifeMax / 10;
      }
    }

    public Vector2 GunCircleCenter(float lerp)
    {
      return Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.GunRotation), 100f), this.CloserGun ? 0.15f : 0.9f), lerp));
    }

    public bool CloserGun
    {
      get
      {
        if (!this.PhaseOne)
          return (double) this.state == 8.0;
        return (double) this.NPC.ai[0] == 0.0 ? (double) this.oldP1state == 0.0 : (double) this.P1state == 0.0;
      }
    }

    public float FormationLerp
    {
      get
      {
        float num = Math.Clamp((float) this.RuneFormationTimer / 60f, 0.0f, 1f);
        return this.RuneFormation != 0 ? num : 1f - num;
      }
    }

    public ref float AI_Timer => ref this.NPC.ai[1];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 8;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 40;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Assets/Effects/LifeStar";
      bestiaryDrawModifiers1.Position = new Vector2(0.0f, 0.0f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(0.0f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      NPCID.Sets.NPCBestiaryDrawOffset.Add(this.NPC.type, bestiaryDrawModifiers2);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 5);
      Span<int> span = CollectionsMarshal.AsSpan<int>(debuffs);
      int num1 = 0;
      span[num1] = 31;
      int num2 = num1 + 1;
      span[num2] = 46;
      int num3 = num2 + 1;
      span[num3] = 68;
      int num4 = num3 + 1;
      span[num4] = ModContent.BuffType<LethargicBuff>();
      int num5 = num4 + 1;
      span[num5] = ModContent.BuffType<ClippedWingsBuff>();
      int num6 = num5 + 1;
      npc.AddDebuffImmunities(debuffs);
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

    public virtual void SetDefaults()
    {
      this.NPC.aiStyle = -1;
      this.NPC.lifeMax = 36500;
      this.NPC.defense = 0;
      this.NPC.damage = 70;
      this.NPC.knockBackResist = 0.0f;
      ((Entity) this.NPC).width = 200;
      ((Entity) this.NPC).height = 200;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath7);
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/LieflightNoCum") : 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.value = (float) Item.buyPrice(0, 15, 0, 0);
      this.NPC.dontTakeDamage = true;
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      if (!WorldSavingSystem.MasochistModeReal)
        return;
      this.UseTrueOriginAI = true;
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write7BitEncodedInt(this.state);
      writer.Write7BitEncodedInt(this.oldstate);
      writer.Write7BitEncodedInt(this.P1state);
      writer.Write7BitEncodedInt(this.oldP1state);
      writer.Write(this.rotspeed);
      writer.Write(this.UseTrueOriginAI);
      writer.Write(this.AttackF1);
      Utils.WriteVector2(writer, this.LockVector1);
      Utils.WriteVector2(writer, this.LockVector2);
      writer.Write7BitEncodedInt(this.PyramidPhase);
      writer.Write7BitEncodedInt(this.PyramidTimer);
      writer.Write7BitEncodedInt(this.RuneFormation);
      writer.Write7BitEncodedInt(this.RuneFormationTimer);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.state = reader.Read7BitEncodedInt();
      this.oldstate = reader.Read7BitEncodedInt();
      this.P1state = reader.Read7BitEncodedInt();
      this.oldP1state = reader.Read7BitEncodedInt();
      this.rotspeed = reader.ReadDouble();
      this.UseTrueOriginAI = reader.ReadBoolean();
      this.AttackF1 = reader.ReadBoolean();
      this.LockVector1 = Utils.ReadVector2(reader);
      this.LockVector2 = Utils.ReadVector2(reader);
      this.PyramidPhase = reader.Read7BitEncodedInt();
      this.PyramidTimer = reader.Read7BitEncodedInt();
      this.RuneFormation = reader.Read7BitEncodedInt();
      this.RuneFormationTimer = reader.Read7BitEncodedInt();
    }

    public virtual void AI()
    {
      Player player1 = Main.player[this.NPC.target];
      Main.time = 27000.0;
      Main.dayTime = true;
      this.NPC.defense = this.NPC.defDefense;
      this.NPC.chaseable = true;
      this.phaseProtectionDR = false;
      this.NPC.dontTakeDamage = false;
      this.Attacking = 1;
      if (this.RuneFormationTimer <= 60)
        ++this.RuneFormationTimer;
      this.useDR = false;
      if (this.PhaseOne && this.NPC.life < this.P2Threshold)
        this.phaseProtectionDR = true;
      if (this.UseTrueOriginAI && this.NPC.life < this.SansThreshold)
        this.phaseProtectionDR = true;
      if (this.UseTrueOriginAI && (double) this.NPC.life < (double) this.SansThreshold * 0.5)
      {
        int num = this.NPC.lifeMax / 10;
        this.NPC.life += num / 60;
        CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num, false, false);
      }
      if (this.PyramidPhase == 1)
      {
        if (this.PyramidTimer == 60)
        {
          SoundEngine.PlaySound(ref SoundID.Item53, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.NPC.HitSound = new SoundStyle?(SoundID.Item52);
          this.NPC.defense = this.NPC.defDefense + 100;
          this.NPC.netUpdate = true;
        }
        this.useDR = true;
        this.ChunkDistance = (float) (65.0 * (1.0 - (double) Math.Min((float) this.PyramidTimer / 60f, 1f)));
      }
      else if (this.PyramidPhase == -1)
      {
        if (this.PyramidTimer == 5)
        {
          SoundStyle shatter = SoundID.Shatter;
          ((SoundStyle) ref shatter).Pitch = -0.5f;
          SoundEngine.PlaySound(ref shatter, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
          this.NPC.defense = this.NPC.defDefense;
          this.NPC.netUpdate = true;
        }
        this.ChunkDistance = 65f * Math.Min((float) this.PyramidTimer / 60f, 1f);
        if ((double) this.ChunkDistance == 65.0)
        {
          this.PyramidPhase = 0;
          this.NPC.netUpdate = true;
        }
      }
      ++this.PyramidTimer;
      this.BodyRotation += (float) ((double) this.RPS * 6.2831854820251465 / 60.0);
      if (this.InternalRuneFormation == 2)
        this.BodyRotation += (float) ((double) this.FormationLerp * 0.25 * 6.2831854820251465 / 60.0);
      if (this.P1state != -2)
      {
        if (this.DoAura)
        {
          if (this.dustcounter > 5 && (this.DoAura && this.state == 1 || this.P1state == 4))
          {
            for (int index1 = 0; index1 < 180; ++index1)
            {
              double num1 = 2.0 * (double) index1 * 0.017453293005625408;
              double num2 = 1200.0;
              int index2 = Dust.NewDust(new Vector2((float) ((int) this.AuraCenter.X - (int) (Math.Cos(num1) * num2)), (float) ((int) this.AuraCenter.Y - (int) (Math.Sin(num1) * num2))), 1, 1, (int) Utils.NextFromList<short>(Main.rand, new short[3]
              {
                (short) 64,
                (short) 242,
                (short) 156
              }), 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index2].noGravity = true;
            }
            this.dustcounter = 0;
          }
          ++this.dustcounter;
          float num3 = Utils.Distance(this.AuraCenter, ((Entity) Main.LocalPlayer).Center);
          float num4 = 1200f;
          Player localPlayer = Main.LocalPlayer;
          if (((Entity) localPlayer).active && !localPlayer.dead && !localPlayer.ghost && (double) num3 > (double) num4 && (double) num3 < (double) num4 * 4.0)
          {
            if ((double) num3 > (double) num4 * 2.0)
            {
              localPlayer.controlLeft = false;
              localPlayer.controlRight = false;
              localPlayer.controlUp = false;
              localPlayer.controlDown = false;
              localPlayer.controlUseItem = false;
              localPlayer.controlUseTile = false;
              localPlayer.controlJump = false;
              localPlayer.controlHook = false;
              if (localPlayer.grapCount > 0)
                localPlayer.RemoveAllGrapplingHooks();
              if (localPlayer.mount.Active)
                localPlayer.mount.Dismount(localPlayer);
              ((Entity) localPlayer).velocity.X = 0.0f;
              ((Entity) localPlayer).velocity.Y = -0.4f;
              localPlayer.FargoSouls().NoUsingItems = 2;
            }
            Vector2 vector2_1 = Vector2.op_Subtraction(this.AuraCenter, ((Entity) localPlayer).Center);
            float num5 = ((Vector2) ref vector2_1).Length() - num4;
            ((Vector2) ref vector2_1).Normalize();
            Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, (double) num5 < 17.0 ? num5 : 17f);
            Player player2 = localPlayer;
            ((Entity) player2).position = Vector2.op_Addition(((Entity) player2).position, vector2_2);
            for (int index3 = 0; index3 < 10; ++index3)
            {
              int num6 = (int) Utils.NextFromList<short>(Main.rand, new short[3]
              {
                (short) 64,
                (short) 242,
                (short) 156
              });
              int index4 = Dust.NewDust(((Entity) localPlayer).position, ((Entity) localPlayer).width, ((Entity) localPlayer).height, num6, 0.0f, 0.0f, 0, new Color(), 1.25f);
              Main.dust[index4].noGravity = true;
              Dust dust = Main.dust[index4];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
            }
          }
        }
        this.AuraCenter = ((Entity) this.NPC).Center;
        if (!((Entity) player1).active || player1.dead || player1.ghost || (double) ((Entity) this.NPC).Distance(((Entity) player1).Center) > 2400.0)
        {
          this.NPC.TargetClosest(false);
          Player player3 = Main.player[this.NPC.target];
          if (!((Entity) player3).active || player3.dead || player3.ghost || (double) ((Entity) this.NPC).Distance(((Entity) player3).Center) > 2400.0)
          {
            if (this.NPC.timeLeft > 60)
              this.NPC.timeLeft = 60;
            ((Entity) this.NPC).velocity.Y -= 0.4f;
            return;
          }
        }
        this.NPC.timeLeft = 60;
      }
      this.DoAura = WorldSavingSystem.MasochistModeReal;
      if (this.PhaseOne)
      {
        this.P1AI();
      }
      else
      {
        if (this.Flying)
          this.FlyingState();
        if (this.Charging)
        {
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity) + 1.57079637f;
          if (Vector2.op_Equality(((Entity) this.NPC).velocity, Vector2.Zero))
            this.NPC.rotation = 0.0f;
        }
        if (!this.Charging && !this.Flying)
          this.NPC.rotation = MathHelper.Lerp(this.NPC.rotation, 0.0f, 0.09f);
        if (this.Attacking == 1)
        {
          if (this.state == this.oldstate)
          {
            this.RandomizeState();
            bool flag = true;
            if (!this.PhaseOne && this.NPC.life < this.SansThreshold)
            {
              this.state = 101;
              this.oldstate = 0;
              flag = false;
            }
            if (flag)
              this.flyTimer = 0;
          }
          if (this.FlightCheck())
            --this.AI_Timer;
          else if (this.state != this.oldstate)
          {
            switch ((LifeChallenger.P2States) this.state)
            {
              case LifeChallenger.P2States.SlurpBurp:
                this.AttackSlurpBurp();
                break;
              case LifeChallenger.P2States.RuneExpand:
                this.AttackRuneExpand();
                break;
              case LifeChallenger.P2States.Charge:
                this.AttackCharge();
                break;
              case LifeChallenger.P2States.Plunge:
                this.AttackPlunge();
                break;
              case LifeChallenger.P2States.Pixies:
                this.AttackPixies();
                break;
              case LifeChallenger.P2States.Roulette:
                this.AttackRoulette();
                break;
              case LifeChallenger.P2States.ReactionShotgun:
                this.AttackReactionShotgun();
                break;
              case LifeChallenger.P2States.RunningMinigun:
                this.AttackRunningMinigun();
                break;
              case LifeChallenger.P2States.Shotgun:
                this.AttackShotgun();
                break;
              case LifeChallenger.P2States.TeleportNukes:
                this.AttackTeleportNukes();
                break;
              case LifeChallenger.P2States.Final:
                this.AttackFinal();
                break;
              default:
                this.StateReset();
                break;
            }
          }
        }
        ++this.AI_Timer;
      }
    }

    public void P1AI()
    {
      ref float local1 = ref this.NPC.ai[0];
      ref float local2 = ref this.NPC.ai[2];
      if ((double) local1 == 0.0)
      {
        if ((double) this.AI_Timer == 30.0 || this.P1state == -2)
          this.NPC.TargetClosest(true);
        if ((double) this.AI_Timer >= 60.0 || this.P1state == -2)
        {
          this.AI_Timer = 0.0f;
          local1 = 1f;
        }
      }
      if ((double) local1 == 1.0)
      {
        if (this.P1state == this.oldP1state && this.P1state != -2)
        {
          this.flyTimer = 0;
          this.RandomizeP1state();
        }
        if (this.FlightCheck())
        {
          --this.AI_Timer;
          --local2;
        }
        else if (this.P1state != this.oldP1state)
        {
          switch (this.P1state)
          {
            case -2:
              this.Opening();
              break;
            case -1:
              this.P1Transition();
              break;
            case 0:
              this.P1ShotSpam();
              break;
            case 1:
              this.P1Nuke();
              break;
            case 2:
              this.P1Mines();
              break;
            case 3:
              this.P1Pixies();
              break;
            case 4:
              this.AttackRuneExpand();
              break;
            case 5:
              this.AttackReactionShotgun();
              break;
            default:
              this.RandomizeP1state();
              this.flyTimer = 9000;
              break;
          }
          if (!this.Flying)
            this.NPC.rotation = MathHelper.Lerp(this.NPC.rotation, 0.0f, 0.09f);
        }
      }
      this.P1PeriodicNuke();
      ++this.AI_Timer;
      ++local2;
    }

    public void Opening()
    {
      ref float local = ref this.NPC.ai[2];
      if (!this.NPC.HasValidTarget)
        this.NPC.TargetClosest(false);
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).position.X = ((Entity) player).Center.X - (float) (((Entity) this.NPC).width / 2);
      ((Entity) this.NPC).position.Y = ((Entity) player).Center.Y - 490f - (float) (((Entity) this.NPC).height / 2);
      this.NPC.alpha = (int) ((double) byte.MaxValue - (double) local * 17.0);
      this.RPS = 0.1f;
      if ((double) this.AI_Timer == 180.0)
      {
        if (this.UseTrueOriginAI)
        {
          string textValue = Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".NPCs.LifeChallenger.FatherOfLies");
          Color goldenrod = Color.Goldenrod;
          FargoSoulsUtil.PrintText(textValue, goldenrod);
          CombatText.NewText(((Entity) player).Hitbox, goldenrod, textValue, true, false);
        }
        if (!Main.dedServ)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.166666672f);
        if (WorldSavingSystem.EternityMode && !WorldSavingSystem.DownedBoss[10] && FargoSoulsUtil.HostCheck)
          Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) Main.player[this.NPC.target]).Hitbox, ModContent.ItemType<FragilePixieLamp>(), 1, false, 0, false, false);
        SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = 0; index < 150; ++index)
        {
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedByRandom(new Vector2(1f, 0.0f), 6.2831854820251465), (float) Main.rand.Next(20));
          Dust.NewDust(((Entity) this.NPC).Center, 0, 0, (int) Utils.NextFromList<short>(Main.rand, new short[3]
          {
            (short) 64,
            (short) 242,
            (short) 156
          }), vector2.X, vector2.Y, 100, new Color(), 1f);
        }
        this.Draw = true;
        this.NPC.dontTakeDamage = false;
      }
      if (this.chunklist.Count < 50)
      {
        double num1 = 3.1415927410125732 * (Math.Sqrt(5.0) - 1.0);
        int count = this.chunklist.Count;
        float num2 = (float) (1.0 - 2.0 * ((double) count / 49.0));
        double num3 = (double) count;
        double num4 = num1 * num3;
        float num5 = (float) Math.Sqrt(1.0 - (double) num2 * (double) num2);
        float num6 = (float) Math.Cos(num4) * num5;
        float num7 = (float) Math.Sin(num4) * num5;
        this.chunklist.Add(new Vector4(num6, num2, num7, (float) (Main.rand.Next(12) + 1)));
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Addition(Vector2.op_Multiply(num6, Vector2.UnitX), Vector2.op_Multiply(num2, Vector2.UnitY)), this.ChunkDistance));
        SoundEngine.PlaySound(ref SoundID.Tink, new Vector2?(vector2), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = Dust.NewDust(vector2, 5, 5, 10, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index2].velocity = Vector2.op_Division(Vector2.op_Subtraction(Main.dust[index2].position, vector2), 10f);
        }
      }
      if ((double) this.AI_Timer < 180.0)
      {
        this.ChunkDistance = (float) (1000.0 - 935.0 * ((double) this.AI_Timer / 180.0));
        this.NPC.dontTakeDamage = true;
      }
      if ((double) this.AI_Timer < 240.0 || this.chunklist.Count < 50)
        return;
      this.ChunkDistance = 65f;
      this.P1state = -3;
      this.oldP1state = this.P1state;
      this.P1stateReset();
    }

    public void P1ShotSpam()
    {
      ref float local1 = ref this.NPC.localAI[1];
      ref float local2 = ref this.NPC.ai[2];
      if (WorldSavingSystem.MasochistModeReal)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      else
        this.FlyingState(0.15f);
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        local1 = 0.0f;
        this.RuneFormation = 2;
        this.RuneFormationTimer = 0;
      }
      this.GunRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
      if ((double) local2 > 60.0 && (double) this.AI_Timer < 280.0)
      {
        int num1 = WorldSavingSystem.MasochistModeReal ? 5 : 6;
        ref float local3 = ref local1;
        float num2 = local1 + 1f;
        double num3 = (double) num2;
        local3 = (float) num3;
        if ((double) num2 % (double) num1 == 0.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          float num4 = (float) (3.1415927410125732 / (WorldSavingSystem.MasochistModeReal ? 8.0 : 5.0));
          float num5 = (float) ((3.1415927410125732 - (double) num4) * 0.89999997615814209);
          float num6 = Math.Min(1f, local1 / 60f);
          float num7 = num4 + num5 * (float) Math.Cos(1.5707963705062866 * (double) num6);
          Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) (3.0 + 12.0 * (double) num6));
          int num8 = (double) local1 > 60.0 ? ModContent.ProjectileType<LifeSplittingProjSmall>() : ModContent.ProjectileType<LifeProjSmall>();
          for (int index = -1; index <= 1; ++index)
          {
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.GunCircleCenter(0.0f), Utils.RotatedBy(vector2, (double) num7 * (double) index, new Vector2()), num8, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      if ((double) this.AI_Timer < 280.0)
        return;
      if (this.RuneFormation != 0)
      {
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }
      else
      {
        if (this.RuneFormationTimer < 60)
          return;
        this.oldP1state = this.P1state;
        this.P1stateReset();
      }
    }

    public void P1Nuke()
    {
      double num1 = (double) this.NPC.ai[2];
      if (WorldSavingSystem.MasochistModeReal)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      else
        this.FlyingState(0.5f);
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer == 70.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item91, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num2 = 12f;
          Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num2);
          float num3 = (float) (WorldSavingSystem.EternityMode ? 1 : 0);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<LifeNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 32f, num3, 0.0f);
        }
      }
      if ((double) this.AI_Timer < 120.0)
        return;
      this.oldP1state = this.P1state;
      this.P1stateReset();
    }

    public void P1Pixies()
    {
      ref float local1 = ref this.NPC.ai[2];
      ref float local2 = ref this.NPC.localAI[1];
      if (WorldSavingSystem.MasochistModeReal)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      else
        this.FlyingState(0.2f);
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        local2 = 0.0f;
      }
      if ((double) local1 > 60.0 && (double) this.NPC.ai[2] % 5.0 == 0.0 && (double) this.AI_Timer < 280.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item25, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num1 = 3f;
          Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).position), (double) local2, new Vector2()), -4f);
          local2 = (float) Main.rand.Next(-20, 20) * ((float) Math.PI / 180f);
          float num2 = 0.0f;
          if (!WorldSavingSystem.MasochistModeReal)
            num2 = -10f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<LifeHomingProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num1, Main.myPlayer, num2, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
      }
      ++local1;
      if ((double) this.AI_Timer < (WorldSavingSystem.MasochistModeReal ? 160.0 : 200.0))
        return;
      this.oldP1state = this.P1state;
      this.P1stateReset();
    }

    public void P1Mines()
    {
      double num1 = (double) this.NPC.ai[2];
      if (WorldSavingSystem.MasochistModeReal)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      else
        this.FlyingState(0.5f);
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        if (FargoSoulsUtil.HostCheck)
        {
          foreach (Projectile projectile in Main.projectile)
          {
            if (projectile.type == ModContent.ProjectileType<LifeBombExplosion>())
            {
              projectile.ai[0] = Math.Max(projectile.ai[0], 2370f);
              projectile.netUpdate = true;
            }
          }
        }
        this.RuneFormation = 2;
        this.RuneFormationTimer = 0;
      }
      this.GunRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
      if ((double) this.AI_Timer > 0.0 && (double) this.AI_Timer % 70.0 == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        int num2 = 14;
        for (int index = 0; index < num2; ++index)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            float num3 = Utils.NextFloat(Main.rand, 25f, 172f);
            int num4 = 40;
            double radians = (double) MathHelper.ToRadians(Utils.NextFloat(Main.rand, (float) -num4, (float) num4));
            Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num3 / 6f), radians, new Vector2());
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.GunCircleCenter(0.8f), vector2, ModContent.ProjectileType<LifeBomb>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer < 210.0)
        return;
      this.RuneFormationTimer = 0;
      this.RuneFormation = 0;
      this.oldP1state = this.P1state;
      this.P1stateReset();
    }

    public void P1PeriodicNuke()
    {
      ref float local = ref this.NPC.ai[3];
      Player player = Main.player[this.NPC.target];
      if ((double) local > 600.0 && WorldSavingSystem.EternityMode)
      {
        SoundEngine.PlaySound(ref SoundID.Item91, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num1 = 8f;
          float num2 = 300f;
          Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num1);
          float num3 = (float) (WorldSavingSystem.EternityMode ? 1 : 0);
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Subtraction(Vector2.Zero, vector2), ModContent.ProjectileType<LifeNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num2, Main.myPlayer, 32f, num3, 0.0f);
          local = 0.0f;
        }
        this.NPC.netUpdate = true;
      }
      ++local;
    }

    public void P1Transition()
    {
      double num1 = (double) this.NPC.ai[2];
      double num2 = (double) this.NPC.localAI[1];
      Player player = Main.player[this.NPC.target];
      this.Charging = false;
      this.Flying = false;
      this.useDR = true;
      this.DoAura = true;
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      this.NPC.ai[3] = 0.0f;
      PhaseTransition();
      if ((double) this.AI_Timer <= 280.0)
        return;
      LaserSpin();

      void PhaseTransition()
      {
        if ((double) this.RPS < 0.20000000298023224)
          this.RPS += 1f / 1000f;
        else
          this.RPS = 0.2f;
        if ((double) this.AI_Timer == 5.0)
        {
          SoundStyle soundStyle = SoundID.Item29;
          ((SoundStyle) ref soundStyle).Volume = 1.5f;
          ((SoundStyle) ref soundStyle).Pitch = -0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        if ((double) this.AI_Timer < 60.0)
        {
          Color drawColor = Utils.NextFromList<Color>(Main.rand, new Color[3]
          {
            Color.Goldenrod,
            Color.Pink,
            Color.Cyan
          });
          Particle particle1 = (Particle) new SmallSparkle(((Entity) this.NPC).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 5f, 50f), Vector2.UnitX), 6.2831854820251465), drawColor, 1f, Main.rand.Next(20, 80), rotationSpeed: Utils.NextFloat(Main.rand, -0.3926991f, 0.3926991f));
          particle1.Spawn();
          Particle particle2 = particle1;
          particle2.Position = Vector2.op_Subtraction(particle2.Position, Vector2.op_Multiply(particle1.Velocity, 4f));
        }
        if ((double) this.AI_Timer == 60.0)
        {
          SoundStyle soundStyle = SoundID.Item82;
          ((SoundStyle) ref soundStyle).Pitch = -0.2f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) Main.LocalPlayer).Center), (SoundUpdateCallback) null);
          this.LockVector1 = Vector2.op_UnaryNegation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center));
          if (this.PyramidPhase == 0)
            this.PyramidTimer = 0;
          this.PyramidPhase = 1;
          this.NPC.netUpdate = true;
          for (int index = 0; index < 100; ++index)
          {
            Color drawColor = Utils.NextFromList<Color>(Main.rand, new Color[3]
            {
              Color.Goldenrod,
              Color.Pink,
              Color.Cyan
            });
            new SmallSparkle(((Entity) this.NPC).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 5f, 50f), Vector2.UnitX), 6.2831854820251465), drawColor, 1f, Main.rand.Next(20, 80), rotationSpeed: Utils.NextFloat(Main.rand, -0.3926991f, 0.3926991f)).Spawn();
          }
        }
        int mineAmount = WorldSavingSystem.EternityMode ? 100 : 60;
        if ((double) this.AI_Timer <= 180.0 && (double) this.AI_Timer > (double) (180 - mineAmount))
        {
          int bombwidth = 22;
          if (FargoSoulsUtil.HostCheck)
          {
            int bombType = ModContent.ProjectileType<LifeTransitionBomb>();
            int i = (int) ((double) this.AI_Timer - (double) (180 - mineAmount));
            Vector2 pos = FindPos();
            for (int index = 0; index < 30; ++index)
            {
              pos = FindPos();
              if (!((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == bombType && (double) Utils.Distance(Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, p.ai[1]), Vector2.op_Multiply(Vector2.UnitY, p.ai[2])), pos) < (double) bombwidth * 1.2000000476837158)))
                break;
            }
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, bombType, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, pos.X, pos.Y);

            Vector2 FindPos()
            {
              float num1 = (float) ((double) i / (double) mineAmount * 6.2831854820251465);
              float num2 = (float) Math.Pow((double) Utils.NextFloat(Main.rand, 1f), Math.Sin(6.2831854820251465 * (double) i / 8.0) * 0.30000001192092896 + 0.89999997615814209);
              float num3 = MathHelper.Lerp((float) ((Entity) this.NPC).width / 3f, 1200f, num2);
              return Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num1), num3));
            }
          }
        }
        if ((double) this.AI_Timer == 120.0)
        {
          this.RuneFormation = 2;
          this.RuneFormationTimer = 0;
        }
        if ((double) this.AI_Timer == 180.0)
        {
          SoundStyle soundStyle = SoundID.Item92;
          ((SoundStyle) ref soundStyle).Pitch = -0.5f;
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (!Main.dedServ)
            ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.166666672f);
          if (FargoSoulsUtil.HostCheck)
          {
            foreach (Projectile projectile in Main.projectile)
            {
              if (projectile.type == ModContent.ProjectileType<LifeBombExplosion>())
              {
                projectile.ai[0] = Math.Max(projectile.ai[0], (float) (LifeBombExplosion.MaxTime - 30));
                projectile.netUpdate = true;
              }
            }
          }
        }
        this.GunRotation = Utils.ToRotation(this.LockVector1);
        if ((double) this.AI_Timer != 240.0 || !FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, -1f, Utils.ToRotation(this.LockVector1), 0.0f);
      }

      void LaserSpin()
      {
        double num1 = (double) this.NPC.ai[0];
        ref float local1 = ref this.NPC.localAI[2];
        ref float local2 = ref this.NPC.localAI[0];
        Player player = Main.player[this.NPC.target];
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
        this.NPC.dontTakeDamage = true;
        this.HitPlayer = true;
        int num2 = 950;
        if (Main.getGoodWorld)
          num2 += 4000;
        if (!WorldSavingSystem.EternityMode)
          num2 = 500;
        if (this.AttackF1)
        {
          this.AttackF1 = false;
          SoundStyle zombie104 = SoundID.Zombie104;
          ((SoundStyle) ref zombie104).Volume = 0.5f;
          SoundEngine.PlaySound(ref zombie104, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, this.LockVector1, ModContent.ProjectileType<LifeChalDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.2f), 3f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, (float) num2);
          ((Entity) this.NPC).velocity.X = 0.0f;
          ((Entity) this.NPC).velocity.Y = 0.0f;
          this.Flying = false;
          double rotation = (double) Utils.ToRotation(Utils.RotatedBy(this.LockVector1, this.rot, new Vector2()));
          Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
          Vector2 rotationVector2 = Utils.ToRotationVector2((float) rotation);
          float num3 = (float) Math.Atan2((double) vector2.Y * (double) rotationVector2.X - (double) vector2.X * (double) rotationVector2.Y, (double) rotationVector2.X * (double) vector2.X + (double) rotationVector2.Y * (double) vector2.Y);
          local2 = (float) Math.Sign(num3);
          this.NPC.netUpdate = true;
          this.rotspeed = 0.0;
          this.rot = 0.0;
        }
        this.GunRotation = Utils.ToRotation(Utils.RotatedBy(this.LockVector1, this.rot, new Vector2()));
        if ((double) local2 == 0.0)
        {
          local2 = 1f;
          this.NPC.netUpdate = true;
        }
        if ((double) local1 >= 10.0 && (double) local1 < (double) num2)
        {
          if (this.rotspeed < 0.81999999284744263)
            this.rotspeed += 0.0050000003539025784;
          else
            this.rotspeed = 0.81999999284744263;
          this.rot += (double) local2 * 3.1415927410125732 / 180.0 * this.rotspeed;
        }
        ++local1;
        if ((double) local1 == (double) num2)
          EndTransition();
        if ((double) local1 <= (double) num2 || this.PyramidPhase != 0 || this.RuneFormationTimer < 60)
          return;
        GoPhase2();
      }

      void EndTransition()
      {
        foreach (Projectile projectile in Main.projectile)
        {
          if (projectile.type == ModContent.ProjectileType<LifeBombExplosion>())
          {
            projectile.ai[0] = Math.Max(projectile.ai[0], (float) (LifeBombExplosion.MaxTime - 30));
            projectile.netUpdate = true;
          }
          if (projectile.type == ModContent.ProjectileType<LifeChalDeathray>())
            projectile.Kill();
        }
        this.PyramidPhase = -1;
        this.PyramidTimer = 0;
        this.NPC.netUpdate = true;
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }

      void GoPhase2()
      {
        this.P1state = 0;
        this.PhaseOne = false;
        this.HitPlayer = false;
        this.NPC.netUpdate = true;
        this.NPC.TargetClosest(true);
        this.AttackF1 = true;
        this.AI_Timer = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.ai[0] = 0.0f;
        this.StateReset();
      }
    }

    public void FlyingState(float speedModifier = 1f)
    {
      ref float local = ref this.NPC.localAI[3];
      this.Flying = true;
      float num1 = 0.0166666675f;
      if ((double) local < (double) speedModifier)
      {
        local += num1;
        if ((double) local > (double) speedModifier)
          local = speedModifier;
      }
      if ((double) local > (double) speedModifier)
      {
        local -= num1;
        if ((double) local < (double) speedModifier)
          local = speedModifier;
      }
      speedModifier = local;
      Player player = Main.player[this.NPC.target];
      float num2 = 0.0f;
      float num3 = 10f;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(((Entity) player).Center.X, ((Entity) player).Center.Y - 300f);
      if (this.state == 8)
        vector2_1.Y = ((Entity) player).Center.Y - 700f;
      if (((double) Math.Abs(vector2_1.Y - ((Entity) this.NPC).Center.Y) >= 32.0 ? 0 : ((double) Math.Abs(vector2_1.X - ((Entity) this.NPC).Center.X) < 160.0 ? 1 : 0)) == 0 && (double) ((Entity) this.NPC).Distance(vector2_1) < 500.0)
      {
        num2 = 9f;
        if (!this.flyfast)
        {
          Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), num2);
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num3 - 1f), vector2_2), num3);
        }
      }
      if (Vector2.op_Equality(((Entity) this.NPC).velocity, Vector2.Zero))
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), 1f);
      if ((double) ((Entity) this.NPC).Distance(vector2_1) > 360.0)
      {
        num2 = ((Entity) this.NPC).Distance(vector2_1) / 35f;
        this.flyfast = true;
        Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), num2);
        ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num3 - 1f), vector2_3), num3);
      }
      if (this.flyfast && ((double) ((Entity) this.NPC).Distance(vector2_1) < 100.0 || (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 100.0))
      {
        this.flyfast = false;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), num2);
      }
      if ((double) Utils.ToRotation(((Entity) this.NPC).velocity) > 3.1415927410125732)
        this.NPC.rotation = (float) (0.0 - 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X * (double) speedModifier / 100.0);
      else
        this.NPC.rotation = (float) (0.0 + 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X * (double) speedModifier / 100.0);
      NPC npc = this.NPC;
      ((Entity) npc).position = Vector2.op_Subtraction(((Entity) npc).position, Vector2.op_Multiply(((Entity) this.NPC).velocity, 1f - speedModifier));
    }

    public void AttackFinal()
    {
      ref float local = ref this.NPC.ai[0];
      Player player = Main.player[this.NPC.target];
      if (this.UseTrueOriginAI)
      {
        this.NPC.chaseable = false;
        if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && Main.LocalPlayer.grapCount > 0)
          Main.LocalPlayer.RemoveAllGrapplingHooks();
      }
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        this.Flying = true;
      }
      if ((double) this.AI_Timer == 0.0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<LifeCageTelegraph>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) player).whoAmI, 0.0f);
      if ((double) this.AI_Timer == 120.0)
      {
        SoundEngine.PlaySound(ref SoundID.DD2_DefenseTowerSpawn, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 26; ++index1)
        {
          for (int index2 = 0; index2 < 2; ++index2)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(((Entity) player).Center.X - 300f + (float) (600 * index2), ((Entity) player).Center.Y - 300f + (float) (24 * index1)), Vector2.Zero, ModContent.ProjectileType<LifeCageProjectile>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, (float) index2, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(((Entity) player).Center.X - 300f + (float) (24 * index1), ((Entity) player).Center.Y - 300f + (float) (600 * index2)), Vector2.Zero, ModContent.ProjectileType<LifeCageProjectile>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, (float) (2 + index2), 0.0f, 0.0f);
            }
          }
        }
        this.LockVector1 = ((Entity) player).Center;
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer > 120.0 && ((Entity) Main.LocalPlayer).active && ((double) Math.Abs(((Entity) Main.LocalPlayer).Center.X - this.LockVector1.X) > 320.0 || (double) Math.Abs(((Entity) Main.LocalPlayer).Center.Y - this.LockVector1.Y) > 320.0) && ((Entity) Main.LocalPlayer).active && ((double) Math.Abs(((Entity) Main.LocalPlayer).Center.X - this.LockVector1.X) < 1500.0 || (double) Math.Abs(((Entity) Main.LocalPlayer).Center.Y - this.LockVector1.Y) < 1500.0))
        ((Entity) Main.LocalPlayer).position = this.LockVector1;
      int num1 = (int) this.AI_Timer - 220;
      if ((double) this.AI_Timer > 220.0 && num1 % 75 + 1 == 1 && (double) this.AI_Timer < 700.0 && FargoSoulsUtil.HostCheck)
      {
        for (int index = 0; index < 2; ++index)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2((float) (8 * index - 4), -2f), ModContent.ProjectileType<LifeNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 24f, 0.0f, 0.0f);
      }
      if ((double) this.AI_Timer > 220.0 && num1 % 50 + 1 == 1 && (double) this.AI_Timer < 700.0)
      {
        SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(0.0f, 2.5f), ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      int num2 = (int) this.AI_Timer - 760;
      if ((double) this.AI_Timer >= 760.0 && num2 % 90 + 1 == 1 && (double) this.AI_Timer < 1480.0)
      {
        local = (float) Main.rand.Next(-90, 90);
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer >= 760.0 && num2 % 90 + 1 == 90 && (double) this.AI_Timer < 1480.0)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(0.0f, 450f);
        if (this.firstblaster < 1 || this.firstblaster > 1)
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = 0; index <= 12; ++index)
        {
          if (FargoSoulsUtil.HostCheck && (this.firstblaster < 1 || this.firstblaster > 1))
          {
            float rotation = Utils.ToRotation(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.Normalize(vector2), (double) index * 3.1415927410125732 / 6.0 + (double) MathHelper.ToRadians(this.NPC.ai[0]), new Vector2())));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(this.LockVector1, Utils.RotatedBy(vector2, (double) index * 3.1415927410125732 / 6.0 + (double) MathHelper.ToRadians(this.NPC.ai[0]), new Vector2())), Vector2.Zero, ModContent.ProjectileType<LifeBlaster>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, rotation, (float) this.firstblaster, 0.0f);
          }
        }
        if (this.firstblaster > 0)
          --this.firstblaster;
        this.NPC.netUpdate = true;
      }
      int num3 = (int) this.AI_Timer - 1570;
      if ((double) this.AI_Timer >= 1570.0 && num3 == 0)
      {
        local = 0.0f;
        this.NPC.netUpdate = true;
        this.LockVector2 = ((Entity) player).Center;
      }
      if ((double) this.AI_Timer > 1570.0 && num2 % 4 == 3 && (double) this.AI_Timer < 2470.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(this.LockVector2, this.LockVector1)), 550f), 1.5707963705062866, new Vector2());
        if (FargoSoulsUtil.HostCheck)
        {
          float rotation = Utils.ToRotation(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.Normalize(vector2), (double) local * 3.1415927410125732 / 18.0, new Vector2())));
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(this.LockVector1, Utils.RotatedBy(vector2, (double) local * 3.1415927410125732 / 18.0, new Vector2())), Vector2.Zero, ModContent.ProjectileType<LifeBlaster>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, rotation, 0.0f, 0.0f);
        }
        this.NPC.netUpdate = true;
        ++local;
      }
      int num4 = 2590;
      if ((double) this.AI_Timer >= (double) num4)
      {
        this.UseTrueOriginAI = false;
        this.NPC.dontTakeDamage = false;
        SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.AI_Timer >= (double) num4 && (double) this.AI_Timer % 10.0 == 0.0)
      {
        for (int index = 0; index < 50; ++index)
          Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, (int) Utils.NextFromList<short>(Main.rand, new short[3]
          {
            (short) 64,
            (short) 242,
            (short) 156
          }), 0.0f, 0.0f, 0, new Color(), 1f);
        ((Entity) this.NPC).position = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(-60, 60), (float) Main.rand.Next(-60, 60)));
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      if ((double) this.AI_Timer != (double) (num4 + 90))
        return;
      this.NPC.life = 0;
      this.NPC.checkDead();
    }

    public void AttackSlurpBurp()
    {
      ref float local1 = ref this.NPC.ai[2];
      ref float local2 = ref this.NPC.ai[3];
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
      }
      if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 2000.0)
        this.FlyingState(1.5f);
      ((Entity) this.NPC).velocity = Vector2.Zero;
      this.Flying = false;
      float num1 = 3f;
      double num2 = (double) this.AI_Timer * 5.721237 * 0.017453293005625408;
      double num3 = 1200.0;
      if (!WorldSavingSystem.MasochistModeReal)
      {
        float num4 = ((Entity) this.NPC).Distance(((Entity) player).Center) + 240f;
        num3 = Math.Min(Math.Max(num3, (double) num4), 2400.0);
      }
      int num5 = (int) ((Entity) this.NPC).Center.X - (int) (Math.Cos(num2) * num3);
      int num6 = (int) ((Entity) this.NPC).Center.Y - (int) (Math.Sin(num2) * num3);
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector((float) num5, (float) num6);
      if ((double) local1 >= 2.0 && (double) this.AI_Timer <= 300.0)
      {
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, Vector2.Zero, ModContent.ProjectileType<LifeSlurp>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num1, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        local1 = 0.0f;
      }
      ++local1;
      if ((double) this.AI_Timer < 300.0)
      {
        if ((double) local2 > 15.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item101, new Vector2?(vector2_1), (SoundUpdateCallback) null);
          if (WorldSavingSystem.MasochistModeReal && this.shoot)
          {
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            float num7 = 10f;
            float num8 = 3f;
            Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num7);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<LifeWave>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num8, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            this.shoot = false;
          }
          else
            this.shoot = true;
          local2 = 0.0f;
        }
        ++local2;
      }
      int num9 = WorldSavingSystem.EternityMode ? 660 : 360;
      if ((double) this.AI_Timer > 300.0 && (double) this.AI_Timer < (double) (num9 - 60))
      {
        if ((double) local2 > 15.0)
        {
          SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          local2 = 0.0f;
        }
        ++local2;
      }
      if (!WorldSavingSystem.MasochistModeReal && (double) this.AI_Timer < 120.0)
      {
        local1 -= 0.5f;
        local2 -= 0.5f;
      }
      if ((double) this.AI_Timer < (double) num9)
        return;
      this.oldstate = this.state;
      this.Flying = true;
      this.StateReset();
    }

    public void AttackShotgun()
    {
      ref float local1 = ref this.NPC.ai[3];
      ref float local2 = ref this.NPC.ai[2];
      int num1 = WorldSavingSystem.MasochistModeReal ? 80 : (WorldSavingSystem.EternityMode ? 95 : 105);
      int num2 = WorldSavingSystem.MasochistModeReal ? 40 : (WorldSavingSystem.EternityMode ? 50 : 55);
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        this.Flying = true;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.RuneFormation = 2;
        this.RuneFormationTimer = 0;
      }
      this.GunRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
      this.Flying = false;
      float num3 = 7f;
      float num4 = ((Entity) this.NPC).Distance(((Entity) player).Center);
      if ((double) num4 < 800.0)
        num3 *= num4 / 800f;
      if ((double) num3 > 3.0)
      {
        float num5 = num3;
        Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num3);
        ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num5 - 1f), vector2), num5);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      if ((double) Utils.ToRotation(((Entity) this.NPC).velocity) > 3.1415927410125732)
        this.NPC.rotation = (float) (0.0 - 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X / 100.0);
      else
        this.NPC.rotation = (float) (0.0 + 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X / 100.0);
      if ((double) local1 < 3.0)
        local1 = 3f;
      if ((double) local2 >= (double) num1)
      {
        SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        float num6 = 10f;
        float num7 = 3f;
        Vector2 vector2_1 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num6);
        if (FargoSoulsUtil.HostCheck)
        {
          int num8 = 10;
          for (int index = 0; (double) index <= (double) local1; ++index)
          {
            double radians = (double) MathHelper.ToRadians((float) (0.0 - (double) local1 * (double) num8 / 2.0) + (float) (index * num8));
            Vector2 vector2_2 = Utils.RotatedBy(vector2_1, radians, new Vector2());
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.GunCircleCenter(1f), vector2_2, ModContent.ProjectileType<LifeWave>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num7, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        ++local1;
        local2 = (float) (num1 - num2);
      }
      ++local2;
      if ((double) local1 < 12.0)
        return;
      if (this.RuneFormation != 0)
      {
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }
      else
      {
        if (this.RuneFormationTimer < 60)
          return;
        this.Flying = true;
        this.oldstate = this.state;
        this.StateReset();
      }
    }

    public void AttackCharge()
    {
      ref float local1 = ref this.NPC.localAI[0];
      ref float local2 = ref this.NPC.localAI[1];
      ref float local3 = ref this.NPC.ai[3];
      ref float local4 = ref this.NPC.ai[2];
      Player player = Main.player[this.NPC.target];
      int num1 = (!this.Variant ? (WorldSavingSystem.MasochistModeReal ? 60 : (WorldSavingSystem.EternityMode ? 70 : 80)) : (WorldSavingSystem.MasochistModeReal ? 80 : (WorldSavingSystem.EternityMode ? 90 : 100))) + 8;
      if (this.AttackF1)
      {
        this.LockVector3 = ((Entity) this.NPC).Center;
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.RuneFormationTimer = 0;
        this.RuneFormation = 1;
      }
      this.Flying = false;
      this.Charging = true;
      this.HitPlayer = true;
      this.AuraCenter = this.LockVector3;
      float num2 = 22f;
      Vector2 vector2_1 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num2);
      if (this.Variant)
      {
        if ((double) this.AI_Timer == 0.0)
        {
          local3 = (float) Main.rand.Next(360);
          if ((double) local4 >= 6.0)
            local3 = 90f;
          this.NPC.netUpdate = true;
        }
        double num3 = (double) local3 * 0.017453293005625408;
        double num4 = 375.0;
        int num5 = (int) ((Entity) player).Center.X - (int) (Math.Cos(num3) * num4);
        int num6 = (int) ((Entity) player).Center.Y - (int) (Math.Sin(num3) * num4);
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector((float) num5, (float) num6);
        local1 = vector2_2.X;
        local2 = vector2_2.Y;
        if ((double) this.AI_Timer == 5.0 && FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(vector2_2.X + (float) (((Entity) this.NPC).width / 2), vector2_2.Y + (float) (((Entity) this.NPC).height / 2)), Vector2.Zero, ModContent.ProjectileType<LifeTpTelegraph>(), 0, 0.0f, Main.myPlayer, -61f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        if ((double) this.AI_Timer == (double) num1 - 15.0)
        {
          ((Entity) this.NPC).Center = new Vector2((float) num5, (float) num6);
          for (int index = 0; index < this.NPC.oldPos.Length; ++index)
            this.NPC.oldPos[index] = ((Entity) this.NPC).Center;
          ((Entity) this.NPC).velocity.X = 0.0f;
          ((Entity) this.NPC).velocity.Y = 0.0f;
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 0.1f);
          SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.NPC.netUpdate = true;
          if ((double) local4 >= 6.0)
          {
            this.RuneFormation = 0;
            this.RuneFormationTimer = 0;
          }
        }
      }
      if ((double) local4 == 0.0 && (double) this.AI_Timer <= 10.0)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 0.1f), 0.3f);
      if ((double) this.AI_Timer > (double) (num1 - 10) && (double) this.AI_Timer < (double) num1 && (double) local4 < 6.0)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, vector2_1, (float) (((double) this.AI_Timer - (double) (num1 - 10)) / 10.0));
      if ((double) this.AI_Timer == (double) num1 && (double) local4 < 6.0)
      {
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num7 = 10f;
          Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num7);
          int num8 = 14;
          for (int index = 0; index < num8; ++index)
          {
            Vector2 vector2_4 = Utils.RotatedBy(vector2_3, (double) index * (3.1415927410125732 / (double) (num8 / 2)), new Vector2());
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_4, ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        ((Entity) this.NPC).velocity = vector2_1;
        local3 = (float) Main.rand.Next(360);
        this.NPC.netUpdate = true;
        this.AI_Timer = 0.0f;
        ++local4;
      }
      if (!this.Variant)
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(((Entity) this.NPC).velocity, 0.99f);
      if (((double) local4 < 6.0 || (double) this.AI_Timer < (double) num1 + 15.0 || this.Variant) && ((double) local4 < 6.0 || (double) this.AI_Timer < (double) num1 + 25.0 || !this.Variant))
        return;
      NPC npc = this.NPC;
      ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.94f);
      if (this.RuneFormation != 0)
      {
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }
      else
      {
        if (this.RuneFormationTimer < 60)
          return;
        ((Entity) this.NPC).velocity.X = 0.0f;
        ((Entity) this.NPC).velocity.Y = 0.0f;
        this.HitPlayer = false;
        this.Flying = true;
        this.Charging = false;
        this.oldstate = this.state;
        this.StateReset();
      }
    }

    public void AttackPlunge()
    {
      ref float local1 = ref this.NPC.localAI[0];
      ref float local2 = ref this.NPC.localAI[1];
      ref float local3 = ref this.NPC.ai[2];
      Player player = Main.player[this.NPC.target];
      int num1 = WorldSavingSystem.MasochistModeReal ? 60 : (WorldSavingSystem.EternityMode ? 70 : 80);
      if (this.AttackF1)
      {
        this.LockVector3 = ((Entity) this.NPC).Center;
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        this.Flying = true;
        this.RuneFormation = 1;
        this.RuneFormationTimer = 0;
      }
      this.AuraCenter = this.LockVector3;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(((Entity) player).Center.X, ((Entity) player).Center.Y - 400f);
      local1 = vector2_1.X;
      double y = (double) vector2_1.Y;
      local2 = (float) y;
      if ((double) this.AI_Timer == 1.0)
        this.LockVector2 = new Vector2(((Entity) player).Center.X, ((Entity) player).Center.Y - 400f);
      if ((double) this.AI_Timer == 5.0 && FargoSoulsUtil.HostCheck)
      {
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, Vector2.Zero, ModContent.ProjectileType<LifeTpTelegraph>(), 0, 0.0f, Main.myPlayer, -40f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        if (WorldSavingSystem.MasochistModeReal)
        {
          for (int index = 0; index < 60; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(this.LockVector2.X - 1500f, (float) ((double) this.LockVector2.Y + 400.0 + 500.0) + (float) (60 * index)), Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) this.AI_Timer == (double) (num1 - 15))
      {
        this.Flying = false;
        this.Charging = true;
        ((Entity) this.NPC).Center = vector2_1;
        for (int index = 0; index < this.NPC.oldPos.Length; ++index)
          this.NPC.oldPos[index] = ((Entity) this.NPC).Center;
        ((Entity) this.NPC).velocity.X = 0.0f;
        ((Entity) this.NPC).velocity.Y = 0.1f;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer == (double) num1)
      {
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.HitPlayer = true;
        ((Entity) this.NPC).velocity.Y = 55f;
        this.NPC.netUpdate = true;
        if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
        {
          for (int index = 0; index < 120; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(this.LockVector2.X - 1200f, (float) ((double) this.LockVector2.Y + 600.0 + 500.0) + (float) (30 * index)), new Vector2(60f, 0.0f), ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          for (int index = 0; index < 120; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(this.LockVector2.X + 1200f, (float) ((double) this.LockVector2.Y + 600.0 + 500.0) + (float) (30 * index)), new Vector2(-60f, 0.0f), ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) this.AI_Timer >= (double) num1)
      {
        this.HitPlayer = true;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(((Entity) this.NPC).velocity, 0.96f);
      }
      if ((double) this.AI_Timer == (double) (num1 + 30) && FargoSoulsUtil.HostCheck)
      {
        float num2 = 3f;
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector(0.0f, 10f);
        SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = 0; index <= 15; ++index)
        {
          double radians = (double) MathHelper.ToRadians((float) (index * 12 - 90));
          Vector2 vector2_3 = Utils.RotatedBy(vector2_2, radians, new Vector2());
          if (!WorldSavingSystem.MasochistModeReal)
            vector2_3.X *= 2f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_3, ModContent.ProjectileType<LifeNeggravProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num2, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) this.AI_Timer == (double) (num1 + 45) && FargoSoulsUtil.HostCheck)
      {
        float num3 = 3f;
        Vector2 vector2_4;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_4).\u002Ector(0.0f, 10f);
        SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = 0; index <= 15; ++index)
        {
          double radians = (double) MathHelper.ToRadians((float) (index * 10 - 90));
          Vector2 vector2_5 = Utils.RotatedBy(vector2_4, radians, new Vector2());
          if (!WorldSavingSystem.MasochistModeReal)
            vector2_5.X *= 2f;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_5, ModContent.ProjectileType<LifeNeggravProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num3, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if ((double) this.AI_Timer == (double) (num1 + 50) && (double) local3 < 1.0)
      {
        this.AI_Timer = 0.0f;
        ++local3;
      }
      if ((double) this.AI_Timer == (double) (num1 + 180 - 60) && (double) local3 >= 1.0)
      {
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }
      if ((double) this.AI_Timer == (double) (num1 + 180))
      {
        ((Entity) this.NPC).position.X = ((Entity) player).Center.X - (float) (((Entity) this.NPC).width / 2);
        ((Entity) this.NPC).position.Y = ((Entity) player).Center.Y - ((float) (((Entity) this.NPC).height / 2) + 450f);
        for (int index = 0; index < this.NPC.oldPos.Length; ++index)
          this.NPC.oldPos[index] = ((Entity) this.NPC).Center;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.HitPlayer = false;
        this.Flying = true;
        this.Charging = false;
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer < (double) (num1 + 180 + 60))
        return;
      this.HitPlayer = false;
      this.oldstate = this.state;
      this.StateReset();
    }

    public void AttackPixies()
    {
      ref float local1 = ref this.NPC.ai[2];
      ref float local2 = ref this.NPC.ai[3];
      Player player = Main.player[this.NPC.target];
      int num1 = WorldSavingSystem.MasochistModeReal ? 60 : (WorldSavingSystem.EternityMode ? 70 : 80);
      if (this.AttackF1)
      {
        this.Flying = true;
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        local2 = 0.0f;
        SoundEngine.PlaySound(ref SoundID.ScaryScream, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.LockVector3 = ((Entity) this.NPC).Center;
        this.RuneFormationTimer = 0;
        this.RuneFormation = 1;
      }
      this.AuraCenter = this.LockVector3;
      this.Flying = false;
      this.Charging = true;
      if ((double) this.AI_Timer <= 10.0)
        ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 0.1f), 0.3f);
      if ((double) this.AI_Timer == (double) num1)
      {
        this.LockVector1 = ((Entity) player).Center;
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer >= 340.0)
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.94f);
        if (this.RuneFormation != 0)
        {
          this.RuneFormation = 0;
          this.RuneFormationTimer = 0;
        }
        else
        {
          if (this.RuneFormationTimer < 60)
            return;
          foreach (Projectile projectile in ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (p => p.Alive())))
          {
            if (projectile.type == ModContent.ProjectileType<LifeHomingProj>())
              projectile.ai[2] = 1f;
          }
          this.Flying = true;
          this.Charging = false;
          this.oldstate = this.state;
          this.StateReset();
        }
      }
      else
      {
        float num2 = 22f;
        Vector2 vector2_1 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num2);
        if ((double) local1 % 68.0 > 58.0)
          ((Entity) this.NPC).velocity = Vector2.Lerp(((Entity) this.NPC).velocity, vector2_1, (float) (((double) local1 % 68.0 - 58.0) / 10.0));
        if ((double) local1 == 68.0)
        {
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          ((Entity) this.NPC).velocity = vector2_1;
          this.NPC.netUpdate = true;
        }
        if ((double) this.AI_Timer % 5.0 == 0.0 && (double) local1 > 68.0 && (double) local1 < 108.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item25, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            float num3 = 3f;
            Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), (double) local2, new Vector2()), 5f);
            local2 = (float) Main.rand.Next(-30, 30) * ((float) Math.PI / 180f);
            float num4 = 0.0f;
            if (!WorldSavingSystem.MasochistModeReal)
            {
              num4 = -30f;
              vector2_2 = Vector2.op_Division(vector2_2, 2f);
            }
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<LifeHomingProj>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num3, Main.myPlayer, num4, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
        }
        if ((double) local1 >= 136.0)
          local1 = 67f;
        ++local1;
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.99f);
      }
    }

    public void AttackRoulette()
    {
      ref float local1 = ref this.NPC.localAI[0];
      ref float local2 = ref this.NPC.localAI[1];
      ref float local3 = ref this.NPC.ai[3];
      Player player = Main.player[this.NPC.target];
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.Flying = false;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        local3 = Utils.NextFloat(Main.rand, MathHelper.ToRadians(45f)) * (Utils.NextBool(Main.rand) ? 1f : -1f);
        if ((double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).Center.X)
          local3 += 3.14159274f;
        this.NPC.netUpdate = true;
      }
      Vector2 vector2_1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(500f, Utils.ToRotationVector2(local3)));
      double x = (double) vector2_1.X;
      local1 = (float) x;
      local2 = vector2_1.Y;
      if ((double) this.AI_Timer == 1.0 && FargoSoulsUtil.HostCheck)
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, Vector2.Zero, ModContent.ProjectileType<LifeTpTelegraph>(), 0, 0.0f, Main.myPlayer, -40f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      if ((double) this.AI_Timer == 40.0)
      {
        ((Entity) this.NPC).Center = vector2_1;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.LockVector1 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
        local2 = 0.0f;
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer > 40.0 && (double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)) - Utils.ToRotation(this.LockVector1))) > 1.0471975803375244)
      {
        this.LockVector1 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
        this.NPC.netUpdate = true;
      }
      if ((double) this.AI_Timer < 540.0 && (double) this.AI_Timer % 9.0 == 0.0 && (double) this.AI_Timer > 60.0 && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2_2 = Vector2.op_Multiply(Utils.RotatedBy(this.LockVector1, 1.0471975803375244, new Vector2()), 20f);
        Vector2 vector2_3 = Vector2.op_Multiply(Utils.RotatedBy(this.LockVector1, -1.0471975803375244, new Vector2()), 20f);
        ++local2;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<LifeProjSmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 3f, 0.0f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_3, ModContent.ProjectileType<LifeProjSmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 4f, 0.0f);
      }
      if ((double) this.AI_Timer >= 70.0 && (double) this.AI_Timer % 70.0 == 0.0 && (double) this.AI_Timer <= 420.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item71, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        int num1 = Utils.NextBool(Main.rand, 2) ? 1 : -1;
        float num2 = Utils.NextFloat(Main.rand, -0.3926991f, 0.3926991f);
        Vector2 vector2_4 = Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 8f), 1.5707963705062866 * (double) num1 + (double) num2, new Vector2());
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2_4), ModContent.ProjectileType<JevilScar>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
      }
      if ((double) this.AI_Timer <= 580.0)
        return;
      foreach (Projectile projectile in Main.projectile)
      {
        if (projectile.type == ModContent.ProjectileType<JevilScar>())
          projectile.ai[0] = 1200f;
      }
      this.Flying = true;
      this.oldstate = this.state;
      this.StateReset();
    }

    public void AttackReactionShotgun()
    {
      // ISSUE: variable of a compiler-generated type
      LifeChallenger.\u003C\u003Ec__DisplayClass86_0 cDisplayClass860;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass860.\u003C\u003E4__this = this;
      if (this.PhaseOne)
      {
        this.oldP1state = this.P1state;
        this.P1stateReset();
      }
      else
      {
        this.oldstate = this.state;
        this.StateReset();
      }
    }

    public void AttackRunningMinigun()
    {
      ref float local = ref this.NPC.ai[2];
      Player player = Main.player[this.NPC.target];
      int num1 = WorldSavingSystem.MasochistModeReal ? 40 : (WorldSavingSystem.EternityMode ? 50 : 60);
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        SoundEngine.PlaySound(ref SoundID.Zombie100, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        this.NPC.netUpdate = true;
        this.rot = 0.0;
        this.RuneFormation = 2;
        this.RuneFormationTimer = 0;
      }
      this.GunRotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
      this.Flying = false;
      float num2 = 5f;
      float num3 = 5f;
      Vector2 vector2_1 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num2);
      ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Addition(Vector2.op_Multiply(((Entity) this.NPC).velocity, num3 - 1f), vector2_1), num3);
      int num4 = 360 + num1;
      float num5 = this.AI_Timer / (float) num4 * 0.2f;
      NPC npc = this.NPC;
      ((Entity) npc).position = Vector2.op_Addition(((Entity) npc).position, Vector2.op_Multiply(((Entity) this.NPC).velocity, num5));
      if ((double) Utils.ToRotation(((Entity) this.NPC).velocity) > 3.1415927410125732)
        this.NPC.rotation = (float) (0.0 - 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X / 100.0);
      else
        this.NPC.rotation = (float) (0.0 + 3.1415927410125732 * (double) ((Entity) this.NPC).velocity.X / 100.0);
      if ((double) this.AI_Timer >= (double) num1 && (double) this.AI_Timer % 15.0 == 0.0)
      {
        SoundEngine.PlaySound(ref SoundID.DD2_WitherBeastCrystalImpact, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = -1; index < 2; index += 2)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2_2 = Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 12f), (double) index * this.rot * 3.1415927410125732 / 180.0, new Vector2());
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.GunCircleCenter(0.8f), vector2_2, ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
        if (this.rot >= 0.0)
          this.rot += (double) local < 5.3333334922790527 || (double) local >= 10.666666984558105 && (double) local < 16.0 ? 7.5 : -7.5;
        else
          this.rot = 0.0;
        ++local;
      }
      if (((double) this.AI_Timer == (double) num4 || (double) this.AI_Timer == (double) ((num4 + num1) / 2)) && FargoSoulsUtil.HostCheck)
      {
        Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 12f);
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.GunCircleCenter(0.8f), vector2_3, ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      if ((double) this.AI_Timer < (double) num4)
        return;
      if (this.RuneFormation != 0)
      {
        this.RuneFormation = 0;
        this.RuneFormationTimer = 0;
      }
      else
      {
        if (this.RuneFormationTimer < 60)
          return;
        this.Flying = true;
        this.oldstate = this.state;
        this.StateReset();
      }
    }

    public void AttackTeleportNukes()
    {
      ref float local1 = ref this.NPC.localAI[0];
      ref float local2 = ref this.NPC.localAI[1];
      ref float local3 = ref this.NPC.ai[2];
      Player player = Main.player[this.NPC.target];
      int num1 = WorldSavingSystem.MasochistModeReal ? 60 : (WorldSavingSystem.EternityMode ? 75 : 85);
      if (this.AttackF1)
      {
        this.AttackF1 = false;
        this.LockVector1 = ((Entity) player).Center;
        this.Flying = false;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.netUpdate = true;
        this.RuneFormation = 2;
        this.RuneFormationTimer = 0;
      }
      local1 = this.LockVector1.X;
      double y = (double) this.LockVector1.Y;
      local2 = (float) y;
      if ((double) this.AI_Timer == 1.0 && FargoSoulsUtil.HostCheck)
      {
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.LockVector1, Vector2.Zero, ModContent.ProjectileType<LifeTpTelegraph>(), 0, 0.0f, Main.myPlayer, -60f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        for (int index = 0; index < 16; ++index)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.LockVector1, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, 0.3926991f * (float) index, 0.0f);
      }
      if ((double) this.AI_Timer < (double) num1)
        this.LockVector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
      if ((double) this.AI_Timer >= (double) num1 && (double) local3 < 6.0)
        FargoSoulsUtil.ScreenshakeRumble(7f);
      if ((double) this.AI_Timer == (double) num1)
      {
        ((Entity) this.NPC).Center = this.LockVector1;
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index = 0; index < 16; ++index)
        {
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, 0.39269909262657166 * (double) index, new Vector2()), 24f), ModContent.ProjectileType<LifeProjLarge>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        for (int index = 0; index < 6; ++index)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(this.LockVector2) + 1.04719758f * (float) index, 0.0f);
      }
      if ((double) this.AI_Timer < (double) (num1 + 60))
      {
        this.GunRotation = Utils.ToRotation(this.LockVector2);
      }
      else
      {
        float num2 = this.AI_Timer - (float) (num1 + 60);
        if ((double) local3 >= 6.0)
          num2 = (float) (18.0 + ((double) num2 - 18.0) / 5.0);
        float num3 = num2 / 3f;
        this.GunRotation = Utils.ToRotation(this.LockVector2) + 1.04719758f * num3;
      }
      if ((double) this.AI_Timer >= (double) (num1 + 60) && ((double) this.AI_Timer - (double) (num1 + 60)) % 3.0 == 0.0 && (double) local3 < 6.0)
      {
        SoundEngine.PlaySound(ref SoundID.Item91, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (FargoSoulsUtil.HostCheck)
        {
          float num4 = WorldSavingSystem.MasochistModeReal ? 32f : 24f;
          float num5 = 0.0f;
          float num6 = 0.6f;
          float num7 = 16f;
          int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(this.LockVector2, 1.0471975803375244 * (double) local3, new Vector2()), num7), ModContent.ProjectileType<LifeNuke>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, num4, num5, num6);
          if (index != Main.maxProjectiles)
            Main.projectile[index].timeLeft = 60;
        }
        ++local3;
        if ((double) local3 >= 6.0)
        {
          this.RuneFormation = 0;
          this.RuneFormationTimer = 0;
        }
      }
      if ((double) this.AI_Timer <= (double) (num1 + 360))
        return;
      this.Flying = true;
      this.oldstate = this.state;
      this.StateReset();
    }

    public void AttackRuneExpand()
    {
      ref float local1 = ref this.NPC.ai[3];
      ref float local2 = ref this.NPC.ai[2];
      Player player = Main.player[this.NPC.target];
      this.NPC.localAI[0] = this.RuneDistance;
      this.NPC.localAI[1] = this.BodyRotation;
      this.NPC.localAI[2] = 12f;
      int num1 = this.PhaseOne ? 5 : 470;
      if (this.AttackF1)
      {
        if (this.PhaseOne && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 550.0 && !WorldSavingSystem.MasochistModeReal)
        {
          ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
          ((Entity) this.NPC).Size = new Vector2(200f, 200f);
          ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
          this.oldP1state = this.P1state;
          this.P1stateReset();
          return;
        }
        this.AttackF1 = false;
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        if (this.PyramidPhase == 0)
          this.PyramidTimer = 0;
        this.PyramidPhase = 1;
        for (int index = 0; index < 12; ++index)
        {
          Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(this.BodyRotation + 0.5235988f * (float) index), this.RuneDistance));
          this.DrawRunes = false;
          this.NPC.netUpdate = true;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<LifeRuneHitbox>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 3f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) index, 0.0f);
        }
        if (!this.PhaseOne)
        {
          this.Flying = false;
          ((Entity) this.NPC).velocity = Vector2.Zero;
          ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
          ((Entity) this.NPC).Size = new Vector2(130f, 130f);
          ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
        }
        else
        {
          ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
          ((Entity) this.NPC).Size = new Vector2(130f, 130f);
          ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
        }
      }
      if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 2000.0)
      {
        this.FlyingState(1.5f);
      }
      else
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
      }
      if ((double) this.AI_Timer < 175.0)
      {
        this.RuneDistance = !WorldSavingSystem.MasochistModeReal ? (float) (100.0 + Math.Pow((double) this.AI_Timer / 5.0, 2.0)) : Math.Min((float) (100.0 + Math.Pow((double) this.AI_Timer / 5.0, 2.0)), 1200f);
        this.RPS += 0.0005f;
      }
      if ((double) this.AI_Timer >= 175.0 && !this.PhaseOne)
      {
        this.HitPlayer = true;
        float num2 = 30f;
        float num3 = 3f;
        int num4 = 24 + (int) local1;
        float num5 = 6.28318548f / local1;
        if (((double) this.AI_Timer - 175.0) % 40.0 == 0.0 && (double) local1 < 9.0)
        {
          this.LockVector1 = ((Entity) this.NPC).Center;
          this.LockVector2 = Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), num2), 0.039269909262657166 * ((double) Utils.NextFloat(Main.rand) - 0.5), new Vector2());
          local2 = Utils.NextFloat(Main.rand, (float) (-(double) num5 / 2.0), num5 / 2f);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; (double) index < (double) num4; ++index)
            {
              double num6 = 6.2831854820251465 / (double) num4 * (double) index;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.LockVector1, Vector2.Zero, ModContent.ProjectileType<BloomLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, Utils.ToRotation(Utils.RotatedBy(this.LockVector2, num6, new Vector2())), 0.0f);
            }
          }
          this.NPC.netUpdate = true;
        }
        if (((double) this.AI_Timer - 175.0) % 40.0 == 39.0 && (double) local1 < 9.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; (double) index < (double) num4; ++index)
            {
              double num7 = 6.2831854820251465 / (double) num4 * (double) index;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), this.LockVector1, Utils.RotatedBy(this.LockVector2, num7, new Vector2()), ModContent.ProjectileType<LifeWave>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), num3, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          ++local1;
        }
      }
      if ((double) this.AI_Timer == (double) (175 + num1))
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      if ((double) this.AI_Timer >= (double) (175 + num1))
      {
        this.HitPlayer = false;
        this.RuneDistance = !WorldSavingSystem.MasochistModeReal ? (float) (100.0 + Math.Pow((175.0 - ((double) this.AI_Timer - 175.0 - (double) num1)) / 5.0, 2.0)) : Math.Min((float) (100.0 + Math.Pow((175.0 - ((double) this.AI_Timer - 175.0 - (double) num1)) / 5.0, 2.0)), 1200f);
        this.RPS -= 0.0005f;
      }
      if ((double) this.AI_Timer < (double) (175 + num1 + 175))
        return;
      this.RuneDistance = 100f;
      if (FargoSoulsUtil.HostCheck)
      {
        foreach (Projectile projectile in Main.projectile)
        {
          if (projectile.type == ModContent.ProjectileType<LifeRuneHitbox>())
            projectile.Kill();
        }
      }
      this.DrawRunes = true;
      this.NPC.netUpdate = true;
      this.PyramidPhase = -1;
      this.PyramidTimer = 0;
      if (this.PhaseOne)
      {
        ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
        ((Entity) this.NPC).Size = new Vector2(200f, 200f);
        ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
        this.oldP1state = this.P1state;
        this.P1stateReset();
      }
      else
      {
        ((Entity) this.NPC).position = ((Entity) this.NPC).Center;
        ((Entity) this.NPC).Size = new Vector2(200f, 200f);
        ((Entity) this.NPC).Center = ((Entity) this.NPC).position;
        this.Flying = true;
        this.oldstate = this.state;
        this.StateReset();
      }
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Division(local1, 2f);
      if (this.phaseProtectionDR)
      {
        ref StatModifier local2 = ref modifiers.FinalDamage;
        local2 = StatModifier.op_Division(local2, 4f);
      }
      else
      {
        if (!this.useDR)
          return;
        ref StatModifier local3 = ref modifiers.FinalDamage;
        local3 = StatModifier.op_Division(local3, 2.5f);
      }
    }

    public virtual void ModifyHoverBoundingBox(ref Rectangle boundingBox)
    {
      boundingBox = ((Entity) this.NPC).Hitbox;
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      if (projectile.type != ModContent.ProjectileType<DecrepitAirstrikeNuke>() && projectile.type != ModContent.ProjectileType<DecrepitAirstrikeNukeSplinter>())
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.75f);
    }

    public virtual void UpdateLifeRegen(ref int damage)
    {
      if (this.NPC.lifeRegen >= 0)
        return;
      this.NPC.lifeRegen /= 2;
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      return this.HitPlayer && this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return this.HitPlayer && this.Collides(((Entity) target).position, ((Entity) target).Size);
    }

    public bool Collides(Vector2 boxPos, Vector2 boxDim)
    {
      Vector2 size = ((Entity) this.NPC).Size;
      Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, Vector2.op_Multiply(0.5f, new Vector2((float) ((Entity) this.NPC).width, (float) ((Entity) this.NPC).height)));
      float num1 = 0.0f;
      float num2 = 0.0f;
      if ((double) boxPos.X > (double) vector2.X)
        num1 = boxPos.X - vector2.X;
      else if ((double) boxPos.X + (double) boxDim.X < (double) vector2.X)
        num1 = boxPos.X + boxDim.X - vector2.X;
      if ((double) boxPos.Y > (double) vector2.Y)
        num2 = boxPos.Y - vector2.Y;
      else if ((double) boxPos.Y + (double) boxDim.Y < (double) vector2.Y)
        num2 = boxPos.Y + boxDim.Y - vector2.Y;
      float num3 = size.X / 2f;
      float num4 = size.Y / 2f;
      return (double) num1 * (double) num1 / ((double) num3 * (double) num3) + (double) num2 * (double) num2 / ((double) num4 * (double) num4) < 1.0;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life <= 0)
      {
        for (int index = 0; index < 400; ++index)
        {
          Color drawColor = Utils.NextFromList<Color>(Main.rand, new Color[3]
          {
            Color.Goldenrod,
            Color.Pink,
            Color.Cyan
          });
          Particle particle1 = (Particle) new SmallSparkle(((Entity) this.NPC).Center, Utils.RotatedByRandom(Vector2.op_Multiply(Utils.NextFloat(Main.rand, 5f, 50f), Vector2.UnitX), 6.2831854820251465), drawColor, 1f, Main.rand.Next(20, 80), rotationSpeed: Utils.NextFloat(Main.rand, -0.3926991f, 0.3926991f));
          particle1.Spawn();
          Particle particle2 = particle1;
          particle2.Position = Vector2.op_Subtraction(particle2.Position, Vector2.op_Multiply(particle1.Velocity, 4f));
        }
      }
      else
      {
        if ((double) this.NPC.GetLifePercent() >= (double) this.chunklist.Count / 50.0 || this.P1state == -2 || this.PyramidPhase != 0 || this.chunklist.Count <= 0)
          return;
        int index = Main.rand.Next(this.chunklist.Count);
        Vector4 vector4 = this.chunklist[index];
        Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(vector4.X, Vector2.UnitX), this.ChunkDistance), Vector2.op_Multiply(Vector2.op_Multiply(vector4.Y, Vector2.UnitY), this.ChunkDistance));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).Center, vector2_1);
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          interpolatedStringHandler.AppendLiteral("ShardGold");
          interpolatedStringHandler.AppendFormatted<float>(vector4.W);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
        this.chunklist.RemoveAt(index);
        SoundEngine.PlaySound(ref SoundID.Tink, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      for (int index = 0; index < this.chunklist.Count; ++index)
      {
        this.chunklist[index] = LifeChallenger.RotateByMatrix(this.chunklist[index], (float) Math.PI / 180f, Vector3.UnitX);
        this.chunklist[index] = LifeChallenger.RotateByMatrix(this.chunklist[index], -1f * (float) Math.PI / 360f, Vector3.UnitZ);
        this.chunklist[index] = LifeChallenger.RotateByMatrix(this.chunklist[index], -1f * (float) Math.PI / 720f, Vector3.UnitY);
      }
      this.chunklist.Sort((Comparison<Vector4>) ((x, y) => Math.Sign(x.Z - y.Z)));
      foreach (Vector4 chunk in this.chunklist.Where<Vector4>((Func<Vector4, bool>) (pos => (double) pos.Z <= 0.0)))
        this.DrawChunk(chunk, spriteBatch, drawColor);
      List<LifeChallenger.Rune> source = new List<LifeChallenger.Rune>();
      this.PostdrawRunes = new List<LifeChallenger.Rune>();
      if (this.Draw || this.NPC.IsABestiaryIconDummy)
      {
        if (this.DrawRunes)
        {
          for (int index = 0; index < 12; ++index)
          {
            float num1 = this.BodyRotation + 0.5235988f * (float) index;
            Vector2 vector2_2 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Utils.ToRotationVector2(num1), this.RuneDistance));
            if (this.RuneFormation != 0 || this.RuneFormationTimer >= 60)
              this.InternalRuneFormation = this.RuneFormation;
            float rotation = num1 + 1.57079637f;
            float scale = this.NPC.scale;
            float num2 = (float) ((double) num1 % 6.2831854820251465 / 6.2831854820251465);
            float num3 = 0.0f;
            Vector2 vector2_3;
            switch (this.InternalRuneFormation)
            {
              case 1:
                float num4 = this.NPC.rotation - 1.57079637f;
                Vector2 vector2_4 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(num4), 100f), 1.2f));
                float num5 = num2 - 0.5f;
                Vector2 zero = Vector2.Zero;
                float num6 = (float) ((Entity) this.NPC).width * 1.2f;
                num3 = Math.Abs(num5);
                Vector2 vector2_5 = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.ToRotationVector2((float) ((double) this.NPC.rotation - 1.5707963705062866 - 0.40840703248977661 * (double) MathHelper.Lerp(1f, 3f, MathF.Pow(Math.Abs(num5), 1.5f)) * (double) Math.Sign(num5)))), num6), Math.Abs(num5));
                Vector2 vector2_6 = Vector2.op_Addition(vector2_4, vector2_5);
                vector2_3 = Vector2.SmoothStep(vector2_2, vector2_6, this.FormationLerp);
                break;
              case 2:
                float num7 = num1;
                Vector2 vector2_7 = Vector2.op_Subtraction(Vector2.op_Addition(vector2_1, this.GunCircleCenter(1f)), ((Entity) this.NPC).Center);
                float num8 = 90f;
                if (this.CloserGun)
                  num8 += 40f;
                float num9 = MathF.Pow(Math.Abs(Utils.ToRotationVector2(num7).X), 1f);
                float num10 = num8 * MathHelper.Lerp(1f, 0.6f, num9);
                Vector2 vector2_8 = Vector2.op_Multiply(Utils.ToRotationVector2(this.GunRotation + num7), num10);
                Vector2 vector2_9 = Vector2.op_Addition(vector2_7, vector2_8);
                vector2_3 = Vector2.SmoothStep(vector2_2, vector2_9, this.FormationLerp);
                float num11 = (float) (((double) Utils.ToRotationVector2((float) (((double) num7 + 1.5707963705062866) % 6.2831854820251465)).Y + 1.0) / 2.0);
                scale *= MathHelper.Lerp(1.3f, 0.7f, num11);
                num3 = num11;
                break;
              default:
                vector2_3 = vector2_2;
                break;
            }
            if ((double) num3 <= 0.0)
              source.Add(new LifeChallenger.Rune(new Vector3(vector2_3.X, vector2_3.Y, num3), index, scale, rotation));
            else
              this.PostdrawRunes.Add(new LifeChallenger.Rune(new Vector3(vector2_3.X, vector2_3.Y, num3), index, scale, rotation));
          }
        }
        if (source.Any<LifeChallenger.Rune>())
        {
          source.Sort((Comparison<LifeChallenger.Rune>) ((x, y) => Math.Sign(x.Center.Z - y.Center.Z)));
          foreach (LifeChallenger.Rune rune in source)
            this.DrawRune(rune, spriteBatch, drawColor);
        }
        if (this.DoAura)
        {
          for (int index1 = 0; index1 < 48; ++index1)
          {
            float num12 = (float) ((this.InternalRuneFormation == 2 ? (double) this.BodyRotation / (double) MathHelper.Lerp(1f, 3f, this.FormationLerp) : (double) this.BodyRotation) + Math.PI / 24.0 * (double) index1);
            DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
            interpolatedStringHandler.AppendLiteral("Rune");
            interpolatedStringHandler.AppendFormatted<int>(index1 % 12 + 1);
            Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/" + interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 1).Value;
            Vector2 vector2_10 = Vector2.op_Subtraction(Vector2.op_Addition(this.AuraCenter, Vector2.op_Multiply(Utils.ToRotationVector2(num12), 1100f + this.RuneDistance)), screenPos);
            float num13 = num12 + 1.57079637f;
            for (int index2 = 0; index2 < 48; ++index2)
            {
              Vector2 vector2_11 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index2 / 12.0)), 1f);
              Color color = index1 % 3 != 0 ? (index1 % 3 != 1 ? Color.op_Multiply(new Color(1f, 0.7529412f, 0.796078444f, 0.0f), 0.7f) : Color.op_Multiply(new Color(1f, 1f, 0.0f, 0.0f), 0.7f)) : Color.op_Multiply(new Color(0.0f, 1f, 1f, 0.0f), 0.7f);
              Main.spriteBatch.Draw(texture2D1, Vector2.op_Addition(vector2_10, vector2_11), new Rectangle?(), color, num13, Vector2.op_Multiply(Utils.Size(texture2D1), 0.5f), this.NPC.scale, (SpriteEffects) 0, 0.0f);
            }
            SpriteBatch spriteBatch1 = spriteBatch;
            Vector2 vector2_12;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_12).\u002Ector((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2));
            Texture2D texture2D2 = texture2D1;
            Vector2 vector2_13 = vector2_10;
            Rectangle? nullable = new Rectangle?();
            Color white = Color.White;
            double num14 = (double) num13;
            Vector2 vector2_14 = vector2_12;
            double scale = (double) this.NPC.scale;
            spriteBatch1.Draw(texture2D2, vector2_13, nullable, white, (float) num14, vector2_14, (float) scale, (SpriteEffects) 0, 0.0f);
          }
        }
        if ((double) this.ChunkDistance > 3.0)
        {
          Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Lifelight_WingUpper", (AssetRequestMode) 1).Value;
          Texture2D texture2D4 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Lifelight_WingLower", (AssetRequestMode) 1).Value;
          Vector2 vector2_15 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
          int y = this.NPC.frame.Y;
          int num15 = texture2D3.Height / Main.npcFrameCount[this.NPC.type];
          Rectangle rectangle1;
          // ISSUE: explicit constructor call
          ((Rectangle) ref rectangle1).\u002Ector(0, y * num15, texture2D3.Width, num15);
          int num16 = texture2D4.Height / Main.npcFrameCount[this.NPC.type];
          Rectangle rectangle2;
          // ISSUE: explicit constructor call
          ((Rectangle) ref rectangle2).\u002Ector(0, y * num16, texture2D4.Width, num16);
          Vector2 vector2_16;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_16).\u002Ector((float) (texture2D3.Width / 2), (float) (texture2D3.Height / 2 / Main.npcFrameCount[this.NPC.type]));
          Vector2 vector2_17;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2_17).\u002Ector((float) (texture2D4.Width / 2), (float) (texture2D4.Height / 2 / Main.npcFrameCount[this.NPC.type]));
          float chunkDistance = this.ChunkDistance;
          if (this.InternalRuneFormation == 1)
            chunkDistance /= MathHelper.Lerp(1f, 1.5f, MathHelper.Clamp((float) this.RuneFormationTimer / 60f, 0.0f, 1f));
          for (int index = -1; index < 2; index += 2)
          {
            float num17 = this.NPC.rotation - 1.57079637f + MathHelper.ToRadians((float) (110 * index));
            float num18 = this.NPC.rotation - 1.57079637f + MathHelper.ToRadians((float) (70 * index));
            SpriteEffects spriteEffects1 = index == 1 ? (SpriteEffects) 0 : (SpriteEffects) 2;
            SpriteBatch spriteBatch2 = spriteBatch;
            Vector2 vector2_18 = vector2_16;
            Texture2D texture2D5 = texture2D3;
            Vector2 vector2_19 = Vector2.op_Addition(vector2_15, Vector2.op_Multiply(Utils.ToRotationVector2(num18), chunkDistance + 30f));
            Rectangle? nullable1 = new Rectangle?(rectangle1);
            Color color1 = drawColor;
            double num19 = (double) num18;
            Vector2 vector2_20 = vector2_18;
            double scale1 = (double) this.NPC.scale;
            SpriteEffects spriteEffects2 = spriteEffects1;
            spriteBatch2.Draw(texture2D5, vector2_19, nullable1, color1, (float) num19, vector2_20, (float) scale1, spriteEffects2, 0.0f);
            SpriteBatch spriteBatch3 = spriteBatch;
            Vector2 vector2_21 = vector2_17;
            Texture2D texture2D6 = texture2D4;
            Vector2 vector2_22 = Vector2.op_Addition(vector2_15, Vector2.op_Multiply(Utils.ToRotationVector2(num17), chunkDistance + 30f));
            Rectangle? nullable2 = new Rectangle?(rectangle2);
            Color color2 = drawColor;
            double num20 = (double) num17;
            Vector2 vector2_23 = vector2_21;
            double scale2 = (double) this.NPC.scale;
            SpriteEffects spriteEffects3 = spriteEffects1;
            spriteBatch3.Draw(texture2D6, vector2_22, nullable2, color2, (float) num20, vector2_23, (float) scale2, spriteEffects3, 0.0f);
          }
        }
      }
      return false;
    }

    public virtual void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      if ((double) Main.LocalPlayer.gravDir < 0.0)
        vector2_1.Y = (float) Main.screenHeight - vector2_1.Y;
      if ((double) this.ChunkDistance > 20.0)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        Texture2D texture2D = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/Effects/LifeStar", (AssetRequestMode) 1).Value;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D.Width, texture2D.Height);
        float num = 0.45f * Utils.NextFloat(Main.rand, 1f, 2.5f);
        Vector2 vector2_2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2_2).\u002Ector((float) (texture2D.Width / 2) + num, (float) (texture2D.Height / 2) + num);
        Vector2 vector2_3 = vector2_1;
        if (this.NPC.IsABestiaryIconDummy)
          vector2_3 = Vector2.op_Addition(vector2_3, Vector2.op_Addition(Vector2.op_Multiply(Vector2.UnitX, 85f), Vector2.op_Multiply(Vector2.UnitY, 48f)));
        spriteBatch.Draw(texture2D, vector2_3, new Rectangle?(rectangle), Color.HotPink, 0.0f, vector2_2, num, (SpriteEffects) 0, 0.0f);
        DrawData drawData;
        // ISSUE: explicit constructor call
        ((DrawData) ref drawData).\u002Ector(texture2D, vector2_3, new Rectangle?(rectangle), Color.HotPink, 0.0f, vector2_2, num, (SpriteEffects) 0, 0.0f);
        GameShaders.Misc["LCWingShader"].UseColor(Color.HotPink).UseSecondaryColor(Color.HotPink);
        GameShaders.Misc["LCWingShader"].Apply(new DrawData?());
        ((DrawData) ref drawData).Draw(spriteBatch);
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      foreach (Vector4 chunk in this.chunklist.Where<Vector4>((Func<Vector4, bool>) (pos => (double) pos.Z > 0.0)))
        this.DrawChunk(chunk, spriteBatch, drawColor);
      if (this.PyramidPhase != 0)
      {
        float num1 = 0.0f;
        if (this.P1state == -1)
          num1 = Utils.ToRotation(Utils.RotatedBy(this.LockVector1, this.rot + 1.5707963705062866, new Vector2()));
        if (this.PyramidPhase == 1 && this.PyramidTimer > 60)
        {
          Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/PyramidFull", (AssetRequestMode) 1).Value;
          Rectangle rectangle;
          // ISSUE: explicit constructor call
          ((Rectangle) ref rectangle).\u002Ector(0, 0, texture2D1.Width, texture2D1.Height);
          Vector2 vector2_4 = Vector2.op_Division(Utils.Size(rectangle), 2f);
          SpriteBatch spriteBatch1 = spriteBatch;
          Vector2 vector2_5 = vector2_4;
          Texture2D texture2D2 = texture2D1;
          Vector2 vector2_6 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
          Rectangle? nullable = new Rectangle?(rectangle);
          Color color = drawColor;
          double num2 = (double) num1;
          Vector2 vector2_7 = vector2_5;
          double scale = (double) this.NPC.scale;
          spriteBatch1.Draw(texture2D2, vector2_6, nullable, color, (float) num2, vector2_7, (float) scale, (SpriteEffects) 0, 0.0f);
        }
        else
        {
          Texture2D[] texture2DArray = new Texture2D[4];
          Rectangle[] rectangleArray = new Rectangle[4];
          Vector2[] vector2Array1 = new Vector2[4];
          Vector2[] vector2Array2 = new Vector2[4];
          texture2DArray[0] = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Pyramid_U", (AssetRequestMode) 1).Value;
          texture2DArray[1] = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Pyramid_L", (AssetRequestMode) 1).Value;
          texture2DArray[2] = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Pyramid_R", (AssetRequestMode) 1).Value;
          texture2DArray[3] = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/Pyramid_D", (AssetRequestMode) 1).Value;
          float num3 = 0.0f;
          if (this.PyramidPhase > 0)
            num3 = 1f - Math.Min((float) this.PyramidTimer / 60f, 1f);
          else if (this.PyramidPhase < 0)
            num3 = Math.Min((float) ((double) this.PyramidTimer * 4.0 / 60.0), 1f);
          float num4 = num3;
          byte num5 = (byte) ((double) byte.MaxValue * (1.0 - (double) num3));
          vector2Array2[0] = Vector2.op_Addition(Vector2.op_Multiply(new Vector2(0.0f, -15f), num4), new Vector2(0.0f, -30f));
          vector2Array2[1] = Vector2.op_Addition(Vector2.op_Multiply(new Vector2(-12.5f, 3f), num4), new Vector2(-25f, 10f));
          vector2Array2[2] = Vector2.op_Addition(Vector2.op_Multiply(new Vector2(12.5f, 3f), num4), new Vector2(25f, 10f));
          vector2Array2[3] = Vector2.op_Addition(Vector2.op_Multiply(new Vector2(0.0f, 10f), num4), new Vector2(0.0f, 20f));
          Color color1 = drawColor;
          ((Color) ref color1).A = num5;
          for (int index = 0; index < 4; ++index)
          {
            rectangleArray[index] = new Rectangle(0, 0, texture2DArray[index].Width, texture2DArray[index].Height);
            vector2Array1[index] = Vector2.op_Division(Utils.Size(texture2DArray[index]), 2f);
            vector2Array2[index] = Utils.RotatedBy(vector2Array2[index], (double) num1, new Vector2());
            SpriteBatch spriteBatch2 = spriteBatch;
            Vector2 vector2_8 = vector2Array1[index];
            Texture2D texture2D = texture2DArray[index];
            Vector2 vector2_9 = Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2Array2[index]), screenPos);
            Rectangle? nullable = new Rectangle?(rectangleArray[index]);
            Color color2 = color1;
            double num6 = (double) num1;
            Vector2 vector2_10 = vector2_8;
            double scale = (double) this.NPC.scale;
            spriteBatch2.Draw(texture2D, vector2_9, nullable, color2, (float) num6, vector2_10, (float) scale, (SpriteEffects) 0, 0.0f);
          }
        }
      }
      if (!this.PostdrawRunes.Any<LifeChallenger.Rune>())
        return;
      this.PostdrawRunes.Sort((Comparison<LifeChallenger.Rune>) ((x, y) => Math.Sign(x.Center.Z - y.Center.Z)));
      foreach (LifeChallenger.Rune postdrawRune in this.PostdrawRunes)
        this.DrawRune(postdrawRune, spriteBatch, drawColor);
    }

    private void DrawRune(LifeChallenger.Rune rune, SpriteBatch spriteBatch, Color drawColor)
    {
      int index1 = rune.Index;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      interpolatedStringHandler.AppendLiteral("Rune");
      interpolatedStringHandler.AppendFormatted<int>(index1 + 1);
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/" + interpolatedStringHandler.ToStringAndClear(), (AssetRequestMode) 1).Value;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(rune.Center.X, rune.Center.Y);
      for (int index2 = 0; index2 < 12; ++index2)
      {
        Vector2 vector2_2 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2((float) (6.2831854820251465 * (double) index2 / 12.0)), 1f), rune.Scale);
        Color color = index1 % 3 != 0 ? (index1 % 3 != 1 ? Color.op_Multiply(new Color(1f, 0.7529412f, 0.796078444f, 0.0f), 0.7f) : Color.op_Multiply(new Color(1f, 1f, 0.0f, 0.0f), 0.7f)) : Color.op_Multiply(new Color(0.0f, 1f, 1f, 0.0f), 0.7f);
        Main.spriteBatch.Draw(texture2D1, Vector2.op_Addition(vector2_1, vector2_2), new Rectangle?(), color, rune.Rotation, Vector2.op_Multiply(Utils.Size(texture2D1), 0.5f), rune.Scale, (SpriteEffects) 0, 0.0f);
      }
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 vector2_3;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_3).\u002Ector((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2));
      Texture2D texture2D2 = texture2D1;
      Vector2 vector2_4 = vector2_1;
      Rectangle? nullable = new Rectangle?();
      Color white = Color.White;
      double rotation = (double) rune.Rotation;
      Vector2 vector2_5 = vector2_3;
      double scale = (double) rune.Scale;
      spriteBatch1.Draw(texture2D2, vector2_4, nullable, white, (float) rotation, vector2_5, (float) scale, (SpriteEffects) 0, 0.0f);
    }

    private void DrawChunk(Vector4 chunk, SpriteBatch spriteBatch, Color drawColor)
    {
      if ((double) this.ChunkDistance <= 20.0)
        return;
      Vector3 vector3 = Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(chunk.X, Vector3.UnitX), Vector3.op_Multiply(chunk.Y, Vector3.UnitY)), Vector3.op_Multiply(chunk.Z, Vector3.UnitZ));
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
      interpolatedStringHandler.AppendLiteral("ShardGold");
      interpolatedStringHandler.AppendFormatted<float>(chunk.W);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      float num1 = 0.3f * vector3.Z;
      byte num2 = (byte) (150.0 + 100.0 * (double) vector3.Z);
      Color color1 = drawColor;
      ((Color) ref color1).A = num2;
      Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, Main.screenPosition);
      float chunkDistance = this.ChunkDistance;
      if (this.RuneFormation == 1)
        chunkDistance /= MathHelper.Lerp(1f, 1.5f, MathHelper.Clamp((float) this.RuneFormationTimer / 60f, 0.0f, 1f));
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Multiply(Vector2.op_Multiply(vector3.X, Vector2.UnitX), chunkDistance), Vector2.op_Multiply(Vector2.op_Multiply(vector3.Y, Vector2.UnitY), chunkDistance));
      Vector2 vector2_3 = Vector2.op_Addition(vector2_1, vector2_2);
      Texture2D texture2D1 = ModContent.Request<Texture2D>("FargowiltasSouls/Assets/ExtraTextures/LifelightParts/" + stringAndClear, (AssetRequestMode) 1).Value;
      SpriteBatch spriteBatch1 = spriteBatch;
      Vector2 vector2_4;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_4).\u002Ector((float) (texture2D1.Width / 2), (float) (texture2D1.Height / 2));
      Texture2D texture2D2 = texture2D1;
      Vector2 vector2_5 = vector2_3;
      Rectangle? nullable = new Rectangle?();
      Color color2 = color1;
      Vector2 vector2_6 = vector2_4;
      double num3 = (double) this.NPC.scale + (double) num1;
      spriteBatch1.Draw(texture2D2, vector2_5, nullable, color2, 0.0f, vector2_6, (float) num3, (SpriteEffects) 0, 0.0f);
    }

    public float WidthFunction(float completionRatio)
    {
      return MathHelper.SmoothStep(this.NPC.scale * 20f, 3.5f, completionRatio);
    }

    public Color ColorFunction(float completionRatio)
    {
      float num = (float) ((double) Main.GameUpdateCount % 180.0 / 60.0) + completionRatio * 2f;
      return Color.op_Multiply(Color.Lerp((double) num > 1.0 ? ((double) num <= 1.0 || (double) num > 2.0 ? Color.Lerp(Color.DeepPink, Color.Cyan, num - 2f) : Color.Lerp(Color.Goldenrod, Color.DeepPink, num - 1f)) : Color.Lerp(Color.Cyan, Color.Goldenrod, num), Color.Transparent, completionRatio * this.FormationLerp), 0.7f);
    }

    public void RenderPixelatedPrimitives(SpriteBatch spriteBatch)
    {
      if (this.InternalRuneFormation != 1)
        return;
      ManagedShader shader = ShaderManager.GetShader("FargowiltasSouls.BlobTrail");
      FargosTextureRegistry.FadedStreak.Value.SetTexture1();
      // ISSUE: method pointer
      // ISSUE: method pointer
      // ISSUE: method pointer
      PrimitiveRenderer.RenderTrail((IEnumerable<Vector2>) this.NPC.oldPos, new PrimitiveSettings(new PrimitiveSettings.VertexWidthFunction((object) this, __methodptr(WidthFunction)), new PrimitiveSettings.VertexColorFunction((object) this, __methodptr(ColorFunction)), new PrimitiveSettings.VertexOffsetFunction((object) this, __methodptr(\u003CRenderPixelatedPrimitives\u003Eb__110_0)), true, true, shader, new int?(), new int?(), false, new (Vector2, Vector2)?()), new int?(44));
    }

    public virtual void FindFrame(int frameHeight)
    {
      double num = 6.0;
      this.NPC.spriteDirection = ((Entity) this.NPC).direction;
      ++this.NPC.frameCounter;
      this.NPC.frameCounter %= (double) Main.npcFrameCount[this.NPC.type] * num;
      this.NPC.frame.Y = (int) (this.NPC.frameCounter / num);
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[10], -1);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 499;

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<LifelightBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<LifelightTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<LifelightRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<LifelightMasterPet>(), 4));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Conditions.NotExpert());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.OneFromOptions(1, new int[4]
      {
        ModContent.ItemType<EnchantedLifeblade>(),
        ModContent.ItemType<Lightslinger>(),
        ModContent.ItemType<CrystallineCongregation>(),
        ModContent.ItemType<KamikazePixieStaff>()
      }), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(3986, 1, 1, 5), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(520, 1, 1, 3), false);
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, ItemDropRule.Common(501, 1, 15, 25), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public bool FlightCheck()
    {
      if (WorldSavingSystem.MasochistModeReal || ++this.flyTimer >= (WorldSavingSystem.EternityMode ? 90 : 120))
        return false;
      this.FlyingState(WorldSavingSystem.EternityMode ? 1.2f : 0.8f);
      return true;
    }

    public void P1stateReset()
    {
      this.AI_Timer = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.AttackF1 = true;
      this.NPC.netUpdate = true;
    }

    public void RandomizeP1state()
    {
      if (FargoSoulsUtil.HostCheck)
      {
        this.P1state = Main.rand.Next(this.P1statecount);
        if (this.P1state == this.oldP1state)
          this.P1state = (this.P1state + 1) % this.P1statecount;
      }
      if (this.NPC.life < this.P2Threshold)
      {
        this.P1state = -1;
        this.flyTimer = 9000;
      }
      this.NPC.netUpdate = true;
    }

    public void StateReset()
    {
      this.NPC.TargetClosest(true);
      this.NPC.netUpdate = true;
      this.RandomizeState();
      this.AI_Timer = 0.0f;
      this.NPC.ai[2] = 0.0f;
      this.NPC.ai[3] = 0.0f;
      this.NPC.ai[0] = 0.0f;
      this.NPC.localAI[1] = 0.0f;
      this.AttackF1 = true;
    }

    public void RandomizeState()
    {
      this.NPC.netUpdate = true;
      if (!this.PhaseOne && this.NPC.life < this.SansThreshold)
      {
        this.state = 101;
        this.oldstate = -665;
      }
      else
      {
        List<int> doableStates = GetDoableStates();
        if (FargoSoulsUtil.HostCheck)
        {
          this.state = Utils.NextFromCollection<int>(Main.rand, doableStates);
          this.availablestates.Remove(this.state);
        }
        this.Variant = Utils.NextBool(Main.rand);
      }

      List<int> GetDoableStates()
      {
        HashSet<int> second = new HashSet<int>();
        float num = 4000f;
        if (this.NPC.target.IsWithinBounds((int) byte.MaxValue))
        {
          Player player = Main.player[this.NPC.target];
          if (player != null && player.Alive())
            num = ((Entity) this.NPC).Distance(((Entity) player).Center);
        }
        if (this.state == 4 || this.state == 2)
        {
          second.Add(4);
          second.Add(2);
        }
        if ((double) num < 550.0)
        {
          second.Add(8);
          second.Add(7);
        }
        if ((double) num >= 550.0)
        {
          second.Add(0);
          second.Add(1);
        }
        List<int> doableStates = this.availablestates.Except<int>((IEnumerable<int>) second).ToList<int>();
        if (doableStates.Count < 1)
        {
          this.availablestates.Clear();
          for (int index = 0; index < this.statecount; ++index)
            this.availablestates.Add(index);
          doableStates = GetDoableStates();
        }
        return doableStates;
      }
    }

    private static Vector4 RotateByMatrix(Vector4 obj, float radians, Vector3 axis)
    {
      Vector3 vector3 = Vector3.Transform(Vector3.op_Addition(Vector3.op_Addition(Vector3.op_Multiply(obj.X, Vector3.UnitX), Vector3.op_Multiply(obj.Y, Vector3.UnitY)), Vector3.op_Multiply(obj.Z, Vector3.UnitZ)), !Vector3.op_Equality(axis, Vector3.UnitX) ? (!Vector3.op_Equality(axis, Vector3.UnitY) ? Matrix.CreateRotationZ(radians) : Matrix.CreateRotationY(radians)) : Matrix.CreateRotationX(radians));
      obj.X = vector3.X;
      obj.Y = vector3.Y;
      obj.Z = vector3.Z;
      return obj;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct Formations
    {
      public const int Circle = 0;
      public const int Spear = 1;
      public const int Gun = 2;
    }

    public enum P1States
    {
      Opening = -2, // 0xFFFFFFFE
      P1Transition = -1, // 0xFFFFFFFF
      P1ShotSpam = 0,
      P1Nuke = 1,
      P1Mines = 2,
      P1Pixies = 3,
      P1RuneExpand = 4,
      P1ReactionShotgun = 5,
    }

    public enum P2States
    {
      SlurpBurp = 0,
      RuneExpand = 1,
      Charge = 2,
      Plunge = 3,
      Pixies = 4,
      Roulette = 5,
      ReactionShotgun = 6,
      RunningMinigun = 7,
      Shotgun = 8,
      TeleportNukes = 9,
      Final = 101, // 0x00000065
    }

    internal struct Rune
    {
      public Vector3 Center;
      public int Index;
      public float Scale;
      public float Rotation;

      public Rune(Vector3 center, int index, float scale, float rotation)
      {
        this.Center = center;
        this.Index = index;
        this.Scale = scale;
        this.Rotation = rotation;
      }
    }
  }
}
