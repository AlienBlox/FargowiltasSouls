// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Terra.TerraChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
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
using System.Linq;
using System.Runtime.CompilerServices;
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
namespace FargowiltasSouls.Content.Bosses.Champions.Terra
{
  [AutoloadBossHead]
  public class TerraChampion : ModNPC
  {
    private bool spawned;

    public virtual void SetStaticDefaults()
    {
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 5;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
      NPCID.Sets.MPAllowedEnemies[this.Type] = true;
      NPCID.Sets.NoMultiplayerSmoothingByType[this.NPC.type] = true;
      NPCID.Sets.BossBestiaryPriority.Add(this.NPC.type);
      NPCID.Sets.ImmuneToRegularBuffs[this.Type] = true;
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Content/Bosses/Champions/Terra/" + ((ModType) this).Name + "_Still";
      bestiaryDrawModifiers1.Scale = 1.25f;
      bestiaryDrawModifiers1.Position = new Vector2(210f, 0.0f);
      bestiaryDrawModifiers1.PortraitScale = new float?(1.25f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(160f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 80;
      ((Entity) this.NPC).height = 80;
      this.NPC.damage = 140;
      this.NPC.defense = 80;
      this.NPC.lifeMax = 170000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit41);
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
      this.NPC.behindTiles = true;
      this.NPC.trapImmune = true;
      this.NPC.scale *= 1.5f;
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
      return (double) ((Entity) this.NPC).Distance(FargoSoulsUtil.ClosestPointInHitbox((Entity) target, ((Entity) this.NPC).Center)) < 30.0 * (double) this.NPC.scale;
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
      this.NPC.ai[3] = 0.0f;
      if (!this.spawned)
      {
        this.spawned = true;
        this.NPC.TargetClosest(false);
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
          this.NPC.oldPos[index] = ((Entity) this.NPC).position;
        if (FargoSoulsUtil.HostCheck)
        {
          int num1 = ((Entity) this.NPC).whoAmI;
          for (int index1 = 0; index1 < 99; ++index1)
          {
            int num2 = index1 == 98 ? ModContent.NPCType<TerraChampionTail>() : ModContent.NPCType<TerraChampionBody>();
            int index2 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, num2, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
            if (index2 != Main.maxNPCs)
            {
              Main.npc[index2].ai[1] = (float) num1;
              Main.npc[index2].ai[3] = (float) ((Entity) this.NPC).whoAmI;
              Main.npc[index2].realLife = ((Entity) this.NPC).whoAmI;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              num1 = index2;
            }
            else
            {
              ((Entity) this.NPC).active = false;
              if (Main.netMode != 2)
                return;
              NetMessage.SendData(23, -1, -1, (NetworkText) null, ((Entity) this.NPC).whoAmI, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              return;
            }
          }
        }
      }
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      Player player1 = Main.player[this.NPC.target];
      if (this.NPC.HasValidTarget && (double) ((Entity) player1).Center.Y >= Main.worldSurface * 16.0 && !player1.ZoneUnderworldHeight)
        this.NPC.timeLeft = 600;
      if (WorldSavingSystem.EternityMode && (double) this.NPC.ai[1] != -1.0 && this.NPC.life < this.NPC.lifeMax / 10)
      {
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
        this.NPC.life = this.NPC.lifeMax / 10;
        ((Entity) this.NPC).velocity = Vector2.Zero;
        this.NPC.ai[1] = -1f;
        this.NPC.localAI[0] = 0.0f;
        this.NPC.localAI[1] = 0.0f;
        this.NPC.localAI[2] = 0.0f;
        this.NPC.localAI[3] = 0.0f;
        this.NPC.netUpdate = true;
      }
      switch (this.NPC.ai[1])
      {
        case -1f:
          if (!((Entity) player1).active || player1.dead || (double) ((Entity) player1).Center.Y < Main.worldSurface * 16.0 || player1.ZoneUnderworldHeight)
          {
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            ++((Entity) this.NPC).velocity.Y;
            break;
          }
          this.NPC.scale = 3f;
          Vector2 center = ((Entity) player1).Center;
          if ((double) ((Entity) this.NPC).Distance(center) > 50.0)
            this.Movement(center, 0.16f, 32f);
          this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center));
          if ((double) ++this.NPC.localAI[0] > 50.0)
          {
            this.NPC.localAI[0] = 0.0f;
            if ((double) this.NPC.localAI[1] > 120.0)
            {
              SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2_1 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center);
                float num = Utils.NextBool(Main.rand) ? 1f : -1f;
                Vector2 vector2_2 = Vector2.op_Multiply(Vector2.Normalize(vector2_1), 22f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_2, ModContent.ProjectileType<HostileLightning>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, Utils.ToRotation(vector2_1), num, 0.0f);
              }
            }
          }
          if ((double) --this.NPC.localAI[1] < 0.0)
          {
            this.NPC.localAI[1] = 420f;
            if (FargoSoulsUtil.HostCheck)
            {
              int index = Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<TerraLightningOrb2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
              Main.projectile[index].localAI[0] += 1f + Utils.NextFloatDirection(Main.rand);
              Main.projectile[index].localAI[1] = Utils.NextBool(Main.rand) ? 1f : -1f;
              Main.projectile[index].netUpdate = true;
              break;
            }
            break;
          }
          break;
        case 0.0f:
        case 3f:
        case 6f:
        case 9f:
          this.WormMovement(player1, 17.22f, 0.122f, 0.188f);
          if ((double) ++this.NPC.localAI[0] > 420.0)
          {
            ++this.NPC.ai[1];
            this.NPC.localAI[0] = 0.0f;
          }
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          break;
        case 1f:
        case 4f:
        case 7f:
        case 10f:
        case 13f:
          this.NPC.ai[3] = 2f;
          if ((double) ++this.NPC.localAI[0] < 90.0)
          {
            Vector2 targetPos = Vector2.op_Addition(((Entity) player1).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player1).Center), 900f));
            this.Movement(targetPos, 0.4f, 18f);
            if ((double) ((Entity) this.NPC).Distance(targetPos) < 100.0)
              this.NPC.localAI[0] = 89f;
          }
          else if ((double) this.NPC.localAI[0] == 90.0)
          {
            foreach (NPC npc in ((IEnumerable<NPC>) Main.npc).Where<NPC>((Func<NPC, bool>) (n => ((Entity) n).active && n.realLife == ((Entity) this.NPC).whoAmI)))
              npc.netUpdate = true;
            if (FargoSoulsUtil.HostCheck)
            {
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 12f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRingHollow>(), 0, 0.0f, Main.myPlayer, 12f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
            }
          }
          else
          {
            bool flag = (double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.NPC).velocity) - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center)))) < (double) MathHelper.ToRadians(45f);
            if (((double) this.NPC.localAI[0] <= 300.0 ? 0 : ((double) this.NPC.localAI[0] > 360.0 | flag ? 1 : 0)) != 0)
            {
              ++this.NPC.ai[1];
              this.NPC.localAI[0] = 0.0f;
              NPC npc = this.NPC;
              ((Entity) npc).velocity = Vector2.op_Division(((Entity) npc).velocity, 4f);
            }
            else
            {
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), Math.Min(48f, ((Vector2) ref ((Entity) this.NPC).velocity).Length() + 1f));
              NPC npc = this.NPC;
              ((Entity) npc).velocity = Vector2.op_Addition(((Entity) npc).velocity, Vector2.op_Division(Vector2.op_Multiply(Utils.RotatedBy(((Entity) this.NPC).velocity, 1.5707963705062866, new Vector2()), ((Vector2) ref ((Entity) this.NPC).velocity).Length()), 300f));
            }
          }
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          break;
        case 2f:
          if ((double) this.NPC.localAI[1] == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
            this.NPC.localAI[1] = 1f;
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center), 24f);
          }
          if ((double) ++this.NPC.localAI[2] > 2.0)
          {
            this.NPC.localAI[2] = 0.0f;
            if (FargoSoulsUtil.HostCheck)
            {
              Vector2 vector2_3 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center), 12f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_3, ModContent.ProjectileType<TerraFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              float num = Utils.ToRotation(((Entity) this.NPC).velocity) - Utils.ToRotation(vector2_3);
              Vector2 vector2_4 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), (double) num, new Vector2()), 12f);
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2_4, ModContent.ProjectileType<TerraFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
            }
          }
          double num3 = (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center)) - (double) Utils.ToRotation(((Entity) this.NPC).velocity);
          while (num3 > Math.PI)
            num3 -= 2.0 * Math.PI;
          while (num3 < -1.0 * Math.PI)
            num3 += 2.0 * Math.PI;
          if ((double) ++this.NPC.localAI[0] > 240.0 || Math.Abs(num3) > Math.PI / 2.0 && (double) ((Entity) this.NPC).Distance(((Entity) player1).Center) > 1200.0)
          {
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), Math.PI / 2.0, new Vector2()), 18f);
            ++this.NPC.ai[1];
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
          }
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          break;
        case 5f:
          this.NPC.ai[3] = 1f;
          if ((double) this.NPC.localAI[0] == 0.0)
          {
            this.NPC.localAI[1] = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center));
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
          }
          float num4 = (float) Math.Sin(6.2831854820251465 * ((double) this.NPC.localAI[0] / 360.0 * 3.0 + 0.25));
          this.NPC.rotation = this.NPC.localAI[1] + 1.57079637f * num4;
          ((Entity) this.NPC).velocity = Vector2.op_Multiply(36f, Utils.ToRotationVector2(this.NPC.rotation));
          if ((double) Math.Abs(num4) < 1.0 / 1000.0)
          {
            SoundEngine.PlaySound(ref SoundID.Item12, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              for (int index = -5; index <= 5; ++index)
              {
                float num5 = (float) (1.5707963705062866 + 0.31415927410125732 * (double) index) * (float) Math.Sign(-num4);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(Vector2.UnitX, (double) this.NPC.localAI[1] + (double) num5, new Vector2())), 467, FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              for (int index = -5; index <= 5; ++index)
              {
                float num6 = (float) (1.5707963705062866 + 0.3490658700466156 * (double) index) * (float) Math.Sign(-num4);
                Vector2 vector2 = Vector2.op_Multiply(Utils.RotatedBy(Vector2.UnitX, Math.PI / 4.0 * (Main.rand.NextDouble() - 0.5), new Vector2()), 36f);
                float num7 = Utils.NextBool(Main.rand) ? 1f : -1f;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(vector2, (double) this.NPC.localAI[1] + (double) num6, new Vector2()), ModContent.ProjectileType<HostileLightning>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, this.NPC.localAI[1] + num6, num7, 0.0f);
              }
            }
          }
          if ((double) ++this.NPC.localAI[0] > 288.0)
          {
            ++this.NPC.ai[1];
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            this.NPC.localAI[2] = 0.0f;
            this.NPC.localAI[3] = 0.0f;
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center), ((Vector2) ref ((Entity) this.NPC).velocity).Length());
            break;
          }
          break;
        case 8f:
          if ((double) this.NPC.localAI[1] == 0.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
            this.NPC.localAI[1] = 1f;
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center), 36f);
          }
          if ((double) this.NPC.localAI[3] == 0.0)
          {
            double num8 = (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center)) - (double) Utils.ToRotation(((Entity) this.NPC).velocity);
            while (num8 > Math.PI)
              num8 -= 2.0 * Math.PI;
            while (num8 < -1.0 * Math.PI)
              num8 += 2.0 * Math.PI;
            if (Math.Abs(num8) > Math.PI / 2.0)
            {
              this.NPC.localAI[3] = (float) Math.Sign(num8);
              ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 24f);
            }
          }
          else
          {
            ((Entity) this.NPC).velocity = Utils.RotatedBy(((Entity) this.NPC).velocity, (double) MathHelper.ToRadians(2.5f) * (double) this.NPC.localAI[3], new Vector2());
            if ((double) ++this.NPC.localAI[2] > 2.0)
            {
              this.NPC.localAI[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2 = Vector2.op_Multiply(12f, Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), Math.PI / 2.0, new Vector2()));
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<TerraFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_UnaryNegation(vector2), ModContent.ProjectileType<TerraFireball>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.localAI[0] > 75.0)
            {
              ++this.NPC.ai[1];
              this.NPC.localAI[0] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
            }
          }
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          break;
        case 11f:
          this.NPC.ai[3] = 2f;
          Vector2 targetPos1 = Vector2.op_Addition(((Entity) player1).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player1).Center), 600f));
          this.Movement(targetPos1, 0.4f, 32f);
          if ((double) ++this.NPC.localAI[0] > 300.0 || (double) ((Entity) this.NPC).Distance(targetPos1) < 50.0)
          {
            ++this.NPC.ai[1];
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = ((Entity) this.NPC).Distance(((Entity) player1).Center);
            ((Entity) this.NPC).velocity = Vector2.op_Multiply(32f, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player1).Center), Math.PI / 2.0, new Vector2()));
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) player1).Center), (SoundUpdateCallback) null);
          }
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          break;
        case 12f:
          this.NPC.ai[3] = 2f;
          Vector2 vector2_5 = Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Utils.RotatedBy(Vector2.Normalize(((Entity) this.NPC).velocity), -1.0 * Math.PI / 2.0, new Vector2()), 32f), 32f), 600f);
          ((Entity) this.NPC).velocity = Vector2.op_Addition(Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), 32f), vector2_5);
          this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
          Vector2 vector2_6 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.Normalize(Utils.RotatedBy(((Entity) this.NPC).velocity, -1.0 * Math.PI / 2.0, new Vector2())), 600f));
          Player player2 = Main.player[this.NPC.target];
          if (((Entity) player2).active && !player2.dead)
          {
            float num9 = ((Entity) player2).Distance(vector2_6);
            if ((double) num9 > 600.0 && (double) num9 < 3000.0)
            {
              Vector2 vector2_7 = Vector2.op_Subtraction(vector2_6, ((Entity) player2).Center);
              float num10 = ((Vector2) ref vector2_7).Length() - 600f;
              ((Vector2) ref vector2_7).Normalize();
              vector2_7 = Vector2.op_Multiply(vector2_7, (double) num10 < 34.0 ? num10 : 34f);
              Player player3 = player2;
              ((Entity) player3).position = Vector2.op_Addition(((Entity) player3).position, vector2_7);
              for (int index3 = 0; index3 < 20; ++index3)
              {
                int index4 = Dust.NewDust(((Entity) player2).position, ((Entity) player2).width, ((Entity) player2).height, 87, 0.0f, 0.0f, 0, new Color(), 2f);
                Main.dust[index4].noGravity = true;
                Dust dust = Main.dust[index4];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 5f);
              }
            }
          }
          if ((double) this.NPC.localAI[0] == 0.0 && FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<TerraLightningOrb2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
          if ((double) ++this.NPC.localAI[0] > 420.0)
          {
            ++this.NPC.ai[1];
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            break;
          }
          break;
        default:
          this.NPC.ai[1] = 0.0f;
          goto case 0.0f;
      }
      this.NPC.netUpdate = true;
      Vector2 vector2_8 = Vector2.op_Multiply(new Vector2(77f, -41f), this.NPC.scale);
      int index5 = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, ((Entity) this.NPC).velocity), Utils.RotatedBy(vector2_8, (double) this.NPC.rotation, new Vector2())), 0, 0, 6, ((Entity) this.NPC).velocity.X * 0.4f, ((Entity) this.NPC).velocity.Y * 0.4f, 0, new Color(), 2f);
      Dust dust1 = Main.dust[index5];
      dust1.velocity = Vector2.op_Multiply(dust1.velocity, 2f);
      if (Utils.NextBool(Main.rand))
      {
        ++Main.dust[index5].scale;
        Main.dust[index5].noGravity = true;
      }
      vector2_8.Y *= -1f;
      int index6 = Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, ((Entity) this.NPC).velocity), Utils.RotatedBy(vector2_8, (double) this.NPC.rotation, new Vector2())), 0, 0, 6, ((Entity) this.NPC).velocity.X * 0.4f, ((Entity) this.NPC).velocity.Y * 0.4f, 0, new Color(), 2f);
      Dust dust2 = Main.dust[index6];
      dust2.velocity = Vector2.op_Multiply(dust2.velocity, 2f);
      if (Utils.NextBool(Main.rand))
      {
        ++Main.dust[index6].scale;
        Main.dust[index6].noGravity = true;
      }
      if ((double) this.NPC.ai[1] != -1.0 && Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height) && this.NPC.soundDelay == 0)
      {
        this.NPC.soundDelay = (int) ((double) ((Entity) this.NPC).Distance(((Entity) player1).Center) / 40.0);
        if (this.NPC.soundDelay < 10)
          this.NPC.soundDelay = 10;
        if (this.NPC.soundDelay > 20)
          this.NPC.soundDelay = 20;
        SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
      }
      int num11 = NPCID.Sets.TrailCacheLength[this.NPC.type] - (int) this.NPC.ai[3] - 1;
      Vector2 vector2_9 = Vector2.op_Addition(((Entity) this.NPC).position, ((Entity) this.NPC).velocity);
      Vector2 vector2_10 = Vector2.op_Subtraction(vector2_9, this.NPC.oldPos[num11 - 1]);
      if ((double) ((Vector2) ref vector2_10).Length() <= 45.0 * (double) this.NPC.scale / 1.5 * 1.25)
        return;
      this.NPC.oldPos[num11 - 1] = Vector2.op_Addition(vector2_9, Vector2.op_Multiply(Vector2.op_Division(Vector2.op_Multiply(Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(this.NPC.oldPos[num11 - 1], vector2_9)), 45f), this.NPC.scale), 1.5f), 1.25f));
    }

    private void WormMovement(Player player, float maxSpeed, float turnSpeed, float accel)
    {
      if (!((Entity) player).active || player.dead || (double) ((Entity) player).Center.Y < Main.worldSurface * 16.0 || player.ZoneUnderworldHeight)
      {
        this.NPC.TargetClosest(false);
        if (this.NPC.timeLeft > 30)
          this.NPC.timeLeft = 30;
        ++((Entity) this.NPC).velocity.Y;
        this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
      }
      else
      {
        float num1 = ((Vector2) ref ((Entity) player).velocity).Length() * 1.5f;
        bool flag = (double) Math.Abs(MathHelper.WrapAngle(Utils.ToRotation(((Entity) this.NPC).velocity) - Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)))) < (double) MathHelper.ToRadians(45f);
        if ((double) maxSpeed < (double) num1 & flag)
          maxSpeed = num1;
        if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 1200.0)
        {
          turnSpeed *= 2f;
          accel *= 2f;
          if (flag && (double) maxSpeed < 30.0)
            maxSpeed = 30f;
        }
        if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > (double) maxSpeed)
        {
          NPC npc = this.NPC;
          ((Entity) npc).velocity = Vector2.op_Multiply(((Entity) npc).velocity, 0.975f);
        }
        Vector2 center = ((Entity) player).Center;
        float x = center.X;
        double y1 = (double) center.Y;
        float num2 = x - ((Entity) this.NPC).Center.X;
        double y2 = (double) ((Entity) this.NPC).Center.Y;
        float num3 = (float) (y1 - y2);
        Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
        float num4 = (float) Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
        float num5 = Math.Abs(num2);
        float num6 = Math.Abs(num3);
        float num7 = maxSpeed / num4;
        float num8 = num2 * num7;
        float num9 = num3 * num7;
        if (((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) num8 > 0.0 || (double) ((Entity) this.NPC).velocity.X < 0.0 && (double) num8 < 0.0) && ((double) ((Entity) this.NPC).velocity.Y > 0.0 && (double) num9 > 0.0 || (double) ((Entity) this.NPC).velocity.Y < 0.0 && (double) num9 < 0.0))
        {
          if ((double) ((Entity) this.NPC).velocity.X < (double) num8)
            ((Entity) this.NPC).velocity.X += accel;
          else if ((double) ((Entity) this.NPC).velocity.X > (double) num8)
            ((Entity) this.NPC).velocity.X -= accel;
          if ((double) ((Entity) this.NPC).velocity.Y < (double) num9)
            ((Entity) this.NPC).velocity.Y += accel;
          else if ((double) ((Entity) this.NPC).velocity.Y > (double) num9)
            ((Entity) this.NPC).velocity.Y -= accel;
        }
        if ((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) num8 > 0.0 || (double) ((Entity) this.NPC).velocity.X < 0.0 && (double) num8 < 0.0 || (double) ((Entity) this.NPC).velocity.Y > 0.0 && (double) num9 > 0.0 || (double) ((Entity) this.NPC).velocity.Y < 0.0 && (double) num9 < 0.0)
        {
          if ((double) ((Entity) this.NPC).velocity.X < (double) num8)
            ((Entity) this.NPC).velocity.X += turnSpeed;
          else if ((double) ((Entity) this.NPC).velocity.X > (double) num8)
            ((Entity) this.NPC).velocity.X -= turnSpeed;
          if ((double) ((Entity) this.NPC).velocity.Y < (double) num9)
            ((Entity) this.NPC).velocity.Y += turnSpeed;
          else if ((double) ((Entity) this.NPC).velocity.Y > (double) num9)
            ((Entity) this.NPC).velocity.Y -= turnSpeed;
          if ((double) Math.Abs(num9) < (double) maxSpeed * 0.20000000298023224 && ((double) ((Entity) this.NPC).velocity.X > 0.0 && (double) num8 < 0.0 || (double) ((Entity) this.NPC).velocity.X < 0.0 && (double) num8 > 0.0))
          {
            if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
              ((Entity) this.NPC).velocity.Y += turnSpeed * 2f;
            else
              ((Entity) this.NPC).velocity.Y -= turnSpeed * 2f;
          }
          if ((double) Math.Abs(num8) >= (double) maxSpeed * 0.20000000298023224 || ((double) ((Entity) this.NPC).velocity.Y <= 0.0 || (double) num9 >= 0.0) && ((double) ((Entity) this.NPC).velocity.Y >= 0.0 || (double) num9 <= 0.0))
            return;
          if ((double) ((Entity) this.NPC).velocity.X > 0.0)
            ((Entity) this.NPC).velocity.X += turnSpeed * 2f;
          else
            ((Entity) this.NPC).velocity.X -= turnSpeed * 2f;
        }
        else if ((double) num5 > (double) num6)
        {
          if ((double) ((Entity) this.NPC).velocity.X < (double) num8)
            ((Entity) this.NPC).velocity.X += turnSpeed * 1.1f;
          else if ((double) ((Entity) this.NPC).velocity.X > (double) num8)
            ((Entity) this.NPC).velocity.X -= turnSpeed * 1.1f;
          if ((double) Math.Abs(((Entity) this.NPC).velocity.X) + (double) Math.Abs(((Entity) this.NPC).velocity.Y) >= (double) maxSpeed * 0.5)
            return;
          if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
            ((Entity) this.NPC).velocity.Y += turnSpeed;
          else
            ((Entity) this.NPC).velocity.Y -= turnSpeed;
        }
        else
        {
          if ((double) ((Entity) this.NPC).velocity.Y < (double) num9)
            ((Entity) this.NPC).velocity.Y += turnSpeed * 1.1f;
          else if ((double) ((Entity) this.NPC).velocity.Y > (double) num9)
            ((Entity) this.NPC).velocity.Y -= turnSpeed * 1.1f;
          if ((double) Math.Abs(((Entity) this.NPC).velocity.X) + (double) Math.Abs(((Entity) this.NPC).velocity.Y) >= (double) maxSpeed * 0.5)
            return;
          if ((double) ((Entity) this.NPC).velocity.X > 0.0)
            ((Entity) this.NPC).velocity.X += turnSpeed;
          else
            ((Entity) this.NPC).velocity.X -= turnSpeed;
        }
      }
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
      if (this.NPC.life >= this.NPC.lifeMax / 10)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Division(local, 3f);
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      target.AddBuff(24, 600, true, false);
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<LivingWastelandBuff>(), 600, true, false);
      target.AddBuff(ModContent.BuffType<LightningRodBuff>(), 600, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 1; index <= 3; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(9, 1);
          interpolatedStringHandler.AppendLiteral("TerraGore");
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
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[1], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<TerraForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<TerraChampionRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<MostlyOrdinaryRock>(), 4));
    }

    public virtual void BossHeadRotation(ref float rotation) => rotation = this.NPC.rotation;

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects1 = this.NPC.spriteDirection < 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color alpha = this.NPC.GetAlpha(drawColor);
      double rotation = (double) this.NPC.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale = (double) this.NPC.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D, vector2_2, nullable, alpha, (float) rotation, vector2_3, (float) scale, spriteEffects2, 0.0f);
      Main.EntitySpriteDraw(ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Terra/TerraChampion_Glow", (AssetRequestMode) 1).Value, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.White, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects1, 0.0f);
      return false;
    }
  }
}
