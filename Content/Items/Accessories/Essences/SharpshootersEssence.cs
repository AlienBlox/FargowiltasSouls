// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Essences.SharpshootersEssence
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Essences
{
  public class SharpshootersEssence : BaseEssence
  {
    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override Color nameColor => new Color(188, 253, 68);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Ranged);
      local = StatModifier.op_Addition(local, 0.18f);
      player.GetCritChance(DamageClass.Ranged) += 5f;
      player.FargoSouls().RangedEssence = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(3350, 1).AddIngredient(1319, 1).AddIngredient(4381, 1).AddIngredient(160, 1).AddIngredient(219, 1).AddIngredient(2888, 1).AddIngredient(3019, 1).AddIngredient(491, 1).AddIngredient(1225, 5).AddTile(114).Register();
    }
  }
}
