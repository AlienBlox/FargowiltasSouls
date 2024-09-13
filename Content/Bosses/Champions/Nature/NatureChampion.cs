// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Nature.NatureChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
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
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Nature
{
  [AutoloadBossHead]
  public class NatureChampion : ModNPC
  {
    public int[] heads = new int[6]
    {
      -1,
      -1,
      -1,
      -1,
      -1,
      -1
    };
    public int lastSet;
    public static readonly KeyValuePair<int, int>[] configurations = new KeyValuePair<int, int>[9]
    {
      new KeyValuePair<int, int>(0, 1),
      new KeyValuePair<int, int>(1, 3),
      new KeyValuePair<int, int>(3, 5),
      new KeyValuePair<int, int>(3, 4),
      new KeyValuePair<int, int>(2, 4),
      new KeyValuePair<int, int>(0, 5),
      new KeyValuePair<int, int>(1, 2),
      new KeyValuePair<int, int>(2, 5),
      new KeyValuePair<int, int>(0, 4)
    };
    public Vector2 position;
    public Vector2 oldPosition;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 14;
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
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Content/Bosses/Champions/Nature/" + ((ModType) this).Name + "_Still";
      bestiaryDrawModifiers1.Scale = 0.3f;
      bestiaryDrawModifiers1.Position = new Vector2(48f, 64f);
      bestiaryDrawModifiers1.PortraitScale = new float?(0.3f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(48f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(24f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundSnow,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 180;
      ((Entity) this.NPC).height = 120;
      this.NPC.damage = 110;
      this.NPC.defense = 100;
      this.NPC.lifeMax = 900000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit6);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath1);
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

    public virtual bool CanHitPlayer(Player target, ref int CooldownSlot) => false;

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      for (int index = 0; index < this.heads.Length; ++index)
        writer.Write(this.heads[index]);
      writer.Write(this.lastSet);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      for (int index = 0; index < this.heads.Length; ++index)
        this.heads[index] = reader.ReadInt32();
      this.lastSet = reader.ReadInt32();
    }

    public virtual void AI()
    {
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(false);
        this.NPC.localAI[3] = 1f;
        if (FargoSoulsUtil.HostCheck)
        {
          int index1 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, -3f, this.NPC.target);
          if (index1 != Main.maxNPCs)
          {
            this.heads[0] = index1;
            ((Entity) Main.npc[index1]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index1]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          int index2 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, -2f, this.NPC.target);
          if (index2 != Main.maxNPCs)
          {
            this.heads[1] = index2;
            ((Entity) Main.npc[index2]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index2]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          int index3 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, -1f, this.NPC.target);
          if (index3 != Main.maxNPCs)
          {
            this.heads[2] = index3;
            ((Entity) Main.npc[index3]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index3]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index3, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          int index4 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, 1f, this.NPC.target);
          if (index4 != Main.maxNPCs)
          {
            this.heads[3] = index4;
            ((Entity) Main.npc[index4]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index4]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index4, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          int index5 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, 2f, this.NPC.target);
          if (index5 != Main.maxNPCs)
          {
            this.heads[4] = index5;
            ((Entity) Main.npc[index5]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index5]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index5, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          int index6 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<NatureChampionHead>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f, 3f, this.NPC.target);
          if (index6 != Main.maxNPCs)
          {
            this.heads[5] = index6;
            ((Entity) Main.npc[index6]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
            ((Entity) Main.npc[index6]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
            if (Main.netMode == 2)
              NetMessage.SendData(23, -1, -1, (NetworkText) null, index6, 0.0f, 0.0f, 0.0f, 0, 0, 0);
          }
          for (int index7 = 0; index7 < this.heads.Length; ++index7)
          {
            if (this.heads[index7] == -1 && FargoSoulsUtil.HostCheck)
            {
              ((Entity) this.NPC).active = false;
              return;
            }
          }
        }
      }
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      Player player = Main.player[this.NPC.target];
      if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 3000.0 && (double) ((Entity) player).Center.Y >= Main.worldSurface * 16.0 && !player.ZoneUnderworldHeight)
        this.NPC.timeLeft = 600;
      if ((double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).position.X)
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = -1;
      else if ((double) ((Entity) player).Center.X > (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width)
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = 1;
      switch (this.NPC.ai[0])
      {
        case -1f:
          this.NPC.noTileCollide = true;
          this.NPC.noGravity = true;
          if ((double) ((Entity) this.NPC).position.X < (double) ((Entity) player).Center.X && (double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width)
          {
            ((Entity) this.NPC).velocity.X *= 0.92f;
            if ((double) Math.Abs(((Entity) this.NPC).velocity.X) < 0.10000000149011612)
              ((Entity) this.NPC).velocity.X = 0.0f;
          }
          else
          {
            float num = 2f;
            if ((double) ((Entity) player).Center.X > (double) ((Entity) this.NPC).Center.X)
              ((Entity) this.NPC).velocity.X = (float) (((double) ((Entity) this.NPC).velocity.X * 20.0 + (double) num) / 21.0);
            else
              ((Entity) this.NPC).velocity.X = (float) (((double) ((Entity) this.NPC).velocity.X * 20.0 - (double) num) / 21.0);
          }
          bool flag1 = false;
          for (int x = (int) ((Entity) this.NPC).position.X; (double) x <= (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width; x += 16)
          {
            Tile tileSafely = Framing.GetTileSafely(new Vector2((float) x, (float) ((double) ((Entity) this.NPC).position.Y + (double) ((Entity) this.NPC).height + (double) ((Entity) this.NPC).velocity.Y + 1.0)));
            if (((Tile) ref tileSafely).TileType == (ushort) 19)
            {
              flag1 = true;
              break;
            }
          }
          bool flag2 = Collision.SolidCollision(((Entity) this.NPC).position, ((Entity) this.NPC).width, ((Entity) this.NPC).height);
          if ((double) ((Entity) this.NPC).position.X < (double) ((Entity) player).position.X && (double) ((Entity) this.NPC).position.X + (double) ((Entity) this.NPC).width > (double) ((Entity) player).position.X + (double) ((Entity) player).width && (double) ((Entity) this.NPC).position.Y + (double) ((Entity) this.NPC).height < (double) ((Entity) player).position.Y + (double) ((Entity) player).height - 16.0)
            ((Entity) this.NPC).velocity.Y += 0.5f;
          else if (flag2 || flag1 && (double) ((Entity) player).position.Y + (double) ((Entity) player).height <= (double) ((Entity) this.NPC).position.Y + (double) ((Entity) this.NPC).height)
          {
            if ((double) ((Entity) this.NPC).velocity.Y > 0.0)
              ((Entity) this.NPC).velocity.Y = 0.0f;
            if (flag2)
            {
              if ((double) ((Entity) this.NPC).velocity.Y > -0.20000000298023224)
                ((Entity) this.NPC).velocity.Y -= 0.025f;
              else
                ((Entity) this.NPC).velocity.Y -= 0.2f;
              if ((double) ((Entity) this.NPC).velocity.Y < -4.0)
                ((Entity) this.NPC).velocity.Y = -4f;
            }
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
          if ((double) ((Entity) this.NPC).velocity.Y > 10.0)
          {
            ((Entity) this.NPC).velocity.Y = 10f;
            break;
          }
          break;
        case 0.0f:
          this.NPC.noTileCollide = false;
          this.NPC.noGravity = false;
          if ((double) ++this.NPC.ai[1] > 45.0)
          {
            this.NPC.TargetClosest(true);
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            goto case -1f;
          }
          else
            goto case -1f;
        case 1f:
        case 9f:
          int num1 = 60;
          if ((double) this.NPC.ai[3] == 1.0)
            num1 = 30;
          this.NPC.noGravity = true;
          this.NPC.noTileCollide = true;
          if ((double) this.NPC.ai[2] == 0.0)
          {
            StompDust();
            this.NPC.ai[2] = 1f;
            this.NPC.netUpdate = true;
            Vector2 center = ((Entity) player).Center;
            center.Y -= (double) this.NPC.ai[3] == 1.0 ? 300f : 600f;
            ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.NPC).Center), (float) num1);
          }
          if ((double) ++this.NPC.ai[1] > (double) (num1 + ((double) this.NPC.ai[3] == 1.0 ? 1 : 18)))
          {
            this.NPC.noGravity = false;
            this.NPC.noTileCollide = false;
            if ((double) ((Entity) this.NPC).velocity.Y == 0.0 || (double) this.NPC.ai[3] == 1.0)
            {
              StompDust();
              if ((double) this.NPC.ai[3] == 1.0)
              {
                for (int index = Main.rand.Next(2); index < this.heads.Length; index += 2)
                {
                  if ((double) Main.npc[this.heads[index]].ai[0] == 0.0)
                  {
                    Main.npc[this.heads[index]].ai[0] = 4f;
                    Main.npc[this.heads[index]].localAI[0] = 0.0f;
                    Main.npc[this.heads[index]].ai[2] = 0.0f;
                    Main.npc[this.heads[index]].localAI[1] = 0.0f;
                    Main.npc[this.heads[index]].netUpdate = true;
                    int num2;
                    switch (Main.npc[this.heads[index]].ai[3])
                    {
                      case -3f:
                        num2 = -7;
                        break;
                      case -2f:
                        num2 = -8;
                        break;
                      case -1f:
                        num2 = -9;
                        break;
                      case 1f:
                        num2 = -10;
                        break;
                      case 2f:
                        num2 = -11;
                        break;
                      case 3f:
                        num2 = -12;
                        break;
                      default:
                        num2 = 0;
                        break;
                    }
                    int num3 = num2;
                    if (FargoSoulsUtil.HostCheck)
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) this.heads[index], (float) num3, 0.0f);
                  }
                }
              }
              this.NPC.TargetClosest(true);
              ++this.NPC.ai[0];
              this.NPC.ai[1] = (double) this.NPC.ai[3] == 1.0 ? 40f : 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          }
          if ((double) this.NPC.ai[1] > (double) num1)
          {
            if ((double) ((Entity) this.NPC).velocity.X > 2.0)
              ((Entity) this.NPC).velocity.X = 2f;
            if ((double) ((Entity) this.NPC).velocity.X < -2.0)
              ((Entity) this.NPC).velocity.X = -2f;
            ((Entity) this.NPC).velocity.Y = 30f;
            break;
          }
          break;
        case 2f:
        case 4f:
        case 6f:
        case 8f:
        case 10f:
          if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 3000.0 || (double) ((Entity) player).Center.Y < Main.worldSurface * 16.0 || player.ZoneUnderworldHeight)
          {
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            ++((Entity) this.NPC).velocity.Y;
            break;
          }
          goto case 0.0f;
        case 3f:
        case 5f:
        case 7f:
          if ((double) this.NPC.ai[2] == 0.0)
          {
            this.NPC.ai[2] = 1f;
            this.NPC.netUpdate = true;
            int index = Main.rand.Next(NatureChampion.configurations.Length);
            while (this.heads[NatureChampion.configurations[index].Key] == this.heads[NatureChampion.configurations[this.lastSet].Key] || this.heads[NatureChampion.configurations[index].Key] == this.heads[NatureChampion.configurations[this.lastSet].Value] || this.heads[NatureChampion.configurations[index].Value] == this.heads[NatureChampion.configurations[this.lastSet].Key] || this.heads[NatureChampion.configurations[index].Value] == this.heads[NatureChampion.configurations[this.lastSet].Value])
              index = Main.rand.Next(NatureChampion.configurations.Length);
            this.lastSet = index;
            if (Main.expertMode)
            {
              ActivateHead(this.heads[NatureChampion.configurations[index].Key]);
              ActivateHead(this.heads[NatureChampion.configurations[index].Value]);
            }
            else if (Utils.NextBool(Main.rand))
              ActivateHead(this.heads[NatureChampion.configurations[index].Key]);
            else
              ActivateHead(this.heads[NatureChampion.configurations[index].Value]);
          }
          if ((double) ++this.NPC.ai[1] > 300.0)
          {
            this.NPC.TargetClosest(true);
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            goto case -1f;
          }
          else
            goto case -1f;
        case 11f:
          if ((double) this.NPC.ai[2] == 0.0 && WorldSavingSystem.EternityMode)
          {
            this.NPC.ai[2] = 1f;
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            for (int index = 0; index < this.heads.Length; ++index)
            {
              Main.npc[this.heads[index]].ai[0] = 4f;
              Main.npc[this.heads[index]].localAI[0] = 0.0f;
              Main.npc[this.heads[index]].ai[2] = 0.0f;
              Main.npc[this.heads[index]].localAI[1] = 0.0f;
              Main.npc[this.heads[index]].netUpdate = true;
              int num4;
              switch (Main.npc[this.heads[index]].ai[3])
              {
                case -3f:
                  num4 = -7;
                  break;
                case -2f:
                  num4 = -8;
                  break;
                case -1f:
                  num4 = -9;
                  break;
                case 1f:
                  num4 = -10;
                  break;
                case 2f:
                  num4 = -11;
                  break;
                case 3f:
                  num4 = -12;
                  break;
                default:
                  num4 = 0;
                  break;
              }
              int num5 = num4;
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) this.heads[index], (float) num5, 0.0f);
            }
          }
          if ((double) ++this.NPC.ai[1] > 330.0 || !WorldSavingSystem.EternityMode)
          {
            this.NPC.TargetClosest(true);
            ++this.NPC.ai[0];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.netUpdate = true;
            goto case -1f;
          }
          else
            goto case -1f;
        default:
          this.NPC.ai[0] = 0.0f;
          goto case 0.0f;
      }
      if (!WorldSavingSystem.EternityMode)
        return;
      if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 1400.0 && (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) < 3000.0 && (double) ((Entity) player).Center.Y > Main.worldSurface * 16.0 && !player.ZoneUnderworldHeight && (double) this.NPC.ai[0] > 1.0)
      {
        this.NPC.ai[0] = 1f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 1f;
        this.NPC.netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
      Vector2 vector2 = Vector2.op_Multiply(Vector2.Normalize(Vector2.op_Subtraction(((Entity) player).Center, ((Entity) this.NPC).Center)), 1400f);
      for (int index8 = 0; index8 < 20; ++index8)
      {
        int index9 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedByRandom(vector2, 2.0 * Math.PI)), 0, 0, 59, 0.0f, 0.0f, 0, new Color(), 2f);
        Main.dust[index9].velocity = ((Entity) this.NPC).velocity;
        Main.dust[index9].noGravity = true;
      }

      void StompDust()
      {
        SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
        for (int index1 = -2; index1 <= 2; ++index1)
        {
          Vector2 center = ((Entity) this.NPC).Center;
          int num1 = ((Entity) this.NPC).width / 5;
          center.X += (float) (num1 * index1) + Utils.NextFloat(Main.rand, (float) -num1, (float) num1);
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
          float num2 = 0.5f;
          for (int index7 = 0; index7 < 4; ++index7)
          {
            int index8 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, new Vector2(), Main.rand.Next(61, 64), 1f);
            Gore gore = Main.gore[index8];
            gore.velocity = Vector2.op_Multiply(gore.velocity, num2);
            ++Main.gore[index8].velocity.X;
            ++Main.gore[index8].velocity.Y;
          }
        }
      }

      void ActivateHead(int targetHead)
      {
        if ((double) Main.npc[targetHead].ai[0] != 0.0)
          return;
        Main.npc[targetHead].ai[0] += Main.npc[targetHead].ai[3];
        Main.npc[targetHead].localAI[0] = 0.0f;
        Main.npc[targetHead].ai[2] = 0.0f;
        Main.npc[targetHead].localAI[1] = 0.0f;
        Main.npc[targetHead].netUpdate = true;
        SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) Main.npc[targetHead]).Center), (SoundUpdateCallback) null);
        int num1;
        switch (Main.npc[targetHead].ai[3])
        {
          case -3f:
            num1 = -7;
            break;
          case -2f:
            num1 = -8;
            break;
          case -1f:
            num1 = -9;
            break;
          case 1f:
            num1 = -10;
            break;
          case 2f:
            num1 = -11;
            break;
          case 3f:
            num1 = -12;
            break;
          default:
            num1 = 0;
            break;
        }
        int num2 = num1;
        if (!FargoSoulsUtil.HostCheck)
          return;
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) targetHead, (float) num2, 0.0f);
      }
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
      if (Vector2.op_Equality(((Entity) this.NPC).velocity, Vector2.Zero))
      {
        if (this.NPC.frame.Y < frameHeight * 8)
          this.NPC.frame.Y = frameHeight * 8;
        if (++this.NPC.frameCounter > 5.0)
        {
          this.NPC.frameCounter = 0.0;
          this.NPC.frame.Y += frameHeight;
        }
        if (this.NPC.frame.Y < Main.npcFrameCount[this.NPC.type] * frameHeight)
          return;
        this.NPC.frame.Y = frameHeight * 8;
      }
      else
      {
        if (++this.NPC.frameCounter > 5.0)
        {
          this.NPC.frameCounter = 0.0;
          this.NPC.frame.Y += frameHeight;
        }
        if (this.NPC.frame.Y < frameHeight * 7)
          return;
        this.NPC.frame.Y = 0;
      }
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(44, 300, true, false);
      target.AddBuff(24, 300, true, false);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      for (int index = 1; index <= 6; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          interpolatedStringHandler.AppendLiteral("NatureGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
      for (int index1 = 0; index1 < Main.maxNPCs; ++index1)
      {
        if (((Entity) Main.npc[index1]).active && Main.npc[index1].type == ModContent.NPCType<NatureChampionHead>() && (double) Main.npc[index1].ai[1] == (double) ((Entity) this.NPC).whoAmI)
        {
          Vector2 center = ((Entity) Main.npc[index1]).Center;
          Vector2 vector2_3 = Vector2.op_Addition(((Entity) this.NPC).Center, new Vector2((float) (54 * this.NPC.spriteDirection), -10f));
          float num = 0.05f;
          bool flag = false;
          for (float t = 0.0f; (double) t <= 1.0; t += num)
          {
            if ((double) t != 0.0)
            {
              Vector2 vector2_4;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_4).\u002Ector(NatureChampion.X(t, vector2_3.X, (float) (((double) vector2_3.X + (double) center.X) / 2.0), center.X) - NatureChampion.X(t - num, vector2_3.X, (float) (((double) vector2_3.X + (double) center.X) / 2.0), center.X), NatureChampion.Y(t, vector2_3.Y, vector2_3.Y + 50f, center.Y) - NatureChampion.Y(t - num, vector2_3.Y, vector2_3.Y + 50f, center.Y));
              if ((double) ((Vector2) ref vector2_4).Length() > 36.0 && (double) num > 0.0099999997764825821)
              {
                num -= 0.01f;
                t -= num;
              }
              else
              {
                double rotation = (double) Utils.ToRotation(vector2_4);
                Vector2 vector2_5;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_5).\u002Ector(NatureChampion.X(t, vector2_3.X, (float) (((double) vector2_3.X + (double) center.X) / 2.0), center.X), NatureChampion.Y(t, vector2_3.Y, vector2_3.Y + 50f, center.Y));
                flag = !flag;
                if (flag && !Main.dedServ)
                  Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_5, ((Entity) Main.npc[index1]).velocity, ModContent.Find<ModGore>(((ModType) this).Mod.Name, "NatureGore7").Type, Main.npc[index1].scale);
              }
            }
          }
          for (int index2 = 8; index2 <= 10; ++index2)
          {
            Vector2 vector2_6 = Vector2.op_Addition(((Entity) Main.npc[index1]).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) Main.npc[index1]).width), Utils.NextFloat(Main.rand, (float) ((Entity) Main.npc[index1]).height)));
            if (!Main.dedServ)
            {
              IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
              Vector2 vector2_7 = vector2_6;
              Vector2 velocity = ((Entity) Main.npc[index1]).velocity;
              string name = ((ModType) this).Mod.Name;
              interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
              interpolatedStringHandler.AppendLiteral("NatureGore");
              interpolatedStringHandler.AppendFormatted<int>(index2);
              string stringAndClear = interpolatedStringHandler.ToStringAndClear();
              int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
              double scale = (double) Main.npc[index1].scale;
              Gore.NewGore(sourceFromThis, vector2_7, velocity, type, (float) scale);
            }
          }
        }
      }
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[3], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<NatureForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<NatureChampionRelic>()));
    }

    private static float X(float t, float x0, float x1, float x2)
    {
      return (float) ((double) x0 * Math.Pow(1.0 - (double) t, 2.0) + (double) x1 * 2.0 * (double) t * Math.Pow(1.0 - (double) t, 1.0) + (double) x2 * Math.Pow((double) t, 2.0));
    }

    private static float Y(float t, float y0, float y1, float y2)
    {
      return (float) ((double) y0 * Math.Pow(1.0 - (double) t, 2.0) + (double) y1 * 2.0 * (double) t * Math.Pow(1.0 - (double) t, 1.0) + (double) y2 * Math.Pow((double) t, 2.0));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      float num1 = (float) (((double) Main.mouseTextColor / 200.0 - 0.34999999403953552) * 0.40000000596046448 + 0.800000011920929);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(this.NPC.GetAlpha(drawColor), 0.5f), this.NPC.rotation, vector2_1, this.NPC.scale * num1, spriteEffects, 0.0f);
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      for (int index = 0; index < Main.maxNPCs; ++index)
      {
        if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<NatureChampionHead>() && (double) Main.npc[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
        {
          if ((double) ((Entity) this.NPC).Distance(((Entity) Main.LocalPlayer).Center) <= 1200.0)
          {
            Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Nature/NatureChampion_Neck", (AssetRequestMode) 1).Value;
            Vector2 center = ((Entity) Main.npc[index]).Center;
            Vector2 vector2_2 = Vector2.op_Addition(((Entity) this.NPC).Center, new Vector2((float) (54 * this.NPC.spriteDirection), -10f));
            float num2 = 0.05f;
            for (float t = 0.0f; (double) t <= 1.0; t += num2)
            {
              if ((double) t != 0.0)
              {
                Vector2 vector2_3;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2_3).\u002Ector(NatureChampion.X(t, vector2_2.X, (float) (((double) vector2_2.X + (double) center.X) / 2.0), center.X) - NatureChampion.X(t - num2, vector2_2.X, (float) (((double) vector2_2.X + (double) center.X) / 2.0), center.X), NatureChampion.Y(t, vector2_2.Y, vector2_2.Y + 50f, center.Y) - NatureChampion.Y(t - num2, vector2_2.Y, vector2_2.Y + 50f, center.Y));
                if ((double) ((Vector2) ref vector2_3).Length() > 36.0 && (double) num2 > 0.0099999997764825821)
                {
                  num2 -= 0.01f;
                  t -= num2;
                }
                else
                {
                  float num3 = Utils.ToRotation(vector2_3) - 1.57079637f;
                  Vector2 vector2_4;
                  // ISSUE: explicit constructor call
                  ((Vector2) ref vector2_4).\u002Ector(NatureChampion.X(t, vector2_2.X, (float) (((double) vector2_2.X + (double) center.X) / 2.0), center.X), NatureChampion.Y(t, vector2_2.Y, vector2_2.Y + 50f, center.Y));
                  spriteBatch.Draw(texture2D2, new Vector2(NatureChampion.X(t, vector2_2.X, (float) (((double) vector2_2.X + (double) center.X) / 2.0), center.X) - screenPos.X, NatureChampion.Y(t, vector2_2.Y, vector2_2.Y + 50f, center.Y) - screenPos.Y), new Rectangle?(new Rectangle(0, 0, texture2D2.Width, texture2D2.Height)), this.NPC.GetAlpha(Lighting.GetColor((int) vector2_4.X / 16, (int) vector2_4.Y / 16)), num3, new Vector2((float) texture2D2.Width * 0.5f, (float) texture2D2.Height * 0.5f), 1f, (double) center.X < (double) vector2_2.X ? (SpriteEffects) 1 : (SpriteEffects) 0, 0.0f);
                }
              }
            }
          }
          this.DrawHead(spriteBatch, screenPos, Lighting.GetColor((int) ((Entity) Main.npc[index]).Center.X / 16, (int) ((Entity) Main.npc[index]).Center.Y / 16), Main.npc[index]);
        }
      }
      return false;
    }

    private void DrawHead(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor, NPC head)
    {
      if (!TextureAssets.Npc[this.NPC.type].IsLoaded)
        return;
      Texture2D texture2D1 = TextureAssets.Npc[head.type].Value;
      Rectangle frame = head.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color color1 = drawColor;
      Color alpha = head.GetAlpha(color1);
      SpriteEffects spriteEffects1 = head.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[head.type]; ++index)
      {
        Color color2 = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[head.type] - index) / (float) NPCID.Sets.TrailCacheLength[head.type]);
        Vector2 oldPo = head.oldPos[index];
        float rotation = head.rotation;
        Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) head).Size, 2f)), screenPos), new Vector2(0.0f, head.gfxOffY)), new Rectangle?(frame), color2, rotation, vector2_1, head.scale, spriteEffects1, 0.0f);
      }
      int num = (int) head.ai[3];
      if (num > 0)
        --num;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Nature/NatureChampionHead_Glow" + (num + 3).ToString(), (AssetRequestMode) 1).Value;
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) head).Center, screenPos), new Vector2(0.0f, head.gfxOffY)), new Rectangle?(frame), head.GetAlpha(drawColor), head.rotation, vector2_1, head.scale, spriteEffects1, 0.0f);
      Vector2 vector2_2 = Vector2.op_Addition(Vector2.op_Subtraction(((Entity) head).Center, screenPos), new Vector2(0.0f, head.gfxOffY));
      Rectangle? nullable = new Rectangle?(frame);
      Color white = Color.White;
      double rotation1 = (double) head.rotation;
      Vector2 vector2_3 = vector2_1;
      double scale = (double) head.scale;
      SpriteEffects spriteEffects2 = spriteEffects1;
      Main.EntitySpriteDraw(texture2D2, vector2_2, nullable, white, (float) rotation1, vector2_3, (float) scale, spriteEffects2, 0.0f);
    }
  }
}
