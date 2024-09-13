// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.HuntressEnchant
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
  public class HuntressEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(122, 192, 76);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 6;
      this.Item.value = 200000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<HuntressEffect>(this.Item);
    }

    public static void HuntressBonus(
      FargoSoulsPlayer modPlayer,
      Projectile proj,
      NPC target,
      ref NPC.HitModifiers modifiers)
    {
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3803, 1).AddIngredient(3804, 1).AddIngredient(3805, 1).AddIngredient(725, 1).AddIngredient(3052, 1).AddIngredient(3854, 1).AddTile(125).Register();
    }
  }
}
