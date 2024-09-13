// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.MinerEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class MinerEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(95, 117, 151);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 1;
      this.Item.value = 20000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      float pickSpeed = player.FargoSouls().ForceEffect<MinerEnchant>() ? 0.75f : 0.5f;
      MinerEnchant.AddEffects(player, pickSpeed, this.Item);
    }

    public static void AddEffects(Player player, float pickSpeed, Item item)
    {
      player.pickSpeed -= pickSpeed;
      player.nightVision = true;
      player.AddEffect<MiningSpelunk>(item);
      player.AddEffect<MiningHunt>(item);
      player.AddEffect<MiningDanger>(item);
      player.AddEffect<MiningShine>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4008, 1).AddIngredient(410, 1).AddIngredient(411, 1).AddIngredient(4056, 1).AddIngredient(3509, 1).AddIngredient(4711, 1).AddTile(26).Register();
    }
  }
}
