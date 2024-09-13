// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.GelicWings
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class GelicWings : SoulsItem
  {
    public override bool Eternity => true;

    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
      ArmorIDs.Wing.Sets.Stats[this.Item.wingSlot] = new WingStats(100, -1f, 1f, false, -1f, 1f);
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 28;
      this.Item.accessory = true;
      this.Item.rare = 5;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.buffImmune[ModContent.BuffType<FlippedHallowBuff>()] = true;
      player.FargoSouls().GelicWingsItem = this.Item;
      player.AddEffect<GelicWingJump>(this.Item);
      player.AddEffect<GelicWingSpikes>(this.Item);
    }

    public virtual void VerticalWingSpeeds(
      Player player,
      ref float ascentWhenFalling,
      ref float ascentWhenRising,
      ref float maxCanAscendMultiplier,
      ref float maxAscentMultiplier,
      ref float constantAscend)
    {
      ascentWhenFalling = 0.5f;
      ascentWhenRising = 0.1f;
      maxCanAscendMultiplier = 0.5f;
      maxAscentMultiplier = 1.5f;
      constantAscend = 0.1f;
    }

    public virtual void HorizontalWingSpeeds(
      Player player,
      ref float speed,
      ref float acceleration)
    {
      speed = 6.75f;
      acceleration = 0.185f;
    }
  }
}
