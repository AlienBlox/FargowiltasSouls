// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Summons.ChampionySigil
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Summons
{
  public class ChampionySigil : SigilOfChampions
  {
    public virtual string Texture => "FargowiltasSouls/Content/Items/Summons/SigilOfChampions";

    public override void SetStaticDefaults()
    {
      ItemID.Sets.SortingPriorityBossSpawns[this.Type] = 12;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 3;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.maxStack = 20;
      this.Item.useAnimation = 30;
      this.Item.useTime = 30;
      this.Item.consumable = true;
    }

    public override bool CanUseItem(Player player) => true;

    public virtual bool ConsumeItem(Player player) => player.altFunctionUse != 2;

    public override void AddRecipes()
    {
    }
  }
}
