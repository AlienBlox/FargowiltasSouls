// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.DanielTheRobot.ROBGyro
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.DanielTheRobot
{
  public class ROBGyro : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(2420);
      this.Item.shoot = ModContent.ProjectileType<ROB>();
      this.Item.buffType = ModContent.BuffType<ROBBuff>();
    }

    public virtual void UseStyle(Player player, Rectangle heldItemFrame)
    {
      base.UseStyle(player, heldItemFrame);
      if (((Entity) player).whoAmI != Main.myPlayer || player.itemTime != 0)
        return;
      player.AddBuff(this.Item.buffType, 3600, true, false);
    }

    public virtual void AddRecipes()
    {
      if (!SoulConfig.Instance.PatreonROB)
        return;
      this.CreateRecipe(1).AddIngredient(548, 5).AddRecipeGroup("IronBar", 10).AddIngredient(530, 5).AddTile(134).Register();
    }
  }
}
