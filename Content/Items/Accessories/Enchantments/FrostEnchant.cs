// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.FrostEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class FrostEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(122, 189, 185);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<FrostEffect>(this.Item);
      player.AddEffect<SnowEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(684, 1).AddIngredient(685, 1).AddIngredient(686, 1).AddIngredient(ModContent.ItemType<SnowEnchant>(), 1).AddIngredient(676, 1).AddIngredient(726, 1).AddTile(125).Register();
    }
  }
}
