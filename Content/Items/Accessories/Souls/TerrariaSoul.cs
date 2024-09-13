// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.TerrariaSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Enchantments;
using FargowiltasSouls.Content.Items.Accessories.Forces;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  [AutoloadEquip]
  public class TerrariaSoul : BaseSoul
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 24, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }

    public virtual bool PreDrawTooltipLine(DrawableTooltipLine line, ref int yOffset)
    {
      if (!(((TooltipLine) line).Mod == "Terraria") || !(((TooltipLine) line).Name == "ItemName"))
        return true;
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 1, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      GameShaders.Armor.Apply(GameShaders.Armor.GetShaderIdFromItemId(2870), (Entity) this.Item, new DrawData?());
      Utils.DrawBorderString(Main.spriteBatch, line.Text, new Vector2((float) line.X, (float) line.Y), Color.White, 1f, 0.0f, 0.0f, -1);
      Main.spriteBatch.End();
      Main.spriteBatch.Begin((SpriteSortMode) 0, (BlendState) null, (SamplerState) null, (DepthStencilState) null, (RasterizerState) null, (Effect) null, Main.UIScaleMatrix);
      return false;
    }

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 5000000;
      this.Item.rare = -12;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().TerrariaSoul = true;
      ((ModItem) ModContent.GetInstance<TimberForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<TerraForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<EarthForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<NatureForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<LifeForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<SpiritForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<ShadowForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<WillForce>()).UpdateAccessory(player, hideVisual);
      ((ModItem) ModContent.GetInstance<CosmoForce>()).UpdateAccessory(player, hideVisual);
    }

    public virtual void UpdateVanity(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
      player.AddEffect<GoldToPiggy>(this.Item);
    }

    public virtual void UpdateInventory(Player player)
    {
      player.FargoSouls().WoodEnchantDiscount = true;
      player.AddEffect<GoldToPiggy>(this.Item);
      AshWoodEnchant.PassiveEffect(player);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "TimberForce", 1).AddIngredient((Mod) null, "TerraForce", 1).AddIngredient((Mod) null, "EarthForce", 1).AddIngredient((Mod) null, "NatureForce", 1).AddIngredient((Mod) null, "LifeForce", 1).AddIngredient((Mod) null, "SpiritForce", 1).AddIngredient((Mod) null, "ShadowForce", 1).AddIngredient((Mod) null, "WillForce", 1).AddIngredient((Mod) null, "CosmoForce", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
