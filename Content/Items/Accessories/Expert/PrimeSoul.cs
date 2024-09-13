// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Expert.PrimeSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Content.Items.Placables;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Expert
{
  public class PrimeSoul : SoulsItem
  {
    public virtual bool IsLoadingEnabled(Mod mod) => false;

    public virtual string Texture => "FargowiltasSouls/Content/Items/Placeholder";

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

    private void PrimeSoulEffect(Player player)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      if (!player.AddEffect<FargowiltasSouls.Content.Items.Accessories.Expert.PrimeSoulEffect>(this.Item))
        return;
      fargoSoulsPlayer.PrimeSoulActive = fargoSoulsPlayer.PrimeSoulActiveBuffer = true;
    }

    public virtual void UpdateInventory(Player player) => this.PrimeSoulEffect(player);

    public virtual void UpdateVanity(Player player) => this.PrimeSoulEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      this.PrimeSoulEffect(player);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(ModContent.ItemType<BoxofGizmos>(), 1).AddIngredient(ModContent.ItemType<RustedOxygenTank>(), 1).AddIngredient(ModContent.ItemType<LifeRevitalizer>(), 1).AddIngredient(575, 3).AddIngredient(520, 3).AddIngredient(521, 3).AddIngredient(548, 3).AddIngredient(547, 3).AddIngredient(549, 3).AddTile<CrucibleCosmosSheet>().Register();
    }
  }
}
