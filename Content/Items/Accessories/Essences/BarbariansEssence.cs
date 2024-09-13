// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Essences.BarbariansEssence
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Essences
{
  public class BarbariansEssence : BaseEssence
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color((int) byte.MaxValue, 111, 6);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Melee);
      local = StatModifier.op_Addition(local, 0.18f);
      player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
      player.GetCritChance(DamageClass.Melee) += 5f;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1304, 1).AddIngredient(1325, 1).AddIngredient(724, 1).AddIngredient(3281, 1).AddIngredient(4818, 1).AddIngredient(4144, 1).AddIngredient(5298, 1).AddIngredient(490, 1).AddIngredient(1225, 5).AddTile(114).Register();
    }
  }
}
