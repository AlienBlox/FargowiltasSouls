// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MeteorEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MeteorEnchant : BaseEnchant
  {
    public const int METEOR_ADDED_DURATION = 450;

    public override Color nameColor => new Color(95, 71, 82);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 2;
      this.Item.value = 100000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      MeteorEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.AddEffect<MeteorMomentumEffect>(item);
      player.AddEffect<MeteorTrailEffect>(item);
      player.AddEffect<MeteorEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(123, 1).AddIngredient(124, 1).AddIngredient(125, 1).AddIngredient(197, 1).AddIngredient(5107, 1).AddIngredient(1485, 1).AddTile(26).Register();
    }
  }
}
