// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Consumables.DeerSinew
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;

#nullable disable
namespace FargowiltasSouls.Content.Items.Consumables
{
  public class DeerSinew : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.rare = 4;
      this.Item.maxStack = 99;
      this.Item.useStyle = 4;
      this.Item.useAnimation = 17;
      this.Item.useTime = 17;
      this.Item.consumable = true;
      this.Item.UseSound = new SoundStyle?(SoundID.Item2);
      this.Item.value = Item.sellPrice(0, 3, 0, 0);
    }

    public virtual bool CanUseItem(Player player) => !player.FargoSouls().DeerSinew;

    public virtual bool? UseItem(Player player)
    {
      if (player.itemAnimation > 0 && player.itemTime == 0)
        player.FargoSouls().DeerSinew = true;
      return new bool?(true);
    }
  }
}
