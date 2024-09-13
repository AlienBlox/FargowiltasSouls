﻿// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ValhallaKnightEnchant
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
  public class ValhallaKnightEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(147, 101, 30);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 8;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().ValhallaEnchantActive = true;
      player.AddEffect<ValhallaDash>(this.Item);
      SquireEnchant.SquireEffect(player, this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3871, 1).AddIngredient(3872, 1).AddIngredient(3873, 1).AddIngredient(879, 1).AddIngredient((Mod) null, "SquireEnchant", 1).AddIngredient(4789, 1).AddTile(125).Register();
    }
  }
}