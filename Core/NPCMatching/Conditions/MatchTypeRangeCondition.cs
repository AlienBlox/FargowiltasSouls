// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.NPCMatching.Conditions.MatchTypeRangeCondition
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace FargowiltasSouls.Core.NPCMatching.Conditions
{
  public class MatchTypeRangeCondition : INPCMatchCondition
  {
    public int[] Types;

    public MatchTypeRangeCondition(IEnumerable<int> types) => this.Types = types.ToArray<int>();

    public MatchTypeRangeCondition(params int[] types) => this.Types = types;

    public bool Satisfies(int type) => ((IEnumerable<int>) this.Types).Contains<int>(type);
  }
}
