// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ForbiddenEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ForbiddenEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(231, 178, 28);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 5;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ForbiddenEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      if (!player.AddEffect<ForbiddenEffect>(item))
        return;
      Lighting.AddLight(((Entity) player).Center, 0.8f, 0.7f, 0.2f);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3776, 1).AddIngredient(3777, 1).AddIngredient(3778, 1).AddIngredient(3770, 1).AddIngredient(3779, 1).AddIngredient(3787, 1).AddTile(125).Register();
    }
  }
}
