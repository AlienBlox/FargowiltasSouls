// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.EModeFirstKillDrop
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  public class EModeFirstKillDrop : GlobalNPC
  {
    public virtual void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
      IItemDropRule iitemDropRule = (IItemDropRule) null;
      if (npc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>())
        iitemDropRule = EModeFirstKillDrop.FirstKillDrop(2, 29);
      switch (npc.type)
      {
        case 4:
          iitemDropRule = EModeFirstKillDrop.FirstKillDrop(3, 29);
          break;
        case 50:
          iitemDropRule = EModeFirstKillDrop.FirstKillDrop(2, 29);
          break;
      }
      if (iitemDropRule == null)
        return;
      ((NPCLoot) ref npcLoot).Add(iitemDropRule);
    }

    private static IItemDropRule Drop(int count, int itemID)
    {
      return ItemDropRule.Common(itemID, 1, count, count);
    }

    public static IItemDropRule FirstKillDrop(int amount, int itemID)
    {
      LeadingConditionRule leadingConditionRule = new LeadingConditionRule((IItemDropRuleCondition) new FirstKillCondition());
      Chains.OnSuccess((IItemDropRule) leadingConditionRule, EModeFirstKillDrop.Drop(amount, itemID), false);
      return (IItemDropRule) leadingConditionRule;
    }
  }
}
