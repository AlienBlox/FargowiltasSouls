// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.NPCMatching.NPCMatcher
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.NPCMatching.Conditions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace FargowiltasSouls.Core.NPCMatching
{
  public class NPCMatcher
  {
    public List<INPCMatchCondition> Conditions;

    public NPCMatcher()
    {
      List<INPCMatchCondition> npcMatchConditionList = new List<INPCMatchCondition>();
      CollectionsMarshal.SetCount<INPCMatchCondition>(npcMatchConditionList, 1);
      Span<INPCMatchCondition> span = CollectionsMarshal.AsSpan<INPCMatchCondition>(npcMatchConditionList);
      int num1 = 0;
      span[num1] = (INPCMatchCondition) new MatchEverythingCondition();
      int num2 = num1 + 1;
      this.Conditions = npcMatchConditionList;
    }

    public NPCMatcher MatchType(int type)
    {
      this.Conditions.Add((INPCMatchCondition) new MatchTypeCondition(type));
      return this;
    }

    public NPCMatcher MatchTypeRange(params int[] types)
    {
      this.Conditions.Add((INPCMatchCondition) new MatchTypeRangeCondition(types));
      return this;
    }

    public bool Satisfies(int type)
    {
      return this.Conditions.TrueForAll((Predicate<INPCMatchCondition>) (condition => condition.Satisfies(type)));
    }
  }
}
