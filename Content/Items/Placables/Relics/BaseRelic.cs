// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.Relics.BaseRelic
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables.Relics
{
  public abstract class BaseRelic : SoulsItem
  {
    protected abstract int TileType { get; }

    public virtual void SetStaticDefaults()
    {
      ((ModType) this).SetStaticDefaults();
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      base.SetDefaults();
      this.Item.DefaultToPlaceableTile(this.TileType, 0);
      ((Entity) this.Item).width = 30;
      ((Entity) this.Item).height = 40;
      this.Item.maxStack = 99;
      this.Item.rare = -13;
      this.Item.master = true;
      this.Item.value = Item.buyPrice(0, 5, 0, 0);
    }
  }
}
