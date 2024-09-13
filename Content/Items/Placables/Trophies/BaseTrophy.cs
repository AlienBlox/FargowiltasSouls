// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Placables.Trophies.BaseTrophy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Placables.Trophies
{
  public abstract class BaseTrophy : SoulsItem
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
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.maxStack = 99;
      this.Item.rare = 1;
      this.Item.value = Item.buyPrice(0, 1, 0, 0);
    }
  }
}
