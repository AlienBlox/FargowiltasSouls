// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.TrojanSquirrelBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Weapons.Challengers;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class TrojanSquirrelBag : BossBag
  {
    protected override bool IsPreHMBag => true;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<BoxofGizmos>(), 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<FargowiltasSouls.Content.Bosses.TrojanSquirrel.TrojanSquirrel>()));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.Find<ModItem>("Fargowiltas", "LumberJaxe").Type, 5, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(4759, 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(2334, 1, 5, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(3093, 1, 5, 5));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(27, 1, 100, 100));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(1, new int[2]
      {
        2018,
        3563
      }));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.OneFromOptions(1, new int[4]
      {
        ModContent.ItemType<TreeSword>(),
        ModContent.ItemType<MountedAcornGun>(),
        ModContent.ItemType<SnowballStaff>(),
        ModContent.ItemType<KamikazeSquirrelStaff>()
      }));
    }
  }
}
