// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.BanishedBaronBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class BanishedBaronBag : BossBag
  {
    protected override bool IsPreHMBag => true;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<RustedOxygenTank>(), 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<LifeChallenger>()));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(1, new int[4]
      {
        ModContent.ItemType<TheBaronsTusk>(),
        ModContent.ItemType<RoseTintedVisor>(),
        ModContent.ItemType<NavalRustrifle>(),
        ModContent.ItemType<DecrepitAirstrikeRemote>()
      }));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(5003, 1, 1, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(3, new int[3]
      {
        3096,
        3037,
        3120
      }));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(5139, 4, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2354, 3, 2, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2355, 2, 2, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2356, 5, 2, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(3183, 50, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2360, 50, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2294, 150, 1, 1));
    }
  }
}
