// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Timber.TimberChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Timber
{
  [AutoloadBossHead]
  public class TimberChampion : ModNPC
  {
    private const float BaseWalkSpeed = 4f;
    private bool drawTrail;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 8;
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
      bestiaryDrawModifiers1.Position = new Vector2(64f, 144f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(16f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(112f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary.TimberChampion")
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 120;
      ((Entity) this.NPC).height = 234;
      this.NPC.damage = 110;
      this.NPC.defense = 50;
      this.NPC.lifeMax = 240000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit7);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
      this.NPC.noGravity = false;
      this.NPC.noTileCollide = false;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(1, 0, 0, 0);
      this.NPC.boss = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(mod, "Assets/Music/Champions") : 81;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
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
      CooldownSlot = 1;
      return true;
    }

    private static int JumpTreshold => !WorldSavingSystem.MasochistModeReal ? 60 : 30;

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(false);
        this.NPC.localAI[3] = 1f;
      }
      this.drawTrail = false;
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      Player player = Main.player[this.NPC.target];
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).position.X < (double) ((Entity) player).position.X ? 1 : -1;
      switch (this.NPC.ai[0])
      {
        case -2f:
          this.TileCollision((double) ((Entity) player).Center.Y > (double) ((Entity) this.NPC).Bottom.Y, (double) Math.Abs(((Entity) player).Center.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2) && (double) ((Entity) this.NPC).Bottom.Y < (double) ((Entity) player).Center.Y);
          break;
        case -1f:
          this.Movement(((Entity) player).Center);
          break;
        case 0.0f:
        case 4f:
        case 8f:
          this.NPC.noTileCollide = false;
          this.NPC.noGravity = false;
          float num1 = WorldSavingSystem.EternityMode ? 60f : 90f;
          float num2 = WorldSavingSystem.EternityMode ? 0.8f : 0.4f;
          if ((double) ++this.NPC.ai[1] == (double) TimberChampion.JumpTreshold)
          {
            this.NPC.TargetClosest(true);
            if (WorldSavingSystem.MasochistModeReal)
            {
              if ((double) this.NPC.localAI[1] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.6600000262260437)
              {
                this.NPC.localAI[1] = 1f;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<TimberPalmTree>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
              }
              if ((double) this.NPC.localAI[2] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.33000001311302185)
              {
                this.NPC.localAI[2] = 1f;
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<TimberPalmTree>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
              }
            }
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Top, ((Entity) this.NPC).Bottom);
            vector2.X += WorldSavingSystem.MasochistModeReal ? ((Entity) player).velocity.X * num1 : (float) (420 * Math.Sign(vector2.X));
            vector2.X /= num1;
            vector2.Y = (float) ((double) vector2.Y / (double) num1 - 0.5 * (double) num2 * (double) num1);
            ((Entity) this.NPC).velocity = vector2;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            this.NPC.netUpdate = true;
            if (FargoSoulsUtil.HostCheck)
            {
              int num3 = WorldSavingSystem.MasochistModeReal ? FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage) : 0;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, 683, num3, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
            SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int index1 = -2; index1 <= 2; ++index1)
            {
              Vector2 center = ((Entity) this.NPC).Center;
              int num4 = ((Entity) this.NPC).width / 5;
              center.X += (float) (num4 * index1) + Utils.NextFloat(Main.rand, (float) -num4, (float) num4);
              center.Y += Utils.NextFloat(Main.rand, (float) (((Entity) this.NPC).height / 2));
              for (int index2 = 0; index2 < 30; ++index2)
              {
                int index3 = Dust.NewDust(center, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
                Dust dust = Main.dust[index3];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
              }
              for (int index4 = 0; index4 < 20; ++index4)
              {
                int index5 = Dust.NewDust(center, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
                Main.dust[index5].noGravity = true;
                Dust dust1 = Main.dust[index5];
                dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
                int index6 = Dust.NewDust(center, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Dust dust2 = Main.dust[index6];
                dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
              }
              float num5 = 0.5f;
              for (int index7 = 0; index7 < 4; ++index7)
              {
                int index8 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore = Main.gore[index8];
                gore.velocity = Vector2.op_Multiply(gore.velocity, num5);
                ++Main.gore[index8].velocity.X;
                ++Main.gore[index8].velocity.Y;
              }
            }
            if (!WorldSavingSystem.EternityMode || !FargoSoulsUtil.HostCheck)
              break;
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Subtraction(((Entity) this.NPC).Bottom, Vector2.op_Multiply(65f, Vector2.UnitY)), Vector2.Zero, ModContent.ProjectileType<TimberJumpMark>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, (float) ((Entity) this.NPC).width, 0.0f);
            break;
          }
          if ((double) this.NPC.ai[1] > (double) TimberChampion.JumpTreshold)
          {
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            ((Entity) this.NPC).velocity.Y += num2;
            this.drawTrail = true;
            if ((double) this.NPC.ai[1] <= (double) TimberChampion.JumpTreshold + (double) num1)
              break;
            this.NPC.TargetClosest(true);
            this.NPC.ai[1] = (float) (TimberChampion.JumpTreshold - 1);
            this.NPC.netUpdate = true;
            if ((double) --this.NPC.ai[2] > 0.0)
              break;
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            goto case -2f;
          }
          else
          {
            if ((double) Math.Abs(((Entity) this.NPC).velocity.X) > (double) Math.Abs(((Entity) this.NPC).velocity.Y))
              ((Entity) this.NPC).velocity.X = Math.Abs(((Entity) this.NPC).velocity.Y) * (float) Math.Sign(((Entity) this.NPC).velocity.X);
            ((Entity) this.NPC).velocity.X *= 0.99f;
            if ((double) this.NPC.ai[0] != 0.0 && (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 3000.0))
            {
              this.NPC.TargetClosest(true);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              this.NPC.noTileCollide = true;
              this.NPC.noGravity = true;
              ++((Entity) this.NPC).velocity.Y;
              this.NPC.ai[1] = 0.0f;
              break;
            }
            this.NPC.timeLeft = 600;
            goto case -2f;
          }
        case 1f:
          if ((double) ++this.NPC.ai[2] > 35.0)
          {
            this.NPC.ai[2] = 0.0f;
            ++this.NPC.ai[3];
            float num6 = 60f;
            Vector2 vector2 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
            vector2.X /= num6;
            vector2.Y = (float) ((double) vector2.Y / (double) num6 - 0.10000000149011612 * (double) num6);
            for (int index = 0; index < 15; ++index)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.NextVector2Square(Main.rand, -0.5f, 0.5f), 3f)), ModContent.ProjectileType<TimberAcorn>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 0.8f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
          }
          this.NPC.localAI[0] = 0.0f;
          if (WorldSavingSystem.EternityMode && (double) this.NPC.ai[3] == 2.0)
          {
            if ((double) this.NPC.ai[2] == 0.0)
              SoundEngine.PlaySound(ref SoundID.Item36, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            ((Entity) this.NPC).velocity.X *= 0.9f;
            this.NPC.localAI[0] = 1f;
            if (!WorldSavingSystem.MasochistModeReal)
            {
              this.NPC.ai[1] -= 0.4f;
              this.NPC.ai[2] -= 0.4f;
            }
            if ((double) this.NPC.ai[2] > 30.0)
            {
              this.NPC.ai[2] = -1000f;
              ++this.NPC.ai[3];
              ((Entity) this.NPC).velocity.Y -= 12f;
              SoundEngine.PlaySound(ref SoundID.Item36, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              float num7 = 45f;
              Vector2 vector2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center), Vector2.op_Multiply(((Entity) player).velocity, 15f));
              vector2.X /= num7;
              vector2.Y = (float) ((double) vector2.Y / (double) num7 - 0.10000000149011612 * (double) num7);
              for (int index = 0; index < 30; ++index)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Addition(vector2, Vector2.op_Multiply(Utils.NextVector2Square(Main.rand, -0.5f, 0.5f), 4.5f)), ModContent.ProjectileType<TimberAcorn>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 120.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            this.NPC.TargetClosest(true);
            goto case -1f;
          }
          else
            goto case -1f;
        case 2f:
        case 6f:
          if ((double) this.NPC.ai[3] == 0.0)
          {
            this.NPC.ai[3] = 1f;
            this.NPC.ai[2] = 4f;
            goto case 0.0f;
          }
          else
            goto case 0.0f;
        case 3f:
        case 7f:
          if ((double) this.NPC.ai[1] == 0.0)
          {
            for (int arm = -1; arm <= 1; arm += 2)
            {
              Vector2 armPos = this.GetArmPos(arm);
              SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(armPos), (SoundUpdateCallback) null);
              for (int index9 = 0; index9 < 20; ++index9)
              {
                int index10 = Dust.NewDust(armPos, 0, 0, 51, 0.0f, 0.0f, 0, new Color(), 3f);
                Main.dust[index10].noGravity = true;
                Dust dust = Main.dust[index10];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 4f);
                Main.dust[index10].velocity.X += (float) ((Entity) this.NPC).direction * Utils.NextFloat(Main.rand, 6f, 24f);
              }
            }
          }
          if (WorldSavingSystem.EternityMode)
          {
            if ((double) ++this.NPC.ai[2] > 60.0)
            {
              this.NPC.ai[2] = 40f;
              this.NPC.ai[3] = (double) this.NPC.ai[3] == 1.0 ? -1f : 1f;
              Vector2 armPos = this.GetArmPos((int) this.NPC.ai[3]);
              Vector2 vector2 = Vector2.op_Multiply(16f, ((Entity) player).DirectionFrom(armPos));
              SoundEngine.PlaySound(ref SoundID.Item11, new Vector2?(armPos), (SoundUpdateCallback) null);
              for (int index = -3; index <= 3; ++index)
              {
                if (FargoSoulsUtil.HostCheck)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), armPos, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(60f) / 3.0 * (double) index, new Vector2()), ModContent.ProjectileType<TimberSnowball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, 0.0f, 0.0f);
              }
            }
          }
          else if ((double) ++this.NPC.ai[2] > 5.0)
          {
            this.NPC.ai[2] = 0.0f;
            if (FargoSoulsUtil.HostCheck && (double) this.NPC.ai[1] > 30.0 && (double) this.NPC.ai[1] < 120.0)
            {
              Vector2 vector2;
              vector2.X = Utils.NextFloat(Main.rand, 0.0f, (float) (((Entity) this.NPC).width / 2)) * (float) ((Entity) this.NPC).direction;
              vector2.Y = 16f;
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2), Vector2.op_Multiply(Vector2.UnitY, -12f), ModContent.ProjectileType<TimberSnowball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 150.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            this.NPC.TargetClosest(true);
            goto case -1f;
          }
          else
            goto case -1f;
        case 5f:
          if ((double) ++this.NPC.ai[2] > 6.0)
          {
            this.NPC.ai[2] = 0.0f;
            FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<LesserSquirrel>(), velocity: new Vector2(Utils.NextFloat(Main.rand, -10f, 10f), Utils.NextFloat(Main.rand, -20f, -10f)));
          }
          if ((double) ++this.NPC.ai[1] > 180.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.netUpdate = true;
            this.NPC.TargetClosest(true);
            goto case -1f;
          }
          else
            goto case -1f;
        case 9f:
          ((Entity) this.NPC).velocity.X *= 0.9f;
          this.NPC.localAI[0] = 0.0f;
          if ((double) ++this.NPC.ai[1] > 60.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.netUpdate = true;
            goto case -2f;
          }
          else
            goto case -2f;
        case 10f:
          if ((double) this.NPC.ai[1] == 0.0)
          {
            this.NPC.ai[3] = (float) Math.Sign(((Entity) player).Center.X - ((Entity) this.NPC).Center.X);
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = (int) this.NPC.ai[3];
            this.NPC.netUpdate = true;
            SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int arm = -1; arm <= 1; arm += 2)
            {
              Vector2 armPos = this.GetArmPos(arm);
              Vector2 vector2 = Vector2.op_Multiply(15f, ((Entity) player).DirectionFrom(armPos));
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), armPos, vector2, ModContent.ProjectileType<TimberHook>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 90f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 180.0 || !((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p => ((Entity) p).active && p.type == ModContent.ProjectileType<TimberHook>() && (double) p.ai[0] == (double) ((Entity) this.NPC).whoAmI)))
          {
            if ((double) ++this.NPC.ai[2] > 3.0)
            {
              ++this.NPC.ai[0];
              this.NPC.ai[2] = 0.0f;
            }
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
          }
          this.drawTrail = true;
          if ((double) this.NPC.localAI[0] == 0.0)
          {
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = (int) this.NPC.ai[3];
            goto case -2f;
          }
          else
          {
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = (int) this.NPC.localAI[0];
            break;
          }
        case 11f:
          if ((double) ++this.NPC.ai[1] > 120.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            goto case -1f;
          }
          else
            goto case -1f;
        default:
          this.NPC.ai[0] = 0.0f;
          goto case 0.0f;
      }
    }

    private void TileCollision(bool fallthrough = false, bool dropDown = false)
    {
      bool flag1 = false;
      for (int x = (int) ((Entity) this.NPC).position.X; (double) x <= (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width; x += 16)
      {
        Tile tileSafely = Framing.GetTileSafely(new Vector2((float) x, ((Entity) this.NPC).Bottom.Y));
        if (((Tile) ref tileSafely).TileType == (ushort) 19)
        {
          flag1 = true;
          break;
        }
      }
      bool flag2 = Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height);
      if (dropDown)
        ((Entity) this.NPC).velocity.Y += 0.5f;
      else if (flag2 || flag1 && !fallthrough)
      {
        if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
          ((Entity) this.NPC).velocity.Y = 0.0f;
        if ((double) ((Entity) this.NPC).velocity.Y > -0.20000000298023224)
          ((Entity) this.NPC).velocity.Y -= 0.025f;
        else
          ((Entity) this.NPC).velocity.Y -= 0.2f;
        if ((double) ((Entity) this.NPC).velocity.Y < -4.0)
          ((Entity) this.NPC).velocity.Y = -4f;
      }
      else
      {
        if ((double) ((Entity) this.NPC).velocity.Y < 0.0)
          ((Entity) this.NPC).velocity.Y = 0.0f;
        if ((double) ((Entity) this.NPC).velocity.Y < 0.10000000149011612)
          ((Entity) this.NPC).velocity.Y += 0.025f;
        else
          ((Entity) this.NPC).velocity.Y += 0.5f;
      }
      if ((double) ((Entity) this.NPC).velocity.Y <= 10.0)
        return;
      ((Entity) this.NPC).velocity.Y = 10f;
    }

    private void Movement(Vector2 target)
    {
      ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) target.X ? 1 : -1;
      if ((double) Math.Abs(target.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2))
      {
        ((Entity) this.NPC).velocity.X *= 0.9f;
        if ((double) Math.Abs(((Entity) this.NPC).velocity.X) < 0.10000000149011612)
          ((Entity) this.NPC).velocity.X = 0.0f;
      }
      else
      {
        float num1 = 4f;
        if (WorldSavingSystem.MasochistModeReal)
          num1 *= 2f;
        int num2 = 30;
        if (((Entity) this.NPC).direction > 0)
          ((Entity) this.NPC).velocity.X = (((Entity) this.NPC).velocity.X * (float) num2 + num1) / (float) (num2 + 1);
        else
          ((Entity) this.NPC).velocity.X = (((Entity) this.NPC).velocity.X * (float) num2 - num1) / (float) (num2 + 1);
      }
      this.TileCollision((double) target.Y > (double) ((Entity) this.NPC).Bottom.Y, (double) Math.Abs(target.X - ((Entity) this.NPC).Center.X) < (double) (((Entity) this.NPC).width / 2) && (double) ((Entity) this.NPC).Bottom.Y < (double) target.Y);
    }

    private Vector2 GetArmPos(int arm)
    {
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector((float) (47.0 + 19.0 * (double) arm), 28f);
      vector2.X *= (float) ((Entity) this.NPC).direction;
      return Vector2.op_Addition(((Entity) this.NPC).Center, vector2);
    }

    public virtual void FindFrame(int frameHeight)
    {
      switch ((int) this.NPC.ai[0])
      {
        case 0:
        case 2:
        case 4:
        case 6:
        case 8:
          if (!this.NPC.IsABestiaryIconDummy)
          {
            if ((double) this.NPC.ai[1] <= (double) TimberChampion.JumpTreshold)
            {
              this.NPC.frame.Y = frameHeight * 6;
              return;
            }
            this.NPC.frame.Y = frameHeight * 7;
            return;
          }
          break;
        case 1:
          if ((double) this.NPC.localAI[0] == 1.0)
          {
            this.NPC.frame.Y = frameHeight * 6;
            return;
          }
          break;
        case 9:
          this.NPC.frame.Y = frameHeight * 6;
          return;
        case 10:
          if ((double) this.NPC.localAI[0] != 0.0)
          {
            this.NPC.frame.Y = frameHeight * 7;
            return;
          }
          break;
      }
      this.NPC.frameCounter += 0.25 * (double) Math.Abs(((Entity) this.NPC).velocity.X);
      if (this.NPC.frameCounter > 2.5)
      {
        this.NPC.frameCounter = 0.0;
        this.NPC.frame.Y += frameHeight;
      }
      if (this.NPC.frame.Y >= frameHeight * 6)
        this.NPC.frame.Y = 0;
      if ((double) ((Entity) this.NPC).velocity.X == 0.0)
        this.NPC.frame.Y = frameHeight;
      if ((double) ((Entity) this.NPC).velocity.Y <= 4.0)
        return;
      this.NPC.frame.Y = frameHeight * 7;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<GuiltyBuff>(), 600, true, false);
    }

    private static bool spawnPhase2 => Main.expertMode;

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 3; index <= 10; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          interpolatedStringHandler.AppendLiteral("TimberGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
      FargoSoulsUtil.GrossVanillaDodgeDust((Entity) this.NPC);
      for (int index = 0; index < 6; ++index)
        this.ExplodeDust(Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height))));
    }

    private void ExplodeDust(Vector2 center)
    {
      SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(center), (SoundUpdateCallback) null);
      Vector2 vector2 = Vector2.op_Subtraction(center, Vector2.op_Division(new Vector2(32f, 32f), 2f));
      for (int index1 = 0; index1 < 20; ++index1)
      {
        int index2 = Dust.NewDust(vector2, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
        Dust dust = Main.dust[index2];
        dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
      }
      for (int index3 = 0; index3 < 15; ++index3)
      {
        int index4 = Dust.NewDust(vector2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
        Main.dust[index4].noGravity = true;
        Dust dust1 = Main.dust[index4];
        dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
        int index5 = Dust.NewDust(vector2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
        Dust dust2 = Main.dust[index5];
        dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
      }
      float num = 0.5f;
      for (int index6 = 0; index6 < 3; ++index6)
      {
        int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, new Vector2(), Main.rand.Next(61, 64), 1f);
        Gore gore = Main.gore[index7];
        gore.velocity = Vector2.op_Multiply(gore.velocity, num);
        ++Main.gore[index7].velocity.X;
        ++Main.gore[index7].velocity.Y;
      }
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual bool PreKill()
    {
      if (!TimberChampion.spawnPhase2)
        return base.PreKill();
      this.NPC.value = 0.0f;
      FargoSoulsUtil.NewNPCEasy(((Entity) this.NPC).GetSource_FromAI((string) null), ((Entity) this.NPC).Center, ModContent.NPCType<TimberChampionHead>(), ((Entity) this.NPC).whoAmI, target: this.NPC.target, velocity: new Vector2());
      return false;
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[0], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<TimberForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<TimberChampionRelic>()));
    }

    public virtual void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref spriteEffects = ((Entity) this.NPC).direction < 0 ? 0 : 1;
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      if (this.drawTrail)
      {
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
        {
          Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.33f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
          Vector2 oldPo = this.NPC.oldPos[index];
          float rotation = this.NPC.rotation;
          Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY + 2f)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY + 2f)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
