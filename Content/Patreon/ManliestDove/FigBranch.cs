// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Patreon.ManliestDove.FigBranch
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Patreon.ManliestDove
{
  public class FigBranch : PatreonModItem
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public virtual void SetDefaults()
    {
      this.Item.CloneDefaults(2420);
      this.Item.shoot = ModContent.ProjectileType<DoveProj>();
      this.Item.buffType = ModContent.BuffType<DoveBuff>();
    }

    public virtual void UseStyle(Player player, Rectangle heldItemFrame)
    {
      if (((Entity) player).whoAmI != Main.myPlayer || player.itemTime != 0)
        return;
      player.AddBuff(this.Item.buffType, 3600, true, false);
    }

    public virtual void AddRecipes()
    {
      if (!SoulConfig.Instance.PatreonDove)
        return;
      this.CreateRecipe(1).AddRecipeGroup("FargowiltasSouls:AnyBird", 1).AddIngredient(9, 50).AddIngredient(2503, 50).AddIngredient(620, 50).AddIngredient(2504, 50).AddIngredient(619, 50).AddIngredient(911, 50).AddTile(304).Register();
    }
  }
}
