// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.NPCs;
using FargowiltasSouls.Assets.ExtraTextures;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Content.Patreon.Phupperbat;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Deathrays;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.DeviBoss
{
  [AutoloadBossHead]
  public class DeviBoss : ModNPC
  {
    public bool playerInvulTriggered;
    private bool droppedSummon;
    public int[] attackQueue = new int[4];
    public int lastStrongAttack;
    public bool ignoreMoney;
    public int ringProj;
    public int spriteProj;
    public bool DrawRuneBorders;
    private string TownNPCName;

    public ref float AttackIndex => ref this.NPC.localAI[2];

    public ref float Phase => ref this.NPC.localAI[3];

    public ref float State => ref this.NPC.ai[0];

    public ref float Timer => ref this.NPC.ai[1];

    public ref float SubTimer => ref this.NPC.ai[2];

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 4;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPC npc = this.NPC;
      List<int> debuffs = new List<int>();
      CollectionsMarshal.SetCount<int>(debuffs, 7);
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
      span[num5] = 119;
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<LethargicBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<ClippedWingsBuff>();
      int num8 = num7 + 1;
      npc.AddDebuffImmunities(debuffs);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary.DeviBoss")
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
      this.NPC.damage = 64;
      this.NPC.defense = 10;
      this.NPC.lifeMax = 6000;
      if (WorldSavingSystem.EternityMode)
      {
        this.NPC.lifeMax = (int) Math.Round((double) this.NPC.lifeMax * 1.5);
        if (!Main.masterMode)
          this.NPC.lifeMax = (int) Math.Round((double) this.NPC.lifeMax * 1.3999999761581421);
      }
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit9);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.npcSlots = 50f;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.boss = true;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.netAlways = true;
      this.NPC.timeLeft = NPC.activeTime * 30;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, mod.Version >= Version.Parse("0.1.4") ? "Assets/Music/Strawberry_Sparkly_Sunrise" : "Assets/Music/LexusCyanixs") : 88;
      this.SceneEffectPriority = (SceneEffectPriority) 7;
      this.NPC.value = (float) Item.buyPrice(0, 5, 0, 0);
    }

    public virtual void ApplyDifficultyAndPlayerScaling(
      int numPlayers,
      float balance,
      float bossAdjustment)
    {
      this.NPC.damage = (int) ((double) this.NPC.damage * 0.5);
      this.NPC.lifeMax = (int) ((double) this.NPC.lifeMax * (double) balance);
    }

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.AttackIndex);
      writer.Write(this.Phase);
      writer.Write7BitEncodedInt(this.attackQueue[0]);
      writer.Write7BitEncodedInt(this.attackQueue[1]);
      writer.Write7BitEncodedInt(this.attackQueue[2]);
      writer.Write7BitEncodedInt(this.attackQueue[3]);
      writer.Write(this.ignoreMoney);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.AttackIndex = reader.ReadSingle();
      this.Phase = reader.ReadSingle();
      this.attackQueue[0] = reader.Read7BitEncodedInt();
      this.attackQueue[1] = reader.Read7BitEncodedInt();
      this.attackQueue[2] = reader.Read7BitEncodedInt();
      this.attackQueue[3] = reader.Read7BitEncodedInt();
      this.ignoreMoney = reader.ReadBoolean();
    }

    public virtual void OnSpawn(IEntitySource source)
    {
      int firstNpc = NPC.FindFirstNPC(ModContent.NPCType<Deviantt>());
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

    public virtual void AI()
    {
      EModeGlobalNPC.deviBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.Phase == 0.0)
      {
        this.NPC.TargetClosest(true);
        if (this.NPC.timeLeft < 30)
          this.NPC.timeLeft = 30;
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) < 2000.0)
        {
          this.Phase = 1f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          do
          {
            this.RefreshAttackQueue();
          }
          while (this.attackQueue[0] == 3 || this.attackQueue[0] == 5 || this.attackQueue[0] == 9 || this.attackQueue[0] == 10);
        }
      }
      if (FargoSoulsUtil.HostCheck)
      {
        if (!FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss.ProjectileExists(this.ringProj, ModContent.ProjectileType<DeviRitual2>()))
          this.ringProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual2>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        if (!FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss.ProjectileExists(this.spriteProj, ModContent.ProjectileType<DeviBossProjectile>()))
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
              projectile.SetDefaults(ModContent.ProjectileType<DeviBossProjectile>());
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
            this.spriteProj = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviBossProjectile>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
      }
      int projectileDamage = FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, (double) this.Phase > 1.0 ? 1f : 0.8f);
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
      this.DrawRuneBorders = false;
      switch (this.State)
      {
        case -2f:
          if (this.AliveCheck(player))
          {
            Die();
            break;
          }
          break;
        case -1f:
          Phase2Transition();
          break;
        case 0.0f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            SpawnEffects();
            break;
          }
          break;
        case 1f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            PaladinHammers();
            break;
          }
          break;
        case 2f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            HeartBarrages();
            break;
          }
          break;
        case 3f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            WyvernOrbSpiral();
            break;
          }
          break;
        case 4f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            Mimics();
            break;
          }
          break;
        case 5f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            FrostballsNados();
            break;
          }
          break;
        case 6f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            RuneWizard();
            break;
          }
          break;
        case 7f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            MothDustCharges();
            break;
          }
          break;
        case 8f:
          if (!this.Phase2Check())
          {
            WhileDashing();
            break;
          }
          break;
        case 9f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            MageSkeletonAttacks();
            break;
          }
          break;
        case 10f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            BabyGuardians();
            break;
          }
          break;
        case 11f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            GeyserRain();
            break;
          }
          break;
        case 12f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            CrossRayHearts();
            break;
          }
          break;
        case 13f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            Butterflies();
            break;
          }
          break;
        case 14f:
          if (((double) this.Timer >= 420.0 || this.AliveCheck(player)) && !this.Phase2Check())
          {
            MedusaRay();
            break;
          }
          break;
        case 15f:
          SparklingLove();
          break;
        case 16f:
          if (this.AliveCheck(player) && !this.Phase2Check())
          {
            Pause();
            break;
          }
          break;
        case 17f:
          Bribery();
          break;
        default:
          Main.NewText("UH OH, STINKY", byte.MaxValue, byte.MaxValue, byte.MaxValue);
          this.NPC.netUpdate = true;
          this.State = 0.0f;
          goto case 0.0f;
      }
      if (player.immune || player.hurtCooldowns[0] != 0 || player.hurtCooldowns[1] != 0)
        this.playerInvulTriggered = true;
      if (!WorldSavingSystem.EternityMode || WorldSavingSystem.DownedDevi || !FargoSoulsUtil.HostCheck || !this.NPC.HasPlayerTarget || this.droppedSummon)
        return;
      Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) player).Hitbox, ModContent.ItemType<DevisCurse>(), 1, false, 0, false, false);
      this.droppedSummon = true;

      void StrongAttackTeleport(Vector2 teleportTarget = default (Vector2))
      {
        if ((Vector2.op_Equality(teleportTarget, new Vector2()) ? ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 450.0 ? 1 : 0) : ((double) ((Entity) this.NPC).Distance(teleportTarget) < 80.0 ? 1 : 0)) != 0)
          return;
        this.TeleportDust();
        if (FargoSoulsUtil.HostCheck)
        {
          if (Vector2.op_Inequality(teleportTarget, new Vector2()))
            ((Entity) this.NPC).Center = teleportTarget;
          else if (Vector2.op_Equality(((Entity) player).velocity, Vector2.Zero))
            ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(450f, Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI)));
          else
            ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(450f, Vector2.Normalize(((Entity) player).velocity)));
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Division(((Entity) npc).velocity, 2f);
          this.NPC.netUpdate = true;
        }
        this.TeleportDust();
        SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }

      void Die()
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
        this.NPC.dontTakeDamage = true;
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 86, 0.0f, 0.0f, 0, new Color(), 2.5f);
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 12f);
        }
        if ((double) ++this.Timer <= 180.0)
          return;
        int num = ModContent.NPCType<Deviantt>();
        if (FargoSoulsUtil.HostCheck && !NPC.AnyNPCs(num))
        {
          int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, num, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (index != Main.maxNPCs)
          {
            Main.npc[index].homeless = true;
            if (this.TownNPCName != null)
              Main.npc[index].GivenName = this.TownNPCName;
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
        }
        this.NPC.life = 0;
        this.NPC.dontTakeDamage = false;
        this.NPC.checkDead();
      }

      void Phase2Transition()
      {
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
        this.NPC.dontTakeDamage = true;
        if (this.NPC.buffType[0] != 0)
          this.NPC.DelBuff(0);
        if ((double) ++this.Timer > 120.0)
        {
          for (int index1 = 0; index1 < 5; ++index1)
          {
            int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 86, 0.0f, 0.0f, 0, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 8f);
          }
          this.Phase = 2f;
          if (WorldSavingSystem.MasochistModeReal)
          {
            int num = (int) ((double) (this.NPC.lifeMax / 90) * (double) Utils.NextFloat(Main.rand, 1f, 1.5f));
            this.NPC.life += num;
            if (this.NPC.life > this.NPC.lifeMax)
              this.NPC.life = this.NPC.lifeMax;
            CombatText.NewText(((Entity) this.NPC).Hitbox, CombatText.HealLife, num, false, false);
          }
          if ((double) this.Timer <= 240.0)
            return;
          this.RefreshAttackQueue();
          this.attackQueue[3] = 15;
          this.AttackIndex = (float) ((double) this.Phase > 1.0 ? 1 : 0);
          this.GetNextAttack();
        }
        else
        {
          if ((double) this.Timer != 120.0)
            return;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
      }
      Vector2 targetPos;

      void SpawnEffects()
      {
        this.NPC.dontTakeDamage = false;
        targetPos = ((Entity) player).Center;
        targetPos.X += (float) (500 * ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X ? -1 : 1));
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, (double) this.Phase > 0.0 ? 0.15f : 2f, (double) this.Phase > 0.0 ? 12f : 1200f);
        if ((double) this.Phase <= 0.0)
          return;
        this.NPC.netUpdate = true;
        this.GetNextAttack();
      }

      void PaladinHammers()
      {
        ref float local1 = ref this.NPC.localAI[1];
        ref float local2 = ref this.NPC.localAI[0];
        if ((double) local1 == 0.0)
        {
          local1 = (double) this.Phase > 1.0 ? (float) Main.rand.Next(3, 10) : (float) Main.rand.Next(3, 6);
          if ((double) local2 > 0.0)
            local1 = Utils.NextBool(Main.rand) ? 2f : 3f;
          this.NPC.netUpdate = true;
        }
        ((Entity) this.NPC).velocity = Vector2.Zero;
        if ((double) ++this.Timer > ((double) this.Phase > 1.0 ? 10.0 : 20.0) && (double) this.SubTimer < (double) local1)
        {
          this.Timer = 0.0f;
          ++this.SubTimer;
          this.TeleportDust();
          if (FargoSoulsUtil.HostCheck)
          {
            int num = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : 0;
            ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(200f, Utils.RotatedBy(Vector2.UnitX, (double) Utils.NextFloat(Main.rand, 0.0f, 6.28318548f), new Vector2())));
            if ((num != 0 ? ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : 0) : ((double) ((Entity) this.NPC).Center.X > (double) ((Entity) player).Center.X ? 1 : 0)) != 0)
              ((Entity) this.NPC).position.X += (((Entity) player).Center.X - ((Entity) this.NPC).Center.X) * 2f;
            this.NPC.netUpdate = true;
          }
          this.TeleportDust();
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if ((double) this.SubTimer == (double) local1 && FargoSoulsUtil.HostCheck)
          {
            for (int index1 = -1; index1 <= 1; index1 += 2)
            {
              for (int index2 = 0; index2 < 3; ++index2)
              {
                float num = (float) ((double) ((MathHelper.ToRadians(105f) - MathHelper.ToRadians(30f) * (float) index2) * (float) index1) / 60.0 * 2.0);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(Vector2.UnitY), ModContent.ProjectileType<DeviHammerHeld>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num, 0.0f);
              }
            }
          }
        }
        if ((double) this.Timer == 60.0)
        {
          this.NPC.netUpdate = true;
          FargoSoulsUtil.DustRing(((Entity) this.NPC).Center, 36, 246, 9f, new Color(), 3f, true);
          SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (!FargoSoulsUtil.HostCheck)
            return;
          SpawnHammers(100f, 0.7853982f);
          SpawnHammers(150f, 0.0f);
          if (WorldSavingSystem.EternityMode)
            SpawnHammers(200f, 0.7853982f);
          if (!WorldSavingSystem.MasochistModeReal)
            return;
          SpawnHammers(300f, 0.0f);
        }
        else
        {
          if ((double) this.Timer <= 90.0)
            return;
          this.NPC.netUpdate = true;
          if ((double) this.Phase > 1.0)
          {
            ref float local3 = ref local2;
            float num1 = local2 + 1f;
            double num2 = (double) num1;
            local3 = (float) num2;
            if ((double) num1 < 3.0)
            {
              this.SubTimer = 0.0f;
              local1 = 0.0f;
              return;
            }
          }
          this.GetNextAttack();
        }
      }

      void SpawnHammers(float rad, float angleOffset)
      {
        float num1 = (float) (6.2831854820251465 * (double) rad / 45.0);
        float num2 = num1 * num1 / rad * (float) ((Entity) this.NPC).direction;
        for (int index = 0; index < 4; ++index)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 2.0 * (double) index + (double) angleOffset, new Vector2()), num1), ModContent.ProjectileType<DeviHammer>(), projectileDamage, 0.0f, Main.myPlayer, num2, 45f, 0.0f);
      }

      void HeartBarrages()
      {
        ref float local = ref this.NPC.localAI[0];
        targetPos = ((Entity) player).Center;
        targetPos.X += (float) (400 * ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X ? -1 : 1));
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.2f);
        if ((double) local == 0.0)
        {
          local = 1f;
          Vector2 teleportTarget;
          // ISSUE: explicit constructor call
          ((Vector2) ref teleportTarget).\u002Ector(((Entity) player).Center.X, ((Entity) this.NPC).Center.Y);
          teleportTarget.X += (double) ((Entity) this.NPC).Center.X < (double) teleportTarget.X ? -450f : 450f;
          StrongAttackTeleport(teleportTarget);
        }
        if ((double) --this.Timer >= 0.0)
          return;
        this.NPC.netUpdate = true;
        this.Timer = 75f;
        if ((double) ++this.SubTimer > 3.0)
        {
          this.GetNextAttack();
        }
        else
        {
          SoundEngine.PlaySound(ref SoundID.Item43, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (!FargoSoulsUtil.HostCheck)
            return;
          int num1 = (int) ((double) this.NPC.damage / 3.2);
          Vector2 vector2 = Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) Main.player[this.NPC.target]).Center), 10f);
          int num2 = 0;
          if (WorldSavingSystem.EternityMode)
            ++num2;
          if (WorldSavingSystem.MasochistModeReal)
            num2 += 3;
          int num3 = 3 + num2;
          for (int index = -num3; index <= num3; ++index)
          {
            float num4 = 30f;
            if (WorldSavingSystem.MasochistModeReal)
              num4 += (float) (3 * Math.Abs(index));
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, Math.PI / 7.0 * (double) index, new Vector2()), ModContent.ProjectileType<FakeHeart2>(), num1, 0.0f, Main.myPlayer, 20f, num4, 0.0f);
          }
          if ((double) this.Phase <= 1.0)
            return;
          int num5 = 5 + num2;
          for (int index = -num5; index <= num5; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(1.5f, Utils.RotatedBy(vector2, Math.PI / 10.0 * (double) index, new Vector2())), ModContent.ProjectileType<FakeHeart2>(), num1, 0.0f, Main.myPlayer, 20f, (float) (40 + 5 * Math.Abs(index)), 0.0f);
        }
      }

      void WyvernOrbSpiral()
      {
        ref float local = ref this.NPC.ai[3];
        if ((double) local == 0.0)
          local = Utils.NextBool(Main.rand) ? 1f : -1f;
        targetPos = Vector2.op_Addition(((Entity) player).Center, Utils.RotatedBy(Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 375f), (double) local * 1.5707963705062866 / 10.0, new Vector2()));
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.15f);
        if ((double) --this.Timer >= 0.0)
          return;
        this.NPC.netUpdate = true;
        this.Timer = WorldSavingSystem.EternityMode ? 120f : 150f;
        local = Utils.NextBool(Main.rand) ? 1f : -1f;
        if ((double) ++this.SubTimer > 3.0)
        {
          this.GetNextAttack();
        }
        else
        {
          if (!FargoSoulsUtil.HostCheck)
            return;
          int num = (double) this.Phase > 1.0 ? 8 : 12;
          Vector2 vector2 = Vector2.Normalize(((Entity) this.NPC).velocity);
          if (WorldSavingSystem.MasochistModeReal)
            vector2 = Vector2.op_Multiply(vector2, 0.75f);
          for (int index = 0; index < num; ++index)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<DeviLightBall>(), projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.008f * (float) ((Entity) this.NPC).direction, 0.0f);
            if ((double) this.Phase > 1.0)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) num * (double) index, new Vector2()), ModContent.ProjectileType<DeviLightBall>(), projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.008f * (float) -((Entity) this.NPC).direction, 0.0f);
          }
        }
      }

      void Mimics()
      {
        targetPos = ((Entity) player).Center;
        targetPos.X += (float) (300 * ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X ? -1 : 1));
        targetPos.Y -= 300f;
        float speedModifier = (double) this.Timer < 120.0 ? 0.3f : 0.15f;
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, speedModifier);
        if ((double) ++this.Timer < 120.0)
        {
          if ((double) ++this.SubTimer <= 20.0)
            return;
          this.SubTimer = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          int num1 = (double) this.Phase > 1.0 ? 45 : 60;
          Vector2 center = ((Entity) player).Center;
          center.Y -= 400f;
          Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.NPC).Center), (float) num1);
          for (int index = 0; index < 20; ++index)
            Dust.NewDust(((Entity) this.NPC).Center, 0, 0, Utils.NextBool(Main.rand) ? 228 : 245, vector2.X, vector2.Y, 0, new Color(), 2f);
          if (!FargoSoulsUtil.HostCheck)
            return;
          int num2 = ModContent.ProjectileType<DeviMimic>();
          float num3 = ((Entity) player).position.Y - 16f;
          if ((double) this.Phase > 1.0)
          {
            num2 = ModContent.ProjectileType<DeviBigMimic>();
            num3 = (float) ((Entity) player).whoAmI;
          }
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, num2, projectileDamage, 0.0f, Main.myPlayer, num3, (float) num1, 0.0f);
        }
        else if ((double) this.Timer == 180.0)
        {
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          int num4 = 150;
          int num5 = (double) ((Entity) player).velocity.X == 0.0 ? num4 * Math.Sign(((Entity) player).Center.X - ((Entity) this.NPC).Center.X) : num4 * Math.Sign(((Entity) player).velocity.X);
          int num6 = 0;
          if (WorldSavingSystem.EternityMode)
            num6 = -3;
          if (WorldSavingSystem.MasochistModeReal)
            num6 = -6;
          for (int index1 = num6; index1 <= 6; ++index1)
          {
            Vector2 center = ((Entity) player).Center;
            center.X += (float) (num5 * (index1 - 1));
            center.Y -= 400f;
            int num7 = (double) this.Phase > 1.0 ? 45 : 60;
            Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.NPC).Center), (float) num7);
            for (int index2 = 0; index2 < 20; ++index2)
              Dust.NewDust(((Entity) this.NPC).Center, 0, 0, Utils.NextBool(Main.rand) ? 228 : 245, vector2.X, vector2.Y, 0, new Color(), 2f);
            if (FargoSoulsUtil.HostCheck)
            {
              int num8 = ModContent.ProjectileType<DeviMimic>();
              float num9 = ((Entity) player).position.Y - 16f;
              if ((double) this.Phase > 1.0)
              {
                num8 = ModContent.ProjectileType<DeviBigMimic>();
                num9 = (float) ((Entity) player).whoAmI;
              }
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, num8, projectileDamage, 0.0f, Main.myPlayer, num9, (float) num7, 0.0f);
            }
          }
        }
        else
        {
          if ((double) this.Timer <= 240.0)
            return;
          this.GetNextAttack();
        }
      }

      void FrostballsNados()
      {
        ref float local1 = ref this.NPC.ai[3];
        if (WorldSavingSystem.EternityMode)
        {
          targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(400f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), (double) MathHelper.ToRadians(10f), new Vector2())));
          NPC npc = this.NPC;
          ((Entity) npc).position = Vector2.op_Addition(((Entity) npc).position, Vector2.op_Division(Vector2.op_Subtraction(((Entity) player).position, ((Entity) player).oldPosition), 2f));
          if (WorldSavingSystem.MasochistModeReal)
            this.Movement(targetPos, 0.8f, 24f);
          else
            this.Movement(targetPos, 0.4f, 18f);
        }
        else
        {
          targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(350f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center)));
          if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
            this.Movement(targetPos, 0.2f);
        }
        if ((double) this.Timer == 0.0)
          StrongAttackTeleport(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(420f, Utils.RotatedByRandom(Vector2.UnitX, 6.2831854820251465))));
        if ((double) ++this.Timer > 360.0)
        {
          this.GetNextAttack();
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == ModContent.ProjectileType<FrostfireballHostile>())
              {
                int num = Main.rand.Next(90, 180);
                if (Main.projectile[index].timeLeft > num)
                  Main.projectile[index].timeLeft = num;
              }
            }
          }
        }
        if ((double) ++this.SubTimer > ((double) this.Phase > 1.0 ? 10.0 : 20.0))
        {
          this.NPC.netUpdate = true;
          this.SubTimer = 0.0f;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(4f, 0.0f), Main.rand.NextDouble() * Math.PI * 2.0, new Vector2()), ModContent.ProjectileType<FrostfireballHostile>(), projectileDamage, 0.0f, Main.myPlayer, (float) this.NPC.target, 15f, 0.0f);
        }
        if ((double) this.Phase <= 1.0)
          return;
        ref float local2 = ref local1;
        float num1 = local1 - 1f;
        double num2 = (double) num1;
        local2 = (float) num2;
        if ((double) num1 >= 0.0)
          return;
        this.NPC.netUpdate = true;
        local1 = 110f;
        Vector2 center = ((Entity) player).Center;
        center.X += ((Entity) player).velocity.X * 90f;
        center.Y -= 150f;
        if (FargoSoulsUtil.HostCheck)
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, Vector2.Zero, 658, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        int num3 = (int) ((Entity) this.NPC).Distance(center) / 10;
        Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, center), 10f);
        for (int index1 = 0; index1 < num3; ++index1)
        {
          int index2 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2, (float) index1)), 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index2].noLight = true;
          Main.dust[index2].scale = 1.25f;
        }
      }

      void RuneWizard()
      {
        int distance = WorldSavingSystem.MasochistModeReal ? 400 : 450;
        EModeGlobalNPC.Aura(this.NPC, (float) distance, true, -1, Color.GreenYellow);
        EModeGlobalNPC.Aura(this.NPC, (float) (WorldSavingSystem.MasochistModeReal ? 200.0 : 150.0), false, -1, new Color(), ModContent.BuffType<HexedBuff>(), ModContent.BuffType<CrippledBuff>(), 160, 197);
        Player localPlayer = Main.LocalPlayer;
        float num1 = ((Entity) localPlayer).Distance(((Entity) this.NPC).Center);
        if (((Entity) localPlayer).active && !localPlayer.dead && !localPlayer.ghost && (double) num1 > (double) distance && (double) num1 < (double) distance * 4.0)
        {
          if ((double) num1 > (double) distance * 2.0)
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
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) this.NPC).Center, ((Entity) localPlayer).Center);
          float num2 = ((Vector2) ref vector2_1).Length() - (float) distance;
          ((Vector2) ref vector2_1).Normalize();
          Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, (double) num2 < 17.0 ? num2 : 17f);
          Player player = localPlayer;
          ((Entity) player).position = Vector2.op_Addition(((Entity) player).position, vector2_2);
          for (int index1 = 0; index1 < 10; ++index1)
          {
            int num3 = (int) Utils.NextFromList<short>(Main.rand, new short[3]
            {
              (short) 64,
              (short) 242,
              (short) 156
            });
            int index2 = Dust.NewDust(((Entity) localPlayer).position, ((Entity) localPlayer).width, ((Entity) localPlayer).height, num3, 0.0f, 0.0f, 0, new Color(), 1.25f);
            Main.dust[index2].noGravity = true;
            Dust dust = Main.dust[index2];
            dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
          }
        }
        this.DrawRuneBorders = true;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        if (WorldSavingSystem.MasochistModeReal && (double) this.SubTimer < 1.0)
          this.SubTimer = 1f;
        int num4 = WorldSavingSystem.EternityMode ? 40 : 50;
        if ((double) ++this.Timer == 1.0)
        {
          this.TeleportDust();
          if (FargoSoulsUtil.HostCheck)
          {
            int num5 = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : 0;
            ((Entity) this.NPC).Center = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(300f, Utils.RotatedBy(Vector2.UnitX, (double) Utils.NextFloat(Main.rand, 0.0f, 6.28318548f), new Vector2())));
            if ((num5 != 0 ? ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : 0) : ((double) ((Entity) this.NPC).Center.X > (double) ((Entity) player).Center.X ? 1 : 0)) != 0)
              ((Entity) this.NPC).position.X += (((Entity) player).Center.X - ((Entity) this.NPC).Center.X) * 2f;
            this.NPC.netUpdate = true;
          }
          this.TeleportDust();
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        else if ((double) this.Timer == (double) num4)
        {
          if ((double) this.SubTimer <= 0.0 || !FargoSoulsUtil.HostCheck)
            return;
          for (int index = -1; index <= 1; ++index)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(12f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (double) MathHelper.ToRadians(5f) * (double) index, new Vector2())), 129, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if ((double) this.Phase <= 1.0)
            return;
          Vector2 vector2 = Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) Main.player[this.NPC.target]).Center), 8f);
          int num6 = WorldSavingSystem.MasochistModeReal ? 10 : 5;
          for (int index3 = 0; index3 < num6; ++index3)
          {
            int index4 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, 2.0 * Math.PI / (double) num6 * (double) index3, new Vector2()), 129, projectileDamage, 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
            if (index4 != Main.maxProjectiles)
              Main.projectile[index4].timeLeft = 300;
          }
        }
        else if ((double) this.Timer > (double) (num4 * 2))
        {
          if ((double) ++this.SubTimer > 4.0)
          {
            this.GetNextAttack();
            if (!FargoSoulsUtil.HostCheck)
              return;
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && Main.projectile[index].type == 129)
              {
                int num7 = Main.rand.Next(90, 180);
                if (Main.projectile[index].timeLeft > num7)
                  Main.projectile[index].timeLeft = num7;
              }
            }
          }
          else
          {
            this.NPC.netUpdate = true;
            this.Timer = 0.0f;
          }
        }
        else
        {
          if ((double) this.SubTimer != 0.0)
            return;
          ++this.Timer;
        }
      }

      void MothDustCharges()
      {
        ref float local1 = ref this.NPC.localAI[0];
        ref float local2 = ref this.NPC.ai[3];
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.9f);
        if ((double) local1 == 0.0)
        {
          local1 = 1f;
          this.Timer = -45f;
          this.TeleportDust();
          if (FargoSoulsUtil.HostCheck)
          {
            bool flag = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X;
            ((Entity) this.NPC).Center = ((Entity) player).Center;
            ((Entity) this.NPC).position.X += flag ? 400f : -400f;
            this.NPC.netUpdate = true;
          }
          this.TeleportDust();
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -1f : 1f, -1f), ModContent.ProjectileType<DeviSparklingLoveSmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0001f * (float) Math.Sign(((Entity) player).Center.X - ((Entity) this.NPC).Center.X), 0.0f);
        }
        ref float local3 = ref local2;
        float num1 = local2 + 1f;
        double num2 = (double) num1;
        local3 = (float) num2;
        if ((double) num1 > 2.0)
        {
          local2 = 0.0f;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f), 2f), ModContent.ProjectileType<MothDust>(), projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        if ((double) this.Timer == 0.0 && WorldSavingSystem.EternityMode && ((double) this.SubTimer % 2.0 == 1.0 && (double) this.Phase > 1.0 || WorldSavingSystem.MasochistModeReal))
        {
          int num3 = WorldSavingSystem.MasochistModeReal ? 8 : 3;
          float num4 = WorldSavingSystem.MasochistModeReal ? 64f : 48f;
          for (int index = 0; index < num3; ++index)
          {
            Vector2 vector2_1 = Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) player).velocity, 30f)), ((Entity) this.NPC).Center), Utils.NextVector2Circular(Main.rand, num4, num4));
            if (WorldSavingSystem.MasochistModeReal)
              vector2_1 = Vector2.op_Multiply(vector2_1, 2f);
            Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, vector2_1), 90f);
            float num5 = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
            int num6 = FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
            float num7 = WorldSavingSystem.MasochistModeReal ? MathHelper.ToRadians(Utils.NextFloat(Main.rand, -10f, 10f)) : 0.0f;
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<DeviEnergyHeart>(), num6, 0.0f, Main.myPlayer, num7, num5, 0.0f);
          }
        }
        if ((double) ++this.Timer <= ((double) this.Phase > 1.0 ? 45.0 : 60.0))
          return;
        this.NPC.netUpdate = true;
        if ((double) ++this.SubTimer > 5.0)
        {
          this.GetNextAttack();
        }
        else
        {
          ++this.State;
          this.Timer = 0.0f;
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) player).Center, ((Entity) player).velocity)), 20f);
          if (!FargoSoulsUtil.HostCheck)
            return;
          float num8 = (float) (4.71238899230957 * ((double) this.SubTimer % 2.0 == 0.0 ? 1.0 : -1.0));
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), -(double) num8 / 2.0, new Vector2()), ModContent.ProjectileType<DeviSparklingLoveSmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) ((double) num8 / 60.0 * 2.0), 0.0f);
        }
      }

      void WhileDashing()
      {
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
        ref float local1 = ref this.NPC.ai[3];
        ref float local2 = ref local1;
        float num1 = local1 + 1f;
        double num2 = (double) num1;
        local2 = (float) num2;
        if ((double) num1 > 2.0)
        {
          local1 = 0.0f;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.NextVector2Unit(Main.rand, 0.0f, 6.28318548f), 2f), ModContent.ProjectileType<MothDust>(), projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        if ((double) ++this.Timer <= 30.0)
          return;
        this.NPC.netUpdate = true;
        --this.State;
        this.Timer = 0.0f;
      }

      void MageSkeletonAttacks()
      {
        ref float local1 = ref this.NPC.ai[3];
        ref float local2 = ref this.NPC.localAI[0];
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 80f))), 2f);
        if ((double) ++this.Timer == 1.0)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -1f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 10f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowLine>(), 0, 0.0f, Main.myPlayer, 10f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        else if ((double) this.Timer < 120.0)
        {
          if ((double) local1 <= 0.0)
            local2 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
          if ((double) ++this.SubTimer <= 90.0)
            return;
          ref float local3 = ref local1;
          float num1 = local1 + 1f;
          double num2 = (double) num1;
          local3 = (float) num2;
          if ((double) num1 <= ((double) this.Phase > 1.0 ? 5.0 : 8.0))
            return;
          local1 = 0.0f;
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.localAI[0], new Vector2())), 290, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (!WorldSavingSystem.MasochistModeReal)
            return;
          for (int index = 0; index < 6; ++index)
          {
            float num3 = ((Entity) player).Center.X - ((Entity) this.NPC).Center.X;
            float num4 = ((Entity) player).Center.Y - ((Entity) this.NPC).Center.Y;
            float num5 = 11f / (float) Math.Sqrt((double) num3 * (double) num3 + (double) num4 * (double) num4);
            float num6 = num3 + (float) Main.rand.Next(-40, 41);
            double num7 = (double) num4 + (double) Main.rand.Next(-40, 41);
            float num8 = num6 * num5;
            double num9 = (double) num5;
            float num10 = (float) (num7 * num9);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center.X, ((Entity) this.NPC).Center.Y, num8, num10, 36, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          SoundEngine.PlaySound(ref SoundID.Item38, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        else if ((double) this.Timer < 240.0)
        {
          local1 = 0.0f;
          local2 = 0.0f;
          if ((double) ++this.SubTimer <= ((double) this.Phase > 1.0 ? 20.0 : 40.0))
            return;
          this.SubTimer = 0.0f;
          if (FargoSoulsUtil.HostCheck)
          {
            float num = (double) this.Phase > 1.0 ? 16f : 8f;
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 1f, 2f) * ((Entity) this.NPC).Distance(((Entity) player).Center), Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)));
            int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(num, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)), 291, projectileDamage, 0.0f, Main.myPlayer, vector2.X, vector2.Y, 0.0f);
            if (index != Main.maxProjectiles)
              Main.projectile[index].timeLeft = 300;
          }
          if (!WorldSavingSystem.MasochistModeReal)
            return;
          if (FargoSoulsUtil.HostCheck)
          {
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
            vector2.X += (float) Main.rand.Next(-20, 21);
            vector2.Y += (float) Main.rand.Next(-20, 21);
            ((Vector2) ref vector2).Normalize();
            int projectileDamage = projectileDamage;
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(4f, vector2), 303, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(10f), new Vector2())), 303, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(3f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(-10f), new Vector2())), 303, projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        }
        else
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Division(((Entity) npc).velocity, 2f);
          if ((double) this.Timer == 241.0 && WorldSavingSystem.EternityMode)
          {
            float num = WorldSavingSystem.MasochistModeReal ? 180f : 420f;
            StrongAttackTeleport(Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(num, Utils.RotatedByRandom(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 0.78539818525314331))));
          }
          if ((double) this.Timer == 315.0)
          {
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              int num = (double) this.Phase > 1.0 ? 30 : 20;
              for (int index1 = 0; index1 < num; ++index1)
              {
                int index2 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.NextFloat(Main.rand, 2f, 6f), Utils.RotatedBy(Vector2.UnitX, (double) Utils.NextFloat(Main.rand, 6.28318548f), new Vector2())), ModContent.ProjectileType<DeviLostSoul>(), projectileDamage, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (index2 != Main.maxProjectiles)
                  Main.projectile[index2].timeLeft = 300;
              }
            }
            if (WorldSavingSystem.MasochistModeReal)
            {
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
                vector2_1.X += (float) Main.rand.Next(-40, 41) * 0.2f;
                vector2_1.Y += (float) Main.rand.Next(-40, 41) * 0.2f;
                ((Vector2) ref vector2_1).Normalize();
                Vector2 vector2_2 = Vector2.op_Multiply(vector2_1, 11f);
                int num = projectileDamage * 2;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<SniperBullet>(), num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              SoundEngine.PlaySound(ref SoundID.Item40, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            }
          }
          if ((double) this.Timer <= 360.0)
            return;
          this.GetNextAttack();
        }
      }

      void BabyGuardians()
      {
        ref float local = ref this.NPC.ai[3];
        targetPos = ((Entity) player).Center;
        targetPos.Y -= 400f;
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.3f);
        if ((double) this.Timer == 1.0)
        {
          this.TeleportDust();
          if (FargoSoulsUtil.HostCheck)
          {
            ((Entity) this.NPC).Center = ((Entity) player).Center;
            ((Entity) this.NPC).position.X += (float) (500 * (Utils.NextBool(Main.rand) ? -1 : 1));
            ((Entity) this.NPC).position.Y -= Utils.NextFloat(Main.rand, 300f, 500f);
            this.NPC.netUpdate = true;
          }
          this.TeleportDust();
          SoundEngine.PlaySound(ref SoundID.Item84, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          local = Utils.NextBool(Main.rand) ? -1f : 1f;
        }
        if ((double) ++this.Timer < 180.0)
        {
          if ((double) this.Timer % 5.0 != 0.0 || !FargoSoulsUtil.HostCheck)
            return;
          Vector2 vector2 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedByRandom(Vector2.UnitY, 0.52359879016876221)), 20f);
          int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<DeviGuardianHarmless>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          if (index == Main.maxProjectiles)
            return;
          Main.projectile[index].timeLeft = 60;
        }
        else if ((double) this.Timer == 180.0)
        {
          this.NPC.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          BabyGuardianWall();
        }
        else
        {
          if ((double) ++this.SubTimer > 3.0)
          {
            this.SubTimer = 0.0f;
            SoundEngine.PlaySound(ref SoundID.Item21, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 center = ((Entity) player).Center;
              center.X += (float) Main.rand.Next(-200, 201);
              center.Y += 700f;
              Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 12f, 16f), Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, center)));
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, vector2, ModContent.ProjectileType<DeviGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) this.Timer > 360.0)
          {
            if ((double) this.Phase > 1.0 && WorldSavingSystem.MasochistModeReal)
            {
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              BabyGuardianWall();
            }
            this.GetNextAttack();
          }
          if ((double) this.Phase <= 1.0 || (double) this.Timer != 270.0)
            return;
          SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          BabyGuardianWall();
        }
      }

      void BabyGuardianWall()
      {
        ref float local = ref this.NPC.ai[3];
        local *= -1f;
        bool flag = (double) local > 0.0;
        for (int index1 = -1; index1 <= 1; ++index1)
        {
          if (index1 != 0)
          {
            int num1 = 1;
            int num2 = 1;
            if (WorldSavingSystem.EternityMode)
            {
              int num3 = WorldSavingSystem.MasochistModeReal ? 2 : 1;
              num1 = flag ? num3 : 12;
              num2 = flag ? 12 : num3;
            }
            for (int index2 = -num1; index2 <= num2; ++index2)
            {
              Vector2 center = ((Entity) player).Center;
              center.X += (float) (1200 * index1);
              center.Y += (float) (50 * index2);
              Vector2 vector2 = Vector2.op_Multiply(Vector2.op_Multiply(10f, Vector2.UnitX), (float) -index1);
              if (FargoSoulsUtil.HostCheck)
              {
                int index3 = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, vector2, ModContent.ProjectileType<DeviGuardian>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                if (index3 != Main.maxProjectiles)
                  Main.projectile[index3].timeLeft = 122;
              }
            }
          }
        }
      }

      void GeyserRain()
      {
        ref float local1 = ref this.NPC.localAI[0];
        ref float local2 = ref this.NPC.localAI[1];
        ref float local3 = ref this.NPC.ai[3];
        if ((double) local1 == 0.0 && (double) local2 == 0.0)
        {
          StrongAttackTeleport(new Vector2(((Entity) this.NPC).Center.X, ((Entity) player).Center.Y - 420f));
          if (WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          local1 = ((Entity) this.NPC).Center.X;
          local2 = ((Entity) this.NPC).Center.Y;
          this.NPC.netUpdate = true;
        }
        targetPos = ((Entity) player).Center;
        if ((double) ((Entity) this.NPC).Center.Y > (double) ((Entity) player).Center.Y)
          targetPos.X += (float) (300 * ((double) ((Entity) this.NPC).Center.X < (double) targetPos.X ? -1 : 1));
        targetPos.Y -= 350f;
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.15f);
        if (WorldSavingSystem.EternityMode && (double) this.Timer % 180.0 == 90.0)
        {
          for (int index1 = -1; index1 <= 1; index1 += 2)
          {
            for (int index2 = -1; index2 <= 1; index2 += 2)
            {
              int num1 = WorldSavingSystem.MasochistModeReal ? 3 : 1;
              for (int index3 = 0; index3 < num1; ++index3)
              {
                Vector2 vector2_1 = ((Entity) player).Center;
                vector2_1.X += (float) (384 * index1);
                vector2_1.Y += (float) (21 * index2);
                if (WorldSavingSystem.MasochistModeReal)
                  vector2_1 = Vector2.op_Addition(vector2_1, Utils.NextVector2Circular(Main.rand, 16f, 16f));
                Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center)), 90f);
                float num2 = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
                int num3 = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
                float num4 = WorldSavingSystem.MasochistModeReal ? MathHelper.ToRadians(Utils.NextFloat(Main.rand, -20f, 20f)) : 0.0f;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<DeviEnergyHeart>(), num3, 0.0f, Main.myPlayer, num4, num2, 0.0f);
              }
            }
          }
        }
        if ((double) ++this.Timer < 120.0)
        {
          if ((double) ++this.SubTimer <= 2.0)
            return;
          this.SubTimer = 0.0f;
          SoundEngine.PlaySound(ref SoundID.Item44, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (!FargoSoulsUtil.HostCheck)
            return;
          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(-24f, Utils.RotatedBy(Vector2.UnitY, (Main.rand.NextDouble() - 0.5) * Math.PI / 2.0, new Vector2())), ModContent.ProjectileType<DeviVisualHeart>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        else if ((double) this.Timer < 420.0)
        {
          ref float local4 = ref local3;
          float num5 = local3 - 1f;
          double num6 = (double) num5;
          local4 = (float) num6;
          if ((double) num5 >= 0.0)
            return;
          this.NPC.netUpdate = true;
          local3 = WorldSavingSystem.MasochistModeReal ? 70f : 85f;
          this.SubTimer = (double) this.SubTimer == 1.0 ? -1f : 1f;
          if (!FargoSoulsUtil.HostCheck)
            return;
          float num7 = 10f;
          if (!WorldSavingSystem.MasochistModeReal)
            num7 = (double) this.Phase > 1.0 ? 5f : 0.0f;
          Vector2 vector2_3 = Vector2.op_Multiply(24f, Utils.RotatedBy(Vector2.UnitY, (double) MathHelper.ToRadians(num7) * (double) this.SubTimer, new Vector2()));
          int num8 = (double) this.Phase > 1.0 ? ModContent.ProjectileType<DeviRainHeart2>() : ModContent.ProjectileType<DeviRainHeart>();
          int num9 = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
          int num10 = (double) this.Phase > 1.0 ? 8 : 10;
          float num11 = 1200f / (float) num10;
          float num12 = Utils.NextFloat(Main.rand, -num11, num11);
          for (int index = -num10; index <= num10; ++index)
          {
            Vector2 vector2_4;
            // ISSUE: explicit constructor call
            ((Vector2) ref vector2_4).\u002Ector(local1, local2);
            vector2_4.X += num11 * (float) index + num12;
            vector2_4.Y -= 1200f;
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_4, vector2_3, num8, num9, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
        }
        else
        {
          if ((double) this.Timer <= 510.0)
            return;
          this.GetNextAttack();
        }
      }

      void CrossRayHearts()
      {
        ref float local1 = ref this.NPC.localAI[0];
        ref float local2 = ref this.NPC.localAI[1];
        ref float local3 = ref this.NPC.ai[3];
        targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 400f));
        if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
          this.Movement(targetPos, 0.3f);
        if ((double) local1 == 0.0)
        {
          StrongAttackTeleport(new Vector2());
          if (WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          local1 = 1f;
          this.NPC.netUpdate = true;
        }
        if ((double) this.SubTimer == 0.0)
        {
          local2 = Utils.NextBool(Main.rand) ? -1f : 1f;
          this.NPC.netUpdate = true;
        }
        if ((double) ++this.SubTimer > ((double) this.Phase > 1.0 ? 75.0 : 100.0))
        {
          ref float local4 = ref local3;
          float num1 = local3 + 1f;
          double num2 = (double) num1;
          local4 = (float) num2;
          if ((double) num1 > (WorldSavingSystem.MasochistModeReal ? 3.0 : 5.0))
          {
            local3 = 0.0f;
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
              vector2_1.X += (float) Main.rand.Next(-75, 76);
              vector2_1.Y += (float) Main.rand.Next(-75, 76);
              Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, vector2_1), 90f);
              float num3 = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
              int num4 = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
              float num5 = WorldSavingSystem.MasochistModeReal ? MathHelper.ToRadians(Utils.NextFloat(Main.rand, -20f, 20f)) : 0.0f;
              if (WorldSavingSystem.EternityMode && (double) local2 > 0.0)
                num5 += (float) (0.78539818525314331 * (Utils.NextBool(Main.rand) ? -1.0 : 1.0));
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<DeviEnergyHeart>(), num4, 0.0f, Main.myPlayer, num5, num3, 0.0f);
            }
          }
          if ((double) this.SubTimer > 130.0)
          {
            this.NPC.netUpdate = true;
            this.SubTimer = 0.0f;
          }
        }
        if ((double) ++this.Timer <= ((double) this.Phase > 1.0 ? 450.0 : 480.0))
          return;
        this.GetNextAttack();
      }

      void Butterflies()
      {
        ((Entity) this.NPC).velocity = Vector2.Zero;
        if ((double) this.SubTimer == 0.0)
        {
          StrongAttackTeleport(new Vector2());
          this.SubTimer = 1f;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            float num1 = Utils.NextFloat(Main.rand, 600f);
            int num2 = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
            int num3 = 8;
            for (int index = 0; index < num3; ++index)
            {
              Vector2 vector2;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2).\u002Ector(Utils.NextFloat(Main.rand, 40f), Utils.NextFloat(Main.rand, -20f, 20f));
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<DeviButterfly>(), num2, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) (75 * index) + num1, 0.0f);
            }
          }
          if (WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if (WorldSavingSystem.MasochistModeReal && (double) this.Timer % 120.0 > 100.0 && (double) this.Timer % 3.0 == 0.0)
        {
          for (int index = -3; index <= 3; ++index)
          {
            float num = (float) index;
            if ((double) this.Timer % 240.0 > 120.0)
              num -= 0.5f * (float) Math.Sign(index);
            Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.NextVector2Circular(Main.rand, 32f, 32f));
            vector2_1.X += 250f * num;
            Vector2 vector2_2 = Vector2.op_Subtraction(vector2_1, ((Entity) this.NPC).Center);
            vector2_2.X /= 180f;
            vector2_2.Y = (float) ((double) vector2_2.Y / 180.0 - 13.500000953674316);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<RainbowSlimeSpike>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
          }
        }
        if ((double) ++this.Timer <= 480.0)
          return;
        this.GetNextAttack();
      }

      void MedusaRay()
      {
        ref float local1 = ref this.NPC.localAI[0];
        ref float local2 = ref this.NPC.localAI[1];
        ref float local3 = ref this.NPC.ai[3];
        if ((double) local1 == 0.0)
        {
          StrongAttackTeleport(new Vector2());
          local1 = 1f;
          ((Entity) this.NPC).velocity = Vector2.Zero;
          if (WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if ((double) local3 < 4.0 && (double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && Collision.CanHitLine(((Entity) this.NPC).Center, 0, 0, ((Entity) Main.LocalPlayer).Center, 0, 0) && Math.Sign(((Entity) Main.LocalPlayer).direction) == Math.Sign(((Entity) this.NPC).Center.X - ((Entity) Main.LocalPlayer).Center.X) && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
        {
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) Main.LocalPlayer).Center, Vector2.op_Multiply(Vector2.UnitY, 12f));
          Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.UnitY, 6f));
          Vector2 vector2_3 = vector2_2;
          Vector2 vector2_4 = Vector2.op_Subtraction(vector2_1, vector2_3);
          int num = (int) ((Vector2) ref vector2_4).Length() / 10;
          Vector2 vector2_5 = Vector2.op_Multiply(Vector2.Normalize(vector2_4), 10f);
          for (int index1 = 0; index1 <= num; ++index1)
          {
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_2, Vector2.op_Multiply(vector2_5, (float) index1)), 0, 0, 228, 0.0f, 0.0f, 0, new Color(), 1f);
            Main.dust[index2].noLight = true;
            Main.dust[index2].noGravity = true;
            Main.dust[index2].scale = 1f;
          }
        }
        if ((double) local3 < 7.0)
        {
          if (WorldSavingSystem.EternityMode)
          {
            this.Timer += 0.4f;
            this.SubTimer += 0.4f;
          }
          if (WorldSavingSystem.MasochistModeReal)
          {
            this.Timer += 0.6f;
            this.SubTimer += 0.6f;
          }
        }
        if ((double) ++this.SubTimer > 60.0)
        {
          this.SubTimer = 0.0f;
          if ((double) this.Phase > 1.0 && (double) local3 < 7.0 && !Main.player[this.NPC.target].stoned && FargoSoulsUtil.HostCheck)
          {
            int num = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
            for (int index = 0; index < 12; ++index)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (double) index, new Vector2())), ModContent.ProjectileType<DeviHeart>(), num, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          ref float local4 = ref local3;
          float num1 = local3 + 1f;
          double num2 = (double) num1;
          local4 = (float) num2;
          if ((double) num1 < 4.0)
          {
            this.NPC.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            FargoSoulsUtil.DustRing(((Entity) this.NPC).Center, 120, 228, 20f, new Color(), 2f);
            if ((double) local3 == 1.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(0.0f, -1f), ModContent.ProjectileType<DeviMedusa>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          else if ((double) local3 == 4.0)
          {
            SoundEngine.PlaySound(ref SoundID.NPCDeath17, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if ((double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) < 3000.0 && Collision.CanHitLine(((Entity) this.NPC).Center, 0, 0, ((Entity) Main.LocalPlayer).Center, 0, 0) && Math.Sign(((Entity) Main.LocalPlayer).direction) == Math.Sign(((Entity) this.NPC).Center.X - ((Entity) Main.LocalPlayer).Center.X) && ((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead && !Main.LocalPlayer.ghost)
            {
              for (int index3 = 0; index3 < 40; ++index3)
              {
                int index4 = Dust.NewDust(((Entity) Main.LocalPlayer).Center, 0, 0, 1, 0.0f, 0.0f, 0, new Color(), 2f);
                Dust dust = Main.dust[index4];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
              }
              Main.LocalPlayer.AddBuff(156, 300, true, false);
              if (Main.LocalPlayer.HasBuff(156))
                Main.LocalPlayer.AddBuff(8, 300, true, false);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) Main.LocalPlayer).Center, new Vector2(0.0f, -1f), ModContent.ProjectileType<DeviMedusa>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          else if ((double) local3 < 7.0)
          {
            this.NPC.netUpdate = true;
            FargoSoulsUtil.DustRing(((Entity) this.NPC).Center, 160, 86, 40f, new Color(), 2.5f);
            local2 = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
            if ((double) local3 == 6.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) local2, new Vector2()), ModContent.ProjectileType<DeviDeathraySmall>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
          else if ((double) local3 == 7.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(-3f, Utils.RotatedBy(Vector2.UnitX, (double) local2, new Vector2()));
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, (double) local2, new Vector2()), ModContent.ProjectileType<DeviBigDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            for (int index5 = 0; index5 < 160; ++index5)
            {
              Vector2 vector2 = Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, (double) index5 * 3.14159274101257 * 2.0 / 160.0, new Vector2())), new Vector2(8f, 16f)), (double) Utils.ToRotation(((Entity) this.NPC).velocity), new Vector2());
              int index6 = Dust.NewDust(((Entity) this.NPC).Center, 0, 0, 86, 0.0f, 0.0f, 0, new Color(), 1f);
              Main.dust[index6].scale = 5f;
              Main.dust[index6].noGravity = true;
              Main.dust[index6].position = ((Entity) this.NPC).Center;
              Main.dust[index6].velocity = Vector2.op_Multiply(vector2, 3f);
            }
          }
        }
        if ((double) local3 < 7.0)
        {
          float num3 = 0.99f;
          if ((double) local3 >= 1.0)
            num3 = 0.79f;
          if ((double) local3 >= 2.0)
            num3 = 0.58f;
          if ((double) local3 >= 3.0)
            num3 = 0.43f;
          if ((double) local3 >= 4.0)
            num3 = 0.33f;
          for (int index = 0; index < 9; ++index)
          {
            if ((double) Utils.NextFloat(Main.rand) >= (double) num3)
            {
              float num4 = Utils.NextFloat(Main.rand) * 6.283185f;
              float num5 = Utils.NextFloat(Main.rand);
              Dust dust = Dust.NewDustPerfect(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Utils.ToRotationVector2(num4), (float) (110.0 + 600.0 * (double) num5))), 86, new Vector2?(Vector2.op_Multiply(Utils.ToRotationVector2(num4 - 3.141593f), (float) (14.0 + 8.0 * (double) num5))), 0, new Color(), 1f);
              dust.scale = 0.9f;
              dust.fadeIn = (float) (1.1499999761581421 + (double) num5 * 0.30000001192092896);
              dust.noGravity = true;
            }
          }
        }
        if ((double) local2 != 0.0)
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(Utils.ToRotationVector2(local2).X);
        if ((double) ++this.Timer <= 600.0)
          return;
        this.GetNextAttack();
      }

      void SparklingLove()
      {
        ref float local1 = ref this.NPC.localAI[0];
        ref float local2 = ref this.NPC.ai[3];
        if ((double) local1 == 0.0)
        {
          StrongAttackTeleport(Vector2.op_Addition(((Entity) player).Center, new Vector2((float) (300 * Math.Sign(((Entity) this.NPC).Center.X - ((Entity) player).Center.X)), -100f)));
          local1 = 1f;
          if (WorldSavingSystem.EternityMode && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<DeviRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
        }
        if ((double) ++this.Timer < 150.0)
        {
          ((Entity) this.NPC).velocity = Vector2.Zero;
          if ((double) this.SubTimer == 0.0)
          {
            double num1 = (double) ((Entity) this.NPC).position.X < (double) ((Entity) player).position.X ? -1.0 * Math.PI / 4.0 : Math.PI / 4.0;
            this.SubTimer = (float) (num1 * -4.0 / 30.0);
            if (FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, num1, new Vector2())), 90f)), Vector2.Zero, ModContent.ProjectileType<DeviSparklingLove>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 2f), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 90f, 0.0f);
            Vector2 vector2_1 = Vector2.op_Multiply(Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, num1, new Vector2())), 80f);
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < 8; ++index)
                SpawnAxeHitbox(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_1, (float) index)));
              for (int index = 1; index < 3; ++index)
              {
                SpawnAxeHitbox(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_1, 5f)), Vector2.op_Multiply(Utils.RotatedBy(vector2_1, -num1 * 2.0, new Vector2()), (float) index)));
                SpawnAxeHitbox(Vector2.op_Addition(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_1, 6f)), Vector2.op_Multiply(Utils.RotatedBy(vector2_1, -num1 * 2.0, new Vector2()), (float) index)));
              }
            }
            if (WorldSavingSystem.MasochistModeReal && FargoSoulsUtil.HostCheck)
            {
              for (int index = 0; index < 4; ++index)
              {
                Vector2 vector2_2 = Vector2.op_Division(Vector2.op_Multiply(2f, Utils.RotatedBy(new Vector2(80f, 80f), 1.5707963705062866 * (double) index, new Vector2())), 90f);
                float num2 = (float) (-(double) ((Vector2) ref vector2_2).Length() / 90.0);
                int num3 = (double) this.Phase > 1.0 ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f) : FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<DeviEnergyHeart>(), num3, 0.0f, Main.myPlayer, 0.0f, num2, 0.0f);
              }
            }
          }
          float num4 = this.Timer / 150f;
          float num5 = 0.025f;
          local2 -= this.SubTimer * num4 * num5;
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.SubTimer);
        }
        else if ((double) this.Timer == 150.0)
        {
          targetPos = ((Entity) player).Center;
          targetPos.X -= (float) (360 * Math.Sign(this.SubTimer));
          ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Subtraction(targetPos, ((Entity) this.NPC).Center), 30f);
          this.NPC.netUpdate = true;
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.SubTimer);
          if (WorldSavingSystem.MasochistModeReal || Math.Sign(targetPos.X - ((Entity) this.NPC).Center.X) == Math.Sign(this.SubTimer))
            return;
          ((Entity) this.NPC).velocity.X *= 0.5f;
        }
        else if ((double) this.Timer < 180.0)
        {
          float num6 = this.Timer - 150f;
          float num7 = (float) ((double) this.SubTimer * 2.0 / 30.0);
          local2 += num6 * num7;
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.SubTimer);
        }
        else
        {
          targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 400f));
          if ((double) ((Entity) this.NPC).Distance(targetPos) > 50.0)
            this.Movement(targetPos, 0.2f);
          if ((double) this.Timer <= 300.0)
            return;
          this.GetNextAttack();
        }
      }

      void SpawnAxeHitbox(Vector2 spawnPos)
      {
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), spawnPos, Vector2.Zero, ModContent.ProjectileType<DeviAxe>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 2f), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, ((Entity) this.NPC).Distance(spawnPos), 0.0f);
      }

      void Pause()
      {
        this.NPC.dontTakeDamage = false;
        targetPos = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) player, ((Entity) this.NPC).Center), 200f));
        this.Movement(targetPos, 0.1f);
        if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 100.0)
          this.Movement(targetPos, 0.5f);
        int num = 180;
        if (WorldSavingSystem.MasochistModeReal)
        {
          num -= 30;
          this.ignoreMoney = true;
        }
        if (WorldSavingSystem.EternityMode)
          num -= 60;
        if ((double) this.Phase > 1.0)
          num -= 30;
        if ((double) ++this.Timer <= (double) num)
          return;
        this.NPC.netUpdate = true;
        this.State = 16f;
        this.Timer = 0.0f;
        this.SubTimer = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
        if (!this.ignoreMoney && this.NPC.extraValue > Item.buyPrice(10, 0, 0, 0))
        {
          this.State = 17f;
        }
        else
        {
          this.State = (float) this.attackQueue[(int) this.AttackIndex];
          int length = this.attackQueue.Length;
          if (!WorldSavingSystem.EternityMode)
            --length;
          if ((double) ++this.AttackIndex < (double) length)
            return;
          this.AttackIndex = (float) ((double) this.Phase > 1.0 ? 1 : 0);
          this.RefreshAttackQueue();
        }
      }

      void Bribery()
      {
        this.NPC.dontTakeDamage = true;
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.95f);
        if (this.NPC.timeLeft < 600)
          this.NPC.timeLeft = 600;
        if (this.NPC.buffType[0] != 0)
          this.NPC.DelBuff(0);
        Rectangle rectangle;
        ref Rectangle local = ref rectangle;
        Rectangle hitbox1 = ((Entity) this.NPC).Hitbox;
        int x = ((Rectangle) ref hitbox1).Center.X;
        Rectangle hitbox2 = ((Entity) this.NPC).Hitbox;
        int num = ((Rectangle) ref hitbox2).Center.Y - ((Entity) this.NPC).height / 4;
        // ISSUE: explicit constructor call
        ((Rectangle) ref local).\u002Ector(x, num, 2, 2);
        if ((double) this.Timer == 0.0)
        {
          if (FargoSoulsUtil.HostCheck)
          {
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].type == ModContent.ProjectileType<DeviRitual>() && (double) Main.projectile[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
                Main.projectile[index].Kill();
            }
          }
        }
        else if ((double) this.Timer == 60.0)
          CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Line1"), false, false);
        else if ((double) this.Timer == 150.0)
          CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Line2"), true, false);
        else if ((double) this.Timer == 300.0)
          CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Line3"), true, false);
        else if ((double) this.Timer == 450.0)
        {
          if (WorldSavingSystem.DownedDevi)
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Accept1"), false, false);
          else
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Reject1"), true, false);
        }
        else if ((double) this.Timer == 600.0)
        {
          if (WorldSavingSystem.DownedDevi)
          {
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Accept2"), true, false);
          }
          else
          {
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Reject2"), true, false);
            SoundEngine.PlaySound(ref SoundID.Item28, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
            Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.UnitX, (float) ((Entity) this.NPC).width), 2f), (double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).Center.X ? -1f : 1f));
            for (int index1 = 0; index1 < 30; ++index1)
            {
              int index2 = Dust.NewDust(vector2, 0, 0, 66, 0.0f, 0.0f, 0, new Color(), 1f);
              Main.dust[index2].noGravity = true;
              Dust dust = Main.dust[index2];
              dust.velocity = Vector2.op_Multiply(dust.velocity, 6f);
            }
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, -1f, -21f, 0.0f);
              Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), vector2, 74, 10, false, 0, false, false);
            }
          }
          this.NPC.extraValue -= Item.buyPrice(10, 0, 0, 0);
        }
        else if ((double) this.Timer == 900.0)
        {
          if (WorldSavingSystem.DownedDevi)
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Accept3"), true, false);
          else
            CombatText.NewText(rectangle, Color.HotPink, Language.GetTextValue("Mods.FargowiltasSouls.NPCs.DeviBoss.Bribe.Reject3"), true, false);
        }
        if ((double) ++this.Timer <= 1050.0)
          return;
        this.ignoreMoney = true;
        if (WorldSavingSystem.DownedDevi)
        {
          this.NPC.life = 0;
          this.NPC.checkDead();
        }
        else
        {
          this.State = 16f;
          this.Timer = 0.0f;
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(20f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center));
        }
        this.NPC.netUpdate = true;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(ModContent.BuffType<LovestruckBuff>(), 240, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      for (int index1 = 0; index1 < 3; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 86, 0.0f, 0.0f, 0, new Color(), 1f);
        Main.dust[index2].noGravity = true;
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
      }
    }

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot)
    {
      if ((double) this.State != 8.0)
        return false;
      CooldownSlot = 1;
      return (double) ((Entity) this.NPC).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) target, ((Entity) this.NPC).Center)) < 42.0;
    }

    public virtual bool CanHitNPC(NPC target)
    {
      return target.type != ModContent.NPCType<Deviantt>() && target.type != ModContent.NPCType<Abominationn>() && target.type != ModContent.NPCType<Deviantt>() && base.CanHitNPC(target);
    }

    public virtual void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
    {
      this.ModifyHitByAnything(player, ref modifiers);
    }

    public virtual void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
    {
      this.ModifyHitByAnything(Main.player[projectile.owner], ref modifiers);
    }

    public void ModifyHitByAnything(Player player, ref NPC.HitModifiers hitModifiers)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss.\u003C\u003Ec__DisplayClass34_0 cDisplayClass340 = new FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss.\u003C\u003Ec__DisplayClass34_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass340.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass340.player = player;
      // ISSUE: reference to a compiler-generated field
      if (!cDisplayClass340.player.loveStruck)
        return;
      // ISSUE: method pointer
      ((NPC.HitModifiers) ref hitModifiers).ModifyHitInfo += new NPC.HitModifiers.HitInfoModifier((object) cDisplayClass340, __methodptr(\u003CModifyHitByAnything\u003Eb__0));
    }

    public virtual bool CheckDead()
    {
      if ((double) this.State == -2.0 && (double) this.Timer >= 180.0)
        return true;
      this.NPC.life = 1;
      ((Entity) this.NPC).active = true;
      if ((double) this.Phase < 2.0)
        this.Phase = 2f;
      if (FargoSoulsUtil.HostCheck && (double) this.State > -2.0)
      {
        this.State = -2f;
        this.Timer = 0.0f;
        this.SubTimer = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
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
      {
        Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).Hitbox, ModContent.ItemType<BrokenBlade>(), 1, false, 0, false, false);
        if (Main.bloodMoon)
          Item.NewItem(((Entity) this.NPC).GetSource_Loot((string) null), ((Entity) this.NPC).Hitbox, ModContent.ItemType<VermillionTopHat>(), 1, false, 0, false, false);
      }
      NPC.SetEventFlagCleared(ref WorldSavingSystem.downedDevi, -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npcLoot);
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ModContent.ItemType<DeviatingEnergy>(), 1, 15, 30, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<DeviBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<DeviTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<DeviRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<ChibiHat>(), 4));
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new EModeDropCondition());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<SparklingAdoration>()), false);
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 188;

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter <= 6.0)
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
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Vector2 position = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle frame = this.NPC.frame;
      Texture2D texture2D2 = texture2D1;
      if (Main.getGoodWorld && !this.NPC.IsABestiaryIconDummy)
      {
        texture2D2 = ModContent.Request<Texture2D>(this.Texture + "_FTW", (AssetRequestMode) 1).Value;
        int num1 = texture2D1.Height / Main.npcFrameCount[this.NPC.type];
        int num2 = frame.Y / num1;
        int num3 = texture2D2.Height / Main.npcFrameCount[this.NPC.type];
        // ISSUE: explicit constructor call
        ((Rectangle) ref frame).\u002Ector(0, num2 * num3, texture2D2.Width, num3);
      }
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Main.EntitySpriteDraw(texture2D2, position, new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      if (this.DrawRuneBorders)
        DrawBorders(spriteBatch, position);
      return false;

      static void DrawBorders(SpriteBatch spriteBatch, Vector2 position)
      {
        Color red = Color.Red;
        ((Color) ref red).A = (byte) 0;
        spriteBatch.Draw(FargosTextureRegistry.HardEdgeRing.Value, position, new Rectangle?(), Color.op_Multiply(red, 0.7f), 0.0f, Vector2.op_Multiply(Utils.Size(FargosTextureRegistry.HardEdgeRing.Value), 0.5f), 0.65f, (SpriteEffects) 0, 0.0f);
        Color green = Color.Green;
        ((Color) ref green).A = (byte) 0;
        spriteBatch.Draw(FargosTextureRegistry.SoftEdgeRing.Value, position, new Rectangle?(), Color.op_Multiply(green, 0.7f), 0.0f, Vector2.op_Multiply(Utils.Size(FargosTextureRegistry.SoftEdgeRing.Value), 0.5f), 2.05f, (SpriteEffects) 0, 0.0f);
      }
    }

    private void GetNextAttack()
    {
      this.NPC.TargetClosest(true);
      this.NPC.netUpdate = true;
      this.State = 16f;
      this.Timer = 0.0f;
      this.SubTimer = 0.0f;
      this.NPC.ai[3] = 0.0f;
      this.NPC.localAI[0] = 0.0f;
      this.NPC.localAI[1] = 0.0f;
    }

    private void RefreshAttackQueue()
    {
      this.NPC.netUpdate = true;
      int[] numArray = new int[4];
      for (int index1 = 0; index1 < 3; ++index1)
      {
        numArray[index1] = Main.rand.Next(1, 11);
        bool flag = false;
        if (numArray[index1] == 8)
          flag = true;
        for (int index2 = 0; index2 < 3; ++index2)
        {
          if (numArray[index1] == this.attackQueue[index2])
            flag = true;
        }
        for (int index3 = index1; index3 >= 0; --index3)
        {
          if (index1 != index3 && numArray[index1] == numArray[index3])
            flag = true;
        }
        if (flag)
          --index1;
      }
      do
      {
        numArray[3] = Main.rand.Next(11, 16);
      }
      while (numArray[3] == this.attackQueue[3] || numArray[3] == this.lastStrongAttack || numArray[3] == 15 && (double) this.Phase <= 1.0);
      this.lastStrongAttack = this.attackQueue[3];
      this.attackQueue = numArray;
    }

    private bool AliveCheck(Player player)
    {
      if ((!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 5000.0) && (double) this.Phase > 0.0)
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
            ModNPC modNpc;
            if (FargoSoulsUtil.HostCheck && ModContent.TryFind<ModNPC>("Fargowiltas", "Deviantt", ref modNpc) && !NPC.AnyNPCs(modNpc.Type))
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
      if (this.NPC.timeLeft < 600)
        this.NPC.timeLeft = 600;
      return true;
    }

    private bool Phase2Check()
    {
      if ((double) this.Phase > 1.0 || (double) this.NPC.life >= (double) this.NPC.lifeMax * (!WorldSavingSystem.EternityMode || WorldSavingSystem.MasochistModeReal ? 0.5 : 0.66) || !Main.expertMode)
        return false;
      if (FargoSoulsUtil.HostCheck)
      {
        this.State = -1f;
        this.Timer = 0.0f;
        this.SubTimer = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
        this.NPC.netUpdate = true;
        FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
      }
      return true;
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
    {
      if ((double) Math.Abs(((Entity) this.NPC).Center.X - targetPos.X) > 10.0)
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
      if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) cap)
        ((Entity) this.NPC).velocity.X = cap * (float) Math.Sign(((Entity) this.NPC).velocity.X);
      if ((double) Math.Abs(((Entity) this.NPC).velocity.Y) <= (double) cap)
        return;
      ((Entity) this.NPC).velocity.Y = cap * (float) Math.Sign(((Entity) this.NPC).velocity.Y);
    }

    private void TeleportDust()
    {
      for (int index1 = 0; index1 < 25; ++index1)
      {
        int index2 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 272, 0.0f, 0.0f, 100, new Color(), 2f);
        Main.dust[index2].noGravity = true;
        Dust dust1 = Main.dust[index2];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        Main.dust[index2].noLight = true;
        int index3 = Dust.NewDust(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height, 272, 0.0f, 0.0f, 100, new Color(), 1f);
        Dust dust2 = Main.dust[index3];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 4f);
        Main.dust[index3].noGravity = true;
        Main.dust[index3].noLight = true;
      }
    }

    private static bool ProjectileExists(int id, int type)
    {
      return id > -1 && id < Main.maxProjectiles && ((Entity) Main.projectile[id]).active && Main.projectile[id].type == type;
    }

    public enum DevianttAttackTypes
    {
      Die = -2, // 0xFFFFFFFE
      Phase2Transition = -1, // 0xFFFFFFFF
      SpawnEffects = 0,
      PaladinHammers = 1,
      HeartBarrages = 2,
      WyvernOrbSpiral = 3,
      Mimics = 4,
      FrostballsNados = 5,
      RuneWizard = 6,
      MothDustCharges = 7,
      WhileDashing = 8,
      MageSkeletonAttacks = 9,
      BabyGuardians = 10, // 0x0000000A
      GeyserRain = 11, // 0x0000000B
      CrossRayHearts = 12, // 0x0000000C
      Butterflies = 13, // 0x0000000D
      MedusaRay = 14, // 0x0000000E
      SparklingLove = 15, // 0x0000000F
      Pause = 16, // 0x00000010
      Bribery = 17, // 0x00000011
    }
  }
}
