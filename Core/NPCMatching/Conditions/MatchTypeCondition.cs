// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.NPCMatching.Conditions.MatchTypeCondition
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

#nullable disable
namespace FargowiltasSouls.Core.NPCMatching.Conditions
{
  public class MatchTypeCondition : INPCMatchCondition
  {
    public int Type;

    public MatchTypeCondition(int type) => this.Type = type;

    public bool Satisfies(int type) => type == this.Type;
  }
}
