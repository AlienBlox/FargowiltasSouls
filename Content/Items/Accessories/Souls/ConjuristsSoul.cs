// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.ConjuristsSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class ConjuristsSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color(0, (int) byte.MaxValue, (int) byte.MaxValue);

    protected override Color? nameColor => new Color?(ConjuristsSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().SummonSoul = true;
      ref StatModifier local1 = ref player.GetDamage(DamageClass.Summon);
      local1 = StatModifier.op_Addition(local1, 0.3f);
      player.maxMinions += 5;
      player.maxTurrets += 2;
      ref StatModifier local2 = ref player.GetKnockback(DamageClass.Summon);
      local2 = StatModifier.op_Addition(local2, 3f);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "OccultistsEssence", 1).AddIngredient(3812, 1).AddIngredient(3810, 1).AddIngredient(3811, 1).AddIngredient(3809, 1).AddIngredient(1158, 1).AddIngredient(1864, 1).AddIngredient(4758, 1).AddIngredient(2584, 1).AddIngredient(2535, 1).AddIngredient(3249, 1).AddIngredient(4607, 1).AddIngredient(1572, 1).AddIngredient(2621, 1).AddIngredient(1802, 1).AddIngredient(2749, 1).AddIngredient(5005, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
