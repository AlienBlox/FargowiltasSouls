// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.UniverseCore
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class UniverseCore : SoulsItem
  {
    public override int NumFrames => 5;

    public virtual void SetStaticDefaults()
    {
      Main.RegisterItemAnimation(this.Type, (DrawAnimation) new DrawAnimationVertical(10, this.NumFrames, false));
      ItemID.Sets.AnimatesAsSoul[this.Type] = true;
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = -12;
      this.Item.value = Item.sellPrice(0, 50, 0, 0);
      this.Item.expert = true;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().UniverseCore = true;
    }
  }
}
