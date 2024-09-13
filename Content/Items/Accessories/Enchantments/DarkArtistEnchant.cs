// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.DarkArtistEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class DarkArtistEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(155, 92, 176);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 8;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.ApprenticeEnchantActive = true;
      fargoSoulsPlayer.DarkArtistEnchantActive = true;
      player.AddEffect<ApprenticeSupport>(this.Item);
      player.AddEffect<DarkArtistMinion>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3874, 1).AddIngredient(3875, 1).AddIngredient(3876, 1).AddIngredient((Mod) null, "ApprenticeEnchant", 1).AddIngredient(3820, 1).AddIngredient(1445, 1).AddTile(125).Register();
    }
  }
}
