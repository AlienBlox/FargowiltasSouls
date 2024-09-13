// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ItemDropRules.DropBasedOnEMode
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ItemDropRules.Conditions;
using FargowiltasSouls.Core.Systems;
using System.Collections.Generic;
using Terraria.GameContent.ItemDropRules;

#nullable disable
namespace FargowiltasSouls.Core.ItemDropRules
{
  public class DropBasedOnEMode : IItemDropRule, INestedItemDropRule
  {
    protected readonly IItemDropRule RuleForEMode;
    protected readonly IItemDropRule RuleForDefault;

    public List<IItemDropRuleChainAttempt> ChainedRules { get; private set; }

    public DropBasedOnEMode(IItemDropRule ruleForEMode, IItemDropRule ruleForDefault)
    {
      this.RuleForEMode = ruleForEMode;
      this.RuleForDefault = ruleForDefault;
      this.ChainedRules = new List<IItemDropRuleChainAttempt>();
    }

    public bool CanDrop(DropAttemptInfo info)
    {
      return !WorldSavingSystem.EternityMode ? this.RuleForDefault.CanDrop(info) : this.RuleForEMode.CanDrop(info);
    }

    public void ReportDroprates(List<DropRateInfo> drops, DropRateInfoChainFeed ratesInfo)
    {
      DropRateInfoChainFeed rateInfoChainFeed1 = ((DropRateInfoChainFeed) ref ratesInfo).With(1f);
      ((DropRateInfoChainFeed) ref rateInfoChainFeed1).AddCondition((IItemDropRuleCondition) new EModeDropCondition());
      this.RuleForEMode.ReportDroprates(drops, rateInfoChainFeed1);
      DropRateInfoChainFeed rateInfoChainFeed2 = ((DropRateInfoChainFeed) ref ratesInfo).With(1f);
      ((DropRateInfoChainFeed) ref rateInfoChainFeed2).AddCondition((IItemDropRuleCondition) new NotEModeDropCondition());
      this.RuleForDefault.ReportDroprates(drops, rateInfoChainFeed2);
      Chains.ReportDroprates(this.ChainedRules, 1f, drops, ratesInfo);
    }

    public ItemDropAttemptResult TryDroppingItem(DropAttemptInfo info)
    {
      return new ItemDropAttemptResult()
      {
        State = (ItemDropAttemptResultState) 3
      };
    }

    public ItemDropAttemptResult TryDroppingItem(
      DropAttemptInfo info,
      ItemDropRuleResolveAction resolveAction)
    {
      return !WorldSavingSystem.EternityMode ? resolveAction.Invoke(this.RuleForDefault, info) : resolveAction.Invoke(this.RuleForEMode, info);
    }
  }
}
