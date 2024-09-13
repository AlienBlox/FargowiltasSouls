// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.Potato.RazorContainer
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.Potato
{
  public class RazorContainer : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      ((Entity) this.Item).width = 20;
      ((Entity) this.Item).height = 20;
      this.Item.accessory = true;
      this.Item.rare = 1;
      this.Item.value = 10000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.GetModPlayer<PatreonPlayer>().RazorContainer = true;
      if (player.ownedProjectileCounts[ModContent.ProjectileType<RazorBlade>()] >= 1)
        return;
      Projectile.NewProjectile(player.GetSource_Accessory(this.Item, (string) null), ((Entity) player).Center, new Vector2((float) Main.rand.Next(-2, 2), -2f), ModContent.ProjectileType<RazorBlade>(), 15, 2f, ((Entity) player).whoAmI, 0.0f, 0.0f, 0.0f);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddRecipeGroup(RecipeGroupID.Wood, 10).AddRecipeGroup(RecipeGroupID.IronBar, 15).AddIngredient(85, 6).AddTile(16).Register();
    }
  }
}
