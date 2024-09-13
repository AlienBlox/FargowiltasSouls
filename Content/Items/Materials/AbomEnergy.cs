// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Materials.AbomEnergy
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
  public class AbomEnergy : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(4, 5, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
      ItemID.Sets.ItemNoGravity[this.Item.type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 30;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.maxStack = 99;
      this.Item.rare = 11;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
    }

    public virtual Color? GetAlpha(Color lightColor) => new Color?(Color.White);
  }
}
