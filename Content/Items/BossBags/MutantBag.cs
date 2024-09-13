// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.MutantBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Content.Items.Misc;
using FargowiltasSouls.Core.ItemDropRules.Conditions;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class MutantBag : BossBag
  {
    protected override bool IsPreHMBag => false;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<Masochist>(), 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<FargowiltasSouls.Content.Bosses.MutantBoss.MutantBoss>()));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.ByCondition((IItemDropRuleCondition) new EModeDropCondition(), ModContent.ItemType<EternalEnergy>(), 1, 15, 20, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<MutantsFury>(), 1, 1, 1));
    }
  }
}
