// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Bosses.Champions.Cosmos.CosmosChampion
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.NPCs;
using FargowiltasSouls.Content.Bosses.MutantBoss;
using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.BossBags;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Placables.Relics;
using FargowiltasSouls.Content.Items.Placables.Trophies;
using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Content.Projectiles.BossWeapons;
using FargowiltasSouls.Content.Projectiles.Masomode;
using FargowiltasSouls.Core.Globals;
using FargowiltasSouls.Core.ItemDropRules;
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
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Bosses.Champions.Cosmos
{
  [AutoloadBossHead]
  public class CosmosChampion : ModNPC
  {
    private bool hitChildren;
    private float epicMe;

    public virtual void SetStaticDefaults()
    {
      Main.npcFrameCount[this.NPC.type] = 9;
      NPCID.Sets.TrailCacheLength[this.NPC.type] = 6;
      NPCID.Sets.TrailingMode[this.NPC.type] = 1;
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
      span[num5] = 119;
      int num6 = num5 + 1;
      span[num6] = ModContent.BuffType<LethargicBuff>();
      int num7 = num6 + 1;
      span[num7] = ModContent.BuffType<ClippedWingsBuff>();
      int num8 = num7 + 1;
      span[num8] = ModContent.BuffType<TimeFrozenBuff>();
      int num9 = num8 + 1;
      span[num9] = ModContent.BuffType<LightningRodBuff>();
      int num10 = num9 + 1;
      npc.AddDebuffImmunities(debuffs);
      Dictionary<int, NPCID.Sets.NPCBestiaryDrawModifiers> bestiaryDrawOffset = NPCID.Sets.NPCBestiaryDrawOffset;
      int type = this.NPC.type;
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers1;
      // ISSUE: explicit constructor call
      ((NPCID.Sets.NPCBestiaryDrawModifiers) ref bestiaryDrawModifiers1).\u002Ector();
      bestiaryDrawModifiers1.Hide = false;
      bestiaryDrawModifiers1.Position = new Vector2(8f, 16f);
      bestiaryDrawModifiers1.PortraitScale = new float?(1f);
      bestiaryDrawModifiers1.PortraitPositionXOverride = new float?(0.0f);
      bestiaryDrawModifiers1.PortraitPositionYOverride = new float?(8f);
      NPCID.Sets.NPCBestiaryDrawModifiers bestiaryDrawModifiers2 = bestiaryDrawModifiers1;
      bestiaryDrawOffset.Add(type, bestiaryDrawModifiers2);
    }

    public virtual void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    {
      bestiaryEntry.Info.AddRange((IEnumerable<IBestiaryInfoElement>) new \u003C\u003Ez__ReadOnlyArray<IBestiaryInfoElement>(new IBestiaryInfoElement[2]
      {
        (IBestiaryInfoElement) BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Sky,
        (IBestiaryInfoElement) new FlavorTextBestiaryInfoElement("Mods.FargowiltasSouls.Bestiary." + ((ModType) this).Name)
      }));
    }

    public virtual Color? GetAlpha(Color drawColor)
    {
      return this.NPC.IsABestiaryIconDummy ? new Color?(this.NPC.GetBestiaryEntryColor()) : base.GetAlpha(drawColor);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.NPC).width = 80;
      ((Entity) this.NPC).height = 100;
      this.NPC.damage = 150;
      this.NPC.defense = 70;
      this.NPC.lifeMax = 600000;
      this.NPC.HitSound = new SoundStyle?(SoundID.NPCHit4);
      this.NPC.DeathSound = new SoundStyle?(SoundID.NPCDeath7);
      this.NPC.noGravity = true;
      this.NPC.noTileCollide = true;
      this.NPC.knockBackResist = 0.0f;
      this.NPC.lavaImmune = true;
      this.NPC.aiStyle = -1;
      this.NPC.value = (float) Item.buyPrice(3, 0, 0, 0);
      this.NPC.boss = true;
      Mod mod;
      this.Music = Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod) ? MusicLoader.GetMusicSlot(((ModType) this).Mod, "Assets/Sounds/Silent") : 84;
      this.SceneEffectPriority = (SceneEffectPriority) 6;
      this.NPC.scale *= 1.5f;
      this.NPC.dontTakeDamage = true;
      this.NPC.alpha = (int) byte.MaxValue;
      this.NPC.trapImmune = true;
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

    private ref float Animation => ref this.NPC.ai[0];

    public virtual void SendExtraAI(BinaryWriter writer)
    {
      writer.Write(this.NPC.localAI[0]);
      writer.Write(this.NPC.localAI[1]);
      writer.Write(this.NPC.localAI[2]);
      writer.Write(this.NPC.localAI[3]);
      writer.Write(this.hitChildren);
    }

    public virtual void ReceiveExtraAI(BinaryReader reader)
    {
      this.NPC.localAI[0] = reader.ReadSingle();
      this.NPC.localAI[1] = reader.ReadSingle();
      this.NPC.localAI[2] = reader.ReadSingle();
      this.NPC.localAI[3] = reader.ReadSingle();
      this.hitChildren = reader.ReadBoolean();
    }

    public virtual void AI()
    {
      EModeGlobalNPC.championBoss = ((Entity) this.NPC).whoAmI;
      if ((double) this.NPC.localAI[3] == 0.0)
      {
        if (!this.NPC.HasValidTarget)
          this.NPC.TargetClosest(false);
        if ((double) this.NPC.ai[1] == 0.0)
        {
          ((Entity) this.NPC).Center = Vector2.op_Subtraction(((Entity) Main.player[this.NPC.target]).Center, Vector2.op_Multiply(250f, Vector2.UnitY));
          if (FargoSoulsUtil.HostCheck)
            Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<CosmosVortex>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
        }
        if ((double) this.NPC.ai[1] == 117.0)
        {
          Mod mod;
          if (Terraria.ModLoader.ModLoader.TryGetMod("FargowiltasMusic", ref mod))
            this.Music = MusicLoader.GetMusicSlot(mod, "Assets/Music/PlatinumStar");
          else
            this.Music = 84;
        }
        if ((double) this.NPC.ai[1] > 117.0)
          Main.musicFade[Main.curMusic] += 0.2f;
        if ((double) ++this.NPC.ai[1] <= 120.0)
          return;
        this.NPC.netUpdate = true;
        this.NPC.ai[1] = 0.0f;
        this.NPC.localAI[3] = 1f;
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(((Entity) this.NPC).DirectionFrom(((Entity) Main.player[this.NPC.target]).Center), 1.5707963705062866), 20f);
      }
      else
      {
        this.NPC.alpha = 0;
        Player player = Main.player[this.NPC.target];
        if (this.NPC.HasValidTarget && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 2500.0 || (double) this.NPC.localAI[3] == 0.0)
          this.NPC.timeLeft = 600;
        ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1 : -1;
        if ((double) this.NPC.localAI[2] == 0.0 && (double) this.Animation != -1.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * (WorldSavingSystem.EternityMode ? 0.8 : 5.0))
        {
          if ((double) this.Animation == 15.0 && (double) this.NPC.ai[1] < 270.0)
          {
            this.NPC.life = (int) ((double) this.NPC.lifeMax * (WorldSavingSystem.EternityMode ? 0.8 : 5.0));
          }
          else
          {
            float num = this.Animation;
            this.Animation = -1f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = num;
            this.NPC.netUpdate = true;
            FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
          }
        }
        if (WorldSavingSystem.EternityMode && (double) this.NPC.localAI[2] < 2.0 && (double) this.Animation != -2.0 && (double) this.NPC.life < (double) this.NPC.lifeMax * 0.2)
        {
          if ((double) this.Animation == 15.0 && (double) this.NPC.ai[1] < 270.0)
          {
            this.NPC.life = (int) ((double) this.NPC.lifeMax * 0.2);
          }
          else
          {
            this.Animation = -2f;
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            this.NPC.netUpdate = true;
            FargoSoulsUtil.ClearHostileProjectiles(2, ((Entity) this.NPC).whoAmI);
          }
        }
        this.NPC.dontTakeDamage = false;
        Vector2 targetPos1;
        switch (this.Animation)
        {
          case -4f:
            this.NPC.timeLeft = 600;
            int n1 = (int) this.NPC.ai[2];
            if ((double) ++this.NPC.ai[3] < 420.0 && n1 > -1 && n1 < Main.maxNPCs && IsDeviantt(n1))
            {
              Vector2 center = ((Entity) Main.npc[n1]).Center;
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = (double) ((Entity) this.NPC).Center.X < (double) center.X ? 1 : -1;
              center.X += (float) (((Entity) this.NPC).width / 4 * ((double) ((Entity) this.NPC).Center.X < (double) center.X ? -1 : 1));
              if ((double) ((Entity) this.NPC).Distance(center) > (double) (((Entity) this.NPC).width / 4))
                this.Movement(center, 1.6f, 64f);
              if ((double) this.NPC.localAI[1] == 0.0)
              {
                this.NPC.localAI[1] = 1f;
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              }
              if ((double) ++this.NPC.localAI[0] <= 5.0)
              {
                this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.npc[n1]).Center));
                if (((Entity) this.NPC).direction < 0)
                  this.NPC.rotation += 3.14159274f;
                if ((double) this.NPC.localAI[0] == 5.0)
                {
                  this.NPC.netUpdate = true;
                  if (FargoSoulsUtil.HostCheck)
                  {
                    Vector2 unitX = Vector2.UnitX;
                    if (((Entity) this.NPC).direction < 0)
                      unitX.X *= -1f;
                    Vector2 vector2 = Utils.RotatedBy(unitX, (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.npc[n1]).Center)), new Vector2());
                    int num = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) Main.npc[n1]).Center.Y);
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) Main.npc[n1]).Center)), (float) num)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.npc[n1]).Center), (float) num), ModContent.ProjectileType<CosmosDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                    break;
                  }
                  break;
                }
                break;
              }
              if ((double) this.NPC.localAI[0] > 10.0)
              {
                this.NPC.localAI[0] = 0.0f;
                this.NPC.netUpdate = true;
                break;
              }
              break;
            }
            if ((double) this.NPC.ai[3] >= 420.0)
              this.hitChildren = true;
            this.Animation = this.NPC.ai[1];
            this.NPC.ai[1] = 0.0f;
            this.NPC.ai[2] = 0.0f;
            this.NPC.ai[3] = 0.0f;
            this.NPC.localAI[0] = 0.0f;
            this.NPC.localAI[1] = 0.0f;
            this.NPC.netUpdate = true;
            break;
          case -3f:
            if (!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0 || (double) ((Entity) player).Center.Y / 16.0 > Main.worldSurface)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              --((Entity) this.NPC).velocity.Y;
              break;
            }
            this.NPC.rotation = 0.0f;
            NPC npc1 = this.NPC;
            ((Entity) npc1).velocity = Vector2.op_Multiply(((Entity) npc1).velocity, 0.9f);
            player.wingTime = (float) player.wingTimeMax;
            if ((double) this.NPC.ai[1] == 0.0)
            {
              this.NPC.ai[1] = 1f;
              if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
                ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                if (WorldSavingSystem.EternityMode)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<CosmosRitual>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                int num1 = 2;
                float num2 = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) player).Center)) + 1.57079637f;
                if (WorldSavingSystem.MasochistModeReal)
                {
                  num1 = 3;
                  num2 = Utils.ToRotation(((Entity) this.NPC).DirectionFrom(((Entity) player).Center));
                }
                for (int index = 0; index < num1; ++index)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<CosmosMoon>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.14285719f), 0.0f, Main.myPlayer, 6.28318548f / (float) num1 * (float) index + num2, (float) ((Entity) this.NPC).whoAmI, 0.0f);
                this.epicMe = 1f;
              }
              Vector2 vector2_1;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_1).\u002Ector(500f, 500f);
              Vector2 center = ((Entity) this.NPC).Center;
              center.X -= vector2_1.X / 2f;
              center.Y -= vector2_1.Y / 2f;
              for (int index1 = 0; index1 < 30; ++index1)
              {
                int index2 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Dust dust = Main.dust[index2];
                dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
              }
              for (int index3 = 0; index3 < 50; ++index3)
              {
                int index4 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
                Main.dust[index4].noGravity = true;
                Dust dust1 = Main.dust[index4];
                dust1.velocity = Vector2.op_Multiply(dust1.velocity, 7f);
                int index5 = Dust.NewDust(center, (int) vector2_1.X, (int) vector2_1.Y, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Dust dust2 = Main.dust[index5];
                dust2.velocity = Vector2.op_Multiply(dust2.velocity, 3f);
              }
              for (int index6 = 0; index6 < 2; ++index6)
              {
                float num = 0.4f;
                if (index6 == 1)
                  num = 0.8f;
                int index7 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore1 = Main.gore[index7];
                gore1.velocity = Vector2.op_Multiply(gore1.velocity, num);
                ++Main.gore[index7].velocity.X;
                ++Main.gore[index7].velocity.Y;
                int index8 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore2 = Main.gore[index8];
                gore2.velocity = Vector2.op_Multiply(gore2.velocity, num);
                --Main.gore[index8].velocity.X;
                ++Main.gore[index8].velocity.Y;
                int index9 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore3 = Main.gore[index9];
                gore3.velocity = Vector2.op_Multiply(gore3.velocity, num);
                ++Main.gore[index9].velocity.X;
                --Main.gore[index9].velocity.Y;
                int index10 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, new Vector2(), Main.rand.Next(61, 64), 1f);
                Gore gore4 = Main.gore[index10];
                gore4.velocity = Vector2.op_Multiply(gore4.velocity, num);
                --Main.gore[index10].velocity.X;
                --Main.gore[index10].velocity.Y;
              }
              for (int index11 = 0; index11 < 20; ++index11)
              {
                Vector2 vector2_2 = center;
                vector2_2.X += (float) Main.rand.Next((int) vector2_1.X);
                vector2_2.Y += (float) Main.rand.Next((int) vector2_1.Y);
                for (int index12 = 0; index12 < 20; ++index12)
                {
                  int index13 = Dust.NewDust(vector2_2, 32, 32, 31, 0.0f, 0.0f, 100, new Color(), 3f);
                  Dust dust = Main.dust[index13];
                  dust.velocity = Vector2.op_Multiply(dust.velocity, 1.4f);
                }
                for (int index14 = 0; index14 < 10; ++index14)
                {
                  int index15 = Dust.NewDust(vector2_2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 3.5f);
                  Main.dust[index15].noGravity = true;
                  Dust dust3 = Main.dust[index15];
                  dust3.velocity = Vector2.op_Multiply(dust3.velocity, 7f);
                  int index16 = Dust.NewDust(vector2_2, 32, 32, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
                  Dust dust4 = Main.dust[index16];
                  dust4.velocity = Vector2.op_Multiply(dust4.velocity, 3f);
                }
                float num = 0.5f;
                for (int index17 = 0; index17 < 2; ++index17)
                {
                  int index18 = Gore.NewGore(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_2, new Vector2(), Main.rand.Next(61, 64), 1f);
                  Gore gore = Main.gore[index18];
                  gore.velocity = Vector2.op_Multiply(gore.velocity, num);
                  ++Main.gore[index18].velocity.X;
                  ++Main.gore[index18].velocity.Y;
                }
              }
            }
            if ((double) ++this.NPC.ai[2] > 200.0 || (double) this.NPC.ai[2] == 100.0)
            {
              if ((double) this.NPC.ai[2] > 200.0)
                this.NPC.ai[2] = 0.0f;
              this.NPC.netUpdate = true;
              SoundEngine.PlaySound(ref SoundID.Item92, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
                ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
              if ((double) this.NPC.ai[3] == 0.0)
              {
                ++this.NPC.ai[3];
                if (FargoSoulsUtil.HostCheck)
                {
                  float num3 = (float) this.NPC.life / ((float) this.NPC.lifeMax * 0.2f);
                  if ((double) num3 < 0.0)
                    num3 = 0.0f;
                  if ((double) num3 > 1.0)
                    num3 = 1f;
                  float num4 = (float) (11.0 + 4.0 * (double) num3);
                  for (int index = 0; index < 12; ++index)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(num4, Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), Math.PI / 6.0 * (double) index, new Vector2())), ModContent.ProjectileType<CosmosFireball2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 30f, 90f, 0.0f);
                  break;
                }
                break;
              }
              if ((double) this.NPC.ai[3] == 1.0)
              {
                ++this.NPC.ai[3];
                if (FargoSoulsUtil.HostCheck)
                {
                  if (!Main.dedServ)
                  {
                    SoundStyle soundStyle;
                    // ISSUE: explicit constructor call
                    ((SoundStyle) ref soundStyle).\u002Ector("FargowiltasSouls/Assets/Sounds/Thunder", (SoundType) 0);
                    ((SoundStyle) ref soundStyle).Volume = 0.8f;
                    ((SoundStyle) ref soundStyle).Pitch = 0.5f;
                    SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                  }
                  for (int index = 0; index < 16; ++index)
                  {
                    Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), 0.39269909262657166 * (double) index, new Vector2());
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Division(Vector2.op_Multiply(vector2, (float) ((Entity) this.NPC).width), 120f), ModContent.ProjectileType<LightningVortexHostile>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 1f, Utils.ToRotation(vector2), 0.0f);
                  }
                  break;
                }
                break;
              }
              if ((double) this.NPC.ai[3] == 2.0)
              {
                ++this.NPC.ai[3];
                if (FargoSoulsUtil.HostCheck)
                {
                  for (int index19 = -1; index19 <= 1; ++index19)
                  {
                    if (index19 != 0)
                    {
                      Vector2 vector2 = Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) Main.player[this.NPC.target]).Center), (double) MathHelper.ToRadians(30f) * (double) index19, new Vector2());
                      for (int index20 = 0; index20 < 15; ++index20)
                        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(6f, Utils.RotatedBy(vector2, (double) MathHelper.ToRadians(8f) * (double) index19 * (double) index20, new Vector2())), ModContent.ProjectileType<CosmosNebulaBlaze>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.009f, 0.0f, 0.0f);
                    }
                  }
                  break;
                }
                break;
              }
              this.NPC.ai[3] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = 0; index < 18; ++index)
                {
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, Math.PI / 9.0 * (double) index, new Vector2()), ModContent.ProjectileType<CosmosInvader>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 180f, 0.04f, 0.0f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Utils.RotatedBy(Vector2.UnitX, Math.PI / 9.0 * ((double) index + 0.5), new Vector2()), ModContent.ProjectileType<CosmosInvader>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 180f, 0.025f, 0.0f);
                }
                for (int index = 0; index < 5; ++index)
                {
                  Vector2 vector2 = Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 1.2566370964050293 * (double) index, new Vector2()));
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, vector2, ModContent.ProjectileType<CosmosGlowything>(), 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                SoundEngine.PlaySound(ref SoundID.Item25, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                break;
              }
              break;
            }
            break;
          case -2f:
            this.NPC.rotation = 0.0f;
            this.NPC.dontTakeDamage = true;
            this.NPC.localAI[2] = 2f;
            Vector2 center1 = ((Entity) player).Center;
            center1.X += (float) (600 * ((double) ((Entity) this.NPC).Center.X < (double) center1.X ? -1 : 1));
            this.Movement(center1, 0.8f, 32f);
            if ((double) --this.NPC.ai[2] < 0.0)
            {
              this.NPC.ai[2] = (float) Main.rand.Next(5);
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height)));
                int num = ModContent.ProjectileType<PhantasmalBlast>();
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) ++this.NPC.ai[1] > 150.0)
            {
              this.NPC.TargetClosest(true);
              --this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case -1f:
            this.NPC.rotation = 0.0f;
            this.NPC.dontTakeDamage = true;
            NPC npc2 = this.NPC;
            ((Entity) npc2).velocity = Vector2.op_Multiply(((Entity) npc2).velocity, 0.9f);
            if ((double) ++this.NPC.ai[1] == 120.0)
            {
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              this.NPC.localAI[2] = 1f;
              this.epicMe = 1f;
              break;
            }
            if ((double) this.NPC.ai[1] > 180.0)
            {
              this.NPC.TargetClosest(true);
              this.Animation = this.NPC.ai[3];
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 0.0f:
          case 4f:
          case 8f:
          case 12f:
            this.NPC.rotation = 0.0f;
            if ((!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0 || (double) ((Entity) player).Center.Y / 16.0 > Main.worldSurface) && (double) this.NPC.localAI[3] != 0.0)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              --((Entity) this.NPC).velocity.Y;
              break;
            }
            Vector2 targetPos2 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 500f));
            if ((double) ((Entity) this.NPC).Distance(targetPos2) > 50.0)
              this.Movement(targetPos2, 0.8f, 32f);
            if ((double) this.NPC.ai[1] == 0.0 && (double) this.NPC.localAI[2] != 0.0 && Main.expertMode && (double) this.Animation != 5.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -23f, 0.0f);
            if ((double) ++this.NPC.ai[1] > 60.0)
            {
              float num = this.Animation;
              this.NPC.TargetClosest(true);
              this.Animation += (double) this.NPC.localAI[2] == 0.0 || !Main.expertMode ? 2f : 1f;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              if (!this.hitChildren)
              {
                for (int n2 = 0; n2 < Main.maxNPCs; ++n2)
                {
                  if (((Entity) Main.npc[n2]).active && (double) ((Entity) this.NPC).Distance(((Entity) Main.npc[n2]).Center) < 2000.0 && (double) ((Entity) player).Distance(((Entity) Main.npc[n2]).Center) < 2000.0 && IsDeviantt(n2))
                  {
                    this.Animation = -4f;
                    this.NPC.ai[1] = num;
                    this.NPC.ai[2] = (float) n2;
                    break;
                  }
                }
                break;
              }
              break;
            }
            break;
          case 1f:
            Vector2 center2 = ((Entity) player).Center;
            center2.X += (float) (300 * ((double) ((Entity) this.NPC).Center.X < (double) center2.X ? -1 : 1));
            if ((double) ((Entity) this.NPC).Distance(center2) > 50.0)
              this.Movement(center2, 0.8f, 32f);
            if ((double) this.NPC.ai[1] == 1.0)
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if ((double) ++this.NPC.ai[2] <= 6.0)
            {
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (((Entity) this.NPC).direction < 0)
                this.NPC.rotation += 3.14159274f;
              this.NPC.ai[3] = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1f : -1f;
              if ((double) this.NPC.ai[2] == 6.0)
              {
                this.NPC.netUpdate = true;
                if ((double) this.NPC.ai[1] > 50.0)
                {
                  if (FargoSoulsUtil.HostCheck)
                  {
                    Vector2 unitX = Vector2.UnitX;
                    if (((Entity) this.NPC).direction < 0)
                      unitX.X *= -1f;
                    Vector2 vector2 = Utils.RotatedBy(unitX, (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)), new Vector2());
                    int num = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) player).Center.Y);
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)), (float) num)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) num), ModContent.ProjectileType<CosmosDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.25f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  }
                }
                else
                  this.NPC.ai[2] = 0.0f;
              }
            }
            else
            {
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.ai[3]);
              if ((double) this.NPC.ai[2] > 12.0)
              {
                this.NPC.ai[2] = 0.0f;
                this.NPC.ai[3] = 0.0f;
                this.NPC.netUpdate = true;
              }
            }
            if ((double) ++this.NPC.ai[1] > 240.0)
            {
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 2f:
          case 6f:
            if ((double) this.Animation != 10.0)
            {
              this.NPC.rotation = 0.0f;
              targetPos1 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 500f));
              if ((double) ((Entity) this.NPC).Distance(targetPos1) > 50.0)
                this.Movement(targetPos1, 0.8f, 32f);
            }
            if ((!((Entity) player).active || player.dead || (double) Vector2.Distance(((Entity) this.NPC).Center, ((Entity) player).Center) > 2500.0) && (double) this.NPC.localAI[3] != 0.0)
            {
              this.NPC.TargetClosest(false);
              if (this.NPC.timeLeft > 30)
                this.NPC.timeLeft = 30;
              --((Entity) this.NPC).velocity.Y;
              break;
            }
            if ((double) ++this.NPC.ai[1] > 60.0)
            {
              float num = this.Animation;
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              if (!this.hitChildren)
              {
                for (int n3 = 0; n3 < Main.maxNPCs; ++n3)
                {
                  if (((Entity) Main.npc[n3]).active && IsDeviantt(n3) && (double) ((Entity) this.NPC).Distance(((Entity) Main.npc[n3]).Center) < 1200.0 && (double) ((Entity) player).Distance(((Entity) Main.npc[n3]).Center) < 1200.0)
                  {
                    this.Animation = -4f;
                    this.NPC.ai[1] = num;
                    this.NPC.ai[2] = (float) n3;
                    break;
                  }
                }
                break;
              }
              break;
            }
            break;
          case 3f:
            if ((double) this.NPC.ai[1] == 1.0 && FargoSoulsUtil.HostCheck)
            {
              float num5 = 0.7853982f;
              int num6 = FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
              for (int index = 0; index < 8; ++index)
              {
                Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Utils.RotatedBy(new Vector2(120f, 0.0f), (double) num5 * (double) index, new Vector2()));
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, ModContent.ProjectileType<CosmosFireball>(), num6, 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, num5 * (float) index, 0.0f);
              }
            }
            int num7 = 70;
            if ((double) ++this.NPC.ai[2] <= (double) num7)
            {
              Vector2 center3 = ((Entity) player).Center;
              center3.X += (float) (600 * ((double) ((Entity) this.NPC).Center.X < (double) center3.X ? -1 : 1));
              center3.Y += (float) (300 * ((double) ((Entity) this.NPC).Center.Y < (double) center3.Y ? -1 : 1));
              this.Movement(center3, 1.6f, 24f);
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (((Entity) this.NPC).direction < 0)
                this.NPC.rotation += 3.14159274f;
              if ((double) this.NPC.ai[2] == (double) num7)
              {
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(42f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                this.NPC.netUpdate = true;
              }
            }
            else
            {
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
              if ((double) this.NPC.ai[2] > (double) (num7 + 30))
              {
                this.NPC.ai[2] = 0.0f;
                this.NPC.netUpdate = true;
                if (WorldSavingSystem.EternityMode && (double) this.NPC.localAI[2] != 0.0)
                {
                  if (!Main.dedServ && ((Entity) Main.LocalPlayer).active)
                    ScreenShakeSystem.StartShake(10f, 6.28318548f, new Vector2?(), 0.333333343f);
                  if (FargoSoulsUtil.HostCheck)
                  {
                    Vector2 vector2_3 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
                    for (int index = 0; index < 6; ++index)
                    {
                      Vector2 vector2_4 = Vector2.op_Multiply((float) (((Entity) this.NPC).height / 2), Utils.RotatedBy(vector2_3, Math.PI / 3.0 * (double) index, new Vector2()));
                      float num8 = index <= 1 || index == 5 ? 32f : 8f;
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Utils.NextVector2Circular(Main.rand, (float) (((Entity) this.NPC).width / 2), (float) (((Entity) this.NPC).height / 2))), Vector2.Zero, ModContent.ProjectileType<MoonLordSunBlast>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, MathHelper.WrapAngle(Utils.ToRotation(vector2_4)), num8, 0.0f);
                    }
                  }
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 330.0)
            {
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 5f:
            if ((double) this.NPC.ai[1] == 1.0)
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if ((double) ++this.NPC.ai[2] <= 75.0)
            {
              Vector2 center4 = ((Entity) player).Center;
              center4.X += (float) (350 * ((double) ((Entity) this.NPC).Center.X < (double) center4.X ? -1 : 1));
              center4.Y -= 700f;
              this.Movement(center4, 1.6f, 32f);
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (((Entity) this.NPC).direction < 0)
                this.NPC.rotation += 3.14159274f;
              this.NPC.localAI[0] = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1f : -1f;
              if ((double) this.NPC.ai[2] == 75.0)
              {
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(42f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                this.NPC.netUpdate = true;
                if (FargoSoulsUtil.HostCheck)
                {
                  int num9 = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) player).Center.Y);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)), (float) num9)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) num9), ModContent.ProjectileType<CosmosDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.25f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  for (int index = -3; index <= 3; ++index)
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(0.5f, Vector2.op_UnaryNegation(Utils.RotatedBy(Vector2.UnitY, 0.52359879016876221 * (double) index, new Vector2()))), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
                this.NPC.ai[3] = (float) Main.rand.Next(4);
              }
            }
            else
            {
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.localAI[0]);
              if ((double) ++this.NPC.ai[3] > 4.0)
              {
                this.NPC.ai[3] = 0.0f;
                if (FargoSoulsUtil.HostCheck)
                {
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(0.5f, Vector2.UnitX), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(-0.5f, Vector2.UnitX), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            if ((double) ++this.NPC.ai[1] > 240.0 || (double) this.NPC.ai[2] > 60.0 && (double) ((Entity) this.NPC).Center.Y > (double) ((Entity) player).Center.Y + 700.0)
            {
              ((Entity) this.NPC).velocity.Y = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                for (int index = -3; index <= 3; ++index)
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(0.5f, Utils.RotatedBy(Vector2.UnitY, 0.52359879016876221 * (double) index, new Vector2())), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 7f:
            Vector2 targetPos3 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 500f));
            if ((double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 200.0 || (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 600.0)
            {
              if ((double) ((Entity) this.NPC).Distance(targetPos3) > 50.0)
                this.Movement(targetPos3, 0.6f, 32f);
            }
            else
            {
              NPC npc3 = this.NPC;
              ((Entity) npc3).velocity = Vector2.op_Multiply(((Entity) npc3).velocity, 0.97f);
            }
            if ((double) this.NPC.ai[1] == 30.0)
            {
              SoundEngine.PlaySound(ref SoundID.ForceRoarPitched, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if (FargoSoulsUtil.HostCheck)
              {
                float num10 = !WorldSavingSystem.EternityMode || (double) this.NPC.localAI[2] == 0.0 ? ((double) this.NPC.localAI[2] == 0.0 ? 1f : -1.6f) : -1.2f;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<CosmosVortex>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, num10, 0.0f);
              }
              int num11 = (int) ((Entity) this.NPC).Distance(((Entity) player).Center);
              Vector2 vector2 = Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center);
              for (int index21 = 0; index21 < num11; index21 += 10)
              {
                int index22 = Dust.NewDust(Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(vector2, (float) index21)), 0, 0, 229, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index22].noGravity = true;
                Main.dust[index22].scale = 1.5f;
              }
            }
            if ((double) ++this.NPC.ai[1] > 450.0)
            {
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 9f:
            if ((double) this.NPC.ai[1] == 1.0)
              SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            if ((double) this.NPC.ai[2] == 0.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<GlowRing>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, -20f, 0.0f);
            if ((double) ++this.NPC.ai[2] <= 200.0)
            {
              Vector2 center5 = ((Entity) player).Center;
              center5.X += (float) (600 * ((double) ((Entity) this.NPC).Center.X < (double) center5.X ? -1 : 1));
              NPC npc4 = this.NPC;
              ((Entity) npc4).position = Vector2.op_Addition(((Entity) npc4).position, Vector2.op_Division(((Entity) player).velocity, 3f));
              this.Movement(center5, 1.2f, 32f);
              if ((double) --this.NPC.localAI[0] < 0.0)
              {
                this.NPC.localAI[0] = 90f;
                if (FargoSoulsUtil.HostCheck)
                {
                  for (int index23 = -1; index23 <= 1; index23 += 2)
                  {
                    for (int index24 = -11; index24 <= 11; ++index24)
                    {
                      Vector2 center6 = ((Entity) player).Center;
                      center6.X += 180f * (float) index24;
                      center6.Y += (float) (400.0 + 27.272727966308594 * (double) Math.Abs(index24)) * (float) index23;
                      Vector2 vector2 = Vector2.op_Division(Vector2.op_Subtraction(center6, ((Entity) this.NPC).Center), 20f);
                      int num12 = 60 + Math.Abs(index24 * 2);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Division(vector2, 2f), ModContent.ProjectileType<CosmosSphere>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 20f, (float) num12, 0.0f);
                    }
                  }
                }
              }
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (((Entity) this.NPC).direction < 0)
                this.NPC.rotation += 3.14159274f;
              this.NPC.ai[3] = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1f : -1f;
              if ((double) this.NPC.ai[2] == 200.0)
              {
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(42f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                this.NPC.netUpdate = true;
                if (FargoSoulsUtil.HostCheck)
                {
                  int num13 = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) player).Center.Y);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)), (float) num13)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) num13), ModContent.ProjectileType<CosmosDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.25f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                }
              }
            }
            else
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.ai[3]);
            if ((double) ++this.NPC.ai[1] > 400.0 || (double) this.NPC.ai[2] > 200.0 && ((double) this.NPC.ai[3] > 0.0 ? ((double) ((Entity) this.NPC).Center.X > (double) ((Entity) player).Center.X + 800.0 ? 1 : 0) : ((double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X - 800.0 ? 1 : 0)) != 0)
            {
              ((Entity) this.NPC).velocity.X = 0.0f;
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 10f:
            NebulaDust();
            if ((double) this.NPC.ai[1] == 0.0)
              SoundEngine.PlaySound(ref SoundID.Item117, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
            targetPos1 = ((Entity) player).Center;
            targetPos1.X += (float) (550 * ((double) ((Entity) this.NPC).Center.X < (double) targetPos1.X ? -1 : 1));
            if ((double) ((Entity) this.NPC).Distance(targetPos1) > 50.0)
              this.Movement(targetPos1, 0.8f, 24f);
            this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
            if (((Entity) this.NPC).direction < 0)
            {
              this.NPC.rotation += 3.14159274f;
              goto case 2f;
            }
            else
              goto case 2f;
          case 11f:
            Vector2 center7 = ((Entity) player).Center;
            if (WorldSavingSystem.EternityMode && (double) this.NPC.localAI[2] != 0.0)
            {
              int num14 = (double) ((Entity) this.NPC).Center.X < (double) center7.X ? -1 : 1;
              Vector2 vector2 = Vector2.op_Multiply((float) (600 * num14), Vector2.UnitX);
              float num15 = (float) ((double) MathHelper.ToRadians(80f) * (double) this.NPC.ai[1] / 420.0) * (float) -num14;
              Vector2 targetPos4 = Vector2.op_Addition(center7, Utils.RotatedBy(vector2, (double) num15, new Vector2()));
              NPC npc5 = this.NPC;
              ((Entity) npc5).position = Vector2.op_Addition(((Entity) npc5).position, Vector2.op_Division(((Entity) player).velocity, 3f));
              this.Movement(targetPos4, 0.8f, 24f);
            }
            else
            {
              center7.X += (float) (550 * ((double) ((Entity) this.NPC).Center.X < (double) center7.X ? -1 : 1));
              if ((double) ((Entity) this.NPC).Distance(center7) > 50.0)
                this.Movement(center7, 0.8f, 24f);
            }
            this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
            if (((Entity) this.NPC).direction < 0)
              this.NPC.rotation += 3.14159274f;
            if ((double) this.NPC.ai[1] == 30.0 && FargoSoulsUtil.HostCheck)
              Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) player).Center, Vector2.Zero, ModContent.ProjectileType<CosmosReticle>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, (float) ((Entity) this.NPC).whoAmI, 0.0f, 0.0f);
            if ((double) this.NPC.ai[1] > 60.0)
            {
              if ((double) ++this.NPC.ai[3] == 3.0)
              {
                SoundEngine.PlaySound(ref SoundID.Item20, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
                if (FargoSoulsUtil.HostCheck)
                {
                  Vector2 vector2_5;
                  // ISSUE: explicit constructor call
                  ((Vector2) ref vector2_5).\u002Ector(80f, 6f);
                  if ((double) ((Entity) player).Center.X < (double) ((Entity) this.NPC).Center.X)
                    vector2_5.X *= -1f;
                  Vector2 vector2_6 = Utils.RotatedBy(vector2_5, (double) this.NPC.rotation, new Vector2());
                  for (int index = 0; index < 2; ++index)
                  {
                    float num16 = MathHelper.ToRadians((double) this.NPC.localAI[2] == 0.0 ? 30f : 20f) + Utils.NextFloat(Main.rand, MathHelper.ToRadians(10f));
                    if (index == 0)
                      num16 *= -1f;
                    Vector2 vector2_7 = Vector2.op_Multiply(Utils.NextFloat(Main.rand, 8f, 12f), Utils.RotatedBy(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (double) num16, new Vector2()));
                    Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2_6), vector2_7, ModContent.ProjectileType<CosmosNebulaBlaze>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f / 500f, 0.0f, 0.0f);
                    if (WorldSavingSystem.EternityMode && (double) this.NPC.localAI[2] != 0.0)
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, vector2_6), Utils.RotatedBy(vector2_7, (double) num16 * (double) Utils.NextFloat(Main.rand, 1f, 4f), new Vector2()), ModContent.ProjectileType<CosmosNebulaBlaze>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 3f / 500f, 0.0f, 0.0f);
                  }
                }
              }
              else if ((double) this.NPC.ai[3] > 6.0)
                this.NPC.ai[3] = 0.0f;
            }
            else
              NebulaDust();
            if ((double) ++this.NPC.ai[1] > 390.0)
            {
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 13f:
            if ((double) ++this.NPC.ai[1] < 110.0)
            {
              Vector2 center8 = ((Entity) player).Center;
              center8.X += (float) (300 * ((double) ((Entity) this.NPC).Center.X < (double) center8.X ? -1 : 1));
              if ((double) ((Entity) this.NPC).Distance(center8) > 50.0)
                this.Movement(center8, 0.8f, 32f);
              if ((double) this.NPC.ai[1] == 1.0)
                SoundEngine.PlaySound(ref SoundID.Roar, new Vector2?(((Entity) this.NPC).Center), (SoundUpdateCallback) null);
              if ((double) ++this.NPC.ai[2] <= 6.0)
              {
                this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                if (((Entity) this.NPC).direction < 0)
                  this.NPC.rotation += 3.14159274f;
                this.NPC.ai[3] = (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? 1f : -1f;
                if ((double) this.NPC.ai[2] == 6.0)
                {
                  this.NPC.netUpdate = true;
                  if ((double) this.NPC.ai[1] > 50.0)
                  {
                    if (FargoSoulsUtil.HostCheck)
                    {
                      Vector2 unitX = Vector2.UnitX;
                      if (((Entity) this.NPC).direction < 0)
                        unitX.X *= -1f;
                      Vector2 vector2 = Utils.RotatedBy(unitX, (double) Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center)), new Vector2());
                      int num17 = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) player).Center.Y);
                      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(Vector2.op_Addition(((Entity) this.NPC).Center, vector2), Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)), (float) num17)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) num17), ModContent.ProjectileType<CosmosDeathray>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.25f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                      break;
                    }
                    break;
                  }
                  this.NPC.ai[2] = 0.0f;
                  break;
                }
                break;
              }
              ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(this.NPC.ai[3]);
              if ((double) this.NPC.ai[2] > 12.0)
              {
                this.NPC.ai[2] = 0.0f;
                this.NPC.ai[3] = 0.0f;
                this.NPC.netUpdate = true;
                break;
              }
              break;
            }
            if ((double) this.NPC.ai[1] <= 155.0)
            {
              Vector2 center9 = ((Entity) player).Center;
              center9.X += (float) (350 * ((double) ((Entity) this.NPC).Center.X < (double) center9.X ? -1 : 1));
              center9.Y += 700f;
              NPC npc6 = this.NPC;
              ((Entity) npc6).position = Vector2.op_Addition(((Entity) npc6).position, Vector2.op_Division(((Entity) player).velocity, 3f));
              this.Movement(center9, 2.4f, 32f);
              this.NPC.rotation = Utils.ToRotation(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
              if (((Entity) this.NPC).direction < 0)
                this.NPC.rotation += 3.14159274f;
              if ((double) this.NPC.ai[1] == 155.0)
              {
                ((Entity) this.NPC).velocity = Vector2.op_Multiply(42f, Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center));
                this.NPC.netUpdate = true;
                this.NPC.ai[3] = Math.Abs(((Entity) player).Center.Y - ((Entity) this.NPC).Center.Y) / 42f;
                this.NPC.ai[3] *= 2f;
                this.NPC.localAI[0] = ((Entity) player).Center.X;
                this.NPC.localAI[1] = ((Entity) player).Center.Y;
                this.NPC.localAI[0] += (double) ((Entity) this.NPC).Center.X < (double) ((Entity) player).Center.X ? -50f : 50f;
                if (FargoSoulsUtil.HostCheck)
                {
                  int num18 = Math.Sign(((Entity) this.NPC).Center.Y - ((Entity) player).Center.Y);
                  Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(Vector2.op_Multiply(3000f, ((Entity) this.NPC).DirectionFrom(((Entity) player).Center)), (float) num18)), Vector2.op_Multiply(Luminance.Common.Utilities.Utilities.SafeDirectionTo((Entity) this.NPC, ((Entity) player).Center), (float) num18), ModContent.ProjectileType<CosmosDeathray2>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage, 1.25f), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                  break;
                }
                break;
              }
              break;
            }
            ((Entity) this.NPC).direction = this.NPC.spriteDirection = Math.Sign(((Entity) this.NPC).velocity.X);
            this.NPC.rotation = Utils.ToRotation(((Entity) this.NPC).velocity);
            if (((Entity) this.NPC).direction < 0)
              this.NPC.rotation += 3.14159274f;
            if ((double) Math.Abs(((Entity) this.NPC).Center.Y - this.NPC.localAI[1]) < 300.0)
            {
              Vector2 vector2_8 = Vector2.op_Subtraction(((Entity) this.NPC).Center, Vector2.op_Division(((Entity) this.NPC).velocity, 2f));
              Vector2 vector2_9;
              // ISSUE: explicit constructor call
              ((Vector2) ref vector2_9).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
              Vector2 vector2_10 = vector2_9;
              Vector2 vector2_11 = Vector2.Normalize(Vector2.op_Subtraction(vector2_8, vector2_10));
              if (FargoSoulsUtil.HostCheck)
              {
                int num19 = Math.Sign(((Entity) player).Center.X - vector2_9.X) == ((Entity) this.NPC).direction ? 1 : -1;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply((float) num19 * 0.5f, vector2_11), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply((float) num19 * 0.5f, ((Entity) this.NPC).DirectionFrom(vector2_9)), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            else if ((double) ++this.NPC.ai[2] > 1.0)
            {
              this.NPC.ai[2] = 0.0f;
              if (FargoSoulsUtil.HostCheck)
              {
                Vector2 vector2;
                // ISSUE: explicit constructor call
                ((Vector2) ref vector2).\u002Ector(this.NPC.localAI[0], this.NPC.localAI[1]);
                Math.Sign(((Entity) player).Center.X - vector2.X);
                int direction = ((Entity) this.NPC).direction;
                Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.op_Multiply(0.5f, ((Entity) this.NPC).DirectionFrom(vector2)), ModContent.ProjectileType<CosmosBolt>(), FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage), 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
              }
            }
            if ((double) this.NPC.ai[1] > 155.0 + (double) this.NPC.ai[3])
            {
              ((Entity) this.NPC).velocity.Y = 0.0f;
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = !WorldSavingSystem.EternityMode || (double) this.NPC.localAI[2] == 0.0 ? -120f : 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.localAI[1] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          case 14f:
            NPC npc7 = this.NPC;
            ((Entity) npc7).velocity = Vector2.op_Multiply(((Entity) npc7).velocity, 0.9f);
            goto case 2f;
          case 15f:
            Vector2 targetPos5 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(((Entity) this.NPC).DirectionFrom(((Entity) player).Center), 500f));
            if ((double) this.NPC.ai[1] < 130.0 || (double) ((Entity) this.NPC).Distance(((Entity) player).Center) > 200.0 && (double) ((Entity) this.NPC).Distance(((Entity) player).Center) < 600.0)
            {
              NPC npc8 = this.NPC;
              ((Entity) npc8).velocity = Vector2.op_Multiply(((Entity) npc8).velocity, 0.97f);
            }
            else if ((double) ((Entity) this.NPC).Distance(targetPos5) > 50.0)
            {
              this.Movement(targetPos5, 0.6f, 32f);
              NPC npc9 = this.NPC;
              ((Entity) npc9).position = Vector2.op_Addition(((Entity) npc9).position, Vector2.op_Division(((Entity) player).velocity, 4f));
            }
            if ((double) this.NPC.ai[1] >= 10.0 && Main.netMode != 2)
              ShaderManager.GetFilter("FargowiltasSouls.Invert").SetFocusPosition(((Entity) this.NPC).Center);
            if ((double) this.NPC.ai[1] == 10.0)
            {
              this.NPC.localAI[0] = Utils.NextFloat(Main.rand, 6.28318548f);
              if (!Main.dedServ)
              {
                SoundStyle soundStyle = new SoundStyle("FargowiltasSouls/Assets/Sounds/ZaWarudo", (SoundType) 0);
                SoundEngine.PlaySound(ref soundStyle, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
              }
            }
            else if ((double) this.NPC.ai[1] < 210.0)
            {
              int num20 = 60 + Math.Max(2, 210 - (int) this.NPC.ai[1]);
              if (((Entity) Main.LocalPlayer).active && !Main.LocalPlayer.dead)
                Main.LocalPlayer.AddBuff(ModContent.BuffType<TimeFrozenBuff>(), num20, true, false);
              for (int index = 0; index < Main.maxNPCs; ++index)
              {
                if (((Entity) Main.npc[index]).active)
                  Main.npc[index].AddBuff(ModContent.BuffType<TimeFrozenBuff>(), num20, true);
              }
              for (int index = 0; index < Main.maxProjectiles; ++index)
              {
                if (((Entity) Main.projectile[index]).active && !Main.projectile[index].FargoSouls().TimeFreezeImmune)
                  Main.projectile[index].FargoSouls().TimeFrozen = num20;
              }
              if ((double) this.NPC.ai[1] < 130.0 && (double) ++this.NPC.ai[2] > 12.0)
              {
                this.NPC.ai[2] = 0.0f;
                bool flag = WorldSavingSystem.EternityMode && (double) this.NPC.localAI[2] != 0.0;
                int num21 = 300;
                float num22 = flag ? 250f : 150f;
                float num23 = flag ? 4f : 2.5f;
                int num24 = FargoSoulsUtil.ScaledProjectileDamage(this.NPC.defDamage);
                if ((double) this.NPC.ai[1] < 85.0 || !flag)
                {
                  if (flag && (double) this.NPC.ai[3] % 2.0 == 0.0)
                  {
                    float num25 = (float) num21 + this.NPC.ai[3] * num22;
                    int num26 = (int) (6.2831854820251465 * (double) num25);
                    this.NPC.localAI[0] = MathHelper.WrapAngle(this.NPC.localAI[0] + 3.14159274f + Utils.NextFloat(Main.rand, 1.57079637f));
                    for (int index = 0; index < num26; index += 120)
                    {
                      float num27 = (float) index / num25;
                      if ((double) num27 <= 2.0 * Math.PI - (double) MathHelper.WrapAngle(MathHelper.ToRadians(60f)))
                      {
                        float num28 = num25;
                        Vector2 vector2_12 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(num28, Utils.RotatedBy(Vector2.UnitX, (double) num27 + (double) this.NPC.localAI[0], new Vector2())));
                        Vector2 vector2_13 = Vector2.op_Multiply(num23, ((Entity) player).DirectionFrom(vector2_12));
                        float num29 = (float) ((double) ((Entity) player).Distance(vector2_12) / (double) num23 + 30.0);
                        if (FargoSoulsUtil.HostCheck)
                          Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_12, vector2_13, ModContent.ProjectileType<CosmosInvaderTime>(), num24, 0.0f, Main.myPlayer, num29, Utils.ToRotation(vector2_13), 0.0f);
                      }
                    }
                  }
                  else
                  {
                    int num30 = flag ? 12 : 8 + (int) this.NPC.ai[3] * ((double) this.NPC.localAI[2] == 0.0 ? 2 : 4);
                    float num31 = Utils.NextFloat(Main.rand, 6.28318548f);
                    for (int index = 0; index < num30; ++index)
                    {
                      float num32 = (float) num21 + this.NPC.ai[3] * num22;
                      Vector2 vector2_14 = Vector2.op_Addition(((Entity) player).Center, Vector2.op_Multiply(num32, Utils.RotatedBy(Vector2.UnitX, 2.0 * Math.PI / (double) num30 * (double) index + (double) num31, new Vector2())));
                      Vector2 vector2_15 = Vector2.op_Multiply(num23, ((Entity) player).DirectionFrom(vector2_14));
                      float num33 = (float) ((double) num32 / (double) num23 + 30.0);
                      if (FargoSoulsUtil.HostCheck)
                        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2_14, vector2_15, ModContent.ProjectileType<CosmosInvaderTime>(), num24, 0.0f, Main.myPlayer, num33, Utils.ToRotation(vector2_15), 0.0f);
                    }
                  }
                }
                ++this.NPC.ai[3];
              }
            }
            if ((double) ++this.NPC.ai[1] > 480.0)
            {
              this.NPC.TargetClosest(true);
              ++this.Animation;
              this.NPC.ai[1] = 0.0f;
              this.NPC.ai[2] = 0.0f;
              this.NPC.ai[3] = 0.0f;
              this.NPC.localAI[0] = 0.0f;
              this.NPC.netUpdate = true;
              break;
            }
            break;
          default:
            this.Animation = 0.0f;
            goto case 0.0f;
        }
        this.epicMe -= 0.02f;
        if ((double) this.epicMe >= 0.0)
          return;
        this.epicMe = 0.0f;
      }

      static bool IsDeviantt(int n)
      {
        if (!((Entity) Main.npc[n]).active || Main.npc[n].dontTakeDamage)
          return false;
        return Main.npc[n].type == ModContent.NPCType<Deviantt>() || Main.npc[n].type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.DeviBoss.DeviBoss>();
      }

      void NebulaDust()
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).Center, Vector2.op_Multiply(new Vector2((float) (-26 * ((Entity) this.NPC).direction), 22f), this.NPC.scale));
        for (int index1 = 0; index1 < 3; ++index1)
        {
          int index2 = Dust.NewDust(vector2, 0, 0, (int) byte.MaxValue, ((Entity) this.NPC).velocity.X * 0.3f, ((Entity) this.NPC).velocity.Y * 0.3f, 160, new Color(), 1f);
          Main.dust[index2].scale = 2.4f;
          Main.dust[index2].noGravity = true;
          Dust dust = Main.dust[index2];
          dust.velocity = Vector2.op_Multiply(dust.velocity, 3f);
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
      float num1 = ((Entity) this.NPC).Distance(targetPos);
      if ((double) num1 == 0.0)
        num1 = 0.1f;
      if ((double) ((Vector2) ref ((Entity) this.NPC).velocity).Length() > (double) num1)
        ((Entity) this.NPC).velocity = Vector2.op_Multiply(Vector2.Normalize(((Entity) this.NPC).velocity), num1);
      double num2 = (double) MathHelper.Clamp(((Entity) this.NPC).velocity.X, -cap, cap);
      double num3 = (double) MathHelper.Clamp(((Entity) this.NPC).velocity.Y, -cap, cap);
    }

    public virtual void FindFrame(int frameHeight)
    {
      if (++this.NPC.frameCounter > 6.0)
      {
        this.NPC.frameCounter = 0.0;
        this.NPC.frame.Y += frameHeight;
      }
      if (this.NPC.frame.Y > frameHeight * 5 - 1)
        this.NPC.frame.Y = 0;
      switch (this.Animation)
      {
        case -4f:
          this.NPC.frame.Y = frameHeight * 5;
          if ((double) this.NPC.localAI[0] >= 5.0)
          {
            this.NPC.frame.Y = frameHeight * 6;
            break;
          }
          break;
        case -3f:
          if ((double) this.NPC.ai[2] < 30.0 || (double) this.NPC.ai[2] > 100.0 && (double) this.NPC.ai[2] < 130.0)
          {
            this.NPC.frame.Y = frameHeight * 8;
            break;
          }
          if ((double) this.NPC.ai[2] > 70.0 && (double) this.NPC.ai[2] < 100.0 || (double) this.NPC.ai[2] > 170.0)
          {
            this.NPC.frame.Y = frameHeight * 7;
            break;
          }
          break;
        case -2f:
          this.NPC.frame.Y = frameHeight * 5;
          break;
        case -1f:
          if ((double) this.NPC.ai[1] > 120.0)
          {
            this.NPC.frame.Y = frameHeight * 8;
            break;
          }
          if ((double) this.NPC.ai[1] > 100.0)
          {
            this.NPC.frame.Y = frameHeight * 7;
            break;
          }
          break;
        case 1f:
          if ((double) this.NPC.ai[2] <= 6.0)
          {
            this.NPC.frame.Y = frameHeight * 5;
            break;
          }
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 3f:
          if ((double) this.NPC.ai[2] <= 70.0)
          {
            this.NPC.frame.Y = frameHeight * 5;
            break;
          }
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 5f:
          if ((double) this.NPC.ai[2] <= 75.0)
          {
            this.NPC.frame.Y = frameHeight * 5;
            break;
          }
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 7f:
          if ((double) this.NPC.ai[1] < 30.0)
          {
            this.NPC.frame.Y = frameHeight * 7;
            break;
          }
          if ((double) this.NPC.ai[1] < 60.0)
          {
            this.NPC.frame.Y = frameHeight * 8;
            break;
          }
          break;
        case 9f:
          if ((double) this.NPC.ai[2] <= 200.0)
          {
            this.NPC.frame.Y = frameHeight * 5;
            break;
          }
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 10f:
          this.NPC.frame.Y = frameHeight * 5;
          break;
        case 11f:
          if ((double) this.NPC.ai[1] > 60.0)
          {
            this.NPC.frame.Y = frameHeight * 6;
            break;
          }
          this.NPC.frame.Y = frameHeight * 5;
          break;
        case 13f:
          if ((double) this.NPC.ai[1] < 110.0)
          {
            if ((double) this.NPC.ai[2] <= 6.0)
            {
              this.NPC.frame.Y = frameHeight * 5;
              break;
            }
            this.NPC.frame.Y = frameHeight * 6;
            break;
          }
          if ((double) this.NPC.ai[1] <= 155.0)
          {
            this.NPC.frame.Y = frameHeight * 5;
            break;
          }
          this.NPC.frame.Y = frameHeight * 6;
          break;
        case 14f:
          this.NPC.frame.Y = frameHeight * 7;
          break;
        case 15f:
          if ((double) this.NPC.ai[1] < 10.0)
          {
            this.NPC.frame.Y = frameHeight * 7;
            break;
          }
          if ((double) this.NPC.ai[1] < 130.0)
          {
            this.NPC.frame.Y = frameHeight * 8;
            break;
          }
          break;
      }
      if (!((IEnumerable<Projectile>) Main.projectile).Any<Projectile>((Func<Projectile, bool>) (p =>
      {
        if (!p.TypeAlive<GlowRing>() || (double) p.ai[0] != (double) ((Entity) this.NPC).whoAmI)
          return false;
        return (double) p.ai[1] == -23.0 || (double) p.ai[1] == -20.0;
      })))
        return;
      this.NPC.frame.Y = frameHeight * 5;
    }

    public virtual void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      target.AddBuff(67, 120, true, false);
      target.AddBuff(144, 300, true, false);
      target.AddBuff(ModContent.BuffType<BerserkedBuff>(), 300, true, false);
      target.AddBuff(44, 300, true, false);
    }

    public virtual void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 0.9f);
    }

    public virtual void HitEffect(NPC.HitInfo hit)
    {
      if (this.NPC.life > 0)
        return;
      for (int index = 1; index <= 6; ++index)
      {
        Vector2 vector2_1 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2(Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).width), Utils.NextFloat(Main.rand, (float) ((Entity) this.NPC).height)));
        if (!Main.dedServ)
        {
          IEntitySource sourceFromThis = ((Entity) this.NPC).GetSource_FromThis((string) null);
          Vector2 vector2_2 = vector2_1;
          Vector2 velocity = ((Entity) this.NPC).velocity;
          string name = ((ModType) this).Mod.Name;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(10, 1);
          interpolatedStringHandler.AppendLiteral("CosmosGore");
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
      NPC.SetEventFlagCleared(ref WorldSavingSystem.DownedBoss[8], -1);
      if (!FargoSoulsUtil.HostCheck)
        return;
      for (int index = 0; index < 10; ++index)
      {
        Vector2 vector2 = Vector2.op_Addition(((Entity) this.NPC).position, new Vector2((float) Main.rand.Next(((Entity) this.NPC).width), (float) Main.rand.Next(((Entity) this.NPC).height)));
        int num = ModContent.ProjectileType<MutantBomb>();
        Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), vector2, Vector2.Zero, num, 0, 0.0f, Main.myPlayer, 0.0f, 0.0f, 0.0f);
      }
      Projectile.NewProjectile(((Entity) this.NPC).GetSource_FromThis((string) null), ((Entity) this.NPC).Center, Vector2.Zero, ModContent.ProjectileType<MoonLordMoonBlast>(), 0, 0.0f, Main.myPlayer, -Utils.ToRotation(Vector2.UnitY), 32f, 0.0f);
    }

    public virtual void ModifyNPCLoot(NPCLoot npcLoot)
    {
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) new ChampionEnchDropRule(BaseForce.EnchantsIn<CosmoForce>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.BossBag(ModContent.ItemType<CosmosBag>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.Common(ModContent.ItemType<EridanusTrophy>(), 10, 1, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.MasterModeCommonDrop(ModContent.ItemType<EridanusRelic>()));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.NotExpert(), ModContent.ItemType<Eridanium>(), 1, 5, 10, 1));
      ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new Conditions.NotExpert(), ModContent.Find<ModItem>("Fargowiltas", "CrucibleCosmos").Type, 1, 1, 1, 1));
    }

    public virtual bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
    {
      Texture2D texture2D1 = TextureAssets.Npc[this.NPC.type].Value;
      Texture2D texture2D2 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosChampion_Glow", (AssetRequestMode) 1).Value;
      Texture2D texture2D3 = ModContent.Request<Texture2D>("FargowiltasSouls/Content/Bosses/Champions/Cosmos/CosmosChampion_Glow2", (AssetRequestMode) 1).Value;
      Rectangle frame = this.NPC.frame;
      Vector2 vector2_1 = Vector2.op_Division(Utils.Size(frame), 2f);
      SpriteEffects spriteEffects = this.NPC.spriteDirection > 0 ? (SpriteEffects) 0 : (SpriteEffects) 1;
      Vector2 vector2_2 = Vector2.op_Subtraction(((Entity) this.NPC).Center, screenPos);
      if ((double) Main.LocalPlayer.gravDir < 0.0)
        vector2_2.Y = (float) Main.screenHeight - vector2_2.Y;
      Color glowColor = new Color(Main.DiscoR / 3 + 150, Main.DiscoG / 3 + 150, Main.DiscoB / 3 + 150);
      float num1 = this.Animation;
      if ((double) num1 != 2.0)
      {
        if ((double) num1 != 3.0)
        {
          if ((double) num1 != 7.0)
          {
            if ((double) num1 != 10.0)
            {
              if ((double) num1 != 11.0)
              {
                if ((double) num1 != 14.0)
                {
                  if ((double) num1 == 15.0)
                    lerpGlow(Color.Blue, (float) ((double) this.NPC.ai[1] / 240.0 - 1.0));
                }
                else
                  lerpGlow(Color.Blue, (float) (1.0 - (double) this.NPC.ai[1] / 60.0));
              }
              else
                lerpGlow(Color.Purple, (float) ((double) this.NPC.ai[1] / 180.0 - 1.0));
            }
            else
              lerpGlow(Color.Purple, (float) (1.0 - (double) this.NPC.ai[1] / 30.0));
          }
          else
            lerpGlow(Color.LimeGreen, (double) this.NPC.ai[1] < 30.0 ? (float) (1.0 - (double) this.NPC.ai[1] / 30.0) : (float) (((double) this.NPC.ai[1] - 30.0) / 360.0));
        }
        else
          lerpGlow(Color.OrangeRed, (float) ((double) this.NPC.ai[1] / 150.0 - 1.0));
      }
      else
        lerpGlow(Color.OrangeRed, (float) (1.0 - (double) this.NPC.ai[1] / 60.0));
      if (!this.NPC.IsABestiaryIconDummy)
      {
        glowColor = Color.op_Multiply(glowColor, this.NPC.Opacity);
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      if ((double) this.NPC.localAI[2] != 0.0 || (double) this.Animation == -4.0)
      {
        for (int index = 0; index < NPCID.Sets.TrailCacheLength[this.NPC.type]; ++index)
        {
          Vector2 vector2_3 = Vector2.op_Subtraction(this.NPC.oldPos[index], screenPos);
          if ((double) Main.LocalPlayer.gravDir < 0.0)
            vector2_3.Y = (float) Main.screenHeight - vector2_3.Y;
          float rotation = this.NPC.rotation;
          Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(Vector2.op_Addition(vector2_3, Vector2.op_Division(((Entity) this.NPC).Size, 2f)), new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(glowColor, 0.6f), rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        }
      }
      if ((double) this.epicMe > 0.0)
      {
        float num2 = 10f * this.NPC.scale * (float) Math.Cos(Math.PI / 2.0 * (double) this.epicMe);
        float num3 = this.NPC.Opacity * (float) Math.Sqrt((double) this.epicMe);
        Main.EntitySpriteDraw(texture2D2, Vector2.op_Addition(vector2_2, new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), Color.op_Multiply(glowColor, num3), this.NPC.rotation, vector2_1, num2, spriteEffects, 0.0f);
      }
      if (!this.NPC.IsABestiaryIconDummy)
      {
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.NonPremultiplied, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      Main.EntitySpriteDraw(texture2D1, Vector2.op_Addition(vector2_2, new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), this.NPC.GetAlpha(drawColor), this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
      if (!this.NPC.IsABestiaryIconDummy)
      {
        Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(vector2_2, new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), glowColor, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
        Main.EntitySpriteDraw(texture2D3, Vector2.op_Addition(vector2_2, new Vector2(0.0f, this.NPC.gfxOffY)), new Rectangle?(frame), glowColor, this.NPC.rotation, vector2_1, this.NPC.scale, spriteEffects, 0.0f);
        spriteBatch.End();
        spriteBatch.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect) null, Main.GameViewMatrix.ZoomMatrix);
      }
      return false;

      void lerpGlow(Color color, float modifier)
      {
        if ((double) modifier < 0.0)
          modifier = 0.0f;
        if ((double) modifier > 1.0)
          modifier = 1f;
        ((Color) ref glowColor).R = (byte) MathHelper.Lerp((float) ((Color) ref color).R, (float) ((Color) ref glowColor).R, modifier);
        ((Color) ref glowColor).G = (byte) MathHelper.Lerp((float) ((Color) ref color).G, (float) ((Color) ref glowColor).G, modifier);
        ((Color) ref glowColor).B = (byte) MathHelper.Lerp((float) ((Color) ref color).B, (float) ((Color) ref glowColor).B, modifier);
      }
    }
  }
}
