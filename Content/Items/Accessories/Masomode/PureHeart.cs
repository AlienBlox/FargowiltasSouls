// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Masomode.PureHeart
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Masomode;
using FargowiltasSouls.Content.Items.Materials;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Masomode
{
  public class PureHeart : SoulsItem
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
      this.Item.rare = 6;
      this.Item.value = Item.sellPrice(0, 4, 0, 0);
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.PureHeart = true;
      player.buffImmune[ModContent.BuffType<RottingBuff>()] = true;
      player.moveSpeed += 0.1f;
      fargoSoulsPlayer.DarkenedHeartItem = this.Item;
      player.AddEffect<DarkenedHeartEaters>(this.Item);
      if (fargoSoulsPlayer.DarkenedHeartCD > 0)
        --fargoSoulsPlayer.DarkenedHeartCD;
      player.buffImmune[ModContent.BuffType<BloodthirstyBuff>()] = true;
      player.statLifeMax2 += player.statLifeMax / 10;
      player.AddEffect<GuttedHeartEffect>(this.Item);
      player.AddEffect<GuttedHeartMinions>(this.Item);
      player.FargoSouls().GelicWingsItem = this.Item;
      player.AddEffect<GelicWingJump>(this.Item);
      player.AddEffect<GelicWingSpikes>(this.Item);
      player.FargoSouls().WingTimeModifier += 0.3f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<DarkenedHeart>(), 1).AddIngredient(ModContent.ItemType<GuttedHeart>(), 1).AddIngredient(ModContent.ItemType<GelicWings>(), 1).AddIngredient(66, 30).AddIngredient(780, 50).AddIngredient(1006, 5).AddIngredient(ModContent.ItemType<DeviatingEnergy>(), 10).AddTile(134).Register();
    }
  }
}
