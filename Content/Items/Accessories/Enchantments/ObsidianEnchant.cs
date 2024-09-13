// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.ObsidianEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Buffs.Souls;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  public class ObsidianEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(69, 62, 115);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 2;
      this.Item.value = 50000;
    }

    public virtual void UpdateInventory(Player player) => AshWoodEnchant.PassiveEffect(player);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ObsidianEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      player.AddEffect<AshWoodEffect>(item);
      player.AddEffect<AshWoodFireballs>(item);
      player.AddEffect<ObsidianEffect>(item);
      player.lavaImmune = true;
      player.fireWalk = true;
      if (((Entity) player).lavaWet)
      {
        player.gravity = Player.defaultGravity;
        player.ignoreWater = true;
        player.accFlipper = true;
        player.AddBuff(ModContent.BuffType<ObsidianLavaWetBuff>(), 600, true, false);
      }
      if (fargoSoulsPlayer.ObsidianCD > 0)
        --fargoSoulsPlayer.ObsidianCD;
      if (!fargoSoulsPlayer.ForceEffect<ObsidianEnchant>() && !((Entity) player).lavaWet && !fargoSoulsPlayer.LavaWet)
        return;
      player.AddEffect<ObsidianProcEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3266, 1).AddIngredient(3267, 1).AddIngredient(3268, 1).AddIngredient(4003, 1).AddIngredient((Mod) null, "AshWoodEnchant", 1).AddTile(26).Register();
    }
  }
}
