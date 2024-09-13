// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.EmodeItemBalance
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items
{
  public class EmodeItemBalance : ModSystem
  {
    public static EmodeItemBalance.EModeChange EmodeBalance(
      ref Item item,
      ref float balanceNumber,
      ref string[] balanceTextKeys,
      ref string extra)
    {
      switch (item.type)
      {
        case 165:
          if (NPC.downedBoss3)
            return EmodeItemBalance.EModeChange.None;
          balanceTextKeys = new string[1]{ "WaterBolt" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 197:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.6f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 208:
          balanceTextKeys = new string[1]{ "JungleRose" };
          return EmodeItemBalance.EModeChange.Buff;
        case 272:
          if (NPC.downedBoss2)
            return EmodeItemBalance.EModeChange.None;
          balanceTextKeys = new string[3]
          {
            "DemonScythe",
            "DamageNoTooltip",
            "SpeedNoTooltip"
          };
          balanceNumber = 0.6f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 277:
        case 280:
        case 1228:
        case 2332:
          balanceNumber = 1f;
          balanceTextKeys = new string[1]{ "SpearRework" };
          return EmodeItemBalance.EModeChange.Buff;
        case 368:
        case 550:
        case 674:
          balanceNumber = 1.2f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "Damage"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 390:
          balanceNumber = -1f;
          balanceTextKeys = new string[2]
          {
            "SpearRework",
            "MythrilHalberdRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 406:
        case 1200:
          balanceNumber = 1.15f;
          balanceTextKeys = new string[2]
          {
            "Damage",
            "SpearRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 482:
        case 1199:
          balanceNumber = 1.2f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "Damage"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 483:
          balanceNumber = 1.5f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "CobaltNaginataRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 484:
          balanceNumber = 1.5f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "MythrilHalberdRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 517:
          if (Main.hardMode)
            return EmodeItemBalance.EModeChange.None;
          balanceTextKeys = new string[2]
          {
            "Damage",
            "Speed"
          };
          balanceNumber = 0.66f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 532:
          if (Main.hardMode)
            return EmodeItemBalance.EModeChange.None;
          balanceTextKeys = new string[1]{ "StarCloak" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 537:
          balanceNumber = -1f;
          balanceTextKeys = new string[2]
          {
            "SpearRework",
            "CobaltNaginataRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 551:
        case 552:
        case 553:
        case 558:
        case 559:
        case 4873:
        case 4896:
        case 4897:
        case 4898:
        case 4899:
        case 4900:
        case 4901:
          balanceTextKeys = new string[1]{ "HolyDodge" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 554:
          balanceTextKeys = new string[1]
          {
            "CrossNecklaceNerf"
          };
          return EmodeItemBalance.EModeChange.Nerf;
        case 724:
          balanceTextKeys = new string[1]
          {
            "IceBladeFrostburn"
          };
          balanceNumber = 1f;
          return EmodeItemBalance.EModeChange.Buff;
        case 862:
          balanceTextKeys = new string[1]
          {
            "CrossNecklaceNerf"
          };
          if (!Main.hardMode)
            balanceTextKeys = new string[1]{ "StarCloak" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 905:
          balanceTextKeys = new string[1]{ "CoinGun" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 1185:
          balanceNumber = 1.5f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "PalladiumPikeRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 1186:
          balanceNumber = -1f;
          balanceTextKeys = new string[2]
          {
            "SpearRework",
            "PalladiumPikeRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 1192:
          balanceNumber = 1.5f;
          balanceTextKeys = new string[2]
          {
            "Speed",
            "OrichalcumHalberdRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 1193:
          balanceNumber = -1f;
          balanceTextKeys = new string[2]
          {
            "SpearRework",
            "OrichalcumHalberdRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 1253:
        case 3997:
          balanceTextKeys = new string[1]
          {
            "FrozenTurtleShell"
          };
          return EmodeItemBalance.EModeChange.Nerf;
        case 1265:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.88f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 1309:
        case 2365:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.1f;
          return EmodeItemBalance.EModeChange.Buff;
        case 1326:
        case 5335:
          balanceTextKeys = new string[1]{ "RodofDiscord" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 1569:
          balanceTextKeys = new string[1]{ "VampireKnives" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 1782:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.5f;
          return EmodeItemBalance.EModeChange.Buff;
        case 1835:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.3f;
          return EmodeItemBalance.EModeChange.Buff;
        case 1844:
          balanceNumber = 15f;
          if (WorldSavingSystem.MasochistModeReal)
          {
            balanceTextKeys = new string[2]
            {
              "MoonsDrops",
              "MoonsWaves"
            };
            return EmodeItemBalance.EModeChange.Nerf;
          }
          balanceTextKeys = new string[1]{ "MoonsDrops" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 1910:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.1f;
          return EmodeItemBalance.EModeChange.Buff;
        case 1930:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.8f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 1931:
          balanceTextKeys = new string[2]
          {
            "Damage",
            "Speed"
          };
          balanceNumber = 0.7f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 1947:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.12f;
          return EmodeItemBalance.EModeChange.Buff;
        case 1958:
          balanceNumber = 15f;
          if (WorldSavingSystem.MasochistModeReal)
          {
            balanceTextKeys = new string[2]
            {
              "MoonsDrops",
              "MoonsWaves"
            };
            return EmodeItemBalance.EModeChange.Nerf;
          }
          balanceTextKeys = new string[1]{ "MoonsDrops" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 2331:
          balanceNumber = 0.8f;
          balanceTextKeys = new string[2]
          {
            "Damage",
            "SpearRework"
          };
          return EmodeItemBalance.EModeChange.Buff;
        case 2359:
          balanceTextKeys = new string[1]
          {
            "WarmthPotionNerf"
          };
          return EmodeItemBalance.EModeChange.Nerf;
        case 3013:
          balanceTextKeys = new string[1]{ "Speed" };
          balanceNumber = 0.75f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 3223:
          balanceTextKeys = new string[1]
          {
            "BrainOfConfusion"
          };
          return EmodeItemBalance.EModeChange.Nerf;
        case 3569:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.5f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 3571:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.6f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 3827:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 0.7f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 4060:
          balanceTextKeys = new string[1]
          {
            "SuperStarCannon"
          };
          balanceNumber = 7f;
          return EmodeItemBalance.EModeChange.Nerf;
        case 4347:
        case 4348:
          balanceTextKeys = new string[1]{ "Zapinator" };
          return EmodeItemBalance.EModeChange.Nerf;
        case 4764:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.2f;
          return EmodeItemBalance.EModeChange.Buff;
        case 4956:
          if (WorldSavingSystem.DownedMutant || Terraria.ModLoader.ModLoader.HasMod("CalamityMod"))
          {
            balanceTextKeys = new string[1]{ "ZenithNone" };
            return EmodeItemBalance.EModeChange.Neutral;
          }
          string str1 = "";
          if (!WorldSavingSystem.DownedAbom)
            str1 = str1 + Language.GetTextValue("Mods.FargowiltasSouls.NPCs.AbomBoss.DisplayName") + ", ";
          string str2 = str1 + Language.GetTextValue("Mods.FargowiltasSouls.NPCs.MutantBoss.DisplayName");
          balanceTextKeys = new string[1]{ "ZenithHitRate" };
          extra = str2;
          return EmodeItemBalance.EModeChange.Nerf;
        case 5095:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.2f;
          return EmodeItemBalance.EModeChange.Buff;
        case 5117:
          balanceTextKeys = new string[1]{ "Damage" };
          balanceNumber = 1.4f;
          return EmodeItemBalance.EModeChange.Buff;
        case 5118:
          balanceTextKeys = new string[1]{ "WeatherPain" };
          return EmodeItemBalance.EModeChange.Buff;
        case 5119:
          balanceTextKeys = new string[2]
          {
            "Damage",
            "HoundiusShootius"
          };
          balanceNumber = 1.2f;
          return EmodeItemBalance.EModeChange.Buff;
        default:
          return EmodeItemBalance.EModeChange.None;
      }
    }

    public static void BalanceWeaponStats(Player player, Item item, ref StatModifier damage)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      string empty = string.Empty;
      float balanceNumber = -1f;
      string[] balanceTextKeys = (string[]) null;
      int num = (int) EmodeItemBalance.EmodeBalance(ref item, ref balanceNumber, ref balanceTextKeys, ref empty);
      if (balanceTextKeys == null)
        return;
      for (int index = 0; index < balanceTextKeys.Length; ++index)
      {
        switch (balanceTextKeys[index])
        {
          case "Damage":
          case "DamageNoTooltip":
            damage = StatModifier.op_Multiply(damage, balanceNumber);
            break;
          case "Speed":
          case "SpeedNoTooltip":
            player.FargoSouls().AttackSpeed *= balanceNumber;
            break;
        }
      }
    }

    private static void ItemBalance(
      List<TooltipLine> tooltips,
      EmodeItemBalance.EModeChange change,
      string key,
      int amount = 0)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
      interpolatedStringHandler.AppendLiteral("Mods.FargowiltasSouls.EModeBalance.");
      interpolatedStringHandler.AppendFormatted<EmodeItemBalance.EModeChange>(change);
      string textValue1 = Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
      string textValue2 = Language.GetTextValue("Mods.FargowiltasSouls.EModeBalance." + key, amount == 0 ? (object) null : (object) amount);
      List<TooltipLine> tooltipLineList = tooltips;
      FargowiltasSouls.FargowiltasSouls instance = FargowiltasSouls.FargowiltasSouls.Instance;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
      interpolatedStringHandler.AppendFormatted<EmodeItemBalance.EModeChange>(change);
      interpolatedStringHandler.AppendFormatted(key);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      string str = textValue1 + textValue2;
      TooltipLine tooltipLine = new TooltipLine((Mod) instance, stringAndClear, str);
      tooltipLineList.Add(tooltipLine);
    }

    private static void ItemBalance(
      List<TooltipLine> tooltips,
      EmodeItemBalance.EModeChange change,
      string key,
      string extra)
    {
      DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(35, 1);
      interpolatedStringHandler.AppendLiteral("Mods.FargowiltasSouls.EModeBalance.");
      interpolatedStringHandler.AppendFormatted<EmodeItemBalance.EModeChange>(change);
      string textValue1 = Language.GetTextValue(interpolatedStringHandler.ToStringAndClear());
      string textValue2 = Language.GetTextValue("Mods.FargowiltasSouls.EModeBalance." + key);
      List<TooltipLine> tooltipLineList = tooltips;
      FargowiltasSouls.FargowiltasSouls instance = FargowiltasSouls.FargowiltasSouls.Instance;
      interpolatedStringHandler = new DefaultInterpolatedStringHandler(0, 2);
      interpolatedStringHandler.AppendFormatted<EmodeItemBalance.EModeChange>(change);
      interpolatedStringHandler.AppendFormatted(key);
      string stringAndClear = interpolatedStringHandler.ToStringAndClear();
      string str = textValue1 + textValue2 + extra;
      TooltipLine tooltipLine = new TooltipLine((Mod) instance, stringAndClear, str);
      tooltipLineList.Add(tooltipLine);
    }

    public static void BalanceTooltips(Item item, ref List<TooltipLine> tooltips)
    {
      if (!WorldSavingSystem.EternityMode)
        return;
      string empty = string.Empty;
      float balanceNumber = -1f;
      string[] balanceTextKeys = (string[]) null;
      EmodeItemBalance.EModeChange emodeChange = EmodeItemBalance.EmodeBalance(ref item, ref balanceNumber, ref balanceTextKeys, ref empty);
      if (balanceTextKeys != null)
      {
        for (int index = 0; index < balanceTextKeys.Length; ++index)
        {
          switch (balanceTextKeys[index])
          {
            case "Damage":
              EmodeItemBalance.EModeChange change1 = (double) balanceNumber > 1.0 ? EmodeItemBalance.EModeChange.Buff : ((double) balanceNumber < 1.0 ? EmodeItemBalance.EModeChange.Nerf : EmodeItemBalance.EModeChange.Neutral);
              int amount1 = change1 == EmodeItemBalance.EModeChange.Buff ? (int) Math.Round(((double) balanceNumber - 1.0) * 100.0) : (int) Math.Round((1.0 - (double) balanceNumber) * 100.0);
              string key1 = change1 == EmodeItemBalance.EModeChange.Buff ? "DamagePositive" : "Damage";
              EmodeItemBalance.ItemBalance(tooltips, change1, key1, amount1);
              continue;
            case "Speed":
              EmodeItemBalance.EModeChange change2 = (double) balanceNumber > 1.0 ? EmodeItemBalance.EModeChange.Buff : ((double) balanceNumber < 1.0 ? EmodeItemBalance.EModeChange.Nerf : EmodeItemBalance.EModeChange.Neutral);
              int amount2 = change2 == EmodeItemBalance.EModeChange.Buff ? (int) Math.Round(((double) balanceNumber - 1.0) * 100.0) : (int) Math.Round((1.0 - (double) balanceNumber) * 100.0);
              string key2 = change2 == EmodeItemBalance.EModeChange.Buff ? "SpeedPositive" : "Speed";
              EmodeItemBalance.ItemBalance(tooltips, change2, key2, amount2);
              continue;
            case "DamageNoTooltip":
            case "SpeedNoTooltip":
              continue;
            default:
              EmodeItemBalance.EModeChange change3 = emodeChange;
              if ((double) balanceNumber != -1.0 && balanceTextKeys != null && index == 0)
              {
                EmodeItemBalance.ItemBalance(tooltips, change3, balanceTextKeys[index], (int) balanceNumber);
                continue;
              }
              if (empty != string.Empty && balanceTextKeys != null && index == 0)
              {
                EmodeItemBalance.ItemBalance(tooltips, change3, balanceTextKeys[index], empty);
                continue;
              }
              EmodeItemBalance.ItemBalance(tooltips, change3, balanceTextKeys[index]);
              continue;
          }
        }
      }
      if (item.shoot > 0 && ProjectileID.Sets.IsAWhip[item.shoot])
      {
        EmodeItemBalance.ItemBalance(tooltips, EmodeItemBalance.EModeChange.Nerf, "WhipSpeed");
        EmodeItemBalance.ItemBalance(tooltips, EmodeItemBalance.EModeChange.Nerf, "WhipStack");
      }
      if (item.prefix < 62 || item.prefix > 65)
        return;
      EmodeItemBalance.ItemBalance(tooltips, EmodeItemBalance.EModeChange.Neutral, "DefensePrefix");
    }

    public enum EModeChange
    {
      None,
      Nerf,
      Buff,
      Neutral,
    }
  }
}
