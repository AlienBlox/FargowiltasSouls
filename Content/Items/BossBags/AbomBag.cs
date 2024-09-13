// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.AbomBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class AbomBag : BossBag
  {
    protected override bool IsPreHMBag => false;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<FargowiltasSouls.Content.Bosses.AbomBoss.AbomBoss>()));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<AbomEnergy>(), 1, 15, 30));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<AbomEnergy>(), 1, 15, 30));
    }
  }
}
