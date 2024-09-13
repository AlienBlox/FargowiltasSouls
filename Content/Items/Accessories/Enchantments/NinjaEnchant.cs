// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.NinjaEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Projectiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class NinjaEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(48, 49, 52);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 30000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.AddEffect<NinjaEffect>(this.Item);
    }

    public static void NinjaSpeedSetup(
      FargoSoulsPlayer modPlayer,
      Projectile projectile,
      FargoSoulsGlobalProjectile globalProj)
    {
      Player player = modPlayer.Player;
      float num = modPlayer.ForceEffect<NinjaEnchant>() ? 7f : 4f;
      if ((double) ((Vector2) ref ((Entity) player).velocity).Length() >= (double) num)
        return;
      globalProj.NinjaSpeedup = projectile.extraUpdates + 1;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(256, 1).AddIngredient(257, 1).AddIngredient(258, 1).AddIngredient(2277, 1).AddIngredient(42, 100).AddIngredient(279, 100).AddTile(26).Register();
    }
  }
}
