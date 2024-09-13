// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Core.ItemDropRules.Conditions.EModeEarlyBirdRewardDropCondition
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.Systems;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;

#nullable disable
namespace FargowiltasSouls.Core.ItemDropRules.Conditions
{
  public class EModeEarlyBirdRewardDropCondition : 
    IItemDropRuleCondition,
    IProvideItemConditionDescription
  {
    public bool CanDrop(DropAttemptInfo info)
    {
      return !info.IsInSimulation && !Main.hardMode && WorldSavingSystem.EternityMode;
    }

    public bool CanShowItemDropInUI() => true;

    public string GetConditionDescription()
    {
      return Language.GetTextValue("Mods.FargowiltasSouls.Conditions.EModeEarlyBirdPHM");
    }
  }
}
