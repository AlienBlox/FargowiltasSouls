// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.RichMahoganyEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class RichMahoganyEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(181, 108, 100);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 10000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<MahoganyEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(733, 1).AddIngredient(734, 1).AddIngredient(735, 1).AddIngredient(314, 1).AddIngredient(4294, 1).AddIngredient(84, 1).AddTile(26).Register();
    }
  }
}
