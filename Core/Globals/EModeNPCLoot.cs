// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.EModeNPCLoot
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Pets;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  public class EModeNPCLoot : GlobalNPC
  {
    private static List<int> EvilCritters;
    private static List<int> Mimics;
    private static List<int> HardmodeDesertEnemies;
    private static List<int> EarlyBirdEnemies;
    private static List<int> Hornets;
    private static List<int> MushroomEnemies;

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      LeadingConditionRule leadingConditionRule1 = new LeadingConditionRule((IItemDropRuleCondition) new EModeDropCondition());
      int type = npc.type;
      switch (type)
      {
        case 4:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<AgitatingLens>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(2335, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(75, 5), false);
          break;
        case 13:
        case 14:
        case 15:
          LeadingConditionRule leadingConditionRule2 = new LeadingConditionRule((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.LegacyHack_IsABoss());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, (IItemDropRule) leadingConditionRule2, false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule2, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<DarkenedHeart>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule2, FargoSoulsUtil.BossBagDropCustom(3203, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule2, ItemDropRule.Common(86, 1, 60, 60), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule2, ItemDropRule.Common(56, 1, 200, 200), false);
          break;
        case 35:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<NecromanticBrew>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3205, 5), false);
          break;
        case 49:
        case 93:
          FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<RabiesShot>(), 5, 1, 1));
          break;
        case 50:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<SlimyShield>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(2334, 5), false);
          break;
        case 68:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(ModContent.ItemType<SinisterIcon>(), 1, 1, 1), false);
          break;
        case 109:
          FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3548, 1, 1, 10));
          break;
        case 113:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<PungentEyeball>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<MutantsDiscountCard>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3986, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(4878, 5), false);
          break;
        case 125:
        case 126:
          LeadingConditionRule leadingConditionRule3 = new LeadingConditionRule((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.MissingTwin());
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, (IItemDropRule) leadingConditionRule3, false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule3, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<FusedLens>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule3, FargoSoulsUtil.BossBagDropCustom(3980, 5), false);
          break;
        case (int) sbyte.MaxValue:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<ReinforcedPlating>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3980, 5), false);
          break;
        case 134:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<GroundStick>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3980, 5), false);
          break;
        case 222:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<QueenStinger>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3208, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3093, 5), false);
          break;
        case 245:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<LihzahrdTreasureBox>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3981, 5), false);
          break;
        case 262:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<MagicalBulb>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3987, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(1291, 3), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(947, 200), false);
          break;
        case 266:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<GuttedHeart>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3204, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(1329, 1, 60, 60), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(880, 1, 200, 200), false);
          break;
        case 327:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(ModContent.ItemType<PumpkingsCape>(), 5, 1, 1), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(1774, 1, 1, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(1825, 10, 1, 1), false);
          break;
        case 345:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(ModContent.ItemType<IceQueensCrown>(), 5, 1, 1), false);
          FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(1869, 1, 1, 5));
          break;
        case 370:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<MutantAntibodies>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<MutantsCreditCard>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(5003, 5), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.OneFromOptions(1, new int[18]
          {
            2428,
            2367,
            2368,
            2369,
            2294,
            3183,
            2360,
            2373,
            2374,
            2375,
            3120,
            3037,
            3096,
            2494,
            3031,
            3032,
            2422,
            1315
          }), false);
          break;
        case 392:
        case 395:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, ItemDropRule.Common(ModContent.ItemType<SaucerControlConsole>(), 5, 1, 1), false);
          break;
        case 398:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<GalacticGlobe>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3460, 150), false);
          break;
        case 439:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<CelestialRune>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<MutantsPact>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3984, 5), false);
          break;
        case 551:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<BetsysHeart>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3981, 5), false);
          break;
        case 618:
          FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<DreadShell>(), 5, 1, 1));
          break;
        case 636:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<PrecisionSeal>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3986, 5), false);
          break;
        case 657:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<GelicWings>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(3986, 5), false);
          break;
        case 668:
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<Deerclawps>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(ModContent.ItemType<DeerSinew>()), false);
          Chains.OnSuccess((IItemDropRule) leadingConditionRule1, FargoSoulsUtil.BossBagDropCustom(4405, 5), false);
          break;
        default:
          if (!EModeNPCLoot.EvilCritters.Contains(npc.type))
          {
            switch (type)
            {
              case 10:
              case 95:
                FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(69, 1, 3, 9));
                break;
              case 586:
              case 587:
                FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2423, 10, 1, 1));
                FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3225, 10, 1, 1));
                break;
              default:
                if (!EModeNPCLoot.Mimics.Contains(npc.type))
                {
                  switch (type)
                  {
                    case 45:
                      ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new EModeDropCondition(), ModContent.ItemType<TimsConcoction>(), 5, 1, 1, 1));
                      break;
                    case 163:
                    case 238:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(1798, 50, 1, 1));
                      break;
                    case 172:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<MysticSkull>(), 5, 1, 1));
                      break;
                    case 195:
                    case 196:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<NymphsPerfume>(), 5, 1, 1));
                      break;
                    case 510:
                    case 511:
                    case 512:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(4407, 1, 1, 1));
                      break;
                    case 580:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new DownedEvilBossDropCondition(), 889, 50, 1, 1, 1));
                      break;
                    default:
                      if (!EModeNPCLoot.HardmodeDesertEnemies.Contains(npc.type))
                      {
                        switch (type)
                        {
                          case 32:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(165, 50, 1, 1));
                            break;
                          case 52:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<SkullCharm>(), 10, 1, 1));
                            break;
                          case 59:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(906, 100, 1, 1));
                            break;
                          case 75:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(4961, 100, 1, 1));
                            break;
                          case 177:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(893, 50, 1, 1));
                            break;
                          case 244:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<ConcentratedRainbowMatter>(), 10, 1, 1));
                            break;
                          case 344:
                          case 346:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(1869, 1, 1, 5));
                            break;
                          case 471:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<WretchedPouch>(), 5, 1, 1));
                            break;
                          case 541:
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(857, 20, 1, 1));
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(4407, 1, 1, 1));
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), 4408, 1, 1, 1, 1));
                            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), ModContent.ItemType<SandsofTime>(), 5, 1, 1, 1));
                            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsPreHardmode(), ModContent.ItemType<SandsofTime>(), 1, 1, 1, 1));
                            break;
                          default:
                            if (!EModeNPCLoot.Hornets.Contains(npc.type))
                            {
                              switch (type)
                              {
                                case 58:
                                  FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(885, 50, 1, 1));
                                  break;
                                case 175:
                                  FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(210, 2, 1, 1));
                                  break;
                                case 380:
                                  FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.Find<ModItem>("Fargowiltas", "CultistSummon").Type, 100, 1, 1));
                                  break;
                                case 381:
                                  FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2771, 100, 1, 1));
                                  break;
                                default:
                                  if (!EModeNPCLoot.MushroomEnemies.Contains(npc.type))
                                  {
                                    switch (type)
                                    {
                                      case 65:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<HokeyBall>(), 100, 1, 1));
                                        break;
                                      case 67:
                                      case 220:
                                      case 221:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2626, 10, 1, 3));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2625, 10, 1, 3));
                                        break;
                                      case 87:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(53, 20, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3206, 1, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), 3985, 1, 1, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), ModContent.ItemType<WyvernFeather>(), 5, 1, 1, 1));
                                        FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<WyvernFeather>(), 1, 1, 1));
                                        break;
                                      case 143:
                                      case 144:
                                      case 145:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<OrdinaryCarrot>(), 50, 1, 1));
                                        break;
                                      case 216:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.Find<ModItem>("Fargowiltas", "GoldenDippingVat").Type, 15, 1, 1));
                                        break;
                                      case 243:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(987, 20, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(4405, 1, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), 4406, 1, 1, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.ByCondition((IItemDropRuleCondition) new Terraria.GameContent.ItemDropRules.Conditions.IsHardmode(), ModContent.ItemType<FrigidGemstone>(), 5, 1, 1, 1));
                                        FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<FrigidGemstone>(), 1, 1, 1));
                                        FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(602, 1, 1, 1));
                                        break;
                                      case 325:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(1774, 1, 1, 5));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(1825, 10, 1, 1));
                                        break;
                                      case 492:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<SecurityWallet>(), 5, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(905, 50, 1, 1));
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(855, 50, 1, 1));
                                        break;
                                      case 634:
                                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(4764, 10, 1, 1));
                                        break;
                                    }
                                  }
                                  else
                                  {
                                    FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(183, 1, 1, 5));
                                    FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(194, 5, 1, 1));
                                    FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2673, 20, 1, 1));
                                    break;
                                  }
                                  break;
                              }
                            }
                            else
                            {
                              if (npc.type == 176)
                                FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(209, 2, 1, 1));
                              FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(195, 10, 1, 1));
                              break;
                            }
                            break;
                        }
                      }
                      else
                      {
                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3347, 3, 1, 10));
                        FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(934, 100, 1, 1));
                        if (npc.type == 532)
                        {
                          FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3781, 50, 1, 1));
                          break;
                        }
                        break;
                      }
                      break;
                  }
                }
                else
                {
                  FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(2336, 1, 1, 1));
                  switch (npc.type)
                  {
                    case 473:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3982, 1, 1, 1));
                      break;
                    case 474:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3983, 1, 1, 1));
                      break;
                    case 475:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3986, 1, 1, 1));
                      break;
                    case 476:
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(3987, 1, 1, 1));
                      FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<TribalCharm>(), 5, 1, 1));
                      break;
                  }
                }
                break;
            }
          }
          else
          {
            FargoSoulsUtil.EModeDrop(npcLoot, ItemDropRule.Common(ModContent.ItemType<SqueakyToy>(), 10, 1, 1));
            break;
          }
          break;
      }
      if (EModeNPCLoot.EarlyBirdEnemies.Contains(npc.type))
      {
        switch (npc.type)
        {
          case 85:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is OneFromOptionsDropRule fromOptionsDropRule && ((IEnumerable<int>) fromOptionsDropRule.dropIds).Contains<int>(437) && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.OneFromOptions(1, new int[4]
            {
              536,
              535,
              554,
              437
            }));
            break;
          case 87:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is DropBasedOnExpertMode basedOnExpertMode && basedOnExpertMode.ruleForNormalMode is CommonDrop ruleForNormalMode && ruleForNormalMode.itemId == 575 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            break;
          case 98:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop1 && commonDrop1.itemId == 522 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.OneFromOptions(1, new int[5]
            {
              162,
              111,
              96,
              115,
              64
            }));
            break;
          case 156:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop2 && commonDrop2.itemId == 683 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(272, 3, 1, 1));
            break;
          case 170:
          case 171:
          case 180:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is ItemDropWithConditionRule withConditionRule1 && withConditionRule1.condition is Terraria.GameContent.ItemDropRules.Conditions.DontStarveIsUp && ((CommonDrop) withConditionRule1).itemId == 5096 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is ItemDropWithConditionRule withConditionRule2 && withConditionRule2.condition is Terraria.GameContent.ItemDropRules.Conditions.DontStarveIsNotUp && ((CommonDrop) withConditionRule2).itemId == 5096 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            break;
          case 250:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop3 && commonDrop3.itemId == 1244 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(3206, 1, 1, 1));
            break;
          case 268:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop4 && commonDrop4.itemId == 1332 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.OneFromOptions(1, new int[5]
            {
              800,
              802,
              1256,
              3062,
              1290
            }));
            break;
          case 480:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop5 && commonDrop5.itemId == 3269 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            break;
          case 510:
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(857, 3, 1, 1));
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.Common(4407, 1, 1, 1));
            break;
          case 629:
            ((NPCLoot) ref npcLoot).RemoveWhere((Predicate<IItemDropRule>) (rule => rule is CommonDrop commonDrop6 && commonDrop6.itemId == 1312 && FargoSoulsUtil.LockEarlyBirdDrop(npcLoot, rule)), true);
            FargoSoulsUtil.AddEarlyBirdDrop(npcLoot, ItemDropRule.OneFromOptions(1, new int[4]
            {
              536,
              535,
              554,
              437
            }));
            break;
        }
      }
      ((NPCLoot) ref npcLoot).Add((IItemDropRule) leadingConditionRule1);
    }

    static EModeNPCLoot()
    {
      List<int> intList1 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList1, 6);
      Span<int> span1 = CollectionsMarshal.AsSpan<int>(intList1);
      int num1 = 0;
      span1[num1] = 47;
      int num2 = num1 + 1;
      span1[num2] = 464;
      int num3 = num2 + 1;
      span1[num3] = 57;
      int num4 = num3 + 1;
      span1[num4] = 465;
      int num5 = num4 + 1;
      span1[num5] = 168;
      int num6 = num5 + 1;
      span1[num6] = 470;
      int num7 = num6 + 1;
      EModeNPCLoot.EvilCritters = intList1;
      List<int> intList2 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList2, 7);
      Span<int> span2 = CollectionsMarshal.AsSpan<int>(intList2);
      int num8 = 0;
      span2[num8] = 85;
      int num9 = num8 + 1;
      span2[num9] = 341;
      int num10 = num9 + 1;
      span2[num10] = 629;
      int num11 = num10 + 1;
      span2[num11] = 473;
      int num12 = num11 + 1;
      span2[num12] = 474;
      int num13 = num12 + 1;
      span2[num13] = 475;
      int num14 = num13 + 1;
      span2[num14] = 476;
      num7 = num14 + 1;
      EModeNPCLoot.Mimics = intList2;
      List<int> intList3 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList3, 10);
      Span<int> span3 = CollectionsMarshal.AsSpan<int>(intList3);
      int num15 = 0;
      span3[num15] = 532;
      int num16 = num15 + 1;
      span3[num16] = 530;
      int num17 = num16 + 1;
      span3[num17] = 531;
      int num18 = num17 + 1;
      span3[num18] = 529;
      int num19 = num18 + 1;
      span3[num19] = 528;
      int num20 = num19 + 1;
      span3[num20] = 524;
      int num21 = num20 + 1;
      span3[num21] = 525;
      int num22 = num21 + 1;
      span3[num22] = 526;
      int num23 = num22 + 1;
      span3[num23] = 527;
      int num24 = num23 + 1;
      span3[num24] = 533;
      num7 = num24 + 1;
      EModeNPCLoot.HardmodeDesertEnemies = intList3;
      List<int> intList4 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList4, 22);
      Span<int> span4 = CollectionsMarshal.AsSpan<int>(intList4);
      int num25 = 0;
      span4[num25] = 87;
      int num26 = num25 + 1;
      span4[num26] = 89;
      int num27 = num26 + 1;
      span4[num27] = 90;
      int num28 = num27 + 1;
      span4[num28] = 91;
      int num29 = num28 + 1;
      span4[num29] = 88;
      int num30 = num29 + 1;
      span4[num30] = 92;
      int num31 = num30 + 1;
      span4[num31] = 85;
      int num32 = num31 + 1;
      span4[num32] = 629;
      int num33 = num32 + 1;
      span4[num33] = 480;
      int num34 = num33 + 1;
      span4[num34] = 170;
      int num35 = num34 + 1;
      span4[num35] = 180;
      int num36 = num35 + 1;
      span4[num36] = 171;
      int num37 = num36 + 1;
      span4[num37] = 268;
      int num38 = num37 + 1;
      span4[num38] = 98;
      int num39 = num38 + 1;
      span4[num39] = 250;
      int num40 = num39 + 1;
      span4[num40] = 156;
      int num41 = num40 + 1;
      span4[num41] = 258;
      int num42 = num41 + 1;
      span4[num42] = 257;
      int num43 = num42 + 1;
      span4[num43] = 254;
      int num44 = num43 + 1;
      span4[num44] = (int) byte.MaxValue;
      int num45 = num44 + 1;
      span4[num45] = 243;
      int num46 = num45 + 1;
      span4[num46] = 541;
      num7 = num46 + 1;
      EModeNPCLoot.EarlyBirdEnemies = intList4;
      List<int> intList5 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList5, 21);
      Span<int> span5 = CollectionsMarshal.AsSpan<int>(intList5);
      int num47 = 0;
      span5[num47] = 42;
      int num48 = num47 + 1;
      span5[num48] = 231;
      int num49 = num48 + 1;
      span5[num49] = 232;
      int num50 = num49 + 1;
      span5[num50] = 233;
      int num51 = num50 + 1;
      span5[num51] = 234;
      int num52 = num51 + 1;
      span5[num52] = 235;
      int num53 = num52 + 1;
      span5[num53] = -57;
      int num54 = num53 + 1;
      span5[num54] = -59;
      int num55 = num54 + 1;
      span5[num55] = -61;
      int num56 = num55 + 1;
      span5[num56] = -63;
      int num57 = num56 + 1;
      span5[num57] = -65;
      int num58 = num57 + 1;
      span5[num58] = -20;
      int num59 = num58 + 1;
      span5[num59] = -21;
      int num60 = num59 + 1;
      span5[num60] = -56;
      int num61 = num60 + 1;
      span5[num61] = -58;
      int num62 = num61 + 1;
      span5[num62] = -60;
      int num63 = num62 + 1;
      span5[num63] = -62;
      int num64 = num63 + 1;
      span5[num64] = -64;
      int num65 = num64 + 1;
      span5[num65] = -19;
      int num66 = num65 + 1;
      span5[num66] = 176;
      int num67 = num66 + 1;
      span5[num67] = -18;
      num7 = num67 + 1;
      EModeNPCLoot.Hornets = intList5;
      List<int> intList6 = new List<int>();
      CollectionsMarshal.SetCount<int>(intList6, 9);
      Span<int> span6 = CollectionsMarshal.AsSpan<int>(intList6);
      int num68 = 0;
      span6[num68] = 259;
      int num69 = num68 + 1;
      span6[num69] = 260;
      int num70 = num69 + 1;
      span6[num70] = 257;
      int num71 = num70 + 1;
      span6[num71] = 258;
      int num72 = num71 + 1;
      span6[num72] = 634;
      int num73 = num72 + 1;
      span6[num73] = 254;
      int num74 = num73 + 1;
      span6[num74] = (int) byte.MaxValue;
      int num75 = num74 + 1;
      span6[num75] = 635;
      int num76 = num75 + 1;
      span6[num76] = 256;
      num7 = num76 + 1;
      EModeNPCLoot.MushroomEnemies = intList6;
    }
  }
}
