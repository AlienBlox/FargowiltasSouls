// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Boss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.AbomBoss
{
  [AutoloadBossHead]
  public class AbomBoss : ModNPC
  {
    public bool playerInvulTriggered;
    private bool droppedSummon;
    public int ritualProj;
    public int ringProj;
    public int spriteProj;
    public int ritualProjMaso;
    public int ritualProjFTW;
    private string TownNPCName;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 9);
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
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[3]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
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
      this.NPC.damage = 240;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 750000;
      if (Main.expertMode)
        this.NPC.lifeMax *= 2;
      if (WorldSavingSystem.MasochistModeReal)
        this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * 0.89999997615814209);
      this.NPC.value = (float) Item.buyPrice(5, 0, 0, 0);
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
      this.Music = 85;
      Mod mod;
      if (Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
      {
        if (FargoSoulsUtil.AprilFools && mod.Version >= Version.Parse("0.1.5.1"))
          this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/TomMorello");
        else if (mod.Version >= Version.Parse("0.1.5"))
          this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Laevateinn_P1");
        else
          this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Stigma");
      }
      this.SceneEffectPriority = (SceneEffectPriority) 7;
      if (!FargoSoulsUtil.AprilFools)
        return;
      this.NPC.GivenName = Language.GetTextValue("Mods.FargowiltasSouls.NPCs.AbomBoss_April.DisplayName");
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.damage = (int) ((double) this.NPC.damage * 0.5);
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      CooldownSlot = 1;
      return (double) ((Entity) this.NPC).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) target, ((Entity) this.NPC).Center)) < 42.0 && (double) this.NPC.ai[0] != 0.0 && (double) this.NPC.ai[0] != 10.0 && (double) this.NPC.ai[0] != 18.0;
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return target.type != ModContent.Find<ModNPC>("Fargowiltas", "Deviantt").Type && target.type != ModContent.Find<ModNPC>("Fargowiltas", "Abominationn").Type && target.type != ModContent.Find<ModNPC>("Fargowiltas", "Mutant").Type && base.CanHitNPC(target);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.NPC.localAI[3]);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      ModNPC modNpc;
      if (!ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc))
        return;
      int firstNpc = NPC.FindFirstNPC(modNpc.Type);
      if (firstNpc == -1 || firstNpc == Main.maxNPCs)
        return;
      ((Entity) this.NPC).Bottom = ((Entity) Main.npc[firstNpc]).Bottom;
      this.TownNPCName = Main.npc[firstNpc].GivenName;
      Main.npc[firstNpc].life = 0;
      ((Entity) Main.npc[firstNpc]).active = false;
      if (Main.netMode != 2)
        return;
      NetMessage.SendData(23, -1, -1, (NetworkText) null, firstNpc, 0.0f, 0.0f, 0.0f, 0, 0, 0);
    }

    public virtual bool PreAI()
    {
      EModeGlobalNPC.abomBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(true);
        if (this.NPC.timeLeft < 30)
          this.NPC.timeLeft = 30;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) < 1500.0)
        {
          this.NPC.localAI[3] = 1f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          this.NPC.localAI[0] = (float) Main.rand.Next(3);
          this.NPC.localAI[1] = (float) Main.rand.Next(2);
        }
      }
      else if ((double) this.NPC.localAI[3] == 1.0)
        EModeGlobalNPC.Aura(this.NPC, 2000f, true, 86, new Color(), ModContent.BuffType<GodEaterBuff>());
      if (FargoSoulsUtil.HostCheck)
      {
        if (WorldSavingSystem.EternityMode && (double) this.NPC.localAI[3] == 2.0)
        {
          if (FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
          {
            ModContent.ProjectileType<AbomRitual>()
          }) == null)
            this.ritualProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if (WorldSavingSystem.MasochistModeReal)
        {
          if (FargoSoulsUtil.ProjectileExists(this.ritualProjMaso, new int[1]
          {
            ModContent.ProjectileType<AbomRitualMaso>()
          }) == null)
            this.ritualProjMaso = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomRitualMaso>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if (Main.getGoodWorld && (double) this.NPC.localAI[3] == 2.0)
        {
          if (FargoSoulsUtil.ProjectileExists(this.ritualProjFTW, new int[1]
          {
            ModContent.ProjectileType<AbomRitualFTW>()
          }) == null)
            this.ritualProjFTW = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomRitualFTW>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if (FargoSoulsUtil.ProjectileExists(this.ringProj, new int[1]
        {
          ModContent.ProjectileType<AbomRitual2>()
        }) == null)
          this.ringProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomRitual2>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        if (FargoSoulsUtil.ProjectileExists(this.spriteProj, new int[1]
        {
          ModContent.ProjectileType<AbomBossProjectile>()
        }) == null)
        {
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
            if (index1 >= 0 && Main.netMode == 0)
            {
              Projectile projectile = Main.projectile[index1];
              projectile.SetDefaults(ModContent.ProjectileType<AbomBossProjectile>());
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
          }
          else
            this.spriteProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomBossProjectile>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
      }
      if (((Entity) Main.LocalPlayer).active && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0)
      {
        if (WorldSavingSystem.EternityMode)
          Main.LocalPlayer.AddBuff(ModContent.BuffType<AbomPresenceBuff>(), 2, true, false);
        if (this.NPC.life == 1 && WorldSavingSystem.MasochistModeReal)
        {
          Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeStopCDBuff>(), 2, true, false);
          Main.LocalPlayer.AddBuff(ModContent.BuffType<GoldenStasisCDBuff>(), 2, true, false);
        }
      }
      if ((double) this.NPC.localAI[3] == 2.0)
      {
        this.Music = 85;
        Mod mod;
        if (Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
        {
          if (FargoSoulsUtil.AprilFools && mod.Version >= Version.Parse("0.1.5.1"))
            this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Gigachad");
          else if (mod.Version >= Version.Parse("0.1.5"))
            this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Laevateinn_P2");
          else
            this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/Stigma");
        }
        if (WorldSavingSystem.EternityMode)
        {
          GameModeData gameModeInfo = Main.GameModeInfo;
          if (((GameModeData) ref gameModeInfo).IsJourneyMode && ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).Enabled)
            ((CreativePowers.ASharedTogglePower) CreativePowerManager.Instance.GetPower<CreativePowers.FreezeTime>()).SetPowerInfo(false);
          if (!((EffectManager<CustomSky>) SkyManager.Instance)["FargowiltasSouls:AbomBoss"].IsActive())
            ((EffectManager<CustomSky>) SkyManager.Instance).Activate("FargowiltasSouls:AbomBoss", new Vector2(), Array.Empty<object>());
          Main.dayTime = true;
          Main.time = 27000.0;
          Main.raining = false;
          Main.rainTime = 0.0;
          Main.maxRaining = 0.0f;
          Main.eclipse = true;
        }
      }
      return base.PreAI();
    }

    public virtual void AI()
    {
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      switch (this.NPC.ai[0])
      {
        case -4f:
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
          this.NPC.dontTakeDamage = true;
          for (int index1 = 0; index1 < 5; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
          }
          if ((double) ++this.NPC.ai[1] > 180.0)
          {
            if (FargoSoulsUtil.HostCheck)
            {
              int num1 = WorldSavingSystem.MasochistModeReal ? 2 : 1;
              int num2 = WorldSavingSystem.MasochistModeReal ? 120 : 30;
              for (int index = 0; index < num2; ++index)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply((float) num1, Utils.RotatedBy(Vector2.UnitX, Main.rand.NextDouble() * Math.PI, new Vector2())), Utils.NextFloat(Main.rand, 30f)), ModContent.ProjectileType<AbomDeathScythe>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 10f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              ModNPC modNpc;
              if (ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
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
              Main.eclipse = false;
              NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
            this.NPC.life = 0;
            this.NPC.dontTakeDamage = false;
            this.NPC.checkDead();
            break;
          }
          break;
        case -3f:
          if (this.AliveCheck(player))
          {
            NPC npc2 = this.NPC;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.9f);
            this.NPC.dontTakeDamage = true;
            if ((double) ++this.NPC.ai[1] > 120.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = 15f;
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case -2f:
          if (this.AliveCheck(player))
          {
            NPC npc3 = this.NPC;
            ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, 0.9f);
            this.NPC.dontTakeDamage = true;
            for (int index3 = 0; index3 < 5; ++index3)
            {
              int index4 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 2.5f);
              Main.dust[index4].noGravity = true;
              Dust dust = Main.dust[index4];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
            }
            if ((double) ++this.NPC.ai[1] > 180.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = 9f;
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case -1f:
          NPC npc4 = this.NPC;
          ((Entity) npc4).velocity = Vector2.op_Multiply(((Entity) npc4).velocity, 0.9f);
          this.NPC.dontTakeDamage = true;
          if (this.NPC.buffType[0] != 0)
            this.NPC.DelBuff(0);
          if ((double) ++this.NPC.ai[1] > 120.0)
          {
            for (int index5 = 0; index5 < 5; ++index5)
            {
              int index6 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index6].noGravity = true;
              Dust dust = Main.dust[index6];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
            this.NPC.localAI[3] = 2f;
            if (WorldSavingSystem.MasochistModeReal)
            {
              int num = (int) ((double) (this.NPC.lifeMax / 90) * (double) Utils.NextFloat(Main.rand, 1f, 1.5f));
              this.NPC.life += num;
              if (this.NPC.life > this.NPC.lifeMax)
                this.NPC.life = this.NPC.lifeMax;
              CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num, false, false);
            }
            if ((double) this.NPC.ai[1] > 210.0)
            {
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          }
          if ((double) this.NPC.ai[1] == 120.0)
          {
            FargoSoulsUtil.ClearFriendlyProjectiles(1);
            if (FargoSoulsUtil.HostCheck)
              this.ritualProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<AbomRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            break;
          }
          break;
        case 0.0f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            this.NPC.dontTakeDamage = false;
            if ((double) this.NPC.localAI[2] == 0.0)
            {
              this.NPC.localAI[2] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center)) + MathHelper.ToRadians(WorldSavingSystem.EternityMode ? 90f : 70f) * Utils.NextFloat(Main.rand, -1f, 1f);
              this.NPC.netUpdate = true;
            }
            Vector2 vector2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(500f, Utils.ToRotationVector2(this.NPC.localAI[2])));
            if ((double) ((Entity) this.NPC).Distance(vector2) > 16.0)
            {
              NPC npc5 = this.NPC;
              ((Entity) npc5).position = Vector2.op_Addition(((Entity) npc5).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition), 3f));
              float num = (double) this.NPC.localAI[3] > 0.0 ? 1f : 2f;
              if ((double) ((Entity) this.NPC).Center.X < (double) vector2.X)
              {
                ((Entity) this.NPC).velocity.X += num;
                if ((double) ((Entity) this.NPC).velocity.X < 0.0)
                  ((Entity) this.NPC).velocity.X += num * 2f;
              }
              else
              {
                ((Entity) this.NPC).velocity.X -= num;
                if ((double) ((Entity) this.NPC).velocity.X > 0.0)
                  ((Entity) this.NPC).velocity.X -= num * 2f;
              }
              if ((double) ((Entity) this.NPC).Center.Y < (double) vector2.Y)
              {
                ((Entity) this.NPC).velocity.Y += num;
                if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
                  ((Entity) this.NPC).velocity.Y += num * 2f;
              }
              else
              {
                ((Entity) this.NPC).velocity.Y -= num;
                if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
                  ((Entity) this.NPC).velocity.Y -= num * 2f;
              }
              if ((double) this.NPC.localAI[3] > 0.0)
              {
                if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > 24.0)
                  ((Entity) this.NPC).velocity.X = (float) (24 * Math.Sign(((Entity) this.NPC).velocity.X));
                if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) > 24.0)
                  ((Entity) this.NPC).velocity.Y = (float) (24 * Math.Sign(((Entity) this.NPC).velocity.Y));
              }
            }
            if ((double) this.NPC.localAI[3] > 0.0)
            {
              ++this.NPC.ai[1];
              if ((double) this.NPC.ai[3] == 0.0)
              {
                this.NPC.ai[3] = 1f;
                if ((double) this.NPC.localAI[3] > 1.0 && WorldSavingSystem.MasochistModeReal)
                {
                  int num = (double) this.NPC.localAI[3] <= 1.0 || !WorldSavingSystem.MasochistModeReal ? 3 : 6;
                  for (int index = 0; index < num; ++index)
                  {
                    float ai2 = (float) index * 6.28318548f / (float) num;
                    FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<AbomSaucer>(), ai0: (float) ((Entity) this.NPC).whoAmI, ai2: ai2, velocity: new Vector2());
                  }
                }
              }
            }
            if ((double) this.NPC.ai[1] > 120.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[1] = WorldSavingSystem.MasochistModeReal ? 45f : 30f;
              this.NPC.localAI[2] = 0.0f;
              if ((double) ++this.NPC.ai[2] > (WorldSavingSystem.MasochistModeReal ? 7.0 : 5.0))
              {
                ++this.NPC.ai[0];
                this.NPC.ai[1] = 0.0f;
                this.NPC.ai[2] = 0.0f;
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 2f);
                break;
              }
              if (FargoSoulsUtil.HostCheck)
              {
                float num3 = (float) ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) / 30.0 * 2.0);
                float num4 = (double) this.NPC.localAI[3] > 1.0 ? 1f : 0.0f;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 30f), ModContent.ProjectileType<AbomScytheSplit>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num3, num4, 0.0f);
                float num5 = (float) (3.1415927410125732 * ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1.0 : -1.0));
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -1f : 1f, -1f), ModContent.ProjectileType<AbomStyxGazer>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) ((double) num5 / 60.0 * 2.0), 0.0f);
                break;
              }
              break;
            }
            break;
          }
          break;
        case 1f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            ((Entity) this.NPC).velocity = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
            NPC npc6 = this.NPC;
            ((Entity) npc6).velocity = Vector2.op_Multiply(((Entity) npc6).velocity, (double) this.NPC.localAI[3] <= 1.0 || !WorldSavingSystem.EternityMode ? 6f : 2f);
            int num6 = (double) this.NPC.localAI[3] > 1.0 ? 7 : 6;
            if (WorldSavingSystem.MasochistModeReal)
              ++num6;
            if ((double) --this.NPC.ai[1] < 0.0)
            {
              if ((double) ++this.NPC.ai[2] > 4.0)
              {
                ++this.NPC.ai[0];
                this.NPC.ai[1] = 0.0f;
                this.NPC.ai[2] = 0.0f;
              }
              else
              {
                this.NPC.ai[1] = 80f;
                float num7 = (double) this.NPC.localAI[3] > 1.0 ? 40f : 20f;
                float num8 = (double) this.NPC.localAI[3] > 1.0 ? 90f : 40f;
                float num9 = (double) this.NPC.localAI[3] > 1.0 ? 40f : 10f;
                float num10 = (double) this.NPC.ai[2] % 2.0 == 0.0 ? 0.0f : 0.5f;
                if (FargoSoulsUtil.HostCheck)
                {
                  for (int index = 0; index < num6; ++index)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 6.2831854820251465 / (double) num6 * ((double) index + (double) num10), new Vector2()), num9), ModContent.ProjectileType<AbomScytheFlaming>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num7, num7 + num8, 0.0f);
                }
                SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              }
              this.NPC.netUpdate = true;
              break;
            }
            break;
          }
          break;
        case 2f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            NPC npc7 = this.NPC;
            ((Entity) npc7).velocity = Vector2.op_Multiply(((Entity) npc7).velocity, 0.9f);
            if ((double) this.NPC.ai[2] == 0.0)
            {
              if ((double) this.NPC.localAI[3] > 1.0)
              {
                if ((double) this.NPC.ai[1] == 30.0)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                else if ((double) this.NPC.ai[1] == 210.0)
                {
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  this.NPC.netUpdate = true;
                }
              }
              else if ((double) this.NPC.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            if ((double) ++this.NPC.ai[1] > ((double) this.NPC.ai[2] != 0.0 || (double) this.NPC.localAI[3] <= 1.0 || !WorldSavingSystem.EternityMode ? 30.0 : 240.0))
            {
              this.NPC.netUpdate = true;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              if ((double) ++this.NPC.ai[2] > 5.0)
              {
                ++this.NPC.ai[0];
                this.NPC.ai[2] = 0.0f;
                break;
              }
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) player).Center, ((Entity) player).velocity)), 30f);
              if ((double) this.NPC.localAI[3] > 1.0)
              {
                if (WorldSavingSystem.EternityMode)
                {
                  NPC npc8 = this.NPC;
                  ((Entity) npc8).velocity = Vector2.op_Multiply(((Entity) npc8).velocity, 1.2f);
                }
                for (int index7 = 0; index7 < 128; ++index7)
                {
                  Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index7 * 3.14159274101257 * 2.0 / 128.0, new Vector2())), new Vector2(8f, 16f)), (double) Utils.ToRotation(((Entity) this.NPC).velocity), new Vector2());
                  int index8 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 87, 0.0f, 0.0f, 0, new Color(), 1f);
                  Main.dust[index8].scale = 3f;
                  Main.dust[index8].noGravity = true;
                  Main.dust[index8].position = ((Entity) this.NPC).Center;
                  Main.dust[index8].velocity = Vector2.Zero;
                  Dust dust = Main.dust[index8];
                  dust.velocity = Vector2.op_Addition(dust.velocity, Vector2.op_Addition(Vector2.op_Multiply(vector2, 1.5f), Vector2.op_Multiply(((Entity) this.NPC).velocity, 0.5f)));
                }
                if (FargoSoulsUtil.HostCheck)
                {
                  float num = (float) (4.71238899230957 * ((double) this.NPC.ai[2] % 2.0 == 0.0 ? 1.0 : -1.0));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), -(double) num / 2.0, new Vector2()), ModContent.ProjectileType<AbomStyxGazer>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) ((double) num / 60.0 * 2.0), 0.0f);
                  break;
                }
                break;
              }
              break;
            }
            break;
          }
          break;
        case 3f:
          if (!this.Phase2Check())
          {
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
            ClearFrozen();
            if ((double) this.NPC.localAI[3] > 1.0)
            {
              for (int index9 = 0; index9 < 2; ++index9)
              {
                int index10 = Dust.NewDust(Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(((Entity) this.NPC).velocity, Utils.NextFloat(Main.rand))), 0, 0, 87, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index10].scale = (float) (1.0 + 4.0 * (1.0 - (double) this.NPC.ai[1] / 30.0));
                Main.dust[index10].noGravity = true;
                Dust dust = Main.dust[index10];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 0.1f);
              }
            }
            if ((double) ++this.NPC.ai[3] > 5.0)
            {
              this.NPC.ai[3] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Normalize(((Entity) this.NPC).velocity), ModContent.ProjectileType<AbomPhoenix>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if ((double) this.NPC.localAI[3] > 1.0)
                {
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), Math.PI / 2.0, new Vector2()), ModContent.ProjectileType<AbomPhoenix>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), -1.0 * Math.PI / 2.0, new Vector2()), ModContent.ProjectileType<AbomPhoenix>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 30.0)
            {
              this.NPC.netUpdate = true;
              --this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 4f:
          if (this.AliveCheck(player))
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            this.NPC.netUpdate = true;
            this.NPC.ai[0] += ++this.NPC.localAI[0];
            if ((double) this.NPC.localAI[0] >= 3.0)
            {
              this.NPC.localAI[0] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 5f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 3f);
            if ((double) ++this.NPC.ai[1] > ((double) this.NPC.localAI[3] > 1.0 ? 75.0 : 90.0))
            {
              this.NPC.ai[1] = 0.0f;
              if ((double) ++this.NPC.ai[2] > 3.0)
              {
                this.NPC.ai[0] = 8f;
                this.NPC.ai[2] = 0.0f;
              }
              else
              {
                if (FargoSoulsUtil.HostCheck)
                {
                  float num11 = (double) this.NPC.localAI[3] > 1.0 ? Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)) : 0.0f;
                  float num12 = 1000f;
                  if ((double) this.NPC.localAI[3] > 1.0 && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > (double) num12 / 2.0)
                    num12 = ((Entity) this.NPC).Distance(((Entity) player).Center);
                  float num13 = num12 / 90f;
                  for (int index = 0; index < 4; ++index)
                  {
                    Vector2 vector2_1 = Utils.RotatedBy(new Vector2(num13, 0.0f), (double) num11 + Math.PI / 2.0 * (double) index, new Vector2());
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<AbomSickleSplit1>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
                    if (!WorldSavingSystem.MasochistModeReal)
                    {
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2_1) + 1.57079637f, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2_1) - 1.57079637f, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2_1) + 0.7853982f, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_1, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, (float) ((double) Utils.ToRotation(vector2_1) + 0.78539818525314331 + 3.1415927410125732), 0.0f);
                    }
                    Vector2 vector2_2 = Utils.RotatedBy(new Vector2(num13, num13), (double) num11 + Math.PI / 2.0 * (double) index, new Vector2());
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<AbomSickleSplit1>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
                    if (!WorldSavingSystem.MasochistModeReal)
                    {
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2_2) + 1.57079637f, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2_2) - 1.57079637f, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, num11 + 1.57079637f * (float) index, 0.0f);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, num11 + (float) (1.5707963705062866 * ((double) index + 0.5)), 0.0f);
                    }
                  }
                }
                SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              }
              this.NPC.netUpdate = true;
              break;
            }
            break;
          }
          break;
        case 6f:
          if (!this.Phase2Check())
          {
            NPC npc9 = this.NPC;
            ((Entity) npc9).velocity = Vector2.op_Multiply(((Entity) npc9).velocity, 0.99f);
            if ((double) this.NPC.ai[2] == 0.0)
            {
              this.NPC.ai[2] = 1f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = -3; index <= 3; ++index)
                {
                  if (index != 0)
                  {
                    Vector2 vector2;
                    // ISSUE: explicit constructor call
                    ((Vector2) ref vector2).\u002Ector(Utils.NextFloat(Main.rand, 40f), Utils.NextFloat(Main.rand, -20f, 20f));
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomFlocko>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) (120 * index), 0.0f);
                  }
                }
                if ((double) this.NPC.localAI[3] > 1.0)
                {
                  Vector2 vector2;
                  // ISSUE: explicit constructor call
                  ((Vector2) ref vector2).\u002Ector(Utils.NextFloat(Main.rand, 40f), Utils.NextFloat(Main.rand, -20f, 20f));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomFlocko2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, -1f, 0.0f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2), ModContent.ProjectileType<AbomFlocko2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, 1f, 0.0f);
                }
                float num = 420f;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.NextVector2CircularEdge(Main.rand, 20f, 20f), ModContent.ProjectileType<AbomFlocko3>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.NextVector2CircularEdge(Main.rand, 20f, 20f), ModContent.ProjectileType<AbomFlocko3>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -num, 0.0f);
                if (!WorldSavingSystem.MasochistModeReal)
                {
                  for (int index11 = -1; index11 <= 1; index11 += 2)
                  {
                    for (int index12 = -1; index12 <= 1; index12 += 2)
                    {
                      int index13 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply((float) (3000 * index11), Vector2.UnitX)), Vector2.op_Multiply(Vector2.UnitY, (float) index12), ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 5f, (float) (220 * index11), 0.0f);
                      if (index13 != Main.maxProjectiles)
                      {
                        Main.projectile[index13].localAI[1] = (float) ((Entity) this.NPC).whoAmI;
                        if (Main.netMode == 2)
                          NetMessage.SendData(27, -1, -1, (NetworkText) null, index13, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                    }
                  }
                }
              }
              SoundEngine.PlaySound(ref SoundID.Item27, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              for (int index14 = 0; index14 < 30; ++index14)
              {
                int index15 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 76, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index15].noGravity = true;
                Main.dust[index15].noLight = true;
                Dust dust = Main.dust[index15];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
              }
            }
            if ((double) ++this.NPC.ai[1] > 420.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = 8f;
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 7f:
          if (!this.Phase2Check())
          {
            NPC npc10 = this.NPC;
            ((Entity) npc10).velocity = Vector2.op_Multiply(((Entity) npc10).velocity, 0.99f);
            if ((double) this.NPC.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -4f, 0.0f);
            if ((double) ++this.NPC.ai[1] > 420.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = 8f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              break;
            }
            if ((double) this.NPC.ai[1] > 60.0)
            {
              if ((double) this.NPC.localAI[3] > 1.0)
              {
                this.NPC.ai[3] = MathHelper.Lerp(this.NPC.ai[3], 1f, 0.05f);
              }
              else
              {
                float rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                while ((double) rotation < -3.1415927410125732)
                  rotation += 6.28318548f;
                while ((double) rotation > 3.1415927410125732)
                  rotation -= 6.28318548f;
                this.NPC.ai[3] = Utils.AngleLerp(this.NPC.ai[3], rotation, 0.05f);
              }
              if ((double) ++this.NPC.ai[2] > 1.0)
              {
                this.NPC.ai[2] = 0.0f;
                SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                if (FargoSoulsUtil.HostCheck)
                {
                  if ((double) this.NPC.localAI[3] > 1.0)
                  {
                    float num = MathHelper.Lerp(180f, 20f, this.NPC.ai[3]);
                    for (int index = -1; index <= 1; index += 2)
                    {
                      Vector2 vector2 = Vector2.op_Multiply(16f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 3.0, new Vector2()));
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(num * (float) index), new Vector2()), ModContent.ProjectileType<AbomLaser>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    }
                  }
                  else
                  {
                    for (int index = 0; index < 2; ++index)
                    {
                      Vector2 vector2 = Vector2.op_Multiply(16f, Utils.RotatedBy(Utils.ToRotationVector2(this.NPC.ai[3]), (Main.rand.NextDouble() - 0.5) * 0.785398185253143 / 2.0, new Vector2()));
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomLaser>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    }
                  }
                }
              }
              if ((double) ++this.NPC.localAI[2] > 60.0)
              {
                this.NPC.localAI[2] = 0.0f;
                int num = WorldSavingSystem.EternityMode ? 5 : 3;
                for (int index = 0; index < num; ++index)
                {
                  Vector2 vector2 = Utils.RotatedByRandom(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 6.2831854820251465 / (double) num * (double) index, new Vector2()), (double) this.NPC.localAI[3] > 1.0 ? 5f : 8f), Utils.NextFloat(Main.rand, 0.9f, 1.1f)), 6.2831854820251465 / (double) num / 3.0);
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomRocket>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) this.NPC.target, (float) Main.rand.Next(25, 36), 0.0f);
                }
                break;
              }
              break;
            }
            if ((double) this.NPC.localAI[3] > 1.0)
            {
              this.NPC.ai[3] = 0.0f;
            }
            else
            {
              this.NPC.ai[3] = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) player).Center)) - 1f / 1000f;
              while ((double) this.NPC.ai[3] < -3.1415927410125732)
                this.NPC.ai[3] += 6.28318548f;
              while ((double) this.NPC.ai[3] > 3.1415927410125732)
                this.NPC.ai[3] -= 6.28318548f;
            }
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int index16 = 0; index16 < 5; ++index16)
            {
              int index17 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
              Main.dust[index17].noGravity = true;
              Dust dust = Main.dust[index17];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
            }
            break;
          }
          break;
        case 8f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            NPC npc11 = this.NPC;
            ((Entity) npc11).velocity = Vector2.op_Multiply(((Entity) npc11).velocity, 0.9f);
            this.NPC.localAI[2] = 0.0f;
            if ((double) ++this.NPC.ai[1] > 120.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.netUpdate = true;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              if ((double) this.NPC.localAI[3] > 1.0 && WorldSavingSystem.EternityMode)
              {
                if ((double) this.NPC.localAI[1] == 0.0)
                {
                  this.NPC.localAI[1] = 1f;
                  this.NPC.ai[0] = 15f;
                  break;
                }
                this.NPC.localAI[1] = 0.0f;
                ++this.NPC.ai[0];
                break;
              }
              this.NPC.ai[0] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 9f:
          if ((double) this.NPC.ai[1] != 0.0 || this.AliveCheck(player))
          {
            ((Entity) this.NPC).velocity = Vector2.Zero;
            this.NPC.localAI[2] = 0.0f;
            if ((double) this.NPC.ai[1] < 60.0)
              FancyFireballs((int) this.NPC.ai[1]);
            if ((double) ++this.NPC.ai[1] == 1.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.ai[3] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (FargoSoulsUtil.HostCheck)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.ToRotationVector2(this.NPC.ai[3]), ModContent.ProjectileType<AbomDeathraySmall>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Utils.ToRotationVector2(this.NPC.ai[3])), ModContent.ProjectileType<AbomDeathraySmall>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                break;
              }
              break;
            }
            if ((double) this.NPC.ai[1] == 61.0)
            {
              for (int index18 = -1; index18 <= 1; index18 += 2)
              {
                Vector2 vector2_3 = Vector2.op_Multiply(Vector2.op_Multiply(Utils.ToRotationVector2(this.NPC.ai[3]), (float) index18), 3f);
                for (int index19 = 0; index19 < 20; ++index19)
                {
                  int index20 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 31, vector2_3.X, vector2_3.Y, 0, new Color(), 3f);
                  Dust dust = Main.dust[index20];
                  dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
                }
                for (int index21 = 1; index21 <= 14; ++index21)
                {
                  double num14 = (double) (index21 * index18) * 100.0 / 30.0;
                  float num15 = index21 % 2 == 0 ? -1f : 1f;
                  Vector2 rotationVector2 = Utils.ToRotationVector2(this.NPC.ai[3]);
                  Vector2 vector2_4 = Vector2.op_Multiply((float) num14, rotationVector2);
                  for (int index22 = 0; index22 < 3; ++index22)
                  {
                    int index23 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 70, vector2_4.X, vector2_4.Y, 0, new Color(), 3f);
                    Dust dust = Main.dust[index23];
                    dust.velocity = Vector2.op_Multiply(dust.velocity, 1.5f);
                    Main.dust[index23].noGravity = true;
                  }
                  if (FargoSoulsUtil.HostCheck)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_4, ModContent.ProjectileType<AbomScytheSpin>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num15, 0.0f);
                }
              }
              break;
            }
            if ((double) this.NPC.ai[1] > 481.0)
            {
              this.NPC.netUpdate = true;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 10f:
          if ((double) this.NPC.ai[1] >= 90.0 || this.AliveCheck(player))
          {
            ClearFrozen();
            if ((double) this.NPC.ai[2] == 0.0 && (double) this.NPC.ai[3] == 0.0)
              this.NPC.ai[2] = ((Entity) this.NPC).Center.X + ((double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).Center.X ? -1400f : 1400f);
            if ((double) this.NPC.localAI[2] == 0.0)
              this.NPC.localAI[2] = (double) this.NPC.ai[2] > (double) ((Entity) this.NPC).Center.X ? -1f : 1f;
            if ((double) this.NPC.ai[1] > 90.0)
              FancyFireballs((int) this.NPC.ai[1] - 90);
            else
              this.NPC.ai[3] = ((Entity) player).Center.Y - 300f;
            Vector2 targetPos;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos).\u002Ector(this.NPC.ai[2], this.NPC.ai[3]);
            this.Movement(targetPos, 1.4f);
            if ((double) ++this.NPC.ai[1] > 150.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.netUpdate = true;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = this.NPC.localAI[2];
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[2] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 11f:
          ((Entity) this.NPC).velocity.X = this.NPC.ai[2] * 18f;
          this.MovementY(((Entity) player).Center.Y - 250f, (double) Math.Abs(((Entity) player).Center.Y - ((Entity) this.NPC).Center.Y) < 200.0 ? 2f : 0.7f);
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
          ClearFrozen();
          if ((double) ++this.NPC.ai[3] > 5.0)
          {
            this.NPC.ai[3] = 0.0f;
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            float num = (float) (2400.0 / (double) Math.Abs(((Entity) this.NPC).velocity.X) * 2.0 - (double) this.NPC.ai[1] + 120.0);
            if ((double) this.NPC.ai[1] <= 15.0)
            {
              num = 0.0f;
            }
            else
            {
              if ((double) this.NPC.localAI[2] != 0.0)
                num = 0.0f;
              if ((double) ++this.NPC.localAI[2] > 2.0)
                this.NPC.localAI[2] = 0.0f;
            }
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), new Vector2()), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), new Vector2())), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 2400.0 / (double) Math.Abs(((Entity) this.NPC).velocity.X))
          {
            this.NPC.netUpdate = true;
            ((Entity) this.NPC).velocity.X = this.NPC.ai[2] * 18f;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            break;
          }
          break;
        case 12f:
          if ((double) this.NPC.ai[1] >= 150.0 || this.AliveCheck(player))
          {
            ClearFrozen();
            ((Entity) this.NPC).velocity.Y = 0.0f;
            NPC npc12 = this.NPC;
            ((Entity) npc12).velocity = Vector2.op_Multiply(((Entity) npc12).velocity, 0.947f);
            this.NPC.ai[3] += ((Vector2) ref ((Entity) this.NPC).velocity).Length();
            if ((double) this.NPC.ai[1] > 150.0)
              FancyFireballs((int) this.NPC.ai[1] - 150);
            if ((double) ++this.NPC.ai[1] > 210.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.netUpdate = true;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 13f:
          ((Entity) this.NPC).velocity.X = this.NPC.ai[2] * -18f;
          this.MovementY(((Entity) player).Center.Y - 250f, (double) Math.Abs(((Entity) player).Center.Y - ((Entity) this.NPC).Center.Y) < 200.0 ? 2f : 0.7f);
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
          ClearFrozen();
          if ((double) ++this.NPC.ai[3] > 5.0)
          {
            this.NPC.ai[3] = 0.0f;
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            float num = (float) (2400.0 / (double) Math.Abs(((Entity) this.NPC).velocity.X) * 2.0 - (double) this.NPC.ai[1] + 120.0);
            if ((double) this.NPC.ai[1] <= 15.0)
            {
              num = 0.0f;
            }
            else
            {
              if ((double) this.NPC.localAI[2] != 0.0)
                num = 0.0f;
              if ((double) ++this.NPC.localAI[2] > 2.0)
                this.NPC.localAI[2] = 0.0f;
            }
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), new Vector2()), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(20f) * (Main.rand.NextDouble() - 0.5), new Vector2())), ModContent.ProjectileType<AbomDeathrayMark>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 2400.0 / (double) Math.Abs(((Entity) this.NPC).velocity.X))
          {
            this.NPC.netUpdate = true;
            ((Entity) this.NPC).velocity.X = this.NPC.ai[2] * -18f;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            break;
          }
          break;
        case 14f:
          if (this.AliveCheck(player))
          {
            NPC npc13 = this.NPC;
            ((Entity) npc13).velocity = Vector2.op_Multiply(((Entity) npc13).velocity, 0.9f);
            if ((double) ++this.NPC.ai[1] > 60.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = this.NPC.dontTakeDamage ? -3f : 0.0f;
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 15f:
          NPC npc14 = this.NPC;
          ((Entity) npc14).velocity = Vector2.op_Multiply(((Entity) npc14).velocity, 0.9f);
          ClearFrozen();
          if ((double) this.NPC.ai[1] < 60.0)
            FancyFireballs((int) this.NPC.ai[1]);
          if ((double) this.NPC.ai[1] == 0.0 && (double) this.NPC.ai[2] != 2.0 && FargoSoulsUtil.HostCheck)
          {
            float num = ((double) this.NPC.ai[2] == 1.0 ? -1f : 1f) * (float) ((double) MathHelper.ToRadians(270f) / 120.0 * -1.0 * 60.0);
            int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f, num, 0.0f);
            if (index != Main.maxProjectiles)
            {
              Main.projectile[index].localAI[1] = (float) ((Entity) this.NPC).whoAmI;
              if (Main.netMode == 2)
                NetMessage.SendData(27, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
          if ((double) ++this.NPC.ai[1] > 90.0)
          {
            this.NPC.netUpdate = true;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 3f);
            break;
          }
          if ((double) this.NPC.ai[1] == 60.0 && FargoSoulsUtil.HostCheck)
          {
            this.NPC.netUpdate = true;
            ((Entity) this.NPC).velocity = Vector2.Zero;
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            float num = ((double) this.NPC.ai[2] == 1.0 ? -1f : 1f) * (MathHelper.ToRadians(270f) / 120f);
            Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), -(double) num * 60.0, new Vector2());
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            if (WorldSavingSystem.MasochistModeReal)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2), ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              break;
            }
            break;
          }
          break;
        case 16f:
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
          if ((double) ++this.NPC.ai[1] > 120.0)
          {
            this.NPC.netUpdate = true;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            break;
          }
          break;
        case 17f:
          if (this.AliveCheck(player))
          {
            ClearFrozen();
            Vector2 targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 500f));
            if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
              this.Movement(targetPos, 0.7f);
            if ((double) ++this.NPC.ai[1] > 60.0)
            {
              this.NPC.netUpdate = true;
              if ((double) ++this.NPC.ai[2] < 2.0)
              {
                this.NPC.ai[0] -= 2f;
              }
              else
              {
                ++this.NPC.ai[0];
                this.NPC.ai[2] = 0.0f;
              }
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 18f:
          if ((double) this.NPC.ai[1] >= 90.0 || this.AliveCheck(player))
          {
            ClearFrozen();
            if ((double) this.NPC.ai[2] == 0.0 && (double) this.NPC.ai[3] == 0.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[2] = ((Entity) player).Center.X;
              this.NPC.ai[3] = ((Entity) player).Center.Y;
              if (FargoSoulsUtil.ProjectileExists(this.ritualProj, new int[1]
              {
                ModContent.ProjectileType<AbomRitual>()
              }) != null)
              {
                this.NPC.ai[2] = ((Entity) Main.projectile[this.ritualProj]).Center.X;
                this.NPC.ai[3] = ((Entity) Main.projectile[this.ritualProj]).Center.Y;
              }
              Vector2 vector2;
              vector2.X = (float) Math.Sign(((Entity) player).Center.X - this.NPC.ai[2]);
              vector2.Y = (float) Math.Sign(((Entity) player).Center.Y - this.NPC.ai[3]);
              this.NPC.localAI[2] = Utils.ToRotation(vector2);
            }
            Vector2 vector2_5 = Vector2.op_Multiply((float) Math.Sqrt(2880000.0), Utils.ToRotationVector2(this.NPC.localAI[2]));
            vector2_5.Y -= (float) (450 * Math.Sign(vector2_5.Y));
            Vector2 targetPos = Vector2.op_Addition(new Vector2(this.NPC.ai[2], this.NPC.ai[3]), vector2_5);
            this.Movement(targetPos, 1f);
            if ((double) this.NPC.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
            {
              double num16 = (double) Math.Sign(this.NPC.ai[2] - targetPos.X);
              float num17 = (float) Math.Sign(this.NPC.ai[3] - targetPos.Y);
              float num18 = num16 > 0.0 ? MathHelper.ToRadians(0.1f) * -num17 : (float) (3.1415927410125732 - (double) MathHelper.ToRadians(0.1f) * -(double) num17);
              float num19 = num16 > 0.0 ? 3.14159274f : 0.0f;
              int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.ToRotationVector2(num18), ModContent.ProjectileType<GlowLine>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 4f, num19, 0.0f);
              if (index != Main.maxProjectiles)
              {
                Main.projectile[index].localAI[1] = (float) ((Entity) this.NPC).whoAmI;
                if (Main.netMode == 2)
                  NetMessage.SendData(27, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
            if ((double) this.NPC.ai[1] > 90.0)
              FancyFireballs((int) this.NPC.ai[1] - 90);
            if ((double) ++this.NPC.ai[1] > 150.0)
            {
              this.NPC.netUpdate = true;
              ((Entity) this.NPC).velocity = Vector2.Zero;
              ++this.NPC.ai[0];
              this.NPC.ai[1] = 0.0f;
              break;
            }
            break;
          }
          break;
        case 19f:
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.ai[2] - ((Entity) this.NPC).Center.X);
          ClearFrozen();
          if ((double) this.NPC.ai[1] == 0.0 && FargoSoulsUtil.HostCheck)
          {
            float num20 = (float) Math.Sign(this.NPC.ai[2] - ((Entity) this.NPC).Center.X);
            float num21 = (float) Math.Sign(this.NPC.ai[3] - ((Entity) this.NPC).Center.Y);
            float num22 = (float) ((double) num20 * 3.1415927410125732 / 60.0) * num21;
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitX, -num20), ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num22, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            if (WorldSavingSystem.MasochistModeReal)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_UnaryNegation(Vector2.UnitX), -num20), ModContent.ProjectileType<AbomSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.5f), 0.0f, Main.myPlayer, num22, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
          if ((double) ++this.NPC.ai[1] > 60.0)
          {
            this.NPC.netUpdate = true;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            ((Entity) this.NPC).velocity.X = 0.0f;
            ((Entity) this.NPC).velocity.Y = (float) (24 * Math.Sign(this.NPC.ai[3] - ((Entity) this.NPC).Center.Y));
            break;
          }
          break;
        case 20f:
          ClearFrozen();
          ((Entity) this.NPC).velocity.Y *= 0.97f;
          NPC npc15 = this.NPC;
          ((Entity) npc15).position = Vector2.op_Addition(((Entity) npc15).position, ((Entity) this.NPC).velocity);
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.ai[2] - ((Entity) this.NPC).Center.X);
          if ((double) ++this.NPC.ai[1] > 90.0)
          {
            this.NPC.netUpdate = true;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            break;
          }
          break;
        case 21f:
          if (this.AliveCheck(player))
          {
            this.NPC.localAI[2] = 0.0f;
            Vector2 center = ((Entity) player).Center;
            center.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
            if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
              this.Movement(center, 0.7f);
            if ((double) ++this.NPC.ai[1] > 60.0)
            {
              this.NPC.netUpdate = true;
              this.NPC.ai[0] = this.NPC.dontTakeDamage ? -4f : 0.0f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              break;
            }
            break;
          }
          break;
        default:
          Main.NewText("UH OH, STINKY", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          this.NPC.netUpdate = true;
          this.NPC.ai[0] = 0.0f;
          goto case 0.0f;
      }
      if ((double) this.NPC.ai[0] >= 9.0 && this.NPC.dontTakeDamage)
      {
        for (int index24 = 0; index24 < 5; ++index24)
        {
          int index25 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1.5f);
          Main.dust[index25].noGravity = true;
          Dust dust = Main.dust[index25];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
        }
      }
      if (player.immune || player.hurtCooldowns[0] != 0 || player.hurtCooldowns[1] != 0)
        this.playerInvulTriggered = true;
      if (!WorldSavingSystem.EternityMode || !NPC.downedMoonlord || WorldSavingSystem.DownedAbom || !FargoSoulsUtil.HostCheck || !this.NPC.HasPlayerTarget || this.droppedSummon)
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) player).Hitbox, ModContent.ItemType<AbomsCurse>(), 1, false, 0, false, false);
      this.droppedSummon = true;

      void FancyFireballs(int repeats)
      {
        float num1 = 0.0f;
        for (int index = 0; index < repeats; ++index)
          num1 = MathHelper.Lerp(num1, 1f, 0.08f);
        float num2 = (float) (1400.0 * (1.0 - (double) num1));
        float num3 = 6.28318548f * num1;
        for (int index1 = 0; index1 < 4; ++index1)
        {
          int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(num2, Utils.RotatedBy(Vector2.UnitX, (double) num3 + 1.5707963705062866 * (double) index1, new Vector2()))), 0, 0, 70, ((Entity) this.NPC).velocity.X * 0.3f, ((Entity) this.NPC).velocity.Y * 0.3f, 0, Color.White, 1f);
          Main.dust[index2].noGravity = true;
          Main.dust[index2].scale = (float) (6.0 - 4.0 * (double) num1);
        }
      }

      void ClearFrozen()
      {
        if (!this.NPC.HasBuff<FrozenBuff>())
          return;
        this.NPC.DelBuff(this.NPC.FindBuffIndex(ModContent.BuffType<FrozenBuff>()));
      }
    }

    private bool AliveCheck(Player player)
    {
      if ((!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 5000.0) && (double) this.NPC.localAI[3] > 0.0)
      {
        this.NPC.TargetClosest(true);
        player = Main.player[this.NPC.target];
        if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 5000.0)
        {
          if (this.NPC.timeLeft > 30)
            this.NPC.timeLeft = 30;
          --((Entity) this.NPC).velocity.Y;
          if (this.NPC.timeLeft == 1)
          {
            if ((double) ((Entity) this.NPC).position.Y < 0.0)
              ((Entity) this.NPC).position.Y = 0.0f;
            if (FargoSoulsUtil.HostCheck)
            {
              FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
              ModNPC modNpc;
              if (ModContent.TryFind<ModNPC>("Fargowiltas", "Abominationn", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
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
              Main.eclipse = false;
              NetMessage.SendData(7, -1, -1, (NetworkText) null, 0, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
          return false;
        }
      }
      if (this.NPC.timeLeft < 600)
        this.NPC.timeLeft = 600;
      if ((double) ((Entity) player).Center.Y / 16.0 <= Main.worldSurface)
        return true;
      ((Entity) this.NPC).velocity.X *= 0.95f;
      --((Entity) this.NPC).velocity.Y;
      if ((double) ((Entity) this.NPC).velocity.Y < -32.0)
        ((Entity) this.NPC).velocity.Y = -32f;
      return false;
    }

    private bool Phase2Check()
    {
      if ((double) this.NPC.localAI[3] > 1.0 || (double) this.NPC.life >= (double) this.NPC.lifeMax * (WorldSavingSystem.EternityMode ? 0.66 : 0.5) || !Main.expertMode)
        return false;
      if (FargoSoulsUtil.HostCheck)
      {
        this.NPC.ai[0] = -1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.netUpdate = true;
        FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
      }
      return true;
    }

    private void Movement(Vector2 targetPos, float speedModifier, bool fastX = true)
    {
      if ((double) Math.Abs(((Entity) this.NPC).Center.X - targetPos.X) > 5.0)
      {
        if ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X)
        {
          ((Entity) this.NPC).velocity.X += speedModifier;
          if ((double) ((Entity) this.NPC).velocity.X < 0.0)
            ((Entity) this.NPC).velocity.X += speedModifier * (fastX ? 2f : 1f);
        }
        else
        {
          ((Entity) this.NPC).velocity.X -= speedModifier;
          if ((double) ((Entity) this.NPC).velocity.X > 0.0)
            ((Entity) this.NPC).velocity.X -= speedModifier * (fastX ? 2f : 1f);
        }
      }
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetPos.Y)
      {
        ((Entity) this.NPC).velocity.Y += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > 24.0)
        ((Entity) this.NPC).velocity.X = (float) (24 * Math.Sign(((Entity) this.NPC).velocity.X));
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= 24.0)
        return;
      ((Entity) this.NPC).velocity.Y = (float) (24 * Math.Sign(((Entity) this.NPC).velocity.Y));
    }

    private void MovementY(float targetY, float speedModifier)
    {
      if ((double) ((Entity) this.NPC).Center.Y < (double) targetY)
      {
        ((Entity) this.NPC).velocity.Y += speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y += speedModifier * 2f;
      }
      else
      {
        ((Entity) this.NPC).velocity.Y -= speedModifier;
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y -= speedModifier * 2f;
      }
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= 24.0)
        return;
      ((Entity) this.NPC).velocity.Y = (float) (24 * Math.Sign(((Entity) this.NPC).velocity.Y));
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<AbomFangBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 120, true, false);
      }
      target.AddBuff(30, 600, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 87, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
    }

    public virtual bool CheckDead()
    {
      if ((double) this.NPC.ai[0] == -4.0 && (double) this.NPC.ai[1] >= 180.0)
        return true;
      this.NPC.life = 1;
      ((Entity) this.NPC).active = true;
      if ((double) this.NPC.localAI[3] < 2.0)
        this.NPC.localAI[3] = 2f;
      if (FargoSoulsUtil.HostCheck && (double) this.NPC.ai[0] > -2.0)
      {
        this.NPC.ai[0] = WorldSavingSystem.MasochistModeReal ? -2f : -4f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[2] = 0.0f;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
        FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
      }
      return false;
    }

    public virtual void OnKill()
    {
      base.OnKill();
      if (!this.playerInvulTriggered && WorldSavingSystem.EternityMode)
        Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).Hitbox, ModContent.ItemType<BrokenHilt>(), 1, false, 0, false, false);
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedAbom, -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npcLoot);
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ModContent.ItemType<AbomEnergy>(), 1, 10, 20, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<AbomBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<AbomTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<AbomRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<BabyScythe>(), 4));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new EModeDropCondition());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AbominableWand>()), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 6.0)
        return;
      this.NPC.frameCounter = 0.0;
      this.NPC.frame.Y += frameHeight;
      if (this.NPC.frame.Y < 4 * frameHeight)
        return;
      this.NPC.frame.Y = 0;
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      return false;
    }
  }
}
