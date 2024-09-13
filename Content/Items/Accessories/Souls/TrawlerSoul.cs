// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.TrawlerSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class TrawlerSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color(0, 238, 125);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.value = 750000;
    }

    protected override Color? nameColor => new Color?(TrawlerSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      TrawlerSoul.AddEffects(player, this.Item, hideVisual);
    }

    public static void AddEffects(Player player, Item item, bool hideVisual)
    {
      Player player1 = player;
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.FishSoul1 = true;
      fargoSoulsPlayer.FishSoul2 = true;
      player1.fishingSkill += 60;
      player1.sonarPotion = true;
      player1.cratePotion = true;
      player1.accFishingLine = true;
      player1.accTackleBox = true;
      player1.accFishFinder = true;
      player1.accLavaFishing = true;
      player.AddEffect<TrawlerGel>(item);
      player.AddEffect<TrawlerSporeSac>(item);
      player1.arcticDivingGear = true;
      player1.accFlipper = true;
      player1.accDivingHelm = true;
      player1.iceSkate = true;
      if (((Entity) player1).wet)
        Lighting.AddLight((int) ((Entity) player1).Center.X / 16, (int) ((Entity) player1).Center.Y / 16, 0.2f, 0.8f, 0.9f);
      player.AddEffect<TrawlerJump>(item);
      player1.jumpBoost = true;
      player1.noFallDmg = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "AnglerEnchant", 1).AddIngredient(3252, 1).AddIngredient(1861, 1).AddIngredient(4987, 1).AddIngredient(3336, 1).AddIngredient(2296, 1).AddIngredient(2294, 1).AddIngredient(2308, 1).AddIngredient(2341, 1).AddIngredient(3211, 1).AddIngredient(2331, 1).AddIngredient(2428, 1).AddIngredient(2491, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
