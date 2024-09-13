// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ItemDropRules.Conditions.RuntimeDropCondition
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using System;
using Terraria.GameContent.ItemDropRules;

#nullable disable
namespace FargowiltasSouls.Core.ItemDropRules.Conditions
{
  public class RuntimeDropCondition : IItemDropRuleCondition, IProvideItemConditionDescription
  {
    protected readonly Func<bool> Condition;
    protected readonly string Description;

    public RuntimeDropCondition(Func<bool> condition, string description)
    {
      this.Condition = condition;
      this.Description = description;
    }

    public bool CanDrop(DropAttemptInfo info) => !info.IsInSimulation && this.Condition();

    public bool CanShowItemDropInUI() => true;

    public string GetConditionDescription() => this.Description;
  }
}
