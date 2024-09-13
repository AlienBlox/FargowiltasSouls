// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Misc.EternityAdvisor
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Essences;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Content.Items.Accessories.Souls;
using FargowiltasSouls.Content.Items.Armor;
using FargowiltasSouls.Content.Items.Consumables;
using FargowiltasSouls.Content.Items.Summons;
using FargowiltasSouls.Core.Systems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Misc
{
  public class EternityAdvisor : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 1;
      this.Item.maxStack = 1;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.useStyle = 4;
      this.Item.consumable = false;
      this.Item.value = Item.buyPrice(0, 0, 1, 0);
    }

    public virtual bool CanUseItem(Player player) => WorldSavingSystem.EternityMode;

    private static string GetBuildText(params int[] args)
    {
      string buildText = "";
      foreach (int num in args)
      {
        if (num != -1)
        {
          string str = buildText;
          DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
          interpolatedStringHandler.AppendLiteral("[i:");
          interpolatedStringHandler.AppendFormatted<int>(num);
          interpolatedStringHandler.AppendLiteral("]");
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          buildText = str + stringAndClear;
        }
      }
      return buildText;
    }

    private static string GetBuildTextRandom(params int[] args)
    {
      List<int> intList = new List<int>();
      int num = args.Length - 1;
      for (int index1 = 0; index1 < args[0]; ++index1)
      {
        int index2 = Main.rand.Next(num) + 1;
        if (intList.Contains(args[index2]) || args[index2] == -1)
        {
          for (int index3 = 0; index3 < num; ++index3)
          {
            if (++index2 >= num)
              index2 = 1;
            if (!intList.Contains(args[index2]))
              break;
          }
        }
        intList.Add(args[index2]);
      }
      return EternityAdvisor.GetBuildText(intList.ToArray());
    }

    private int GetBossHelp(out string build, Player player)
    {
      build = "";
      string str1 = string.Empty;
      int[] melee = (int[]) null;
      int[] ranged = (int[]) null;
      int[] magic = (int[]) null;
      int[] summoner = (int[]) null;
      int bossHelp;
      DefaultInterpolatedStringHandler interpolatedStringHandler;
      if (!WorldSavingSystem.DownedBoss[9])
      {
        bossHelp = ModContent.ItemType<SquirrelCoatofArms>();
        build += EternityAdvisor.GetBuildText(ModContent.ItemType<EurusSock>(), ModContent.ItemType<PuffInABottle>(), ModContent.ItemType<BorealWoodEnchant>(), ModContent.ItemType<CactusEnchant>());
      }
      else if (!NPC.downedSlimeKing)
      {
        bossHelp = 560;
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<EurusSock>(), ModContent.ItemType<PuffInABottle>()) + EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<EbonwoodEnchant>(), ModContent.ItemType<BorealWoodEnchant>(), ModContent.ItemType<PumpkinEnchant>(), ModContent.ItemType<ShadewoodEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<CactusEnchant>());
      }
      else if (!NPC.downedBoss1)
      {
        bossHelp = 43;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[5]
        {
          54,
          3200,
          1579,
          128,
          405
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          399,
          3241,
          983,
          1163
        })) + EternityAdvisor.GetBuildTextRandom(3, 860, ModContent.ItemType<NinjaEnchant>(), ModContent.ItemType<LeadEnchant>(), ModContent.ItemType<BorealWoodEnchant>(), ModContent.ItemType<ShadewoodEnchant>(), ModContent.ItemType<CactusEnchant>(), ModContent.ItemType<PalmWoodEnchant>(), ModContent.ItemType<TungstenEnchant>());
      }
      else if (!NPC.downedBoss2)
      {
        bossHelp = WorldGen.crimson ? 1331 : 70;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[3]
        {
          405,
          898,
          1862
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          3250,
          3252,
          1251
        }), Utils.Next<int>(Main.rand, new int[2]
        {
          3097,
          ModContent.ItemType<JungleEnchant>()
        })) + EternityAdvisor.GetBuildTextRandom(2, ModContent.ItemType<LeadEnchant>(), ModContent.ItemType<EbonwoodEnchant>(), ModContent.ItemType<CactusEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<CopperEnchant>());
      }
      else if (!NPC.downedQueenBee)
      {
        bossHelp = 1133;
        ref string local = ref build;
        string str2 = build;
        int[] numArray = new int[5]
        {
          Utils.NextBool(Main.rand) ? 1862 : 898,
          0,
          0,
          0,
          0
        };
        numArray[1] = Utils.Next<int>(Main.rand, new int[3]
        {
          3097,
          ModContent.ItemType<JungleEnchant>(),
          ModContent.ItemType<MeteorEnchant>()
        });
        numArray[2] = 887;
        numArray[3] = Utils.Next<int>(Main.rand, new int[3]
        {
          3250,
          3252,
          1251
        });
        numArray[4] = Utils.Next<int>(Main.rand, new int[5]
        {
          ModContent.ItemType<RainEnchant>(),
          ModContent.ItemType<TungstenEnchant>(),
          ModContent.ItemType<ShadowEnchant>(),
          ModContent.ItemType<ShadewoodEnchant>(),
          ModContent.ItemType<NinjaEnchant>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string str3 = str2 + buildText;
        local = str3;
        string str4 = str1;
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.Find<ModItem>("Fargowiltas", "CityBuster").Type);
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str4 + stringAndClear;
      }
      else if (!NPC.downedBoss3)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "SuspiciousSkull", ref modItem) ? modItem.Type : 1281;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          1862,
          5000
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          3097,
          ModContent.ItemType<JungleEnchant>(),
          ModContent.ItemType<QueenStinger>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          1164,
          5331,
          ModContent.ItemType<BeeEnchant>()
        })) + EternityAdvisor.GetBuildTextRandom(2, ModContent.ItemType<SkullCharm>(), ModContent.ItemType<ShadowEnchant>(), ModContent.ItemType<TinEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<NinjaEnchant>(), ModContent.ItemType<CrimsonEnchant>());
      }
      else if (!NPC.downedDeerclops)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "DeerThing2", ref modItem) ? modItem.Type : 5120;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          1862,
          5000
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          3097,
          ModContent.ItemType<JungleEnchant>(),
          ModContent.ItemType<QueenStinger>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          1164,
          5331,
          ModContent.ItemType<BeeEnchant>()
        })) + EternityAdvisor.GetBuildTextRandom(2, 1921, ModContent.ItemType<EbonwoodEnchant>(), ModContent.ItemType<ShadowEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<TinEnchant>(), ModContent.ItemType<NinjaEnchant>());
      }
      else if (!WorldSavingSystem.DownedDevi)
      {
        bossHelp = ModContent.ItemType<DevisCurse>();
        build += EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          1862,
          5000
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          3097,
          ModContent.ItemType<JungleEnchant>(),
          ModContent.ItemType<QueenStinger>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          1164,
          5331,
          ModContent.ItemType<BeeEnchant>()
        }), Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<NymphsPerfume>(),
          ModContent.ItemType<ShadowEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          ModContent.ItemType<TinEnchant>(),
          ModContent.ItemType<NinjaEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        }));
      }
      else if (!Main.hardMode)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "FleshyDoll", ref modItem) ? modItem.Type : 267;
        ref string local = ref build;
        string str5 = build;
        int[] numArray = new int[2]
        {
          ModContent.ItemType<ZephyrBoots>(),
          0
        };
        numArray[1] = Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<SupremeDeathbringerFairy>(),
          ModContent.ItemType<SparklingAdoration>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<TinEnchant>(), ModContent.ItemType<SkullCharm>(), ModContent.ItemType<CopperEnchant>(), ModContent.ItemType<NinjaEnchant>());
        string str6 = str5 + buildText + buildTextRandom;
        local = str6;
        string str7 = str1;
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.Find<ModItem>("Fargowiltas", "DoubleObsidianInstabridge").Type);
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str7 + stringAndClear;
        melee = new int[1]
        {
          ModContent.ItemType<TungstenEnchant>()
        };
      }
      else if (!NPC.downedQueenSlime)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "JellyCrystal", ref modItem) ? modItem.Type : 4988;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<ZephyrBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          822,
          493,
          ModContent.ItemType<BeeEnchant>()
        }), Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<SupremeDeathbringerFairy>(),
          ModContent.ItemType<SparklingAdoration>()
        })) + EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<PalladiumEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          490,
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]{ 491 };
        magic = new int[1]{ 489 };
        summoner = new int[2]{ 2998, 1158 };
      }
      else if (!WorldSavingSystem.downedBoss[12])
      {
        bossHelp = ModContent.ItemType<MechLure>();
        ref string local = ref build;
        string str8 = build;
        int[] numArray = new int[3]
        {
          ModContent.ItemType<ZephyrBoots>(),
          0,
          0
        };
        numArray[1] = Utils.Next<int>(Main.rand, new int[4]
        {
          822,
          493,
          ModContent.ItemType<GelicWings>(),
          ModContent.ItemType<BeeEnchant>()
        });
        numArray[2] = Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<CrystalAssassinEnchant>(),
          ModContent.ItemType<MeteorEnchant>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<SupremeDeathbringerFairy>(), ModContent.ItemType<SparklingAdoration>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<PalladiumEnchant>(), ModContent.ItemType<PearlwoodEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<FrostEnchant>());
        string str9 = str8 + buildText + buildTextRandom;
        local = str9;
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          490,
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]{ 491 };
        magic = new int[1]{ 489 };
        summoner = new int[2]{ 2998, 1158 };
      }
      else if (!NPC.downedMechBoss1)
      {
        bossHelp = 556;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<ZephyrBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[2]
        {
          822,
          ModContent.ItemType<GelicWings>()
        })) + EternityAdvisor.GetBuildTextRandom(4, ModContent.ItemType<SupremeDeathbringerFairy>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<PalladiumEnchant>(), ModContent.ItemType<PearlwoodEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<FrostEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          490,
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]{ 491 };
        magic = new int[1]{ 489 };
        summoner = new int[2]{ 2998, 1158 };
      }
      else if (!NPC.downedMechBoss2)
      {
        bossHelp = 544;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<ZephyrBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          821,
          822,
          ModContent.ItemType<GelicWings>()
        })) + EternityAdvisor.GetBuildTextRandom(4, ModContent.ItemType<SupremeDeathbringerFairy>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<PalladiumEnchant>(), ModContent.ItemType<PearlwoodEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<FrostEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BarbariansEssence>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SharpshootersEssence>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ApprenticesEssence>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<OccultistsEssence>(),
          ModContent.ItemType<AncientHallowEnchant>()
        };
      }
      else if (!NPC.downedMechBoss3)
      {
        bossHelp = 557;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<ZephyrBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[3]
        {
          821,
          822,
          ModContent.ItemType<GelicWings>()
        })) + EternityAdvisor.GetBuildTextRandom(4, ModContent.ItemType<SupremeDeathbringerFairy>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<TungstenEnchant>(), ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<PalladiumEnchant>(), ModContent.ItemType<PearlwoodEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<FrostEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BarbariansEssence>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SharpshootersEssence>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ApprenticesEssence>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<OccultistsEssence>(),
          ModContent.ItemType<AncientHallowEnchant>()
        };
      }
      else if (!WorldSavingSystem.downedBoss[10])
      {
        bossHelp = ModContent.ItemType<FragilePixieLamp>();
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<AeolusBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          821,
          822,
          1515,
          ModContent.ItemType<GelicWings>()
        })) + EternityAdvisor.GetBuildTextRandom(4, ModContent.ItemType<ChlorophyteEnchant>(), ModContent.ItemType<SquireEnchant>(), ModContent.ItemType<SupremeDeathbringerFairy>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<HallowEnchant>(), ModContent.ItemType<MythrilEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<DubiousCircuitry>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BarbariansEssence>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SharpshootersEssence>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ApprenticesEssence>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<OccultistsEssence>(),
          ModContent.ItemType<AncientHallowEnchant>()
        };
      }
      else if (!NPC.downedPlantBoss)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "PlanterasFruit", ref modItem) ? modItem.Type : 2109;
        build = build + EternityAdvisor.GetBuildText(Utils.Next<int>(Main.rand, new int[2]
        {
          ModContent.ItemType<AeolusBoots>(),
          ModContent.ItemType<MeteorEnchant>()
        }), Utils.Next<int>(Main.rand, new int[4]
        {
          821,
          822,
          1515,
          ModContent.ItemType<GelicWings>()
        })) + EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<ForbiddenEnchant>(), ModContent.ItemType<ChlorophyteEnchant>(), ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<AncientShadowEnchant>(), ModContent.ItemType<OrichalcumEnchant>(), ModContent.ItemType<ApprenticeEnchant>(), ModContent.ItemType<HallowEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<TitaniumEnchant>());
        string str10 = str1 + EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.Find<ModItem>("Fargowiltas", "CityBuster").Type);
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str10 + stringAndClear;
        melee = new int[1]
        {
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[1]
        {
          ModContent.ItemType<AncientHallowEnchant>()
        };
      }
      else if (!NPC.downedGolemBoss)
      {
        bossHelp = 1293;
        ref string local = ref build;
        string str11 = build;
        int[] numArray = new int[2]
        {
          ModContent.ItemType<AeolusBoots>(),
          0
        };
        numArray[1] = Utils.Next<int>(Main.rand, new int[1]
        {
          1830
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(3, Utils.Next<int>(Main.rand, new int[4]
        {
          984,
          ModContent.ItemType<MonkEnchant>(),
          ModContent.ItemType<ChlorophyteEnchant>(),
          ModContent.ItemType<MeteorEnchant>()
        }), ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<CrimsonEnchant>(), ModContent.ItemType<HallowEnchant>(), ModContent.ItemType<AncientHallowEnchant>(), ModContent.ItemType<ForbiddenEnchant>(), ModContent.ItemType<AdamantiteEnchant>(), ModContent.ItemType<LumpOfFlesh>());
        string str12 = str11 + buildText + buildTextRandom;
        local = str12;
        string str13 = str1 + EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(ModContent.Find<ModItem>("Fargowiltas", "LihzahrdInstactuationBomb").Type);
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str13 + stringAndClear;
        melee = new int[2]
        {
          ModContent.ItemType<BarbariansEssence>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SharpshootersEssence>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ApprenticesEssence>()
        };
        summoner = new int[3]
        {
          ModContent.ItemType<OccultistsEssence>(),
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!WorldSavingSystem.DownedBetsy)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "BetsyEgg", ref modItem) ? modItem.Type : 3863;
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<AeolusBoots>(), 2280, ModContent.ItemType<LihzahrdTreasureBox>()) + EternityAdvisor.GetBuildTextRandom(3, Utils.Next<int>(Main.rand, new int[4]
        {
          984,
          ModContent.ItemType<MonkEnchant>(),
          ModContent.ItemType<ChlorophyteEnchant>(),
          ModContent.ItemType<MeteorEnchant>()
        }), ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<LumpOfFlesh>(), ModContent.ItemType<CrimsonEnchant>(), ModContent.ItemType<HallowEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BeetleEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!NPC.downedFishron)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "TruffleWorm2", ref modItem) ? modItem.Type : 2673;
        ref string local = ref build;
        string str14 = build;
        int[] numArray = new int[3]
        {
          Utils.NextBool(Main.rand) ? ModContent.ItemType<AeolusBoots>() : ModContent.ItemType<ValhallaKnightEnchant>(),
          3883,
          0
        };
        numArray[2] = Utils.Next<int>(Main.rand, new int[4]
        {
          ModContent.ItemType<SupremeDeathbringerFairy>(),
          ModContent.ItemType<LihzahrdTreasureBox>(),
          ModContent.ItemType<BetsysHeart>(),
          ModContent.ItemType<MeteorEnchant>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<ForbiddenEnchant>(), ModContent.ItemType<DarkArtistEnchant>(), ModContent.ItemType<LumpOfFlesh>(), ModContent.ItemType<PumpkingsCape>());
        string str15 = str14 + buildText + buildTextRandom;
        local = str15;
        str1 = str1 + EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>()) + EternityAdvisor.GetBuildText(ModContent.ItemType<RabiesVaccine>());
        melee = new int[2]
        {
          ModContent.ItemType<BeetleEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!NPC.downedEmpressOfLight)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "PrismaticPrimrose", ref modItem) ? modItem.Type : 4961;
        ref string local = ref build;
        string str16 = build;
        int[] numArray = new int[3]
        {
          Utils.NextBool(Main.rand) ? ModContent.ItemType<AeolusBoots>() : ModContent.ItemType<ValhallaKnightEnchant>(),
          0,
          0
        };
        numArray[1] = Utils.Next<int>(Main.rand, new int[2]
        {
          3883,
          2609
        });
        numArray[2] = Utils.Next<int>(Main.rand, new int[4]
        {
          ModContent.ItemType<SupremeDeathbringerFairy>(),
          ModContent.ItemType<LihzahrdTreasureBox>(),
          ModContent.ItemType<BetsysHeart>(),
          ModContent.ItemType<MeteorEnchant>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<ForbiddenEnchant>(), ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<DarkArtistEnchant>(), ModContent.ItemType<SpectreEnchant>(), ModContent.ItemType<RainEnchant>());
        string str17 = str16 + buildText + buildTextRandom;
        local = str17;
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BeetleEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!NPC.downedAncientCultist)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "CultistSummon", ref modItem) ? modItem.Type : 3372;
        ref string local = ref build;
        string str18 = build;
        int[] numArray = new int[4]
        {
          Utils.NextBool(Main.rand) ? ModContent.ItemType<AeolusBoots>() : ModContent.ItemType<ValhallaKnightEnchant>(),
          Utils.NextBool(Main.rand) ? 3883 : 2609,
          4989,
          0
        };
        numArray[3] = Utils.Next<int>(Main.rand, new int[4]
        {
          ModContent.ItemType<SupremeDeathbringerFairy>(),
          ModContent.ItemType<LihzahrdTreasureBox>(),
          ModContent.ItemType<BetsysHeart>(),
          ModContent.ItemType<MeteorEnchant>()
        });
        string buildText = EternityAdvisor.GetBuildText(numArray);
        string buildTextRandom = EternityAdvisor.GetBuildTextRandom(2, ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<DarkArtistEnchant>(), ModContent.ItemType<LumpOfFlesh>(), ModContent.ItemType<SpectreEnchant>());
        string str19 = str18 + buildText + buildTextRandom;
        local = str19;
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BeetleEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!NPC.downedMoonlord)
      {
        ModItem modItem;
        bossHelp = ModContent.TryFind<ModItem>("Fargowiltas", "CelestialSigil2", ref modItem) ? modItem.Type : 3601;
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<GaiaHelmet>(), ModContent.ItemType<GaiaPlate>(), ModContent.ItemType<GaiaGreaves>()) + " " + EternityAdvisor.GetBuildText(Utils.NextBool(Main.rand) ? 3883 : 2609, 4989, ModContent.ItemType<ChaliceoftheMoon>()) + EternityAdvisor.GetBuildTextRandom(4, Utils.NextBool(Main.rand) ? ModContent.ItemType<AeolusBoots>() : ModContent.ItemType<ValhallaKnightEnchant>(), ModContent.ItemType<DubiousCircuitry>(), ModContent.ItemType<PrecisionSeal>(), ModContent.ItemType<MutantAntibodies>(), ModContent.ItemType<DarkArtistEnchant>(), ModContent.ItemType<LumpOfFlesh>(), ModContent.ItemType<SpectreEnchant>());
        str1 += EternityAdvisor.GetBuildText(ModContent.ItemType<WizardEnchant>());
        melee = new int[2]
        {
          ModContent.ItemType<BeetleEnchant>(),
          ModContent.ItemType<TungstenEnchant>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<SpookyEnchant>(),
          ModContent.ItemType<TikiEnchant>()
        };
      }
      else if (!WorldSavingSystem.DownedBoss[8])
      {
        bossHelp = ModContent.ItemType<SigilOfChampions>();
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<FlightMasterySoul>(), Utils.NextBool(Main.rand) ? ModContent.ItemType<SupersonicSoul>() : ModContent.ItemType<ColossusSoul>()) + EternityAdvisor.GetBuildTextRandom(4, ModContent.ItemType<NebulaEnchant>(), ModContent.ItemType<TerraForce>(), ModContent.ItemType<EarthForce>(), ModContent.ItemType<ShadowForce>(), ModContent.ItemType<NatureForce>());
        melee = new int[1]
        {
          ModContent.ItemType<BerserkerSoul>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SnipersSoul>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ArchWizardsSoul>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<ConjuristsSoul>(),
          ModContent.ItemType<SpiritForce>()
        };
      }
      else if (!WorldSavingSystem.DownedAbom)
      {
        bossHelp = ModContent.ItemType<AbomsCurse>();
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<FlightMasterySoul>(), ModContent.ItemType<UniverseCore>(), ModContent.ItemType<ColossusSoul>()) + EternityAdvisor.GetBuildTextRandom(3, ModContent.ItemType<EarthForce>(), ModContent.ItemType<CosmoForce>(), ModContent.ItemType<SpiritForce>(), ModContent.ItemType<NatureForce>(), ModContent.ItemType<HeartoftheMasochist>());
        melee = new int[1]
        {
          ModContent.ItemType<BerserkerSoul>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SnipersSoul>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ArchWizardsSoul>()
        };
        summoner = new int[2]
        {
          ModContent.ItemType<ConjuristsSoul>(),
          ModContent.ItemType<SpiritForce>()
        };
      }
      else if (!WorldSavingSystem.DownedMutant)
      {
        bossHelp = ModContent.ItemType<AbominationnVoodooDoll>();
        build += EternityAdvisor.GetBuildText(ModContent.ItemType<TerrariaSoul>(), ModContent.ItemType<MasochistSoul>(), ModContent.ItemType<UniverseSoul>(), ModContent.ItemType<DimensionSoul>(), ModContent.ItemType<SparklingAdoration>(), ModContent.ItemType<AbominableWand>());
        melee = new int[1]
        {
          ModContent.ItemType<BerserkerSoul>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SnipersSoul>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ArchWizardsSoul>()
        };
        summoner = new int[1]
        {
          ModContent.ItemType<ConjuristsSoul>()
        };
      }
      else
      {
        bossHelp = ModContent.ItemType<MutantsCurse>();
        build = build + EternityAdvisor.GetBuildText(ModContent.ItemType<MutantMask>(), ModContent.ItemType<MutantBody>(), ModContent.ItemType<MutantPants>()) + " " + EternityAdvisor.GetBuildText(ModContent.ItemType<EternitySoul>(), ModContent.ItemType<MasochistSoul>(), ModContent.ItemType<UniverseSoul>(), ModContent.ItemType<SparklingAdoration>(), ModContent.ItemType<AbominableWand>(), ModContent.ItemType<MutantEye>());
        melee = new int[1]
        {
          ModContent.ItemType<BerserkerSoul>()
        };
        ranged = new int[1]
        {
          ModContent.ItemType<SnipersSoul>()
        };
        magic = new int[1]
        {
          ModContent.ItemType<ArchWizardsSoul>()
        };
        summoner = new int[1]
        {
          ModContent.ItemType<ConjuristsSoul>()
        };
      }
      if (Main.hardMode)
      {
        if (NPC.downedMechBossAny && !((IEnumerable<Item>) player.inventory).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.type == ModContent.ItemType<BionomicCluster>())) && !((IEnumerable<Item>) player.armor).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.type == ModContent.ItemType<BionomicCluster>())) && !((IEnumerable<Item>) player.armor).Any<Item>((Func<Item, bool>) (i => !i.IsAir && i.type == ModContent.ItemType<MasochistSoul>())) && !WorldSavingSystem.DownedAbom)
        {
          string str20 = str1;
          interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
          interpolatedStringHandler.AppendLiteral(" [i:");
          interpolatedStringHandler.AppendFormatted<int>(ModContent.ItemType<BionomicCluster>());
          interpolatedStringHandler.AppendLiteral("]");
          string stringAndClear = interpolatedStringHandler.ToStringAndClear();
          str1 = str20 + stringAndClear;
        }
        ModItem omni1;
        ModItem omni2;
        if (ModContent.TryFind<ModItem>("Fargowiltas", "Omnistation", ref omni1) && ModContent.TryFind<ModItem>("Fargowiltas", "Omnistation2", ref omni2))
        {
          bool flag = false;
          if (((IEnumerable<Item>) player.inventory).Any<Item>((Func<Item, bool>) (i =>
          {
            if (i.IsAir)
              return false;
            return i.type == omni1.Type || i.type == omni2.Type;
          })))
          {
            flag = true;
          }
          else
          {
            ModBuff modBuff;
            if (ModContent.TryFind<ModBuff>("Fargowiltas", "Omnistation", ref modBuff) && player.HasBuff(modBuff.Type))
              flag = true;
          }
          if (!flag)
          {
            string str21 = str1;
            string stringAndClear;
            if (!Utils.NextBool(Main.rand))
            {
              interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
              interpolatedStringHandler.AppendLiteral(" [i:");
              interpolatedStringHandler.AppendFormatted<int>(omni2.Type);
              interpolatedStringHandler.AppendLiteral("]");
              stringAndClear = interpolatedStringHandler.ToStringAndClear();
            }
            else
            {
              interpolatedStringHandler = new DefaultInterpolatedStringHandler(5, 1);
              interpolatedStringHandler.AppendLiteral(" [i:");
              interpolatedStringHandler.AppendFormatted<int>(omni1.Type);
              interpolatedStringHandler.AppendLiteral("]");
              stringAndClear = interpolatedStringHandler.ToStringAndClear();
            }
            str1 = str21 + stringAndClear;
          }
        }
      }
      string str22 = EternityAdvisor.ClassSpecific(player, melee, ranged, magic, summoner);
      build = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternityAdvisor.General", (object) build);
      if (!string.IsNullOrEmpty(str22))
        str22 = "\n" + Language.GetTextValue("Mods.FargowiltasSouls.Items.EternityAdvisor.ClassSpecific", (object) str22);
      build += str22;
      if (!string.IsNullOrEmpty(str1))
        str1 = "\n" + Language.GetTextValue("Mods.FargowiltasSouls.Items.EternityAdvisor.Other", (object) str1);
      build += str1;
      ref string local1 = ref build;
      string str23 = build;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
      interpolatedStringHandler.AppendLiteral("[i:");
      interpolatedStringHandler.AppendFormatted<int>(bossHelp);
      interpolatedStringHandler.AppendLiteral("]");
      string textValue = Language.GetTextValue("Mods.FargowiltasSouls.Items.EternityAdvisor.Summon", (object) interpolatedStringHandler.ToStringAndClear());
      string str24 = str23 + "\n" + textValue;
      local1 = str24;
      return bossHelp;
    }

    private static string ClassSpecific(
      Player player,
      int[] melee = null,
      int[] ranged = null,
      int[] magic = null,
      int[] summoner = null)
    {
      double num1 = Damage(DamageClass.Melee);
      double num2 = Damage(DamageClass.Ranged);
      double num3 = Damage(DamageClass.Magic);
      double num4 = Damage(DamageClass.Summon);
      string str1 = string.Empty;
      List<int> intList = new List<int>();
      double num5 = Utils.Max<double>(new double[4]
      {
        num1,
        num2,
        num3,
        num4
      });
      if (num1 >= num5 && melee != null)
        intList.AddRange((IEnumerable<int>) melee);
      if (num2 >= num5 && ranged != null)
        intList.AddRange((IEnumerable<int>) ranged);
      if (num3 >= num5 && magic != null)
        intList.AddRange((IEnumerable<int>) magic);
      if (num4 >= num5 && summoner != null)
        intList.AddRange((IEnumerable<int>) summoner);
      if (intList.Count <= 0)
        return string.Empty;
      foreach (int num6 in intList)
      {
        string str2 = str1;
        DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
        interpolatedStringHandler.AppendLiteral("[i:");
        interpolatedStringHandler.AppendFormatted<int>(num6);
        interpolatedStringHandler.AppendLiteral("]");
        string stringAndClear = interpolatedStringHandler.ToStringAndClear();
        str1 = str2 + stringAndClear;
      }
      return str1;

      double Damage(DamageClass damageClass)
      {
        StatModifier totalDamage = player.GetTotalDamage(damageClass);
        double additive = (double) ((StatModifier) ref totalDamage).Additive;
        totalDamage = player.GetTotalDamage(damageClass);
        double multiplicative = (double) ((StatModifier) ref totalDamage).Multiplicative;
        return Math.Round(additive * multiplicative * 100.0 - 100.0);
      }
    }

    public virtual bool? UseItem(Player player)
    {
      if (player.ItemTimeIsZero)
      {
        string build;
        this.GetBossHelp(out build, player);
        if (((Entity) player).whoAmI == Main.myPlayer)
          Main.NewText(build, byte.MaxValue, byte.MaxValue, byte.MaxValue);
        SoundEngine.PlaySound(ref SoundID.Meowmere, new Vector2?(((Entity) player).Center), (SoundUpdateCallback) null);
      }
      return new bool?(true);
    }
  }
}
