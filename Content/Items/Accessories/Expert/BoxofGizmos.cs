// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.BoxofGizmos
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class BoxofGizmos : SoulsItem
  {
    private int counter;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = -12;
      this.Item.value = Item.sellPrice(0, 1, 0, 0);
      this.Item.expert = true;
    }

    private void PassiveEffect(Player player)
    {
      player.FargoSouls().BoxofGizmos = true;
      if (((Entity) player).whoAmI == Main.myPlayer && player.FargoSouls().IsStandingStill && player.itemAnimation == 0)
      {
        if (++this.counter <= 60)
          return;
        player.detectCreature = true;
        if (this.counter % 10 != 0)
          return;
        Main.instance.SpelunkerProjectileHelper.AddSpotToCheck(((Entity) player).Center);
      }
      else
        this.counter = 0;
    }

    public virtual void UpdateInventory(Player player) => this.PassiveEffect(player);

    public virtual void UpdateVanity(Player player) => this.PassiveEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.PassiveEffect(player);
    }
  }
}
