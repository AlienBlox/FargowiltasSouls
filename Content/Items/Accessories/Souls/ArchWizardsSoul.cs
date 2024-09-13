// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.ArchWizardsSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class ArchWizardsSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color((int) byte.MaxValue, 83, (int) byte.MaxValue);

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    protected override Color? nameColor => new Color?(ArchWizardsSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().MagicSoul = true;
      ref StatModifier local = ref player.GetDamage(DamageClass.Magic);
      local = StatModifier.op_Addition(local, 0.3f);
      player.GetCritChance(DamageClass.Magic) += 15f;
      player.statManaMax2 += 200;
      player.manaFlower = true;
      player.manaMagnet = true;
      player.magicCuffs = true;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "ApprenticesEssence", 1).AddIngredient(4001, 1).AddIngredient(4000, 1).AddIngredient(3991, 1).AddIngredient(2221, 1).AddIngredient(2220, 1).AddIngredient(3269, 1).AddIngredient(4270, 1).AddIngredient(1266, 1).AddIngredient(1260, 1).AddIngredient(3870, 1).AddIngredient(4715, 1).AddIngredient(2622, 1).AddIngredient(2795, 1).AddIngredient(3541, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
