// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.MagicalBulb
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class MagicalBulb : SoulsItem
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
      this.Item.rare = 7;
      this.Item.value = Item.sellPrice(0, 6, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      MagicalBulb.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.buffImmune[70] = true;
      player.buffImmune[ModContent.BuffType<IvyVenomBuff>()] = true;
      player.buffImmune[ModContent.BuffType<SwarmingBuff>()] = true;
      Point tileCoordinates = Utils.ToTileCoordinates(((Entity) player).Center);
      if (tileCoordinates.X > 0 && tileCoordinates.Y > 0 && tileCoordinates.X < Main.maxTilesX && tileCoordinates.Y < Main.maxTilesY && ((Entity) player).whoAmI == Main.myPlayer)
      {
        Color color = Lighting.GetColor(tileCoordinates);
        Vector3 vector3 = ((Color) ref color).ToVector3();
        float num = ((Vector3) ref vector3).Length() / 1.732f;
        if ((double) num < 1.0)
          num /= 2f;
        player.lifeRegen += (int) (6.0 * (double) num);
      }
      player.FargoSouls().MagicalBulb = true;
      player.AddEffect<PlantMinionEffect>(item);
    }
  }
}
