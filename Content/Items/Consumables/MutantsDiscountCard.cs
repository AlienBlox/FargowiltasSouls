// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Consumables.MutantsDiscountCard
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Consumables
{
  public class MutantsDiscountCard : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetDefaults()
    {
      this.Item.DefaultToFood(20, 20, 207, 28800, false, 17);
      this.Item.rare = 4;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
    }

    public virtual bool CanUseItem(Player player) => !player.FargoSouls().MutantsDiscountCard;

    public virtual bool? UseItem(Player player)
    {
      player.FargoSouls().MutantsDiscountCard = true;
      return new bool?(true);
    }
  }
}
