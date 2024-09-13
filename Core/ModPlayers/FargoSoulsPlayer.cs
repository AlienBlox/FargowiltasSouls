// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ModPlayers.FargoSoulsPlayer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs;
using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Content.UI;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.Systems;
using FargowiltasSouls.Core.Toggler;
using Luminance.Core.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Default;
using Terraria.ModLoader.IO;
using Terraria.UI;

#nullable disable
namespace FargowiltasSouls.Core.ModPlayers
{
  public class FargoSoulsPlayer : ModPlayer
  {
    public bool CanJungleJump;
    public bool JungleJumping;
    public int savedRocketTime;
    public int[] ApprenticeItemCDs = new int[10];
    public Mount.MountData BaseSquireMountData;
    public int BaseMountType = -1;
    public bool extraCarpetDuration = true;
    private int fastFallCD;
    private const int BASE_PARRY_WINDOW = 20;
    private const int BASE_SHIELD_COOLDOWN = 100;
    private const int HARD_PARRY_WINDOW = 10;
    private const int LONG_SHIELD_COOLDOWN = 360;
    private const int PERFECT_PARRY_WINDOW = 10;
    public Item QueenStingerItem;
    public bool EridanusSet;
    public bool EridanusEmpower;
    public int EridanusTimer;
    public bool GaiaSet;
    public bool GaiaOffense;
    public bool StyxSet;
    public int StyxMeter;
    public int StyxTimer;
    public int StyxAttackReadyTimer;
    public bool NekomiSet;
    public int NekomiMeter;
    public int NekomiTimer;
    public int NekomiHitCD;
    public int NekomiAttackReadyTimer;
    public const int SuperAttackMaxWindow = 30;
    public bool BrainMinion;
    public bool EaterMinion;
    public bool BigBrainMinion;
    public bool DukeFishron;
    public bool SquirrelMount;
    public bool SeekerOfAncientTreasures;
    public bool AccursedSarcophagus;
    public bool BabySilhouette;
    public bool BabyLifelight;
    public bool BiteSizeBaron;
    public bool Nibble;
    public bool ChibiDevi;
    public bool MutantSpawn;
    public bool BabyAbom;
    public bool PetsActive;
    public bool MahoganyCanUseDR;
    public Vector2[] PearlwoodTrail = new Vector2[30];
    public int PearlwoodIndex;
    public int PearlwoodGrace;
    public Vector2 PStarelinePos;
    public int ShadewoodCD;
    public bool WoodEnchantDiscount;
    public int CopperProcCD;
    public bool GuardRaised;
    public int ParryDebuffImmuneTime;
    public int ObsidianCD;
    public bool LavaWet;
    public float TinCritMax;
    public float TinCrit = 5f;
    public int TinProcCD;
    public bool TinCritBuffered;
    public int TungstenCD;
    public bool PearlwoodStar;
    public int AshwoodCD;
    public int MeteorTimer;
    public int MeteorCD = 60;
    public bool MeteorShower;
    public int ApprenticeCD;
    public bool IronRecipes;
    public bool CactusImmune;
    public int CactusProcCD;
    public bool ChlorophyteEnchantActive;
    public bool MonkEnchantActive;
    public bool ShinobiEnchantActive;
    public int monkTimer;
    public int PumpkinSpawnCD;
    public bool TitaniumDRBuff;
    public bool TitaniumCD;
    public bool SquireEnchantActive;
    public bool ValhallaEnchantActive;
    public bool AncientShadowEnchantActive;
    public int AncientShadowFlameCooldown;
    public int ShadowOrbRespawnTimer;
    public Item PlatinumEffect;
    public int PalladCounter;
    public int MythrilTimer;
    public bool PrimeSoulActive;
    public bool PrimeSoulActiveBuffer;
    public int PrimeSoulItemCount;
    public int CrimsonRegenAmount;
    public int CrimsonRegenTime;
    public bool CanSummonForbiddenStorm;
    public int IcicleCount;
    public int icicleCD;
    public int GladiatorCD;
    public bool GoldEnchMoveCoins;
    public bool GoldShell;
    private int goldHP;
    public int HallowHealTime;
    public int HuntressStage;
    public int HuntressCD;
    public double AdamantiteSpread;
    public bool CanCobaltJump;
    public bool JustCobaltJumped;
    public int CobaltCooldownTimer;
    public int CobaltImmuneTimer;
    public bool ApprenticeEnchantActive;
    public bool DarkArtistEnchantActive;
    public int BeeCD;
    public int JungleCD;
    public int BeetleEnchantDefenseTimer;
    public int BorealCD;
    public bool CrystalEnchantActive;
    public int MonkDashing;
    public int NecroCD;
    public Projectile CrystalSmokeBombProj;
    public bool FirstStrike;
    public int SmokeBombCD;
    public int RedRidingArrowCD;
    public int DashCD;
    public bool SnowVisual;
    public int SpectreCD;
    public bool MinionCrits;
    public bool FreezeTime;
    public int freezeLength;
    public bool ChillSnowstorm;
    public int chillLength;
    public int TurtleCounter;
    public int TurtleShellHP = 25;
    public int turtleRecoverCD = 240;
    public bool ShellHide;
    public int ValhallaDashCD;
    public bool VortexStealth;
    public bool WizardEnchantActive;
    public bool WizardTooltips;
    public Item WizardedItem;
    public int CritterAttackTimer;
    public HashSet<int> ForceEffects = new HashSet<int>();
    public bool MeleeSoul;
    public bool MagicSoul;
    public bool RangedSoul;
    public bool SummonSoul;
    public bool ColossusSoul;
    public bool SupersonicSoul;
    public bool WorldShaperSoul;
    public bool FlightMasterySoul;
    public bool RangedEssence;
    public bool BuilderMode;
    public bool UniverseSoul;
    public bool UniverseCore;
    public bool FishSoul1;
    public bool FishSoul2;
    public bool TerrariaSoul;
    public bool VoidSoul;
    public int HealTimer;
    public int HurtTimer;
    public bool Eternity;
    public float TinEternityDamage;
    public Item SlimyShieldItem;
    public bool SlimyShieldFalling;
    public int AgitatingLensCD;
    public Item DarkenedHeartItem;
    public int DarkenedHeartCD;
    public int GuttedHeartCD = 60;
    public Item NecromanticBrewItem;
    public float NecromanticBrewRotation;
    public int IsDashingTimer;
    public bool DeerSinewNerf;
    public int DeerSinewFreezeCD;
    public bool PureHeart;
    public bool PungentEyeballMinion;
    public bool CrystalSkullMinion;
    public int WyvernBallsCD;
    public bool FusedLens;
    public bool FusedLensCanDebuff;
    public bool Supercharged;
    public bool Probes;
    public bool MagicalBulb;
    public bool PlanterasChild;
    public bool SkullCharm;
    public bool PungentEyeball;
    public bool LumpOfFlesh;
    public Item LihzahrdTreasureBoxItem;
    public int GroundPound;
    public Item BetsysHeartItem;
    public bool BetsyDashing;
    public int SpecialDashCD;
    public bool MutantAntibodies;
    public Item GravityGlobeEXItem;
    public int AdditionalAttacksTimer;
    public bool MoonChalice;
    public bool LunarCultist;
    public bool TrueEyes;
    public Item AbomWandItem;
    public int AbomWandCD;
    public bool MasochistSoul;
    public Item MasochistSoulItem;
    public bool MasochistHeart;
    public bool MutantsPactSlot;
    public bool HasClickedWrench;
    public bool SandsofTime;
    public bool SecurityWallet;
    public Item FrigidGemstoneItem;
    public int FrigidGemstoneCD;
    public int WretchedPouchCD;
    public bool NymphsPerfume;
    public bool NymphsPerfumeRespawn;
    public int NymphsPerfumeRestoreLife;
    public int NymphsPerfumeCD = 30;
    public bool RainbowSlime;
    public bool SkeletronArms;
    public bool IceQueensCrown;
    public bool CirnoGraze;
    public bool MiniSaucer;
    public bool CanAmmoCycle;
    public bool TribalCharm;
    public bool TribalCharmEquipped;
    public bool TribalCharmClickBonus;
    public bool SupremeDeathbringerFairy;
    public bool GodEaterImbue;
    public Item MutantSetBonusItem;
    public bool AbomMinion;
    public bool PhantasmalRing;
    public bool MutantsDiscountCard;
    public bool MutantsCreditCard;
    public bool DeerSinew;
    public bool RabiesVaccine;
    public bool TwinsEX;
    public bool TimsConcoction;
    public bool ReceivedMasoGift;
    public bool DeviGraze;
    public bool Graze;
    public float GrazeRadius;
    public int DeviGrazeCounter;
    public int CirnoGrazeCounter;
    public double DeviGrazeBonus;
    public Item DevianttHeartItem;
    public int DevianttHeartsCD;
    public Item MutantEyeItem;
    public bool MutantEyeVisual;
    public int MutantEyeCD;
    public bool AbominableWandRevived;
    public bool AbomRebirth;
    public bool WasHurtBySomething;
    public bool PrecisionSeal;
    public bool PrecisionSealNoDashNoJump;
    public Item GelicWingsItem;
    public bool ConcentratedRainbowMatter;
    public bool Hexed;
    public bool Unstable;
    private int unstableCD;
    public bool Fused;
    public bool Shadowflame;
    public bool Oiled;
    public bool DeathMarked;
    public bool Hypothermia;
    public bool noDodge;
    public bool noSupersonic;
    public bool NoMomentum;
    public bool Bloodthirsty;
    public bool Unlucky;
    public bool DisruptedFocus;
    public bool Smite;
    public bool Anticoagulation;
    public bool GodEater;
    public bool FlamesoftheUniverse;
    public bool MutantNibble;
    public int StatLifePrevious = -1;
    public bool Asocial;
    public bool WasAsocial;
    public bool HidePetToggle0 = true;
    public bool HidePetToggle1 = true;
    public bool Kneecapped;
    public bool Defenseless;
    public bool Purified;
    public bool Infested;
    public int MaxInfestTime;
    public bool FirstInfection = true;
    public float InfestedDust;
    public bool Rotting;
    public bool SqueakyToy;
    public bool Atrophied;
    public bool Jammed;
    public bool Slimed;
    public byte lightningRodTimer;
    public bool ReverseManaFlow;
    public bool CurseoftheMoon;
    public bool OceanicMaul;
    public int MaxLifeReduction;
    public int CurrentLifeReduction;
    public int LifeReductionUpdateTimer;
    public bool Midas;
    public bool HadMutantPresence;
    public bool MutantPresence;
    public int PresenceTogglerTimer;
    public bool MutantFang;
    public bool DevianttPresence;
    public bool Swarming;
    public bool LowGround;
    public bool Flipped;
    public bool Mash;
    public bool[] MashPressed = new bool[4];
    public int MashCounter;
    public int StealingCooldown;
    public bool LihzahrdCurse;
    public bool Berserked;
    public bool CerebralMindbreak;
    public bool NanoInjection;
    public bool Stunned;
    public bool HaveCheckedAttackSpeed;
    public bool HasJungleRose;
    public int ReallyAwfulDebuffCooldown;
    public bool BoxofGizmos;
    public bool OxygenTank;
    public int DreadShellVulnerabilityTimer;
    public int shieldTimer;
    public int shieldCD;
    public int shieldHeldTime;
    public bool wasHoldingShield;
    public int LightslingerHitShots;
    public int ChargeSoundDelay;
    public int NoUsingItems;
    public bool HasDash;
    private DashManager.DashType fargoDash;
    public bool CanShinobiTeleport;
    public int WeaponUseTimer;
    public ToggleBackend Toggler = new ToggleBackend();
    public Dictionary<AccessoryEffect, bool> TogglesToSync = new Dictionary<AccessoryEffect, bool>();
    public List<AccessoryEffect> disabledToggles = new List<AccessoryEffect>();
    public List<BaseEnchant> EquippedEnchants = new List<BaseEnchant>();
    public bool IsStandingStill;
    public float AttackSpeed;
    public float WingTimeModifier = 1f;
    public bool FreeEaterSummon = true;
    public bool RustRifleReloading;
    public float RustRifleReloadZonePos;
    public float RustRifleTimer;
    public int RockeaterDistance = 600;
    public int The22Incident;
    public Dictionary<int, bool> KnownBuffsToPurify = new Dictionary<int, bool>();
    public bool Toggler_ExtraAttacksDisabled;
    public bool Toggler_MinionsDisabled;
    public int ToggleRebuildCooldown;
    internal List<int> prevDyes;
    public int frameCounter;
    public int frameSnow = 1;
    public int frameMutantAura;

    public void GoldKey()
    {
      if (!this.Player.HasBuff(ModContent.BuffType<GoldenStasisBuff>()) && !this.Player.HasBuff(ModContent.BuffType<GoldenStasisCDBuff>()))
      {
        int num = 300;
        if (this.ForceEffect<GoldEnchant>())
          num *= 2;
        this.Player.AddBuff(ModContent.BuffType<GoldenStasisBuff>(), num, true, false);
        this.Player.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 3600, true, false);
        this.goldHP = this.Player.statLife;
        if (Main.dedServ)
          return;
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Zhonyas", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      }
      else
        this.Player.ClearBuff(ModContent.BuffType<GoldenStasisBuff>());
    }

    public void GoldUpdate()
    {
      this.Player.immune = true;
      this.Player.immuneTime = 90;
      this.Player.hurtCooldowns[0] = 90;
      this.Player.hurtCooldowns[1] = 90;
      this.Player.stealth = 1f;
      if (this.Player.statLife < this.goldHP)
        this.Player.statLife = this.goldHP;
      if (this.Player.ownedProjectileCounts[ModContent.ProjectileType<GoldShellProj>()] > 0)
        return;
      Projectile.NewProjectile(((Entity) this.Player).GetSource_Misc(""), ((Entity) this.Player).Center.X, ((Entity) this.Player).Center.Y, 0.0f, 0.0f, ModContent.ProjectileType<GoldShellProj>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
    }

    private int GetNumSentries()
    {
      int numSentries = 0;
      for (int index = 0; index < Main.maxProjectiles; ++index)
      {
        Projectile projectile = Main.projectile[index];
        if (((Entity) projectile).active && projectile.owner == ((Entity) this.Player).whoAmI && projectile.sentry)
          ++numSentries;
      }
      return numSentries;
    }

    public void OnLandingEffects()
    {
      if (this.SlimyShieldFalling)
      {
        if ((double) ((Entity) this.Player).velocity.Y < 0.0)
          this.SlimyShieldFalling = false;
        if ((double) ((Entity) this.Player).velocity.Y != 0.0)
          return;
        this.SlimyShieldFalling = false;
        if (((Entity) this.Player).whoAmI != Main.myPlayer || (double) this.Player.gravDir <= 0.0)
          return;
        if (this.SlimyShieldItem != null)
        {
          SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
          Vector2 mouseWorld = Main.MouseWorld;
          int num1 = 8;
          if (this.SupremeDeathbringerFairy)
            num1 = 16;
          if (this.MasochistSoul)
            num1 = 80;
          int num2 = (int) ((double) num1 * (double) this.Player.ActualClassDamage(DamageClass.Melee));
          for (int index = 0; index < 3; ++index)
          {
            Vector2 vector2_1;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_1).\u002Ector(mouseWorld.X + (float) Main.rand.Next(-200, 201), mouseWorld.Y - (float) Main.rand.Next(600, 901));
            if (Collision.CanHitLine(mouseWorld, 0, 0, vector2_1, 0, 0))
            {
              Vector2 vector2_2 = Vector2.op_Subtraction(mouseWorld, vector2_1);
              ((Vector2) ref vector2_2).Normalize();
              Vector2 vector2_3 = Vector2.op_Multiply(vector2_2, 10f);
              Projectile.NewProjectile(this.Player.GetSource_Accessory(this.SlimyShieldItem, "SlimyShield"), vector2_1, vector2_3, ModContent.ProjectileType<SlimeBall>(), num2, 1f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
        }
        if (this.GelicWingsItem == null || !this.Player.HasEffect<GelicWingSpikes>())
          return;
        int num = 60;
        for (int index1 = -1; index1 <= 1; index1 += 2)
        {
          Vector2 vector2_4 = Utils.RotatedBy(Vector2.UnitX, (double) MathHelper.ToRadians((float) (-10 * index1)), new Vector2());
          for (int index2 = 0; index2 < 8; ++index2)
          {
            Vector2 vector2_5 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 5f, 10f) * (float) index1, Utils.RotatedBy(vector2_4, -0.078539818525314331 * (double) index2 * (double) index1, new Vector2()));
            Projectile.NewProjectile(this.Player.GetSource_Accessory(this.GelicWingsItem, (string) null), Vector2.op_Subtraction(((Entity) this.Player).Bottom, Vector2.op_Multiply(Vector2.UnitY, 8f)), vector2_5, ModContent.ProjectileType<GelicWingSpike>(), num, 5f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
        }
      }
      else
      {
        if ((double) ((Entity) this.Player).velocity.Y <= 3.0)
          return;
        this.SlimyShieldFalling = true;
      }
    }

    public void DarkenedHeartAttack(Projectile projectile)
    {
      if (this.DarkenedHeartCD > 0)
        return;
      this.DarkenedHeartCD = 60;
      if (!this.Player.HasEffect<DarkenedHeartEaters>() || projectile != null && projectile.type == 307)
        return;
      SoundEngine.PlaySound(ref SoundID.NPCHit1, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].scale *= 1.1f;
        Main.dust[index2].noGravity = true;
      }
      for (int index3 = 0; index3 < 30; ++index3)
      {
        int index4 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 184, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index4];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2.5f);
        Main.dust[index4].scale *= 0.8f;
        Main.dust[index4].noGravity = true;
      }
      int num1 = 2;
      if (Utils.NextBool(Main.rand, 3))
        ++num1;
      if (Utils.NextBool(Main.rand, 6))
        ++num1;
      if (Utils.NextBool(Main.rand, 9))
        ++num1;
      int num2 = this.PureHeart ? 45 : 18;
      if (this.MasochistSoul)
        num2 *= 2;
      for (int index = 0; index < num1; ++index)
        Projectile.NewProjectile(this.Player.GetSource_Accessory(this.DarkenedHeartItem, (string) null), ((Entity) this.Player).Center.X, ((Entity) this.Player).Center.Y, (float) ((double) Main.rand.Next(-35, 36) * 0.019999999552965164 * 10.0), (float) ((double) Main.rand.Next(-35, 36) * 0.019999999552965164 * 10.0), 307, (int) ((double) num2 * (double) this.Player.ActualClassDamage(DamageClass.Melee)), 1.75f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
    }

    public void FrigidGemstoneKey()
    {
      if (this.FrigidGemstoneCD > 0 || !this.Player.CheckMana(6, true, false))
        return;
      this.FrigidGemstoneCD = 10;
      this.Player.manaRegenDelay = Math.Max(this.Player.manaRegenDelay, 30f);
      SoundEngine.PlaySound(ref SoundID.Item28, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      int num = (int) (14.0 * (double) this.Player.ActualClassDamage(DamageClass.Magic));
      if (!Main.hardMode)
        num = 0;
      Projectile.NewProjectile(this.Player.GetSource_Accessory(this.FrigidGemstoneItem, (string) null), ((Entity) this.Player).Center, Vector2.op_Multiply(12f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Player, Main.MouseWorld)), 80, num, 2f, ((Entity) this.Player).whoAmI, (float) Player.tileTargetX, (float) Player.tileTargetY, 0.0f);
    }

    public void SpecialDashKey()
    {
      if (this.SpecialDashCD > 0)
        return;
      this.SpecialDashCD = Luminance.Common.Utilities.Utilities.SecondsToFrames(5f);
      if (((Entity) this.Player).whoAmI != Main.myPlayer)
        return;
      this.Player.RemoveAllGrapplingHooks();
      this.Player.controlUseItem = false;
      this.Player.controlUseTile = false;
      this.Player.controlHook = false;
      this.Player.controlMount = false;
      this.Player.itemAnimation = 0;
      this.Player.itemTime = 0;
      this.Player.reuseDelay = 0;
      if (this.BetsysHeartItem != null)
      {
        Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Player, Main.MouseWorld), 25f);
        Projectile.NewProjectile(this.Player.GetSource_Accessory(this.BetsysHeartItem, (string) null), ((Entity) this.Player).Center, vector2, ModContent.ProjectileType<BetsyDash>(), (int) (100.0 * (double) this.Player.ActualClassDamage(DamageClass.Melee)), 6f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
        this.Player.immune = true;
        this.Player.immuneTime = Math.Max(this.Player.immuneTime, 2);
        this.Player.hurtCooldowns[0] = Math.Max(this.Player.hurtCooldowns[0], 2);
        this.Player.hurtCooldowns[1] = Math.Max(this.Player.hurtCooldowns[1], 2);
        foreach (int debuffId in FargowiltasSouls.FargowiltasSouls.DebuffIDs)
        {
          if (!this.Player.HasBuff(debuffId))
            this.Player.buffImmune[debuffId] = true;
        }
      }
      else if (this.QueenStingerItem != null)
      {
        this.SpecialDashCD += Luminance.Common.Utilities.Utilities.SecondsToFrames(1f);
        Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.Player, Main.MouseWorld), 20f);
        Projectile.NewProjectile(this.Player.GetSource_Accessory(this.QueenStingerItem, (string) null), ((Entity) this.Player).Center, vector2, ModContent.ProjectileType<BeeDash>(), (int) (44.0 * (double) this.Player.ActualClassDamage(DamageClass.Melee)), 6f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      this.Player.AddBuff(ModContent.BuffType<BetsyDashBuff>(), 20, true, false);
    }

    public bool TryCleanseDebuffs()
    {
      bool flag = false;
      int length = this.Player.buffType.Length;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int num = this.Player.buffTime[index1];
        if (num > 0)
        {
          int index2 = this.Player.buffType[index1];
          if (index2 > 0 && num > 5 && Main.debuff[index2] && !Main.buffNoTimeDisplay[index2] && !BuffID.Sets.NurseCannotRemoveDebuff[index2])
          {
            this.Player.DelBuff(index1);
            flag = true;
            --index1;
            --length;
          }
        }
      }
      return flag;
    }

    public void MagicalBulbKey()
    {
      if (this.Player.HasBuff(ModContent.BuffType<MagicalCleanseCDBuff>()) || !this.TryCleanseDebuffs())
        return;
      int num = 40;
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && !Main.npc[index].friendly && Main.npc[index].lifeMax > 5)
          Main.npc[index].AddBuff(ModContent.BuffType<MagicalCurseBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames((float) (num + 5)), false);
      }
      this.Player.AddBuff(ModContent.BuffType<MagicalCleanseCDBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames((float) num), true, false);
      SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      for (int index1 = 0; index1 < 50; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, Utils.NextBool(Main.rand) ? 107 : 157, 0.0f, 0.0f, 0, new Color(), 3f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
      }
    }

    public void BombKey()
    {
      if (this.MutantEyeItem != null && this.MutantEyeCD <= 0)
      {
        this.MutantEyeCD = 3600;
        if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        this.Player.immune = true;
        this.Player.immuneTime = 90;
        this.Player.hurtCooldowns[0] = 90;
        this.Player.hurtCooldowns[1] = 90;
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 100; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 30f), (double) (index1 - 49) * 6.2831854820251465 / 100.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity = vector2_2;
        }
        for (int index3 = 0; index3 < 50; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index4].noGravity = true;
          Main.dust[index4].noLight = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 24f);
        }
        FargoSoulsUtil.ClearHostileProjectiles(1);
        int damage2 = (int) (1700.0 * (double) this.Player.ActualClassDamage(DamageClass.Magic));
        SpawnSphereRing(24, 12f, damage2, -1f);
        SpawnSphereRing(24, 12f, damage2, 1f);
      }
      else
      {
        if (!this.CirnoGraze)
          return;
        Projectile projectile = ((IEnumerable<Projectile>) Main.projectile).FirstOrDefault<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.friendly && p.owner == ((Entity) this.Player).whoAmI && p.type == ModContent.ProjectileType<CirnoBomb>()));
        if (projectile == null)
          return;
        projectile.ai[0] = 1f;
        projectile.netUpdate = true;
        this.CirnoGrazeCounter = 0;
      }

      void SpawnSphereRing(int ringMax, float speed, int damage2, float rotationModifier)
      {
        float num1 = 6.28318548f / (float) ringMax;
        Vector2 vector2 = Vector2.op_Multiply(Vector2.UnitY, speed);
        int num2 = ModContent.ProjectileType<PhantasmalSphereRing>();
        for (int index = 0; index < ringMax; ++index)
        {
          vector2 = Utils.RotatedBy(vector2, (double) num1, new Vector2());
          Projectile.NewProjectile(this.Player.GetSource_Accessory(this.MutantEyeItem, (string) null), ((Entity) this.Player).Center, vector2, num2, damage2, 3f, Main.myPlayer, rotationModifier * (float) ((Entity) this.Player).direction, speed, 0.0f);
        }
      }
    }

    public void DebuffInstallKey()
    {
      if (this.Player.HasEffect<AgitatingLensInstall>() && this.Player.controlUp && this.Player.controlDown)
      {
        if (this.Player.HasBuff(ModContent.BuffType<BerserkerInstallBuff>()) || this.Player.HasBuff(ModContent.BuffType<BerserkerInstallCDBuff>()))
          return;
        SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        this.Player.AddBuff(ModContent.BuffType<BerserkerInstallBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames(8f), true, false);
        for (int index1 = 0; index1 < 60; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 60, 0.0f, 0.0f, 0, new Color(), 3f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 9f);
        }
      }
      else
      {
        if (this.Player.HasEffect<FusedLensInstall>())
        {
          int num1 = ModContent.BuffType<TwinsInstallBuff>();
          if (this.Player.HasBuff(num1))
          {
            this.Player.ClearBuff(num1);
          }
          else
          {
            SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
            this.Player.AddBuff(num1, 2, true, false);
            int num2 = 60;
            for (int index3 = 0; index3 < num2; ++index3)
            {
              float num3 = 3f;
              int index4 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, Utils.NextBool(Main.rand) ? 90 : 89, 0.0f, 0.0f, 0, new Color(), num3);
              Main.dust[index4].noGravity = true;
              Dust dust = Main.dust[index4];
              dust.velocity = Vector2.op_Multiply(dust.velocity, num3 * 3f);
            }
          }
        }
        if (this.Player.HasEffect<WretchedPouchEffect>())
        {
          int num4 = ModContent.BuffType<WretchedHexBuff>();
          if (this.Player.HasBuff(num4))
          {
            this.Player.ClearBuff(num4);
          }
          else
          {
            SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
            this.Player.AddBuff(num4, 2, true, false);
            int num5 = 60;
            for (int index5 = 0; index5 < num5; ++index5)
            {
              float num6 = 3f;
              int index6 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 27, 0.0f, 0.0f, 0, new Color(), num6);
              Main.dust[index6].noGravity = true;
              Dust dust = Main.dust[index6];
              dust.velocity = Vector2.op_Multiply(dust.velocity, num6 * 3f);
            }
          }
        }
        if (!this.Player.HasEffect<PearlwoodEffect>())
          return;
        int num7 = ModContent.BuffType<PearlwoodStarBuff>();
        if (this.Player.HasBuff(num7))
        {
          this.Player.ClearBuff(num7);
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
          this.Player.AddBuff(num7, 2, true, false);
          int num8 = 60;
          for (int index7 = 0; index7 < num8; ++index7)
          {
            float num9 = 3f;
            int index8 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, (int) byte.MaxValue, 0.0f, 0.0f, 0, new Color(), num9);
            Main.dust[index8].noGravity = true;
            Dust dust = Main.dust[index8];
            dust.velocity = Vector2.op_Multiply(dust.velocity, num9 * 3f);
          }
        }
      }
    }

    public void AmmoCycleKey()
    {
      SoundEngine.PlaySound(ref SoundID.Unlock, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      for (int index1 = 54; index1 <= 56; ++index1)
      {
        int index2 = index1 + 1;
        Item[] inventory1 = this.Player.inventory;
        int index3 = index2;
        Item[] inventory2 = this.Player.inventory;
        int num = index1;
        Item obj1 = this.Player.inventory[index1];
        Item obj2 = this.Player.inventory[index2];
        inventory1[index3] = obj1;
        int index4 = num;
        Item obj3 = obj2;
        inventory2[index4] = obj3;
      }
    }

    public void TryFastfallUpdate()
    {
      if (this.fastFallCD > 0)
        --this.fastFallCD;
      if (!((double) this.Player.gravDir > 0.0 & (this.Player.HasEffect<LihzahrdGroundPound>() || this.Player.HasEffect<DeerclawpsDive>())))
        return;
      if (this.fastFallCD <= 0 && !this.Player.mount.Active && this.Player.controlDown && this.Player.releaseDown && !this.Player.controlJump && this.Player.doubleTapCardinalTimer[0] > 0 && this.Player.doubleTapCardinalTimer[0] != 15 && (double) ((Entity) this.Player).velocity.Y != 0.0)
      {
        if ((double) ((Entity) this.Player).velocity.Y < 15.0)
          ((Entity) this.Player).velocity.Y = 15f;
        if (this.GroundPound <= 0)
          this.GroundPound = 1;
      }
      if (this.GroundPound <= 0)
        return;
      this.fastFallCD = 60;
      if ((double) ((Entity) this.Player).velocity.Y == 0.0 && this.Player.controlDown)
      {
        Vector2 vector2 = Collision.TileCollision(((Entity) this.Player).position, Vector2.op_Multiply(15f, Vector2.UnitY), ((Entity) this.Player).width, ((Entity) this.Player).height, true, true, (int) this.Player.gravDir);
        if (Vector2.op_Inequality(vector2, Vector2.Zero))
        {
          Player player = this.Player;
          ((Entity) player).position = Vector2.op_Addition(((Entity) player).position, vector2);
          ((Entity) this.Player).velocity.Y = 15f;
        }
      }
      if ((double) ((Entity) this.Player).velocity.Y < 0.0 || this.Player.mount.Active || this.Player.controlJump && !this.Player.controlDown)
        this.GroundPound = 0;
      else if ((double) ((Entity) this.Player).velocity.Y == 0.0 && (double) ((Entity) this.Player).oldVelocity.Y == 0.0)
      {
        int num1 = (int) ((Entity) this.Player).Center.X / 16;
        int num2 = (int) ((double) ((Entity) this.Player).position.Y + (double) ((Entity) this.Player).height + 8.0) / 16;
        if (num1 < 0 || num1 >= Main.maxTilesX || num2 < 0 || num2 >= Main.maxTilesY || !Tile.op_Inequality(((Tilemap) ref Main.tile)[num1, num2], (ArgumentException) null))
          return;
        Tile tile = ((Tilemap) ref Main.tile)[num1, num2];
        if (!((Tile) ref tile).HasUnactuatedTile)
          return;
        bool[] tileSolid1 = Main.tileSolid;
        tile = ((Tilemap) ref Main.tile)[num1, num2];
        int index1 = (int) ((Tile) ref tile).TileType;
        if (!tileSolid1[index1])
          return;
        this.GroundPound = 0;
        if (this.Player.HasEffect<DeerclawpsDive>())
          DeerclawpsDive.DeerclawpsLandingSpikes(this.Player, ((Entity) this.Player).Bottom);
        if (!this.Player.HasEffect<LihzahrdBoulders>())
          return;
        if (!Main.dedServ)
          ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
        if (((Entity) this.Player).whoAmI != Main.myPlayer)
          return;
        int num3 = 500;
        if (this.MasochistSoul)
          num3 *= 3;
        Projectile.NewProjectile(this.Player.GetSource_Accessory(this.LihzahrdTreasureBoxItem, (string) null), ((Entity) this.Player).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), 0, 0.0f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
        int index2 = Projectile.NewProjectile(this.Player.GetSource_Accessory(this.LihzahrdTreasureBoxItem, (string) null), ((Entity) this.Player).Center, Vector2.Zero, ModContent.ProjectileType<Explosion>(), (int) ((double) (num3 * 2) * (double) this.Player.ActualClassDamage(DamageClass.Melee)), 9f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
        if (index2 != Main.maxProjectiles)
          Main.projectile[index2].DamageType = DamageClass.Melee;
        int num4 = num3;
        for (int index3 = -5; index3 <= 5; index3 += 2)
          Projectile.NewProjectile(this.Player.GetSource_Accessory(this.LihzahrdTreasureBoxItem, (string) null), ((Entity) this.Player).Center, Vector2.op_Multiply(-10f, Utils.RotatedBy(Vector2.UnitY, 0.2617993950843811 * (double) index3, new Vector2())), ModContent.ProjectileType<LihzahrdBoulderFriendly>(), (int) ((double) num4 * (double) this.Player.ActualClassDamage(DamageClass.Melee)), 7.5f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
        int num5 = (int) ((double) (num3 / 2) * (double) this.Player.ActualClassDamage(DamageClass.Melee));
        if (this.MasochistSoul)
          num5 *= 3;
        int num6 = num2 - 2;
        for (int index4 = -3; index4 <= 3; ++index4)
        {
          if (index4 != 0)
          {
            int num7 = num1 + 16 * index4;
            int num8 = num6;
            if (Tile.op_Inequality(((Tilemap) ref Main.tile)[num7, num8], (ArgumentException) null) && num7 >= 0 && num7 < Main.maxTilesX)
            {
              for (; Tile.op_Inequality(((Tilemap) ref Main.tile)[num7, num8], (ArgumentException) null) && num8 >= 0 && num8 < Main.maxTilesY; ++num8)
              {
                tile = ((Tilemap) ref Main.tile)[num7, num8];
                if (!((Tile) ref tile).HasUnactuatedTile)
                  continue;
                bool[] tileSolid2 = Main.tileSolid;
                tile = ((Tilemap) ref Main.tile)[num7, num8];
                int index5 = (int) ((Tile) ref tile).TileType;
                if (tileSolid2[index5])
                  break;
              }
              Projectile.NewProjectile(this.Player.GetSource_Accessory(this.LihzahrdTreasureBoxItem, (string) null), (float) (num7 * 16 + 8), (float) (num8 * 16 + 8), 0.0f, -8f, ModContent.ProjectileType<GeyserFriendly>(), num5, 8f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
            }
          }
        }
      }
      else
      {
        if (this.Player.controlDown && (double) ((Entity) this.Player).velocity.Y < 15.0)
          ((Entity) this.Player).velocity.Y = 15f;
        this.Player.maxFallSpeed = 15f;
        ++this.GroundPound;
        if (this.Player.HasEffect<LihzahrdGroundPound>())
        {
          for (int index6 = 0; index6 < 5; ++index6)
          {
            int index7 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 6, 0.0f, 0.0f, 0, new Color(), 1.5f);
            Main.dust[index7].noGravity = true;
            Dust dust = Main.dust[index7];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
          }
        }
        if (!this.Player.HasEffect<DeerclawpsDive>())
          return;
        for (int index8 = 0; index8 < 5; ++index8)
        {
          int index9 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, this.LumpOfFlesh ? 296 : 135, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index9].noGravity = true;
          Dust dust = Main.dust[index9];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 0.2f);
        }
      }
    }

    public void AbomWandUpdate()
    {
      if (!this.AbominableWandRevived)
        return;
      if (this.Player.statLife >= this.Player.statLifeMax2)
      {
        this.AbominableWandRevived = false;
        SoundEngine.PlaySound(ref SoundID.Item28, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        FargoSoulsUtil.DustRing(((Entity) this.Player).Center, 50, 87, 8f, new Color(), 2f);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
        }
      }
      else
        this.Player.AddBuff(ModContent.BuffType<AbomCooldownBuff>(), 2, true, false);
    }

    public void DreadParryCounter()
    {
      SoundEngine.PlaySound(ref SoundID.Item119, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      if (((Entity) this.Player).whoAmI != Main.myPlayer)
        return;
      int projDamage = FargoSoulsUtil.HighestDamageTypeScaling(this.Player, 1000);
      for (int index1 = 0; index1 < 20; ++index1)
      {
        SharpTears(((Entity) this.Player).Center, Vector2.op_Multiply(16f, Utils.RotatedBy(Vector2.UnitX, 0.31415927410125732 * ((double) index1 + (double) Utils.NextFloat(Main.rand, -0.5f, 0.5f)), new Vector2())));
        for (int index2 = 0; index2 < 6; ++index2)
        {
          Vector2 bottom = ((Entity) this.Player).Bottom;
          bottom.X += Utils.NextFloat(Main.rand, -256f, 256f);
          bool flag = false;
          for (int index3 = 0; index3 < 40; ++index3)
          {
            Tile tileSafely = Framing.GetTileSafely(bottom);
            if (((Tile) ref tileSafely).HasUnactuatedTile && (Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType]))
            {
              flag = true;
              break;
            }
            bottom.Y += 16f;
          }
          if (!flag)
            bottom.Y = ((Entity) this.Player).Bottom.Y + Utils.NextFloat(Main.rand, -64f, 64f);
          for (int index4 = 0; index4 < 40; ++index4)
          {
            Tile tileSafely = Framing.GetTileSafely(bottom);
            if (((Tile) ref tileSafely).HasUnactuatedTile && (Main.tileSolid[(int) ((Tile) ref tileSafely).TileType] || Main.tileSolidTop[(int) ((Tile) ref tileSafely).TileType]))
              bottom.Y -= 16f;
            else
              break;
          }
          if (!Collision.SolidCollision(bottom, 0, 0))
          {
            bottom.Y += 16f;
            SharpTears(bottom, Vector2.op_Multiply(16f, Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, (double) MathHelper.ToRadians(30f)))));
            break;
          }
        }
      }

      void SharpTears(Vector2 pos, Vector2 vel)
      {
        int index = Projectile.NewProjectile(this.Player.GetSource_EffectItem<DreadShellEffect>(), pos, vel, 756, projDamage, 12f, ((Entity) this.Player).whoAmI, 0.0f, Utils.NextFloat(Main.rand, 0.5f, 1f), 0.0f);
        if (index == Main.maxProjectiles)
          return;
        Main.projectile[index].DamageType = DamageClass.Default;
        Main.projectile[index].usesLocalNPCImmunity = false;
        Main.projectile[index].usesIDStaticNPCImmunity = true;
        Main.projectile[index].idStaticNPCHitCooldown = 60;
        Main.projectile[index].FargoSouls().noInteractionWithNPCImmunityFrames = true;
        Main.projectile[index].FargoSouls().DeletionImmuneRank = 1;
      }
    }

    public void PumpkingsCapeCounter(int damage)
    {
      SoundEngine.PlaySound(ref SoundID.Item62, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      if (((Entity) this.Player).whoAmI != Main.myPlayer)
        return;
      int healMultiplier = this.GetHealMultiplier(damage);
      this.Player.statLife += healMultiplier;
      if (this.Player.statLife > this.Player.statLifeMax2)
        this.Player.statLife = this.Player.statLifeMax2;
      this.Player.HealEffect(healMultiplier, true);
      int rawBaseDamage = damage;
      for (int index = 0; index < 30; ++index)
      {
        Vector2 velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), Utils.NextFloat(Main.rand, 12f, 24f));
        FargoSoulsUtil.NewSummonProjectile(this.Player.GetSource_EffectItem<PumpkingsCapeEffect>(), ((Entity) this.Player).Center, velocity, ModContent.ProjectileType<SpookyScythe>(), rawBaseDamage, 6f, ((Entity) this.Player).whoAmI);
      }
    }

    public void DeerSinewDash(int dir)
    {
      float num = 12f;
      this.DashCD = 60;
      this.Player.dashDelay = this.DashCD;
      if (this.IsDashingTimer < 15)
        this.IsDashingTimer = 15;
      ((Entity) this.Player).velocity.X = (float) dir * num;
      if (Main.netMode != 1)
        return;
      NetMessage.SendData(13, -1, -1, (NetworkText) null, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public float DeerSinewCritNerf()
    {
      return MathHelper.Lerp(1f, 0.75f, Math.Min(((Vector2) ref ((Entity) this.Player).velocity).Length() / 16f, 1f));
    }

    public void TryParryAttack(ref Player.HurtInfo hurtInfo)
    {
      bool flag1 = this.Player.HasEffect<SilverEffect>();
      bool flag2 = this.Player.HasEffect<DreadShellEffect>();
      bool flag3 = this.Player.HasEffect<PumpkingsCapeEffect>();
      if (!this.GuardRaised || this.shieldTimer <= 0 || this.Player.immune)
        return;
      this.Player.immune = true;
      int num1 = this.Player.longInvince ? 90 : 60;
      int num2 = 40;
      int num3 = 100;
      if (flag2 | flag3)
      {
        num3 = 200;
        num1 += 60;
        num2 = 360;
      }
      else if (flag1)
        num2 = 100;
      bool flag4 = this.shieldHeldTime <= 10;
      if (flag1)
      {
        if (flag4 || this.ForceEffect<SilverEnchant>())
        {
          num3 = 200;
          this.Player.AddBuff(198, 300, true, false);
          SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        }
        int num4 = flag4 ? 1 : 0;
        Projectile.NewProjectile(((Entity) this.Player).GetSource_Misc(""), ((Entity) this.Player).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, (float) num4, 0.0f, 0.0f);
      }
      int damage = Math.Min(num3, ((Player.HurtInfo) ref hurtInfo).Damage);
      int num5 = ((Player.HurtInfo) ref hurtInfo).Damage - damage;
      if (num5 < 1)
        hurtInfo.Null();
      else
        ((Player.HurtInfo) ref hurtInfo).Damage = num5;
      if (flag2 & flag4)
        this.DreadParryCounter();
      if (flag3 & flag4)
        this.PumpkingsCapeCounter(damage);
      this.Player.immuneTime = num1;
      this.Player.hurtCooldowns[0] = num1;
      this.Player.hurtCooldowns[1] = num1;
      this.ParryDebuffImmuneTime = num1;
      this.shieldCD = num1 + num2;
      foreach (int debuffId in FargowiltasSouls.FargowiltasSouls.DebuffIDs)
      {
        if (!this.Player.HasBuff(debuffId))
          this.Player.buffImmune[debuffId] = true;
      }
    }

    private void RaisedShieldEffects()
    {
      bool flag1 = this.Player.HasEffect<SilverEffect>();
      bool flag2 = this.Player.HasEffect<DreadShellEffect>();
      bool flag3 = this.Player.HasEffect<PumpkingsCapeEffect>();
      if (flag2 && !this.MasochistSoul)
        this.DreadShellVulnerabilityTimer = 60;
      if (flag3)
      {
        for (int index = 0; index < Main.maxNPCs; ++index)
        {
          if (((Entity) Main.npc[index]).active && !Main.npc[index].friendly && (double) ((Entity) Main.npc[index]).Distance(((Entity) this.Player).Center) < 300.0)
            Main.npc[index].AddBuff(ModContent.BuffType<RottingBuff>(), 600, false);
        }
        for (int index = 0; index < 20; ++index)
        {
          Vector2 vector2 = new Vector2();
          double num = Main.rand.NextDouble() * 2.0 * Math.PI;
          vector2.X += (float) (Math.Sin(num) * 300.0);
          vector2.Y += (float) (Math.Cos(num) * 300.0);
          Dust dust1 = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.Player).Center, vector2), new Vector2(4f, 4f)), 0, 0, 119, 0.0f, 0.0f, 100, Color.White, 1f)];
          dust1.velocity = ((Entity) this.Player).velocity;
          if (Utils.NextBool(Main.rand, 3))
          {
            Dust dust2 = dust1;
            dust2.velocity = Vector2.op_Addition(dust2.velocity, Vector2.op_Multiply(Vector2.Normalize(vector2), -5f));
          }
          dust1.noGravity = true;
        }
      }
      if (flag2 | flag3 && !flag1)
      {
        ((Entity) this.Player).velocity.X *= 0.85f;
        if ((double) ((Entity) this.Player).velocity.Y < 0.0)
          ((Entity) this.Player).velocity.Y *= 0.85f;
      }
      int num1 = flag2 | flag3 ? 360 : 100;
      if (this.shieldCD >= num1)
        return;
      this.shieldCD = num1;
    }

    public void UpdateShield()
    {
      bool flag1 = this.Player.HasEffect<SilverEffect>();
      bool flag2 = this.Player.HasEffect<DreadShellEffect>();
      bool flag3 = this.Player.HasEffect<PumpkingsCapeEffect>();
      this.GuardRaised = false;
      if (!flag1 && !flag2 && !flag3 || this.Player.inventory[this.Player.selectedItem].type == 3823 || this.Player.inventory[this.Player.selectedItem].type == 4760)
      {
        this.shieldTimer = 0;
        this.shieldHeldTime = 0;
        this.wasHoldingShield = false;
      }
      else
      {
        this.Player.shieldRaised = this.Player.selectedItem != 58 && this.Player.controlUseTile && !this.Player.tileInteractionHappened && this.Player.releaseUseItem && !this.Player.controlUseItem && !this.Player.mouseInterface && !CaptureManager.Instance.Active && !Main.HoveringOverAnNPC && !Main.SmartInteractShowingGenuine && this.Player.itemAnimation == 0 && this.Player.itemTime == 0 && this.Player.reuseDelay == 0 && PlayerInput.Triggers.Current.MouseRight;
        if (this.Player.shieldRaised)
        {
          this.GuardRaised = true;
          ++this.shieldHeldTime;
          for (int index = 3; index < 8 + this.Player.extraAccessorySlots; ++index)
          {
            if (this.Player.shield == -1 && this.Player.armor[index].shieldSlot != -1)
              this.Player.shield = this.Player.armor[index].shieldSlot;
          }
          if (this.shieldTimer > 0)
            --this.shieldTimer;
          if (!this.wasHoldingShield)
          {
            this.wasHoldingShield = true;
            if (this.shieldCD == 0)
              this.shieldTimer = flag1 ? 20 : 10;
            this.Player.itemAnimation = 0;
            this.Player.itemTime = 0;
            this.Player.reuseDelay = 0;
          }
          else
            this.RaisedShieldEffects();
          if (this.shieldTimer != 1)
            return;
          SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
          List<int> intList = new List<int>();
          if (flag2)
            intList.Add(235);
          if (flag3)
            intList.Add(87);
          if (flag1)
            intList.Add(66);
          if (intList.Count <= 0)
            return;
          for (int index1 = 0; index1 < 20; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, Utils.Next<int>(Main.rand, (IList<int>) intList), 0.0f, 0.0f, 0, new Color(), 1.5f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
          }
        }
        else
        {
          this.shieldTimer = 0;
          this.shieldHeldTime = 0;
          if (this.wasHoldingShield)
          {
            this.wasHoldingShield = false;
            this.Player.shield_parry_cooldown = 0;
          }
          if (this.shieldCD == 1)
          {
            SoundEngine.PlaySound(ref SoundID.Item28, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
            List<int> intList = new List<int>();
            if (flag2)
              intList.Add(235);
            if (flag3)
              intList.Add(87);
            if (flag1)
              intList.Add(66);
            if (intList.Count > 0)
            {
              for (int index3 = 0; index3 < 30; ++index3)
              {
                int index4 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, Utils.Next<int>(Main.rand, (IList<int>) intList), 0.0f, 0.0f, 0, new Color(), 2.5f);
                Main.dust[index4].noGravity = true;
                Dust dust = Main.dust[index4];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
              }
            }
          }
          if (this.shieldCD <= 0)
            return;
          --this.shieldCD;
        }
      }
    }

    public bool PStarelineActive
    {
      get
      {
        PearlwoodStareline pearlwoodStareline;
        return ((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.owner == ((Entity) this.Player).whoAmI && p.type == 931 && p.TryGetGlobalProjectile<PearlwoodStareline>(ref pearlwoodStareline) && pearlwoodStareline.Pearlwood));
      }
    }

    public int MythrilMaxTime
    {
      get
      {
        return !this.Player.HasEffect<MythrilEffect>() || !this.Player.ForceEffect<MythrilEffect>() ? 180 : 300;
      }
    }

    public float MythrilMaxSpeedBonus
    {
      get
      {
        return !this.Player.HasEffect<MythrilEffect>() || !this.Player.ForceEffect<MythrilEffect>() ? 1.5f : 1.75f;
      }
    }

    public int CHILL_DURATION => !this.Player.HasEffect<FrostEffect>() ? 900 : 1200;

    public DashManager.DashType FargoDash
    {
      get => this.fargoDash;
      set
      {
        this.fargoDash = value;
        if (value == DashManager.DashType.None)
          return;
        this.HasDash = true;
      }
    }

    public bool IsStillHoldingInSameDirectionAsMovement
    {
      get
      {
        return (double) ((Entity) this.Player).velocity.X > 0.0 && this.Player.controlRight || (double) ((Entity) this.Player).velocity.X < 0.0 && this.Player.controlLeft || this.Player.dashDelay < 0 || this.IsDashingTimer > 0;
      }
    }

    public bool IsInADashState
    {
      get => (this.Player.dashDelay == -1 || this.IsDashingTimer > 0) && this.Player.grapCount <= 0;
    }

    public virtual void SaveData(TagCompound tag)
    {
      List<string> stringList1 = new List<string>();
      if (this.MutantsPactSlot)
        stringList1.Add("MutantsPactSlot");
      if (this.MutantsDiscountCard)
        stringList1.Add("MutantsDiscountCard");
      if (this.MutantsCreditCard)
        stringList1.Add("MutantsCreditCard");
      if (this.ReceivedMasoGift)
        stringList1.Add("ReceivedMasoGift");
      if (this.RabiesVaccine)
        stringList1.Add("RabiesVaccine");
      if (this.DeerSinew)
        stringList1.Add("DeerSinew");
      if (this.HasClickedWrench)
        stringList1.Add("HasClickedWrench");
      if (this.Toggler_ExtraAttacksDisabled)
        stringList1.Add("Toggler_ExtraAttacksDisabled");
      if (this.Toggler_MinionsDisabled)
        stringList1.Add("Toggler_MinionsDisabled");
      tag.Add(((ModType) this).Mod.Name + "." + this.Player.name + ".Data", (object) stringList1);
      List<string> stringList2 = new List<string>();
      if (this.Toggler != null && this.Toggler.Toggles != null)
      {
        foreach (KeyValuePair<AccessoryEffect, Toggle> toggle in this.Toggler.Toggles)
        {
          if (!this.Toggler.Toggles[toggle.Key].ToggleBool)
            stringList2.Add(toggle.Key.Name);
        }
      }
      tag.Add(((ModType) this).Mod.Name + "." + this.Player.name + ".TogglesOff", (object) stringList2);
      this.Toggler.Save();
    }

    public virtual void LoadData(TagCompound tag)
    {
      IList<string> list = tag.GetList<string>(((ModType) this).Mod.Name + "." + this.Player.name + ".Data");
      this.MutantsPactSlot = list.Contains("MutantsPactSlot");
      this.MutantsDiscountCard = list.Contains("MutantsDiscountCard");
      this.MutantsCreditCard = list.Contains("MutantsCreditCard");
      this.ReceivedMasoGift = list.Contains("ReceivedMasoGift");
      this.RabiesVaccine = list.Contains("RabiesVaccine");
      this.DeerSinew = list.Contains("DeerSinew");
      this.HasClickedWrench = list.Contains("HasClickedWrench");
      this.Toggler_ExtraAttacksDisabled = list.Contains("Toggler_ExtraAttacksDisabled");
      this.Toggler_MinionsDisabled = list.Contains("Toggler_MinionsDisabled");
      List<string> disabledToggleNames = tag.GetList<string>(((ModType) this).Mod.Name + "." + this.Player.name + ".TogglesOff").ToList<string>();
      this.disabledToggles = ToggleLoader.LoadedToggles.Keys.Where<AccessoryEffect>((Func<AccessoryEffect, bool>) (x => disabledToggleNames.Contains(x.Name))).ToList<AccessoryEffect>();
    }

    public virtual void OnEnterWorld()
    {
      this.Toggler.TryLoad();
      this.Toggler.LoadPlayerToggles(this);
      this.disabledToggles.Clear();
      Mod mod1;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod1))
      {
        Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.NoMusic1"), new Color?(Color.LimeGreen));
        Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.NoMusic2"), new Color?(Color.LimeGreen));
      }
      Mod mod2;
      if (!Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasCrossmod", ref mod2))
      {
        List<string> stringList = new List<string>();
        Mod mod3;
        if (Terraria.ModLoader.ModLoader.TryGetMod("CalamityMod", ref mod3))
          stringList.Add(mod3.DisplayName);
        Mod mod4;
        if (Terraria.ModLoader.ModLoader.TryGetMod("NoxusBoss", ref mod4))
          stringList.Add(mod4.DisplayName);
        if (stringList.Count > 0)
        {
          string str = "";
          for (int index = 0; index < stringList.Count; ++index)
          {
            str += stringList[index];
            if (index + 2 < stringList.Count)
              str += ", ";
            else if (index + 1 < stringList.Count)
              str += " and ";
          }
          Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.NoDLC1", (object) str), new Color?(Color.Green));
          Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.NoDLC2"), new Color?(Color.Green));
        }
      }
      Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.Wiki"), new Color?(Color.Lime));
      if (!this.Toggler.CanPlayMaso)
        return;
      if (Main.netMode == 0)
      {
        WorldSavingSystem.CanPlayMaso = true;
      }
      else
      {
        if (Main.netMode != 1)
          return;
        ModPacket packet = ((ModType) this).Mod.GetPacket(256);
        ((BinaryWriter) packet).Write((byte) 10);
        ((BinaryWriter) packet).Write(this.Toggler.CanPlayMaso);
        packet.Send(-1, -1);
      }
    }

    public virtual void ResetEffects()
    {
      this.HasDash = false;
      this.FargoDash = DashManager.DashType.None;
      this.AttackSpeed = 1f;
      if (this.NoUsingItems > 0)
        --this.NoUsingItems;
      this.WingTimeModifier = 1f;
      this.QueenStingerItem = (Item) null;
      this.EridanusSet = false;
      this.GaiaSet = false;
      this.StyxSet = false;
      if (this.StyxAttackReadyTimer > 0)
        --this.StyxAttackReadyTimer;
      this.NekomiSet = false;
      if (this.NekomiHitCD > 0)
        --this.NekomiHitCD;
      if (this.NekomiAttackReadyTimer > 0)
        --this.NekomiAttackReadyTimer;
      this.BrainMinion = false;
      this.EaterMinion = false;
      this.BigBrainMinion = false;
      this.DukeFishron = false;
      this.SquirrelMount = false;
      this.SeekerOfAncientTreasures = false;
      this.AccursedSarcophagus = false;
      this.BabyLifelight = false;
      this.BabySilhouette = false;
      this.BiteSizeBaron = false;
      this.Nibble = false;
      this.ChibiDevi = false;
      this.MutantSpawn = false;
      this.BabyAbom = false;
      this.PetsActive = true;
      this.MinionCrits = false;
      this.FirstStrike = false;
      this.ShellHide = false;
      this.GoldShell = false;
      this.LavaWet = false;
      this.WoodEnchantDiscount = false;
      this.SnowVisual = false;
      this.ApprenticeEnchantActive = false;
      this.DarkArtistEnchantActive = false;
      this.CrystalEnchantActive = false;
      this.IronRecipes = false;
      this.ChlorophyteEnchantActive = false;
      this.PearlwoodStar = false;
      if (!this.MonkEnchantActive)
        this.Player.ClearBuff(ModContent.BuffType<MonkBuff>());
      this.MonkEnchantActive = false;
      this.ShinobiEnchantActive = false;
      this.PlatinumEffect = (Item) null;
      this.AncientShadowEnchantActive = false;
      this.SquireEnchantActive = false;
      this.ValhallaEnchantActive = false;
      this.TitaniumDRBuff = false;
      this.TitaniumCD = false;
      this.CactusImmune = false;
      this.MeleeSoul = false;
      this.MagicSoul = false;
      this.RangedSoul = false;
      this.SummonSoul = false;
      this.ColossusSoul = false;
      this.SupersonicSoul = false;
      this.WorldShaperSoul = false;
      this.FlightMasterySoul = false;
      this.RangedEssence = false;
      this.BuilderMode = false;
      this.UniverseSoul = false;
      this.UniverseCore = false;
      this.FishSoul1 = false;
      this.FishSoul2 = false;
      this.TerrariaSoul = false;
      this.VoidSoul = false;
      this.Eternity = false;
      this.PrimeSoulItemCount = 0;
      if (!this.PrimeSoulActiveBuffer)
        this.PrimeSoulActive = false;
      this.PrimeSoulActiveBuffer = false;
      if (this.ForceEffects != null)
        this.ForceEffects.Clear();
      this.SlimyShieldItem = (Item) null;
      this.DarkenedHeartItem = (Item) null;
      this.NecromanticBrewItem = (Item) null;
      this.DeerSinewNerf = false;
      this.PureHeart = false;
      this.PungentEyeballMinion = false;
      this.CrystalSkullMinion = false;
      this.FusedLens = false;
      this.FusedLensCanDebuff = false;
      this.Supercharged = false;
      this.Probes = false;
      this.MagicalBulb = false;
      this.PlanterasChild = false;
      this.SkullCharm = false;
      this.PungentEyeball = false;
      this.LumpOfFlesh = false;
      this.LihzahrdTreasureBoxItem = (Item) null;
      this.BetsysHeartItem = (Item) null;
      this.BetsyDashing = false;
      this.MutantAntibodies = false;
      this.GravityGlobeEXItem = (Item) null;
      this.MoonChalice = false;
      this.LunarCultist = false;
      this.TrueEyes = false;
      this.AbomWandItem = (Item) null;
      this.MasochistSoul = false;
      this.MasochistSoulItem = (Item) null;
      this.MasochistHeart = false;
      this.SandsofTime = false;
      this.SecurityWallet = false;
      this.FrigidGemstoneItem = (Item) null;
      this.NymphsPerfume = false;
      this.NymphsPerfumeRespawn = false;
      this.RainbowSlime = false;
      this.SkeletronArms = false;
      this.IceQueensCrown = false;
      this.CirnoGraze = false;
      this.MiniSaucer = false;
      this.CanAmmoCycle = false;
      this.TribalCharm = false;
      this.TribalCharmEquipped = false;
      this.SupremeDeathbringerFairy = false;
      this.GodEaterImbue = false;
      this.MutantSetBonusItem = (Item) null;
      this.AbomMinion = false;
      this.PhantasmalRing = false;
      this.TwinsEX = false;
      this.TimsConcoction = false;
      this.DeviGraze = false;
      this.Graze = false;
      this.GrazeRadius = 100f;
      this.DevianttHeartItem = (Item) null;
      this.MutantEyeItem = (Item) null;
      this.MutantEyeVisual = false;
      this.AbomRebirth = false;
      this.WasHurtBySomething = false;
      this.PrecisionSeal = false;
      this.GelicWingsItem = (Item) null;
      this.ConcentratedRainbowMatter = false;
      this.Hexed = false;
      this.Unstable = false;
      this.Fused = false;
      this.Shadowflame = false;
      this.Oiled = false;
      this.Slimed = false;
      this.noDodge = false;
      this.noSupersonic = false;
      this.NoMomentum = false;
      this.Bloodthirsty = false;
      this.DisruptedFocus = false;
      this.Smite = false;
      this.Anticoagulation = false;
      this.GodEater = false;
      this.FlamesoftheUniverse = false;
      this.MutantNibble = false;
      this.Asocial = false;
      this.Kneecapped = false;
      this.Defenseless = false;
      this.Purified = false;
      this.Infested = false;
      this.Rotting = false;
      this.SqueakyToy = false;
      this.Atrophied = false;
      this.Jammed = false;
      this.ReverseManaFlow = false;
      this.CurseoftheMoon = false;
      this.OceanicMaul = false;
      this.DeathMarked = false;
      this.Hypothermia = false;
      this.Midas = false;
      if (!this.MutantPresence)
        this.PresenceTogglerTimer = 0;
      this.MutantPresence = this.MutantPresence && this.Player.HasBuff(ModContent.BuffType<MutantPresenceBuff>());
      this.HadMutantPresence = this.MutantPresence;
      this.MutantFang = false;
      this.DevianttPresence = false;
      this.Swarming = false;
      this.LowGround = false;
      this.Flipped = false;
      this.LihzahrdCurse = false;
      this.Berserked = false;
      this.CerebralMindbreak = false;
      this.NanoInjection = false;
      this.Stunned = false;
      this.HasJungleRose = false;
      this.HaveCheckedAttackSpeed = false;
      this.BoxofGizmos = false;
      this.OxygenTank = false;
      this.WizardedItem = (Item) null;
      this.EquippedEnchants.Clear();
      this.WizardTooltips = false;
      if (this.WizardEnchantActive)
      {
        this.WizardEnchantActive = false;
        List<Item> objList = new List<Item>();
        for (int index = 3; index < 10; ++index)
        {
          if (this.Player.IsItemSlotUnlockedAndUsable(index))
            objList.Add(this.Player.armor[index]);
        }
        AccessorySlotLoader accessorySlotLoader = LoaderManager.Get<AccessorySlotLoader>();
        ModAccessorySlotPlayer modPlayer = this.Player.GetModPlayer<ModAccessorySlotPlayer>();
        for (int index = 0; index < modPlayer.SlotCount; ++index)
        {
          if (accessorySlotLoader.ModdedIsItemSlotUnlockedAndUsable(index, this.Player))
            objList.Add(accessorySlotLoader.Get(index, this.Player).FunctionalItem);
        }
        for (int index = 0; index < objList.Count - 1; ++index)
        {
          if (!objList[index].IsAir && (objList[index].type == ModContent.ItemType<WizardEnchant>() || objList[index].type == ModContent.ItemType<CosmoForce>()))
          {
            this.WizardEnchantActive = true;
            Item obj = objList[index + 1];
            if (obj != null && !obj.IsAir && obj.ModItem != null && obj.ModItem is BaseEnchant)
            {
              this.WizardedItem = obj;
              break;
            }
            break;
          }
        }
      }
      if (!this.Mash && this.MashCounter > 0)
        --this.MashCounter;
      this.Mash = false;
    }

    public virtual void OnRespawn()
    {
      if (!this.NymphsPerfumeRespawn)
        return;
      this.NymphsPerfumeRestoreLife = 6;
    }

    public virtual void UpdateDead()
    {
      bool sandsofTime = this.SandsofTime;
      bool nymphsPerfumeRespawn = this.NymphsPerfumeRespawn;
      base.ResetEffects();
      this.SandsofTime = sandsofTime;
      this.NymphsPerfumeRespawn = nymphsPerfumeRespawn;
      if (this.SandsofTime && !Luminance.Common.Utilities.Utilities.AnyBosses() && this.Player.respawnTimer > 10)
        this.Player.respawnTimer -= this.Eternity ? 6 : 1;
      this.ReallyAwfulDebuffCooldown = 0;
      this.ParryDebuffImmuneTime = 0;
      this.WingTimeModifier = 1f;
      this.FreeEaterSummon = true;
      this.AbominableWandRevived = false;
      this.EridanusTimer = 0;
      this.StyxMeter = 0;
      this.StyxTimer = 0;
      this.StyxAttackReadyTimer = 0;
      this.NekomiMeter = 0;
      this.NekomiTimer = 0;
      this.NekomiAttackReadyTimer = 0;
      this.CirnoGrazeCounter = 0;
      this.unstableCD = 0;
      this.lightningRodTimer = (byte) 0;
      this.BuilderMode = false;
      this.NoUsingItems = 0;
      this.FreezeTime = false;
      this.freezeLength = 0;
      this.ChillSnowstorm = false;
      this.chillLength = 0;
      this.SlimyShieldFalling = false;
      this.DarkenedHeartCD = 60;
      this.GuttedHeartCD = 60;
      this.IsDashingTimer = 0;
      this.GroundPound = 0;
      this.NymphsPerfumeCD = 30;
      this.WretchedPouchCD = 0;
      this.DeviGrazeBonus = 0.0;
      this.MutantEyeCD = 60;
      this.MythrilTimer = this.MythrilMaxTime;
      this.BeetleEnchantDefenseTimer = 0;
      this.Mash = false;
      this.WizardEnchantActive = false;
      this.MashCounter = 0;
      this.MaxLifeReduction = 0;
      this.CurrentLifeReduction = 0;
      this.The22Incident = 0;
    }

    public virtual void ModifyLuck(ref float luck)
    {
      if (this.Unlucky)
        --luck;
      this.Unlucky = false;
    }

    public void ManageLifeReduction()
    {
      if (this.OceanicMaul && this.LifeReductionUpdateTimer <= 0)
        this.LifeReductionUpdateTimer = 1;
      if (this.LifeReductionUpdateTimer > 0 && this.LifeReductionUpdateTimer++ > 30)
      {
        this.LifeReductionUpdateTimer = 1;
        if (this.OceanicMaul)
        {
          if (this.MutantFang)
            this.LifeReductionUpdateTimer = 20;
          int num = this.CurrentLifeReduction + 5;
          if (num > this.MaxLifeReduction)
            num = this.MaxLifeReduction;
          if (num > this.Player.statLifeMax2 - 100)
            num = this.Player.statLifeMax2 - 100;
          if (this.CurrentLifeReduction < num)
          {
            this.CurrentLifeReduction = num;
            CombatText.NewText(((Entity) this.Player).Hitbox, Color.DarkRed, Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Buffs.OceanicMaulBuff.LifeDown"), false, false);
          }
        }
        else
        {
          this.CurrentLifeReduction -= 5;
          if (this.MaxLifeReduction > this.CurrentLifeReduction)
            this.MaxLifeReduction = this.CurrentLifeReduction;
          CombatText.NewText(((Entity) this.Player).Hitbox, Color.DarkGreen, Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Buffs.OceanicMaulBuff.LifeUp"), false, false);
        }
      }
      if (this.CurrentLifeReduction > 0)
      {
        if (this.CurrentLifeReduction > this.Player.statLifeMax2 - 100)
          this.CurrentLifeReduction = this.Player.statLifeMax2 - 100;
        this.Player.statLifeMax2 -= this.CurrentLifeReduction;
      }
      else
      {
        if (this.OceanicMaul)
          return;
        this.CurrentLifeReduction = 0;
        this.MaxLifeReduction = 0;
        this.LifeReductionUpdateTimer = 0;
      }
    }

    public virtual float UseSpeedMultiplier(Item item)
    {
      int useTime = item.useTime;
      int useAnimation = item.useAnimation;
      if (useTime <= 0 || useAnimation <= 0 || item.damage <= 0)
        return base.UseSpeedMultiplier(item);
      if (!this.HaveCheckedAttackSpeed)
      {
        this.HaveCheckedAttackSpeed = true;
        this.AttackSpeed += this.Player.AccessoryEffects().ModifyUseSpeed(item);
        if (this.Berserked)
          this.AttackSpeed += 0.1f;
        if (this.MagicSoul && item.CountsAsClass(DamageClass.Magic))
          this.AttackSpeed += 0.2f;
        if (this.Player.HasEffect<MythrilEffect>())
          MythrilEffect.CalcMythrilAttackSpeed(this, item);
        if (this.Player.HasEffect<WretchedPouchEffect>() && !this.MasochistSoul && (double) this.AttackSpeed > 1.0)
          this.AttackSpeed -= (this.AttackSpeed - 1f) / 2f;
        while ((double) useTime / (double) this.AttackSpeed < 1.0)
          this.AttackSpeed -= 0.01f;
        while ((double) useAnimation / (double) this.AttackSpeed < 3.0)
          this.AttackSpeed -= 0.01f;
        if ((double) this.AttackSpeed < 0.10000000149011612)
          this.AttackSpeed = 0.1f;
      }
      return this.AttackSpeed;
    }

    public virtual void DrawEffects(
      PlayerDrawSet drawInfo,
      ref float r,
      ref float g,
      ref float b,
      ref float a,
      ref bool fullBright)
    {
      if (this.Shadowflame)
      {
        if (Utils.NextBool(Main.rand, 4) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width, ((Entity) this.Player).height, 27, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 100, new Color(), 2f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
          Main.dust[index].velocity.Y -= 0.5f;
          drawInfo.DustCache.Add(index);
        }
        fullBright = true;
      }
      if (this.Rotting && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width, ((Entity) this.Player).height, 5, ((Entity) this.Player).velocity.X * 0.1f, ((Entity) this.Player).velocity.Y * 0.1f, 0, new Color(), 2f);
        Main.dust[index].noGravity = Utils.NextBool(Main.rand);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.8f);
        Main.dust[index].velocity.Y -= 0.5f;
        drawInfo.DustCache.Add(index);
      }
      if (this.Purified && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 91, 0.0f, 0.0f, 100, new Color(), 2.5f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index].noGravity = true;
        drawInfo.DustCache.Add(index);
      }
      if (this.Smite && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 91, 0.0f, 0.0f, 100, Main.DiscoColor, 2.5f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        Main.dust[index].noGravity = true;
        drawInfo.DustCache.Add(index);
      }
      if (this.Anticoagulation && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        ++Main.dust[index].scale;
      }
      if (this.Hexed)
      {
        if (Utils.NextBool(Main.rand, 3) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width, ((Entity) this.Player).height, 257, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 100, new Color(), 2.5f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
          Main.dust[index].velocity.Y -= 0.5f;
          Main.dust[index].color = Color.GreenYellow;
          drawInfo.DustCache.Add(index);
        }
        if (Utils.NextBool(Main.rand) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 106, 0.0f, 0.0f, 100, new Color(), 2.5f);
          Main.dust[index].noGravity = true;
          drawInfo.DustCache.Add(index);
        }
        fullBright = true;
      }
      if (this.Infested)
      {
        if (Utils.NextBool(Main.rand, 4) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width, ((Entity) this.Player).height, 44, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 100, new Color(), this.InfestedDust);
          Main.dust[index].noGravity = true;
          drawInfo.DustCache.Add(index);
        }
        fullBright = true;
      }
      if (this.CurrentLifeReduction > 0 && Utils.NextBool(Main.rand) && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 5, 0.0f, 0.0f, 0, new Color(), 1f);
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        ++Main.dust[index].scale;
        drawInfo.DustCache.Add(index);
      }
      if (this.GodEater)
      {
        if (Utils.NextBool(Main.rand, 3) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width + 4, ((Entity) this.Player).height + 4, 86, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 100, new Color(), 3f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 1.2f);
          Main.dust[index].velocity.Y -= 0.15f;
          drawInfo.DustCache.Add(index);
        }
        r *= 0.15f;
        g *= 0.03f;
        b *= 0.09f;
        fullBright = true;
      }
      if (this.FlamesoftheUniverse)
      {
        if (Utils.NextBool(Main.rand, 4) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 21, ((Entity) this.Player).velocity.X * 0.2f, ((Entity) this.Player).velocity.Y * 0.2f, 100, new Color(50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5, 50 * Main.rand.Next(6) + 5), 2.5f);
          --Main.dust[index].velocity.Y;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
          Main.dust[index].noGravity = true;
          drawInfo.DustCache.Add(index);
        }
        fullBright = true;
      }
      if (this.CurseoftheMoon)
      {
        if (Utils.NextBool(Main.rand, 5))
        {
          int index = Dust.NewDust(((Entity) this.Player).Center, 0, 0, 229, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index].noGravity = true;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
          drawInfo.DustCache.Add(index);
        }
        if (Utils.NextBool(Main.rand, 5))
        {
          int index = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 229, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 0, new Color(), 1f);
          Main.dust[index].noGravity = true;
          --Main.dust[index].velocity.Y;
          Dust dust = Main.dust[index];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
          drawInfo.DustCache.Add(index);
        }
      }
      if (this.DeathMarked)
      {
        if (Utils.NextBool(Main.rand) && (double) drawInfo.shadow == 0.0)
        {
          int index = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.Player).position, new Vector2(2f, 2f)), ((Entity) this.Player).width, ((Entity) this.Player).height, 109, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 0, new Color(), 1.5f);
          --Main.dust[index].velocity.Y;
          if (!Utils.NextBool(Main.rand, 3))
          {
            Main.dust[index].noGravity = true;
            Main.dust[index].scale += 0.5f;
            Dust dust = Main.dust[index];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
            Main.dust[index].velocity.Y -= 0.5f;
          }
          drawInfo.DustCache.Add(index);
        }
        r *= 0.2f;
        g *= 0.2f;
        b *= 0.2f;
        fullBright = true;
      }
      if (this.Fused && Utils.NextBool(Main.rand) && (double) drawInfo.shadow == 0.0)
      {
        int index = Dust.NewDust(Vector2.op_Addition(((Entity) this.Player).position, new Vector2((float) (((Entity) this.Player).width / 2), (float) (((Entity) this.Player).height / 5))), 0, 0, 6, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 0, new Color(), 2f);
        Main.dust[index].velocity.Y -= 2f;
        Dust dust = Main.dust[index];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 2f);
        if (Utils.NextBool(Main.rand, 4))
        {
          Main.dust[index].scale += 0.5f;
          Main.dust[index].noGravity = true;
        }
        drawInfo.DustCache.Add(index);
      }
      if (!this.Supercharged || !Utils.NextBool(Main.rand) || (double) drawInfo.shadow != 0.0)
        return;
      int index1 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 229, ((Entity) this.Player).velocity.X * 0.4f, ((Entity) this.Player).velocity.Y * 0.4f, 0, new Color(), 1f);
      Main.dust[index1].scale += 0.5f;
      Main.dust[index1].noGravity = true;
      Dust dust1 = Main.dust[index1];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 1.8f);
      if (!Utils.NextBool(Main.rand, 3))
        return;
      Main.dust[index1].noGravity = false;
      Main.dust[index1].scale *= 0.5f;
    }

    public void ConcentratedRainbowMatterTryAutoHeal()
    {
      if (!this.ConcentratedRainbowMatter || this.Player.statLife >= this.Player.statLifeMax2 || this.Player.potionDelay > 0 || !this.Player.HasEffect<RainbowHealEffect>() || this.MutantNibble)
        return;
      Item itemToUse = this.Player.QuickHeal_GetItemToUse();
      if (itemToUse == null || this.Player.statLife >= this.Player.statLifeMax2 - this.GetHealMultiplier(itemToUse.healLife) || (double) this.Player.statLife >= (double) this.Player.statLifeMax2 * 0.4 || !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n =>
      {
        if (!((Entity) n).active || n.damage <= 0 || n.friendly || (double) ((Entity) this.Player).Distance(((Entity) n).Center) >= 1200.0)
          return false;
        return n.noTileCollide || Collision.CanHitLine(((Entity) this.Player).Center, 0, 0, ((Entity) n).Center, 0, 0);
      })))
        return;
      this.Player.QuickHeal();
    }

    private PlayerDeathReason DeathByLocalization(string key)
    {
      return PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage." + key, (object) this.Player.name));
    }

    public virtual bool PreKill(
      double damage,
      int hitDirection,
      bool pvp,
      ref bool playSound,
      ref bool genGore,
      ref PlayerDeathReason damageSource)
    {
      bool flag = true;
      if (this.Player.statLife <= 0)
      {
        if (((Entity) this.Player).whoAmI == Main.myPlayer & flag && this.AbomRebirth && !this.WasHurtBySomething)
        {
          this.Player.statLife = 1;
          return false;
        }
        if (((Entity) this.Player).whoAmI == Main.myPlayer & flag && this.Player.HasEffect<FossilEffect>() && !this.Player.HasBuff<FossilReviveCDBuff>())
        {
          FossilEffect.FossilRevive(this.Player);
          flag = false;
        }
        if (((Entity) this.Player).whoAmI == Main.myPlayer & flag && this.MutantSetBonusItem != null && this.Player.FindBuffIndex(ModContent.BuffType<MutantRebirthBuff>()) == -1)
        {
          this.TryCleanseDebuffs();
          this.Player.statLife = this.Player.statLifeMax2;
          this.Player.HealEffect(this.Player.statLifeMax2, true);
          this.Player.immune = true;
          this.Player.immuneTime = 180;
          this.Player.hurtCooldowns[0] = 180;
          this.Player.hurtCooldowns[1] = 180;
          Main.NewText((object) Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.Revived"), new Color?(Color.LimeGreen));
          this.Player.AddBuff(ModContent.BuffType<MutantRebirthBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames(120f), true, false);
          flag = false;
        }
        if (((Entity) this.Player).whoAmI == Main.myPlayer & flag && this.AbomWandItem != null && !this.AbominableWandRevived)
        {
          this.AbominableWandRevived = true;
          int num = 1;
          this.Player.statLife = num;
          this.Player.HealEffect(num, true);
          this.Player.immune = true;
          this.Player.immuneTime = 120;
          this.Player.hurtCooldowns[0] = 120;
          this.Player.hurtCooldowns[1] = 120;
          string textValue = Language.GetTextValue("Mods." + ((ModType) this).Mod.Name + ".Message.Revived");
          CombatText.NewText(((Entity) this.Player).Hitbox, Color.Yellow, textValue, true, false);
          Main.NewText((object) textValue, new Color?(Color.Yellow));
          this.Player.AddBuff(ModContent.BuffType<AbomRebirthBuff>(), 900, true, false);
          flag = false;
          for (int index = 0; index < 24; ++index)
            Projectile.NewProjectile(this.Player.GetSource_Accessory(this.AbomWandItem, (string) null), ((Entity) this.Player).Center, Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465), Utils.NextFloat(Main.rand, 4f, 16f)), ModContent.ProjectileType<StyxArmorScythe2>(), 0, 10f, Main.myPlayer, (float) (-60 - Main.rand.Next(60)), -1f, 0.0f);
        }
      }
      if (damage == 10.0 && hitDirection == 0 && damageSource.SourceOtherIndex == 8)
      {
        if (this.Infested)
          damageSource = this.DeathByLocalization("Infested");
        if (this.Anticoagulation)
          damageSource = this.DeathByLocalization("Anticoagulation");
        if (this.Rotting)
          damageSource = this.DeathByLocalization("Rotting");
        if (this.Shadowflame)
          damageSource = this.DeathByLocalization("Shadowflame");
        if (this.NanoInjection)
          damageSource = this.DeathByLocalization("NanoInjection");
        if (this.GodEater || this.FlamesoftheUniverse || this.CurseoftheMoon || this.MutantFang)
          damageSource = this.DeathByLocalization("DivineWrath");
      }
      if (this.StatLifePrevious > 0 && this.Player.statLife > this.StatLifePrevious)
        this.StatLifePrevious = this.Player.statLife;
      if (!flag)
      {
        if (!Main.dedServ)
        {
          SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/Revive", (SoundType) 0);
          SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        }
        if (((Entity) this.Player).whoAmI == Main.myPlayer && this.MutantSetBonusItem != null)
          Projectile.NewProjectile(this.Player.GetSource_Accessory(this.MutantSetBonusItem, (string) null), ((Entity) this.Player).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<GiantDeathray>(), (int) (7000.0 * (double) this.Player.ActualClassDamage(DamageClass.Magic)), 10f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f);
      }
      return flag;
    }

    public virtual void ModifyDrawInfo(ref PlayerDrawSet drawInfo)
    {
      if (this.GaiaOffense)
      {
        int shaderIdFromItemId = GameShaders.Armor.GetShaderIdFromItemId(ModContent.ItemType<GaiaDye>());
        drawInfo.cBody = shaderIdFromItemId;
        drawInfo.cHead = shaderIdFromItemId;
        drawInfo.cLegs = shaderIdFromItemId;
        drawInfo.cWings = shaderIdFromItemId;
        drawInfo.cHandOn = shaderIdFromItemId;
        drawInfo.cHandOff = shaderIdFromItemId;
        drawInfo.cShoe = shaderIdFromItemId;
      }
      if (!this.GuardRaised)
        return;
      this.Player.bodyFrame.Y = this.Player.bodyFrame.Height * 10;
      if (this.shieldTimer <= 0)
        return;
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 1);
      Span<int> span = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span[num1] = GameShaders.Armor.GetShaderIdFromItemId(3026);
      int num2 = num1 + 1;
      List<int> intList2 = intList1;
      if (this.Player.HasEffect<DreadShellEffect>())
        intList2.Add(GameShaders.Armor.GetShaderIdFromItemId(4663));
      if (this.Player.HasEffect<PumpkingsCapeEffect>())
        intList2.Add(GameShaders.Armor.GetShaderIdFromItemId(2879));
      int num3 = intList2[(int) ((long) (Main.GameUpdateCount / 4U) % (long) intList2.Count)];
      drawInfo.cBody = num3;
      drawInfo.cHead = num3;
      drawInfo.cLegs = num3;
      drawInfo.cWings = num3;
      drawInfo.cHandOn = num3;
      drawInfo.cHandOff = num3;
      drawInfo.cShoe = num3;
      drawInfo.cBack = num3;
      drawInfo.cBackpack = num3;
      drawInfo.cShield = num3;
      drawInfo.cNeck = num3;
      drawInfo.cHandOn = num3;
      drawInfo.cHandOff = num3;
      drawInfo.cBalloon = num3;
      drawInfo.cBalloonFront = num3;
      drawInfo.cFace = num3;
      drawInfo.cFaceHead = num3;
      drawInfo.cFront = num3;
    }

    public void AddPet(bool toggle, bool vanityToggle, int buff, int proj)
    {
      if (vanityToggle)
      {
        this.PetsActive = false;
      }
      else
      {
        if (!(((Entity) this.Player).whoAmI == Main.myPlayer & toggle) || this.Player.FindBuffIndex(buff) != -1 || this.Player.ownedProjectileCounts[proj] >= 1)
          return;
        Main.projectile[Projectile.NewProjectile(((Entity) this.Player).GetSource_Misc("Pet"), ((Entity) this.Player).Center.X, ((Entity) this.Player).Center.Y, 0.0f, -1f, proj, 0, 0.0f, ((Entity) this.Player).whoAmI, 0.0f, 0.0f, 0.0f)].netUpdate = true;
      }
    }

    public void AddMinion(Item item, bool toggle, int proj, int damage, float knockback)
    {
      if (((Entity) this.Player).whoAmI != Main.myPlayer || ((this.Player.ownedProjectileCounts[proj] >= 1 ? 0 : (((Entity) this.Player).whoAmI == Main.myPlayer ? 1 : 0)) & (toggle ? 1 : 0)) == 0)
        return;
      FargoSoulsUtil.NewSummonProjectile(this.Player.GetSource_Accessory(item, (string) null), ((Entity) this.Player).Center, Vector2.op_UnaryNegation(Vector2.UnitY), proj, damage, knockback, Main.myPlayer);
    }

    private void KillPets()
    {
      int buffType1 = this.Player.miscEquips[0].buffType;
      int buffType2 = this.Player.miscEquips[1].buffType;
      this.Player.buffImmune[buffType1] = true;
      this.Player.buffImmune[buffType2] = true;
      this.Player.ClearBuff(buffType1);
      this.Player.ClearBuff(buffType2);
      if (!this.WasAsocial)
      {
        this.HidePetToggle0 = ((BitsByte) ref this.Player.hideMisc)[0];
        this.HidePetToggle1 = ((BitsByte) ref this.Player.hideMisc)[1];
        this.WasAsocial = true;
      }
      if (!((BitsByte) ref this.Player.hideMisc)[0])
        this.Player.TogglePet();
      if (!((BitsByte) ref this.Player.hideMisc)[1])
        this.Player.ToggleLight();
      ((BitsByte) ref this.Player.hideMisc)[0] = true;
      ((BitsByte) ref this.Player.hideMisc)[1] = true;
    }

    public static void Squeak(Vector2 center)
    {
      if (Main.dedServ)
        return;
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(48, 1);
      interpolatedStringHandler.AppendLiteral("FargowiltasSouls/Assets/Sounds/SqueakyToy/squeak");
      interpolatedStringHandler.AppendFormatted<int>(Main.rand.Next(1, 7));
      SoundStyle soundStyle = new SoundStyle(interpolatedStringHandler.ToStringAndClear(), (SoundType) 0);
      SoundEngine.PlaySound(ref soundStyle, new Vector2?(center), (SoundUpdateCallback) null);
    }

    private int InfestedExtraDot()
    {
      int buffIndex = this.Player.FindBuffIndex(ModContent.BuffType<InfestedBuff>());
      if (buffIndex == -1)
      {
        buffIndex = this.Player.FindBuffIndex(ModContent.BuffType<NeurotoxinBuff>());
        if (buffIndex == -1)
          return 0;
      }
      float num1 = (float) (this.MaxInfestTime - this.Player.buffTime[buffIndex]) / 90f;
      int num2 = (int) ((double) num1 * (double) num1 + 4.0);
      this.InfestedDust = (float) ((double) num1 / 10.0 + 1.0);
      if ((double) this.InfestedDust > 5.0)
        this.InfestedDust = 5f;
      return num2 * 2;
    }

    public virtual void PostNurseHeal(NPC nurse, int health, bool removeDebuffs, int price)
    {
      if (!this.Player.HasEffect<GuttedHeartMinions>())
        return;
      GuttedHeartMinions.NurseHeal(this.Player);
    }

    public virtual bool CanConsumeAmmo(Item weapon, Item ammo)
    {
      return (!weapon.CountsAsClass(DamageClass.Ranged) || (!this.RangedEssence || !Utils.NextBool(Main.rand, 10)) && (!this.RangedSoul || !Utils.NextBool(Main.rand, 5))) && (!this.GaiaSet || !Utils.NextBool(Main.rand, 10));
    }

    public virtual void HideDrawLayers(PlayerDrawSet drawInfo)
    {
      if (!this.BetsyDashing && !this.ShellHide && !this.GoldShell)
        return;
      foreach (PlayerDrawLayer layer in (IEnumerable<PlayerDrawLayer>) PlayerDrawLayerLoader.Layers)
        layer.Hide();
    }

    public int GetHealMultiplier(int heal)
    {
      float num = 1f;
      bool squireEnchantActive = this.SquireEnchantActive;
      bool valhallaEnchantActive = this.ValhallaEnchantActive;
      if (squireEnchantActive | valhallaEnchantActive)
      {
        bool flag = this.ForceEffect<SquireEnchant>() || this.ForceEffect<ValhallaKnightEnchant>();
        if (this.Eternity)
          num = 5f;
        else if (flag & valhallaEnchantActive)
          num = 1.2f;
        else if (valhallaEnchantActive || flag & squireEnchantActive)
          num = 1.15f;
        else if (squireEnchantActive)
          num = 1.1f;
      }
      heal = (int) ((double) heal * (double) num);
      return heal;
    }

    public virtual void GetHealLife(Item item, bool quickHeal, ref int healValue)
    {
      healValue = this.GetHealMultiplier(healValue);
      if (!this.Player.HasEffect<HallowEffect>())
        return;
      healValue = 0;
    }

    public void HealPlayer(int amount)
    {
      amount = this.GetHealMultiplier(amount);
      this.Player.statLife += amount;
      if (this.Player.statLife > this.Player.statLifeMax2)
        this.Player.statLife = this.Player.statLifeMax2;
      this.Player.HealEffect(amount, true);
    }

    public virtual void CopyClientState(ModPlayer clientClone)
    {
      (clientClone as FargoSoulsPlayer).Toggler = this.Toggler;
    }

    public void SyncToggle(AccessoryEffect effect)
    {
      if (this.TogglesToSync.ContainsKey(effect))
        return;
      this.TogglesToSync.Add(effect, this.Player.GetToggle(effect).ToggleBool);
    }

    public virtual void SyncPlayer(int toWho, int fromWho, bool newPlayer)
    {
      ModPacket packet1 = ((ModType) this).Mod.GetPacket(256);
      ((BinaryWriter) packet1).Write((byte) 9);
      ((BinaryWriter) packet1).Write((byte) ((Entity) this.Player).whoAmI);
      ((BinaryWriter) packet1).Write(this.Toggler_ExtraAttacksDisabled);
      ((BinaryWriter) packet1).Write(this.Toggler_MinionsDisabled);
      packet1.Send(toWho, fromWho);
      foreach (KeyValuePair<AccessoryEffect, bool> keyValuePair in this.TogglesToSync)
      {
        ModPacket packet2 = ((ModType) this).Mod.GetPacket(256);
        ((BinaryWriter) packet2).Write((byte) 8);
        ((BinaryWriter) packet2).Write((byte) ((Entity) this.Player).whoAmI);
        ((BinaryWriter) packet2).Write(keyValuePair.Key.FullName);
        ((BinaryWriter) packet2).Write(keyValuePair.Value);
        packet2.Send(toWho, fromWho);
      }
      this.TogglesToSync.Clear();
    }

    public virtual void SendClientChanges(ModPlayer clientPlayer)
    {
      if ((clientPlayer as FargoSoulsPlayer).Toggler.Toggles == this.Toggler.Toggles)
        return;
      ModPacket packet = ((ModType) this).Mod.GetPacket(256);
      ((BinaryWriter) packet).Write((byte) 7);
      ((BinaryWriter) packet).Write((byte) ((Entity) this.Player).whoAmI);
      ((BinaryWriter) packet).Write((byte) this.Toggler.Toggles.Count);
      for (int index = 0; index < this.Toggler.Toggles.Count; ++index)
        ((BinaryWriter) packet).Write(this.Toggler.Toggles.Values.ElementAt<Toggle>(index).ToggleBool);
      packet.Send(-1, -1);
    }

    public void AddBuffNoStack(int buff, int duration)
    {
      if (this.Player.HasBuff(buff) || this.ReallyAwfulDebuffCooldown > 0)
        return;
      this.Player.AddBuff(buff, duration, true, false);
      int buffIndex = this.Player.FindBuffIndex(buff);
      if (buffIndex == -1)
        return;
      this.ReallyAwfulDebuffCooldown = this.Player.buffTime[buffIndex] + 240;
    }

    public void TryAdditionalAttacks(int damage, DamageClass damageType)
    {
    }

    public Rectangle GetPrecisionHurtbox()
    {
      Rectangle hitbox = ((Entity) this.Player).Hitbox;
      hitbox.X += hitbox.Width / 2;
      hitbox.Y += hitbox.Height / 2;
      hitbox.Width = Math.Min(hitbox.Width, hitbox.Height);
      hitbox.Height = Math.Min(hitbox.Width, hitbox.Height);
      hitbox.X -= hitbox.Width / 2;
      hitbox.Y -= hitbox.Height / 2;
      return hitbox;
    }

    public bool ForceEffect(ModItem modItem)
    {
      return this.TerrariaSoul || !Main.gamePaused && modItem != null && modItem.Item != null && !modItem.Item.IsAir && (modItem is BaseEnchant && (CheckWizard(modItem.Item.type) || CheckForces(modItem.Item.type)) || modItem is BaseSoul || modItem is BaseForce);

      bool CheckForces(int type)
      {
        int num = BaseEnchant.Force[type];
        if (num > 0)
          return this.ForceEffects.Contains(num);
        return BaseEnchant.CraftsInto[type] > 0 && CheckForces(BaseEnchant.CraftsInto[type]);
      }

      bool CheckWizard(int type)
      {
        if (this.WizardedItem != null && !this.WizardedItem.IsAir && this.WizardedItem.type == type)
          return true;
        return BaseEnchant.CraftsInto[type] > 0 && CheckWizard(BaseEnchant.CraftsInto[type]);
      }
    }

    public bool ForceEffect<T>() where T : BaseEnchant
    {
      return this.ForceEffect((ModItem) ModContent.GetInstance<T>());
    }

    public bool ForceEffect(int? enchType)
    {
      if (enchType.HasValue)
      {
        int? nullable = enchType;
        int num = 0;
        if (!(nullable.GetValueOrDefault() <= num & nullable.HasValue))
        {
          ModItem modItem = ModContent.GetModItem(enchType.Value);
          return modItem != null && this.ForceEffect(modItem);
        }
      }
      return false;
    }

    public virtual void PreSavePlayer() => SquireEnchant.ResetMountStats(this);

    public virtual void ModifyHitNPCWithProj(
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
      if (proj.hostile)
        return;
      if (this.MinionCrits && FargoSoulsUtil.IsSummonDamage(proj) && (double) Main.rand.Next(100) < (double) this.Player.ActualClassCrit(DamageClass.Summon))
        ((NPC.HitModifiers) ref modifiers).SetCrit();
      if (this.SqueakyToy)
      {
        modifiers.FinalDamage.Base = 1f;
        FargoSoulsPlayer.Squeak(((Entity) target).Center);
      }
      else
      {
        if (this.Asocial && FargoSoulsUtil.IsSummonDamage(proj, includeWhips: false))
          modifiers.Null();
        if (this.Atrophied && (proj.CountsAsClass(DamageClass.Melee) || proj.CountsAsClass(DamageClass.Throwing)))
          modifiers.Null();
        this.ModifyHitNPCBoth(target, ref modifiers, proj.DamageType);
      }
    }

    public virtual void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
    {
      if (this.SqueakyToy)
      {
        ((NPC.HitModifiers) ref modifiers).SetMaxDamage(1);
        FargoSoulsPlayer.Squeak(((Entity) target).Center);
      }
      else
      {
        if (this.Atrophied)
          modifiers.Null();
        this.ModifyHitNPCBoth(target, ref modifiers, item.DamageType);
      }
    }

    public void ModifyHitNPCBoth(
      NPC target,
      ref NPC.HitModifiers modifiers,
      DamageClass damageClass)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FargoSoulsPlayer.\u003C\u003Ec__DisplayClass420_0 displayClass4200 = new FargoSoulsPlayer.\u003C\u003Ec__DisplayClass420_0();
      // ISSUE: reference to a compiler-generated field
      displayClass4200.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      displayClass4200.target = target;
      // ISSUE: reference to a compiler-generated field
      displayClass4200.damageClass = damageClass;
      // ISSUE: method pointer
      ((NPC.HitModifiers) ref modifiers).ModifyHitInfo += new NPC.HitModifiers.HitInfoModifier((object) displayClass4200, __methodptr(\u003CModifyHitNPCBoth\u003Eb__0));
      if (this.DeerSinewNerf)
      {
        float num = Math.Min(((Vector2) ref ((Entity) this.Player).velocity).Length() / 20f, 1f);
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, MathHelper.Lerp(1f, 0.85f, num));
      }
      if (this.CerebralMindbreak)
      {
        ref StatModifier local = ref modifiers.FinalDamage;
        local = StatModifier.op_Multiply(local, 0.7f);
      }
      if (!this.FirstStrike)
        return;
      ((NPC.HitModifiers) ref modifiers).SetCrit();
      ref StatModifier local1 = ref modifiers.FinalDamage;
      local1 = StatModifier.op_Multiply(local1, 1.5f);
      this.Player.ClearBuff(ModContent.BuffType<FirstStrikeBuff>());
      // ISSUE: reference to a compiler-generated field
      displayClass4200.target.AddBuff(36, 600, false);
    }

    public virtual void OnHitNPCWithProj(
      Projectile proj,
      NPC target,
      NPC.HitInfo hit,
      int damageDone)
    {
      if (target.type == 488 || target.friendly)
        return;
      if (proj.minion)
        this.TryAdditionalAttacks(proj.damage, proj.DamageType);
      this.OnHitNPCEither(target, hit, proj.DamageType, proj);
    }

    private void OnHitNPCEither(
      NPC target,
      NPC.HitInfo hitInfo,
      DamageClass damageClass,
      Projectile projectile = null,
      Item item = null)
    {
      // ISSUE: variable of a compiler-generated type
      FargoSoulsPlayer.\u003C\u003Ec__DisplayClass422_0 displayClass4220;
      // ISSUE: reference to a compiler-generated field
      displayClass4220.hitInfo = hitInfo;
      // ISSUE: reference to a compiler-generated field
      displayClass4220.projectile = projectile;
      // ISSUE: reference to a compiler-generated field
      displayClass4220.item = item;
      // ISSUE: reference to a compiler-generated field
      displayClass4220.\u003C\u003E4__this = this;
      if (this.StyxSet)
      {
        // ISSUE: reference to a compiler-generated field
        this.StyxMeter += ((NPC.HitInfo) ref displayClass4220.hitInfo).Damage;
        if (this.StyxTimer <= 0 && !target.friendly && target.lifeMax > 5 && target.type != 488)
          this.StyxTimer = 60;
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (this.Player.HasEffect<TitaniumEffect>() && (displayClass4220.projectile == null || displayClass4220.projectile.type != 908))
        TitaniumEffect.TitaniumShards(this, this.Player);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      if (this.DevianttHeartItem != null && this.DevianttHeartsCD <= 0 && this.Player.HasEffect<DevianttHearts>() && (displayClass4220.projectile == null || displayClass4220.projectile.type != ModContent.ProjectileType<FriendRay>() && displayClass4220.projectile.type != ModContent.ProjectileType<FriendHeart>()))
      {
        this.DevianttHeartsCD = this.AbomWandItem == null ? 600 : 300;
        if (Main.myPlayer == ((Entity) this.Player).whoAmI)
        {
          Vector2 vector2_1 = Vector2.op_Multiply(300f, ((Entity) this.Player).DirectionFrom(Main.MouseWorld));
          for (int index = -3; index <= 3; ++index)
          {
            Vector2 center = ((Entity) this.Player).Center;
            Vector2 vector2_2 = vector2_1;
            double num = Math.PI / 7.0 * (double) index;
            Vector2 vector2_3 = new Vector2();
            Vector2 vector2_4 = vector2_3;
            Vector2 vector2_5 = Utils.RotatedBy(vector2_2, num, vector2_4);
            Vector2 spawn = Vector2.op_Addition(center, vector2_5);
            Vector2 velocity = Vector2.Normalize(Vector2.op_Subtraction(Main.MouseWorld, spawn));
            int rawBaseDamage = this.AbomWandItem == null ? 17 : 170;
            vector2_3 = Vector2.op_Subtraction(Main.MouseWorld, spawn);
            float ai1 = ((Vector2) ref vector2_3).Length() / 17f;
            if (this.MutantEyeItem == null)
              FargoSoulsUtil.NewSummonProjectile(this.Player.GetSource_Accessory(this.DevianttHeartItem, (string) null), spawn, Vector2.op_Multiply(17f, velocity), ModContent.ProjectileType<FriendHeart>(), rawBaseDamage, 3f, ((Entity) this.Player).whoAmI, -1f, ai1);
            else
              FargoSoulsUtil.NewSummonProjectile(this.Player.GetSource_Accessory(this.DevianttHeartItem, (string) null), spawn, velocity, ModContent.ProjectileType<FriendRay>(), rawBaseDamage, 3f, ((Entity) this.Player).whoAmI, 0.448798954f * (float) index);
            Vector2 position = spawn;
            double rotation = (double) Utils.ToRotation(velocity);
            vector2_3 = new Vector2();
            Vector2 addedVel = vector2_3;
            FargoSoulsUtil.HeartDust(position, (float) rotation, addedVel);
          }
        }
      }
      if (this.GodEaterImbue)
        target.AddBuff(ModContent.BuffType<GodEaterBuff>(), 420, false);
      if (this.NymphsPerfume && this.NymphsPerfumeCD <= 0 && !target.immortal && !this.Player.moonLeech)
      {
        this.NymphsPerfumeCD = 600;
        switch (Main.netMode)
        {
          case 0:
            Item.NewItem(((Entity) this.Player).GetSource_OnHit((Entity) target, (string) null), ((Entity) target).Hitbox, 58, 1, false, 0, false, false);
            break;
          case 1:
            ModPacket packet = ((ModType) this).Mod.GetPacket(256);
            ((BinaryWriter) packet).Write((byte) 1);
            ((BinaryWriter) packet).Write((byte) ((Entity) this.Player).whoAmI);
            ((BinaryWriter) packet).Write((byte) ((Entity) target).whoAmI);
            packet.Send(-1, -1);
            break;
        }
      }
      if (this.MasochistSoul)
        target.AddBuff(ModContent.BuffType<SadismBuff>(), 600, false);
      if (this.FusedLens)
      {
        if (this.Player.onFire2 || this.FusedLensCanDebuff)
          target.AddBuff(39, 360, false);
        if (this.Player.ichor || this.FusedLensCanDebuff)
          target.AddBuff(69, 360, false);
      }
      if (this.Supercharged)
      {
        target.AddBuff(144, 240, false);
        target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 60, false);
      }
      if (this.DarkenedHeartItem == null)
        return;
      // ISSUE: reference to a compiler-generated field
      this.DarkenedHeartAttack(displayClass4220.projectile);
    }

    public virtual void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
    {
      if (target.type == 488 || target.friendly)
        return;
      this.OnHitNPCEither(target, hit, item.DamageType, item: item);
    }

    private void ApplyDR(Player player, float dr, ref Player.HurtModifiers modifiers)
    {
      float num = 0.75f;
      player.endurance += dr;
      if (!WorldSavingSystem.EternityMode || (double) this.Player.endurance <= (double) num)
        return;
      player.endurance = num;
    }

    public virtual void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
    {
      float num = 0.0f + NecromanticBrew.NecroBrewDashDR(this.Player);
      if (npc.FargoSouls().Corrupted || npc.FargoSouls().CorruptedForce)
        num += 0.2f;
      if (npc.FargoSouls().BloodDrinker)
        num -= 0.3f;
      if (this.Player.HasBuff(ModContent.BuffType<ShellHideBuff>()))
        --num;
      if (this.Smite)
        num -= 0.2f;
      if (npc.coldDamage && this.Hypothermia)
        num -= 0.2f;
      if (npc.FargoSouls().CurseoftheMoon)
        num += 0.2f;
      this.ApplyDR(this.Player, num + this.Player.AccessoryEffects().ContactDamageDR(npc, ref modifiers), ref modifiers);
    }

    public virtual void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
    {
      float num = 0.0f + NecromanticBrew.NecroBrewDashDR(this.Player);
      if (this.Smite)
        num -= 0.2f;
      if (proj.coldDamage && this.Hypothermia)
        num -= 0.2f;
      this.ApplyDR(this.Player, num + this.Player.AccessoryEffects().ProjectileDamageDR(proj, ref modifiers), ref modifiers);
    }

    public virtual void OnHitByNPC(NPC npc, Player.HurtInfo hurtInfo)
    {
      this.OnHitByEither(npc, (Projectile) null);
    }

    public virtual void OnHitByProjectile(Projectile proj, Player.HurtInfo hurtInfo)
    {
      this.OnHitByEither((NPC) null, proj);
    }

    public void OnHitByEither(NPC npc, Projectile proj)
    {
      if (this.Anticoagulation && Main.myPlayer == ((Entity) this.Player).whoAmI)
      {
        Entity entity = (Entity) null;
        if (npc != null)
          entity = (Entity) npc;
        else if (proj != null)
          entity = (Entity) proj;
        int index1 = ModContent.ProjectileType<Bloodshed>();
        for (int index2 = 0; index2 < 6; ++index2)
        {
          if (Utils.NextBool(Main.rand, this.Player.ownedProjectileCounts[index1] + 2))
            Projectile.NewProjectile(((Entity) this.Player).GetSource_OnHurt(entity, (string) null), ((Entity) this.Player).Center, Utils.NextVector2Circular(Main.rand, 12f, 12f), index1, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
      }
      if (!ModContent.GetInstance<SoulConfig>().BigTossMode)
        return;
      this.AddBuffNoStack(ModContent.BuffType<StunnedBuff>(), 60);
      Vector2 vector2 = new Vector2();
      if (npc != null)
        vector2 = ((Entity) npc).Center;
      else if (proj != null)
        vector2 = ((Entity) proj).Center;
      if (!Vector2.op_Inequality(vector2, new Vector2()))
        return;
      ((Entity) this.Player).velocity = Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) this.Player).Center, vector2)), 30f), 2f);
    }

    public virtual bool CanBeHitByNPC(NPC npc, ref int CooldownSlot)
    {
      return !this.BetsyDashing && !this.GoldShell;
    }

    public virtual bool CanBeHitByProjectile(Projectile proj)
    {
      return !this.BetsyDashing && !this.GoldShell && (!this.Player.HasEffect<PrecisionSealHurtbox>() || proj.Colliding(((Entity) proj).Hitbox, this.GetPrecisionHurtbox()));
    }

    public virtual void ModifyHurt(ref Player.HurtModifiers modifiers)
    {
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.deviBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>()) && EModeGlobalNPC.deviBoss.IsWithinBounds(Main.maxNPCs))
        ((FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss) Main.npc[EModeGlobalNPC.deviBoss].ModNPC).playerInvulTriggered = true;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.abomBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>()) && EModeGlobalNPC.abomBoss.IsWithinBounds(Main.maxNPCs))
        ((FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss) Main.npc[EModeGlobalNPC.abomBoss].ModNPC).playerInvulTriggered = true;
      if (FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && EModeGlobalNPC.mutantBoss.IsWithinBounds(Main.maxNPCs))
        ((FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss) Main.npc[EModeGlobalNPC.mutantBoss].ModNPC).playerInvulTriggered = true;
      if (this.DeathMarked)
      {
        ref StatModifier local = ref modifiers.SourceDamage;
        local = StatModifier.op_Multiply(local, 1.5f);
      }
      if (((Entity) this.Player).whoAmI == Main.myPlayer && !this.noDodge && this.Player.HasEffect<SqueakEffect>() && Utils.NextBool(Main.rand, 10))
      {
        FargoSoulsPlayer.Squeak(((Entity) this.Player).Center);
        ((Player.HurtModifiers) ref modifiers).SetMaxDamage(1);
      }
      // ISSUE: method pointer
      ((Player.HurtModifiers) ref modifiers).ModifyHurtInfo += new Player.HurtModifiers.HurtInfoModifier((object) this, __methodptr(TryParryAttack));
      if (this.StyxSet && !this.BetsyDashing && !this.GoldShell && this.Player.ownedProjectileCounts[ModContent.ProjectileType<StyxArmorScythe>()] > 0)
      {
        // ISSUE: method pointer
        ((Player.HurtModifiers) ref modifiers).ModifyHurtInfo += new Player.HurtModifiers.HurtInfoModifier((object) this, __methodptr(\u003CModifyHurt\u003Eb__432_0));
      }
      if (!this.DeerSinewNerf || this.DeerSinewFreezeCD > 0 || !((Player.HurtModifiers) ref modifiers).DamageSource.SourceNPCIndex.IsWithinBounds(Main.maxNPCs) && (!((Player.HurtModifiers) ref modifiers).DamageSource.SourceProjectileType.IsWithinBounds(Main.maxProjectiles) || Main.projectile[((Player.HurtModifiers) ref modifiers).DamageSource.SourceProjectileType].aiStyle == 10))
        return;
      this.DeerSinewFreezeCD = 120;
      FargoSoulsUtil.AddDebuffFixedDuration(this.Player, 47, 20);
    }

    public virtual void OnHurt(Player.HurtInfo info)
    {
      this.WasHurtBySomething = true;
      this.MahoganyCanUseDR = false;
      if (this.Player.HasBuff(ModContent.BuffType<FargowiltasSouls.Content.Buffs.Souls.TitaniumDRBuff>()) && !this.Player.HasBuff(ModContent.BuffType<TitaniumCDBuff>()))
        this.Player.AddBuff(ModContent.BuffType<TitaniumCDBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames(10f), true, false);
      if (this.NekomiSet && this.NekomiHitCD <= 0)
      {
        this.NekomiHitCD = 90;
        int num1 = 400;
        int num2 = num1;
        int num3 = this.NekomiMeter / num1;
        if (num3 > 1)
          num3 = 1;
        this.Player.AddBuff(58, Luminance.Common.Utilities.Utilities.SecondsToFrames((float) num3) * 5 / 1, true, false);
        this.NekomiMeter -= num2;
        if (this.NekomiMeter < 0)
          this.NekomiMeter = 0;
      }
      if (this.ShellHide)
      {
        --this.TurtleShellHP;
        for (int index1 = 0; index1 < 30; ++index1)
        {
          Vector2 vector2_1 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.UnitY, 5f), (double) (index1 - 14) * 6.2831854820251465 / 30.0, new Vector2()), ((Entity) Main.LocalPlayer).Center);
          Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) Main.LocalPlayer).Center);
          int index2 = Dust.NewDust(Vector2.op_Addition(vector2_1, vector2_2), 0, 0, 228, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].velocity = vector2_2;
        }
      }
      if (this.Defenseless)
      {
        SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        for (int index3 = 0; index3 < 30; ++index3)
        {
          int index4 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 8, 0.0f, 0.0f, 0, new Color(), 2f);
          Main.dust[index4].noGravity = true;
          Dust dust = Main.dust[index4];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
        }
      }
      if (this.Midas && Main.myPlayer == ((Entity) this.Player).whoAmI)
        this.Player.DropCoins();
      this.DeviGrazeBonus = 0.0;
      this.DeviGrazeCounter = 0;
      if (Main.myPlayer != ((Entity) this.Player).whoAmI)
        return;
      if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.mutantBoss, ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()) && EModeGlobalNPC.mutantBoss.IsWithinBounds(Main.maxNPCs))
      {
        if (this.Player.HasBuff(ModContent.BuffType<TimeFrozenBuff>()))
          return;
        this.The22Incident += Main.getGoodWorld ? 2 : 1;
        Rectangle rectangle;
        // ISSUE: explicit constructor call
        ((Rectangle) ref rectangle).\u002Ector((int) ((Entity) this.Player).Center.X - 111, (int) ((Entity) this.Player).Center.Y, 222, 222);
        for (int index = 0; index < this.The22Incident; ++index)
          CombatText.NewText(rectangle, Color.DarkOrange, this.The22Incident, true, false);
        if (this.The22Incident < 22)
          return;
        this.Player.KillMe(PlayerDeathReason.ByCustomReason(Language.GetTextValue("Mods.FargowiltasSouls.DeathMessage.TwentyTwo", (object) this.Player.name)), 22222222.0, 0, false);
        Projectile.NewProjectile(((Entity) this.Player).GetSource_Death((string) null), ((Entity) this.Player).Center, Vector2.Zero, ModContent.ProjectileType<TwentyTwo>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
      }
      else
        this.The22Incident = 0;
    }

    public virtual void ProcessTriggers(TriggersSet triggersSet)
    {
      if (this.Mash)
      {
        this.Player.doubleTapCardinalTimer[0] = 0;
        this.Player.doubleTapCardinalTimer[1] = 0;
        this.Player.doubleTapCardinalTimer[2] = 0;
        this.Player.doubleTapCardinalTimer[3] = 0;
        if (triggersSet.Up)
        {
          if (!this.MashPressed[0])
            ++this.MashCounter;
          this.MashPressed[0] = true;
        }
        else
          this.MashPressed[0] = false;
        if (triggersSet.Left)
        {
          if (!this.MashPressed[1])
            ++this.MashCounter;
          this.MashPressed[1] = true;
        }
        else
          this.MashPressed[1] = false;
        if (triggersSet.Right)
        {
          if (!this.MashPressed[2])
            ++this.MashCounter;
          this.MashPressed[2] = true;
        }
        else
          this.MashPressed[2] = false;
        if (triggersSet.Down)
        {
          if (!this.MashPressed[3])
            ++this.MashCounter;
          this.MashPressed[3] = true;
        }
        else
          this.MashPressed[3] = false;
      }
      if (FargowiltasSouls.FargowiltasSouls.FreezeKey.JustPressed && this.Player.HasEffect<StardustEffect>() && !this.Player.HasBuff(ModContent.BuffType<TimeStopCDBuff>()))
      {
        int num = 60;
        if (this.ForceEffect<StardustEnchant>())
          num = 50;
        if (this.TerrariaSoul)
          num = 40;
        if (this.Eternity)
          num = 30;
        this.Player.ClearBuff(ModContent.BuffType<TimeFrozenBuff>());
        this.Player.AddBuff(ModContent.BuffType<TimeStopCDBuff>(), num * 60, true, false);
        this.FreezeTime = true;
        this.freezeLength = 540;
        SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudo", (SoundType) 0);
        SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
      }
      if (this.PrecisionSeal)
      {
        if (SoulConfig.Instance.PrecisionSealIsHold)
          this.PrecisionSealNoDashNoJump = FargowiltasSouls.FargowiltasSouls.PrecisionSealKey.Current;
        else if (FargowiltasSouls.FargowiltasSouls.PrecisionSealKey.JustPressed)
          this.PrecisionSealNoDashNoJump = !this.PrecisionSealNoDashNoJump;
      }
      else
        this.PrecisionSealNoDashNoJump = false;
      if (this.PrecisionSealNoDashNoJump)
      {
        this.Player.doubleTapCardinalTimer[2] = 0;
        this.Player.doubleTapCardinalTimer[3] = 0;
      }
      if (FargowiltasSouls.FargowiltasSouls.AmmoCycleKey.JustPressed && this.CanAmmoCycle)
        this.AmmoCycleKey();
      if (FargowiltasSouls.FargowiltasSouls.SoulToggleKey.JustPressed)
        FargoUIManager.ToggleSoulToggler();
      if (FargowiltasSouls.FargowiltasSouls.GoldKey.JustPressed && this.Player.HasEffect<GoldEffect>())
        this.GoldKey();
      if (this.GoldShell || this.Player.CCed || this.NoUsingItems > 2)
        return;
      if (FargowiltasSouls.FargowiltasSouls.SmokeBombKey.JustPressed && this.CrystalEnchantActive && this.SmokeBombCD == 0)
        CrystalAssassinEnchant.SmokeBombKey(this);
      if (FargowiltasSouls.FargowiltasSouls.SpecialDashKey.JustPressed && (this.BetsysHeartItem != null || this.QueenStingerItem != null))
        this.SpecialDashKey();
      if (FargowiltasSouls.FargowiltasSouls.MagicalBulbKey.JustPressed && this.MagicalBulb)
        this.MagicalBulbKey();
      if (this.FrigidGemstoneItem != null)
      {
        if (this.FrigidGemstoneCD > 0)
          --this.FrigidGemstoneCD;
        if (FargowiltasSouls.FargowiltasSouls.FrigidSpellKey.Current)
          this.FrigidGemstoneKey();
      }
      if (FargowiltasSouls.FargowiltasSouls.BombKey.JustPressed)
        this.BombKey();
      if (!FargowiltasSouls.FargowiltasSouls.DebuffInstallKey.JustPressed)
        return;
      this.DebuffInstallKey();
    }

    public virtual void PreUpdate()
    {
      this.Toggler.TryLoad();
      if (this.Player.CCed)
      {
        this.Player.doubleTapCardinalTimer[2] = 2;
        this.Player.doubleTapCardinalTimer[3] = 2;
      }
      if (this.HurtTimer > 0)
        --this.HurtTimer;
      this.IsStandingStill = (double) Math.Abs(((Entity) this.Player).velocity.X) < 0.05 && (double) Math.Abs(((Entity) this.Player).velocity.Y) < 0.05;
      if (!this.Infested && !this.FirstInfection)
        this.FirstInfection = true;
      if (this.Unstable && ((Entity) this.Player).whoAmI == Main.myPlayer)
      {
        if (this.unstableCD == 0)
        {
          Vector2 position = ((Entity) this.Player).position;
          int num1 = Main.rand.Next((int) position.X - 500, (int) position.X + 500);
          int num2 = Main.rand.Next((int) position.Y - 500, (int) position.Y + 500);
          Vector2 vector2;
          // ISSUE: explicit constructor call
          ((Vector2) ref vector2).\u002Ector((float) num1, (float) num2);
          while (Collision.SolidCollision(vector2, ((Entity) this.Player).width, ((Entity) this.Player).height) && (double) vector2.X > 50.0 && (double) vector2.X < (double) (Main.maxTilesX * 16 - 50) && (double) vector2.Y > 50.0 && (double) vector2.Y < (double) (Main.maxTilesY * 16 - 50))
          {
            int num3 = Main.rand.Next((int) position.X - 500, (int) position.X + 500);
            int num4 = Main.rand.Next((int) position.Y - 500, (int) position.Y + 500);
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2).\u002Ector((float) num3, (float) num4);
          }
          this.Player.Teleport(vector2, 1, 0);
          NetMessage.SendData(65, -1, -1, (NetworkText) null, 0, (float) ((Entity) this.Player).whoAmI, vector2.X, vector2.Y, 1, 0, 0);
          this.unstableCD = 60;
        }
        --this.unstableCD;
      }
      if (this.OxygenTank)
        RustedOxygenTank.PassiveEffect(this.Player);
      if (this.GoldShell)
        this.GoldUpdate();
      if (this.MonkDashing > 0)
      {
        --this.MonkDashing;
        if (this.MonkDashing != 0 || !this.Player.mount.Active || (double) ((Vector2) ref ((Entity) this.Player).velocity).Length() <= (double) this.Player.mount._data.dashSpeed)
          return;
        float num = ((Vector2) ref ((Entity) this.Player).velocity).Length() / this.Player.mount._data.dashSpeed;
        Player player = this.Player;
        ((Entity) player).velocity = Vector2.op_Multiply(((Entity) player).velocity, 1f / num);
      }
      else
      {
        if (this.MonkDashing >= 0)
          return;
        ++this.MonkDashing;
        if ((double) ((Entity) this.Player).velocity.Y > 0.0)
        {
          this.Player.maxFallSpeed *= 10f;
          this.Player.gravity = 8f;
        }
        if (this.MonkDashing != 0 || !this.Player.mount.Active)
          return;
        Player player = this.Player;
        ((Entity) player).velocity = Vector2.op_Multiply(((Entity) player).velocity, 0.5f);
      }
    }

    public virtual void PostUpdate()
    {
      if (!this.FreeEaterSummon && !((IEnumerable<NPC>) Main.npc).Any<NPC>((Func<NPC, bool>) (n =>
      {
        if (!((Entity) n).active)
          return false;
        return n.type == 13 || n.type == 14 || n.type == 15;
      })))
        this.FreeEaterSummon = true;
      if (this.NymphsPerfumeRestoreLife > 0 && --this.NymphsPerfumeRestoreLife == 0 && this.Player.statLife < this.Player.statLifeMax2)
        this.Player.statLife = this.Player.statLifeMax2;
      if (this.SquireEnchantActive && this.BaseMountType != -1)
        SquireEnchant.ResetMountStats(this);
      this.ConcentratedRainbowMatterTryAutoHeal();
    }

    public virtual void PostUpdateBuffs()
    {
      if (this.Berserked && !this.Player.CCed && this.Player.HeldItem != null && this.Player.HeldItem.IsWeapon())
      {
        this.Player.controlUseItem = true;
        this.Player.releaseUseItem = true;
      }
      if (!this.LowGround)
        return;
      this.Player.gravControl = false;
      this.Player.gravControl2 = false;
    }

    public virtual void PostUpdateEquips()
    {
      if (this.Graze && this.NekomiSet)
        this.GrazeRadius *= this.DeviGraze || this.CirnoGraze ? 1.5f : 0.75f;
      if (this.DeerSinew)
        this.Player.AddEffect<DeerSinewEffect>(ModContent.GetInstance<FargowiltasSouls.Content.Items.Consumables.DeerSinew>().Item);
      if (this.NoMomentum && !this.Player.mount.Active)
      {
        if (this.Player.vortexStealthActive && (double) Math.Abs(((Entity) this.Player).velocity.X) > 6.0)
          this.Player.vortexStealthActive = false;
        this.Player.runAcceleration *= 5f;
        this.Player.runSlowdown *= 5f;
        if (!this.IsStillHoldingInSameDirectionAsMovement)
          this.Player.runSlowdown += 7f;
      }
      if (this.TribalCharmEquipped)
        FargowiltasSouls.Content.Items.Accessories.Masomode.TribalCharm.Effects(this);
      if (this.DarkenedHeartItem != null && !this.IsStillHoldingInSameDirectionAsMovement)
        this.Player.runSlowdown += 0.2f;
      if (!this.Player.HasEffect<StardustEffect>())
        this.FreezeTime = false;
      this.UpdateShield();
      this.Player.wingTimeMax = (int) ((double) this.Player.wingTimeMax * (double) this.WingTimeModifier);
      if (this.MutantAntibodies && ((Entity) this.Player).wet)
      {
        this.Player.wingTime = (float) this.Player.wingTimeMax;
        this.Player.AddBuff(ModContent.BuffType<RefreshedBuff>(), Luminance.Common.Utilities.Utilities.SecondsToFrames(30f), true, false);
      }
      if (this.StyxSet)
      {
        this.Player.accDreamCatcher = true;
        if (this.StyxTimer > 0 && --this.StyxTimer == 1)
        {
          int dps = this.Player.getDPS();
          if (dps != 0)
          {
            int num = 37500 - dps;
            if (num > 0)
              this.StyxMeter += num / 2;
          }
        }
      }
      else
      {
        this.StyxMeter = 0;
        this.StyxTimer = 0;
      }
      if (!this.GaiaSet)
        this.GaiaOffense = false;
      if (!this.EridanusSet)
        this.EridanusEmpower = false;
      if (this.RabiesVaccine)
        this.Player.buffImmune[148] = true;
      if (this.AbomWandItem != null)
        this.AbomWandUpdate();
      if (this.Flipped && !this.Player.gravControl)
      {
        this.Player.gravControl = true;
        this.Player.controlUp = false;
        this.Player.gravDir = -1f;
      }
      if (this.DevianttHeartItem != null && this.DevianttHeartsCD > 0)
        --this.DevianttHeartsCD;
      if ((this.BetsysHeartItem != null || this.QueenStingerItem != null) && this.SpecialDashCD > 0 && --this.SpecialDashCD == 0)
      {
        SoundEngine.PlaySound(ref SoundID.Item9, new Vector2?(((Entity) this.Player).Center), (SoundUpdateCallback) null);
        for (int index1 = 0; index1 < 30; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
      }
      if ((double) ((Entity) this.Player).velocity.Y == 0.0)
        this.CanSummonForbiddenStorm = true;
      if (this.SlimyShieldItem != null || this.LihzahrdTreasureBoxItem != null || this.GelicWingsItem != null)
        this.OnLandingEffects();
      if (this.noDodge)
      {
        this.Player.onHitDodge = false;
        this.Player.shadowDodge = false;
        this.Player.blackBelt = false;
        this.Player.brainOfConfusionItem = (Item) null;
      }
      if (this.Player.dashType != 0)
        this.HasDash = true;
      if (this.PrecisionSealNoDashNoJump)
      {
        this.Player.dashType = 0;
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.CloudInABottle)).Disable();
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.SandstormInABottle)).Disable();
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.BlizzardInABottle)).Disable();
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.FartInAJar)).Disable();
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.TsunamiInABottle)).Disable();
        ((ExtraJumpState) ref this.Player.GetJumpState<ExtraJump>(ExtraJump.UnicornMount)).Disable();
        this.JungleJumping = false;
        this.CanJungleJump = false;
        this.DashCD = 2;
        this.IsDashingTimer = 0;
        this.HasDash = false;
        this.Player.dashDelay = 10;
        if (this.fastFallCD < 2)
          this.fastFallCD = 2;
      }
      if (this.Player.dashDelay > 0 && this.DashCD > 0)
        this.Player.dashDelay = Math.Max(this.DashCD, this.Player.dashDelay);
      DashManager.AddDashes(this.Player);
      DashManager.ManageDashes(this.Player);
      if (this.LihzahrdTreasureBoxItem != null || this.Player.HasEffect<DeerclawpsDive>())
        this.TryFastfallUpdate();
      if (this.Player.HasEffect<DeerclawpsEffect>() && this.IsInADashState)
        DeerclawpsEffect.DeerclawpsAttack(this.Player, ((Entity) this.Player).Bottom);
      if (this.NecromanticBrewItem != null && this.IsInADashState && this.Player.HasEffect<NecroBrewSpin>())
      {
        float num = 0.5f * ((Entity) this.Player).velocity.X;
        ((Entity) this.Player).position.X += num;
        if (Collision.SolidCollision(((Entity) this.Player).position, ((Entity) this.Player).width, ((Entity) this.Player).height))
          ((Entity) this.Player).position.X -= num;
        this.Player.noKnockback = true;
        this.Player.thorns = 4f;
        this.NecromanticBrewRotation += 0.6f * (float) Math.Sign((double) ((Entity) this.Player).velocity.X == 0.0 ? (float) ((Entity) this.Player).direction : ((Entity) this.Player).velocity.X);
        this.Player.fullRotation = this.NecromanticBrewRotation;
        this.Player.fullRotationOrigin = Vector2.op_Subtraction(((Entity) this.Player).Center, ((Entity) this.Player).position);
      }
      else
      {
        if ((double) this.NecromanticBrewRotation == 0.0)
          return;
        this.Player.fullRotation = 0.0f;
        this.NecromanticBrewRotation = 0.0f;
      }
    }

    public virtual void UpdateBadLifeRegen()
    {
      if (this.Player.electrified && ((Entity) this.Player).wet)
        this.Player.lifeRegen -= 16;
      if (this.NanoInjection)
        DamageOverTime(10);
      if (this.Shadowflame)
        DamageOverTime(10);
      if (this.GodEater)
      {
        DamageOverTime(170, true);
        this.Player.lifeRegenCount -= 70;
      }
      if (this.MutantNibble)
        DamageOverTime(0, true);
      if (this.Infested)
        DamageOverTime(this.InfestedExtraDot());
      if (this.Rotting)
        DamageOverTime(2);
      if (this.CurseoftheMoon)
        DamageOverTime(20);
      if (this.Oiled && this.Player.lifeRegen < 0)
        this.Player.lifeRegen *= 2;
      if (this.MutantPresence && this.Player.lifeRegen > 5)
        this.Player.lifeRegen = 5;
      if (this.FlamesoftheUniverse)
        DamageOverTime(79, true);
      if (this.Smite)
        DamageOverTime(0, true);
      if (this.Anticoagulation)
        DamageOverTime(4, true);
      if (this.Player.onFire && this.Player.HasEffect<AshWoodEffect>())
        this.Player.lifeRegen += 8;
      if (this.Player.lifeRegen < 0)
      {
        LeadEffect.ProcessLeadEffectLifeRegen(this.Player);
        this.FusedLensCanDebuff = true;
      }
      if (!WorldSavingSystem.EternityMode || WorldSavingSystem.MasochistModeReal || this.Player.lifeRegen >= 0 || this.Player.statLife >= 10)
        return;
      this.Player.lifeRegen = 0;

      void DamageOverTime(int badLifeRegen, bool affectLifeRegenCount = false)
      {
        if (this.Player.lifeRegen > 0)
          this.Player.lifeRegen = 0;
        if (affectLifeRegenCount && this.Player.lifeRegenCount > 0)
          this.Player.lifeRegen = 0;
        this.Player.lifeRegenTime = 0.0f;
        this.Player.lifeRegen -= badLifeRegen;
      }
    }

    public virtual void PostUpdateMiscEffects()
    {
      if (this.ToggleRebuildCooldown > 0)
        --this.ToggleRebuildCooldown;
      if (this.SquireEnchantActive)
        this.Player.setSquireT2 = true;
      if (this.ValhallaEnchantActive)
        this.Player.setSquireT3 = true;
      if (this.ApprenticeEnchantActive)
        this.Player.setApprenticeT2 = true;
      if (this.DarkArtistEnchantActive)
        this.Player.setApprenticeT3 = true;
      if (this.MonkEnchantActive)
        this.Player.setMonkT2 = true;
      if (this.ShinobiEnchantActive)
        this.Player.setMonkT3 = true;
      if (this.Player.channel && this.WeaponUseTimer < 2)
        this.WeaponUseTimer = 2;
      if (--this.WeaponUseTimer < 0)
        this.WeaponUseTimer = 0;
      if (this.IsDashingTimer > 0)
      {
        --this.IsDashingTimer;
        this.Player.dashDelay = -1;
      }
      if (this.GoldEnchMoveCoins)
      {
        ChestUI.MoveCoins(this.Player.inventory, this.Player.bank.item, ContainerTransferContext.FromUnknown(this.Player));
        this.GoldEnchMoveCoins = false;
      }
      if (this.SpectreCD > 0)
        --this.SpectreCD;
      if (this.ChargeSoundDelay > 0)
        --this.ChargeSoundDelay;
      if (this.RustRifleReloading && this.Player.HeldItem.type == ModContent.ItemType<NavalRustrifle>())
        ++this.RustRifleTimer;
      if (this.ParryDebuffImmuneTime > 0)
      {
        --this.ParryDebuffImmuneTime;
        this.DreadShellVulnerabilityTimer = 0;
      }
      else if (this.DreadShellVulnerabilityTimer > 0)
      {
        --this.DreadShellVulnerabilityTimer;
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Subtraction(player.statDefense, 30);
        this.Player.endurance -= 0.3f;
      }
      if (this.HallowHealTime > 0)
      {
        if (this.Player.HasEffect<HallowEffect>() && this.HallowHealTime % 60 == 0)
          this.Player.Heal(this.Player.ForceEffect<HallowEffect>() ? 17 : 14);
        --this.HallowHealTime;
      }
      if (++this.frameCounter >= 60)
        this.frameCounter = 0;
      if (this.HealTimer > 0)
        --this.HealTimer;
      if (this.LowGround)
      {
        this.Player.waterWalk = false;
        this.Player.waterWalk2 = false;
      }
      if (this.DashCD > 0)
        --this.DashCD;
      if (this.ReallyAwfulDebuffCooldown > 0)
        --this.ReallyAwfulDebuffCooldown;
      if (this.OceanicMaul && FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.fishBossEX, 370))
      {
        this.Player.statLifeMax2 /= 5;
        if (this.Player.statLifeMax2 < 100)
          this.Player.statLifeMax2 = 100;
      }
      if (this.StealingCooldown > 0 && !this.Player.dead)
        --this.StealingCooldown;
      if (this.LihzahrdCurse)
      {
        Tile tileSafely = Framing.GetTileSafely(((Entity) this.Player).Center);
        if (((Tile) ref tileSafely).WallType == (ushort) 87)
        {
          this.Player.dangerSense = false;
          this.Player.InfoAccMechShowWires = false;
        }
      }
      if (this.Graze)
      {
        if (++this.DeviGrazeCounter > 60)
        {
          this.DeviGrazeCounter = 0;
          if (this.DeviGrazeBonus > 0.0)
            this.DeviGrazeBonus -= 0.01;
        }
        if (this.CirnoGrazeCounter > 0)
          --this.CirnoGrazeCounter;
      }
      if (this.Atrophied)
      {
        ref StatModifier local = ref this.Player.GetDamage(DamageClass.Melee);
        local = StatModifier.op_Multiply(local, 0.01f);
        this.Player.GetCritChance(DamageClass.Melee) /= 100f;
      }
      if (this.Slimed)
      {
        this.Player.moveSpeed *= 0.75f;
        this.Player.jump /= 2;
      }
      if (this.GodEater)
      {
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Multiply(player.statDefense, 0.0f);
        this.Player.endurance = 0.0f;
      }
      if (this.MutantNibble && this.Player.statLife > 0 && this.StatLifePrevious > 0 && this.Player.statLife > this.StatLifePrevious)
        this.Player.statLife = this.StatLifePrevious;
      if (this.Defenseless)
      {
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Subtraction(player.statDefense, 30);
        this.Player.endurance = 0.0f;
        this.Player.longInvince = false;
      }
      if (this.Purified)
      {
        for (int index = Player.MaxBuffs - 1; index >= 0; --index)
        {
          if (this.Player.buffType[index] > 0 && !Main.debuff[this.Player.buffType[index]] && !Main.buffNoTimeDisplay[this.Player.buffType[index]] && !BuffID.Sets.TimeLeftDoesNotDecrease[this.Player.buffType[index]])
          {
            if (!this.KnownBuffsToPurify.ContainsKey(this.Player.buffType[index]))
              this.KnownBuffsToPurify[this.Player.buffType[index]] = true;
            this.Player.DelBuff(index);
          }
        }
        foreach (int key in this.KnownBuffsToPurify.Keys)
          this.Player.buffImmune[key] = true;
      }
      if (this.Asocial)
      {
        this.KillPets();
        this.Player.maxMinions = 0;
        this.Player.maxTurrets = 0;
      }
      else if (this.WasAsocial)
      {
        ((BitsByte) ref this.Player.hideMisc)[0] = this.HidePetToggle0;
        ((BitsByte) ref this.Player.hideMisc)[1] = this.HidePetToggle1;
        this.WasAsocial = false;
      }
      if (this.Rotting)
      {
        this.Player.moveSpeed *= 0.9f;
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Subtraction(player.statDefense, 10);
        this.Player.endurance -= 0.1f;
        this.AttackSpeed -= 0.1f;
        ref StatModifier local = ref this.Player.GetDamage(DamageClass.Generic);
        local = StatModifier.op_Subtraction(local, 0.1f);
      }
      if (this.Kneecapped)
        this.Player.accRunSpeed = this.Player.maxRunSpeed;
      this.ManageLifeReduction();
      if (this.Eternity)
        this.Player.statManaMax2 = 999;
      else if (this.UniverseSoul)
        this.Player.statManaMax2 += 300;
      if (this.Player.HasEffect<CelestialRuneAttacks>() && this.AdditionalAttacksTimer > 0)
        --this.AdditionalAttacksTimer;
      if (this.MutantPresence || this.DevianttPresence)
      {
        Player player = this.Player;
        player.statDefense = Player.DefenseStat.op_Division(player.statDefense, 2f);
        this.Player.endurance /= 2f;
        this.Player.shinyStone = false;
      }
      if (this.RockeaterDistance > 600)
        --this.RockeaterDistance;
      else
        this.RockeaterDistance = 600;
      this.StatLifePrevious = this.Player.statLife;
    }
  }
}
