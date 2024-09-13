// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Consumables.MutantsCreditCard
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.Audio;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Consumables
{
  public class MutantsCreditCard : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 9;
      this.Item.maxStack = Item.CommonMaxStack;
      this.Item.useStyle = 4;
      this.Item.useAnimation = 17;
      this.Item.useTime = 17;
      this.Item.consumable = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Roar);
      this.Item.value = Item.sellPrice(0, 10, 0, 0);
    }

    public virtual bool CanUseItem(Player player) => !player.FargoSouls().MutantsCreditCard;

    public virtual bool? UseItem(Player player)
    {
      player.FargoSouls().MutantsCreditCard = true;
      return new bool?(true);
    }
  }
}
