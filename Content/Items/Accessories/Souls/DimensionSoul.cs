// Decompiled with JetBrains decompiler
// Type: FargowiltasSouls.Content.Items.Accessories.Souls.DimensionSoul
// Assembly: FargowiltasSouls, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A7A46DC-AE03-47A6-B5D0-CF3B5722B0BF
// Assembly location: C:\Users\Alien\OneDrive\文档\My Games\Terraria\tModLoader\ModSources\AlienBloxMod\Libraries\FargowiltasSouls.dll

using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

#nullable disable
namespace FargowiltasSouls.Content.Items.Accessories.Souls
{
  [AutoloadEquip]
  public class DimensionSoul : FlightMasteryWings
  {
    public override bool HasSupersonicSpeed => true;

    public override void SetStaticDefaults()
    {
      base.SetStaticDefaults();
      Main.RegisterItemAnimation(this.Item.type, (DrawAnimation) new DrawAnimationVertical(6, 30, false));
      ItemID.Sets.AnimatesAsSoul[this.Item.type] = true;
    }

    public override int NumFrames => 30;

    public override void SetDefaults()
    {
      ((Entity) this.Item).width = 32;
      ((Entity) this.Item).height = 32;
      this.Item.accessory = true;
      this.Item.defense = 15;
      this.Item.value = 5000000;
      this.Item.rare = -12;
      this.Item.expert = true;
      this.Item.useStyle = 4;
      this.Item.UseSound = new SoundStyle?(SoundID.Item6);
      this.Item.useTime = this.Item.useAnimation = 90;
    }

    public virtual bool? UseItem(Player player) => new bool?(true);

    public virtual void UseItemFrame(Player player)
    {
      if (player.itemTime != player.itemTimeMax / 2)
        return;
      player.Spawn((PlayerSpawnContext) 2);
      for (int index = 0; index < 70; ++index)
        Dust.NewDust(((Entity) player).position, ((Entity) player).width, ((Entity) player).height, 15, 0.0f, 0.0f, 150, new Color(), 1.5f);
    }

    public virtual void UpdateInventory(Player player)
    {
      player.accWatch = 3;
      player.accDepthMeter = 1;
      player.accCompass = 1;
      player.accFishFinder = true;
      player.accDreamCatcher = true;
      player.accOreFinder = true;
      player.accStopwatch = true;
      player.accCritterGuide = true;
      player.accJarOfSouls = true;
      player.accThirdEye = true;
      player.accCalendar = true;
      player.accWeatherRadio = true;
    }

    public virtual void UpdateAccessory(Player player, bool hideVisual)
    {
      player.FargoSouls();
      ColossusSoul.AddEffects(player, this.Item, 300, 0.2f, 8);
      SupersonicSoul.AddEffects(player, this.Item, hideVisual);
      FlightMasterySoul.AddEffects(player, this.Item);
      TrawlerSoul.AddEffects(player, this.Item, hideVisual);
      WorldShaperSoul.AddEffects(player, this.Item, hideVisual);
    }

    public virtual void AddRecipes()
    {
      this.CreateRecipe(1).AddIngredient((Mod) null, "ColossusSoul", 1).AddIngredient((Mod) null, "SupersonicSoul", 1).AddIngredient((Mod) null, "FlightMasterySoul", 1).AddIngredient((Mod) null, "TrawlerSoul", 1).AddIngredient((Mod) null, "WorldShaperSoul", 1).AddIngredient((Mod) null, "AbomEnergy", 10).AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet")).Register();
    }
  }
}
