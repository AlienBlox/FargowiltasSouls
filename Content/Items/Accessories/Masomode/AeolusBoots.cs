// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.AeolusBoots
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  [AutoloadEquip]
  public class AeolusBoots : SoulsItem
  {
    public virtual void SetStaticDefaults()
    {
      CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[this.Type] = 1;
    }

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 6;
      this.Item.value = Item.sellPrice(0, 20, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.accRunSpeed = 6.75f;
      player.rocketBoots = player.vanityRocketBoots = 4;
      player.moveSpeed += 0.08f;
      player.iceSkate = true;
      player.waterWalk = true;
      player.fireWalk = true;
      player.lavaMax += 420;
      player.lavaRose = true;
      player.AddEffect<MasoAeolusFrog>(this.Item);
      player.AddEffect<MasoAeolusFlower>(this.Item);
      player.AddEffect<ZephyrJump>(this.Item);
      player.desertBoots = true;
      player.jumpBoost = true;
      player.noFallDmg = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<ZephyrBoots>(), 1).AddIngredient(3990, 1).AddIngredient(3993, 1).AddIngredient(4055, 1).AddIngredient(547, 5).AddIngredient(548, 5).AddIngredient(549, 5).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(134).Register();
    }
  }
}
