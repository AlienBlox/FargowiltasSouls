// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.PatreonGlobalNPC
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Patreon.Catsounds;
using FargowiltasSouls.Content.Patreon.Daawnz;
using FargowiltasSouls.Content.Patreon.DemonKing;
using FargowiltasSouls.Content.Patreon.DevAesthetic;
using FargowiltasSouls.Content.Patreon.Gittle;
using FargowiltasSouls.Content.Patreon.LaBonez;
using FargowiltasSouls.Content.Patreon.Purified;
using FargowiltasSouls.Content.Patreon.Sam;
using FargowiltasSouls.Content.Patreon.Shucks;
using FargowiltasSouls.Core;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon
{
  public class PatreonGlobalNPC : GlobalNPC
  {
    public virtual void ModifyShop(NPCShop shop)
    {
      if (((AbstractNPCShop) shop).NpcType != 178)
        return;
      NPCShop npcShop = shop;
      Item obj = new Item(ModContent.ItemType<RoombaPet>(), 1, 0);
      obj.shopCustomPrice = new int?(Item.buyPrice(0, 0, 0, 50000));
      Condition[] conditionArray = new Condition[1]
      {
        new Condition("Mods.FargowiltasSouls.Conditions.RoombaPetSold", (Func<bool>) (() => SoulConfig.Instance.PatreonRoomba))
      };
      npcShop.Add(obj, conditionArray);
    }

    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      base.ModifyNPCLoot(npc, npcLoot);
      switch (npc.type)
      {
        case 20:
          ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new PatreonPlantDropCondition(Language.GetTextValue("Mods.FargowiltasSouls.Conditions.PatreonPlant")), ModContent.ItemType<PiranhaPlantVoodooDoll>(), 1, 1, 1, 1));
          break;
        case 50:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonKingSlime && WorldSavingSystem.EternityMode), ModContent.ItemType<MedallionoftheFallenKing>(), 100, "PatreonEMode");
          break;
        case (int) sbyte.MaxValue:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonPrime && WorldSavingSystem.EternityMode), ModContent.ItemType<PrimeStaff>(), 20, "PatreonEMode");
          break;
        case 221:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonDoor), ModContent.ItemType<SquidwardDoor>(), 50);
          break;
        case 245:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonOrb), ModContent.ItemType<ComputationOrb>(), 10);
          break;
        case 266:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonCrimetroid), ModContent.ItemType<CrimetroidEgg>(), 25);
          break;
        case 398:
          AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonDevious && WorldSavingSystem.EternityMode), ModContent.ItemType<DeviousAestheticus>(), 20, "PatreonEMode");
          break;
      }
      if (npc.type != ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>())
        return;
      AddPatreonDrop((Func<bool>) (() => SoulConfig.Instance.PatreonFishron), ModContent.ItemType<StaffOfUnleashedOcean>(), 25);

      void AddPatreonDrop(Func<bool> condition, int item, int chanceDenominator = 1, string extraKey = null)
      {
        string textValue = Language.GetTextValue("Mods.FargowiltasSouls.Conditions.Patreon");
        if (extraKey != null)
          textValue = Language.GetTextValue("Mods.FargowiltasSouls.Conditions." + extraKey);
        RuntimeDropCondition runtimeDropCondition = new RuntimeDropCondition(condition, textValue);
        ((NPCLoot) ref npcLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) runtimeDropCondition, item, chanceDenominator, 1, 1, 1));
      }
    }
  }
}
