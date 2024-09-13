// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ItemDropRules.ChampionEnchDropRule
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

#nullable disable
namespace FargowiltasSouls.Core.ItemDropRules
{
  public class ChampionEnchDropRule : IItemDropRule
  {
    public readonly int[] DropIds;

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public ChampionEnchDropRule(int[] drops)
    {
      this.DropIds = drops;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info) => true;

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      int num1 = 1;
      if (info.IsExpertMode)
        ++num1;
      if (WorldSavingSystem.EternityMode)
        ++num1;
      List<int> intList = new List<int>((IEnumerable<int>) this.DropIds);
      while (intList.Count > num1)
        intList.RemoveAt(info.rng.Next(intList.Count));
      foreach (int num2 in intList)
        CommonCode.DropItem(info, num2, 1, false);
      return new ItemDropAttemptResult()
      {
        State = (ItemDropAttemptResultState) 2
      };
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      float num1 = 1f;
      float num2 = (float) (1.0 / (double) this.DropIds.Length * ((double) num1 * (double) ratesInfo.parentDroprateChance));
      for (int index = 0; index < this.DropIds.Length; ++index)
        drops.Add(new DropRateInfo(this.DropIds[index], 1, 1, num2, ratesInfo.conditions));
      Chains.ReportDroprates(this.ChainedRules, num1, drops, ratesInfo);
    }
  }
}
