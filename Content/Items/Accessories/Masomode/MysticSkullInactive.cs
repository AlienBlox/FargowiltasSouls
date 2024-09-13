// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.MysticSkullInactive
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class MysticSkullInactive : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 0;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 5;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
    }

    private static void Effects(Player player)
    {
      player.buffImmune[68] = true;
      player.manaMagnet = true;
      player.manaFlower = true;
    }

    public virtual void UpdateVanity(Player player) => MysticSkullInactive.Effects(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      MysticSkullInactive.Effects(player);
    }

    public virtual bool CanRightClick() => true;

    public virtual void RightClick(Player player)
    {
      player.ReplaceItem(this.Item, ModContent.ItemType<MysticSkull>());
    }
  }
}
