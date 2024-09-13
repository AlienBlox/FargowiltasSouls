// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.DarkenedHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class DarkenedHeart : SoulsItem
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
      this.Item.rare = 2;
      this.Item.value = Item.sellPrice(0, 2, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.buffImmune[ModContent.BuffType<RottingBuff>()] = true;
      player.moveSpeed += 0.1f;
      fargoSoulsPlayer.DarkenedHeartItem = this.Item;
      player.AddEffect<DarkenedHeartEaters>(this.Item);
      if (fargoSoulsPlayer.DarkenedHeartCD <= 0)
        return;
      --fargoSoulsPlayer.DarkenedHeartCD;
    }
  }
}
