// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Essences.ApprenticesEssence
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Essences
{
  public class ApprenticesEssence : BaseEssence
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color((int) byte.MaxValue, 83, (int) byte.MaxValue);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Magic);
      local = StatModifier.op_Addition(local, 0.18f);
      player.GetCritChance(DamageClass.Magic) += 5f;
      player.statManaMax2 += 50;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(5147, 1).AddIngredient(4347, 1).AddRecipeGroup("FargowiltasSouls:VilethornOrCrimsonRod", 1).AddIngredient(5118, 1).AddIngredient(165, 1).AddIngredient(218, 1).AddIngredient(272, 1).AddIngredient(489, 1).AddIngredient(1225, 5).AddTile(114).Register();
    }
  }
}
