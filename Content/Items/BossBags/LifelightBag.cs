// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.LifelightBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Lifelight;
using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class LifelightBag : BossBag
  {
    protected override bool IsPreHMBag => false;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<LifeRevitalizer>(), 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<LifeChallenger>()));
      ((ItemLoot) ref itemLoot).Add((IItemDropRule) new OneFromOptionsDropRule(1, 1, new int[4]
      {
        ModContent.ItemType<EnchantedLifeblade>(),
        ModContent.ItemType<Lightslinger>(),
        ModContent.ItemType<CrystallineCongregation>(),
        ModContent.ItemType<KamikazePixieStaff>()
      }));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(3986, 1, 5, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(520, 1, 3, 3));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(501, 1, 25, 25));
    }
  }
}
