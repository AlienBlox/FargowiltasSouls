// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.TurtleEnchant
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
  public class TurtleEnchant : BaseEnchant
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(248, 156, 92);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 6;
      this.Item.value = 250000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      TurtleEnchant.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      fargoSoulsPlayer.CactusImmune = true;
      player.AddEffect<CactusEffect>(item);
      player.AddEffect<TurtleEffect>(item);
      player.turtleThorns = true;
      player.thorns = 1f;
      if (player.HasEffect<TurtleEffect>() && !player.HasBuff(ModContent.BuffType<BrokenShellBuff>()) && fargoSoulsPlayer.IsStandingStill && !player.controlUseItem && ((Entity) player).whoAmI == Main.myPlayer && !fargoSoulsPlayer.noDodge)
      {
        ++fargoSoulsPlayer.TurtleCounter;
        if (fargoSoulsPlayer.TurtleCounter > 20)
          player.AddBuff(ModContent.BuffType<ShellHideBuff>(), 2, true, false);
      }
      else
        fargoSoulsPlayer.TurtleCounter = 0;
      if (fargoSoulsPlayer.TurtleShellHP >= 20 || player.HasBuff(ModContent.BuffType<BrokenShellBuff>()) || fargoSoulsPlayer.ShellHide || !fargoSoulsPlayer.ForceEffect<TurtleEnchant>())
        return;
      --fargoSoulsPlayer.turtleRecoverCD;
      if (fargoSoulsPlayer.turtleRecoverCD > 0)
        return;
      fargoSoulsPlayer.turtleRecoverCD = 240;
      ++fargoSoulsPlayer.TurtleShellHP;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1316, 1).AddIngredient(1317, 1).AddIngredient(1318, 1).AddIngredient((Mod) null, "CactusEnchant", 1).AddIngredient(1228, 1).AddIngredient(3286, 1).AddTile(125).Register();
    }
  }
}
