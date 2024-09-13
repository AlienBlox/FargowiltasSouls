// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Essences.OccultistsEssence
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Essences
{
  public class OccultistsEssence : BaseEssence
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(0, (int) byte.MaxValue, (int) byte.MaxValue);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Summon);
      local = StatModifier.op_Addition(local, 0.18f);
      ++player.maxMinions;
      ++player.maxTurrets;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4281, 1).AddIngredient(1309, 1).AddIngredient(4273, 1).AddIngredient(5119, 1).AddIngredient(2364, 1).AddIngredient(4913, 1).AddIngredient(2365, 1).AddIngredient(2998, 1).AddIngredient(1225, 5).AddTile(114).Register();
    }
  }
}
