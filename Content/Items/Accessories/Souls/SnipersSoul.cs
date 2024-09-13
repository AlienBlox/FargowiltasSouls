// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.SnipersSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class SnipersSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color(188, 253, 68);

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    protected override Color? nameColor => new Color?(SnipersSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls().RangedSoul = true;
      ref StatModifier local = ref player.GetDamage(DamageClass.Ranged);
      local = StatModifier.op_Addition(local, 0.3f);
      player.GetCritChance(DamageClass.Ranged) += 15f;
      player.AddEffect<SniperScopeEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "SharpshootersEssence", 1).AddIngredient(4002, 1).AddIngredient(4006, 1).AddIngredient(4005, 1).AddIngredient(3007, 1).AddIngredient(533, 1).AddIngredient(2223, 1).AddIngredient(3107, 1).AddIngredient(1156, 1).AddIngredient(1254, 1).AddIngredient(2624, 1).AddIngredient(1835, 1).AddIngredient(1910, 1).AddIngredient(2797, 1).AddIngredient(3930, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
