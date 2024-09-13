// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.FrigidGemstoneInactive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class FrigidGemstoneInactive : SoulsItem
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
      this.Item.accessory = true;
      this.Item.rare = 5;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
    }

    private void Effects(Player player)
    {
      player.buffImmune[44] = true;
      player.buffImmune[46] = true;
      player.FargoSouls().FrigidGemstoneItem = this.Item;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual) => this.Effects(player);

    public virtual void UpdateVanity(Player player) => this.Effects(player);

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      player.ReplaceItem(this.Item, ModContent.ItemType<FrigidGemstone>());
    }
  }
}
