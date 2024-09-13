// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Will.WillChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Dyes;
using FargowiltasSouls.Content.Items.Pets;
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
namespace FargowiltasSouls.Content.Bosses.Champions.Will
{
  [AutoloadBossHead]
  public class WillChampion : ModNPC
  {
    public bool spawned;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 8;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 12;
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
      bestiaryDrawModifiers1.Position = new Vector2(56f, -24f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(-12f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Ocean,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 150;
      ((Entity) this.NPC).height = 100;
      this.NPC.damage = 120;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 420000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath14);
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
      this.NPC.netAlways = true;
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
      return (double) ((Entity) this.NPC).Distance(((Entity) target).Center) < 120.0;
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

    public virtual void AI()
    {
      if (!this.spawned)
      {
        this.NPC.TargetClosest(false);
        this.Movement(((Entity) Main.player[this.NPC.target]).Center, 0.8f, 32f);
        if ((double) ((Entity) this.NPC).Distance(((Entity) Main.player[this.NPC.target]).Center) >= 750.0)
          return;
        this.spawned = true;
        this.NPC.ai[2] = 4f;
        NPC npc = this.NPC;
        ((Entity) npc).velocity = Vector2.op_Division(((Entity) npc).velocity, 2f);
        this.NPC.netUpdate = true;
      }
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      if (!this.NPC.HasValidTarget)
        this.NPC.TargetClosest(false);
      Player player = Main.player[this.NPC.target];
      if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0)
        this.NPC.timeLeft = 600;
      if ((double) this.NPC.localAI[2] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.66)
      {
        this.NPC.localAI[2] = 1f;
        this.NPC.ai[0] = -1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.netUpdate = true;
      }
      else if ((double) this.NPC.localAI[3] == 0.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.33)
      {
        this.NPC.localAI[3] = 1f;
        this.NPC.ai[0] = -1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.netUpdate = true;
      }
      this.NPC.damage = this.NPC.defDamage;
      switch (this.NPC.ai[0])
      {
        case -1f:
          if (!this.NPC.HasValidTarget)
            this.NPC.TargetClosest(false);
          this.NPC.damage = 0;
          this.NPC.dontTakeDamage = true;
          NPC npc1 = this.NPC;
          ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
          if ((double) ++this.NPC.ai[2] >= 60.0)
          {
            this.NPC.ai[2] = 0.0f;
            this.NPC.localAI[0] = (double) this.NPC.localAI[0] > 0.0 ? -1f : 1f;
            if ((double) this.NPC.ai[1] <= 420.0)
            {
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                int num1 = this.NPC.life >= this.NPC.lifeMax / 2 || !WorldSavingSystem.EternityMode ? 8 : 10;
                float num2 = (double) this.NPC.localAI[0] <= 0.0 || !Vector2.op_Inequality(((Entity) player).velocity, Vector2.Zero) ? Utils.ToRotation(((Entity) player).velocity) : Utils.NextFloat(Main.rand, 6.28318548f);
                for (int index = 0; index < num1; ++index)
                {
                  float num3 = num2 + 6.28318548f / (float) num1 * (float) index;
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(450f, Utils.RotatedBy(Vector2.UnitX, (double) num3, new Vector2()))), Vector2.Zero, ModContent.ProjectileType<WillJavelin3>(), this.NPC.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, num3 + 3.14159274f, 0.0f);
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] == 1.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item4, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int index = 0; index < Main.maxProjectiles; ++index)
            {
              if (((Entity) Main.projectile[index]).active && Main.projectile[index].hostile && (Main.projectile[index].type == ModContent.ProjectileType<WillBomb>() || Main.projectile[index].type == ModContent.ProjectileType<WillJavelin>()))
                Main.projectile[index].Kill();
            }
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<WillShell>(), 0, 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 12f), ModContent.ProjectileType<WillBomb>(), this.NPC.defDamage / 4, 0.0f, Main.myPlayer, 0.3f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -6f, 0.0f);
              break;
            }
            break;
          }
          if ((double) this.NPC.ai[1] > 480.0)
          {
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        case 0.0f:
          this.NPC.dontTakeDamage = false;
          if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0)
          {
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            --((Entity) this.NPC).velocity.Y;
            return;
          }
          if ((double) ++this.NPC.ai[1] > 45.0)
          {
            this.NPC.TargetClosest(false);
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.netUpdate = true;
            if ((double) ++this.NPC.ai[2] > 4.0)
            {
              this.NPC.ai[2] = 0.0f;
              if ((double) ++this.NPC.ai[3] > 3.0)
                this.NPC.ai[3] = 1f;
              this.NPC.ai[0] += this.NPC.ai[3];
              break;
            }
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 33f);
            SoundEngine.PlaySound(ref SoundID.NPCHit14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            break;
          }
          Vector2 vector2_1 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
          this.NPC.rotation = Utils.ToRotation(vector2_1);
          if ((double) vector2_1.X > 0.0)
          {
            vector2_1.X -= 450f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = 1;
          }
          else
          {
            vector2_1.X += 450f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = -1;
          }
          vector2_1.Y -= 200f;
          ((Vector2) ref vector2_1).Normalize();
          vector2_1 = Vector2.op_Multiply(vector2_1, 20f);
          if ((double) ((Entity) this.NPC).velocity.X < (double) vector2_1.X)
          {
            ((Entity) this.NPC).velocity.X += 2f;
            if ((double) ((Entity) this.NPC).velocity.X < 0.0 && (double) vector2_1.X > 0.0)
              ((Entity) this.NPC).velocity.X += 2f;
          }
          else if ((double) ((Entity) this.NPC).velocity.X > (double) vector2_1.X)
          {
            ((Entity) this.NPC).velocity.X -= 2f;
            if ((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) vector2_1.X < 0.0)
              ((Entity) this.NPC).velocity.X -= 2f;
          }
          if ((double) ((Entity) this.NPC).velocity.Y < (double) vector2_1.Y)
          {
            ((Entity) this.NPC).velocity.Y += 2f;
            if ((double) ((Entity) this.NPC).velocity.Y < 0.0 && (double) vector2_1.Y > 0.0)
            {
              ((Entity) this.NPC).velocity.Y += 2f;
              break;
            }
            break;
          }
          if ((double) ((Entity) this.NPC).velocity.Y > (double) vector2_1.Y)
          {
            ((Entity) this.NPC).velocity.Y -= 2f;
            if ((double) ((Entity) this.NPC).velocity.Y > 0.0 && (double) vector2_1.Y < 0.0)
            {
              ((Entity) this.NPC).velocity.Y -= 2f;
              break;
            }
            break;
          }
          break;
        case 1f:
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).velocity.X > 0.0 ? 1 : -1;
          int num4 = 7;
          for (int index1 = 0; index1 < num4; ++index1)
          {
            Vector2 vector2_2 = Vector2.op_Addition(Utils.RotatedBy(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), new Vector2((float) (((Entity) this.NPC).width + 50) / 2f, (float) ((Entity) this.NPC).height)), 0.75f), (double) (index1 - (num4 / 2 - 1)) * Math.PI / (double) num4, new Vector2()), ((Entity) this.NPC).Center);
            Vector2 vector2_3 = Vector2.op_Multiply(Utils.ToRotationVector2((float) (Main.rand.NextDouble() * 3.14159274101257) - 1.57079637f), (float) Main.rand.Next(3, 8));
            Vector2 vector2_4 = vector2_3;
            int index2 = Dust.NewDust(Vector2.op_Addition(vector2_2, vector2_4), 0, 0, 87, vector2_3.X * 2f, vector2_3.Y * 2f, 100, new Color(), 1.4f);
            Main.dust[index2].noGravity = true;
            Dust dust1 = Main.dust[index2];
            dust1.velocity = Vector2.op_Division(dust1.velocity, 4f);
            Dust dust2 = Main.dust[index2];
            dust2.velocity = Vector2.op_Subtraction(dust2.velocity, ((Entity) this.NPC).velocity);
          }
          if ((double) --this.NPC.localAI[0] < 0.0)
          {
            this.NPC.localAI[0] = 2f;
            if (FargoSoulsUtil.HostCheck && (double) this.NPC.localAI[3] == 1.0)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(1.5f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), Math.PI / 2.0, new Vector2())), ModContent.ProjectileType<WillFireball2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(1.5f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), -1.0 * Math.PI / 2.0, new Vector2())), ModContent.ProjectileType<WillFireball2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 30.0)
          {
            --this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 2f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        case 2f:
          NPC npc2 = this.NPC;
          ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.975f);
          this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
          ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
          if ((double) ++this.NPC.ai[1] == 30.0)
          {
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 12f), ModContent.ProjectileType<WillBomb>(), this.NPC.defDamage / 4, 0.0f, Main.myPlayer, 0.3f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              break;
            }
            break;
          }
          if ((double) this.NPC.ai[1] > 120.0)
          {
            this.NPC.ai[0] = 0.0f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        case 3f:
          Vector2 vector2_5 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
          this.NPC.rotation = Utils.ToRotation(vector2_5);
          if ((double) vector2_5.X > 0.0)
          {
            vector2_5.X -= 450f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = 1;
          }
          else
          {
            vector2_5.X += 450f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = -1;
          }
          vector2_5.Y -= 200f;
          ((Vector2) ref vector2_5).Normalize();
          vector2_5 = Vector2.op_Multiply(vector2_5, 16f);
          if ((double) ((Entity) this.NPC).velocity.X < (double) vector2_5.X)
          {
            ((Entity) this.NPC).velocity.X += 0.25f;
            if ((double) ((Entity) this.NPC).velocity.X < 0.0 && (double) vector2_5.X > 0.0)
              ((Entity) this.NPC).velocity.X += 0.25f;
          }
          else if ((double) ((Entity) this.NPC).velocity.X > (double) vector2_5.X)
          {
            ((Entity) this.NPC).velocity.X -= 0.25f;
            if ((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) vector2_5.X < 0.0)
              ((Entity) this.NPC).velocity.X -= 0.25f;
          }
          if ((double) ((Entity) this.NPC).velocity.Y < (double) vector2_5.Y)
          {
            ((Entity) this.NPC).velocity.Y += 0.25f;
            if ((double) ((Entity) this.NPC).velocity.Y < 0.0 && (double) vector2_5.Y > 0.0)
              ((Entity) this.NPC).velocity.Y += 0.25f;
          }
          else if ((double) ((Entity) this.NPC).velocity.Y > (double) vector2_5.Y)
          {
            ((Entity) this.NPC).velocity.Y -= 0.25f;
            if ((double) ((Entity) this.NPC).velocity.Y > 0.0 && (double) vector2_5.Y < 0.0)
              ((Entity) this.NPC).velocity.Y -= 0.25f;
          }
          if ((double) --this.NPC.localAI[0] < 0.0)
          {
            this.NPC.localAI[0] = (double) this.NPC.localAI[2] == 1.0 ? 30f : 40f;
            if ((double) this.NPC.ai[1] < 110.0 || (double) this.NPC.localAI[3] == 1.0)
            {
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 15; ++index)
                {
                  double num5 = (double) Utils.NextFloat(Main.rand, 240f, 720f) / 120.0 * 2.0;
                  Vector2 vector2_6 = Vector2.op_Multiply((float) num5, Utils.RotatedByRandom(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 1.5707963705062866));
                  float num6 = (float) (num5 / 120.0);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_6, ModContent.ProjectileType<WillJavelin>(), this.NPC.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, num6, 0.0f);
                }
              }
            }
          }
          if ((double) ++this.NPC.ai[1] > 150.0)
          {
            this.NPC.ai[0] = 0.0f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        case 4f:
          Vector2 vector2_7 = Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center);
          this.NPC.rotation = Utils.ToRotation(vector2_7);
          if ((double) vector2_7.X > 0.0)
          {
            vector2_7.X -= 550f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = 1;
          }
          else
          {
            vector2_7.X += 550f;
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = -1;
          }
          vector2_7.Y -= 250f;
          ((Vector2) ref vector2_7).Normalize();
          vector2_7 = Vector2.op_Multiply(vector2_7, 16f);
          if ((double) ((Entity) this.NPC).velocity.X < (double) vector2_7.X)
          {
            ((Entity) this.NPC).velocity.X += 0.25f;
            if ((double) ((Entity) this.NPC).velocity.X < 0.0 && (double) vector2_7.X > 0.0)
              ((Entity) this.NPC).velocity.X += 0.25f;
          }
          else if ((double) ((Entity) this.NPC).velocity.X > (double) vector2_7.X)
          {
            ((Entity) this.NPC).velocity.X -= 0.25f;
            if ((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) vector2_7.X < 0.0)
              ((Entity) this.NPC).velocity.X -= 0.25f;
          }
          if ((double) ((Entity) this.NPC).velocity.Y < (double) vector2_7.Y)
          {
            ((Entity) this.NPC).velocity.Y += 0.25f;
            if ((double) ((Entity) this.NPC).velocity.Y < 0.0 && (double) vector2_7.Y > 0.0)
              ((Entity) this.NPC).velocity.Y += 0.25f;
          }
          else if ((double) ((Entity) this.NPC).velocity.Y > (double) vector2_7.Y)
          {
            ((Entity) this.NPC).velocity.Y -= 0.25f;
            if ((double) ((Entity) this.NPC).velocity.Y > 0.0 && (double) vector2_7.Y < 0.0)
              ((Entity) this.NPC).velocity.Y -= 0.25f;
          }
          if ((double) ++this.NPC.localAI[0] > 3.0)
          {
            this.NPC.localAI[0] = 0.0f;
            if (FargoSoulsUtil.HostCheck && (double) this.NPC.ai[1] < 90.0)
            {
              SoundEngine.PlaySound(ref SoundID.Item34, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              Vector2 vector2_8;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_8).\u002Ector(40f, 50f);
              if (((Entity) this.NPC).direction < 0)
              {
                vector2_8.X *= -1f;
                vector2_8 = Utils.RotatedBy(vector2_8, Math.PI, new Vector2());
              }
              Vector2 vector2_9 = Vector2.op_Addition(Utils.RotatedBy(vector2_8, (double) this.NPC.rotation, new Vector2()), ((Entity) this.NPC).Center);
              Vector2 vector2_10 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (Main.rand.NextDouble() - 0.5) * Math.PI / 10.0, new Vector2());
              ((Vector2) ref vector2_10).Normalize();
              Vector2 vector2_11 = Vector2.op_Multiply(vector2_10, Utils.NextFloat(Main.rand, 8f, 12f));
              int num7 = 467;
              if (Utils.NextBool(Main.rand))
              {
                num7 = ModContent.ProjectileType<WillFireball>();
                vector2_11 = Vector2.op_Multiply(vector2_11, 2.5f);
              }
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_9, vector2_11, num7, this.NPC.defDamage / 4, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          if ((double) --this.NPC.localAI[1] < 0.0)
          {
            this.NPC.localAI[1] = (double) this.NPC.localAI[3] == 1.0 ? 35f : 180f;
            if ((double) this.NPC.localAI[2] == 1.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), new Vector2(((Entity) player).Center.X, Math.Max(600f, ((Entity) player).Center.Y - 2000f)), Vector2.UnitY, ModContent.ProjectileType<WillDeathraySmall>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.33333337f), 0.0f, Main.myPlayer, ((Entity) player).Center.X, (float) ((Entity) this.NPC).whoAmI, 0.0f);
          }
          if ((double) ++this.NPC.ai[1] == 1.0)
          {
            SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            break;
          }
          if ((double) this.NPC.ai[1] > 120.0)
          {
            this.NPC.ai[0] = 0.0f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          }
          break;
        default:
          this.NPC.ai[0] = 0.0f;
          goto case 0.0f;
      }
      if (this.NPC.spriteDirection >= 0 || (double) this.NPC.ai[0] == -1.0)
        return;
      this.NPC.rotation += 3.14159274f;
    }

    private void Movement(Vector2 targetPos, float speedModifier, float cap = 12f)
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

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter > 4.0)
      {
        this.NPC.frameCounter = 0.0;
        this.NPC.frame.Y += frameHeight;
      }
      if ((double) this.NPC.ai[0] == 0.0 || (double) this.NPC.ai[0] == 2.0)
      {
        if (this.NPC.frame.Y < 6 * frameHeight)
          return;
        this.NPC.frame.Y = 0;
      }
      else
        this.NPC.frame.Y = frameHeight * 7;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (WorldSavingSystem.EternityMode)
      {
        target.AddBuff(ModContent.BuffType<DefenselessBuff>(), 300, true, false);
        target.AddBuff(ModContent.BuffType<MidasBuff>(), 300, true, false);
      }
      target.AddBuff(30, 300, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
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
          interpolatedStringHandler.AppendLiteral("WillGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[7], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<WillForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<WillChampionRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<EnerGear>(), 4));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<WillDye>(), 1, 1, 1));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Will/" + ((ModType) this).Name + "_Glow", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Will/" + ((ModType) this).Name + "_Glow2", (AssetRequestMode) 1).Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double rotation1 = (double) this.NPC.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale1 = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D1, vector2_2, nullable, alpha, (float) rotation1, vector2_3, (float) scale1, spriteEffects2, 0.0f);
      Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.White, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects1, 0.0f);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      GameShaders.Armor.GetShaderFromItemId(3527).Apply((Entity) this.NPC, new DrawData?());
      Color color1 = Color.op_Multiply(Color.op_Multiply(Color.White, this.NPC.Opacity), 0.5f);
      for (float index1 = 0.0f; (double) index1 < (double) NPCID.Sets.TrailCacheLength[this.NPC.type]; index1 += 0.25f)
      {
        Color color2 = Color.op_Multiply(Color.op_Multiply(color1, 0.25f), ((float) NPCID.Sets.TrailCacheLength[this.NPC.type] - index1) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        float scale2 = this.NPC.scale;
        int index2 = (int) index1 - 1;
        if (index2 >= 0)
        {
          ref Vector2 local = ref this.NPC.oldPos[index2];
          float rotation2 = this.NPC.rotation;
          Vector2 vector2_4 = Vector2.op_Addition(Vector2.Lerp(this.NPC.oldPos[(int) index1], this.NPC.oldPos[index2], (float) (1.0 - (double) index1 % 1.0)), Vector2.op_Division(((Entity) this.NPC).Size, 2f));
          Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(vector2_4, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color2, rotation2, vector2_1, scale2, spriteEffects1, 0.0f);
        }
      }
      Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color1, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects1, 0.0f);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;
    }
  }
}
