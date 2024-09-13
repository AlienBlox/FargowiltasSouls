// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.BerserkerSoul
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
  public class BerserkerSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color((int) byte.MaxValue, 111, 6);

    public override void SetStaticDefaults() => base.SetStaticDefaults();

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.defense = 4;
    }

    protected override Color? nameColor => new Color?(BerserkerSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ref StatModifier local = ref player.GetDamage(DamageClass.Melee);
      local = StatModifier.op_Addition(local, 0.3f);
      player.GetCritChance(DamageClass.Melee) += 15f;
      player.AddEffect<MeleeSpeedEffect>(this.Item);
      player.FargoSouls().MeleeSoul = true;
      player.AddEffect<MagmaStoneEffect>(this.Item);
      player.kbGlove = true;
      player.autoReuseGlove = true;
      player.meleeScaleGlove = true;
      player.counterWeight = 556 + Main.rand.Next(6);
      player.yoyoGlove = true;
      player.yoyoString = true;
      player.wolfAcc = true;
      player.accMerman = true;
      if (hideVisual)
      {
        player.hideMerman = true;
        player.hideWolf = true;
      }
      player.lifeRegen += 2;
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "BarbariansEssence", 1).AddIngredient(4007, 1).AddIngredient(3366, 1).AddIngredient(1343, 1).AddIngredient(3992, 1).AddIngredient(3110, 1).AddIngredient(1314, 1).AddIngredient(1306, 1).AddIngredient(4272, 1).AddIngredient(1571, 1).AddIngredient(3291, 1).AddIngredient(2611, 1).AddIngredient(3858, 1).AddIngredient(1947, 1).AddIngredient(4956, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
