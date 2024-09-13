// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Spirit.SpiritChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.Souls;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Spirit
{
  [AutoloadBossHead]
  public class SpiritChampion : ModNPC
  {
    private bool doPredictiveSandnado;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 2;
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
      bestiaryDrawModifiers1.CustomTexturePath = "FargowiltasSouls/Content/Bosses/Champions/Spirit/" + ((ModType) this).Name + "_Still";
      bestiaryDrawModifiers1.Position = new Vector2(4f, 0.0f);
      bestiaryDrawModifiers1.PortraitScale = new float?(0.5f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundDesert,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.IsABestiaryIconDummy ? new Color?(this.NPC.GetBestiaryEntryColor()) : base.GetAlpha(drawColor);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 120;
      ((Entity) this.NPC).height = 150;
      this.NPC.damage = 125;
      this.NPC.defense = 40;
      this.NPC.lifeMax = 550000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit54);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath52);
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
      this.NPC.alpha = (int) byte.MaxValue;
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
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        this.NPC.TargetClosest(false);
        if ((double) this.NPC.ai[2] == 1.0)
        {
          ((Entity) this.NPC).velocity = Vector2.Zero;
          this.NPC.noTileCollide = true;
          this.NPC.noGravity = true;
          this.NPC.alpha = 0;
          if (WorldSavingSystem.DownedBoss[6] && (double) this.NPC.ai[1] < 120.0)
            this.NPC.ai[1] = 120f;
          if ((double) this.NPC.ai[1] == 180.0)
          {
            SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if (FargoSoulsUtil.HostCheck)
            {
              int index1 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, -1f, -1f, this.NPC.target);
              if (index1 != Main.maxNPCs)
              {
                ((Entity) Main.npc[index1]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                ((Entity) Main.npc[index1]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                if (Main.netMode == 2)
                  NetMessage.SendData(23, -1, -1, (NetworkText) null, index1, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
              int index2 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, -1f, 1f, this.NPC.target);
              if (index2 != Main.maxNPCs)
              {
                ((Entity) Main.npc[index2]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                ((Entity) Main.npc[index2]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                if (Main.netMode == 2)
                  NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
              int index3 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 1f, -1f, this.NPC.target);
              if (index3 != Main.maxNPCs)
              {
                ((Entity) Main.npc[index3]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                ((Entity) Main.npc[index3]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                if (Main.netMode == 2)
                  NetMessage.SendData(23, -1, -1, (NetworkText) null, index3, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
              int index4 = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 1f, 1f, this.NPC.target);
              if (index4 != Main.maxNPCs)
              {
                ((Entity) Main.npc[index4]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                ((Entity) Main.npc[index4]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                if (Main.netMode == 2)
                  NetMessage.SendData(23, -1, -1, (NetworkText) null, index4, 0.0f, 0.0f, 0.0f, 0, 0, 0);
              }
            }
          }
          if ((double) ++this.NPC.ai[1] <= 300.0)
            return;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 0.0f;
          this.NPC.ai[3] = 0.0f;
          this.NPC.localAI[3] = 1f;
          this.NPC.netUpdate = true;
        }
        else
        {
          if ((double) this.NPC.ai[3] == 0.0 && this.NPC.HasValidTarget)
          {
            this.NPC.ai[3] = 1f;
            ((Entity) this.NPC).Center = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, Vector2.op_Multiply(Vector2.UnitY, 500f));
            this.NPC.ai[2] = ((Entity) Main.player[this.NPC.target]).Center.Y - (float) (((Entity) this.NPC).height / 2);
            this.NPC.netUpdate = true;
          }
          this.NPC.alpha -= 10;
          if (this.NPC.alpha < 0)
          {
            this.NPC.alpha = 0;
            this.NPC.noGravity = false;
          }
          if ((double) ((Entity) this.NPC).Center.Y > (double) this.NPC.ai[2])
            this.NPC.noTileCollide = false;
          if ((double) ++this.NPC.ai[1] <= 300.0 && ((double) ((Entity) this.NPC).velocity.Y != 0.0 || (double) this.NPC.ai[1] <= 30.0))
            return;
          this.NPC.ai[1] = 0.0f;
          this.NPC.ai[2] = 1f;
          SoundEngine.PlaySound(ref SoundID.Item14, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          for (int index5 = -2; index5 <= 2; ++index5)
          {
            Vector2 center = ((Entity) this.NPC).Center;
            int num1 = ((Entity) this.NPC).width / 5;
            center.X += (float) (num1 * index5);
            center.Y += (float) (((Entity) this.NPC).height / 2);
            for (int index6 = 0; index6 < 20; ++index6)
              Dust.NewDust(Vector2.op_Subtraction(center, new Vector2(16f, 16f)), 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 2f);
            float num2 = 0.5f;
            for (int index7 = 0; index7 < 4; ++index7)
            {
              int index8 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), center, new Vector2(), Main.rand.Next(61, 64), 1f);
              Gore gore = Main.gore[index8];
              gore.velocity = Vector2.op_Multiply(gore.velocity, num2);
            }
          }
        }
      }
      else
      {
        Player player = Main.player[this.NPC.target];
        Tile tileSafely;
        if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0)
        {
          tileSafely = Framing.GetTileSafely(((Entity) player).Center);
          if (((Tile) ref tileSafely).WallType != (ushort) 0 || player.ZoneUndergroundDesert)
            this.NPC.timeLeft = 600;
        }
        this.NPC.dontTakeDamage = false;
        switch (this.NPC.ai[0])
        {
          case -4f:
            this.NPC.dontTakeDamage = true;
            goto case 0.0f;
          case -3f:
            this.NPC.dontTakeDamage = true;
            if ((double) this.NPC.localAI[2] == 0.0)
              this.NPC.localAI[2] = 1f;
            if (((Entity) player).active && !player.dead && (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) <= 2500.0)
            {
              tileSafely = Framing.GetTileSafely(((Entity) player).Center);
              if (((Tile) ref tileSafely).WallType != (ushort) 0 || player.ZoneUndergroundDesert)
              {
                Vector2 targetPos;
                // ISSUE: explicit constructor call
                ((Vector2) ref targetPos).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
                if ((double) ((Entity) this.NPC).Distance(targetPos) > 25.0)
                  this.Movement(targetPos, 0.8f, 24f);
                if ((double) this.NPC.ai[1] == 0.0)
                {
                  bool[] flagArray = new bool[4];
                  for (int index = 0; index < Main.maxNPCs; ++index)
                  {
                    if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<SpiritChampionHand>() && (double) Main.npc[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
                    {
                      if (!flagArray[0])
                        flagArray[0] = (double) Main.npc[index].ai[2] == -1.0 && (double) Main.npc[index].ai[3] == -1.0;
                      if (!flagArray[1])
                        flagArray[1] = (double) Main.npc[index].ai[2] == -1.0 && (double) Main.npc[index].ai[3] == 1.0;
                      if (!flagArray[2])
                        flagArray[2] = (double) Main.npc[index].ai[2] == 1.0 && (double) Main.npc[index].ai[3] == -1.0;
                      if (!flagArray[3])
                        flagArray[3] = (double) Main.npc[index].ai[2] == 1.0 && (double) Main.npc[index].ai[3] == 1.0;
                    }
                  }
                  if (FargoSoulsUtil.HostCheck)
                  {
                    if (!flagArray[0])
                    {
                      int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, -1f, -1f, this.NPC.target);
                      if (index != Main.maxNPCs)
                      {
                        ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                        ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                        if (Main.netMode == 2)
                          NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                    }
                    if (!flagArray[1])
                    {
                      int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, -1f, 1f, this.NPC.target);
                      if (index != Main.maxNPCs)
                      {
                        ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                        ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                        if (Main.netMode == 2)
                          NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                    }
                    if (!flagArray[2])
                    {
                      int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 1f, -1f, this.NPC.target);
                      if (index != Main.maxNPCs)
                      {
                        ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                        ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                        if (Main.netMode == 2)
                          NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                    }
                    if (!flagArray[3])
                    {
                      int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 0.0f, (float) ((Entity) this.NPC).whoAmI, 1f, 1f, this.NPC.target);
                      if (index != Main.maxNPCs)
                      {
                        ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                        ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                        if (Main.netMode == 2)
                          NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                      }
                    }
                  }
                }
                else if ((double) this.NPC.ai[1] == 120.0)
                {
                  SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                  for (int index = 0; index < Main.maxNPCs; ++index)
                  {
                    if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<SpiritChampionHand>() && (double) Main.npc[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
                    {
                      Main.npc[index].ai[0] = 1f;
                      Main.npc[index].netUpdate = true;
                    }
                  }
                  if (FargoSoulsUtil.HostCheck)
                  {
                    int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 3f, (float) ((Entity) this.NPC).whoAmI, 1f, 1f, this.NPC.target);
                    if (index != Main.maxNPCs)
                    {
                      ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
                      ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
                      if (Main.netMode == 2)
                        NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
                    }
                  }
                }
                if ((double) ++this.NPC.ai[2] > 85.0)
                {
                  this.NPC.ai[2] = 0.0f;
                  if (FargoSoulsUtil.HostCheck)
                  {
                    SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                    for (int index = 0; index < 12; ++index)
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).position.X + (float) Main.rand.Next(((Entity) this.NPC).width), ((Entity) this.NPC).position.Y + (float) Main.rand.Next(((Entity) this.NPC).height), Utils.NextFloat(Main.rand, -8f, 8f), Utils.NextFloat(Main.rand, -8f, 8f), ModContent.ProjectileType<SpiritCrossBone>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
                if ((double) ++this.NPC.ai[3] > 110.0)
                {
                  this.NPC.ai[3] = 0.0f;
                  if (FargoSoulsUtil.HostCheck)
                  {
                    Vector2 center = ((Entity) player).Center;
                    center.Y -= 100f;
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), center, Vector2.Zero, 658, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    int num = (int) ((Entity) this.NPC).Distance(center) / 10;
                    Vector2 vector2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, center), 10f);
                    for (int index9 = 0; index9 < num; ++index9)
                    {
                      int index10 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2, (float) index9)), 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f);
                      Main.dust[index10].noLight = true;
                      Main.dust[index10].scale = 1.25f;
                    }
                  }
                }
                if ((double) ++this.NPC.ai[1] > 600.0)
                {
                  this.NPC.dontTakeDamage = false;
                  this.NPC.netUpdate = true;
                  this.NPC.ai[0] = 0.0f;
                  this.NPC.ai[1] = 0.0f;
                  this.NPC.ai[2] = 0.0f;
                  this.NPC.ai[3] = 0.0f;
                  this.NPC.localAI[3] = 2f;
                  break;
                }
                break;
              }
            }
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            ++((Entity) this.NPC).velocity.Y;
            return;
          case -1f:
            Vector2 targetPos1;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos1).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
            if ((double) ((Entity) this.NPC).Distance(targetPos1) > 25.0)
              this.Movement(targetPos1, 0.8f, 24f);
            if ((double) ++this.NPC.ai[1] > 360.0)
            {
              this.NPC.TargetClosest(true);
              this.NPC.ai[0] = 4f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              Rectangle hitbox = ((Entity) this.NPC).Hitbox;
              if (((Rectangle) ref hitbox).Intersects(((Entity) player).Hitbox))
              {
                ((Entity) player).velocity.X = (double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).Center.X ? -15f : 15f;
                ((Entity) player).velocity.Y = -10f;
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            }
            break;
          case 0.0f:
          case 2f:
          case 4f:
          case 6f:
            if (((Entity) player).active && !player.dead && (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) <= 2500.0)
            {
              tileSafely = Framing.GetTileSafely(((Entity) player).Center);
              if (((Tile) ref tileSafely).WallType != (ushort) 0 || player.ZoneUndergroundDesert)
              {
                if ((double) this.NPC.ai[1] == 0.0)
                {
                  Vector2 center = ((Entity) player).Center;
                  ((Entity) this.NPC).velocity = Vector2.op_Division(Vector2.op_Subtraction(center, ((Entity) this.NPC).Center), 75f);
                  this.NPC.localAI[0] = center.X;
                  this.NPC.localAI[1] = center.Y;
                }
                if ((double) ++this.NPC.ai[1] > 75.0)
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
              }
            }
            this.NPC.TargetClosest(false);
            if (this.NPC.timeLeft > 30)
              this.NPC.timeLeft = 30;
            this.NPC.noTileCollide = true;
            this.NPC.noGravity = true;
            ++((Entity) this.NPC).velocity.Y;
            return;
          case 1f:
            if ((double) this.NPC.localAI[2] == 0.0)
              this.NPC.localAI[2] = 1f;
            Vector2 targetPos2;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos2).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
            if ((double) ((Entity) this.NPC).Distance(targetPos2) > 25.0)
              this.Movement(targetPos2, 0.8f, 24f);
            if ((double) ++this.NPC.ai[2] > 45.0)
            {
              this.NPC.ai[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                if ((double) this.NPC.ai[1] < 180.0)
                {
                  SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                  for (int index = 0; index < 12; ++index)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).position.X + (float) Main.rand.Next(((Entity) this.NPC).width), ((Entity) this.NPC).position.Y + (float) Main.rand.Next(((Entity) this.NPC).height), Utils.NextFloat(Main.rand, -8f, 8f), Utils.NextFloat(Main.rand, -8f, 8f), ModContent.ProjectileType<SpiritCrossBone>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                else
                {
                  this.doPredictiveSandnado = !this.doPredictiveSandnado;
                  Vector2 vector2_1 = ((Entity) player).Center;
                  if (this.doPredictiveSandnado && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.66)
                    vector2_1 = Vector2.op_Addition(vector2_1, Vector2.op_Multiply(((Entity) player).velocity, 30f));
                  vector2_1.Y -= 100f;
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_1, Vector2.Zero, 658, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  int num = (int) ((Entity) this.NPC).Distance(vector2_1) / 10;
                  Vector2 vector2_2 = Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, vector2_1), 10f);
                  for (int index11 = 0; index11 < num; ++index11)
                  {
                    int index12 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2_2, (float) index11)), 0, 0, 269, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index12].noLight = true;
                    Main.dust[index12].scale = 1.25f;
                  }
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 400.0)
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
            Vector2 targetPos3;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos3).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
            if ((double) ((Entity) this.NPC).Distance(targetPos3) > 25.0)
              this.Movement(targetPos3, 0.8f, 24f);
            if ((double) ++this.NPC.ai[2] == 30.0)
            {
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active && Main.npc[index].type == ModContent.NPCType<SpiritChampionHand>() && (double) Main.npc[index].ai[1] == (double) ((Entity) this.NPC).whoAmI)
                {
                  Main.npc[index].ai[0] = 1f;
                  Main.npc[index].netUpdate = true;
                }
              }
            }
            if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.66 && (double) ++this.NPC.ai[3] > 55.0)
            {
              this.NPC.ai[3] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 5; ++index)
                {
                  Vector2 vector2 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 1f, 2f), Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI));
                  float num = (float) (60 + Main.rand.Next(30));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<SpiritSpirit>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num, 0.0f);
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
          case 5f:
            Vector2 targetPos4;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos4).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
            if ((double) ((Entity) this.NPC).Distance(targetPos4) > 25.0)
              this.Movement(targetPos4, 0.8f, 24f);
            if ((double) ++this.NPC.ai[2] > 80.0)
            {
              this.NPC.ai[2] = 0.0f;
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 15; ++index)
                {
                  double num3 = (double) Utils.NextFloat(Main.rand, 4f, 8f);
                  Vector2 vector2 = Vector2.op_Multiply((float) num3, Utils.RotatedBy(Vector2.UnitX, Main.rand.NextDouble() * 2.0 * Math.PI, new Vector2()));
                  float num4 = (float) num3 / Utils.NextFloat(Main.rand, 60f, 120f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<SpiritSword>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, num4, 0.0f);
                }
                if ((double) this.NPC.life < (double) this.NPC.lifeMax * 0.66)
                {
                  for (int index = 0; index < 12; ++index)
                  {
                    Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (double) index, new Vector2());
                    float num = 1.04f;
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<SpiritHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num, 0.0f, 0.0f);
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
          case 7f:
            ++this.NPC.ai[0];
            break;
          case 8f:
            Vector2 targetPos5;
            // ISSUE: explicit constructor call
            ((Vector2) ref targetPos5).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
            if ((double) ((Entity) this.NPC).Distance(targetPos5) > 25.0)
              this.Movement(targetPos5, 0.8f, 24f);
            for (int index = 0; index < 20; ++index)
            {
              Vector2 vector2 = new Vector2();
              double num = Main.rand.NextDouble() * 2.0 * Math.PI;
              vector2.X += (float) (Math.Sin(num) * 150.0);
              vector2.Y += (float) (Math.Cos(num) * 150.0);
              Dust dust = Main.dust[Dust.NewDust(Vector2.op_Subtraction(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), new Vector2(4f, 4f)), 0, 0, 87, 0.0f, 0.0f, 100, Color.White, 1f)];
              dust.velocity = ((Entity) this.NPC).velocity;
              dust.noGravity = true;
            }
            if ((double) this.NPC.ai[1] > 60.0)
              ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x => ((Entity) x).active && x.friendly && !FargoSoulsUtil.IsSummonDamage(x, false))).ToList<Projectile>().ForEach((Action<Projectile>) (x =>
              {
                if ((double) Vector2.Distance(((Entity) x).Center, ((Entity) this.NPC).Center) > 150.0)
                  return;
                for (int index13 = 0; index13 < 5; ++index13)
                {
                  int index14 = Dust.NewDust(((Entity) x).position, ((Entity) x).width, ((Entity) x).height, 87, ((Entity) x).velocity.X * 0.2f, ((Entity) x).velocity.Y * 0.2f, 100, new Color(), 1.5f);
                  Main.dust[index14].noGravity = true;
                }
                x.hostile = true;
                x.friendly = false;
                x.owner = Main.myPlayer;
                x.damage /= 4;
                Projectile projectile = x;
                ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, -1f);
                if ((double) ((Entity) x).Center.X > (double) ((Entity) this.NPC).Center.X * 0.5)
                {
                  ((Entity) x).direction = 1;
                  x.spriteDirection = 1;
                }
                else
                {
                  ((Entity) x).direction = -1;
                  x.spriteDirection = -1;
                }
                if (x.owner != Main.myPlayer)
                  return;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) x).Center, Vector2.Zero, ModContent.ProjectileType<IronParry>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }));
            if ((double) this.NPC.ai[1] == 0.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), 0, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -6f, 0.0f);
            }
            if ((double) ++this.NPC.ai[3] > 10.0)
            {
              this.NPC.ai[3] = 0.0f;
              SoundEngine.PlaySound(ref SoundID.Item8, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                Point tileCoordinates1 = Utils.ToTileCoordinates(((Entity) this.NPC).Center);
                Point tileCoordinates2 = Utils.ToTileCoordinates(((Entity) Main.player[this.NPC.target]).Center);
                Vector2 vector2 = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, ((Entity) this.NPC).Center);
                int num5 = 6;
                int num6 = 6;
                int num7 = 0;
                int num8 = 2;
                int num9 = 0;
                bool flag1 = false;
                if ((double) ((Vector2) ref vector2).Length() > 2000.0)
                  flag1 = true;
                while (!flag1 && num9 < 50)
                {
                  ++num9;
                  int num10 = Main.rand.Next(tileCoordinates2.X - num5, tileCoordinates2.X + num5 + 1);
                  int num11 = Main.rand.Next(tileCoordinates2.Y - num5, tileCoordinates2.Y + num5 + 1);
                  if ((num11 < tileCoordinates2.Y - num7 || num11 > tileCoordinates2.Y + num7 || num10 < tileCoordinates2.X - num7 || num10 > tileCoordinates2.X + num7) && (num11 < tileCoordinates1.Y - num6 || num11 > tileCoordinates1.Y + num6 || num10 < tileCoordinates1.X - num6 || num10 > tileCoordinates1.X + num6))
                  {
                    tileSafely = ((Tilemap) ref Main.tile)[num10, num11];
                    if (!((Tile) ref tileSafely).HasUnactuatedTile)
                    {
                      bool flag2 = true;
                      if (flag2)
                      {
                        tileSafely = ((Tilemap) ref Main.tile)[num10, num11];
                        if (((Tile) ref tileSafely).LiquidType == 1)
                        {
                          tileSafely = ((Tilemap) ref Main.tile)[num10, num11];
                          if (((Tile) ref tileSafely).LiquidAmount > (byte) 0)
                            flag2 = false;
                        }
                      }
                      if (flag2 && Collision.SolidTiles(num10 - num8, num10 + num8, num11 - num8, num11 + num8))
                        flag2 = false;
                      if (flag2)
                      {
                        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), (float) (num10 * 16 + 8), (float) (num11 * 16 + 8), 0.0f, 0.0f, 596, 0, 1f, Main.myPlayer, (float) this.NPC.target, 0.0f, 0.0f);
                        break;
                      }
                    }
                  }
                }
              }
            }
            if ((double) ++this.NPC.ai[2] > 70.0)
            {
              this.NPC.ai[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 4; ++index)
                {
                  Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (Main.rand.NextDouble() - 0.5), new Vector2());
                  float num12 = Utils.NextFloat(Main.rand, 1.04f, 1.06f);
                  float num13 = Utils.NextFloat(Main.rand, 0.025f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<SpiritHand>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, num12, num13, 0.0f);
                }
              }
            }
            if ((double) this.NPC.ai[1] % 30.0 == 0.0 && FargoSoulsUtil.HostCheck && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.66)
            {
              SoundEngine.PlaySound(ref SoundID.Item2, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              for (int index = 0; index < 3; ++index)
              {
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).position.X + (float) Main.rand.Next(((Entity) this.NPC).width), ((Entity) this.NPC).position.Y + (float) Main.rand.Next(((Entity) this.NPC).height), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, -8f, 0.0f), ModContent.ProjectileType<SpiritCrossBone>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).position.X + (float) Main.rand.Next(((Entity) this.NPC).width), ((Entity) this.NPC).position.Y + (float) Main.rand.Next(((Entity) this.NPC).height), Utils.NextFloat(Main.rand, -1f, 1f), Utils.NextFloat(Main.rand, 8f, 0.0f), ModContent.ProjectileType<SpiritCrossBoneReverse>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
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
          case 9f:
            ++this.NPC.ai[0];
            break;
          default:
            this.NPC.ai[0] = 0.0f;
            goto case 0.0f;
        }
        if ((double) this.NPC.localAI[2] == 0.0 || !WorldSavingSystem.EternityMode)
          return;
        float num14 = ((Entity) this.NPC).Distance(((Entity) player).Center);
        if ((double) num14 > 1200.0 && (double) num14 < 3000.0 && (double) ++this.NPC.localAI[2] > 60.0)
        {
          this.NPC.localAI[2] = 1f;
          this.NPC.netUpdate = true;
          SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
          if (FargoSoulsUtil.HostCheck)
          {
            int index = NPC.NewNPC(((Entity) this.NPC).GetSource_FromAI((string) null), (int) ((Entity) this.NPC).Center.X, (int) ((Entity) this.NPC).Center.Y, ModContent.NPCType<SpiritChampionHand>(), ((Entity) this.NPC).whoAmI, 4f, (float) ((Entity) this.NPC).whoAmI, 1f, 1f, this.NPC.target);
            if (index != Main.maxNPCs)
            {
              ((Entity) Main.npc[index]).velocity.X = Utils.NextFloat(Main.rand, -24f, 24f);
              ((Entity) Main.npc[index]).velocity.Y = Utils.NextFloat(Main.rand, -24f, 24f);
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
        for (int index15 = 0; index15 < 20; ++index15)
        {
          int index16 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(1200f, Utils.RotatedBy(Vector2.UnitX, 2.0 * Math.PI * Main.rand.NextDouble(), new Vector2()))), 0, 0, 87, 0.0f, 0.0f, 0, new Color(), 1f);
          Main.dust[index16].velocity = ((Entity) this.NPC).velocity;
          Main.dust[index16].noGravity = true;
          ++Main.dust[index16].scale;
        }
      }
    }

    public virtual bool CheckDead()
    {
      if ((double) this.NPC.localAI[3] == 2.0 || !WorldSavingSystem.EternityMode)
        return true;
      ((Entity) this.NPC).active = true;
      this.NPC.life = 1;
      if (FargoSoulsUtil.HostCheck)
      {
        this.NPC.TargetClosest(false);
        this.NPC.ai[0] = -4f;
        this.NPC.ai[1] = 0.0f;
        this.NPC.ai[2] = 0.0f;
        this.NPC.ai[3] = 0.0f;
        this.NPC.dontTakeDamage = true;
        this.NPC.netUpdate = true;
      }
      return false;
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

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(ModContent.BuffType<InfestedBuff>(), 360, true, false);
      target.AddBuff(ModContent.BuffType<ClippedWingsBuff>(), 180, true, false);
    }

    public virtual void BossLoot(ref string name, ref int potionType) => potionType = 3544;

    public virtual void FindFrame(int frameHeight)
    {
      NPC npc = this.NPC;
      Rectangle frame = npc.frame;
      int num;
      switch ((int) this.NPC.ai[0])
      {
        case -4:
        case 0:
        case 2:
        case 4:
        case 6:
        case 8:
          num = frameHeight;
          break;
        default:
          num = 0;
          break;
      }
      npc.frame.Y = num;
      if ((double) this.NPC.localAI[3] != 0.0)
        return;
      if ((double) this.NPC.ai[2] == 1.0 && (double) this.NPC.ai[1] > 180.0)
        this.NPC.frame.Y = 0;
      else
        this.NPC.frame.Y = frameHeight;
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0 || (double) this.NPC.localAI[3] != 2.0)
        return;
      for (int index = 1; index <= 5; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          interpolatedStringHandler.AppendLiteral("SpiritGore");
          interpolatedStringHandler.AppendFormatted<int>(index);
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          int type = ModContent.Find<ModGore>(name, stringAndClear).Type;
          double scale = (double) this.NPC.scale;
          Gore.NewGore(sourceFromThis, vector2_2, velocity, type, (float) scale);
        }
      }
    }

    public virtual void OnKill()
    {
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[6], -1);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<SpiritForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<SpiritChampionRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeDropOnAllPlayers(ModContent.ItemType<AccursedRags>(), 4));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D = TextureAssets.Npc[this.NPC.type].Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2 = Vector2.op_Division(Utils.Size(frame), 2f);
      Color alpha = this.NPC.GetAlpha(drawColor);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
      {
        Color color = Color.op_Multiply(Color.op_Multiply(alpha, 0.5f), (float) (NPCID.Sets.TrailCacheLength[this.NPC.type] - index) / (float) NPCID.Sets.TrailCacheLength[this.NPC.type]);
        Vector2 oldPo = this.NPC.oldPos[index];
        float rotation = this.NPC.rotation;
        Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(Vector2.op_Addition(oldPo, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), color, rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      }
      Main.EntitySpriteDraw(texture2D, Vector2.op_Addition(Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2, this.NPC.scale, spriteEffects, 0.0f);
      return false;
    }
  }
}
