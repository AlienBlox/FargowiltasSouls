// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.RainEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class RainEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color((int) byte.MaxValue, 236, 0);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      RainEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.buffImmune[103] = true;
      player.AddEffect<RainUmbrellaEffect>(item);
      player.AddEffect<RainInnerTubeEffect>(item);
      player.AddEffect<RainWetEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1135, 1).AddIngredient(1136, 1).AddIngredient(1243, 1).AddIngredient(4404, 1).AddIngredient(946, 1).AddIngredient(2272, 1).AddTile(26).Register();
    }
  }
}
