// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Materials.Eridanium
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;

#nullable disable
namespace FargowiltasSouls.Content.Items.Materials
{
  public class Eridanium : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 30;
    }

    public virtual void SetDefaults()
    {
      this.Item.maxStack = 99;
      this.Item.rare = 11;
      ((Entity) this.Item).width = 12;
      ((Entity) this.Item).height = 12;
      this.Item.value = Item.sellPrice(0, 5, 0, 0);
    }
  }
}
