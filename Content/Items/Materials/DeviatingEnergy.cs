// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Materials.DeviatingEnergy
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Materials
{
  public class DeviatingEnergy : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Type, (DrawAnimation) new DrawAnimationVertical(5, 7, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      ItemID.Sets.ItemNoGravity[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 30;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 99;
      this.Item.rare = 3;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
