// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.BossBags.CosmosBag
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Bosses.Champions.Cosmos;
using FargowiltasSouls.Content.Items.Accessories.Expert;
using FargowiltasSouls.Content.Items.Materials;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.BossBags
{
  public class CosmosBag : BossBag
  {
    protected override bool IsPreHMBag => false;

    public virtual void ModifyItemLoot(ItemLoot itemLoot)
    {
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<UniverseCore>(), 1, 1, 1));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.CoinsBasedOnNPCValue(ModContent.NPCType<CosmosChampion>()));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.ItemType<Eridanium>(), 1, 10, 10));
      ((ItemLoot) ref itemLoot).Add(ItemDropRule.Common(ModContent.Find<ModItem>("Fargowiltas", "CrucibleCosmos").Type, 1, 1, 1));
    }
  }
}
