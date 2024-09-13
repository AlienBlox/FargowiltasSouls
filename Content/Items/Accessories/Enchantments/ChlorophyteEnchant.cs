// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ChlorophyteEnchant
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
  public class ChlorophyteEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(36, 137, 0);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 6;
      this.Item.value = 150000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ChlorophyteEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      player.AddEffect<ChloroMinion>(item);
      player.FargoSouls().ChlorophyteEnchantActive = true;
      player.AddEffect<JungleJump>(item);
      player.AddEffect<JungleDashEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyChloroHead", 1).AddIngredient(1004, 1).AddIngredient(1005, 1).AddIngredient((Mod) null, "JungleEnchant", 1).AddIngredient(1234, 1).AddIngredient(1226, 1).AddTile(125).Register();
    }
  }
}
