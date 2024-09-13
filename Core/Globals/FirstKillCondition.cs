// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.Globals.FirstKillCondition
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Core.Globals
{
  internal class FirstKillCondition : IItemDropRuleCondition, IProvideItemConditionDescription
  {
    public bool CanDrop(DropAttemptInfo info)
    {
      bool flag1 = !info.IsInSimulation && WorldSavingSystem.EternityMode;
      if (flag1)
      {
        bool flag2;
        switch (info.npc.type)
        {
          case 4:
            flag2 = !NPC.downedBoss1;
            break;
          case 50:
            flag2 = !NPC.downedSlimeKing;
            break;
          default:
            flag2 = info.npc.type == ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>() && !WorldSavingSystem.DownedBoss[9];
            break;
        }
        flag1 = flag2;
      }
      return flag1;
    }

    public bool CanShowItemDropInUI() => true;

    public string GetConditionDescription()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.Conditions.FirstKill");
    }
  }
}
