// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Enchantments.BeeEnchant
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using FargowiltasSouls.Core.AccessoryEffectSystem;
using FargowiltasSouls.Core.ModPlayers;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Enchantments
{
  [AutoloadEquip]
  public class BeeEnchant : BaseEnchant
  {
    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      ArmorIDs.Wing.Sets.Stats[this.Item.wingSlot] = ArmorIDs.Wing.Sets.Stats[46];
    }

    public override Color nameColor => new Color(254, 246, 37);

    public override void SetDefaults()
    {
      base.SetDefaults();
      this.Item.rare = 3;
      this.Item.value = 50000;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      FargoSoulsPlayer fargoSoulsPlayer = player.FargoSouls();
      ArmorIDs.Wing.Sets.Stats[this.Item.wingSlot] = fargoSoulsPlayer.ForceEffect(new int?(this.Item.type)) ? ArmorIDs.Wing.Sets.Stats[15] : ArmorIDs.Wing.Sets.Stats[46];
      player.AddEffect<BeeEffect>(this.Item);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient(2361, 1).AddIngredient(2362, 1).AddIngredient(2363, 1).AddIngredient(3333, 1).AddIngredient(1121, 1).AddIngredient(2314, 1).AddTile(26).Register();
    }
  }
}
