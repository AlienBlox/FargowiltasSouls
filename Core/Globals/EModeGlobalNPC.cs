// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.EModeGlobalNPC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Events;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  public class EModeGlobalNPC : GlobalNPC
  {
    public bool BeetleOffenseAura;
    public bool BeetleDefenseAura;
    public bool BeetleUtilAura;
    public int BeetleTimer;
    public bool PaladinsShield;
    public bool isWaterEnemy;
    public bool HasWhipDebuff;
    public static int slimeBoss = -1;
    public static int eyeBoss = -1;
    public static int eaterBoss = -1;
    public static int brainBoss = -1;
    public static int beeBoss = -1;
    public static int skeleBoss = -1;
    public static int deerBoss = -1;
    public static int wallBoss = -1;
    public static int retiBoss = -1;
    public static int spazBoss = -1;
    public static int destroyBoss = -1;
    public static int primeBoss = -1;
    public static int queenSlimeBoss = -1;
    public static int empressBoss = -1;
    public static int betsyBoss = -1;
    public static int fishBoss = -1;
    public static int cultBoss = -1;
    public static int moonBoss = -1;
    public static int guardBoss = -1;
    public static int fishBossEX = -1;
    public static bool spawnFishronEX;
    public static int deviBoss = -1;
    public static int abomBoss = -1;
    public static int mutantBoss = -1;
    public static int championBoss = -1;
    public static int eaterTimer;

    public virtual bool InstancePerEntity => true;

    public virtual void ResetEffects(NPC npc)
    {
      this.PaladinsShield = false;
      this.HasWhipDebuff = false;
      if (this.BeetleTimer <= 0 || --this.BeetleTimer > 0)
        return;
      this.BeetleDefenseAura = false;
      this.BeetleOffenseAura = false;
      this.BeetleUtilAura = false;
    }

    public virtual void SetDefaults(NPC npc)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      npc.value = (float) (int) ((double) npc.value * 1.3);
      if (npc.boss || npc.townNPC || npc.CountsAsACritter || npc.life <= 10 || Main.masterMode || Luminance.Common.Utilities.Utilities.AnyBosses())
        return;
      npc.lifeMax = (int) Math.Round((double) npc.lifeMax * 1.1000000238418579);
    }

    public virtual bool PreAI(NPC npc)
    {
      if (!WorldSavingSystem.EternityMode)
        return base.PreAI(npc);
      if (!Main.dayTime && !Main.hardMode && ((IEnumerable<Player>) Main.player).Any<Player>((Func<Player, bool>) (p => p.Alive() && p.FargoSouls().BoxofGizmos)))
      {
        int num1 = (int) ((Entity) npc).Center.X / 16;
        int num2 = (int) ((Entity) npc).Center.Y / 16;
        if ((double) num2 < Main.worldSurface && num2 > 0 && num1 > 0 && num1 < Main.maxTilesX)
        {
          Tile tileSafely = Framing.GetTileSafely(num1, num2);
          if (Tile.op_Inequality(tileSafely, (ArgumentException) null) && ((Tile) ref tileSafely).WallType == (ushort) 0)
            Lighting.AddLight(((Entity) npc).Center, 0.5f, 0.5f, 0.5f);
        }
      }
      if (!npc.dontTakeDamage)
      {
        bool flag = npc.boss || npc.type == 13 || npc.type == 14 || npc.type == 15;
        if ((double) ((Entity) npc).position.Y / 16.0 < Main.worldSurface * 0.34999999403953552 && !flag)
          npc.AddBuff(68, 2, true);
        else if ((double) ((Entity) npc).position.Y / 16.0 > (double) (Main.maxTilesY - 200) && !flag && !Main.remixWorld && FargoSoulsUtil.HostCheck)
          npc.AddBuff(24, 2, false);
        Vector2 center = ((Entity) npc).Center;
        center.X /= 16f;
        center.Y /= 16f;
        Tile tileSafely = Framing.GetTileSafely((int) center.X, (int) center.Y);
        if (Main.raining && (double) ((Entity) npc).position.Y / 16.0 < Main.worldSurface && ((Tile) ref tileSafely).WallType == (ushort) 0)
          npc.AddBuff(103, 2, false);
        if (((Entity) npc).wet && !npc.noTileCollide && !this.isWaterEnemy && npc.HasPlayerTarget)
        {
          npc.AddBuff(ModContent.BuffType<LethargicBuff>(), 2, true);
          if (Main.player[npc.target].ZoneCorrupt)
            npc.AddBuff(39, 2, true);
          if (Main.player[npc.target].ZoneCrimson)
            npc.AddBuff(69, 2, true);
          if (Main.player[npc.target].ZoneHallow)
            npc.AddBuff(ModContent.BuffType<SmiteBuff>(), 2, true);
          if (Main.player[npc.target].ZoneJungle)
            npc.AddBuff(20, 2, true);
        }
      }
      return true;
    }

    public virtual void OnHitPlayer(NPC npc, Player target, Player.HurtInfo hurtInfo)
    {
      if (!WorldSavingSystem.EternityMode || !this.BeetleUtilAura)
        return;
      target.FargoSouls().AddBuffNoStack(47, 30);
    }

    public virtual void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      spawnRate = (int) ((double) spawnRate * 0.9);
      maxSpawns = (int) ((double) maxSpawns * 1.2000000476837158);
      if ((!player.ZoneTowerSolar || NPC.ShieldStrengthTowerSolar != 0) && (!player.ZoneTowerVortex || NPC.ShieldStrengthTowerVortex != 0) && (!player.ZoneTowerNebula || NPC.ShieldStrengthTowerNebula != 0) && (!player.ZoneTowerStardust || NPC.ShieldStrengthTowerStardust != 0))
        return;
      maxSpawns = 0;
    }

    public virtual void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
    {
      int spawnTileY = spawnInfo.SpawnTileY;
      bool flag1 = (double) spawnTileY >= (double) Main.maxTilesY * 0.40000000596046448 && (double) spawnTileY <= (double) Main.maxTilesY * 0.800000011920929;
      bool flag2 = (double) spawnTileY > Main.worldSurface && (double) spawnTileY <= (double) Main.maxTilesY * 0.40000000596046448;
      bool flag3 = (double) spawnTileY < Main.worldSurface && !spawnInfo.Sky;
      bool flag4 = flag1 | flag2;
      bool underworldHeight = spawnInfo.Player.ZoneUnderworldHeight;
      bool sky = spawnInfo.Sky;
      bool flag5 = !Main.dayTime;
      bool dayTime = Main.dayTime;
      bool flag6 = FargowiltasSouls.FargowiltasSouls.NoBiomeNormalSpawn(spawnInfo);
      bool zoneBeach = spawnInfo.Player.ZoneBeach;
      bool zoneDungeon = spawnInfo.Player.ZoneDungeon;
      bool spiderCave = spawnInfo.SpiderCave;
      bool zoneGlowshroom = spawnInfo.Player.ZoneGlowshroom;
      bool zoneJungle = spawnInfo.Player.ZoneJungle;
      bool granite = spawnInfo.Granite;
      bool marble = spawnInfo.Marble;
      bool zoneCorrupt = spawnInfo.Player.ZoneCorrupt;
      bool zoneCrimson = spawnInfo.Player.ZoneCrimson;
      bool zoneSnow = spawnInfo.Player.ZoneSnow;
      bool zoneHallow = spawnInfo.Player.ZoneHallow;
      bool zoneDesert = spawnInfo.Player.ZoneDesert;
      bool zoneTowerNebula = spawnInfo.Player.ZoneTowerNebula;
      bool zoneTowerVortex = spawnInfo.Player.ZoneTowerVortex;
      bool zoneTowerStardust = spawnInfo.Player.ZoneTowerStardust;
      bool zoneTowerSolar = spawnInfo.Player.ZoneTowerSolar;
      bool flag7 = DD2Event.Ongoing && spawnInfo.Player.ZoneOldOneArmy;
      bool flag8 = flag3 & flag5 && Main.snowMoon;
      bool flag9 = flag3 & flag5 && Main.pumpkinMoon;
      bool flag10 = flag3 & dayTime && Main.eclipse;
      bool flag11 = NPC.LunarApocalypseIsUp && zoneTowerNebula | zoneTowerVortex | zoneTowerStardust | zoneTowerSolar;
      bool flag12 = Main.invasionType == 0 && !flag7 && !flag8 && !flag9 && !flag10 && !flag11;
      bool flag13 = FargowiltasSouls.FargowiltasSouls.NoInvasion(spawnInfo);
      bool flag14 = ((!(!spawnInfo.PlayerInTown & flag13) ? 0 : (!flag7 ? 1 : 0)) & (flag12 ? 1 : 0)) != 0;
      bool flag15 = WorldSavingSystem.MasochistModeReal && !spawnInfo.Player.HasEffect<SinisterIconEffect>() && !Luminance.Common.Utilities.Utilities.AnyBosses();
      if (!WorldSavingSystem.EternityMode)
        return;
      if (!Main.hardMode)
      {
        if (flag3)
        {
          if (flag5 & flag14)
          {
            if (flag6)
            {
              pool[47] = NPC.downedBoss1 ? 0.02f : 0.01f;
              pool[464] = NPC.downedBoss1 ? 0.02f : 0.01f;
            }
            if (zoneSnow)
            {
              pool[168] = NPC.downedBoss1 ? 0.04f : 0.02f;
              pool[470] = NPC.downedBoss1 ? 0.04f : 0.02f;
            }
            if (zoneBeach || Main.raining)
            {
              pool[57] = NPC.downedBoss1 ? 0.04f : 0.02f;
              pool[465] = NPC.downedBoss1 ? 0.04f : 0.02f;
            }
            if (NPC.downedBoss1)
            {
              if (zoneJungle)
                pool[52] = 0.05f;
              if (((!NPC.downedBoss3 ? 0 : (!NPC.downedMechBoss2 ? 1 : 0)) & (flag15 ? 1 : 0)) != 0)
                pool[4] = Main.bloodMoon ? 0.0004f : 0.0002f;
            }
          }
          if (flag14 && WorldSavingSystem.DownedAnyBoss)
          {
            if (zoneSnow)
              pool[243] = 0.005f;
            if (zoneDesert)
              pool[541] = 0.005f;
          }
          if (((!Main.slimeRain ? 0 : (NPC.downedBoss2 ? 1 : 0)) & (flag15 ? 1 : 0)) != 0)
            pool[50] = 0.004f;
          if (dayTime && NPC.downedGolemBoss && flag6 | zoneDungeon)
            pool[380] = 0.01f;
        }
        else if (flag4)
        {
          if (marble && NPC.downedBoss2)
            pool[480] = 0.04f;
          if (granite)
          {
            pool[483] = 0.1f;
            pool[482] = 0.1f;
          }
          if (flag1 && flag6 && NPC.downedBoss3)
            pool[32] = 0.02f;
          if (NPC.downedGoblins && !NPC.savedGoblin && !NPC.AnyNPCs(105))
            pool[105] = 0.5f;
          if (spiderCave && !NPC.savedStylist && !NPC.AnyNPCs(354))
            pool[354] = 0.5f;
        }
        else if (underworldHeight)
        {
          pool[117] = 0.02f;
          pool[72] = 0.05f;
        }
        else if (sky && flag14)
        {
          pool[250] = 0.02f;
          if (WorldSavingSystem.DownedAnyBoss)
            pool[87] = 0.005f;
        }
        if (zoneCorrupt && NPC.downedBoss2)
        {
          pool[98] = 0.005f;
          if (((!flag14 || !NPC.downedBoss3 ? 0 : (!underworldHeight ? 1 : 0)) & (flag15 ? 1 : 0)) != 0)
            pool[13] = 0.0002f;
        }
        if (zoneCrimson && NPC.downedBoss2)
        {
          pool[268] = 0.005f;
          if (((!flag14 || !NPC.downedBoss3 ? 0 : (!underworldHeight ? 1 : 0)) & (flag15 ? 1 : 0)) != 0)
            pool[266] = 0.0002f;
        }
        if (zoneJungle && WorldSavingSystem.MasochistModeReal & flag14)
          pool[252] = 0.025f;
        if (zoneGlowshroom)
        {
          pool[259] = 0.02f;
          pool[258] = 0.02f;
          pool[254] = 0.02f;
          pool[(int) byte.MaxValue] = 0.02f;
          pool[257] = 0.02f;
        }
        if (zoneBeach)
        {
          pool[170] = 1f / 500f;
          pool[180] = 1f / 500f;
          pool[171] = 1f / 500f;
        }
        if (!flag3 & flag14)
        {
          pool[85] = 1f / 500f;
          if (zoneDesert && NPC.downedBoss2)
            pool[510] = 1f / 500f;
        }
      }
      else
      {
        if (flag3 && !flag11)
        {
          if (dayTime)
          {
            if (flag14)
            {
              if (flag6 & flag15)
                pool[50] = Main.slimeRain ? 0.0004f : 0.0002f;
              if (NPC.downedGolemBoss && flag6 | zoneDungeon)
                pool[380] = 0.01f;
            }
          }
          else
          {
            if (Main.bloodMoon)
              pool[378] = 0.1f;
            if (((!flag13 ? 0 : (!flag7 ? 1 : 0)) & (flag15 ? 1 : 0)) != 0)
              pool[109] = 0.01f;
            if (flag14)
            {
              if (NPC.downedBoss1)
              {
                if (flag6)
                {
                  pool[47] = 0.05f;
                  pool[464] = 0.05f;
                }
                if (zoneSnow)
                {
                  pool[168] = 0.05f;
                  pool[470] = 0.05f;
                }
                if (zoneBeach || Main.raining)
                {
                  pool[57] = 0.05f;
                  pool[465] = 0.05f;
                }
              }
              if (flag15 && !NPC.downedMechBoss2)
                pool[4] = 1f / 1000f;
              if (NPC.downedMechBossAny)
                pool[139] = 0.01f;
              if (NPC.downedPlantBoss)
              {
                if (flag15)
                {
                  pool[125] = 0.0001f;
                  pool[126] = 0.0001f;
                  pool[134] = 0.0001f;
                  pool[(int) sbyte.MaxValue] = 0.0001f;
                }
                pool[291] = 0.005f;
                pool[293] = 0.005f;
                pool[292] = 0.005f;
              }
            }
            if (NPC.downedMechBossAny & flag13)
            {
              if (flag6)
              {
                pool[305] = 0.01f;
                pool[306] = 0.01f;
                pool[307] = 0.01f;
                pool[308] = 0.01f;
                pool[309] = 0.01f;
                pool[310] = 0.01f;
                pool[311] = 0.01f;
                pool[312] = 0.01f;
                pool[313] = 0.01f;
                pool[314] = 0.01f;
                if (NPC.downedHalloweenKing & flag15)
                  pool[327] = 1f / 400f;
              }
              else
              {
                if (zoneHallow)
                  pool[341] = 0.01f;
                else if (zoneCrimson | zoneCorrupt)
                {
                  pool[326] = 0.05f;
                  if (NPC.downedHalloweenTree & flag15)
                    pool[325] = 1f / 400f;
                }
                if (zoneSnow)
                {
                  pool[338] = 0.02f;
                  pool[339] = 0.02f;
                  pool[340] = 0.02f;
                  pool[343] = 0.01f;
                  pool[350] = 0.05f;
                  pool[347] = 0.01f;
                  if (NPC.downedChristmasTree & flag15)
                    pool[344] = 1f / 400f;
                  if (NPC.downedChristmasSantank & flag15)
                    pool[346] = 1f / 400f;
                }
              }
            }
          }
          if (zoneHallow)
          {
            if (!Main.raining)
              pool[244] = 1f / 1000f;
            pool[342] = 0.05f;
          }
          if (zoneSnow & flag13 && !Main.raining && !spawnInfo.PlayerInTown)
            pool[243] = 0.01f;
          if (zoneBeach)
          {
            if (flag5)
              pool[461] = 0.02f;
            pool[170] = 0.01f;
            pool[180] = 0.01f;
            pool[171] = 0.01f;
            if (NPC.downedFishron & flag15)
              pool[370] = 0.0002f;
          }
          else if (zoneDesert)
            pool[532] = 0.05f;
          if (NPC.downedMechBossAny && Main.raining)
            pool[358] = 0.1f;
          if (zoneCorrupt)
            pool[98] = 0.1f;
          if (zoneCrimson)
            pool[268] = 0.1f;
        }
        else if (flag4)
        {
          if (zoneDesert && !zoneCorrupt && !zoneCrimson)
            pool[533] = 0.05f;
          if (flag1 && flag6 && NPC.downedBoss3)
            pool[32] = 0.05f;
          if (!NPC.savedWizard && !NPC.AnyNPCs(106))
            pool[106] = 0.5f;
          if (zoneDungeon & flag5 & flag14 & flag15)
            pool[35] = 5E-05f;
          if (NPC.downedMechBossAny && zoneSnow && !Main.dayTime)
          {
            if (flag2)
              pool[348] = 0.05f;
            if (flag1)
            {
              pool[351] = 0.025f;
              if (NPC.downedChristmasIceQueen & flag15)
                pool[345] = 1f / 400f;
            }
          }
          if (NPC.downedAncientCultist & zoneDungeon & flag15)
            pool[439] = 2E-05f;
          if (spawnInfo.Player.ZoneUndergroundDesert)
          {
            if (!zoneHallow && !zoneCorrupt && !zoneCrimson)
            {
              pool[542] = 0.2f;
            }
            else
            {
              if (zoneHallow)
                pool[545] = 0.2f;
              if (zoneCorrupt)
                pool[543] = 0.2f;
              if (zoneCrimson)
                pool[544] = 0.2f;
            }
          }
        }
        else if (underworldHeight)
        {
          pool[117] = 0.025f;
          pool[39] = 0.025f;
          pool[72] = 0.05f;
          if (flag15 && !FargoSoulsUtil.BossIsAlive(ref EModeGlobalNPC.wallBoss, 113))
            pool[116] = 0.03f;
          if (NPC.downedMechBossAny)
            pool[72] = 0.05f;
          if (NPC.downedPlantBoss)
          {
            pool[285] = 1f / 1000f;
            pool[286] = 1f / 1000f;
            pool[283] = 1f / 1000f;
            pool[284] = 1f / 1000f;
            pool[281] = 1f / 1000f;
            pool[282] = 1f / 1000f;
          }
          if (WorldSavingSystem.DownedBetsy & flag15)
            pool[551] = 0.0002f;
        }
        else if (sky && flag14)
        {
          pool[250] = 0.05f;
          pool[395] = 1f / 1000f;
          if (NPC.downedGolemBoss)
          {
            pool[412] = 0.03f;
            pool[426] = 0.03f;
            pool[420] = 0.03f;
            pool[407] = 0.03f;
            pool[521] = 0.03f;
            pool[454] = 0.03f;
          }
          else if (NPC.downedMechBossAny)
          {
            pool[412] = 1f / 1000f;
            pool[426] = 1f / 1000f;
            pool[420] = 1f / 1000f;
            pool[407] = 1f / 1000f;
          }
          if (NPC.downedMoonlord & flag15)
            pool[398] = 0.0002f;
        }
        if (zoneCorrupt && flag14 & flag15)
          pool[13] = 0.0002f;
        if (zoneCrimson && flag14 & flag15)
          pool[266] = 0.0002f;
        if (zoneJungle)
        {
          if (flag14 & flag15)
            pool[222] = 0.0001f;
          if (flag14)
            pool[252] = 0.025f;
          if (!flag3)
          {
            pool[476] = 0.0015f;
            if (NPC.downedGolemBoss & flag15)
              pool[262] = 5E-05f;
          }
        }
        if (spawnInfo.Lihzahrd && spawnInfo.SpawnTileType == 226)
        {
          pool[72] = 0.1f;
          pool[70] = 0.1f;
          if (NPC.downedPlantBoss)
          {
            pool[476] = 0.01f;
            pool[285] = 0.005f;
            pool[286] = 0.005f;
            pool[283] = 0.005f;
            pool[284] = 0.005f;
            pool[281] = 0.005f;
            pool[282] = 0.005f;
          }
        }
        if (zoneBeach && spawnInfo.Water)
          pool[102] = 0.1f;
      }
      if (!zoneSnow || flag3)
        return;
      pool[185] = 0.05f;
    }

    public virtual void OnKill(NPC npc)
    {
      base.OnKill(npc);
      if (npc.type != 227 || !WorldSavingSystem.DownedMutant || !NPC.AnyNPCs(ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()))
        return;
      Item.NewItem(((Entity) npc).GetSource_Loot((string) null), ((Entity) npc).Hitbox, ModContent.ItemType<ScremPainting>(), 1, false, 0, false, false);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      int type = npc.type;
      if (type <= 184)
      {
        if (type <= 95)
        {
          if (type <= 16)
          {
            if (type <= 3)
            {
              if ((uint) (type - -35) > 1U)
              {
                switch (type - 1)
                {
                  case 0:
                    TimsConcoctionDrop(ItemDropRule.Common(npc.netID == -4 ? 2351 : 2350, 1, 1, 1));
                    goto label_103;
                  case 1:
                    goto label_52;
                  case 2:
                    goto label_60;
                  default:
                    goto label_103;
                }
              }
              else
                goto label_60;
            }
            else if (type != 10)
            {
              if (type == 16)
              {
                TimsConcoctionDrop(ItemDropRule.Common(2328, 1, 1, 6));
                goto label_103;
              }
              else
                goto label_103;
            }
          }
          else if (type <= 85)
          {
            switch (type - 24)
            {
              case 0:
                TimsConcoctionDrop(ItemDropRule.Common(2348, 1, 1, 3));
                goto label_103;
              case 1:
              case 6:
              case 9:
              case 10:
              case 11:
              case 12:
              case 13:
              case 14:
              case 16:
              case 17:
              case 22:
              case 26:
              case 29:
              case 30:
              case 31:
              case 32:
              case 37:
              case 38:
              case 42:
              case 44:
              case 46:
                goto label_103;
              case 2:
              case 3:
              case 4:
                TimsConcoctionDrop(ItemDropRule.Common(300, 1, 1, 1));
                goto label_103;
              case 5:
                TimsConcoctionDrop(ItemDropRule.Common(293, 1, 1, 3));
                goto label_103;
              case 7:
                goto label_57;
              case 8:
                TimsConcoctionDrop(ItemDropRule.Common(4870, 1, 1, 6));
                goto label_103;
              case 15:
                TimsConcoctionDrop(ItemDropRule.Common(288, 1, 1, 12));
                goto label_103;
              case 18:
                goto label_53;
              case 19:
                goto label_101;
              case 20:
                TimsConcoctionDrop(ItemDropRule.Common(2322, 1, 1, 6));
                goto label_103;
              case 21:
                TimsConcoctionDrop(ItemDropRule.Common(294, 1, 1, 6));
                goto label_103;
              case 23:
              case 33:
                goto label_102;
              case 24:
                TimsConcoctionDrop(ItemDropRule.Common(2324, 1, 1, 3));
                goto label_103;
              case 25:
              case 27:
              case 36:
                goto label_56;
              case 28:
                TimsConcoctionDrop(ItemDropRule.Common(296, 1, 1, 6));
                goto label_103;
              case 34:
                goto label_68;
              case 35:
                TimsConcoctionDrop(ItemDropRule.Common(2359, 1, 1, 3));
                goto label_103;
              case 39:
              case 40:
                goto label_80;
              case 41:
                TimsConcoctionDrop(ItemDropRule.Common(2327, 1, 1, 3));
                goto label_103;
              case 43:
                TimsConcoctionDrop(ItemDropRule.Common(2356, 1, 1, 3));
                goto label_103;
              case 45:
                goto label_61;
              case 47:
                TimsConcoctionDrop(ItemDropRule.Common(4478, 1, 1, 6));
                goto label_103;
              default:
                if (type == 85)
                  goto case 28;
                else
                  goto label_103;
            }
          }
          else if (type != 87)
          {
            if (type != 93)
            {
              if (type != 95)
                goto label_103;
            }
            else
              goto label_56;
          }
          else
          {
            TimsConcoctionDrop(ItemDropRule.Common(305, 1, 1, 12));
            goto label_103;
          }
          TimsConcoctionDrop(ItemDropRule.Common(2997, 1, 1, 3));
          goto label_103;
        }
        else if (type <= 120)
        {
          if (type <= 109)
          {
            if (type != 103)
            {
              if (type == 109)
              {
                TimsConcoctionDrop(ItemDropRule.Common(4479, 1, 1, 6));
                goto label_103;
              }
              else
                goto label_103;
            }
            else
              goto label_80;
          }
          else if (type != 111)
          {
            if (type == 120)
            {
              TimsConcoctionDrop(ItemDropRule.Common(2351, 1, 1, 3));
              TimsConcoctionDrop(ItemDropRule.Common(4870, 1, 1, 6));
              goto label_103;
            }
            else
              goto label_103;
          }
          else
          {
            TimsConcoctionDrop(ItemDropRule.Common(303, 1, 1, 3));
            goto label_103;
          }
        }
        else if (type <= 137)
        {
          if (type != 132)
          {
            if (type != 137)
              goto label_103;
          }
          else
            goto label_60;
        }
        else
        {
          switch (type - 147)
          {
            case 0:
              TimsConcoctionDrop(ItemDropRule.Common(302, 1, 1, 3));
              goto label_103;
            case 1:
            case 2:
            case 8:
            case 9:
            case 13:
            case 14:
            case 15:
            case 19:
            case 20:
            case 22:
            case 23:
            case 24:
              goto label_103;
            case 3:
            case 4:
            case 5:
            case 11:
            case 12:
              break;
            case 6:
            case 7:
              goto label_59;
            case 10:
              goto label_68;
            case 16:
            case 17:
            case 18:
              goto label_62;
            case 21:
              goto label_102;
            case 25:
              TimsConcoctionDrop(ItemDropRule.Common(294, 1, 1, 6));
              TimsConcoctionDrop(ItemDropRule.Common(293, 1, 1, 6));
              goto label_103;
            default:
              if (type != 176)
              {
                if (type == 184)
                  goto case 0;
                else
                  goto label_103;
              }
              else
                goto label_53;
          }
        }
label_56:
        TimsConcoctionDrop(ItemDropRule.Common(2355, 1, 1, 1));
        goto label_103;
label_68:
        TimsConcoctionDrop(ItemDropRule.Common(291, 1, 1, 3));
        goto label_103;
label_80:
        TimsConcoctionDrop(ItemDropRule.Common(298, 1, 1, 3));
        goto label_103;
      }
      else if (type <= 296)
      {
        if (type <= 211)
        {
          if (type <= 196)
          {
            if ((uint) (type - 186) > 3U)
            {
              if (type == 196)
              {
                TimsConcoctionDrop(ItemDropRule.Common(2352, 1, 1, 6));
                goto label_103;
              }
              else
                goto label_103;
            }
            else
              goto label_60;
          }
          else if (type != 200)
          {
            if ((uint) (type - 210) <= 1U)
            {
              TimsConcoctionDrop(ItemDropRule.Common(2349, 2, 1, 1));
              goto label_103;
            }
            else
              goto label_103;
          }
          else
            goto label_60;
        }
        else if (type <= 244)
        {
          if (type != 216)
          {
            switch (type - 224)
            {
              case 0:
                TimsConcoctionDrop(ItemDropRule.Common(2354, 1, 1, 3));
                goto label_103;
              case 1:
                TimsConcoctionDrop(ItemDropRule.Common(295, 1, 1, 3));
                goto label_103;
              case 7:
              case 8:
              case 9:
              case 10:
              case 11:
                goto label_53;
              case 12:
              case 13:
              case 14:
                goto label_62;
              case 20:
                TimsConcoctionDrop(ItemDropRule.Common(289, 1, 1, 3));
                goto label_103;
              default:
                goto label_103;
            }
          }
          else
          {
            TimsConcoctionDrop(ItemDropRule.Common(2344, 1, 1, 12));
            goto label_103;
          }
        }
        else if (type != 252)
        {
          if ((uint) (type - 269) > 11U)
          {
            switch (type)
            {
              case 294:
              case 295:
              case 296:
                goto label_57;
              default:
                goto label_103;
            }
          }
          else
            goto label_57;
        }
        else
          goto label_101;
      }
      else if (type <= 348)
      {
        if (type <= 321)
        {
          if ((uint) (type - 317) > 1U)
          {
            if ((uint) (type - 319) <= 2U)
              goto label_60;
            else
              goto label_103;
          }
        }
        else if ((uint) (type - 331) > 1U)
        {
          if (type == 348)
            goto label_101;
          else
            goto label_103;
        }
        else
          goto label_60;
      }
      else
      {
        if (type <= 532)
        {
          switch (type - 464)
          {
            case 0:
            case 1:
            case 6:
              goto label_102;
            case 2:
            case 3:
            case 4:
            case 5:
            case 8:
            case 13:
            case 14:
            case 15:
            case 16:
            case 20:
            case 21:
            case 22:
            case 23:
            case 24:
            case 27:
            case 28:
            case 29:
            case 30:
            case 31:
            case 43:
            case 44:
            case 45:
            case 47:
            case 48:
              goto label_103;
            case 7:
              TimsConcoctionDrop(ItemDropRule.Common(2328, 1, 1, 12));
              goto label_103;
            case 9:
            case 10:
            case 11:
              TimsConcoctionDrop(ItemDropRule.Common(2345, 1, 1, 12));
              goto label_103;
            case 12:
              TimsConcoctionDrop(ItemDropRule.Common(678, 1, 1, 1));
              goto case 9;
            case 17:
              TimsConcoctionDrop(ItemDropRule.Common(2344, 1, 1, 3));
              goto label_103;
            case 18:
              TimsConcoctionDrop(ItemDropRule.Common(2346, 1, 1, 3));
              goto label_103;
            case 19:
              TimsConcoctionDrop(ItemDropRule.Common(292, 1, 1, 3));
              goto label_103;
            case 25:
              TimsConcoctionDrop(ItemDropRule.Common(289, 1, 1, 3));
              goto label_103;
            case 26:
              TimsConcoctionDrop(ItemDropRule.Common(2323, 1, 1, 3));
              goto label_103;
            case 32:
            case 33:
              goto label_59;
            case 34:
            case 35:
            case 36:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 42:
              TimsConcoctionDrop(ItemDropRule.Common(297, 1, 1, 3));
              goto label_103;
            case 46:
            case 49:
              TimsConcoctionDrop(ItemDropRule.Common(304, 1, 1, 6));
              goto label_103;
            default:
              if (type == 532)
                break;
              goto label_103;
          }
        }
        else if (type != 546)
        {
          if (type != 580)
          {
            if (type == 624)
            {
              TimsConcoctionDrop(ItemDropRule.Common(4477, 1, 1, 6));
              goto label_103;
            }
            else
              goto label_103;
          }
          else
            goto label_61;
        }
        TimsConcoctionDrop(ItemDropRule.Common(290, 1, 1, 6));
        goto label_103;
      }
label_52:
      TimsConcoctionDrop(ItemDropRule.Common(299, 1, 1, 1));
      goto label_103;
label_53:
      TimsConcoctionDrop(ItemDropRule.Common(2347, 1, 1, 2));
      goto label_103;
label_57:
      TimsConcoctionDrop(ItemDropRule.Common(2326, 1, 1, 1));
      goto label_103;
label_59:
      TimsConcoctionDrop(ItemDropRule.Common(301, 1, 1, 6));
      goto label_103;
label_60:
      TimsConcoctionDrop(ItemDropRule.Common(2353, 1, 1, 1));
      goto label_103;
label_61:
      TimsConcoctionDrop(ItemDropRule.Common(2325, 1, 1, 3));
      goto label_103;
label_62:
      TimsConcoctionDrop(ItemDropRule.Common(2329, 1, 1, 3));
      goto label_103;
label_101:
      TimsConcoctionDrop(ItemDropRule.Common(2756, 1, 1, 3));
      goto label_103;
label_102:
      TimsConcoctionDrop(ItemDropRule.Common(5211, 1, 1, 3));
label_103:
      int allowedRecursionDepth = 10;
      foreach (IItemDropRule dropRule in ((NPCLoot) ref npcLoot).Get(true))
        CheckMasterDropRule(dropRule);

      void TimsConcoctionDrop(IItemDropRule rule)
      {
        IItemDropRule iitemDropRule = (IItemDropRule) new LeadingConditionRule((IItemDropRuleCondition) new TimsConcoctionDropCondition());
        Chains.OnSuccess(iitemDropRule, rule, false);
        ((NPCLoot) ref npcLoot).Add(iitemDropRule);
      }

      void AddDrop(IItemDropRule dropRule)
      {
        if (npc.type == 125 || npc.type == 126)
        {
          LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.MissingTwin());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule, dropRule, false);
          ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
        }
        else if (npc.type == 14 || npc.type == 13 || npc.type == 15)
        {
          LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.LegacyHack_IsABoss());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule, dropRule, false);
          ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule);
        }
        else
          ((NPCLoot) ref npcLoot).Add(dropRule);
      }

      void CheckMasterDropRule(IItemDropRule dropRule)
      {
        if (--allowedRecursionDepth > 0)
        {
          if (dropRule != null && dropRule.ChainedRules != null)
          {
            foreach (IItemDropRuleChainAttempt chainedRule in dropRule.ChainedRules)
            {
              if (chainedRule != null && chainedRule.RuleToChain != null)
                CheckMasterDropRule(chainedRule.RuleToChain);
            }
          }
          if (dropRule is DropBasedOnMasterMode basedOnMasterMode && basedOnMasterMode != null && basedOnMasterMode.ruleForMasterMode != null)
            CheckMasterDropRule(basedOnMasterMode.ruleForMasterMode);
        }
        allowedRecursionDepth++;
        if (dropRule is ItemDropWithConditionRule withConditionRule && withConditionRule.condition is Terraria.GameContent.ItemDropRules.Conditions.IsMasterMode)
        {
          AddDrop(ItemDropRule.ByCondition((IItemDropRuleCondition) new EModeNotMasterDropCondition(), ((CommonDrop) withConditionRule).itemId, ((CommonDrop) withConditionRule).chanceDenominator, ((CommonDrop) withConditionRule).amountDroppedMinimum, ((CommonDrop) withConditionRule).amountDroppedMaximum, ((CommonDrop) withConditionRule).chanceNumerator));
        }
        else
        {
          if (!(dropRule is DropPerPlayerOnThePlayer playerOnThePlayer) || !(playerOnThePlayer.condition is Terraria.GameContent.ItemDropRules.Conditions.IsMasterMode))
            return;
          AddDrop(ItemDropRule.ByCondition((IItemDropRuleCondition) new EModeNotMasterDropCondition(), ((CommonDrop) playerOnThePlayer).itemId, ((CommonDrop) playerOnThePlayer).chanceDenominator, ((CommonDrop) playerOnThePlayer).amountDroppedMinimum, ((CommonDrop) playerOnThePlayer).amountDroppedMaximum, ((CommonDrop) playerOnThePlayer).chanceNumerator));
        }
      }
    }

    public virtual void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode || !this.BeetleOffenseAura)
        return;
      ref StatModifier local = ref modifiers.FinalDamage;
      local = StatModifier.op_Multiply(local, 1.25f);
    }

    public virtual void ModifyHitByItem(
      NPC npc,
      Player player,
      Item item,
      ref NPC.HitModifiers modifiers)
    {
      if (!WorldSavingSystem.EternityMode || !NPCID.Sets.CountsAsCritter[npc.type])
        return;
      player.AddBuff(ModContent.BuffType<GuiltyBuff>(), 300, true, false);
    }

    public virtual void ModifyHitByProjectile(
      NPC npc,
      Projectile projectile,
      ref NPC.HitModifiers modifiers)
    {
      Player player = Main.player[projectile.owner];
      if (!WorldSavingSystem.EternityMode || !NPCID.Sets.CountsAsCritter[npc.type] || !projectile.friendly || projectile.hostile || projectile.type == 12)
        return;
      player.AddBuff(ModContent.BuffType<GuiltyBuff>(), 300, true, false);
    }

    public virtual void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
      if (WorldSavingSystem.EternityMode)
      {
        if (this.BeetleDefenseAura)
        {
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 0.75f);
        }
        if (this.PaladinsShield)
        {
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 0.5f);
        }
        if (WorldSavingSystem.MasochistModeReal && (npc.boss || Luminance.Common.Utilities.Utilities.AnyBosses() && (double) ((Entity) npc).Distance(((Entity) Main.npc[FargoSoulsGlobalNPC.boss]).Center) < 3000.0))
        {
          ref StatModifier local = ref modifiers.FinalDamage;
          local = StatModifier.op_Multiply(local, 0.9f);
        }
      }
      base.ModifyIncomingHit(npc, ref modifiers);
    }

    public static bool StealFromInventory(Player target, ref Item item)
    {
      if (target.FargoSouls().StealingCooldown > 0 || item.IsAir)
        return false;
      target.FargoSouls().StealingCooldown = 900;
      target.AddBuff(ModContent.BuffType<ThiefCDBuff>(), 900, true, false);
      target.DropItem(((Entity) target).GetSource_DropAsItem("Stolen"), ((Entity) target).Center, ref item);
      return true;
    }

    public static void Horde(NPC npc, int size)
    {
      if (npc == null || !((Entity) npc).active)
        return;
      int num = 50;
      for (int index1 = 0; index1 < size; ++index1)
      {
        Vector2 vector2;
        // ISSUE: explicit constructor call
        ((Vector2) ref vector2).\u002Ector(((Entity) npc).Center.X + Utils.NextFloat(Main.rand, -2f, 2f) * (float) ((Entity) npc).width, ((Entity) npc).Center.Y);
        if (Collision.SolidCollision(vector2, ((Entity) npc).width, ((Entity) npc).height))
        {
          if (num > 0)
          {
            --num;
            --index1;
          }
        }
        else if (FargoSoulsUtil.HostCheck)
        {
          int index2 = NPC.NewNPC(((Entity) npc).GetSource_FromAI((string) null), (int) vector2.X + ((Entity) npc).width / 2, (int) vector2.Y + ((Entity) npc).height / 2, npc.type, 0, 0.0f, 0.0f, 0.0f, 0.0f, (int) byte.MaxValue);
          if (index2 != Main.maxNPCs)
          {
            NPC npc1 = Main.npc[index2];
            if (npc1 != null && ((Entity) npc1).active && npc1.type == npc.type)
            {
              ((Entity) npc1).velocity = Vector2.op_Multiply(Utils.RotatedByRandom(Vector2.UnitX, 2.0 * Math.PI), 5f);
              npc1.FargoSouls().CanHordeSplit = false;
              if (Main.netMode == 2)
                NetMessage.SendData(23, -1, -1, (NetworkText) null, index2, 0.0f, 0.0f, 0.0f, 0, 0, 0);
            }
          }
        }
      }
    }

    public static void Aura(
      NPC npc,
      float distance,
      int buff,
      bool reverse = false,
      int dustid = 228,
      Color color = default (Color))
    {
      EModeGlobalNPC.Aura(npc, distance, (reverse ? 1 : 0) != 0, dustid, color, buff);
    }

    public static void Aura(
      NPC npc,
      float distance,
      bool reverse = false,
      int dustid = -1,
      Color color = default (Color),
      params int[] buffs)
    {
      Player localPlayer = Main.LocalPlayer;
      if (dustid != -1)
        FargoSoulsUtil.AuraDust((Entity) npc, distance, dustid, color, reverse);
      if (buffs.Length == 0 || buffs[0] < 0)
        return;
      float num = ((Entity) npc).Distance(((Entity) localPlayer).Center);
      if (!((Entity) localPlayer).active || localPlayer.dead || localPlayer.ghost || (reverse ? ((double) num <= (double) distance ? 0 : ((double) num < (double) Math.Max(3000f, distance * 2f) ? 1 : 0)) : ((double) num < (double) distance ? 1 : 0)) == 0)
        return;
      foreach (int buff in buffs)
        FargoSoulsUtil.AddDebuffFixedDuration(localPlayer, buff, 2);
    }

    public static void CustomReflect(NPC npc, int dustID, int ratio = 1)
    {
      float distance = 32f;
      ((IEnumerable<Projectile>) Main.projectile).Where<Projectile>((Func<Projectile, bool>) (x => ((Entity) x).active && x.friendly && !FargoSoulsUtil.IsSummonDamage(x, false))).ToList<Projectile>().ForEach((Action<Projectile>) (x =>
      {
        if ((double) Vector2.Distance(((Entity) x).Center, ((Entity) npc).Center) > (double) distance)
          return;
        for (int index1 = 0; index1 < 5; ++index1)
        {
          int index2 = Dust.NewDust(new Vector2(((Entity) x).position.X, ((Entity) x).position.Y + 2f), ((Entity) x).width, ((Entity) x).height + 5, dustID, ((Entity) x).velocity.X * 0.2f, ((Entity) x).velocity.Y * 0.2f, 100, new Color(), 1.5f);
          Main.dust[index2].noGravity = true;
        }
        x.hostile = true;
        x.friendly = false;
        x.owner = Main.myPlayer;
        x.damage /= ratio;
        Projectile projectile = x;
        ((Entity) projectile).velocity = Vector2.op_Multiply(((Entity) projectile).velocity, -1f);
        if ((double) ((Entity) x).Center.X > (double) ((Entity) npc).Center.X * 0.5)
        {
          ((Entity) x).direction = 1;
          x.spriteDirection = 1;
        }
        else
        {
          ((Entity) x).direction = -1;
          x.spriteDirection = -1;
        }
      }));
    }
  }
}
