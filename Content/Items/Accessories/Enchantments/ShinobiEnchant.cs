// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ShinobiEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ShinobiEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(147, 91, 24);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 8;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ShinobiEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.AddEffect<ShinobiDashEffect>(item);
      player.AddEffect<ShinobiThroughWalls>(item);
      fargoSoulsPlayer.ShinobiEnchantActive = true;
      MonkEnchant.AddEffects(player, item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3880, 1).AddIngredient(3881, 1).AddIngredient(3882, 1).AddIngredient<MonkEnchant>(1).AddIngredient(3012, 1).AddIngredient(3106, 1).AddTile(125).Register();
    }
  }
}
