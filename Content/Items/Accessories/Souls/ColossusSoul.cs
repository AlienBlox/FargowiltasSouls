// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.ColossusSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Fargowiltas.Items.Tiles;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  public class ColossusSoul : BaseSoul
  {
    public static readonly Color ItemColor = new Color(252, 59, 0);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.defense = 10;
      this.Item.shieldSlot = 4;
    }

    protected override Color? nameColor => new Color?(ColossusSoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      ColossusSoul.AddEffects(player, this.Item, 0, 0.1f, 5);
    }

    public static void AddEffects(
      Player player,
      Item item,
      int maxHP,
      float damageResist,
      int lifeRegen)
    {
      Player player1 = player;
      player.FargoSouls().ColossusSoul = true;
      player1.statLifeMax2 += maxHP;
      player1.endurance += damageResist;
      player1.lifeRegen += lifeRegen;
      player1.buffImmune[46] = true;
      player1.buffImmune[47] = true;
      player1.buffImmune[156] = true;
      player1.buffImmune[33] = true;
      player1.buffImmune[36] = true;
      player1.buffImmune[30] = true;
      player1.buffImmune[20] = true;
      player1.buffImmune[32] = true;
      player1.buffImmune[31] = true;
      player1.buffImmune[35] = true;
      player1.buffImmune[23] = true;
      player1.buffImmune[22] = true;
      player1.noKnockback = true;
      player1.fireWalk = true;
      player1.noFallDmg = true;
      player.AddEffect<DefenseBrainEffect>(item);
      player1.pStone = true;
      player.AddEffect<DefenseStarEffect>(item);
      player.AddEffect<DefenseBeeEffect>(item);
      player1.longInvince = true;
      player1.shinyStone = true;
      player.AddEffect<FleshKnuckleEffect>(item);
      player.AddEffect<FrozenTurtleEffect>(item);
      player.AddEffect<PaladinShieldEffect>(item);
      player.AddEffect<ShimmerImmunityEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(1921, 1).AddIngredient(396, 1).AddIngredient(3224, 1).AddIngredient(3223, 1).AddIngredient(860, 1).AddIngredient(1247, 1).AddIngredient(862, 1).AddIngredient(3337, 1).AddIngredient(3998, 1).AddIngredient(3997, 1).AddIngredient(1613, 1).AddIngredient(5355, 1).AddTile<CrucibleCosmosSheet>().Register();
    }
  }
}
