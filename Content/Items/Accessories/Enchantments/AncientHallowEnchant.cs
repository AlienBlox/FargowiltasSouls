// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.AncientHallowEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles.Minions;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class AncientHallowEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(150, 133, 100);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 6;
      this.Item.value = 180000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      AncientHallowEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      bool toggle = player.AddEffect<AncientHallowMinion>(item);
      int damage = fargoSoulsPlayer.ForceEffect<AncientHallowEnchant>() ? 600 : 350;
      fargoSoulsPlayer.AddMinion(item, toggle, ModContent.ProjectileType<HallowSword>(), damage, 2f);
    }

    public static Color GetFairyQueenWeaponsColor(
      float alphaChannelMultiplier,
      float lerpToWhite,
      float rawHueOverride)
    {
      double num1 = ((double) rawHueOverride + 0.5) % 1.0;
      float num2 = 1f;
      float num3 = 0.5f;
      double num4 = (double) num2;
      double num5 = (double) num3;
      Color queenWeaponsColor = Main.hslToRgb((float) num1, (float) num4, (float) num5, byte.MaxValue);
      if ((double) lerpToWhite != 0.0)
        queenWeaponsColor = Color.Lerp(queenWeaponsColor, Color.White, lerpToWhite);
      ((Color) ref queenWeaponsColor).A = (byte) ((double) ((Color) ref queenWeaponsColor).A * (double) alphaChannelMultiplier);
      return queenWeaponsColor;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyAncientHallowHead", 1).AddIngredient(4900, 1).AddIngredient(4901, 1).AddIngredient(495, 1).AddIngredient(4678, 1).AddIngredient(422, 50).AddTile(125).Register();
    }
  }
}
