// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.FlightMasterySoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Content.Items.Accessories.Masomode;
using FargowiltasSouls.Core.AccessoryEffectSystem;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  [AutoloadEquip]
  public class FlightMasterySoul : FlightMasteryWings
  {
    public static readonly Color ItemColor = new Color(56, 134, (int) byte.MaxValue);

    public override bool HasSupersonicSpeed => false;

    protected override Color? nameColor => new Color?(FlightMasterySoul.ItemColor);

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FlightMasterySoul.AddEffects(player, this.Item);
    }

    public static void AddEffects(Player player, Item item)
    {
      Player player1 = player;
      player.FargoSouls().FlightMasterySoul = true;
      player1.wingTimeMax = 999999;
      player1.wingTime = (float) player1.wingTimeMax;
      player1.ignoreWater = true;
      player.AddEffect<FlightMasteryInsignia>(item);
      player.AddEffect<FlightMasteryGravity>(item);
      if (item.ModItem != null && item.ModItem is FlightMasteryWings modItem && modItem.HasSupersonicSpeed)
        player.AddEffect<SupersonicSpeedEffect>(item);
      if (player1.controlDown && player1.controlJump && !player1.mount.Active)
      {
        ((Entity) player1).position.Y -= ((Entity) player1).velocity.Y;
        if ((double) ((Entity) player1).velocity.Y > 0.10000000149011612)
          ((Entity) player1).velocity.Y = 0.1f;
        else if ((double) ((Entity) player1).velocity.Y < -0.10000000149011612)
          ((Entity) player1).velocity.Y = -0.1f;
      }
      player.AddEffect<MasoGravEffect>(item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(4989, 1).AddIngredient(1165, 1).AddIngredient(4978, 1).AddIngredient(761, 1).AddIngredient(785, 1).AddIngredient(786, 1).AddIngredient(822, 1).AddIngredient(821, 1).AddIngredient(1797, 1).AddIngredient(1871, 1).AddIngredient(3883, 1).AddIngredient(2609, 1).AddIngredient(4823, 1).AddIngredient(4954, 1).AddIngredient(1131, 1).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
